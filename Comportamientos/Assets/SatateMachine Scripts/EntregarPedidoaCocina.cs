using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntregarPedidoaCocina : StateMachineBehaviour {

    public WaiterAgent waiter;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
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
        if (waiter.agent.remainingDistance <= waiter.agent.stoppingDistance)
        {
            waiter.agent.isStopped = true;
            //notify Cocinero.
            animator.SetTrigger("Idle");
        }
        animator.SetFloat("speed", waiter.agent.desiredVelocity.magnitude);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.ResetTrigger("Cocina");
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
