using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬전환에 사용
using UnityEngine.UI; // UI기능에 사용

public class ChangeSceneManager : MonoBehaviour
{
    [SerializeField]
    private Image fadeImage;

    public static ChangeSceneManager instance;

    private bool cutSceneisActive;

    [SerializeField]
    private GameObject cutSceneVideo01;
      

    private void Awake()
    {

        if (ChangeSceneManager.instance == null)
        {
            instance = this;
        }

        fadeImage.color = new Color(0, 0, 0, 1.0f); // 기본 초기화
        StartCoroutine(FadeIn()); // 쌩으로 넣는 것은 하면안된다(코루틴이 지속적으로 처리가 되는 문제가 발생 단, 조건문을 활용하면 가능)

        cutSceneisActive = false;
    }

    private void Update()
    {
        // 조건문 나중에 정리하기
        //if (BossManager.instance.bossAppearanceTime + 1f < BossManager.instance.curTime && BossManager.instance.curTime < BossManager.instance.bossAppearanceTime + 2f)
        //{

        //    if (!cutSceneisActive)
        //    {
        //        cutSceneisActive = true; //트리거 브레이킹
        //        Debug.Log("컷신 시작");
        //        CutScene01();
        //    }
        //}

        if (BossManager.instance.bossAppearanceTime + 2f < BossManager.instance.curTime && BossManager.instance.curTime < BossManager.instance.bossAppearanceTime + 3f)
        {

            if (!cutSceneisActive)
            {
                cutSceneisActive = true; //트리거 브레이킹
                //StartCoroutine(CutSceneVideo());

                if (cutSceneisActive)
                {
                    
                    StartCoroutine(CutSceneVideo());
                }

            }
        }


        if (UI_Script.instance.isGameOver) // 게임오버가 되면
        {
            UI_Script.instance.isGameOver = false;
            StartCoroutine(GameOverScene());
        }
    }

    private void ChangeScene_MainScene() // 메인씬으로 넘어가는 함수
    {
        SceneManager.LoadScene("MainScene01");
    }

    private float curProgress;

    private void CutScene01() // 바람마을 컷씬
    {

        //SceneManager.LoadScene("B", LoadSceneMode.Additive);

        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("B"));

        //SceneManager.LoadScene("Wind-Boss_Events", LoadSceneMode.Additive);

        //curProgress = BossManager.instance.curTime;

        //todo : 보스가 나타날 시점

        //StartCoroutine(CutSceneDelay());

        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("DirectActionScene01"));

    }

    private float fadeInCount; // 페이드 인에 사용될 변수

    private float fadeOutCount; //페이드 아웃에 사용될 변수

    [HideInInspector]
    public bool fadeInOuting;// 페이드 인 아웃 중인지 판단 여부

    // 속도 조절에 사용될 코루틴 YieldInstuctionCash: 캐싱 작업해 놓은것 불러오기
    IEnumerator FadeIn() // 점점 밝아지게
    {
        fadeInCount = 1f;
        Debug.Log("페이드 인 중");
        fadeInOuting = true;
        while (0.0f < fadeInCount)
        {
            fadeInCount -= 0.01f;
            yield return YieldInstuctionCash.WaitForSeconds(0.01f); // 캐싱 불러오기
            fadeImage.color = new Color(0, 0, 0, fadeInCount); // 투명도(알파값)은 1이 최대치이다.
        }

        fadeInOuting = false;
        yield return YieldInstuctionCash.WaitForSeconds(1.5f);
        StopCoroutine(FadeIn());
    }

    IEnumerator FadeOut()// 점점 어두워지게
    {
        fadeOutCount = 0f;
        Debug.Log("페이드 아웃 중");
        fadeInOuting = true;
        while (fadeOutCount < 1.0f)
        {
            fadeOutCount += 0.01f;
            yield return YieldInstuctionCash.WaitForSeconds(0.01f); // 캐싱 불러오기
            fadeImage.color = new Color(0, 0, 0, fadeOutCount); // 투명도(알파값)은 1이 최대치이다.
        }

        fadeInOuting = false;
        yield return YieldInstuctionCash.WaitForSeconds(1.5f);
        StopCoroutine(FadeIn());
        
        //ChangeScene(); // 씬전환 연출쪽으로 넘어가게 설정

    }

    IEnumerator CutSceneDelay()
    {
        StartCoroutine(FadeOut());
        yield return YieldInstuctionCash.WaitForSeconds(1.5f);
        //Time.timeScale = 0f;
        //SceneManager.LoadScene("DirectActionScene01", LoadSceneMode.Additive); // 현재씬을 종료하지 않고 "씬 이름"씬을 실행
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("DirectActionScene01"));
        //SceneManager.GetActiveScene();

        Debug.Log("씬전환");

        yield return YieldInstuctionCash.WaitForSeconds(5f);
        //Time.timeScale = 1f;
        StopCoroutine(CutSceneDelay());

        StartCoroutine(FadeIn());
    }

    IEnumerator GameOverScene() // 게임 오버되었으면 메인씬으로 넘어가기
    {
        StartCoroutine(FadeOut());
        yield return YieldInstuctionCash.WaitForSeconds(2f);

        ChangeScene_MainScene();
        //Debug.Log("씬전환");

        yield return YieldInstuctionCash.WaitForSeconds(5f);
        
        StartCoroutine(FadeIn());
    }

    IEnumerator CutSceneVideo()
    {
        //Debug.Log("코루틴 시작");
        Time.timeScale = 0f;
        
        cutSceneVideo01.gameObject.SetActive(true);


        yield return new WaitForSecondsRealtime(19f); // 타임스케일에 영향안가게

        //Debug.Log("웨잇폴 세컨드 끝");

        Time.timeScale = 1f;

        cutSceneVideo01.gameObject.SetActive(false);


        StopCoroutine(CutSceneVideo());
    }

}
