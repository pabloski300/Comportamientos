using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleCooker : StateMachineBehaviour {

    public CookerAgent cooker;
    float looking;
    int encimeras;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        encimeras = cooker.world.barraComida.Count;
        animator.SetBool("Bandeja", false);
        looking = 0;
        cooker.Completed();
        NavMeshHit navPos;
        animator.SetBool("Lavar", true);
        if (NavMesh.SamplePosition(cooker.fregadero.transform.position, out navPos, 100, -1))
        {
            cooker.agent.isStopped = false;
            cooker.agent.SetDestination(navPos.position);
        }
        else
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
            Debug.Log("Lavando");
        }
        else if (cooker.agent.isStopped && looking < 1)
        {
            cooker.soundManager.Play("CocineroFregar");
            Vector3 look = cooker.fregadero.transform.position - cooker.transform.position;
            cooker.LookAt(look, looking);
            looking += Time.deltaTime;
        }
        else if (cooker.currentTask != null)
        {
            bool encimeraLibre = false;
            for(int i=0; i<encimeras && !encimeraLibre; i++)
            {
                if (!cooker.world.barraComida[i].ocupado)
                {
                    encimeraLibre = true;
                    cooker.encimeraSeleccionada = cooker.world.barraComida[i];
                    cooker.encimeraSeleccionada.ocupado = true;
                }
            }
            if (encimeraLibre)
            {
                animator.SetTrigger(cooker.currentTask.Id);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        cooker.soundManager.Stop("CocineroFregar");
        animator.SetBool("Lavar", false);
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
