using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acompañar : StateMachineBehaviour {

    public MaitreAgent maitre;
    bool back;
    float looking;
    bool interaccion;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        back = false;
        looking = 0;
        interaccion = false;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(back && Vector3.Distance(maitre.transform.position, maitre.agent.destination) <= maitre.agent.stoppingDistance && !maitre.agent.isStopped)
        {
            animator.SetTrigger("Recibir");
        }
        else if (Vector3.Distance(maitre.transform.position, maitre.agent.destination) <= maitre.agent.stoppingDistance && !maitre.agent.isStopped)
        {
            maitre.agent.isStopped = true;
        }
        else if (maitre.agent.isStopped && looking < 1)
        {
            Vector3 look = maitre.mesa.transform.position - maitre.transform.position;
            maitre.LookAt(look, looking);
            looking += Time.deltaTime;
        }
        else if (maitre.agent.isStopped && looking >= 1 && !interaccion)
        {
            System.Random r = new System.Random();
            int x = r.Next(1, 5);
            maitre.soundManager.Play("InteraccionM" + x);
            Debug.Log("Recogiendo");
            animator.SetTrigger("Interaccion");
            interaccion = true;
        }
        else if (maitre.agent.isStopped && !animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {

            animator.SetTrigger("FinInteraccion");
            maitre.CalculateNavPos(maitre.startPosition);
            back = true;

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
