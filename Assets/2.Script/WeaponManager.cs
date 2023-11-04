using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    private static WeaponManager instance;

    private int selectWeapon_NomalAttack;

    private int selectWeapon_SubSkill;

    private void Awake()
    {
        if (WeaponManager.instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        
    }

    // 레벨업시 스킬 획득
    private void SelectWeapon()
    {

    }

    // 무기 변환
    private void ChangeWeapon()
    {

    }

}
