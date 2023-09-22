using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Script : MonoBehaviour
{
    public static UI_Script instance;

    [SerializeField]
    private GameObject stageUI;

    [SerializeField]
    private GameObject warningUI;

    private void Awake()
    {
        StartCoroutine(StageUI());

        if (UI_Script.instance == null)
        {
            instance = this;
        }

    }

    private void Update()
    {
        if (!BossManager.instance.bossSpawnActive)
        {
            StageProgress();
        }

        if (BossManager.instance.bossAppearanceTime < bossProgress && bossProgress < BossManager.instance.bossAppearanceTime + 1f) // 해당 코루틴에 if문을 넣는 것으로 바꿀것
        {
            //todo : 보스가 나타날 시점
            Warnnig();
        }

        if (!BossManager.instance.bossSpawnActive)
        {
            bossHPBar.gameObject.SetActive(false);
        }
        else if (BossManager.instance.bossSpawnActive)
        {
            BossHPUI();
        }

        PausePopup();

    }

    [SerializeField]
    private GameObject stageProgressbar; // 스테이지 진해도 바

    [SerializeField]
    private Image stageProgressFill; // 스테이지 진행도 fill이미지

    private float bossProgress;

    // 스테이지 진척도
    private void StageProgress()
    {
        stageProgressbar.gameObject.SetActive(true);

        bossProgress = BossManager.instance.curTime; // 보스매니저 스크립트에서 time변수를 가져옴

        stageProgressFill.fillAmount = bossProgress / BossManager.instance.bossAppearanceTime; //bossProgress: 현 시간 ,bossAppearanceTime: 보스 등장 시간

        if (BossManager.instance.bossAppearanceTime < bossProgress)
        {
            stageProgressbar.gameObject.SetActive(false);
        }

    }

    private void Warnnig() // 경고 문구
    {

        //Debug.Log("경고 문구 생성");
        StartCoroutine(BossWarning());
    }

    private bool pauseActive = false; // 일시정지 상태 여부

    //일시정지 버튼
    public void PauseBtn() // 재시작 버튼도 겸하고있음
    {

        if (pauseActive)
        {
            Time.timeScale = 1f;
            pauseActive = false;
        }
        else
        {
            Time.timeScale = 0f;
            pauseActive = true;
        }

    }

    [SerializeField]
    private GameObject pausePopup;

    private void PausePopup()
    {
        if (pauseActive)
        {
            pausePopup.gameObject.SetActive(true);
        }
        else if (!pauseActive)
        {
            pausePopup.gameObject.SetActive(false);
        }
    }

    [SerializeField]
    public GameObject bossHPBar;

    [SerializeField]
    private Image bossHPBarFill;

    private void BossHPUI()
    {
        bossHPBar.gameObject.SetActive(true);

        bossHPBarFill.fillAmount = BossScript.instance.bossCurHP / BossScript.instance.bossMaxHP;

        if (BossScript.instance.bossCurHP < 0f)
        {
            bossHPBar.gameObject.SetActive(false);
        }
    }

    //YieldInstuctionCash 미리 캐싱해둔것
    IEnumerator StageUI() // 애니메이션 효과 시간 맞추기
    {
        yield return YieldInstuctionCash.WaitForSeconds(1.5f);
        stageUI.gameObject.SetActive(true);
        yield return YieldInstuctionCash.WaitForSeconds(2f); // 나중에 애니메이션 루프 조정하기
        stageUI.gameObject.SetActive(false);
        StopCoroutine(StageUI());
    }

    IEnumerator BossWarning()
    {
        warningUI.gameObject.SetActive(true);
        yield return YieldInstuctionCash.WaitForSeconds(2.0f);
        warningUI.gameObject.SetActive(false); // 페이드 및 씬전환 적용해보고 위치 조정

        //페이드 아웃과 씬전환을 넣을 것

        StopCoroutine(BossWarning());
    }

}
