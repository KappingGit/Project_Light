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
        nomalAttackUID = 6 => 물 기본 공격 0레벨(미획득)
        nomalAttackUID = 7 => 물 기본 공격 1레벨(획득)

        이런 형태이다
        */



    // 부모 객체를 잊어버리고 자식 객체를 우선시하는 방법(오버라이드)
    //base.WeaponTypeDamage(); // 해당 base를 사용하면 강제로 호출하는 방식임

    // UID 별 해당 데이터를 리스트로 정리해보자
    //private int[] windSlashUID = new int[6];
    //private int[] windSlashLevel = new int[6];
    //private float[] windSlashDamage = new float[6];

    // 해당 무기를 리스트로 뽑아온다

    //public override void WindSlash_TypeList() // 윈드 슬래쉬의 데이터를 뽑아온 다음 리스트화 시킴
    //{
    //    for (int i = 0; i < 6; i++)
    //    {
    //        windSlashUID[i] = statusDB.NomalAttack[i].nomalAttackUID;
    //        windSlashLevel[i] = statusDB.NomalAttack[i].typeLevel;
    //        windSlashDamage[i] = statusDB.NomalAttack[i].singleDamage;
    //    }
    //}

    protected override void InitWindWeapon()
    {
        //base.InitWindWeapon();

        //currentWeapon_Wind = 0; // 현재 무기의 인덱스

    }

    Dictionary<int, NomalAttack_WindSlash> nomalAttack_Wind;

    // 해당 무기를 딕셔너리로 뽑아온다

    protected override void WindSlash_TypeDictionary() // 윈드 슬래쉬의 데이터를 뽑아온 다음 리스트화 시킴
    {
        nomalAttack_Wind = new Dictionary<int, NomalAttack_WindSlash>();

        // 바람 공격 데이터값 저장
        for (int nomalAttack_UID = 0; nomalAttack_UID < 6; nomalAttack_UID++)
        {
            // 가독성 높이기
            int indexLevel = nomalAttack_UID;

            int indexName = nomalAttack_UID;

            int indexDamage = nomalAttack_UID;

            nomalAttack_Wind.Add(nomalAttack_UID, new NomalAttack_WindSlash(statusDB.NomalAttack[indexLevel].typeLevel, statusDB.NomalAttack[indexName].name, statusDB.NomalAttack[indexDamage].singleDamage));

        }
    }

    // 플레이어의 공격력
    private float playerATK;

    //private float windSlashDamage;

    //private int windSlashUID;

    //private int windSlashLevel;

    // 주의 return 함수는 float이며 최종 데미지 계산은 finalDamage로 결과가 나오는데 여기서 해당 레벨이나 UID가 변동이 없을 수 있음
    // 윈드 슬래쉬의 데이터를 뽑아온 데이터를 데미지 수식에 추가
    public override float WindSlashTypeDamage(int indexNum)
    {
        // 바람 속성 기본 공격(평타)의 효과
        // 단일 대상에서 공격력*퍼센트의 단일 대미지를 준다라는 형식이 필요

        playerATK = statusDB.PlayerStatus[0].playerDamage; // 플레이어의 공격력 패시브로 얻는 선택지는 아직 미구현이니 인덱스 0으로 고정

        //windSlashDamage = statusDB.NomalAttack[1].singleDamage; // 해당 코드는 하나의 데이터를 뽑는다

        //windSlashDamage = statusDB.NomalAttack[1].nomalAttackUID; // 해당 코드는 바람 기본평타 1레벨 UID이다(UID : 1)

        //해당 변수는 바람 기본평타의 UID이다
        //windSlashUID = statusDB.NomalAttack[1].nomalAttackUID;

        // 해당 변수는 바람 기본 평의 레벨이다
        //windSlashLevel = statusDB.NomalAttack[1].typeLevel;

        // 해당 변수는 바람 기본 평타의 데미지 퍼센트다
        //windSlashDamage = statusDB.NomalAttack[1].singleDamage;


        NomalAttack_WindSlash windData = nomalAttack_Wind[indexNum];

        //최종 데미지
        float finalDamage = playerATK * windData.singleDamage;

        //if (target.TryGetComponent<IDamage>(out IDamage damage))
        //{
        //    damage.TargetDamage(finalDamage);
        //}

        //임시 반환
        float path = 0.5f;

        Debug.Log("자식 스크립트의 WindSlashTypeDamage() 함수 실행");
        Debug.Log("바람 기본 평타 최종 데미지 : " + finalDamage);

        return finalDamage;
                
    }

   
    
    
}
