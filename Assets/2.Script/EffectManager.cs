using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    private PoolManager poolManager;

    private GameObject effectObjPool;

    private void Awake()
    {
        poolManager = GetComponent<PoolManager>();

        effectObjPool = gameObject.GetComponent<GameObject>();

        if (EffectManager.instance == null)
        {
            instance = this;
        }

    }

    private void Update()
    {
        
    }

    public void EnemyDieEffectPool()
    {
        EffectScript newEffect01 = poolManager.GetFromPool<EffectScript>(0);
        // 해당 오브젝트를 꺼냈을때 원하는 위치로 리턴할 것...
       
    }

    public void EffectReturnPool(EffectScript clone)
    {
        poolManager.TakeToPool<EffectScript>(clone.idName, clone);
    }

}
