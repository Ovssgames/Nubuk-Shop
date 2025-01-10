using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class NextCameraProgress : MonoBehaviour
{
    [SerializeField] float timeWait;

    private PlayerController _playerController;

    private void Start()
    {
        StartValues();
    }

    private void StartValues()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerController>();
    }

    public void NextCamera(GameObject progressItem)
    {
        var VirtualCamera = progressItem.GetComponentInChildren<CinemachineVirtualCamera>();

        if (VirtualCamera != null)
        {
            StartCoroutine(MoveCamera(VirtualCamera));
        }
    }

    private IEnumerator MoveCamera(CinemachineVirtualCamera camera)
    {
        camera.Priority = 15;

        float distanse = Vector3.Distance(Camera.main.transform.position, camera.transform.position);
        while (distanse > 0.01f)
        {
            distanse = Vector3.Distance(Camera.main.transform.position, camera.transform.position);
            yield return null;
        }
        yield return new WaitForSeconds(timeWait);

        camera.Priority = 0;
        _playerController.enabled = true;
    }
}
