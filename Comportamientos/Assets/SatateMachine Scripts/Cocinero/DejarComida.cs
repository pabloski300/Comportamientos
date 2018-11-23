using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DejarComida : StateMachineBehaviour {

    public CookerAgent cooker;
    float looking;
    GameObject plato;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Bandeja",true);
        plato = Instantiate(cooker.platoPrefab,cooker.plato);
        plato.transform.localPosition = Vector3.zero;
        plato.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        cooker.agent.isStopped = false;
        looking = 0;

        if (!cooker.CalculateNavPos(cooker.encimeraSeleccionada.transform.position))
        {
            animator.SetTrigger("Idle");
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (cooker.agent.remainingDistance <= cooker.agent.stoppingDistance && !cooker.agent.isStopped)
        {
            cooker.agent.isStopped = true;
        }
        else if (cooker.agent.isStopped && looking < 1)
        {
            cooker.LookAt(cooker.encimeraSeleccionada.orientationCocinero.forward,looking);
            looking += Time.deltaTime*5;
        }
        else if (cooker.agent.isStopped && looking >= 1)
        {
            plato.transform.parent = cooker.encimeraSeleccionada.plato;
            plato.transform.localPosition = Vector3.zero;
            plato.transform.localRotation = Quaternion.Euler(-90,0,0);
            cooker.encimeraSeleccionada.platoPrefab = plato;
            cooker.world.Notify(new Task("Comida", cooker.encimeraSeleccionada.gameObject, cooker, Task.Receptor.Camarero, cooker.currentTask.extraInfo));
            animator.SetTrigger("Idle");
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
