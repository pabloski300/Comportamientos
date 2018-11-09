using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts;

namespace Assets.Scripts.AbstractClasses
{
    public abstract class StandardAgent : MonoBehaviour
    {

        /// <summary>
        /// Esta máquina de estados tiene el comportamiento del agente
        /// </summary>
        protected abstract void Behave();

        /// <summary>
        /// Este método se usará para enviar notificaciones al agente
        /// </summary>
        /// <param name="notification">
        /// String que le llegará al agente y que interpretará usando diccionarios
        /// </param>
        public abstract void Notify(Task notification);
    }
}

public class TaskOrder : IComparer<Task>
{
    // Compares by Height, Length, and Width.
    public int Compare(Task x, Task y)
    {
        if (x.priority>y.priority)
        {
            return -1;
        }
        else if (x.priority < y.priority)
        {
            return 1;
        }
        else if (x.priority == y.priority)
        {
            if (x.Order > y.Order)
            {
                return 1;
            }
            else if (x.Order < y.Order)
            {
                return -1;
            }
            else
                return 0;
        }
        else
        {
            return 0;
        }
    }
}
