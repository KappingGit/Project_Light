using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalAttackManager
{
    
    
}

public class NomalAttack_WindSlash
{

    public int typeLevel;

    public string weaponName;

    public float singleDamage;


    public NomalAttack_WindSlash(int _typeLevel, string _weaponName, float _singleDamage)
    {
        //typeLevel = statusDB.NomalAttack[1].typeLevel;
        //name = statusDB.NomalAttack[1].name;
        //singleDamage = statusDB.NomalAttack[1].singleDamage;

        this.typeLevel = _typeLevel;

        this.weaponName = _weaponName;

        this.singleDamage = _singleDamage;

    }

    public void CheckData() // 데이터 저장 확인용 (디버그 출력용)
    {
        Debug.Log(this.typeLevel);
        Debug.Log(this.weaponName);
        Debug.Log(this.singleDamage);
    }

}

public class NomalAttack_WaterSlash
{

    public int typeLevel;

    public string weaponName;

    public float speedDown;

    public NomalAttack_WaterSlash(int _typeLevel, string _weaponName, float _speedDown)
    {
        //typeLevel = statusDB.NomalAttack[1].typeLevel;
        //name = statusDB.NomalAttack[1].name;
        //singleDamage = statusDB.NomalAttack[1].singleDamage;

        this.typeLevel = _typeLevel;

        this.weaponName = _weaponName;

        this.speedDown = _speedDown;

    }

    public void CheckData() // 데이터 저장 확인용 (디버그 출력용)
    {
        Debug.Log(this.typeLevel);
        Debug.Log(this.weaponName);
        Debug.Log(this.speedDown);
    }

}

public class NomalAttack_FireSlash
{

    public int typeLevel;

    public string weaponName;

    public float spreadDamage;

    public NomalAttack_FireSlash(int _typeLevel, string _weaponName, float _spreadDamage)
    {
        //typeLevel = statusDB.NomalAttack[1].typeLevel;
        //name = statusDB.NomalAttack[1].name;
        //singleDamage = statusDB.NomalAttack[1].singleDamage;

        this.typeLevel = _typeLevel;

        this.weaponName = _weaponName;

        this.spreadDamage = _spreadDamage;

    }

    public void CheckData() // 데이터 저장 확인용 (디버그 출력용)
    {
        Debug.Log(this.typeLevel);
        Debug.Log(this.weaponName);
        Debug.Log(this.spreadDamage);
    }

}