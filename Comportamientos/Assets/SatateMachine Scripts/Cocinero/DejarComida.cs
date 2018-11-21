using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DejarComida : StateMachineBehaviour {

    public CookerAgent cooker;
    public CheckPoint checkPoint;
    float looking;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cooker.agent.isStopped = false;
        looking = 0;
        Vector3 closePoint = ColsePoint();

        if (!cooker.CalculateNavPos(closePoint))
        {
            animator.SetTrigger("Idle");
        }
    }

    private Vector3 ColsePoint()
    {
        Vector3 v = cooker.world.barraCocina[0].transform.position;
        float d = Vector3.Distance(cooker.transform.position, v);
        checkPoint = cooker.world.barraCocina[0];

        for (int i = 1; i < cooker.world.barraCocina.Count; i++)
        {
            if (Vector3.Distance(cooker.transform.position, cooker.world.barraCocina[i].transform.position) < d && !cooker.world.barraCocina[i].ocupado)
            {
                v = cooker.world.barraCocina[i].transform.position;
                d = Vector3.Distance(cooker.transform.position, v);
                checkPoint = cooker.world.barraCocina[i];
            }
        }
        checkPoint.ocupado = true;
        return v;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (cooker.agent.remainingDistance <= cooker.agent.stoppingDistance && !cooker.agent.isStopped)
        {
            cooker.agent.isStopped = true;
            //notify Cocinero.
        }
        else if (cooker.agent.isStopped && looking < 1)
        {
            cooker.LookAt(checkPoint.transform.forward, looking);
            looking += Time.deltaTime;
        }
        else if (cooker.agent.isStopped && looking >= 1 /*&& !animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1*/)
        {
            checkPoint.ocupado = false;
            animator.SetTrigger("Idle");
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
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
