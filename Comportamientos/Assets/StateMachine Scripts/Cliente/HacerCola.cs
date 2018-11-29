using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class HacerCola : StateMachineBehaviour {

    public ClientAgent client;
    QeuePoint cola;
    int actualPos;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        actualPos = 0;
        client.esperando = false;
        checkColaFirst();
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!client.esperando)
        {
            checkCola();
        }
        else if(client.currentTask != null)
        {
            cola.ocupado = false;
            animator.SetTrigger(client.currentTask.Id);
        } 
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

    void checkColaFirst()
    {
        bool check = false;

        while (actualPos < client.world.cola.Count && !check )
        {
            if (!client.world.cola[actualPos].ocupado)
                check = true;
            else
                actualPos++;
        }
        if (check)
        {
            client.CalculateNavPos(client.world.cola[actualPos].transform.position);
            cola = client.world.cola[actualPos];
            cola.cliente = client;
            cola.ocupado = true;
        }
    }

    void checkCola()
    {
        if (actualPos != 0)
        {
            if (!client.world.cola[actualPos-1].ocupado)
            {
                client.CalculateNavPos(client.world.cola[actualPos-1].transform.position);
                cola.ocupado = false;
                cola = client.world.cola[actualPos-1];
                cola.cliente = client;
                cola.ocupado = true;
                actualPos--;
            }
        }
        else
        {
            client.esperando = true;
            client.world.Notify(new Task("ClienteEsperando", client.gameObject, client, Task.Receptor.Maitre));
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
