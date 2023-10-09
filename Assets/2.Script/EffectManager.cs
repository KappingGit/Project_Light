using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    private PoolManager poolManager;

    //private GameObject effectObjPop;

    private void Awake()
    {
        poolManager = GetComponent<PoolManager>(); // PoolManager에 접근 EffectManager의 풀 매니저에 접근

        //effectObjPop = EffectScript.instance.gameObject.GetComponent<GameObject>();

        if (EffectManager.instance == null)
        {
            instance = this;
        }

        //InvokeRepeating("EnemyDieEffectPool", 1, 5);

    }

    private void Update()
    {
        
    }

    public GameObject EnemyDieEffectPool()
    {

        EffectScript newEffect01 = poolManager.GetFromPool<EffectScript>(0); //인덱스 0은 사망이펙트부분

        GameObject newEffectObj01 = newEffect01.gameObject; // 문제 해결: GetComponent를 남발하는 과정에서 생긴 문제 이부분을 빼면 정상작동

        return newEffectObj01;
    }

    //임시 주석처리 
    //public void EffectReturnPool(EffectScript clone)
    //{
    //    poolManager.TakeToPool<EffectScript>(clone.idName, clone);
    //}

    // 스크립트로 반환하는 것이 아닌 오브젝트를 반환하는 방식으로 변경
    public void EffectReturnPool(EffectScript clone)
    {
        poolManager.TakeToPool<EffectScript>(clone.idName, clone);
    }

}
