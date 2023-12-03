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

    [HideInInspector]
    public bool cutSceneisActive;

    [SerializeField]
    private GameObject cutSceneVideo01;

    [HideInInspector]
    public bool isSkip;

    private void Awake()
    {

        if (ChangeSceneManager.instance == null)
        {
            instance = this;
        }

        fadeImage.color = new Color(0, 0, 0, 1.0f); // 기본 초기화
        StartCoroutine(FadeIn()); // 쌩으로 넣는 것은 하면안된다(코루틴이 지속적으로 처리가 되는 문제가 발생 단, 조건문을 활용하면 가능)

        cutSceneisActive = false;

        isSkip = false;

        stageNum = 1; // 스테이지 번호 변수 (나중에 스테이지 선택 기능 구현할때 다시 고려하기)

        //isBreak = false; // 임시 브레이킹용 나중에 지울것


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

        /*#########################################################################################*/
        // 아래 코드는 컷씬 비디오 관련

        #region 보스 출현 연출 컷씬 비디오 관련(일시 테스트로 주석처리)

        //if (BossManager.instance.bossAppearanceTime + 2f < BossManager.instance.curTime && BossManager.instance.curTime < BossManager.instance.bossAppearanceTime + 3f)
        //{

        //    if (!cutSceneisActive)
        //    {
        //        cutSceneisActive = true; //트리거 브레이킹
        //        //StartCoroutine(CutSceneVideo());

        //        if (cutSceneisActive)
        //        {

        //            StartCoroutine(CutSceneVideo());
        //        }

        //    }
        //}


        //if (cutSceneisActive)
        //{
        //    if (isSkipBtn || Input.GetKey(KeyCode.Escape)) // 스킵하게 되면...
        //    {
        //        isSkipBtn = false;
        //        if (!isSkip)
        //        {
        //            isSkip = true;

        //            //cutSceneisActive = false;

        //            StopCoroutine(CutSceneVideo());

        //            Time.timeScale = 1f;

        //            cutSceneVideo01.gameObject.SetActive(false);
        //        }
        //    }

        //}

        #endregion


        /*#########################################################################################*/

        if (UI_Script.instance.isGameOver) // 게임오버가 되면
        {
            UI_Script.instance.isGameOver = false;
            StartCoroutine(GameOverScene());
        }

        if (UI_Script.instance.isRestart)
        {
            UI_Script.instance.isRestart = false;
            StartCoroutine(Restart());

        }

        StageClear();


        //Debug.Log("스테이지 번호 : " + stageNum);
        //Debug.Log("체인지 씬 매니저에서 유아이 이스 스테이지 클리어 디버그" + UI_Script.instance.isStageClear);
        //Debug.Log("체인지 씬 매니저에서 빅토리 트리거 확인 : "+UI_Script.instance.isVictoryTrigger);
        //Debug.Log("스테이지 클리어 데이터 : " + isStageClear_Data);
        

    }

    private void ChangeScene_MainScene() // 메인씬으로 넘어가는 함수
    {
        SceneManager.LoadScene("MainScene01");
    }

    private bool isSkipBtn;

    public void CutSceneSkipBtn()
    {
        isSkipBtn = true;
    }

    private void RestartScene()
    {
        SceneManager.LoadScene("GameScene01");
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

    [HideInInspector]
    public int stageNum;

    [HideInInspector]
    public bool nextStageActive; // 다음 스테이지 작동 여부

    [HideInInspector]
    public bool isStageClear_Data;

    // 스테이지별 브레이킹 변수
    private bool isBreak = false;

    
    private void StageClear()
    {
        if (BossManager.instance.bossSpawnActive)
        {
            if (isStageClear_Data)
            {

                if (stageNum == 1 && !isBreak)
                {
                    Debug.Log("1스테이지 클리어");
                    isBreak = true;
                    StartCoroutine(StageBreaking()); // 임시 : 스테이지 브레이킹용도
                    StartCoroutine(StageClearCoroutine());

                }
                else if (stageNum == 2 && !isBreak)
                {
                    Debug.Log("2스테이지 클리어");
                    isBreak = true;
                    StartCoroutine(StageBreaking());
                    //StartCoroutine(StageClearCoroutine());
                    //Debug.Log("스테이지 클리어 관련 함수(2스테이지 클리어 디버그)");
                    //StartCoroutine(StageClearCoroutine());
                    StartCoroutine(GameOverScene());

                }

            }
        }
        
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
        //Debug.Log("게임 종료 코루틴");
        yield return YieldInstuctionCash.WaitForSeconds(1f); // 빅토리 UI보여지게 하는거 시간버는 용도
        StartCoroutine(FadeOut());
        yield return YieldInstuctionCash.WaitForSeconds(4f);
        //Debug.Log("2스테이지 클리어 메인씬으로가기");
        ChangeScene_MainScene();
        //Debug.Log("씬전환");

        yield return YieldInstuctionCash.WaitForSeconds(5f);

        StartCoroutine(FadeIn());
    }

    IEnumerator Restart() // 게임 오버되었으면 메인씬으로 넘어가기
    {
        StartCoroutine(FadeOut());
        yield return YieldInstuctionCash.WaitForSeconds(3f);

        RestartScene();
        //Debug.Log("씬전환");

        yield return YieldInstuctionCash.WaitForSeconds(5f);

        StartCoroutine(FadeIn());
    }

    IEnumerator CutSceneVideo()
    {
        //Debug.Log("코루틴 시작");
        Time.timeScale = 0f;

        cutSceneVideo01.gameObject.SetActive(true);


        int timeCount = 0;

        while (timeCount < 19)
        {
            timeCount++;

            //if ()
            //{
            //    Time.timeScale = 1f;

            //    cutSceneVideo01.gameObject.SetActive(false);
            //}


            yield return new WaitForSecondsRealtime(1f);

        }

        //yield return new WaitForSecondsRealtime(19f); // 타임스케일에 영향안가게

        //Debug.Log("웨잇폴 세컨드 끝");

        Time.timeScale = 1f;

        cutSceneVideo01.gameObject.SetActive(false);


        StopCoroutine(CutSceneVideo());
    }

    IEnumerator StageClearCoroutine()
    {
        Debug.Log("다음 스테이지 넘어감");
        //yield return YieldInstuctionCash.WaitForSeconds(3f);
        //StartCoroutine(FadeOut());

        stageNum += 1;
        //UI_Script.instance.isVictoryTrigger = false; // 스테이지 변화되면서 승리 트리거 초기화
        //StartCoroutine(FadeIn());

        //yield return YieldInstuctionCash.WaitForSeconds(8f);

        // 초기화 진행은 게임매니저 스크립트에서 작업중
        //UI_Script.instance.bossProgress = 0;

        //UI_Script.instance.stageProgressbar.gameObject.SetActive(true);

        //BossManager.instance.bossSpawnActive = false;


        yield return YieldInstuctionCash.WaitForSeconds(2f);
        StopCoroutine(StageClearCoroutine());
    }

    IEnumerator StageBreaking() // 스테이지 브레이크용도
    {
        yield return YieldInstuctionCash.WaitForSeconds(12f);

        isBreak = false;

        yield return YieldInstuctionCash.WaitForSeconds(2f);
        StopCoroutine(StageBreaking());
    }

}
