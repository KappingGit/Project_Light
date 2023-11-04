using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSlashScript : BulletScript
{
    //Dictionary<int, NomalAttack_WaterSlash> nomalAttack_Water;

    //protected override void WaterSlash_TypeDictionary() // 윈드 슬래쉬의 데이터를 뽑아온 다음 리스트화 시킴
    //{
    //    nomalAttack_Water = new Dictionary<int, NomalAttack_WaterSlash>();

    //    // 물 공격 데이터값 저장
    //    for (int nomalAttack_UID = 6; nomalAttack_UID < 12; nomalAttack_UID++)
    //    {

    //        int indexLevel = nomalAttack_UID;

    //        int indexName = nomalAttack_UID;

    //        int indexDamage = nomalAttack_UID;

    //        nomalAttack_Water.Add(nomalAttack_UID, new NomalAttack_WaterSlash(statusDB.NomalAttack[indexLevel].typeLevel, statusDB.NomalAttack[indexName].name, statusDB.NomalAttack[indexDamage].speedDown));

    //    }
    //}


    //// 플레이어의 공격력
    //private float playerATK;

    //public override float WaterSlashTypeDamage(int indexNum) // 여기 값이 7~11이 들어가야지 유효(6은 없는 디폴트 물평타)
    //{
       
    //    playerATK = statusDB.PlayerStatus[0].playerDamage; // 플레이어의 공격력 패시브로 얻는 선택지는 아직 미구현이니 인덱스 0으로 고정
                        
    //    //최종 데미지
    //    float finalDamage = playerATK;

    //    //임시 반환
    //    //float path = 0.5f;

    //    Debug.Log("자식 스크립트의 WaterSlashTypeDamage() 함수 실행");
    //    Debug.Log("물 기본 평타 최종 데미지 : " + finalDamage);
        

    //    return finalDamage;

    //}

    //public override float WaterSlashType_SlowEffect(int indexNum) //물 평타의 슬로우 효과
    //{
    //    NomalAttack_WaterSlash waterData = nomalAttack_Water[indexNum];


    //    float speedDownEffect = waterData.speedDown;
    //    Debug.Log("물 기본 평타 슬로우 : " + speedDownEffect);

    //    return speedDownEffect;
    //}


}
