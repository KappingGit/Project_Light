using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class SubSkillManager : MonoBehaviour
{
    public static SubSkillManager instance;

    private PoolManager poolManager;

    private void Awake()
    {
        poolManager = GetComponent<PoolManager>();

        if (SubSkillManager.instance == null)
        {
            Debug.Log("SkillManager.instance가 null상태입니다");
            instance = this;
        }

    }

    private void Update()
    {
        
    }

    //풀링 되어있는 오브젝트를 호출
    public GameObject GetPoolSkill(int skillNum)
    {
        SubSkillScript newSkill01 = poolManager.GetFromPool<SubSkillScript>(skillNum);

        GameObject newSkillObj01 = newSkill01.gameObject;

        return newSkillObj01;
    }

    //반환
    public void ReturnSkill(SubSkillScript clone)
    {
        poolManager.TakeToPool<SubSkillScript>(clone.idName, clone); 
    }

   
}
