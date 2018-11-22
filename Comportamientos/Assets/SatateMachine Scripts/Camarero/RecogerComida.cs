using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RecogerComida : StateMachineBehaviour {

    public WaiterAgent waiter;
    float looking;
    bool cogiendo;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        looking = 0;
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
        }
        else if (waiter.agent.isStopped && looking < 1)
        {
            Vector3 look = waiter.currentTask.Coordinates.transform.position - waiter.transform.position;
            waiter.LookAt(look, looking);
            looking += Time.deltaTime;
        }
        else if (waiter.agent.isStopped && looking >= 1 && !cogiendo)
        {
            animator.SetTrigger("Coger");
            Debug.Log("Cogiendo");
            cogiendo = true;
            Encimera enci = waiter.currentTask.Coordinates.GetComponent<Encimera>();
            waiter.plato = enci.platoPrefab;
            waiter.plato.transform.parent = waiter.coger;
            waiter.plato.transform.localPosition = Vector3.zero;
            waiter.plato.transform.localRotation = Quaternion.Euler(-90, 0, 0);
            enci.platoPrefab = null;
            enci.ocupado = false;
        }
        else if (waiter.agent.isStopped && !animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            animator.SetTrigger("Entregar");
            animator.SetTrigger("FinCoger");
            waiter.plato.transform.parent = waiter.centroBandeja;
            waiter.plato.transform.localPosition = Vector3.zero;
            waiter.plato.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        }
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
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
