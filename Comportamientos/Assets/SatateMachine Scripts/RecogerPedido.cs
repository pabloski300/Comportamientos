using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RecogerPedido : StateMachineBehaviour {

    public WaiterAgent waiter;
    bool recogido;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        NavMeshHit navPos;
        recogido = false;
        if (NavMesh.SamplePosition(waiter.currentTask.Coordinates, out navPos, 100, -1))
        {
            waiter.agent.isStopped = false;
            waiter.agent.SetDestination(navPos.position);
        }
        else
        {
            animator.SetTrigger("Cocina");
        }
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (waiter.agent.remainingDistance <= waiter.agent.stoppingDistance)
        {
            waiter.agent.isStopped = true;

            Debug.Log("Recogiendo");
            recogido = true;
            //animator.SetTrigger("RecibirPedido");
        }

        if (waiter.agent.isStopped)
        {
            if (/*animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0)*/recogido)
            {
                animator.SetTrigger("Cocina");
            }
        }
        animator.SetFloat("speed", waiter.agent.desiredVelocity.magnitude);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    animator.ResetTrigger("Pedido");
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
