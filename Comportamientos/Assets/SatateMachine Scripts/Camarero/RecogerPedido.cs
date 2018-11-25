using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RecogerPedido : StateMachineBehaviour {

    public WaiterAgent waiter;
    int times;
    bool cogiendo;
    float looking;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        times = 0;
        cogiendo = false;
        if (!waiter.CalculateNavPos(waiter.currentTask.Coordinates.transform.position))
        {
            animator.SetTrigger("Entregar");
        }
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (waiter.agent.remainingDistance <= waiter.agent.stoppingDistance && !waiter.agent.isStopped)
        {            
            waiter.agent.isStopped = true;
            Debug.Log("Recogiendo");
        }
        else if (waiter.agent.isStopped && looking < 1)
        {
            Vector3 look = waiter.currentTask.Emisor.transform.position - waiter.transform.position;
            waiter.LookAt(look, looking);
            looking += Time.deltaTime;
        }
        else if (waiter.agent.isStopped && looking >= 1 && !cogiendo)
        {
            System.Random r = new System.Random();
            cogiendo = true;
            int x = r.Next(1, 5);
            waiter.soundManager.Play("InteraccionF" + x);
            Debug.Log("Recogiendo");
            animator.SetTrigger("Interaccion");
        }
        else if (waiter.agent.isStopped && !animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            if (times == 2)
            {
                animator.SetTrigger("Entregar");
                animator.SetTrigger("FinInteraccion");
            }
            else
            {
                System.Random r = new System.Random();
                int x = r.Next(1, 5);
                waiter.soundManager.Play("InteraccionF" + x);
                animator.SetTrigger("Interaccion");
                times++;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //animator.ResetTrigger("Pedido");
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
