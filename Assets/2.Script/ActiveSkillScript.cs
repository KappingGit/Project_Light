using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class ActiveSkillScript : MonoBehaviour, IPoolObject
{
    // 해당 스크립트 보류
    public static ActiveSkillScript instance;

    // 플레이어 액티브 스킬
    private void Awake()
    {
        if (ActiveSkillScript.instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        
    }

    [SerializeField]
    private GameObject fireDragon;

    // 임시 이름 저장 나중에 변경
    private void FireDragonSkill() // 액티브 스킬 : 파이어 드래곤
    {
        // 특징 투사체임

    }

    [SerializeField]
    private GameObject windLay;

    private void WindLay() // 액티브 스킬 : 윈드레이
    {
        // 특징 레이저임

    }

    [SerializeField]
    private GameObject lt_Circle;

    private void LT_Circle() // 궁극기 스킬 : 라이트닝 서클
    {
        // 특징 뻥터짐

    }

    //풀 오브젝트 초기 불러올때
    public void OnCreatedInPool()
    {
        
    }

    // 풀 오브젝트 계속 불러올때
    public void OnGettingFromPool()
    {
        
    }


}
