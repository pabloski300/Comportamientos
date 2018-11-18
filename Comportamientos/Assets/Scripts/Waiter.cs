using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Waiter : MonoBehaviour {
    //variables necesarias para la IA
    public NavMeshAgent agent;

    public float wanderRadius;
    public float wanderTimer;

    public Animator anim;

    //Estados
    IdleWaiter idleState;

    private void Awake()
    {
        idleState = anim.GetBehaviour<IdleWaiter>();
        //idleState.waiter = this;
    }
}
