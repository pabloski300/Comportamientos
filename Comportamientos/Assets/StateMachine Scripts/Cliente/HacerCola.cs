using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class HacerCola : StateMachineBehaviour {

    public ClientAgent client;
    QeuePoint cola;
    int actualPos;
    int actualIdle;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        actualIdle = 1;
        actualPos = 0;
        client.esperando = false;
        client.hasAsked = false;
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
        else if(client.esperando && (Vector3.Distance(client.transform.position, client.agent.destination) <= client.agent.stoppingDistance && !client.agent.isStopped) && !client.hasAsked)
        {
            client.world.Notify(new Task("ClienteEsperando", client.gameObject, client, Task.Receptor.Maitre));
            client.hasAsked = true;
        }

        if (client.agent.desiredVelocity.magnitude == 0 && !animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            System.Random r = new System.Random();
            float x = (float)r.NextDouble();
            if(x > 0.8 && x > 0.9 && actualIdle != 2)
            {
                actualIdle = 2;
                animator.SetTrigger(actualIdle.ToString());
            }
            else if (x > 0.9 && actualIdle != 3)
            {
                actualIdle = 3;
                animator.SetTrigger(actualIdle.ToString());
            }
            else if (x < 0.8 && actualIdle != 1)
            {
                actualIdle = 1;
                animator.SetTrigger(actualIdle.ToString());
            }
            
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
