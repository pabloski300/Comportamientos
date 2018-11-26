using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Sentarse : StateMachineBehaviour {

    public ClientAgent client;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        client.mesa = client.currentTask.extraInfo.GetComponent<Mesa>();
        client.agent.isStopped = false;
        client.world.genteEnCola--;
        client.CalculateNavPos(client.mesa.asiento.position);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	    if(Vector3.Distance(client.transform.position, client.agent.destination) <= client.agent.stoppingDistance && !client.agent.isStopped)
        {
            client.world.genteDentro++;
            client.agent.isStopped = true;
            client.Completed();
        }
        else if (client.agent.isStopped)
        {
            client.agent.enabled = false;
            client.mesa.estadoActual = Mesa.Estado.Ocupada;
            client.transform.position = client.mesa.asiento.transform.position;
            client.transform.rotation = client.mesa.asiento.transform.rotation;
            animator.SetBool("Sentado", true);
            client.mesa.ChangeState(Mesa.Estado.Ocupada);
            animator.SetTrigger("PedirComida");
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
