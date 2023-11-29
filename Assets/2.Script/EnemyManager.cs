using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class EnemyManager : MonoBehaviour
{
   
    public static EnemyManager instance; // 싱글톤화

    private PoolManager poolManager; //풀매니저 스크립트에 접근

    //[SerializeField]
    //private float firstSpawn = 1f; // 첫 생성 시간

    //[SerializeField]
    //private float spawnCycle = 0.5f; // 생성 주기(생성 주기부분을 캐릭터 스피드와 연결시켜서 게임 스피드가 빨라지면 스폰도 빨라지게 구현)

    private int changeMonsterNum;

    private void Awake()
    {

        poolManager = GetComponent<PoolManager>();

        if (EnemyManager.instance == null)
        {
            //Debug.Log("EnemyManager.instance가 null상태입니다.");
            instance = this;
        }

        //if (BossManager.instance.BossTimer().) // 마저 제작하기 todo: 보스 등장시 스폰을 멈추고 지금까지 나온 모든 몬스터 반환시키기
        //{

        //}

        //InvokeRepeating("Spawn", firstSpawn, spawnCycle); // Invokerepeating은 해당 함수를 firstSpawn초후에 spawnCycle초 간격으로 실행하는 용도

        changeMonsterNum = 0;

        StartCoroutine(SpanwCycle());

        //SpawnReapeating();

    }

    private void Update()
    {
        //Debug.Log("스포너 위치값 : " + spawnerPos.position.x + "   " + spawnerPos.position.y + "   " + spawnerPos.position.z);

        // 보스 출현시 일반 몬스터 생성 중단
        if (BossManager.instance.bossSpawnActive)
        {
            //CancelInvoke("Spawn");

            StopCoroutine(SpanwCycle());
        }

        if (ChangeSceneManager.instance.stageNum == 2)
        {
            changeMonsterNum = 1;
        }
        else if (ChangeSceneManager.instance.stageNum == 3)
        {
            // changeMonsterNum = 2; //3스테이지
        }

    }

    // 기획적인 부분 : 만약 도로와 같이 1차선 2차선 3차선으로 할 경우 랜덤함수를 쓰는 것이 아닌 배열로 값으로 해당 포지션 값을 반환하는 것

    //GetFromPool은 가져온다는 행위
    //public void GetPool() 
    //{
    //    EnemyScript newEnemy = poolManager.GetFromPool<EnemyScript>(0); // 동일한 타입의 풀이 많다면 인자값으로 선택하는 것이 가능(풀 매니저의 인덱스를 가져오는 것 중요!!!)
    //}

    // 의문 스폰 함수를 각 인덱스 값다른 오브젝트에 붙여지나 <= 이게 안되면 몬스터 스크립트에 스폰을 집어넣어야함 

    public GameObject Spawn()
    {
        //Debug.Log("몬스터 스폰");
        EnemyScript newEnemy01 = poolManager.GetFromPool<EnemyScript>(changeMonsterNum);

        GameObject newEnemyObj01 = newEnemy01.gameObject;

        return newEnemyObj01;
    }

    public void ReturnPool(EnemyScript clone) // TakeToPool은 다시 돌려준다는 행위
    {
        //poolManager.TakeToPool<EnemyScript>(clone);
        poolManager.TakeToPool<EnemyScript>(clone.idName, clone); //TakeToPool : 지정된 풀에 반환 (idName : EnemyScript에서 idName으로 리턴 풀링시킬 오브젝트 이름 지정)
    }

    IEnumerator SpanwCycle()
    {
        bool isSpawn = true;

        yield return YieldInstuctionCash.WaitForSeconds(1f);

        while (isSpawn)
        {
            if (BossManager.instance.bossSpawnActive)
            {
                isSpawn = false;

                StopCoroutine(SpanwCycle());

                break;
            }

            int rand = Random.Range(1, 4); //0~1의 랜덤 수를 소환

            //Debug.Log("소환했습니다.");

            for (int i = 0; i < rand; i++)
            {
                Spawn();
                yield return YieldInstuctionCash.WaitForSeconds(0.1f);
            }

            yield return YieldInstuctionCash.WaitForSeconds(1f); //생성주기
                        
        }
        
    }

}
