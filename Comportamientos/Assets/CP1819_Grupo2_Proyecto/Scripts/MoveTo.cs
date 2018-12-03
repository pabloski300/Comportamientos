using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour {

    NavMeshAgent agent;
    Camera cam;
    Animator anim;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        cam = FindObjectOfType<Camera>();
        anim = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask(new string[] { "Barra", "Cocina", "Mesas", "Calle" })))
            {
                Debug.Log(hit.point);
                agent.SetDestination(hit.point);
            }
        }
        //anim.SetFloat("speed", agent.desiredVelocity.magnitude);
    }
}
