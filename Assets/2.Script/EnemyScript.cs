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

    private PoolManager poolManager; //풀매니저 스크립트에 접근

    //NavMeshAgent ai;

    private Rigidbody enemyRig;

    private void Awake()
    {
        enemyRig = GetComponent<Rigidbody>();

        //ai = GetComponent<NavMeshAgent>(); // Ai에 접근

        // 해당 스크립트 인스턴스
        if (EnemyScript.instance == null)
        {
            instance = this;
        }

        // 해당 몬스터 오브젝트 바라보는 방향 조정
        transform.rotation = Quaternion.Euler(0f, 180f, 0f);

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

        EnemySpeed();

    }

    [SerializeField]
    private float maxHp; // 몬스터의 최대체력

    private float currHp; // 몬스터의 현재 체력
    
    public void Init() // 생성되는 기본 정보
    {

        // todo: Gamemanger 싱글톤 작업 아직 미진행

        SpawnPos(); // 소환되는 position값

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
        if (other.gameObject.CompareTag("Tile"))
        {
            //Debug.Log("몬스터를 반환시도.");
            //todo : 몬스터서 플레이어랑 부딪히면...  => 몬스터가 사라짐 or 몬스터가 잠시 무적상태로 비활성화
            OnTargetReached(); //관련 타겟에 부딪히면 다시 반환시켜준다...
        }
    }

    private float xMax;
    private float xMin;

    private float[] xLoad = new float[3]; // x축 차선을 활용할때

    [SerializeField]
    private Transform spawnerPos; // 스폰되는 좌표

    public void SpawnPos() // 스폰되는 영역
    {
        #region 스폰 방식 1 : x축 제한 범위 안에서 랜덤하게 적을 스폰 (이방식을 채택)

        // 스폰 영역 제한
        xMax = spawnerPos.position.x + 2f; // 적이 나타날 구간 최대치(좌우)
        xMin = spawnerPos.position.x - 2f; // 적이 나타날 구간 최소치(좌우)

        float rand = Random.Range(xMin, xMax);

        //int randIndex = Random.Range(0, 9); // 풀링 매니저의 오브젝트 해당 인덱스 풀 (예시 => 0번 몬스터 : 일반몬스터, 1번 몬스터 : 보스 몬스터)

        transform.position = new Vector3(rand, spawnerPos.position.y, spawnerPos.position.z);

        //Debug.Log("스폰 실행");

        #endregion

        #region 스폰 방식 2 : 차선 도로와 같은 방식의 스폰

        //xLoad[0] = transform.position.x - 3f;
        //xLoad[1] = transform.position.x;
        //xLoad[2] = transform.position.x + 3f;

        //int randInt = Random.Range(0,3);

        //Instantiate(enemyObject[0], new Vector3(xLoad[randInt], transform.position.y, transform.position.z), transform.rotation);

        #endregion

    }

    private float enemySpeed = 25.0f; // 적 오브젝트 속도, 몬스터 속도

    private void EnemySpeed()
    {
        enemyRig.velocity = new Vector3(0, 0, -enemySpeed);
    }

    private void OnTargetReached() // 반환 작업용 함수
    {
        EnemyManager.instance.ReturnPool(this); // 해당 오브젝트를 다시 반환 시켜준다
        //Debug.Log(" 몬스터 반환되었습니다.");
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
        //Debug.Log("몬스터 초기화");
    }

}