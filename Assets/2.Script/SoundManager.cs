using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    private AudioSource theAudio;

    [SerializeField]
    private AudioClip audioClip;

    private bool isClick; // 해당 변수는 사운드 매니저에 있는 변수

    private void Awake()
    {
        theAudio = GetComponent<AudioSource>();

        isClick = false;

    }

    private void Update()
    {
        if (!isClick)
        {
            if (Input.GetMouseButtonDown(0)) // 마우스 좌클릭을 하면 씬전환
            {
                isClick = true;
                MainSceneStartSound();
            }
        }
        
    }

    private void MainSceneStartSound()
    {
        theAudio.clip = audioClip;

        theAudio.volume = 0.5f;

        theAudio.loop = false;

        theAudio.Play();
    }

}
