using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectSoundManager : MonoBehaviour
{
    // 나중에 다중상속 기술로 정리

    private AudioSource theAudio;

    [SerializeField]
    private AudioClip audioClip01;

    [SerializeField]
    private AudioClip audioClip02;

    private void Awake()
    {
        theAudio = GetComponent<AudioSource>();

        StartCoroutine(BossDirectSceneSound());

    }

    private void Update()
    {
        
    }

    // 토네이도 걷혀지는 사운드
    private void BossTornadoOffSound()
    {
        theAudio.clip = audioClip01;

        theAudio.volume = 0.5f;

        theAudio.loop = false;

        theAudio.Play();
    }

    private void BossHowling()
    {
        theAudio.clip = audioClip02;

        theAudio.volume = 0.5f;

        theAudio.loop = false;

        theAudio.Play();
    }

    IEnumerator BossDirectSceneSound()
    {
        yield return YieldInstuctionCash.WaitForSeconds(14f);

        BossTornadoOffSound();

        yield return YieldInstuctionCash.WaitForSeconds(3f);

        //todo: 보스 하울링 사운드 처리
        BossHowling();        

        StopCoroutine(BossDirectSceneSound());
    }
}
