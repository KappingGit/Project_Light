using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindDrillScript : SubSkillScript
{
    // 해당 딕셔너리 처리를 SubSkillManager로 관리하기로 변경


    //Dictionary<int, SubSkill_WindDrill> subSkill_Wind;
    
    //protected override void WindDrill_TypeDictionary() // 윈드 슬래쉬의 데이터를 뽑아온 다음 리스트화 시킴
    //{
    //    subSkill_Wind = new Dictionary<int, SubSkill_WindDrill>();

    //    // 바람 공격 데이터값 저장
    //    for (int nomalAttack_UID = 0; nomalAttack_UID < 6; nomalAttack_UID++)
    //    {
    //        // 가독성 높이기
    //        int indexLevel = nomalAttack_UID;

    //        int indexCoolTime = nomalAttack_UID;

    //        int indexName = nomalAttack_UID;

    //        int indexDamage = nomalAttack_UID;

    //        subSkill_Wind.Add(nomalAttack_UID, new SubSkill_WindDrill(statusDB.SubSkill[indexLevel].typeLevel, statusDB.SubSkill[indexCoolTime].coolTime,
    //            statusDB.SubSkill[indexName].name, statusDB.SubSkill[indexDamage].drillDamage, statusDB.SubSkill[indexDamage].drillCount));

    //    }

    //    // 데이터 확인용
    //    //SubSkill_WindDrill testData = subSkill_Wind[1];

    //    //testData.CheckData();

    //}

    //public override float WindDrillType_CoolTime(int indexNum)
    //{

    //    // 나중에 플레이어 쿨타임 감소 넣기

    //    SubSkill_WindDrill windDrillCoolTime = subSkill_Wind[indexNum];

    //    float finalCoolTime = windDrillCoolTime.coolTime;

    //    return finalCoolTime;
    //}



    //// 플레이어의 공격력
    //private float playerATK;

    //public override float WindDrillType_Damage(int indexNum)
    //{
       
    //    playerATK = statusDB.PlayerStatus[0].playerDamage; // 플레이어의 공격력 패시브로 얻는 선택지는 아직 미구현이니 인덱스 0으로 고정

    //    SubSkill_WindDrill windDrillData = subSkill_Wind[indexNum];

    //    //최종 데미지
    //    float finalDamage = playerATK * windDrillData.drillDamage;


    //    Debug.Log("자식 스크립트의 WindDrillType_Damage() 함수 실행");
    //    Debug.Log("윈드 드릴 최종 데미지 : " + finalDamage);

    //    return finalDamage;

    //}

    //public override int WindDrillType_Count(int indexNum)
    //{

    //    SubSkill_WindDrill windDrillCountData = subSkill_Wind[indexNum];

    //    int drillCount = windDrillCountData.drillCount;

    //    return drillCount;
    //}

}
