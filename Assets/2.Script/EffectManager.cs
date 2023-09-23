using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    private PoolManager poolManager;

    private GameObject effectObjPop;

    private void Awake()
    {
        poolManager = GetComponent<PoolManager>(); // PoolManager에 접근

        if (EffectManager.instance == null)
        {
            instance = this;
        }

    }

    private void Update()
    {
        
    }

    public GameObject EnemyDieEffectPool()
    {
        EffectScript newEffect01 = poolManager.GetFromPool<EffectScript>(0); //EffctScript의 newEffect01로 풀링(꺼낸다)

        effectObjPop = newEffect01.gameObject.GetComponent<GameObject>();

        return effectObjPop;

        // 해당 오브젝트를 꺼냈을때 원하는 위치로 리턴할 것...
    }

    public void EffectReturnPool(EffectScript clone)
    {
        poolManager.TakeToPool<EffectScript>(clone.idName, clone);
    }

}
