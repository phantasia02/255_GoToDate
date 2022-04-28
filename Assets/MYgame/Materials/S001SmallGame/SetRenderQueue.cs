using UnityEngine;

[AddComponentMenu("Rendering/SetRenderQueue")]
public class SetRenderQueue : MonoBehaviour
{

    [SerializeField]
    protected int[] m_queues = new int[] { 3011 };

    protected void Start()
    {
        Renderer renderer = this.gameObject.GetComponent<Renderer>();
        if (renderer == null)
            return;

        Material[] materials = renderer.materials;
        for (int i = 0; i < materials.Length && i < m_queues.Length; ++i)
        {
            materials[i].renderQueue = m_queues[i];
        }
    }
}