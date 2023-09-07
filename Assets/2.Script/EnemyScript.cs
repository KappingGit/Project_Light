using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class EnemyScript : MonoBehaviour, IPoolObject
{
    [SerializeField]
    public string idName; // 풀링작업에 사용될 오브젝트 닉네임

    //[SerializeField]
    //private Animator anim;

    //[SerializeField]
    //private Vector3 targetPos;

    //private bool isAtDestination;

    public static EnemyScript instance;

    //NavMeshAgent ai;

    private void Awake()
    {
        //ai = GetComponent<NavMeshAgent>(); // Ai에 접근
        // 해당 스크립트 인스턴스
        if (EnemyScript.instance = null)
        {
            instance = this;
        }

    }

    private void Update()
    {

        #region 애니메이션 관련
        //Vector3 reVelocity = transform.InverseTransformDirection(ai.velocity); // 각각의 AI객체에 anim 추가
        //reVelocity = 0;
        //anim.SetFloat("NormalizedSpeed", reVelocity.magnitude / anim.transform.lossyScale.x); // 애니메이션 추가
        //                                                                                      //애니메이션 쪽
        //if (ai.remainingDistance < 2f)
        //{
        //    if (!isAtDestination)
        //        OnTargetReached();

        //    isAtDestination = true;
        //}
        //else
        //{
        //    isAtDestination = false;
        //}
        #endregion

    }

    //private void OnEnable() // 활성화 될때마다 호출 (논외: Ondisable() => 비활성화 될때마다 호출)
    //{
    //    Init();
    //}

    [SerializeField]
    private float maxHp; // 몬스터의 최대체력

    private float currHp; // 몬스터의 현재 체력
    
    public void Init() // 생성되는 기본 정보
    {

        // todo: Gamemanger 싱글톤 작업 아직 미진행

        currHp = maxHp; // 현재 체력에 저장

        //Debug.Log("기본값");

        
        //Transform[] spawnPos = GameManger.instance.points; //  스폰 포인트를 지정

        //ai.SetDestination(spawnPos[Random.Range(0, spawnPos.Length)].position); //해당 스폰 포인트로 이동
    }

    private void Hit()
    {
        if (0 < currHp) // 현재 체력이 떨어지면...
        {
            //todo : 몬스터 사망 처리
        }

    }

    private void Attack()
    {
        // todo : 몬스터 공격관련
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag ("Tile"))
        {
            Debug.Log("몬스터를 반환했습니다.");
            //todo : 몬스터서 플레이어랑 부딪히면...  => 몬스터가 사라짐 or 몬스터가 잠시 무적상태로 비활성화
            OnTargetReached(); //관련 타겟에 부딪히면 다시 반환시켜준다...
        }
    }

    private void OnTargetReached()
    {
        EnemyManager.instance.ReturnPool(this); // 해당 오브젝트를 다시 반환 시켜준다
        //Debug.Log("다시 반환");
    }

    // 인터페이스 IPoolObject을 명시적으로 구현
    public void OnCreatedInPool()
    {
       // 해당 오브젝트가 처음 생성됐을때 실행 함수
    }

    // 인터페이스 IPoolObject을 명시적으로 구현
    public void OnGettingFromPool() //풀에서 관련된 풀 오브젝트를 가져올때...
    {
        // 해당 오브젝트가 가져올때마다 실행
        Init(); // 재사용하기 위해 초기화 로직(몬스터 기본 상태값)을 작성
        Debug.Log("OnGettingFromPool상태");
    }

}