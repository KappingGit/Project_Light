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

    private int patternIndex = 1; // 보스패턴 인덱스

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

    private int patternCurrDuration = 0; // 패턴 지속된 현재 시간

    private int patternDuration = 5; // 패턴 지속 시간 (몇초까지 패턴이 발생되는지...)

    private int spanwCount = 2; // 몇초에 '몇번'소환되는지

    private float delayTime = 1f; // '몇초'에 몇번 소환되는지

    private float coolTime = 15f; // 쿨타임

    private bool isCoolTime = false;

    IEnumerator SpawnRate() // 쿨타임 15초, 5초에 10마리 소환
    {
        while (!isCoolTime)
        {
            
            for (int i = 0; i < spanwCount; i++) // 2마리 소환
            {
                SpawnPattern();
                yield return YieldInstuctionCash.WaitForSeconds(0.75f);                
            }

            yield return YieldInstuctionCash.WaitForSeconds(delayTime); //1초의 시간이 흐르면...
            patternCurrDuration++;            
            Debug.Log("지속 시간 :" + patternCurrDuration);

            if (patternCurrDuration == patternDuration) // 주의 : 5초의 지속시간이면 +1을 한 6을 기입하는 수식임
            {
                //Debug.Log("패턴 종료");
                isCoolTime = true;
                patternCurrDuration = 0;
                //todo : 쿨타임 적용
                break;
            }
        }
        //Debug.Log("코루틴 종료");
        

        if (isCoolTime)
        {
            isCoolTime = false;

            yield return YieldInstuctionCash.WaitForSeconds(coolTime);
        }

        StopCoroutine(SpawnRate());

    }

    //보스패턴2 흑풍 패턴
    private void TornadoPattern()
    {

    }



    //보스패턴3 거대 흑풍
    private void NuClearPattern()
    {

    }

}
