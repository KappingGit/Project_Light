using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Camera shakeCamera;
    Vector3 cameraPos;

    [SerializeField]
    [Range(0.01f, 0.1f)] float shakeRange = 0.05f;

    [SerializeField]
    [Range(0.1f, 1f)] float duration = 0.05f;

    private void Awake()
    {

        shakeCamera = GetComponent<Camera>();

    }

    private void Update()
    {
        Shake();
    }

    private void Shake()
    {
        cameraPos = shakeCamera.transform.position;
        InvokeRepeating("StartShake", 0f, 0.005f);
        Invoke("StopShake", duration);
    }

    private void StartShake()
    {
        float cameraPosX = Random.value * shakeRange * 2 - shakeRange;
        float cameraPosY = Random.value * shakeRange * 2 - shakeRange;

        Vector3 cameraPos = shakeCamera.transform.position;

        cameraPos.x += cameraPosX;
        cameraPos.y += cameraPosY;

        shakeCamera.transform.position = cameraPos;

    }

    private void StopShake()
    {
        CancelInvoke("StartShake");
        shakeCamera.transform.position = cameraPos;
    }
}
