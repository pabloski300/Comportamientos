﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class PedirComida : StateMachineBehaviour {
    public ClientAgent client;
    bool pedido;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pedido = false;
        animator.SetTrigger("Llamar");
        client.soundManager.Play("ClienteLlamarCamarero");
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !pedido)
        {
            pedido = true;
            client.world.Notify(new Task("Pedido", client.mesa.gameObject, client, Task.Receptor.Camarero));
            animator.SetTrigger("FinLlamar");
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
