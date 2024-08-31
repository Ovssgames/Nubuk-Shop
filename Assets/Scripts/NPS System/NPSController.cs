using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPSController : MonoBehaviour
{
    private Transform _player;
    private CapsuleCollider _capsuleCollider;

    private void Start()
    {
        StartValues();
    }

    private void StartValues()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        LookPlayer();
    }



    public void MoveAbroad()
    {
        _capsuleCollider.enabled = false;
    }

    public void MoveShop()
    {

    }

    private void LookPlayer()
    {
        transform.LookAt(_player);
    }
}
