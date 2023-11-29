using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    // 게임 데이터를 저장하는 스크립트로 활용하기


    // 스킬 교체 관련 작업리스트
    // 방향성: 첫번재 저장 스크립트에서 현재 스킬 정보를 저장, 두번째 스킬 교체할 위치를 클릭,
    //  세번째 교체할 스킬을 선택, 네번째 교체할 것인지 여부

    // 여기서 세세하게 계획: 교체할 스킬 위치를 누르고 교체할 스킬을 선택했을 때 해당 UID의 정보를 뽑아와야 함으로
    // 현재 캐릭터가 가지고 있는 스킬 정보를 저장할 장소가 필요로하다.
    // 예를 들어 기본공격 UID 2를 저장하고 가져오면 바람평타 2레벨의 데이터를 보내주는 역할로 수행한다...

    // 일단 저장할 데이터 나열해보기
    // 일반공격 UID, 서브 스킬1 UID, 서브 스킬2 UID를 저장 현재 가지고 있는 스킬 정보와 스위치문을 활용한 오류를 잡는 방향

    // 오브젝트 풀링의 인덱스 번호랑 DB에 있는 인덱스 번호랑 헷갈리지 말것...

    // 대략적으로 정리 : 일반공격은 0~5까지 오브젝트 풀링의 인덱스는 0
    // 6~11까지는 오브젝트 풀링의 인덱스는 1
    // 12~17까지는 오브젝트 풀링의 인덱스 2 Shooting스크립트에는 오브젝트 풀링 인덱스에 맞춰서 오브젝트를 뽑아오니깐 이점을 유의

    public static GameManager instance;


    // 일반공격
    private int wind_NomalAttackUID;

    private int water_NomalAttackUID;

    private int fire_NomalAttackUID;

    
    // 서브 스킬
    private int wind_SubSkillUID;

    private int water_SubSkillUID;

    private int fire_SubSkillUID;


    // 데이터 저장 변수
    private int nomalAttack_UID; // 가지고 있는 기본공격 저장하는 용도

    private int subSkill_UID;

    private int mainSkill_UID;

    private int passive_UID;


    private void Awake()
    {
        if (GameManager.instance == null)
        {
            instance = this;
        }

        // 기본적으로 가지고 있는 UID는 기본 속성 공격을 제외하면 없으므로 전부 0
        // 초기화 (나중에 스테이지 선택하는 경우가 생기면 이 부분 다시 건들기)

        wind_NomalAttackUID = 1;

        water_NomalAttackUID = 7;

        fire_NomalAttackUID = 12;

        wind_SubSkillUID = 0;

        water_SubSkillUID = 0;

        fire_SubSkillUID = 0;

    }

    private void Update()
    {



        //Wind_Transducer();
        //Water_Transducer();
        //Fire_Transducer();


        // 실시간 체크하게 설정
        CheckUID_Transducer();

    }

    // 좀더 쉬운 방향 생각
    // 기존 생각 : 현재 UID가 인덱스 값 x라면 case를 0~5까지 만들어서 해당 UID의 번호라면 오브젝트 풀링 인덱스 값는 0을 출력
    // 위사항대로 만들게 되면 case의 가지수가 매우 많아진다... 이를 간략하게 표현해보자

    // 새로운 생각 : 해당 UID의 값이 0~5라면 관련된 오브젝트 풀링 인덱스 번호를 가져온다


    private int current_WindAttack;

    private int current_WaterAttack;

    private int current_FireAttack;

    // DB UID를 오브젝트 풀링 인덱스로 전환하는 함수
    private int Wind_Transducer() // DB UID 오브젝트 풀링 인덱스 변환기
    {

        // 바람 기본공격
        if (0 < wind_NomalAttackUID && wind_NomalAttackUID <= 5) 
        {
            current_WindAttack = 0;
            return current_WindAttack; // 오브젝트 풀링 인덱스 번호
        }
        else if (wind_NomalAttackUID == 0)
        {
            //스킬을 가지고 있지 않는 상태

            current_WindAttack = -1;
            return current_WindAttack;
        }
     

        return -1; // 아니라면 -1로 반환(아무도 쓰지않는 인덱스 번호)

    }

    private int Water_Transducer()// 오브젝트 풀링 인덱스 1로 변환
    {

        // 물 기본 공격
        if (6 < water_NomalAttackUID && water_NomalAttackUID <= 11)
        {
            current_WaterAttack = 1;
            return current_WaterAttack;
        }
        else if (water_NomalAttackUID == 6)
        {

            current_WaterAttack = -1;
            return current_WaterAttack; // 아니라면 -1로 반환(아무도 쓰지않는 인덱스 번호)

        }

        return -1;
    }

    private int Fire_Transducer() // 오브젝트 풀링 인덱스 2로 변환
    {
        // 불 기본공격
        if (12 < fire_NomalAttackUID && fire_NomalAttackUID <= 17)
        {
            current_FireAttack = 2;
            return current_FireAttack;
        }
        else if (fire_NomalAttackUID == 12)
        {
            current_FireAttack = -1;
            return current_FireAttack; // 아니라면 -1로 반환(아무도 쓰지않는 인덱스 번호)

        }


        return -1;
    }


    private void CheckUID_Transducer() // UID오브젝트 풀링 변환기 체크용 함수
    {
        //Debug.Log("변환기 테스트 : 현재 바람무기의 UID는  "+ wind_NomalAttackUID +"  이며 변환기로 오브젝트 풀링 인덱스 값은  "+ current_WindAttack + "이거이다.");
        //Debug.Log("변환기 테스트 : 현재 물 무기의 UID는  " + water_NomalAttackUID + "  이며 변환기로 오브젝트 풀링 인덱스 값은  " + current_WaterAttack + "이거이다.");
        //Debug.Log("변환기 테스트 : 현재 불 무기의 UID는  " + fire_NomalAttackUID + "  이며 변환기로 오브젝트 풀링 인덱스 값은  " + current_FireAttack + "이거이다.");
    }

    
}
