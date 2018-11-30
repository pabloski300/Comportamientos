using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cocinar : StateMachineBehaviour {

    public CookerAgent cooker;
    private CheckPoint checkPoint;
    float looking;
    bool cocinando;
    float times;
    int cocinarRandom;
    Vector3 pos;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        System.Random r = new System.Random();
        cocinarRandom = r.Next(1, 3);
        looking = 0;
        cocinando = false;
        times = 0;

        if (cocinarRandom == 1)
        {
            pos = cooker.sarten.transform.position;
        }
        else
        {
            pos = cooker.tabla.transform.position;
        }

        if (!cooker.CalculateNavPos(pos))
        {
            animator.SetTrigger("Idle");
        }
    }

    /// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(cooker.transform.position, cooker.agent.destination) <= cooker.agent.stoppingDistance && !cooker.agent.isStopped)
        {
            cooker.agent.isStopped = true;
        }
        else if (cooker.agent.isStopped && looking < 1)
        {
            Vector3 look = pos - cooker.transform.position;
            cooker.LookAt(look, looking);
            looking += Time.deltaTime;
        }
        else if (cooker.agent.isStopped && looking >= 1 && !cocinando)
        {
            if(cocinarRandom == 1)
            {
                cooker.soundManager.Play("CocineroManos");
                cooker.humo.SetActive(true);
            }
            else
            {
                cooker.soundManager.Play("CocineroCuchillo");
                cooker.cuchilloPrefab.SetActive(true);
            }
            animator.SetTrigger("Cocinar"+cocinarRandom);
            Debug.Log("Cocinando");
            cocinando = true;
        }
        else if (cooker.agent.isStopped && !animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            if (times == 2)
            {
                animator.SetTrigger("Entregar");
                animator.SetTrigger("FinCocinar");
                cooker.humo.SetActive(false);
                cooker.cuchilloPrefab.SetActive(false);
            }
            else
            {
                animator.SetTrigger("Cocinar"+cocinarRandom);
                times++;
            }
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
