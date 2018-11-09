using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    //Esta clase se usa simplemente como una manera de transmitir datos de los objetos interactuables a los agentes correspondientes
    public class Chore
    {
        //etiqueta que identifica el tipo de tarea que este objeto representa
        public readonly string Id;
        //Posición donde debe acudir el agente a realizar la tarea
        public readonly Vector3 Coordinates;
        //Valor para poder ordenar las tareas por prioridad
        public readonly int priority;
    }
}
