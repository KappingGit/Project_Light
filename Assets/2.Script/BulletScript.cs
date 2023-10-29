using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

// 내가 해야하는 방향성을 고려해보자
#region 현재 전체적인 코드 방식

/*
 일단 전체적으로 어떻게 구현했는지 나열해보기
=> BulletScript.cs라는 스크립트를 모든 총알(날아가는 스킬)에 집어넣었으며
해당 스크립트에는 해당 오브젝트의 속도를 부여, 생성 위치가 지정되어있다.

즉, 해당 스크립트가 들어있는 오브젝트를 오브젝트 풀링 되어있는 오브젝트를 끌어오면
발사되는 매커니즘을 가지고 있다.

단... 이러한 문제점으로 총알 자체에는 해당 무기 타입별로 들어있는 코드가 없으므로
자식 코드를 통해 타입을 지정해야하는 상황이 벌어지며 발사 위치를 매 오브젝트의 매번 넣어
야한다

또한, 발사 방식을 PlayerShooting.cs를 통해서 오브젝트 풀의 인덱스 번호를
가져오는 방식

내 방식대로 한다

정리해보자
Attacktype의 indexUID 번호를 파악
0~5번까지는 windSlash로 바람 속서의 기본 평타 테이블로 나뉜다
6~11번까지 waterSlash로 물 속성의 기본 평타 테이블로 나뉜다
12번~17번까지 fireSlash로 불 속성의 기본 평타 테이블로 나뉜다.

-----------------------------------------------------

오브젝트 풀링으로 처리되어있는 오브젝트의 인덱스번호를 나열해보자

BulletManager에 있는 순서대로

0번은 바람 총알
1번은 물 총알
3번은 불총알

SubSkillManager.cs에 있는 순서대로

0번은 서브 스킬 윈드드릴
1번은 서브 스킬 워터 베리어
2번은 서브 스킬 파이어볼

EffectManager에 있는 순서대로

0번은 사망 이펙트 
1번 바람총알 히트 이펙트
2번 물총알 히트 이펙트
3번 불총알 히트 이펙트

4번 바람 속성 칼 이펙트
5번 물 속성 칼 이펙트
6번 불 속성 칼 이펙트

7번 서브 스킬 바람 속성 마법진(주문시전) 이펙트
8번 서브 스킬 물 속성 cast(주문시전) 이펙트
9번 서브 스킬 불 속성 마법진(주문시전) 이펙트

재밌는 짓거리를 해볼까

-----------------------------------------------------

현재 코딩되어있는 함수 방식으로

만약, 현재 무기가 " x번(몇번이라면)" => 해당 x번의 오브젝트 풀링(총알) x번의 오브젝트를 부른다
만약, x번의 오브젝트를 불렀다면 해당 히트 이펙트와 슬래쉬 이펙트를 불러오는데 
히트의 경우 " x + 1번"을 불러오고 슬래쉬의 경우 "x + 4"를 불러온다

만약, 해당 무기가 SubSkill이라면  "y번(몇번이라면)" => 해당 y번의 오브젝트 풀링(서브 스킬) y번의 오
브젝트를 부른다
만약, y번의 오브젝트를 불렀다면 해당 히트 펙트와 캐스트 이펙트를 불러오는데
히트의 경우 "y + 1"번을 불러오고 캐스트의 경우 " y + 7"을 불러온다.

히트 이펙트는 속성별 이펙트가 다 같기 때문에 비슷한 함수를 사용함

여기서 나뉘어보자 불릿의 속성을 가져가면서 각 종류별 스크립트를 만들어서 데미지 처리 방식으로 표현

서브 스킬의 경우 PlayerShooting.cs에서 버튼을 눌렀을 때 해당 들어있는 
변수 subSkillType의 인트 값을 바꾸는 형식으로 서브 스킬을 바꾼다

만약, 서브 스킬 버튼 1을 눌렀을 때 1번 안에 있는 변수에 해당하는 타입 인트값이 존재할 것이다 해당 

인트값을 바꾸는 것으로 제작

이제 해보자

 */

#endregion

// 무기 타입 열거형
public enum WeaponType
{
    windSlash,
    warterSlash,
    fireSlash,
    windDrill,
    
}

public class BulletScript : MonoBehaviour, IPoolObject, INomalAttack
{
    [SerializeField]
    protected DB_Status statusDB; // 플레이어, 기본공격 타입의 데이터를 불러오기(자식에게 접근하게 protected사용)


    // 새로운 방식의 코드(딕셔너리와 열거형)
    // 주의 함수 이름과 똑같은게 있음 (좀 더 좋은 이름이 있으면 선정)
    protected WeaponType currentWeaponType;
    
    

    //[SerializeField]
    //private int nomalAttackNum; // 해당 오브젝트의 넘버
    
    [SerializeField]
    public string idName; // 풀링작업에 사용될 오브젝트 닉네임

    public static BulletScript instance;

    private Rigidbody bulletRig;

    private int bulletType;

    [SerializeField]
    private float bulletSpeed = 40.0f; // 투사체 속도

