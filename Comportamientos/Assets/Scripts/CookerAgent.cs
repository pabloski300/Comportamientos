using Assets.Scripts;
using Assets.Scripts.AbstractClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CookerAgent : StandardAgent {

    public Transform plato;
    public GameObject platoPrefab;

    public Transform cuchilo;
    public Transform cuchilloPrefab;

    public Encimera encimeraSeleccionada;

    public GameObject fregadero;
    public GameObject tabla;
    public GameObject sarten;
    public GameObject humo;

    //Estados
    IdleCooker idleState;
    Cocinar cocinarState;
    DejarComida dejarComidaState;

    // Use this for initialization
    void Awake()
    {
        taskList = new List<Task>();
        idleState = anim.GetBehaviour<IdleCooker>();
        idleState.cooker = this;
        cocinarState = anim.GetBehaviour<Cocinar>();
        cocinarState.cooker = this;
        dejarComidaState = anim.GetBehaviour<DejarComida>();
        dejarComidaState.cooker = this;
        startPosition = transform.position;
        startForward = transform.forward;
        world = FindObjectOfType<World>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void Update()
    {
        anim.SetFloat("Speed", agent.desiredVelocity.magnitude);
    }
}
