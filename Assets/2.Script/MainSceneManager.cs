using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬전환에 사용
using UnityEngine.UI; // UI기능에 사용

public class MainSceneManager : MonoBehaviour
{
    // 전체적으로 사용될 씬전환용

    [SerializeField]
    private Image fadeImage;

    private void Awake()
    {
        //fadeImage.color = new Color(0, 0, 0, 0f); // 기본 초기화
    }

    private bool clicking = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !clicking) // 마우스 좌클릭을 하면 씬전환
        {
            clicking = true;
            Debug.Log("마우스 버튼이 눌렸습니다.");
            if (clicking) // 파라매터 넣어서 "현재 페이드가 진행중이라면 클릭 안되게 처리
            {
                StartCoroutine(FadeInOut()); 
            }
            
        }
        
    }

    private void ChangeScene() // 씬전환에 사용
    {
        SceneManager.LoadScene("GameScene01"); // 해당 씬으로 이동 나중에 선택한 월드로 이동하게 설정
    }

    private float fadeCount = 0f; // 페이드 인아웃에 사용될 변수

    // 속도 조절에 사용될 코루틴 YieldInstuctionCash: 캐싱 작업해 놓은것 불러오기
    IEnumerator FadeInOut()
    {
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return YieldInstuctionCash.WaitForSeconds(0.01f); // 캐싱 불러오기
            fadeImage.color = new Color(0, 0, 0, fadeCount); // 투명도(알파값)은 1이 최대치이다.
        }
        yield return YieldInstuctionCash.WaitForSeconds(1.5f);
        ChangeScene(); // 씬전환

    }
}
