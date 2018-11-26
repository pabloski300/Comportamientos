using Assets.Scripts;
using Assets.Scripts.AbstractClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntregarComida : StateMachineBehaviour {

    public WaiterAgent waiter;
    float looking;
    bool dejando;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        looking = 0;
        dejando = false;
        
        if (!waiter.CalculateNavPos(waiter.currentTask.extraInfo.transform.position))
        {
            animator.SetTrigger("Idle");
        }
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (Vector3.Distance(waiter.transform.position, waiter.agent.destination) <= waiter.agent.stoppingDistance && !waiter.agent.isStopped)
        {
            waiter.agent.isStopped = true;
        }
        else if (waiter.agent.isStopped && looking < 1)
        {
            Vector3 look = waiter.currentTask.extraInfo.GetComponent<ClientAgent>().mesa.GetComponent<Mesa>().posicionPlato.transform.position - waiter.transform.position;
            waiter.LookAt(look, looking);
            looking += Time.deltaTime;
        }
        else if (waiter.agent.isStopped && looking >= 1 && !dejando)
        {
            animator.SetTrigger("Dejar");
            Debug.Log("Dejando");
            dejando = true;
        }
        else if (waiter.agent.isStopped && !animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            waiter.plato.transform.parent = waiter.currentTask.extraInfo.GetComponent<ClientAgent>().mesa.GetComponent<Mesa>().posicionPlato;
            waiter.plato.transform.localPosition = Vector3.zero;
            waiter.plato.transform.localRotation = Quaternion.Euler(-90, 0, 0);
            waiter.currentTask.extraInfo.GetComponent<StandardAgent>().anim.SetTrigger("Comer");
            //waiter.currentTask.extraInfo.GetComponent<StandardAgent>().Notify(new Task("Comer",waiter.plato,waiter,Task.Receptor.Cliente));
            waiter.plato = null;
            animator.SetTrigger("Idle");
            animator.SetTrigger("FinDejar");
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
