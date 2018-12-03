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
    public List<QeuePoint> cola;
    public int genteEnCola;
    public List<CheckPoint> barraPedidos;
    public List<Encimera> barraComida;
    public List<Transform> calle;

    public SoundManager soundManager;
    public int genteDentro;
    public int mesasDisponibles;

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
        soundManager.Play("AmbienteGente");
        

        camareros.AddRange(FindObjectsOfType<WaiterAgent>());
        cocineros.AddRange(FindObjectsOfType<CookerAgent>());
        maitre.AddRange(FindObjectsOfType<MaitreAgent>());
        limpieza.AddRange(FindObjectsOfType<CleanerAgent>());
        mesas.AddRange(FindObjectsOfType<Mesa>());
        barraPedidos.AddRange(FindObjectsOfType<CheckPoint>());
        barraComida.AddRange(FindObjectsOfType<Encimera>());
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Calle"))
            calle.Add(g.transform);
        cola.AddRange(FindObjectsOfType<QeuePoint>());
        cola.Sort((s1, s2) => s1.puesto.CompareTo(s2.puesto));
        mesasDisponibles = mesas.Count;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Task t = new Task("Sentarse",mesas[x].gameObject,null,Task.Receptor.Cliente,mesas[x].gameObject);
            cola[0].cliente.Notify(t);
            x = (x + 1) % mesas.Count;
        }

        soundManager.ChangeVolume("AmbienteGente", (float)genteDentro / (float)mesas.Count);
    }

    public void Notify(Task tarea)
    {
        agentsDictionary[tarea.receptor][0].Notify(tarea);
        agentsDictionary[tarea.receptor] = agentsDictionary[tarea.receptor].OrderBy(n => n.taskNumber).ToList<StandardAgent>();
    }
}
