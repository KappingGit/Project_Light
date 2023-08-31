using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // 카메라의 기본적인 transform 값을 코딩으로 고정시키는 방법

    [SerializeField]
    private Transform playerTrans;

    private Vector3 startOffset; // 카메라의 해당 위치 값(지속적 위치값)

    private float startOffset_X = 0;

    private float startOffset_Y = 3.5f;

    

    private void Awake()
    {
        startOffset.z = transform.position.z - playerTrans.position.z; // 카메라의 위치 - 플레이어의 위치
    }

    private void Update()
    {
        FollowCamera();
    }

    private void FollowCamera()
    {
        //transform.position = playerTrans.position + startOffset; // 카메라의 위치는 

        transform.position = new Vector3(startOffset_X, startOffset_Y, playerTrans.position.z + startOffset.z);

    }
}
