using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshDestroyer : MonoBehaviour
{
    [SerializeField] NavMeshSurface _navMeshSurface;

    private void Start()
    {
        NavMesh.RemoveAllNavMeshData();
        _navMeshSurface.BuildNavMesh();
    }

    public void NavMeshUpdate()
    {
        _navMeshSurface.UpdateNavMesh(_navMeshSurface.navMeshData);
        Debug.Log("navMeshUpdate");
    }
}
