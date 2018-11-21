using Assets.Scripts;
using Assets.Scripts.AbstractClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class World : MonoBehaviour {

    int x = 0;

    public List<StandardAgent> camareros;
    public List<StandardAgent> cocineros;
    public List<StandardAgent> maitre;
    public List<StandardAgent> limpieza;

    public List<Mesa> mesas;
    public List<CheckPoint> cola;
    public List<CheckPoint> barra;
    public List<CheckPoint> cocina;
    public List<CheckPoint> barraCocina;



    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
             Task t = new Task("Pedido",mesas[x].transform.position,null,"Camareros");
            camareros[0].Notify(t);
            camareros = camareros.OrderBy(n => n.taskNumber).ToList<StandardAgent>();
            x = (x + 1) % mesas.Count;
        }
    }

    public void Notify(Task tarea)
    {
        switch (tarea.Receptor)
        {
            case "Cocinero":
                cocineros[0].Notify(tarea);
                cocineros = cocineros.OrderBy(n => n.taskNumber).ToList<StandardAgent>();
                break;
        }
    }
}
