using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts
{
    public class AgentMovement : MonoBehaviour
    {

        NavMeshAgent agent;

        // Use this for initialization
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            //if(agent.desirableSpeed != 0)
            //{
                //anim.SetBoolMoving
            //}
        }

        public void moveTo(Vector3 point)
        {
            agent.SetDestination(point);
        }
    }
}
