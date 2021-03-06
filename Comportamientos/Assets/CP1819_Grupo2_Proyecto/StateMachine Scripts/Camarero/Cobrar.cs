﻿using Assets.Scripts;
using Assets.Scripts.AbstractClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cobrar : StateMachineBehaviour {

    public WaiterAgent waiter;
    private float looking;
    private bool cobrando;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        looking = 0;
        cobrando = false;

        if (!waiter.CalculateNavPos(waiter.currentTask.Coordinates.transform.position))
        {
            animator.SetTrigger("Idle");
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(waiter.transform.position,waiter.agent.destination) <= waiter.agent.stoppingDistance && !waiter.agent.isStopped)
        {
            waiter.agent.isStopped = true;
        }
        else if (waiter.agent.isStopped && looking < 1)
        {
            Vector3 look = waiter.currentTask.Emisor.transform.position - waiter.transform.position;
            waiter.LookAt(look, looking);
            looking += Time.deltaTime;
        }
        else if (waiter.agent.isStopped && looking >= 1 && !cobrando)
        {
            waiter.soundManager.Play("ClientePagar");
            waiter.dolar.SetActive(true);
            animator.SetTrigger("Interaccion");
            Debug.Log("Cobrando");
            cobrando = true;
        }
        else if (waiter.agent.isStopped && looking >= 1 && !animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            waiter.dolar.SetActive(false);
            animator.SetTrigger("Idle");
            animator.SetTrigger("FinInteraccion");
            waiter.currentTask.Emisor.GetComponent<StandardAgent>().anim.SetTrigger("Irse");
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
