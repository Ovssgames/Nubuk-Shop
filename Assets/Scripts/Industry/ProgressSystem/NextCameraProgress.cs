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

        StartCoroutine(MoveCamera(VirtualCamera));
    }

    private IEnumerator MoveCamera(CinemachineVirtualCamera camera)
    {
        camera.Priority = 15;

        while (Camera.main.transform.position != camera.transform.position)
        {
            yield return null;
        }
        yield return new WaitForSeconds(timeWait);

        _playerController.enabled = true;
    }
}
