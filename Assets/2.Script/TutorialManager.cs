using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬전환에 사용
using UnityEngine.UI; // UI기능에 사용


public class TutorialManager : MonoBehaviour
{
    //튜토리얼 용 씬 스크립트

    [SerializeField]
    private Image fadeImage;

    [SerializeField]
    private GameObject tutorialPage01;

    [SerializeField]
    private GameObject tutorialPage02;

    [SerializeField]
    private GameObject tutorialPage03;

    [SerializeField]
    private GameObject tutorialPage04;

    [SerializeField]
    private GameObject leftBtn;

    [SerializeField]
    private GameObject rightBtn;

    //[SerializeField]
    //private GameObject nextScene; // 아무 화면 클릭으로 씬전환

    private void Awake()
    {
        //fadeImage.color = new Color(0, 0, 0, 0f); // 기본 초기화

        fadeCount = 0f;
        fadeImage.color = new Color(0, 0, 0, fadeCount); // 기본 페이드 아웃으로 초기화

        //StartCoroutine(FadeIn());

        // 초기화작업
        tutorialPage01.gameObject.SetActive(true);

        tutorialPage02.gameObject.SetActive(false);

        tutorialPage03.gameObject.SetActive(false);

        tutorialPage04.gameObject.SetActive(false);


    }

    private bool clicking = false;

    private void Update()
    {
        //if (!clicking)
        //{
        //    if (Input.GetKeyDown(KeyCode.Space)) // 엔터 누르면 하면 씬전환
        //    {
        //        clicking = true;
        //        //Debug.Log("마우스 버튼이 눌렸습니다.");
        //        if (clicking) // 파라매터 넣어서 "현재 페이드가 진행중이라면 클릭 안되게 처리
        //        {
        //            StartCoroutine(FadeOut());
        //        }

        //    }
        //}

        //if (tutorialPage04.gameObject.activeSelf == true) // 4페이지라면
        //{

        //}


        //1페이지와 4페이지 각각 버튼 좌우 보여지는 방식이 다르므로...
        if (tutorialPage01.gameObject.activeSelf == true) // 1페이지라면 왼쪽 버튼 없애기
        {
            leftBtn.gameObject.SetActive(false);
            rightBtn.gameObject.SetActive(true);
        }
        else if (tutorialPage02.gameObject.activeSelf == true)
        {
            leftBtn.gameObject.SetActive(true);
            rightBtn.gameObject.SetActive(true);
        }
        else if (tutorialPage03.gameObject.activeSelf == true)
        {
            leftBtn.gameObject.SetActive(true);
            rightBtn.gameObject.SetActive(true);
        }
        else if (tutorialPage04.gameObject.activeSelf == true) // 4페이지라면 오른쪽 버튼 없애기
        {
            leftBtn.gameObject.SetActive(true);
            rightBtn.gameObject.SetActive(false);
        }
        
        // PC버전용 주석
        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    LeftBtn();
        //}
        
        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    RightBtn();
        //}

    }

    public virtual void SkipBtn() // 스킵 버튼
    {
        Debug.Log("스킵 버튼");
        StartCoroutine(FadeOut());
    }

    //private bool leftActive = false;
    //private bool rightActive = false;

    public void LeftBtn()
    {
        
        if (tutorialPage02.activeSelf == true)
        {
            tutorialPage02.gameObject.SetActive(false);
            tutorialPage01.gameObject.SetActive(true);
        }
        else if (tutorialPage03.activeSelf == true)
        {
            tutorialPage03.gameObject.SetActive(false);
            tutorialPage02.gameObject.SetActive(true);
        }
        else if (tutorialPage04.activeSelf == true)
        {
            tutorialPage04.gameObject.SetActive(false);
            tutorialPage03.gameObject.SetActive(true);
        }
    }

    public void RightBtn()
    {
       
        if (tutorialPage01.activeSelf == true)
        {
            tutorialPage01.gameObject.SetActive(false);
            tutorialPage02.gameObject.SetActive(true);
        }
        else if (tutorialPage02.activeSelf == true)
        {
            tutorialPage02.gameObject.SetActive(false);
            tutorialPage03.gameObject.SetActive(true);
        }
        else if (tutorialPage03.activeSelf == true)
        {
            tutorialPage03.gameObject.SetActive(false);
            tutorialPage04.gameObject.SetActive(true);
        }
    }

    public virtual void StartGame() // 아무 화면을 눌러 게임시작 버튼
    {
        StartCoroutine(FadeOut());
    }

    private void ChangeScene() // 씬전환에 사용
    {
        SceneManager.LoadScene("GameScene01"); // 해당 씬으로 이동 나중에 선택한 월드로 이동하게 설정
    }

    private float fadeCount = 0f; // 페이드 아웃에 사용될 변수

    // 속도 조절에 사용될 코루틴 YieldInstuctionCash: 캐싱 작업해 놓은것 불러오기
    IEnumerator FadeOut()// 점점 어두워지게
    {
        while (fadeCount <= 1.0f)
        {
            fadeCount += 0.01f;
            yield return YieldInstuctionCash.WaitForSeconds(0.01f); // 캐싱 불러오기
            fadeImage.color = new Color(0, 0, 0, fadeCount); // 투명도(알파값)은 1이 최대치이다.
        }
        yield return YieldInstuctionCash.WaitForSeconds(1.5f);
        ChangeScene(); // 씬전환

    }

    IEnumerator FadeIn()// 점점 밝아지게
    {
        fadeCount = 1f; // 한번더 초기화
        //yield return YieldInstuctionCash.WaitForSeconds(1.5f);

        while (fadeCount > 0.0f)
        {
            fadeCount -= 0.01f;
            yield return YieldInstuctionCash.WaitForSeconds(0.01f); // 캐싱 불러오기
            fadeImage.color = new Color(0, 0, 0, fadeCount); // 투명도(알파값)은 1이 최대치이다.
        }

        yield return YieldInstuctionCash.WaitForSeconds(1.5f);
        StartCoroutine(FadeOut());

        yield return YieldInstuctionCash.WaitForSeconds(1.5f);
        StopCoroutine(FadeIn());

    }

}
