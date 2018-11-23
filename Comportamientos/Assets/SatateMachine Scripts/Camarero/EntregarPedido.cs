using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntregarPedido : StateMachineBehaviour
{

    public WaiterAgent waiter;
    private CheckPoint checkPoint;
    private float times;
    private float looking;
    private bool entregando;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        looking = 0;
        times = 1;
        entregando = false;
        Vector3 closePoint = ClosePoint();
        if (!waiter.CalculateNavPos(closePoint))
        {
            animator.SetTrigger("Idle");
        }
    }

    private Vector3 ClosePoint()
    {
        Vector3 v = waiter.world.barraPedidos[0].transform.position;
        float d = Vector3.Distance(waiter.transform.position, v);
        checkPoint = waiter.world.barraPedidos[0];

        for (int i = 1; i < waiter.world.barraPedidos.Count; i++)
        {
            if (Vector3.Distance(waiter.transform.position, waiter.world.barraPedidos[i].transform.position) < d && !waiter.world.barraPedidos[i].ocupado)
            {
                v = waiter.world.barraPedidos[i].transform.position;
                d = Vector3.Distance(waiter.transform.position, v);
                checkPoint = waiter.world.barraPedidos[i];
            }
        }
        checkPoint.ocupado = true;
        return v;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (waiter.agent.remainingDistance <= waiter.agent.stoppingDistance && !waiter.agent.isStopped)
        {
            waiter.agent.isStopped = true;
        }
        else if (waiter.agent.isStopped && looking < 1)
        {
            waiter.LookAt(checkPoint.transform.forward, looking);
            looking += Time.deltaTime;
        }
        else if (waiter.agent.isStopped && looking >= 1 && !entregando)
        {
            animator.SetTrigger("Dejar");
            Debug.Log("Dejando");
            entregando = true;
        }
        else if (waiter.agent.isStopped && looking >= 1 && !animator.IsInTransition(0)&& animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            checkPoint.ocupado = false;
            animator.SetTrigger("Idle");
            animator.SetTrigger("FinDejar");
            waiter.world.Notify(new Task("Cocinar", null, waiter, Task.Receptor.Cocinero,waiter.currentTask.Coordinates));
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
