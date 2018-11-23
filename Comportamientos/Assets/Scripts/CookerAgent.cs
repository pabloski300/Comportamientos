﻿using Assets.Scripts;
using Assets.Scripts.AbstractClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CookerAgent : StandardAgent {

    public Transform plato;
    public GameObject platoPrefab;

    public Encimera encimeraSeleccionada;

    public GameObject fregadero;

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
    }

    private void Update()
    {
        anim.SetFloat("Speed", agent.desiredVelocity.magnitude);
    }
}
