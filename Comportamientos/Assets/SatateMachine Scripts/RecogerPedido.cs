using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RecogerPedido : StateMachineBehaviour {

    public WaiterAgent waiter;
    bool arrived;
    bool recived;
    int times;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        NavMeshHit navPos;
        recived = false;
        arrived = false;
        times = 0;
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
        if (waiter.agent.remainingDistance <= waiter.agent.stoppingDistance && !arrived)
        {
            waiter.agent.isStopped = true;

            Debug.Log("Recogiendo");
            arrived = true;
            animator.SetTrigger("RecibirPedido");

        }else if (waiter.agent.isStopped)
        {
            if (!animator.IsInTransition(0))
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !recived)
                {
                    if (times == 2)
                    {
                        animator.SetTrigger("Cocina");
                        animator.SetTrigger("PedidoRecibido");
                        recived = true;
                    }
                    else
                    {
                        animator.SetTrigger("RecibirPedido");
                        times++;
                    }
                }
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
