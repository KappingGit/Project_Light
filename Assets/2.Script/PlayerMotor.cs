using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{

    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Movement();
    }

    private float speed = 15.0f; // 캐릭터 속도

    private Vector3 moveVector;

    private float verticalVelocity = 0.0f;

    //private float gravity = 12.0f;

    private void Movement()
    {
        moveVector = Vector3.zero; // 계속 초기화

        // x - 왼쪽 오른쪽 컨트롤
        moveVector.x = Input.GetAxisRaw("Horizontal") * speed; // 기본적인 테스트용

        // y - 위 아래 컨트롤
        moveVector.y = verticalVelocity;

        // z - 앞 뒤 컨트롤
        moveVector.z = speed;

        controller.Move(moveVector * Time.deltaTime); // 캐릭터 컨트롤러Move를 통해 캐릭터 자동 움직임을 설정

        //if (controller.isGrounded) //캐릭터가 떨어지는 것을 조건문으로 제작(인스펙터창에서 Gravity를 사용하지 않는 방법)
        //{
        //    verticalVelocity = -0.5f; // 아예 멈추게 할꺼면 0.0f
        //}
        //else
        //{
        //    verticalVelocity -= gravity * Time.deltaTime; // 중력 값을 조정
        //}


        #region 터치식 움직임 [공기기 활용해보기] -  주석 처리했음

        //Debug.Log(Screen.width);
        //Debug.Log(Screen.height);

        //if (Input.touchCount > 0) // Input.touchCount => 손가락 개수
        //{

        //    if (Input.GetTouch(0).position.x > (Screen.width / 2))
        //    {
        //        Debug.Log("우측 화면을 터치했습니다.");
        //        //controller.Move(Vector3.right* speed * Time.deltaTime);

        //    }
        //    else
        //    {
        //        Debug.Log("좌측 화면을 터치했습니다.");
        //        //controller.Move(Vector3.left * speed * Time.deltaTime);

        //    }

        //    if (Input.GetTouch(0).phase == TouchPhase.Ended) // Ended 손가락이 화면 위를 벗어나 떨어지게 되는 순간...
        //    {
        //        Debug.Log("화면에서 손가락을 뗐습니다.");
        //    }
        //}

        #endregion

    }

}
