using Assets.Scripts;
using Assets.Scripts.AbstractClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanerAgent : StandardAgent {

    //Estados
    Barrer barrerState;
    LimpiarMesa limpiarState;

    public GameObject escoba;

    // Use this for initialization
    void Start () {
        taskList = new List<Task>();
        barrerState = anim.GetBehaviour<Barrer>();
        barrerState.cleaner = this;
        limpiarState = anim.GetBehaviour<LimpiarMesa>();
        limpiarState.cleaner = this;
        startPosition = transform.position;
        startForward = transform.forward;
        world = FindObjectOfType<World>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        anim.SetFloat("Speed", agent.desiredVelocity.magnitude);
    }
}
