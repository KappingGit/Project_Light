using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class FireDragonSkill : MonoBehaviour, IPoolObject, IActiveSkiil
{

    [SerializeField]
    public string idName; // 풀링작업에 사용될 오브젝트 닉네임

    public static FireDragonSkill instance;

    private Rigidbody skillRig;

    //파이어드래곤 스킬 스크립트
    private void Awake()
    {
        skillRig = GetComponent<Rigidbody>();

        if (FireDragonSkill.instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        ActiveSkillSpeed();
    }

    [SerializeField]
    private float skillSpeed;

    //인터페이스 IActiveSkiil 참조 함수
    public void ActiveSkillSpeed() // 투사체 발사 속도 조절
    {
        skillRig.velocity = new Vector3(0, 0, skillSpeed);
    }


    [SerializeField]
    private Transform shootPos;

    //인터페이스 IActiveSkiil 참조 함수
    public void ActiveSkillPos() // 투사체의 발사 위치
    {
        transform.position = new Vector3(shootPos.position.x, 1f, shootPos.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Enemy")) // 어느 곳에서 충돌하면 총알 사라짐
        {

            OnTargetReached();

            //Debug.Log("타격 이펙트");

            //Debug.Log("몬스터 또는 벽에 충돌했습니다");
        }

    }

    private void OnTargetReached() // 반환 작업용 함수
    {
        SkillManager.instance.ReturnPoolSkill01(this);// 총알 반환

    }

    //초기화 함수
    private void InitSkill()
    {
        ActiveSkillPos();
    }

    public void OnCreatedInPool()
    {

    }

    public void OnGettingFromPool()
    {
        InitSkill();
    }

}
