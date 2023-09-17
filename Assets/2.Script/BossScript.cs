using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class BossScript : MonoBehaviour, IPoolObject
{

    [SerializeField]
    public string idName; // 풀링작업에 사용될 오브젝트 닉네임

    public static BossScript instance;

    //private PoolManager poolManager;

    private void Awake()
    {
        //해당 스크립트 인스턴스
        if (BossScript.instance == null)
        {
            instance = this;
        }

        // 해당 몬스터 오브젝트 바라보는 방향 조정
        transform.rotation = Quaternion.Euler(0f, 180f, 0f);

    }

    private void Update()
    {
        
    }

    [SerializeField]
    private Transform spawnerPos; // 스폰되는 좌표

    private void BossSpawn()
    {
        transform.position = new Vector3(spawnerPos.position.x, 1f, 10f);

    }

    private void BossInit()
    {
        BossSpawn();

        //todo: 보스 기본 정보 초기화 넣을 것

        // hp
    }

    // 인터페이스 IPoolObject을 명시적으로 구현

    // 해당 오브젝트가 처음 생성됐을때 실행 함수
    public void OnCreatedInPool()
    {
        BossInit();
    }

    // 해당 오브젝트가 가져올때마다 실행
    public void OnGettingFromPool()
    {
        BossInit();
    }
}
