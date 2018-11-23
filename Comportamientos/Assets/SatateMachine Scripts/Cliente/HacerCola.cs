using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class HacerCola : StateMachineBehaviour {

    World world;
    public ClientAgent client;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        world = FindObjectOfType<World>();
        checkCola();
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        checkCola();
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

    void checkCola()
    {
        int count = 0;
        bool check = false;
        while (count < world.cola.Count && !check)
        {
            if (!world.cola[count].ocupado)
                check = true;
            else
                count++;
        }
        if (check)
        {
            client.CalculateNavPos(world.cola[count].transform.position);
            world.cola[count].ocupado = true;
        }
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
