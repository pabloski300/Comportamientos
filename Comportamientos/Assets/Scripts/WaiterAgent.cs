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

        public Animator anim;

        public Vector3 startPosition;

        //Estados
        IdleWaiter idleState;
        RecogerPedido recogerState;
        EntregarPedidoaCocina entregarState;

        // Use this for initialization
        void Awake()
        {
            taskList = new List<Task>();
            idleState = anim.GetBehaviour<IdleWaiter>();
            idleState.waiter = this;
            recogerState = anim.GetBehaviour<RecogerPedido>();
            recogerState.waiter = this;
            entregarState = anim.GetBehaviour<EntregarPedidoaCocina>();
            entregarState.waiter = this;
            startPosition = transform.position;
        }

        public override void Notify(Task notification)
        {
            if (currentTask == null)
            {
                currentTask = notification;
            }
            else
            {
                taskList.Add(notification);
            }
            taskNumber++;
        }

        public void Completed()
        {
            if (taskList.Count > 0)
            {
                currentTask = taskList[0];
                taskList.RemoveAt(0);
            }
            else
            {
                currentTask = null;
            }
            taskNumber--;
        }
    }
}
