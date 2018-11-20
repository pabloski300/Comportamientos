using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts;
using System;

namespace Assets.Scripts.AbstractClasses
{
    public abstract class StandardAgent : MonoBehaviour, IComparable
    {
        public List<Task> taskList;
        public Task currentTask;
        public int taskNumber;

        public int CompareTo(object obj)
        {
            StandardAgent s = obj as StandardAgent;

            if (this.taskNumber > s.taskNumber)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Este método se usará para enviar notificaciones al agente
        /// </summary>
        /// <param name="notification">
        /// String que le llegará al agente y que interpretará usando diccionarios
        /// </param>
        public abstract void Notify(Task notification);
    }
}
