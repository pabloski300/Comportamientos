using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.AbstractClasses;

namespace Assets.Scripts
{
    public class ClientAgent : StandardAgent
    {
        public Animator anim;

        public Vector3 startPosition;
        public Vector3 startForward;

        public World world;

        //Estados:
        Paseando paseando;
        HacerCola hacerCola;
        Sentarse sentarse;
        PedirComida pedirComida;
        Comer comer;
        PedirCuenta pedirCuenta;
        Irse irse;

        // Use this for initialization
        void Start()
        {
            paseando = anim.GetBehaviour<Paseando>();
            hacerCola = anim.GetBehaviour<HacerCola>();
            sentarse = anim.GetBehaviour<Sentarse>();
            pedirComida = anim.GetBehaviour<PedirComida>();
            comer = anim.GetBehaviour<Comer>();
            pedirCuenta = anim.GetBehaviour<PedirCuenta>();
            irse = anim.GetBehaviour<Irse>();
        }

        // Update is called once per frame
        void Update()
        {
            anim.SetFloat("Speed", agent.desiredVelocity.magnitude);
        }
    }
}
