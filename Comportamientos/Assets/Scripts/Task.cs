using Assets.Scripts.AbstractClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    //Esta clase se usa simplemente como una manera de transmitir datos de los objetos interactuables a los agentes correspondientes
    public class Task
    {
        //etiqueta que identifica el tipo de tarea que este objeto representa
        public readonly string Id;
        //Posición donde debe acudir el agente a realizar la tarea
        public readonly Vector3 Coordinates;
        //Emisor de la tarea por si es necesario
        public readonly StandardAgent Emisor;
        //Tipo de receptor
        public readonly string Receptor;

        public Task(string _Id, Vector3 _Coordinates, StandardAgent _Emisor, string _Receptor)
        {
            Id = _Id;
            Coordinates = _Coordinates;
            Emisor = _Emisor;
            Receptor = _Receptor;
        }
    }
}
