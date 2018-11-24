using Assets.Scripts;
using Assets.Scripts.AbstractClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class World : MonoBehaviour {

    int x = 0;
    public Dictionary<Task.Receptor, List<StandardAgent>> agentsDictionary;
    public List<StandardAgent> camareros;
    public List<StandardAgent> cocineros;
    public List<StandardAgent> maitre;
    public List<StandardAgent> limpieza;

    public List<Mesa> mesas;
    public List<CheckPoint> cola;
    public List<CheckPoint> barraPedidos;
    public List<Encimera> barraComida;
    public List<CheckPoint> calle;

    public SoundManager soundManager;

    public void Start()
    {
        agentsDictionary = new Dictionary<Task.Receptor, List<StandardAgent>>()
        {
            {Task.Receptor.Camarero, camareros},
            {Task.Receptor.Cocinero, cocineros},
            {Task.Receptor.Maitre, maitre},
            {Task.Receptor.Limpiador, limpieza}
        };

        soundManager.Play("AmbienteCiudad");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Task t = new Task("Pedido",mesas[x].gameObject,null,Task.Receptor.Camarero);
            camareros[0].Notify(t);
            camareros = camareros.OrderBy(n => n.taskNumber).ToList<StandardAgent>();
            x = (x + 1) % mesas.Count;
        }
    }

    public void Notify(Task tarea)
    {
        agentsDictionary[tarea.receptor][0].Notify(tarea);
        agentsDictionary[tarea.receptor] = agentsDictionary[tarea.receptor].OrderBy(n => n.taskNumber).ToList<StandardAgent>();
    }
}
