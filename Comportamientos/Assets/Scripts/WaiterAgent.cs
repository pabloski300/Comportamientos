using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.AbstractClasses;
using UnityEngine.AI;

namespace Assets.Scripts
{
    public class WaiterAgent : StandardAgent
    {
        public NavMeshAgent agent;

        public float wanderRadius;
        public float wanderTimer;

        public Animator anim;

        //Estados
        IdleWaiter idleState;

        // Use this for initialization
        void Awake()
        {
            taskList = new List<Task>();
            idleState = anim.GetBehaviour<IdleWaiter>();
            idleState.waiter = this;
        }

        public override void Notify(Task notification)
        {
            taskList.Add(notification);
        }
    }
}
