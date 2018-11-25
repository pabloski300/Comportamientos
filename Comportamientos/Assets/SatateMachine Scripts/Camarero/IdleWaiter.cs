using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleWaiter : StateMachineBehaviour {

    public WaiterAgent waiter;
    float looking;
    float time;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        time = 0;
        looking = 0;
        waiter.Completed();
        waiter.agent.isStopped = false;

        if (!waiter.CalculateNavPos(waiter.startPosition))
        {
            animator.SetTrigger("Idle");
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (time <= 0.1f)
        {
            if (Vector3.Distance(waiter.transform.position,waiter.startPosition) <= waiter.agent.stoppingDistance && !waiter.agent.isStopped)
            {
                waiter.agent.isStopped = true;
                Debug.Log("Parado");
            }
            else if (waiter.agent.isStopped && looking < 1)
            {
                waiter.LookAt(waiter.startForward, looking);
                looking += Time.deltaTime;
            }
            else if (waiter.currentTask != null)
            {
                animator.SetTrigger(waiter.currentTask.Id);
            }
        }
        else
        {
            time += Time.deltaTime;
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
