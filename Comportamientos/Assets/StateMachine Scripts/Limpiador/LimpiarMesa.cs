using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class LimpiarMesa : StateMachineBehaviour {

    public CleanerAgent cleaner;
    public float cleanTime;
    float currentTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cleaner.CalculateNavPos(cleaner.currentTask.Coordinates.transform.position);
        currentTime = 0;
    }
        

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (Vector3.Distance(cleaner.transform.position, cleaner.agent.destination) <= cleaner.agent.stoppingDistance && !cleaner.agent.isStopped)
        {
            cleaner.agent.isStopped = true;
        }
        else if(cleaner.agent.isStopped)
        {
            currentTime += Time.deltaTime;
            if(currentTime>=cleanTime)
            {
                cleaner.currentTask.Coordinates.GetComponent<Mesa>().ChangeState(Mesa.Estado.Libre);
                cleaner.world.Notify(new Task("MesaLibre", cleaner.currentTask.Coordinates, cleaner, Task.Receptor.Maitre));
                cleaner.Completed();
                cleaner.CalculateNavPos(cleaner.startPosition);
                animator.SetTrigger("Barrer");
            }
        }
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
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
