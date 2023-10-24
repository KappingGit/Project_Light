using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class SubSkillScript : MonoBehaviour, IPoolObject, ISubSkiil // 기본 공격의 스크립트를 상속 받는다...
{
    [SerializeField]
    public string idName; // 풀링작업에 사용될 오브젝트 닉네임

    public static SubSkillScript instance;

    private Rigidbody skillRig;

    private int skillTypeBtn;

    // 부모 클래스 스크립트인 BulletScript에서 가져온다
    private void Awake()
    {
        if (SubSkillScript.instance == null)
        {
            instance = null;
        }

        skillRig = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        SkillSpeed();
    }

    [SerializeField]
    private Transform shootPos; // 총알이 발사될 위치 문제점 해당 transform이 윈드 슬래쉬에 겹쳐지는 문제점이 발생

    private void SkillSpawnPos()
    {
        transform.position = new Vector3(shootPos.position.x, 1f, shootPos.position.z); // 총알이 생성되었을때 위치
    }

    [SerializeField]
    private float skillSpeed = 40.0f; // 투사체 속도

    private void SkillSpeed()// 총알의 투사체 힘
    {
        skillRig.velocity = new Vector3(0, 0, skillSpeed);
    }

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

            HitEffect(skillTypeBtn); //해당 스킬 타입에 맞는 히트 이펙트를 소환

            //Debug.Log("타격 이펙트");

            //Debug.Log("몬스터 또는 벽에 충돌했습니다");
        }

    }

    private void OnTargetReached() // 반환 작업용 함수
    {
        SubSkillManager.instance.ReturnSkill(this);
        
    }

    //------------------------------------------------------------------------------------------------------------------------
    //###############################↓↓↓↓↓↓인터페이스 함수 영역↓↓↓↓↓↓###############################--------------
    //------------------------------------------------------------------------------------------------------------------------

    // 히트 이펙트
    public GameObject HitEffect(int skillType)
    {
        GameObject hitEffect_01 = EffectManager.instance.EffectPool(skillType + 1); // 1인덱스는 바람 공격 히트 이펙트

        hitEffect_01.transform.position = gameObject.transform.position;

        return hitEffect_01;
    }

    // 여기서는 매직 서클 이펙트를 부름
    public GameObject MagicCircleEffect(int skillType) // 여기서는 매직 서클 이펙트를 부름
    {
        GameObject slashEffect_01 = EffectManager.instance.EffectPool(skillType + 7); ; // 7인덱스는 바람 매직 서클 이펙트

        slashEffect_01.transform.position = new Vector3(shootPos.position.x, shootPos.position.y + 0.5f, shootPos.position.z + 0.5f);

        return slashEffect_01;
    }

    //------------------------------------------------------------------------------------------------------------------------
    //###############################↑↑↑↑↑↑인터페이스 함수 영역↑↑↑↑↑↑↑###############################------------
    //------------------------------------------------------------------------------------------------------------------------

    public void OnCreatedInPool()
    {
        
    }

    public void OnGettingFromPool()
    {
        SkillSpawnPos();
        //skillType = PlayerShooting.intance.weaponType;

        skillTypeBtn =  PlayerShooting.intance.subSkillType;

        //Debug.Log(" 현재 스킬 타입 " + skillTypeBtn);

        MagicCircleEffect(skillTypeBtn);


    }
}
