using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AbstractClasses
{
    public abstract class InteractableObject : MonoBehaviour
    {
        //Lista de agentes suscritos a este objeto
        protected abstract List<StandardAgent> Agents { get; set; }

        /// <summary>
        /// Este método avisará a los agentes cuando sea necesario
        /// </summary>
        protected abstract void NotifyAgents();

        /// <summary>
        /// Este método se usa para añadir agentes a la lista de agentes a los que debe notificar este objeto
        /// </summary>
        /// <param name="Agent"></param>
        public abstract void AddAgent(StandardAgent Agent);

    }
}
