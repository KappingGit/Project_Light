using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSlashScript : BulletScript
{
    Dictionary<int, NomalAttack_FireSlash> nomalAttack_Fire;

    protected override void FireSlash_TypeDictionary() // 파이어 슬래쉬의 데이터를 뽑아온 다음 리스트화 시킴
    {
        nomalAttack_Fire = new Dictionary<int, NomalAttack_FireSlash>();

        // 불 공격 데이터 값 저장
        for (int nomalAttack_UID = 12; nomalAttack_UID < 18; nomalAttack_UID++)
        {

            int indexLevel = nomalAttack_UID;

            int indexName = nomalAttack_UID;

            int indexDamage = nomalAttack_UID;

            nomalAttack_Fire.Add(nomalAttack_UID, new NomalAttack_FireSlash(statusDB.NomalAttack[indexLevel].typeLevel, statusDB.NomalAttack[indexName].name, statusDB.NomalAttack[indexDamage].spreadDamage));

        }
    }

    // 플레이어의 공격력
    private float playerATK;


    protected override float FireSlashTypeDamage(int indexNum)
    {

        playerATK = statusDB.PlayerStatus[0].playerDamage; // 플레이어의 공격력 패시브로 얻는 선택지는 아직 미구현이니 인덱스 0으로 고정




        //최종 데미지
        float finalDamage = playerATK;

        //임시 반환
        //float path = 0.5f;

        Debug.Log("자식 스크립트의 FireSlashTypeDamage() 함수 실행");
        Debug.Log("불 기본 평타 최종 데미지 : " + finalDamage);


        return finalDamage;

    }

}
