using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts;
using System;
using UnityEngine.AI;

namespace Assets.Scripts.AbstractClasses
{
    public abstract class StandardAgent : MonoBehaviour
    {
        public NavMeshAgent agent;
        protected List<Task> taskList;
        public Task currentTask;
        public int taskNumber;
        public World world;

        public Vector3 startPosition;
        public Vector3 startForward;

        public Animator anim;

        public SoundManager soundManager;

        /// <summary>
        /// Este método se usará para enviar notificaciones al agente
        /// </summary>
        /// <param name="notification">
        /// String que le llegará al agente y que interpretará usando diccionarios
        /// </param>
        public void Notify(Task notification)
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

        /// <summary>
        /// Este método se llama cuando ha completado una tarea el agente, en cuyo caso mira si tiene más tareas pendientes.
        /// Si hay más tareas coge la primera y la pone como current task, quitándola de la lista.
        /// </summary>
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


        public void LookAt(Vector3 dir, float t)
        {
            Vector3 trueDir = new Vector3(dir.x, transform.forward.y, dir.z);
            transform.forward = Vector3.Slerp(transform.forward, trueDir, t);
        }

        public bool CalculateNavPos(Vector3 pos)
        {
            NavMeshHit navPos;
            if (NavMesh.SamplePosition(pos, out navPos, 100, -1))
            {
                agent.isStopped = false;
                agent.SetDestination(navPos.position);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
