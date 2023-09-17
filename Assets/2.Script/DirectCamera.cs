using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectCamera : MonoBehaviour
{
    // 메인씬 연출용 카메라 스크립트

    float circleR; //반지름
    float deg; //각도
    float objSpeed = 10f; //원운동 속도

    private void Awake()
    {

    }
    
    // 해당 코드 리뷰 필수
    private void Update()
    {
        deg += Time.deltaTime * objSpeed; // 시간에 따른 원운동 속도
        if (deg < 360)
        {
            var rad = Mathf.Deg2Rad * (deg); // 해당 클래스 레퍼런스는(Mathf.Deg2Rad)는 (PI * 2) / 360와 같은 뜻이다
            var x = circleR * Mathf.Sin(rad);
            var y = circleR * Mathf.Cos(rad);
            
            transform.position = transform.position + new Vector3(x, y);
            transform.rotation = Quaternion.Euler(10f, deg * -1, 0); //가운데를 바라보게 각도 조절
        }
        else
        {
            deg = 0;
        }
    }
}
