using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSlashScript : BulletScript
{
    // 불릿 스크립트가 부모 클래스


    public override float WeaponTypeDamage()
    {
        //임시 반환
        float path = 0;

        return path;
    }
    //바람 속성 기본 공격 방식 : 단일 대상에서 공격력의 100% 이상의 데미지를 줌 (단일 특화)
    public float SingleDamage(int playerATK, float skillType) // 단일 공격(공격력 * 퍼센트) 
    {

        //임시 반환
        float path = 0f;

        return path;
    }
}
