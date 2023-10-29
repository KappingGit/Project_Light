using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSlashScript : BulletScript
{
   
    

    // 불릿 스크립트가 부모 클래스
    // 불릿에서 가져오는 함수 : 총알 프리펩을 소환, 총알의 속도,
    // 히트 이펙트, 검 휘두르는 이펙트, 해당 총알의 발사 위치
    // 적에게 히트 했을 경우...(collider)

    /*
        nomalAttackUID가 1이면 바람기본 공격 1레벨, 2이면 바람 기본공격 2레벨
        3이면 바람 기본공격 3레벨의 뜻이며
        만약 값이 6이면 물 기본 공격 0레벨(미획득)이고 값이 7이면 물 기본 공격 1레벨이라는 형식

        위 공식을 아래로 정리해서 나열하면

        nomalAttackUID = 0 => 바람 기본 공격 0레벨(미획득)
        nomalAttackUID = 1 => 바람 기본 공격 1레벨(획득)
        nomalAttackUID = 2 => 바람 기본 공격 2레벨(획득)
        nomalAttackUID = 3 => 바람 기본 공격 3레벨(획득)
        nomalAttackUID = 4 => 바람 기본 공격 4레벨(획득)
        nomalAttackUID = 5 => 바람 기본 공격 5레벨(획득)
        nomalAttackUID = 6 => 바람 기본 공격 0레벨(미획득)
        nomalAttackUID = 7 => 바람 기본 공격 1레벨(획득)

        이런 형태이다
        */



    // 부모 객체를 잊어버리고 자식 객체를 우선시하는 방법(오버라이드)
    //base.WeaponTypeDamage(); // 해당 base를 사용하면 강제로 호출하는 방식임

    // 플레이어의 공격력
    private float playerATK;

    private float windSlashDamage;

    public override float WindSlashTypeDamage()
    {
        // 바람 속성 기본 공격(평타)의 효과
        // 단일 대상에서 공격력*퍼센트의 단일 대미지를 준다라는 형식이 필요

        playerATK = statusDB.PlayerStatus[0].playerDamage; // 플레이어의 공격력

        //windSlashDamage = statusDB.NomalAttack[1].singleDamage; // 해당 코드는 하나의 데이터를 뽑는다
        windSlashDamage = statusDB.NomalAttack[1].nomalAttackUID; // 해당 코드는 int 1이다.

        //최종 데미지
        float finalDamage = playerATK * windSlashDamage;

        //임시 반환
        //float path = 0.5f;

        Debug.Log("자식 스크립트의 WeaponTypeDamage() 함수 실행");
                
        return finalDamage;

        
    }

    // UID의 넘버 , 해당 객체의 Value
    Dictionary<int, float> windSlashType = new Dictionary<int, float>();


    // 현재 무기 타입(딕셔너리 함수인데) (해당 함수는 초기화 로직 함수에 넣어둠)
    private void CurrentWindSlashType() //현재 바람 기본 평타 타입(레벨이나 공격력 등등), 지역 변수로 현재 레벨을 입력
    {

        // 해당 부분을 열거형으로 간략하게 표현하기...
        // dictionary를 활용 UID가 0이라면 바람 기본 공격

        windSlashType.Add(statusDB.NomalAttack[1].nomalAttackUID, statusDB.NomalAttack[1].singleDamage);

        Debug.Log("해당 기본공격은 바람 기본 평타 1레벨입니다. " + windSlashType[0]);


    }

    //바람 속성 기본 공격 방식 : 단일 대상에서 공격력의 100% 이상의 데미지를 줌 (단일 특화)
    public float SingleDamage(int playerATK, float skillType) // 단일 공격(공격력 * 퍼센트) 
    {

        //임시 반환
        float path = 0f;

        return path;
    }
}
