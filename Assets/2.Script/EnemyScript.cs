using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;


public class EnemyScript : MonoBehaviour, IPoolObject, IDie, IDamage
{
    [SerializeField]
    public string idName; // 풀링작업에 사용될 오브젝트 닉네임   

    public static EnemyScript instance;
                
    //NavMeshAgent ai;

    //private GameObject enemyObj;

    private Transform enemyTrans;

    private Rigidbody enemyRig;

    private void Awake()
    {
        //enemyObj = GetComponent<GameObject>();

        enemyRig = GetComponent<Rigidbody>();

        enemyTrans = GetComponent<Transform>();

        //ai = GetComponent<NavMeshAgent>(); // Ai에 접근

        // 해당 스크립트 인스턴스
        if (EnemyScript.instance == null)
        {
            instance = this;
        }

        // 해당 몬스터 오브젝트 바라보는 방향 조정
        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
               
    }

    private Vector3 nowPos; // 실시간 좌표 추출

    private bool allReturnDone = false; // 모두 반환이 이루어졌다면
        
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

        // 보스 등장시 모두 반환
        if (BossManager.instance.bossSpawnActive)
        {
            if (!allReturnDone)
            {
                OnTargetReached(); //반환 함수
                allReturnDone = true;
            }

            if (BossScript.instance.isTimeToReturn)
            {
                OnTargetReached();
            }
        }

        // 해당 트리거 만들 => EnemyScript.cs => isTrigger bool is true
        // 해당 Null상태라서 오류가 뜸
                
