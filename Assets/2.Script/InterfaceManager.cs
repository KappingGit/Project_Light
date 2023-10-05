using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 인터 페이스 매니저 스크립트
public class InterfaceManager : MonoBehaviour
{
    
}

public interface IDie
{
    void Die();

    // 죽는 이펙트
    GameObject DieEffect();
    
}



public interface IActiveSkiil
{
    void ActiveSkillSpeed(); //스킬 속도

    void ActiveSkillPos(); // 스킬 포지션
}

// 실시간 좌표
public interface ICurPos
{
    //실시간 좌표 인터페이스
    void CurPos();
}