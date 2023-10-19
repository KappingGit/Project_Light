using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class BossPattern_Wind : MonoBehaviour
{
    public static BossPattern_Wind instance;

    private PoolManager poolManager;

    private void Awake()
    {
        if (BossPattern_Wind.instance == null)
        {
            instance = this;
        }
    }

    private int patternIndex = 1;

    private void Update()
    {
        if (BossManager.instance.bossSpawnActive) //보스가 등장했다면...패턴 시작
        {
            //Debug.Log("보스 등장");
            switch (patternIndex)
            {
                case 1: // 패턴1
                    Debug.Log("보스 패턴 시작");
                    Pattern01();
                    patternIndex++;
                    break;
                case 2: // 패턴2
                    //Debug.Log("보스패턴2 시작");
                    break;
                case 3: // 패턴3
                    break;
                default:
                    break;
            }
        }
    }

    private void Pattern01() // 몹 소환
    {        
        StartCoroutine(SpawnRate());
    }       

    private void Pattern02() // 흑풍 패턴
    {

    }

    private void Pattern03() // 거대 흑풍 패턴
    {

    }

    // 스폰하는 패턴
    private GameObject SpawnPattern()
    {
        GameObject newEnemy01 = EnemyManager.instance.Spawn();

        //GameObject newEnemyObj01 = newEnemy01.gameObject;

        return newEnemy01; // BossPattern_Wind.cs - SpawnPattertn()의 지역함수
    }

    private int patternDuration = 0; // 패턴 지속 시간

    private int spanwCount = 2; // 몇초에 '몇번'소환되는지

    private float spanwTime = 1f; // '몇초'에 몇번 소환되는지

    private bool isCoolTime = true;

    IEnumerator SpawnRate()
    {
        while (isCoolTime)
        {
            //for (int i = 0; i < spanwCount; i++) 
            //{
            //    SpawnPattern();
            //    patternDuration++;
            //}

            SpawnPattern();

            patternDuration++;

            yield return YieldInstuctionCash.WaitForSeconds(spanwTime);

            if (patternDuration == 15)
            {
                //Debug.Log("패턴 종료");
                isCoolTime = false;
                //todo : 쿨타임 적용
                break;
            }
        }
        //Debug.Log("코루틴 종료");
        isCoolTime = true;
        StopCoroutine(SpawnRate());

    }
}
