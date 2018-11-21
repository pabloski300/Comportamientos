using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleWaiter : StateMachineBehaviour {

    public WaiterAgent waiter;
    bool arrived;
    float looking;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        looking = 0;
        waiter.Completed();
        arrived = false;
        NavMeshHit navPos;
        if (NavMesh.SamplePosition(waiter.startPosition, out navPos, 100, -1))
        {
            waiter.agent.isStopped = false;
            waiter.agent.SetDestination(navPos.position);
        }
        else
        {
            animator.SetTrigger("Idle");
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Comprobar usando waiter si tenemos alguna tarea o algo y cambiar de estado en consecuencia usando el animator.
        if (waiter.agent.remainingDistance <= waiter.agent.stoppingDistance && !arrived)
        {
            waiter.agent.isStopped = true;
            Debug.Log("Parado");
            arrived = true;
        }
        else if (waiter.agent.isStopped && looking < 1)
        {
            //waiter.transform.rotation = Quaternion.Lerp(waiter.transform.rotation, waiter.startRotation, looking);
            waiter.LookAt(waiter.startForward, looking);
            looking += Time.deltaTime;
        }
        else if (waiter.currentTask != null)
        {
            animator.SetTrigger(waiter.currentTask.Id);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //animator.ResetTrigger("Idle");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
