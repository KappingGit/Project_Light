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

        bossSpawnActive = false; //다시 실행할때 액티브 거짓판정으로

    }

    private void Update()
    {
        BossTimer();
        

    }

    [SerializeField]
    private GameObject post_P_Dark;

    [SerializeField]
    private CameraShake cameraShake;

    private bool isTrigger = false; // 카메라 쉐이크 코루틴 브레이크용(지금 안사용함)

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
               
                if (ChangeSceneManager.instance.stageNum == 1)
                {
                    BossSpawn(0); // 여기서 보스 소환 처리
                }
                else if (ChangeSceneManager.instance.stageNum == 2)
                {
                    BossSpawn(1); // 여기서 보스 소환 처리
                }

                bossSpawnActive = true;
            }
        }
    }

    public GameObject BossSpawn(int index)
    {
        BossScript newBoss01 = poolManager.GetFromPool<BossScript>(index);

        GameObject newBossObj_01 = newBoss01.gameObject;
        
        return newBossObj_01;
    }

    public void BossReturnPool(BossScript clone)
    {
        //Debug.Log("보스 매니저에서 보스 반환 함수");
        bossSpawnActive = false; // 보스 반환 작업시 초기화 작업
        curTime = 0f; // 보스 반환 작업시 초기화 작업
        
        BossScript.instance.isPurification = false; // 보스 반환 작업시 정화 초기화 작업
        BossScript.instance.isTimeToReturn = false; // 몬스터 연속 반환을 막기위한 초기화 작업(BossScript.instance.isTimeToReturn는 보스가 죽었을때 연출하는 동안을 뜻함)
        BossScript.instance.bossCurHP = BossScript.instance.bossMaxHP; // 보스 체력 맥스 hp로 초기화
        //UI_Script.instance.bossCurHP_Data = BossScript.instance.bossCurHP;
        post_P_Dark.gameObject.SetActive(false); // 보스 출현시 어두워지는 효과 끄기

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
