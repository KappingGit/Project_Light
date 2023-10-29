using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectEventManager : MonoBehaviour
{
    [SerializeField]
    private GameObject camera01;

    [SerializeField]
    private GameObject camera02;

    private void Awake()
    {

        // 한번 초기화
        camera01.gameObject.SetActive(true);
        camera02.gameObject.SetActive(false);

        StartCoroutine(CameraDelay());

    }

    private void Update()
    {

    }

    IEnumerator CameraDelay()
    {

        yield return YieldInstuctionCash.WaitForSeconds(11f);
        camera01.gameObject.SetActive(false);
        camera02.gameObject.SetActive(true);

        StopCoroutine(CameraDelay());
    }

}
