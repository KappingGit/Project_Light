using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Script : MonoBehaviour
{
    private void Awake()
    {

    }

    private void Update()
    {
        StageProgress();
        Warnnig();
    }

    [SerializeField]
    private Image stageProgressBar; // 스테이지 진행도 이미지

    private float bossProgress;

    // 스테이지 진척도
    private void StageProgress()
    {
        bossProgress = BossManager.instance.curTime; // 보스매니저 스크립트에서 time변수를 가져옴

        stageProgressBar.fillAmount = bossProgress / BossManager.instance.bossAppearanceTime; //bossProgress: 현 시간 ,bossAppearanceTime: 보스 등장 시간

    }

    private void Warnnig() // 경고 문구
    {
        if (BossManager.instance.bossAppearanceTime - 2f < bossProgress && bossProgress < BossManager.instance.bossAppearanceTime) // 경고 문구 시간 조절할 것
        {
            //Debug.Log("경고 문구 생성");
            //todo : 경고문구 집어 넣기
        }
    }

    private bool pauseActive = false; // 일시정지 상태 여부

    //일시정지 버튼
    public void PauseBtn()
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

}
