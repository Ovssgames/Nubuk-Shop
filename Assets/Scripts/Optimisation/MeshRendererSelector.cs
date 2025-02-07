using UnityEngine;

public class MeshRendererSelector : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private MeshRenderer _startMeshRenderer;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _startMeshRenderer = _meshRenderer;
    }

    private void OnBecameInvisible()
    {
        // Отключаем MeshRenderer, когда объект выходит из поля зрения камеры
        _meshRenderer.material = null;
        Debug.Log("MeshRendererInvisible");
    }

    private void OnBecameVisible()
    {
        // Включаем MeshRenderer, когда объект появляется в поле зрения камеры
        _meshRenderer.material = _startMeshRenderer.material;
        Debug.Log("MeshRendererVisible");
    }
}
