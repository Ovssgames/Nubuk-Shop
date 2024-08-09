using UnityEngine;

public class FinishManufactureCollision : MonoBehaviour
{
    [SerializeField] Manufacture _manufacture;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Inventory>() != null)
            _manufacture.MovePrefabToInventory(other);
    }
}
