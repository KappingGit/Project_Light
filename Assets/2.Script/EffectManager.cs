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
        poolManager = GetComponent<PoolManager>(); // PoolManager에 접근

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

    private GameObject effectObjPop;

    public GameObject EnemyDieEffectPool()
    {

        EffectScript newEffect01 = poolManager.GetFromPool<EffectScript>(0); //EffctScript의 newEffect01로 풀링(꺼낸다)

        if (newEffect01 == null)
        {
            Debug.Log("newEffect01가 널 상태입니다");
        }
        else if (newEffect01 != null)
        {
            Debug.Log("newEffect01가 널 상태가 아닙니다");
        }

        effectObjPop = newEffect01.gameObject.GetComponent<GameObject>();

        if (effectObjPop == null)
        {
            Debug.Log("effectObjPop가 널 상태입니다");
        }
        else if (newEffect01 != null)
        {
            Debug.Log("effectObjPop 널 상태가 아닙니다");
        }

        return effectObjPop;
        // 해당 오브젝트를 꺼냈을때 원하는 위치로 리턴할 것...
    }

    public void EffectReturnPool(EffectScript clone)
    {
        poolManager.TakeToPool<EffectScript>(clone.idName, clone);
    }

}
