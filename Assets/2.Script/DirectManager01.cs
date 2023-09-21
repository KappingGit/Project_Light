using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectManager01 : MonoBehaviour
{
    // 초록마을 연출매니저 스크립트

    [SerializeField]
    private GameObject mainCamera01;

    [SerializeField]
    private GameObject mainCamera02;

    [SerializeField]
    private GameObject mainCamera03;

    [SerializeField]
    private GameObject mainCamera04;

    [SerializeField]
    private GameObject bossObj;

    [SerializeField]
    private GameObject effect01;

    [SerializeField]
    private GameObject effect02;

    [SerializeField]
    private GameObject effect03;

    private Animator bossObj_Anim;

    private void Awake()
    {
        bossObj_Anim = bossObj.gameObject.GetComponent<Animator>();
        bossObj.gameObject.SetActive(false);
        StartCoroutine(DelayTime());
    }

    private void Update()
    {
        


    }
    

    IEnumerator DelayTime()
    {
        mainCamera01.gameObject.SetActive(true);

        yield return YieldInstuctionCash.WaitForSeconds(4f);

        mainCamera01.gameObject.SetActive(false);
        mainCamera02.gameObject.SetActive(true);

        yield return YieldInstuctionCash.WaitForSeconds(3f);

        mainCamera02.gameObject.SetActive(false);
        bossObj.gameObject.SetActive(true);
        mainCamera03.gameObject.SetActive(true);

        yield return YieldInstuctionCash.WaitForSeconds(1.5f);

        // 이사이에 펑퍼지는 효과 넣으면 될듯...

        effect01.gameObject.SetActive(false);
        effect02.gameObject.SetActive(false);
        effect03.gameObject.SetActive(false);

        yield return YieldInstuctionCash.WaitForSeconds(2.5f);

        bossObj_Anim.SetBool("isHowling", true);
        
        mainCamera03.gameObject.SetActive(false);
        mainCamera04.gameObject.SetActive(true);

        //mainCamera04.gameObject.SetActive(true);


        StopAllCoroutines();
    }

}
