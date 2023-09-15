using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬전환에 사용
using UnityEngine.UI; // UI기능에 사용

public class ChangeSceneManager : MonoBehaviour
{
    // 전체적으로 사용될 씬전환용

    [SerializeField]
    private Image fadeImage;

    private void Awake()
    {
        //fadeImage.color = new Color(0, 0, 0, 0f); // 기본 초기화
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 좌클릭을 하면 씬전환
        {
            Debug.Log("마우스 버튼이 눌렸습니다.");
            FadeInOut();
        }
    }

    private void ChangeScene() // 씬전환에 사용
    {
        SceneManager.LoadScene("GameScene01"); // 해당 씬으로 이동
    }

    private float fadeCount = 0f; // 페이드 인아웃에 사용될 변수

    private void FadeInOut() // 페이드 아웃
    {
        // 코루틴으로 속도조절 또는 다른 방법 모색하기
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;

            fadeImage.color = new Color(0, 0, 0, fadeCount); // 투명도(알파값)은 1이 최대치이다.
        }
    }

    // 속도 조절에 사용될 코루틴
    IEnumerator WaitForTime()
    {
        yield return 0;
    }
}
