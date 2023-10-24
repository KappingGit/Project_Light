using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 인터 페이스 매니저 스크립트
public class InterfaceManager : MonoBehaviour
{
    
}

public interface IDie
{
    // 사용된 곳 : EnemyScript.CS, 

    void Die();

    // 죽는 이펙트
    GameObject DieEffect();

    //GameObject DropItem();// todo: 몬스터 드랍 아이템 관련 GameObject DropItem();

    // todo: 타격 이펙트 관련 GameObject HitEffect();

}

public interface INomalAttack
{
    // 평타 관련 인터페이스(사용된 곳: BulletScript.cs, )

    // 데미지 함수 이부분 스테이터스 연결 작업하면서 건들기

    // 히트 이펙트
    GameObject HitEffect();

    // 슬래쉬(검 휘두르는) 이펙트
    GameObject SlashEffect();
}

public interface ISubSkiil
{
    
    //void ActiveSkillSpeed(); //스킬 속도

    //void ActiveSkillPos(); // 스킬 포지션

    // 히트 이펙트
    GameObject HitEffect(int skillType);

    // 
    GameObject MagicCircleEffect(int skillType);

}
