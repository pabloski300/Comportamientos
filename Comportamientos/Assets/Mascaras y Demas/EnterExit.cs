using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterExit : MonoBehaviour {

    SkinnedMeshRenderer[] skin;

    public void Start()
    {
        skin = GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach(SkinnedMeshRenderer s in skin)
        {
            s.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mask"))
            foreach (SkinnedMeshRenderer s in skin)
            {
                s.enabled = true;
            }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Mask"))
            foreach (SkinnedMeshRenderer s in skin)
            {
                s.enabled = false;
            }
    }
}
