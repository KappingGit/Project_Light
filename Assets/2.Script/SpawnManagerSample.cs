using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class SpawnManagerSample : MonoBehaviour
{
    [SerializeField]
    private int maxCount; // 몬스터 최대 생성수
    private int curCount; // 몬스터 현재 생성수
    private PoolManager poolManager; // 풀링 

    [SerializeField]
    private int spawnType; // 스폰 타입

    private bool bossSpawn; // 보스가 생성되었다면

    private void Awake()
    {
        poolManager = GetComponent<PoolManager>();
        curCount = 0;
        spawnType = 1;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (bossSpawn) // 만약 플레이어가 들어간다면
        {
            // 생존 몬스터수 < 필드 최대 몬스터 수보다 작다면... 보충하는 코루틴을 실행
            StartCoroutine("TrySpawn");
        }


        while (curCount < maxCount) // 일시적으로 가득 스폰시키는 것(현재 생성수가 최대치보다 작다면 생성)
        {
            SpawnUnit();
        }
    }

    private void OnTriggerExit(Collider other) // 주인공이 나간다면...
    {
        if (!bossSpawn)
        {
            StopCoroutine("TrySpawn");
        }
    }

    IEnumerator TrySpawn() // 스폰을 시킨다.
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); //1초 간격으로
            if (curCount < maxCount)
            {
                SpawnUnit();
            }
        }
    }

    private Vector3 pos; // 위치값
    private float xMax;
    private float xMin;

    private void SpawnUnit()
    {
        curCount++; // 스폰을 하게 되면 현재 생성되었으니 카운터 증가
        // 존하나당 몬스터 한마리
        //MonsterBase monster = poolManager.GetFromPool<MonsterBase>(0); // 몬스터 베이스 스크립트에 풀매니저를 부른다.
        //pos = transform.position;
        //pos.x += Random.Range(-3f, 3f); // x축으로 랜덤하게 생성
        //pos.y = 0f;
        //pos.z += Random.Range(-3f, 3f); // z축으로 랜덤하게 생성

        int tableID = 100000 + spawnType + 1000 * 1; //todo: 난이도 적용. GameManager.Inst.st // 해당 테이블 id에 접근
        //monster.InitMonster(tableID); // 해당 몬스터 아이디를 불러온다


        #region 스폰 제작

        // 스폰 영역 제한
        xMax = transform.position.x + 5f; // 적이 나타날 구간 최대치(좌우)
        xMin = transform.position.x - 5f; // 적이 나타날 구간 최소치(좌우)

        //spawnTrans.x = Mathf.Clamp(spawn_X, xMax, xMin); // 해당 Mathf의 함수는  xMax와 xMin의 사이에서 Value값(spawn_X)을 반환받는 것을 사용

        float rand = Random.Range(xMax, xMin);

        //Instantiate(enemyObject[0], new Vector3(rand, 0.75f, transform.position.z), Quaternion.Euler(new Vector3(0f, 180f, 0f)));

        #endregion


    }


    // 외부에서 호출할 것
    //public void ReturnPool(MonsterBase monster) // 몬스터 스크립트의 몬스터 오브젝트
    //{
    //    poolManager.TakeToPool<MonsterBase>(monster.POOLNAME, monster);
    //    curCount--; // 한마리가 줄어들었으니까...
    //}
}