        enemyTrans.transform.position = transform.position;

    }    

    
    
    public void Init() // 생성되는 기본 정보
    {

        // todo: Gamemanger 싱글톤 작업 아직 미진행

        SpawnPos(); // 소환되는 position값

        EnemyHP();       

        isDie = false;

        //Debug.Log("기본값");

        //Transform[] spawnPos = GameManger.instance.points; //  스폰 포인트를 지정

        //ai.SetDestination(spawnPos[Random.Range(0, spawnPos.Length)].position); //해당 스폰 포인트로 이동

        
    }

    // 스테이터스 작업에서 상속 개념을 활용해보기

    [SerializeField]
    private DB_Status statusDB; // 에셋화 되어있는 데이터테이블 가져오기

    private float maxHp; // 몬스터의 최대 체력

    private float currHp; // 몬스터의 현재 체력

    // 몬스터 HP (난이도 테이블 사용)
    private void EnemyHP() 
    {
        // 현재 체력에 저장
        maxHp = statusDB.MonsterStatus[0].monsterHP;
        currHp = maxHp; // 초기화  
    }

    // 몬스터 이동 속도
    //[SerializeField]
    //private float enemySpeed = 25.0f; // 적 오브젝트 속도, 몬스터 속도

    private void EnemySpeed()
    {
        float enemySpeed = statusDB.MonsterStatus[0].monsterSpeed;

        enemyRig.velocity = new Vector3(0, 0, -enemySpeed); // 나중에 속도 느려지게 하는 효과를 넣으면 수식 변경
    }

    //몬스터 획득 경험치 (난이도 테이블 사용)
    private void EnemyEXP()
    {

    }

    //몬스터 획득 골드 (난이도 테이블 사용)
    private void EnemyGold()
    {

    }

    // 어떤 무기 닿았는지 알기 위한 함수
    private float WhatWeaponType()
    {


        if (true) // 만약 바람 속성 무기라면...
        {
            float damage = BulletScript.instance.WindSlashTypeDamage(1); // 해당 무기의 데미지

            return damage;

        }
        //else if (true) // 만약 바람 서브 스킬이라면...
        //{

        //}
    }

    private void Hit() // 최종 데미지 피격 함수
    {
        if (currHp > 0)
        {
            //몬스터의 피격 시스템 처리를 여기서 하지말 것, 만약 해당 몬스터가 weapon이라는 태그의 오브젝트를 맞았다면
            // 해당 오브젝트의 공격력, 공격 타입을 받아와서 currHp에서 기입(계산하는 처리방법으로하기
            // 현재 방식은 몬스터가 부딪혔을 때 수치만큼 데미지를 받는 형태임
            //currHp -= 10; // 수치부분에 플레이어 공격 관련 수치를 넣으면 해결

            // 만약, 해당 오브젝트의 UID nomalAttackUID가 1이라면 바람 기본 공격 1레벨짜리 공격을 받은 것
            // 해당 nomalAttackUID의 공격 값을 currHp로 처리
            // 원래 처음 생각으로는 if(해당 UID라면){해당하는 레벨의 함수를 불러온다}
            // 하지만 위 방식은 너무 비효율적임 Dictionary를 활용

            Debug.Log("몬스터가 피격 받기전 체력입니다" + currHp);

            //TargetDamage();

            currHp -= WhatWeaponType();

            Debug.Log("몬스터가 피격을 받았습니다." + currHp);


        }

        if (currHp <= 0) // 현재 체력이 떨어지면...
        {
            Die();

            PlayerStatus.instance.GetGold(); // 골드 획득(플레이어가 죽이면 골드 얻게 함)
            PlayerStatus.instance.GetEXP(); // 경험치 획득(플레이어가 죽이면 경험치 얻게 함)
        }

    }

    #region 잘못된 코드 언젠가 다시보고 왜 이렇게 했지? 라는 생각을 가질 수 있게 내비둔다

    //private float HitCalculate(int indexType) //피격했을 때 속성 종류별 피격 계산 함수
    //{
    //    // 바람 기본 평타: 바람 기본 평타 최종 데미지 = 플레이어 공격력 * 바람 추가 피해 퍼센트

    //    //테이블 데이터 다른 형태로 변경

    //    // 임시 테스트 현재 무기 타입 : 바람 기본 공격

    //    currentWeaponType = WeaponType.windSlash;

    //    switch (currentWeaponType) // 현재 무기타입이...
    //    {
    //        case WeaponType.windSlash: // 인덱스 0 = 
    //            Debug.Log("바람 기본 평타입니다.");
    //            break;
    //        case WeaponType.waterSlash:
    //            Debug.Log("물 기본 평타입니다.");
    //            break;
    //        case WeaponType.fireSlash:
    //            Debug.Log("불 기본 평타입니다.");
    //            break;
    //    }

    //    if (statusDB.AttackType[indexType].weaponType == 0) // 만약 해당 무기타입이 '0'(바람 속성이라면(어떤 속성이라면...))
    //    {
    //        // 무기의 타입 레벨, 인덱스 0은 스킬, 평타를 얻지 않은 상태를 뜻함
    //        if (statusDB.AttackType[1].typeLevel == 1)// 해당 무기의 레벨, 만약 무기 타입의 레벨이 '1'이라면
    //        {
    //            float weaponAbility = statusDB.AttackType[1].typeAbility; // 해당 능력치를 가져온다

    //            float tureDamage = statusDB.PlayerStatus[0].playerDamage * weaponAbility; // 플레이어의 공격력 기반

    //            return tureDamage; // 계산된 스킬 대미지
    //        }
    //    }


    //    // 고려해야할 것... => 해당 무기가 어떤 무기인가?, 그 해당 무기의 레벨은 어떻게 되는가? => 결과 해당 속성 무기의 레벨의 공격력 수치

    //    float path = 0; //만약 아무것도 없다면...

    //    return path;

    //}
    #endregion




    private void Attack()
    {
        // todo : 몬스터 공격관련
    }

    //------------------------------------------------------------------------------------------------------------------------
    //###############################↓↓↓↓↓↓인터페이스 함수 영역↓↓↓↓↓↓###############################--------------
    //------------------------------------------------------------------------------------------------------------------------


    public void TargetDamage(float damage)
    {
        currHp -= damage;
        Debug.Log("몬스터가 피격을 받았습니다." + damage);
    }

    [HideInInspector]
    public bool isDie = false;

    // IDie의 인턴페이스 참조문
    public void Die()// 몬스터 죽는 함수
    {
        isDie = true;

        OnTargetReached();  // 몬스터 반환

        DieEffect();

    }

    // 죽는 이펙트 인터페이스 함수
    public GameObject DieEffect()
    {
        //사용법: 에너미 스크립트에 죽는 이펙트를 자식으로 불러온다???, 죽는 이펙트에 에너미 스크립트를 자식으로 불러온다???

        GameObject newDieEffect_01 = EffectManager.instance.EffectPool(0); // 이펙트 매니저에서 게임오브젝트화를 거쳐서 GameObject로 변경이 가능함

        newDieEffect_01.transform.position = gameObject.transform.position; // 적오브젝트 위치에 생성

        //Debug.Log("죽는 이펙트 나올때 좌표 : " + newDieEffect01.transform.position);

        return newDieEffect_01; // EnemyScript- DieEffect()함수의 지역변수
    }

    // 드랍 아이템 인터페이스 - 인터페이스 스크립트에서 한번 확인할 것(아직 비활성화)
    public void DropItem()
    {
        // todo: 몬스터 드랍 아이템 관련 GameObject DropItem();
    }

    //------------------------------------------------------------------------------------------------------------------------
    //###############################↑↑↑↑↑↑인터페이스 함수 영역↑↑↑↑↑↑↑###############################------------
    //------------------------------------------------------------------------------------------------------------------------

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tile"))
        {
            //Debug.Log("몬스터를 반환시도.");
            //todo : 몬스터서 플레이어랑 부딪히면...  => 몬스터가 사라짐 or 몬스터가 잠시 무적상태로 비활성화
            Die(); //관련 타겟에 부딪히면 다시 반환시켜준다...
        }

        if (other.gameObject.CompareTag("Weapon"))
        {
            //Debug.Log("몬스터를 반환시도.");

            Hit();
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

    IEnumerator WaitForTime()
    {
        yield return YieldInstuctionCash.WaitForSeconds(2f);
        Die();
        StopCoroutine(WaitForTime());
    }

}