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
                    Debug.Log("보스패턴2 시작");
                    Pattern02();
                    patternIndex++;
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
        StartCoroutine(Pattern01_CoolTime());
    }       

    private void Pattern02() // 흑풍 패턴
    {
        StartCoroutine(Pattern02_CoolTime());
    }

    private void Pattern03() // 거대 흑풍 패턴
    {

    }

    //보스 패턴01 스폰하는 패턴
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

    private float coolTime01 = 15f; // 쿨타임

    private bool isCoolTime01 = false;

    IEnumerator Pattern01_CoolTime() // 쿨타임 작업 쿨타임 15초, 5초에 10마리 소환
    {
        while (!isCoolTime01)
        {
            
            for (int i = 0; i < spanwCount; i++) // 2마리 소환
            {
                SpawnPattern();
                yield return YieldInstuctionCash.WaitForSeconds(0.75f);                
            }

            yield return YieldInstuctionCash.WaitForSeconds(delayTime); //1초의 시간이 흐르면...
            patternCurrDuration++;            //delayTime(딜레이 타임에 따라 지속시간 표현)
            Debug.Log("지속 시간 :" + patternCurrDuration);

            if (patternCurrDuration == patternDuration) // 주의 : 5초의 지속시간이면 +1을 한 6을 기입하는 수식임
            {
                //Debug.Log("패턴 종료");
                isCoolTime01 = true;
                patternCurrDuration = 0;
                //todo : 쿨타임 적용
                break;
            }
        }
        //Debug.Log("코루틴 종료");
        

        if (isCoolTime01)
        {
            isCoolTime01 = false;

            yield return YieldInstuctionCash.WaitForSeconds(coolTime01);
        }

        StopCoroutine(Pattern01_CoolTime());

    }

    private float[] xLoad = new float[3]; // x축 차선을 활용할때

    [SerializeField]
    private Transform spawnerPos; // 스폰되는 좌표

    private float coolTime02 = 1f; // 쿨타임

    private bool isCoolTime02 = false;

    //보스패턴2 흑풍 패턴
    private GameObject TornadoSpawnPattern(int patternNum,int xPosIndex)
    {
        GameObject path = null; // 함수 빈호출용도

        if (patternNum == 0)
        {
            GameObject newGimmick_Obj01 = GimmickManager.instance.GimmickSpawn();

            xLoad[0] = spawnerPos.position.x - 2f;
            xLoad[1] = spawnerPos.position.x + 2f;

            newGimmick_Obj01.transform.position = new Vector3(xLoad[xPosIndex], spawnerPos.position.y, 55f);

            return newGimmick_Obj01;
        }
        else if (patternNum == 1)
        {
            GameObject newGimmick_Obj01 = GimmickManager.instance.GimmickSpawn();

            xLoad[0] = spawnerPos.position.x - 2f;
            xLoad[1] = spawnerPos.position.x + 2f;
            xLoad[2] = spawnerPos.position.x; // 가운데는 무조건 나와야함으로

            newGimmick_Obj01.transform.position = new Vector3(xLoad[xPosIndex], spawnerPos.position.y, 55f);

            return newGimmick_Obj01;
        }

        return path; // 함수 빈호출용도
    }    

    IEnumerator Pattern02_CoolTime()
    {
        while (!isCoolTime02)
        {
            int patternNum = Random.Range(0, 2); //0,1을 호출

            if (patternNum == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    TornadoSpawnPattern(patternNum, i);
                }
                isCoolTime02 = true;
            }
            else if (patternNum == 1)
            {
                //for문 하나로 처리하는 방법 기회되면 모색해보기
                int xPosIndex = Random.Range(0, 2);
                TornadoSpawnPattern(patternNum, xPosIndex); //왼쪽 or 오른쪽
                TornadoSpawnPattern(patternNum, 2); // 가운데는 무조건 나와야한다...
                isCoolTime02 = true;
            }
            
        }
        //임시 방지
        yield return YieldInstuctionCash.WaitForSeconds(5f); // 이걸로 토네이도가 미친듯이 오는 것을 방지

        if (isCoolTime02)
        {
            isCoolTime02 = false;

            yield return YieldInstuctionCash.WaitForSeconds(coolTime02); // 쿨타임                      
        }

        StopCoroutine(Pattern02_CoolTime());
    }

    //보스패턴3 거대 흑풍
    private void NuClearPattern()
    {

    }

}
