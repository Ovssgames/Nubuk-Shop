using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishManufactureCollision : MonoBehaviour
{
    [SerializeField] Manufacture _manufacture;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Inventory>() != null)
        {
            _manufacture.onFinishCollision.Invoke();
        }
    }
}
