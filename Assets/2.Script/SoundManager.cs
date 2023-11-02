using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬전환에 사용
using UnityEngine.UI; // UI기능에 사용


public class SoundManager : MonoBehaviour
{
    
    private AudioSource theAudio;

    [SerializeField]
    private AudioClip startAudioClip;

    private bool isClick; // 해당 변수는 사운드 매니저에 있는 변수

    //private bool clicking = false;

    private Scene nowScene;

    private void Awake()
    {
        theAudio = GetComponent<AudioSource>();

        if (theAudio == null)
        {
            Debug.Log("오디오 소스가 null상태입니다");
        }

        isClick = false;

        nowScene = SceneManager.GetActiveScene(); // 해당씬이 어떤 씬인지 알 수 있는 방법

        Debug.Log(nowScene.name);
    }

    private void Update()
    {
        //if (!isClick)
        //{
        //    if (Input.GetMouseButtonDown(0)) // 마우스 좌클릭을 하면 씬전환
        //    {
        //        isClick = true;
        //        MainSceneStartSound();
        //    }
        //}

        //PlayerLevelUpSound();

        switch (nowScene.name)
        {
            case "MainScene01":
                if (!isClick)
                {
                    
                    if (Input.GetMouseButtonDown(0)) // 마우스 좌클릭을 하면 씬전환
                    {
                        isClick = true;
                        MainSceneStartSound();
                    }
                }
                break;

            case "StartScene01":

                break;

            case "GameScene01":
                //WindStageBGM();
                PlayerLevelUpSound();
                VictorySound();
                

                break;
        }

       
        
    }

    private void MainSceneStartSound() // 게임 시작 버튼 클릭시 나는 소리
    {
        theAudio.clip = startAudioClip;

        theAudio.volume = 0.5f;

        theAudio.loop = false;

        theAudio.Play();
    }

    [SerializeField]
    private AudioClip windStageBgm;

    private void WindStageBGM() // 해당 스테이지별 사운드 적용하는 함수(오류있어서 배제시킴)
    {
        theAudio.clip = windStageBgm;

        theAudio.volume = 0.5f;

        //theAudio.loop = true;

        theAudio.Play();
    }

    [SerializeField]
    private AudioClip levelUpClip;

    private bool isTrigger01 = true;

    private void PlayerLevelUpSound()
    {
        if (UI_Script.instance.isLevelUp)
        {
            if (isTrigger01)
            {
                isTrigger01 = false;
                Debug.Log("레벨업했습니다-사운드");

                theAudio.PlayOneShot(levelUpClip);

                StartCoroutine(Trigger01_Delay()); // 트리거 다시 트루화(다음 레벨업때 송출하기 위해)

            }
            
        }

    }

    [SerializeField]
    private AudioClip victoryClip;

    private bool isTrigger02 = true;

    private void VictorySound()
    {
        if (BossManager.instance.bossSpawnActive)
        {
            if (BossScript.instance.isBossDie)
            {
                if (isTrigger02)
                {
                    isTrigger02 = false;

                    Debug.Log("승리 사운드"); 

                    theAudio.PlayOneShot(victoryClip);

                    //StartCoroutine(Trigger02_Delay()); // 트리거 다시 트루화(다음 레벨업때 송출하기 위해)

                }
            }
        }
        
    }

    IEnumerator Trigger01_Delay() // 레벨업 트리거용 코루틴
    {
        yield return YieldInstuctionCash.WaitForSeconds(1f);

        isTrigger01 = true;

        StopCoroutine(Trigger01_Delay());
    }

    IEnumerator Trigger02_Delay() // 빅토리 사운드 트리거용 코루틴
    {
        yield return YieldInstuctionCash.WaitForSeconds(0.1f);

        //isTrigger02 = true;

        StopCoroutine(Trigger02_Delay());
    }

}
