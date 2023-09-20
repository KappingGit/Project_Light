using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    private PoolManager poolManager;

    private void Awake()
    {
        poolManager = GetComponent<PoolManager>();

        if (EffectManager.instance == null)
        {
            instance = this;
        }

    }

    private void Update()
    {
        
    }

    public void EnemyDieEffect()
    {
        EffectScript newEffect01 = poolManager.GetFromPool<EffectScript>(0);
    }

    public void EffectReturnPool(EffectScript clone)
    {
        poolManager.TakeToPool<EffectScript>(clone.idName, clone);
    }

}
