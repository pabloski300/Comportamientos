using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

[AddComponentMenu("Rendering/SetRenderQueueCharacter")]

public class SetRenderQueueCharacter : MonoBehaviour {

    [SerializeField]
    protected int[] m_queues = new int[] { 3000 };

    SkinnedMeshRenderer renderer;

    protected void Awake()
    {
        renderer = GetComponent<SkinnedMeshRenderer>();
        Material[] materials = renderer.materials;
        for (int i = 0; i < materials.Length && i < m_queues.Length; ++i)
        {
            materials[i].renderQueue = m_queues[i];
        }
    }
}
