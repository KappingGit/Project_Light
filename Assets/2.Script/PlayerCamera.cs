using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // 카메라의 기본적인 transform 값을 코딩으로 고정시키는 방법

    [SerializeField]
    private Transform playerTrans;

    private Vector3 startOffset;

    private void Awake()
    {
        startOffset = transform.position - playerTrans.position;
    }

    private void Update()
    {
        FollowCamera();
    }

    private void FollowCamera()
    {
        transform.position = playerTrans.position + startOffset;
    }
}
