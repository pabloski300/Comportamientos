using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.AbstractClasses;


namespace Assets.Scripts
{
    public class WaiterAgent : StandardAgent
    {
        //Estados
        IdleWaiter idleState;
        RecogerPedido recogerPedidoState;
        EntregarPedido entregarPedidoState;
        RecogerComida recogerComidaState;
        EntregarComida entregarComidaState;
        Cobrar cobrarState;

        public Transform coger;
        public Transform centroBandeja;

        public GameObject dolar;

        public GameObject plato;

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
            cobrarState = anim.GetBehaviour<Cobrar>();
            cobrarState.waiter = this;
            startPosition = transform.position;
            startForward = transform.forward;
            world = FindObjectOfType<World>();
            soundManager = FindObjectOfType<SoundManager>();
        }

        private void Update()
        {
            anim.SetFloat("Speed", agent.desiredVelocity.magnitude);
        }
    }
}
