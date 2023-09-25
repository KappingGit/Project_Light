using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    private AudioSource theAudio;

    [SerializeField]
    private AudioClip audioClip;

    private void Awake()
    {
        theAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 좌클릭을 하면 씬전환
        {
            MainSceneStartSound();
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
