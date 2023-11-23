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
        //Movement();

        //TouchMove();

        //transform.position = worldPos;

        MouseMove();

    }

    //[SerializeField] // 캐릭터 속도 조절 접근
    //private float speed; // 정면캐릭터 속도(필요없음)

    private float speed_X = 4f; // 좌우 속도

    private Vector3 moveVector;

    private float verticalVelocity = 0.0f; // 위아래는 의미 없으므로 0

    //private float gravity = 12.0f;

    

    private void Movement()
    {
        moveVector = Vector3.zero; // 계속 초기화

        // x - 왼쪽 오른쪽 컨트롤
        moveVector.x = Input.GetAxisRaw("Horizontal") * speed_X; // InputManager에서 a,d 키코드 빼버림 유의

        // y - 위 아래 컨트롤
        moveVector.y = verticalVelocity;

        // z - 앞 뒤 컨트롤
        moveVector.z = 0f;

        controller.Move(moveVector * Time.deltaTime); // 캐릭터 컨트롤러Move를 통해 캐릭터 자동 움직임을 설정

        //if (controller.isGrounded) //캐릭터가 떨어지는 것을 조건문으로 제작(인스펙터창에서 Gravity를 사용하지 않는 방법)
        //{
        //    verticalVelocity = -0.5f; // 아예 멈추게 할꺼면 0.0f
        //}
        //else
        //{
        //    verticalVelocity -= gravity * Time.deltaTime; // 중력 값을 조정
        //}

        #region 인풋 필드로 움직임 제어



        #endregion



    }

    // 터치용 변수
    private Touch touch;

    private float currentTouch;

    private float previousTouch;

    [SerializeField]
    private Camera mainCamera;

    private void TouchMove()
    {
        #region 터치식 움직임 [공기기 활용해보기] -  주석 처리했음
        // 과거 산물
        //Debug.Log(Screen.width);
        //Debug.Log(Screen.height);

        //if (Input.touchCount > 0) // Input.touchCount => 손가락 개수
        //{
        //    if (Input.GetTouch(0).position.x > (Screen.width / 2))
        //    {
        //        Debug.Log("우측 화면을 터치했습니다.");
        //        controller.Move(Vector3.right * speed_X * Time.deltaTime);

        //    }
        //    else
        //    {
        //        Debug.Log("좌측 화면을 터치했습니다.");
        //        controller.Move(Vector3.left * speed_X * Time.deltaTime);

        //    }

        //    if (Input.GetTouch(0).phase == TouchPhase.Ended) // Ended 손가락이 화면 위를 벗어나 떨어지게 되는 순간...
        //    {
        //        Debug.Log("화면에서 손가락을 뗐습니다.");
        //    }
        //}

        // 터치한 부분에 따라 움직이게 하기
        //if (Input.touchCount > 0) // Input.touchCount => 손가락 개수
        //{




        #endregion

        if (Input.touchCount > 0) // Input.touchCount => 손가락 개수
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved) // 손가락이 유지되는 순간
            {
                controller.Move(mainCamera.WorldToScreenPoint(transform.position) * speed_X * Time.deltaTime);

            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended) // Ended 손가락이 화면 위를 벗어나 떨어지게 되는 순간...
            {
                //Debug.Log("화면에서 손가락을 뗐습니다.");
            }
        }


    }

    private Vector2 screenPos;
    private Vector3 worldPos;

    private Ray ray;

    
    private void MouseMove() // 테스트용 마우스 무브 함수(스크린 좌표와 월드 좌표 활용)
    {

        //ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        //RaycastHit hit;

        //worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        //screenPos = mainCamera.WorldToScreenPoint(Input.mousePosition);

        //Debug.Log("이동 테스트 : " + screenPos.x / 3800f + "   " + screenPos.y + "   ");

        //transform.position = new Vector2(-screenPos.x / 3800f, 0f);

        //if (Input.GetMouseButton(0))
        //{
        //    transform.position.anchoredPosition = Input.mousePosition;
        //}

    }


    // 플레이어가 화면을 클릭하고 있으면 공격, 화면을 때면 공격하지 않게끔...
    private bool isTouch;

    private PlayerShooting playerShooting;

    public bool TouchInput
    {

        set
        {
            isTouch = value;

            if (isTouch == true)
            {
                //공격

            }
            else
            {
                //공격 금지
            }

        }

    }


}