using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleCooker : StateMachineBehaviour {

    public CookerAgent cooker;
    float looking;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        looking = 0;
        cooker.Completed();
        NavMeshHit navPos;
        if (NavMesh.SamplePosition(cooker.startPosition, out navPos, 100, -1))
        {
            cooker.agent.isStopped = false;
            cooker.agent.SetDestination(navPos.position);
        }
        else
        {
            animator.SetTrigger("Idle");
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (cooker.agent.remainingDistance <= cooker.agent.stoppingDistance && !cooker.agent.isStopped)
        {
            cooker.agent.isStopped = true;
            Debug.Log("Lavando");
        }
        else if (cooker.agent.isStopped && looking < 1)
        {
            cooker.LookAt(cooker.startForward, looking);
            looking += Time.deltaTime;
        }
        else if (cooker.currentTask != null)
        {
            animator.SetTrigger(cooker.currentTask.Id);
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
