using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cocinar : StateMachineBehaviour {

    public CookerAgent cooker;
    private CheckPoint checkPoint;
    float looking;
    bool cocinando;
    float times;
    int cocinarRandom;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        System.Random r = new System.Random();
        cocinarRandom = r.Next(1, 3);
        looking = 0;
        cocinando = false;
        times = 0;
        Vector3 closePoint = ColsePoint();

        if (!cooker.CalculateNavPos(closePoint))
        {
            animator.SetTrigger("Idle");
        }
    }

    private Vector3 ColsePoint()
    {
        Vector3 v = cooker.world.cocina[0].transform.position;
        float d = Vector3.Distance(cooker.transform.position, v);
        checkPoint = cooker.world.cocina[0];

        for (int i = 1; i < cooker.world.cocina.Count; i++)
        {
            if (Vector3.Distance(cooker.transform.position, cooker.world.cocina[i].transform.position) < d && !cooker.world.cocina[i].ocupado)
            {
                v = cooker.world.cocina[i].transform.position;
                d = Vector3.Distance(cooker.transform.position, v);
                checkPoint = cooker.world.cocina[i];
            }
        }
        checkPoint.ocupado = true;
        return v;
    }

    /// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (cooker.agent.remainingDistance <= cooker.agent.stoppingDistance && !cooker.agent.isStopped)
        {
            cooker.agent.isStopped = true;
        }
        else if (cooker.agent.isStopped && looking < 1)
        {
            cooker.LookAt(Vector3.zero, looking);
            looking += Time.deltaTime;
        }
        else if (cooker.agent.isStopped && looking >= 1 && !cocinando)
        {
            animator.SetTrigger("Cocinar"+cocinarRandom);
            Debug.Log("Cocinando");
            cocinando = true;
        }
        else if (cooker.agent.isStopped && !animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            if (times == 2)
            {
                checkPoint.ocupado = false;
                animator.SetTrigger("Entregar");
                animator.SetTrigger("FinCocinar");
            }
            else
            {
                animator.SetTrigger("Cocinar"+cocinarRandom);
                times++;
            }
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
