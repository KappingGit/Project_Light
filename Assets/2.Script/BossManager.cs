using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    public static BossManager instance;

    private PoolManager poolManager;

    [HideInInspector]
    public float curTime;  // 게임 시간, 다른 스크립트에 사용하기 위해 만듦

    [SerializeField]
    public float bossAppearanceTime; // 보스 출현 시간

    [HideInInspector]
    public bool bossSpawnActive; // 보스 출현 여부

    private void Awake()
    {
        poolManager = GetComponent<PoolManager>();

        if (BossManager.instance == null)
        {
            instance = this;
        }

        bossSpawnActive = false;

    }

    private void Update()
    {
        BossTimer();
        

    }

    [SerializeField]
    private GameObject post_P_Dark;

    [SerializeField]
    private CameraShake cameraShake;

    private bool isTrigger = false;

    // 보스 등장 시간 조절
    public void BossTimer()
    {
        if (!bossSpawnActive)
        {
            curTime += Time.deltaTime;
            //Debug.Log("걸린 시간" + curTime.ToString("F1"));

            if (curTime > bossAppearanceTime + 2f) // 경고 UI 나타나는 시간 관련으로 조율하기
            {
                post_P_Dark.gameObject.SetActive(true);

                if (!isTrigger)
                {
                    //StartCoroutine(Delay(cameraShake));
                }
               
                BossSpawn(); // 여기서 보스 소환 처리

                bossSpawnActive = true;
            }
        }
    }

    public GameObject BossSpawn()
    {
        BossScript newBoss01 = poolManager.GetFromPool<BossScript>(0);

        GameObject newBossObj_01 = newBoss01.gameObject;
        
        return newBossObj_01;
    }

    public void BossReturnPool(BossScript clone)
    {
        poolManager.TakeToPool<BossScript>(clone.idName, clone);
    }


    IEnumerator Delay(CameraShake cameraShake)
    {
        //Debug.Log("카메라 쉐이크 딜레이 코루틴 실행 ");
        isTrigger = true;

        cameraShake.enabled = true;
        CameraShake.instance.shakeRange = 0.1f;
        CameraShake.instance.duration = 0.5f;

        yield return YieldInstuctionCash.WaitForSeconds(1.5f);

        cameraShake.enabled = false;

        StopCoroutine(Delay(cameraShake));
    }

}
