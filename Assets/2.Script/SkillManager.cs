using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    private PoolManager poolManager;

    private void Awake()
    {
        poolManager = GetComponent<PoolManager>();

        if (SkillManager.instance == null)
        {
            Debug.Log("SkillManager.instance가 null상태입니다");
            instance = this;
        }

    }

    private void Update()
    {
        
    }

    //풀링 되어있는 오브젝트를 호출
    public void GetPoolSkill(int idexNum)
    {
        if (idexNum == 0)
        {
            FireDragonSkill newActiveSkill01 = poolManager.GetFromPool<FireDragonSkill>(0); // 파이어 드래곤 idexNum0
        }
        else if (idexNum == 1)
        {
            WindRaySkill newActiveSkill02 = poolManager.GetFromPool<WindRaySkill>(1); // 윈드레이 idexNum1
        }
        else if(idexNum==2)
        {
            LtCircleSkill newActiveSkill03 = poolManager.GetFromPool<LtCircleSkill>(2); // 라이트닝 서클 idexNum2
        }

        
    }

    //반환
    public void ReturnPoolSkill01(FireDragonSkill clone)
    {
        poolManager.TakeToPool<FireDragonSkill>(clone); // 인덱스 이름부분에서 오류남 clone만 사용 :원인 알기
        //poolManager.TakeToPool<ActiveSkillScript>(clone);
    }

    public void ReturnPoolSkill02(WindRaySkill clone)
    {
        poolManager.TakeToPool<WindRaySkill>(clone);
        //poolManager.TakeToPool<ActiveSkillScript>(clone);
    }

    public void ReturnPoolSkill03(LtCircleSkill clone)
    {
        poolManager.TakeToPool<LtCircleSkill>(clone);
        //poolManager.TakeToPool<ActiveSkillScript>(clone);
    }
}