    private void Awake()
    {
        bulletRig = GetComponent<Rigidbody>();

        if (BulletScript.instance == null)
        {
            //Debug.Log("BulletScript.instance가 Null상태입니다");
            instance = this;
        }
        //Debug.Log("총알 위치: x = " + transform.position.x + "    y = " + transform.position.y + "    z = " + transform.position.z);
        //SlashEffect(); // 공격이 나갔을 때...

        //자식 클래스에 있는 함수
        WindSlashTypeDamage();

        //해당 딕셔너리 변수를 썼을때 문제가 없음
        //windSlashType.Add(statusDB.NomalAttack[1].nomalAttackUID, statusDB.NomalAttack[1].singleDamage);

        //Debug.Log("해당 기본공격은 바람 기본 평타 1레벨입니다. " + windSlashType[0]);


    }

   
    private void Update()
    {
        BulletSpeed();


        //Debug.Log("장착된 무기 속성(인덱스 넘버) : " + PlayerShooting.intance.weaponType);
        //Debug.Log("자식 오브젝트 이름 : "+hitObj.gameObject.name);

       // 열거형 테스트
        switch (currentWeaponType)
        {
            // 만약 WeaponType의 형태가 바람 기본 공격이라면...
            case WeaponType.windSlash:
                
                break;

            // 만약 WeaponType의 형태가 물 기본 공격이라면...
            case WeaponType.warterSlash:

                break;

            // 만약 WeaponType의 형태가 불 기본 공격이라면...
            case WeaponType.fireSlash:

                break;
        }

    }

    private void InitBullet() // 초기화 로직 함수
    {
        BulletSpawnPos();
        //CurrentWindSlashType();
        SlashEffect();
        //hitObj = GetComponentInChildren<GameObject>();


    }
       
    //밑에 있는 함수는 WindSlahScript.cs의 자식 함수로 사용하려고 했던 잔재함수
    public virtual float WindSlashTypeDamage()
    {
        
        // 임시 반환용도
        float path = 0f;
        Debug.Log("해당 디버그는 부모 함수 디버그이다.");
        return path;

    }

    //------------------------------------------------------------------------------------------------------------------------
    //###############################↓↓↓↓↓↓인터페이스 함수 영역↓↓↓↓↓↓###############################--------------
    //------------------------------------------------------------------------------------------------------------------------

    //계산 수치는 case문을 활용해서 데이터테이블 인덱스 값을 불러오는 형식으로 나중에 변경하기
    // 히트 이펙트
    public GameObject HitEffect() //풀 가져오는 수식을 조금 쉽고 간단하며 여러 상황에 쓸수 있게 수정하기
    {
        
        GameObject hitEffect_01 = EffectManager.instance.EffectPool(bulletType + 1); // 1인덱스는 바람 공격 히트 이펙트

        hitEffect_01.transform.position = gameObject.transform.position;

        return hitEffect_01;
    }
        
    // 슬래쉬(검 휘두르는) 이펙트 인덱스 확인 잘할 것....
    public GameObject SlashEffect()
    {
        
        GameObject slashEffect_01 = EffectManager.instance.EffectPool(bulletType + 4); ; // 4인덱스는 바람 검 휘두르는 이펙트

        slashEffect_01.transform.position =new Vector3(shootPos.position.x, shootPos.position.y + 0.5f, shootPos.position.z + 0.5f);

        return slashEffect_01;
    }

    //------------------------------------------------------------------------------------------------------------------------
    //###############################↑↑↑↑↑↑인터페이스 함수 영역↑↑↑↑↑↑↑###############################------------
    //------------------------------------------------------------------------------------------------------------------------
      
    
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Tile")) // 어느 곳에서 충돌하면 총알 사라짐
        //{
        //    OnTargetReached();
        //    Debug.Log("한계점 벽에 닿았습니다.");
        //}

        if (other.gameObject.CompareTag("Enemy")) // 어느 곳에서 충돌하면 총알 사라짐
        {

            OnTargetReached();// 적과 부딪히면 총알 반환

            HitEffect();

            //Debug.Log("타격 이펙트");

            //Debug.Log("몬스터 또는 벽에 충돌했습니다");
        }

    }

    [SerializeField]
    private Transform shootPos; // 총알이 발사될 위치 문제점 해당 transform이 윈드 슬래쉬에 겹쳐지는 문제점이 발생

    private void BulletSpawnPos()
    {
        transform.position = new Vector3(shootPos.position.x, 1f, shootPos.position.z); // 총알이 생성되었을때 위치
    }

    private void BulletSpeed()// 총알의 투사체 힘
    {
        bulletRig.velocity = new Vector3(0, 0, bulletSpeed);
    }

    

    private void OnTargetReached() // 반환 작업용 함수
    {
        BulletManager.instance.ReturnBullet(this);
        //SkillManager.instance.ReturnSkill(this);
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
        // 해당 불릿의 타입은 무기 타입의 인트 값(index)를 가져온다.
        bulletType = PlayerShooting.intance.weaponType;
        InitBullet(); // 재사용하기 위해 초기화 로직(총알 기본 상태값)을 작성
        //Debug.Log("총알을 초기화시켰습니다.");

    }
    
}
