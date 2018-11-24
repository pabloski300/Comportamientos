using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.AbstractClasses;

namespace Assets.Scripts
{
    public class ClientAgent : StandardAgent
    {

        //Estados:
        Paseando paseando;
        HacerCola hacerCola;
        Sentarse sentarse;
        PedirComida pedirComida;
        Comer comer;
        PedirCuenta pedirCuenta;
        Irse irse;
        public Mesa mesa;

        // Use this for initialization
        void Start()
        {
            anim = GetComponent<Animator>();
            paseando = anim.GetBehaviour<Paseando>();
            hacerCola = anim.GetBehaviour<HacerCola>();
            sentarse = anim.GetBehaviour<Sentarse>();
            pedirComida = anim.GetBehaviour<PedirComida>();
            comer = anim.GetBehaviour<Comer>();
            pedirCuenta = anim.GetBehaviour<PedirCuenta>();
            irse = anim.GetBehaviour<Irse>();
            paseando.client = this;
            hacerCola.client = this;
            sentarse.client = this;
            pedirComida.client = this;
            comer.client = this;
            pedirCuenta.client = this;
            irse.client = this;
        }

        // Update is called once per frame
        void Update()
        {
            anim.SetFloat("Speed", agent.desiredVelocity.magnitude);
        }
    }
}
