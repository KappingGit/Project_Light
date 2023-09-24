using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class WindRaySkill : MonoBehaviour, IPoolObject, IActiveSkiil
{

    [SerializeField]
    public string idName; // 풀링작업에 사용될 오브젝트 닉네임

    public static WindRaySkill instance;

    //윈드레이 스킬 스크립트
    private void Awake()
    {
        
        if (WindRaySkill.instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        
    }

    [SerializeField]
    private float skillTime;

    //인터페이스 IActiveSkiil 참조 함수
    public void ActiveSkillSpeed() // 투사체 발사 속도 조절 : 특이사항 :여기서는 스킬 지속시간
    {
        //todo: 스킬 지속 시간
        StartCoroutine(SkillForTime(skillTime));
    }

    [SerializeField]
    private Transform shootPos;

    //인터페이스 IActiveSkiil 참조 함수
    public void ActiveSkillPos() // 투사체의 발사 위치
    {
        transform.position = new Vector3(shootPos.position.x, 1f, shootPos.position.z);
    }

    private void OnTargetReached() // 반환 작업용 함수
    {
        SkillManager.instance.ReturnPoolSkill02(this);// 총알 반환

    }

    //초기화 함수
    private void InitSkill()
    {
        ActiveSkillPos(); // 스킬 위치
        ActiveSkillSpeed(); // 유의 : 스킬작동 인터페이스때문에 이름이 저럼...
    }

    public void OnCreatedInPool()
    {
        
    }

    public void OnGettingFromPool()
    {
        InitSkill();
    }

    IEnumerator SkillForTime(float time)
    {
        yield return YieldInstuctionCash.WaitForSeconds(time); //time 지속시간

        OnTargetReached();// 반환 작업용 함수

        StopCoroutine(SkillForTime(time));
    }

}
