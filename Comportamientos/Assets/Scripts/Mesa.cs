using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mesa : MonoBehaviour {
    
    public enum Estado
    {
        Libre,
        Ocupada,
        Sucia
    }

    public Decoration[] botellas;

    public Estado estadoActual = Estado.Libre;

    public GameObject platoPrefab;
    public Transform posicionPlato;
    public Transform asiento;

    public Material limpia;
    public Material sucia;

    public MeshRenderer renderer;

    public void Awake()
    {
        botellas = GetComponentsInChildren<Decoration>();
        this.ChangeState(Estado.Libre);
    }

    public void ChangeState(Estado e)
    {
        switch (e)
        {
            case Estado.Libre:
                renderer.material = limpia;
                foreach (Decoration g in botellas)
                {
                    g.gameObject.SetActive(false);
                }
                break;
            case Estado.Ocupada:
                foreach (Decoration g in botellas)
                {
                    g.gameObject.SetActive(true);
                }
                break;
            case Estado.Sucia:
                renderer.material = sucia;
                GameObject p = platoPrefab;
                Destroy(p);
                    break;
        }
    }
}
