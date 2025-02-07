using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshDestroyer : MonoBehaviour
{
    [SerializeField] NavMeshSurface _navMeshSurface;

    private void Awake()
    {
        NavMesh.RemoveAllNavMeshData();
        _navMeshSurface.BuildNavMesh();
    }
}
