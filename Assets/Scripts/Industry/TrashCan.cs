using NUnit;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    [SerializeField] List<GameObject> animPosition;
    private AudioSource _audioSourse;

    private void Start()
    {
        _audioSourse = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Inventory inventory = other.GetComponent<Inventory>();
        
        if (inventory != null && inventory.count != 0)
        {
            for (int i = 0; i < inventory.thing.Count; i++)
            {
                if (inventory.thing[i] != null)
                {
                    Destroy(inventory.thing[i]);
                    inventory.animator.SetBool("IsHand", false);
                    inventory.animator.SetLayerWeight(1, 0);
                    inventory.thing[i] = null;
                    inventory.count--;
                }
            }
            _audioSourse.Play();
        }
    }
}
