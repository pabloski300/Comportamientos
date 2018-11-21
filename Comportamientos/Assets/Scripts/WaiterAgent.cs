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
        public Vector3 startForward;

        public World world;

        //Estados
        IdleWaiter idleState;
        RecogerPedido recogerPedidoState;
        EntregarPedido entregarPedidoState;
        RecogerComida recogerComidaState;
        EntregarComida entregarComidaState;

        // Use this for initialization
        void Awake()
        {
            taskList = new List<Task>();
            idleState = anim.GetBehaviour<IdleWaiter>();
            idleState.waiter = this;
            recogerPedidoState = anim.GetBehaviour<RecogerPedido>();
            recogerPedidoState.waiter = this;
            entregarPedidoState = anim.GetBehaviour<EntregarPedido>();
            entregarPedidoState.waiter = this;
            recogerComidaState = anim.GetBehaviour<RecogerComida>();
            recogerComidaState.waiter = this;
            entregarComidaState = anim.GetBehaviour<EntregarComida>();
            entregarComidaState.waiter = this;
            startPosition = transform.position;
            startForward = transform.forward;
            world = FindObjectOfType<World>();
        }
        private void Update()
        {
            anim.SetFloat("speed", agent.desiredVelocity.magnitude);
        }
    }
}
