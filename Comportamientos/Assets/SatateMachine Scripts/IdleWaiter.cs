using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleWaiter : StateMachineBehaviour {

    public WaiterAgent waiter;
    private Transform target;
    private float timer = 100;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Comprobar usando waiter si tenemos alguna tarea o algo y cambiar de estado en consecuencia usando el animator.
        Look(animator);
        //moverse por la lista de puntos del path y mandar velocidad al animator
        Behave(animator);
	}

    //Comprobacion de cambio de estado
    void Look(Animator animator)
    {

    }

    //Actuacion en funcion de lo comprobado en Look
    void Behave(Animator animator)
    {
        //animator.SetFloat("speed", waiter.agent.desiredVelocity.magnitude);
    }

    //Funciones de movimiento aleatorio, coge un punto del navmesh dentro de un radio cada x tiempo
    void Wander()
    {
        timer += Time.deltaTime;

        if (timer >= waiter.wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(waiter.transform.position, waiter.wanderRadius, 1 << NavMesh.GetAreaFromName("NavCamareros"));
            waiter.agent.SetDestination(newPos);
            timer = 0;
        }
    }

    Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, waiter.wanderRadius, layermask);

        return navHit.position;
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
