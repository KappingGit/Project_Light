using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class LtCircleSkill : MonoBehaviour, IPoolObject, IActiveSkiil
{

    [SerializeField]
    public string idName; // 풀링작업에 사용될 오브젝트 닉네임

    public static LtCircleSkill instance;

    private Transform skillTrans;

    //라이트닝 써클 스킬 스크립트
    private void Awake()
    {
        skillTrans = GetComponent<Transform>(); // scale접근용...

        //todo: 콜라이더 크기 접근용도 필요

        if (LtCircleSkill.instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        SkillVolume();
    }

    [SerializeField]
    private float skillTime; //퍼지는 정도의 시간(시간이 늘수록 퍼지는 시간이 길어짐)

    //인터페이스 IActiveSkiil 참조 함수
    public void ActiveSkillSpeed() // 투사체 발사 속도 조절: 특이사항 : 여기서는 퍼지는 정도
    {
        //todo: 스킬 지속 시간:퍼지는 정도의 시간(시간이 늘수록 퍼지는 시간이 길어짐)
        StartCoroutine(SkillForTime(skillTime));
    }


    [SerializeField]
    private Transform shootPos;

    //인터페이스 IActiveSkiil 참조 함수
    public void ActiveSkillPos() // 투사체의 발사 위치 : 여기서는 필요 없을지도...(제자리 궁이라...)
    {
        transform.position = new Vector3(shootPos.position.x, 0f, shootPos.position.z);

        skillTrans.transform.localScale = new Vector3(1f, 1f, 1f); // 스킬 크기도 여기서 조정
        //Debug.Log("스킬 크기 값 초기화" + skillTrans.transform.localScale);
    }

    private void OnTargetReached() // 반환 작업용 함수
    {
        SkillManager.instance.ReturnPoolSkill03(this);// 총알 반환

    }

    private float timeVolume = 0f;

    private Vector3 scale;

    private float testSpeed =2f; // 나중에 퍼지는 속도 조절하느데 사용(기획자님께 말씀드리기)

    private void SkillVolume() // 점점 커지게하는 함수
    {

        timeVolume += Time.deltaTime;

        skillTrans.transform.localScale = new Vector3(1f + timeVolume, 1f + timeVolume, 1f + timeVolume);

        if (timeVolume > skillTime) //두개의 변수가 알맞아야지 제대로 작동
        {            
            timeVolume = 0f; // 다시 초기화가 안된다
            //Debug.Log("timeVolume초기화 시도" + timeVolume);
        }

    }

    //초기화 함수
    private void InitSkill()
    {
        ActiveSkillPos(); //스킬 위치, 스킬 크기 초기화도 여기서 조정
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
