using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Paseando : StateMachineBehaviour
{
    public ClientAgent client;
    bool going;
    public float chance;
    public float checkTime;
    float currentTime;
    int currentPoint;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        going = (0.4 < Random.Range(0, 1));
        if (going)
        {
            currentPoint = 0;
            client.CalculateNavPos(client.world.calle[currentPoint].position);
        }
        else
        {
            currentPoint = client.world.calle.Count-1;
            client.CalculateNavPos(client.world.calle[currentPoint].position);
        }

        currentTime = Random.Range(0, checkTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        currentTime += Time.deltaTime;
        if (currentTime>=checkTime)
        {
            currentTime = 0;
            if (Random.Range(0, 1) < chance && client.world.genteEnCola<client.world.cola.Count)
            {
                client.world.genteEnCola++;
                animator.SetTrigger("HacerCola");
            }
        }
        if (Vector3.Distance(client.transform.position, client.agent.destination) <= client.agent.stoppingDistance && !client.agent.isStopped)
        {
            System.Random r = new System.Random();
            currentPoint = currentPoint + r.Next(1, client.world.calle.Count - 1);
            currentPoint = currentPoint % client.world.calle.Count;
            client.CalculateNavPos(client.world.calle[currentPoint].position);

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
