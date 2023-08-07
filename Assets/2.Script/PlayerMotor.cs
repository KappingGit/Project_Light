using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{

    private CharacterController controller;

    private float speed = 5.0f; // 캐릭터 속도

    // Start is called before the first frame update
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        controller.Move((Vector3.forward * speed)*Time.deltaTime ); // 캐릭터 컨트롤러Move를 통해 캐릭터 자동 움직임을 설정
    }
}
