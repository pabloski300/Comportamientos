using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mesa : MonoBehaviour {
    
    public enum Estado
    {
        Libre,
        Ocupada,
        Sucia,
        Limpia
    }

    public Estado estadoActual = Estado.Libre;
}
