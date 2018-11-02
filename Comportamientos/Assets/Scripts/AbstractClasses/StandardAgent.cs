using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AbstractClasses
{
    public abstract class StandardAgent : MonoBehaviour
    {
        /// <summary>
        /// Esta máquina de estados tiene el comportamiento del agente
        /// </summary>
        public abstract void Behave();

        /// <summary>
        /// Este método se usará para enviar notificaciones al agente
        /// </summary>
        /// <param name="notification">
        /// String que le llegará al agente y que interpretará usando diccionarios
        /// </param>
        public abstract void Notify(string notification);
    }
}
