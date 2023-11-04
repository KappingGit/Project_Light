using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubSkillManager_Setting 
{
    
}

public class SubSkill_WindDrill
{

    public int typeLevel;

    public string weaponName;

    public float coolTime;

    public float drillDamage;

    public int drillCount;


    public SubSkill_WindDrill(int _typeLevel, float _coolTime, string _weaponName, 
        float _drillDamage, int _drillCount)
    {
        //typeLevel = statusDB.NomalAttack[1].typeLevel;
        //name = statusDB.NomalAttack[1].name;
        //singleDamage = statusDB.NomalAttack[1].singleDamage;

        this.typeLevel = _typeLevel;

        this.weaponName = _weaponName;

        this.coolTime = _coolTime;

        this.drillDamage = _drillDamage;

        this.drillCount = _drillCount; // 드릴 타수
    }

    public void CheckData() // 데이터 저장 확인용 (디버그 출력용)
    {
        Debug.Log(this.typeLevel);
        Debug.Log(this.weaponName);
        Debug.Log(this.coolTime);
        Debug.Log(this.drillDamage);
        Debug.Log(this.drillCount);
    }

}

public class SubSkill_WaterBarrier
{

    public int typeLevel;

    public string weaponName;

    public float coolTime;

    public float barrierDuration;

    public int barrierCount;

    public SubSkill_WaterBarrier(int _typeLevel, float _coolTime, string _weaponName, 
        float _barrierDuration, int _barrierCount)
    {
        //typeLevel = statusDB.NomalAttack[1].typeLevel;
        //name = statusDB.NomalAttack[1].name;
        //singleDamage = statusDB.NomalAttack[1].singleDamage;

        this.typeLevel = _typeLevel;

        this.weaponName = _weaponName;

        this.coolTime = _coolTime;

        this.barrierDuration = _barrierDuration; //지속시간

        this.barrierCount = _barrierCount; // 막는 횟수
    }

    public void CheckData() // 데이터 저장 확인용 (디버그 출력용)
    {
        Debug.Log(this.typeLevel);
        Debug.Log(this.weaponName);
        Debug.Log(this.coolTime);
        Debug.Log(this.barrierDuration);
        Debug.Log(this.barrierCount);
    }

}

public class SubSkill_FireBall
{

    public int typeLevel;

    public string weaponName;

    public float coolTime;

    public float penetDamage;

    public int penetCount;

    public SubSkill_FireBall(int _typeLevel, float _coolTime, string _weaponName, 
        float _penetDamage, int _penetCount)
    {
        //typeLevel = statusDB.NomalAttack[1].typeLevel;
        //name = statusDB.NomalAttack[1].name;
        //singleDamage = statusDB.NomalAttack[1].singleDamage;

        this.typeLevel = _typeLevel;

        this.weaponName = _weaponName;

        this.coolTime = _coolTime;

        this.penetDamage = _penetDamage;

        this.penetCount = _penetCount; // 관통 횟수
    }

    public void CheckData() // 데이터 저장 확인용 (디버그 출력용)
    {
        Debug.Log(this.typeLevel);
        Debug.Log(this.weaponName);
        Debug.Log(this.coolTime);
        Debug.Log(this.penetDamage);
        Debug.Log(this.penetCount);
    }

}
