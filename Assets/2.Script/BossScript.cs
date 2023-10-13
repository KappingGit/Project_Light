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
    public float bossMaxHP; // 보스 최대체력

    [HideInInspector]
    public float bossCurHP; // 보스 현재체력

    private void BossInit()
    {
        BossSpawnPos();

        //todo: 보스 기본 정보 초기화 넣을 것

        bossCurHP = bossMaxHP; // 체력 초기화


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            bossCurHP -= 1f; //todo : 보스 공격의 데미지 스크립트를 따로 제작하기
            Debug.Log("보스 현재 체력 : " + bossCurHP);
            if (bossCurHP < 0f)
            {
                //todo: 사망 처리
                OnTargetReached();
                //BossManager.instance.bossSpawnActive = false;

                Debug.Log("보스 죽음" + BossManager.instance.bossSpawnActive);
            }

        }
    }

    private void OnTargetReached() // 반환 작업용 함수
    {
        BossManager.instance.BossReturnPool(this);
    }

    [SerializeField]
    private Transform spawnerPos; // 스폰되는 좌표

    private void BossSpawnPos()
    {
        transform.position = new Vector3(spawnerPos.position.x, 1f, 10f);

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
