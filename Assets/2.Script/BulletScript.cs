using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class BulletScript : MonoBehaviour, IPoolObject, INomalAttack
{
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

    }
        

    private void Update()
    {
        BulletSpeed();

        
        //Debug.Log("장착된 무기 속성(인덱스 넘버) : " + PlayerShooting.intance.weaponType);
        //Debug.Log("자식 오브젝트 이름 : "+hitObj.gameObject.name);
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

    private void InitBullet() // 초기화 로직 함수
    {
        BulletSpawnPos();
        
        SlashEffect();
        //hitObj = GetComponentInChildren<GameObject>();


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
        bulletType = PlayerShooting.intance.weaponType;
        InitBullet(); // 재사용하기 위해 초기화 로직(총알 기본 상태값)을 작성
        //Debug.Log("총알을 초기화시켰습니다.");

    }
    
}
