using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts;

namespace Assets.Scripts.AbstractClasses
{
    public abstract class StandardAgent : MonoBehaviour, IComparer<StandardAgent>
    {
        protected List<Task> taskList;

        public int Compare(StandardAgent x, StandardAgent y)
        {
            if (x.taskList.Count > y.taskList.Count)
            {
                return -1;
            }
            else
            {
                return 1;
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
