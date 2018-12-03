using Assets.Scripts;
using Assets.Scripts.AbstractClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaitreAgent : StandardAgent {

    //Estados
    Recibir recibirState;
    Acompañar acompañarState;

    public GameObject mesa;

    // Use this for initialization
    void Start()
    {
        taskList = new List<Task>();
        recibirState = anim.GetBehaviour<Recibir>();
        recibirState.maitre = this;
        acompañarState = anim.GetBehaviour<Acompañar>();
        acompañarState.maitre = this;
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
