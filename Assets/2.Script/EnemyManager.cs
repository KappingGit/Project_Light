using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class EnemyManager : MonoBehaviour
{
   
    private float firstSpawn = 1f; // 첫 생성 시간

    private float spawnCycle = 1f; // 생성 주기(생성 주기부분을 캐릭터 스피드와 연결시켜서 게임 스피드가 빨라지면 스폰도 빨라지게 구현)

    public static EnemyManager instance; // 싱글톤화

    private PoolManager poolManager; //풀매니저 스크립트에 접근
    

    private void Awake()
    {

        poolManager = GetComponent<PoolManager>();

        if (EnemyManager.instance = null)
        {
            Debug.Log("EnemySpawn.instance가 null상태입니다.");
            instance = this;
        }

        InvokeRepeating("Spawn", firstSpawn, spawnCycle); // Invokerepeating은 해당 함수를 firstSpawn초후에 spawnCycle초 간격으로 실행하는 용도

    }

    private void Update()
    {
        //Debug.Log("스포너 위치값 : " + spawnerPos.position.x + "   " + spawnerPos.position.y + "   " + spawnerPos.position.z);
    }

    private float xMax;
    private float xMin;

    private float[]xLoad = new float[3]; // x축 차선을 활용할때

    [SerializeField]
    private Transform spawnerPos; // 스폰되는 좌표

    // 기획적인 부분 : 만약 도로와 같이 1차선 2차선 3차선으로 할 경우 랜덤함수를 쓰는 것이 아닌 배열로 값으로 해당 포지션 값을 반환하는 것
    public void Spawn() // 스폰되는 영역
    {
        #region 스폰 방식 1 : x축 제한 범위 안에서 랜덤하게 적을 스폰 (이방식을 채택)

        // 스폰 영역 제한
        xMax = spawnerPos.position.x + 2f; // 적이 나타날 구간 최대치(좌우)
        xMin = spawnerPos.position.x - 2f; // 적이 나타날 구간 최소치(좌우)

        float rand = Random.Range(xMin, xMax);

        //int randIndex = Random.Range(0, 9); // 풀링 매니저의 오브젝트 해당 인덱스 풀 (예시 => 0번 몬스터 : 일반몬스터, 1번 몬스터 : 보스 몬스터)
        EnemyScript newEnemy = poolManager.GetFromPool<EnemyScript>(0); // 동일한 타입의 풀이 많다면 인자값으로 선택하는 것이 가능(풀 매니저의 인덱스를 가져오는 것 중요!!!)

        transform.position = new Vector3(rand, spawnerPos.position.y, spawnerPos.position.z);

        Debug.Log("스폰 실행");

        #endregion

        #region 스폰 방식 2 : 차선 도로와 같은 방식의 스폰

        //xLoad[0] = transform.position.x - 3f;
        //xLoad[1] = transform.position.x;
        //xLoad[2] = transform.position.x + 3f;

        //int randInt = Random.Range(0,3);

        //Instantiate(enemyObject[0], new Vector3(xLoad[randInt], transform.position.y, transform.position.z), transform.rotation);

        #endregion

    }

    public void ReturnPool(EnemyScript clone) 
    {
        //poolManager.TakeToPool<EnemyScript>(clone);
        poolManager.TakeToPool<EnemyScript>(clone.idName, clone); //TakeToPool : 지정된 풀에 반환 (idName : EnemyScript에서 idName으로 리턴 풀링시킬 오브젝트 이름 지정)
    }

    
}
