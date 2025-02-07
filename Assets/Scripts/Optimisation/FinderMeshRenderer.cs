using Unity.VisualScripting;
using UnityEngine;

public class FinderMeshRenderer : MonoBehaviour
{
    private MeshRenderer[] _meshRenderers;

    private void Start()
    {
        _meshRenderers = FindObjectsOfType<MeshRenderer>();

        foreach (MeshRenderer renderer in _meshRenderers)
        {
            MeshRendererSelector selector = renderer.AddComponent<MeshRendererSelector>();
        }

        Destroy(this);
    }
}
