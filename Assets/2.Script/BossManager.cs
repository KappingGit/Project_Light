using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    public static BossManager instance;

    private PoolManager poolManager;

    private float time = 0f;

    private bool bossSpawnTimer;

    private void Awake()
    {
        poolManager = GetComponent<PoolManager>();

        if (BossManager.instance == null)
        {
            instance = this;
        }

        bossSpawnTimer = true;

    }

    private void Update()
    {
        BossTimer();


    }

    public void BossTimer()
    {
        if (bossSpawnTimer)
        {
            time += Time.deltaTime;
            Debug.Log("걸린 시간" + time.ToString("F1"));

            if (time > 10f)
            {
                BossSpawn();
                bossSpawnTimer = false;
            }
        }
    }

    public void BossSpawn()
    {
        BossScript newEnemy = poolManager.GetFromPool<BossScript>(0);
    }

    public void BossReturnPool(BossScript clone)
    {
        poolManager.TakeToPool<BossScript>(clone.idName, clone);
    }


}
