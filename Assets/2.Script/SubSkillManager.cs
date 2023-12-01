using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class SubSkillManager : MonoBehaviour
{
    public static SubSkillManager instance;

    private PoolManager poolManager;

    [SerializeField]
    private DB_Status statusDB;

    // 플레이어의 공격력
    private float playerATK;

    private void Awake()
    {
        poolManager = GetComponent<PoolManager>();

        if (SubSkillManager.instance == null)
        {
            //Debug.Log("SkillManager.instance가 null상태입니다");
            instance = this;
        }

        WindDrill_TypeDictionary();
        FireBall_TypeDictionary();
    }

    private void Update()
    {

    }

    //풀링 되어있는 오브젝트를 호출
    public GameObject GetPoolSkill(int skillNum)
    {
        SubSkillScript newSkill01 = poolManager.GetFromPool<SubSkillScript>(skillNum);

        GameObject newSkillObj01 = newSkill01.gameObject;

        return newSkillObj01;
    }

    //반환
    public void ReturnSkill(SubSkillScript clone)
    {
        poolManager.TakeToPool<SubSkillScript>(clone.idName, clone);
    }


    //여기서 딕셔너리 처리

    #region 윈드 드릴 딕셔너리 처리
    Dictionary<int, SubSkill_WindDrill> subSkill_Wind;

    public void WindDrill_TypeDictionary() // 윈드 슬래쉬의 데이터를 뽑아온 다음 리스트화 시킴
    {
        subSkill_Wind = new Dictionary<int, SubSkill_WindDrill>();

        // 바람 공격 데이터값 저장
        for (int nomalAttack_UID = 0; nomalAttack_UID < 6; nomalAttack_UID++)
        {
            // 가독성 높이기
            int indexLevel = nomalAttack_UID;

            int indexCoolTime = nomalAttack_UID;

            int indexName = nomalAttack_UID;

            int indexDamage = nomalAttack_UID;

            subSkill_Wind.Add(nomalAttack_UID, new SubSkill_WindDrill(statusDB.SubSkill[indexLevel].typeLevel, statusDB.SubSkill[indexCoolTime].coolTime,
                statusDB.SubSkill[indexName].name, statusDB.SubSkill[indexDamage].drillDamage, statusDB.SubSkill[indexDamage].drillCount));

        }

        // 데이터 확인용
        //SubSkill_WindDrill testData = subSkill_Wind[1];

        //testData.CheckData();

    }

    public float WindDrillType_CoolTime(int indexNum)
    {

        // 나중에 플레이어 쿨타임 감소 넣기

        SubSkill_WindDrill windDrillCoolTime = subSkill_Wind[indexNum];

        float finalCoolTime = windDrillCoolTime.coolTime;

        return finalCoolTime;
    }


    public float WindDrillType_Damage(int indexNum)
    {

        playerATK = statusDB.PlayerStatus[0].playerDamage; // 플레이어의 공격력 패시브로 얻는 선택지는 아직 미구현이니 인덱스 0으로 고정

        SubSkill_WindDrill windDrillData = subSkill_Wind[indexNum];

        //최종 데미지
        float finalDamage = playerATK * windDrillData.drillDamage;


        //Debug.Log("자식 스크립트의 WindDrillType_Damage() 함수 실행");
        //Debug.Log("윈드 드릴 최종 데미지 : " + finalDamage);

        return finalDamage;

    }

    public int WindDrillType_Count(int indexNum)
    {

        SubSkill_WindDrill windDrillCountData = subSkill_Wind[indexNum];

        int drillCount = windDrillCountData.drillCount;

        return drillCount;
    }


    #endregion

    #region 서브 스킬 파이어볼 딕셔너리 처리

    Dictionary<int, SubSkill_FireBall> subSkill_Fire;

    public void FireBall_TypeDictionary()
    {
        subSkill_Fire = new Dictionary<int, SubSkill_FireBall>();

        // 주의 UID의 인덱스를 데이터 테이블을 확인해서 올바르게 입력

        // 불 서브 스킬 공격 데이터값 저장
        for (int nomalAttack_UID = 12; nomalAttack_UID < 18; nomalAttack_UID++)
        {
            // 가독성 높이기
            int indexLevel = nomalAttack_UID;

            int indexCoolTime = nomalAttack_UID;

            int indexName = nomalAttack_UID;

            int indexDamage = nomalAttack_UID;

            int indexCount = nomalAttack_UID;

            subSkill_Fire.Add(nomalAttack_UID, new SubSkill_FireBall(statusDB.SubSkill[indexLevel].typeLevel, statusDB.SubSkill[indexCoolTime].coolTime,
                statusDB.SubSkill[indexName].name, statusDB.SubSkill[indexDamage].penetDamage, statusDB.SubSkill[indexCount].penetCount));

        }

        // 데이터 확인용
        //SubSkill_FireBall testData = subSkill_Fire[13]; // UID 잘 확인할 것

        //testData.CheckData();
    }

    public float FireBallType_CoolTime(int indexNum)
    {

        // 나중에 플레이어 쿨타임 감소 넣기

        SubSkill_FireBall fireBallCoolTime = subSkill_Fire[indexNum];

        float finalCoolTime = fireBallCoolTime.coolTime;

        return finalCoolTime;
    }



    public float FireBallType_PenetDamage(int indexNum)
    {

        playerATK = statusDB.PlayerStatus[0].playerDamage; // 플레이어의 공격력 패시브로 얻는 선택지는 아직 미구현이니 인덱스 0으로 고정

        SubSkill_FireBall fireBallData = subSkill_Fire[indexNum];

        //최종 데미지
        float finalDamage = playerATK * fireBallData.penetDamage;


        //Debug.Log("서브 스킬 매니저 스크립트의 FireBallType_PenetDamage() 함수 실행");
        //Debug.Log("파이어볼 최종 데미지 : " + finalDamage);

        return finalDamage;

    }

    public int FireBallType_PenetCount(int indexNum)
    {

        SubSkill_FireBall fireBallCountData = subSkill_Fire[indexNum];

        int drillCount = fireBallCountData.penetCount;

        return drillCount;
    }

    #endregion



    #region 물 서브 스킬 딕셔너리 처리

    Dictionary<int, SubSkill_WaterBarrier> subSkill_Water;

    public void WaterBarrier_TypeDictionary()
    {
        subSkill_Water = new Dictionary<int, SubSkill_WaterBarrier>();

        // 물 서브 스킬 공격 데이터값 저장
        for (int nomalAttack_UID = 6; nomalAttack_UID < 12; nomalAttack_UID++)
        {
            // 가독성 높이기
            int indexLevel = nomalAttack_UID;

            int indexCoolTime = nomalAttack_UID;

            int indexName = nomalAttack_UID;

            int indexDuration = nomalAttack_UID;

            int indexCount = nomalAttack_UID;

            subSkill_Water.Add(nomalAttack_UID, new SubSkill_WaterBarrier(statusDB.SubSkill[indexLevel].typeLevel, statusDB.SubSkill[indexCoolTime].coolTime,
                statusDB.SubSkill[indexName].name, statusDB.SubSkill[indexDuration].barrierDuration, statusDB.SubSkill[indexCount].barrierCount));

        }

    }

    public float WaterBarrierType_CoolTime(int indexNum)
    {
        SubSkill_WaterBarrier waterBarrierCool = subSkill_Water[7];

        float finalCool = waterBarrierCool.coolTime;

        return finalCool;
    }

    public float WaterBarrierType_Duration(int indexNum)
    {
        SubSkill_WaterBarrier waterBarrierDuration = subSkill_Water[7];

        float currDuration = waterBarrierDuration.barrierDuration;

        return currDuration;
    }

    public int WaterBarrierType_Count(int indexNum)
    {
        SubSkill_WaterBarrier waterBarrierCount = subSkill_Water[7];

        int currCount = waterBarrierCount.barrierCount;

        return currCount;
    }

    #endregion

}
