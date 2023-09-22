using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class GimmickManager : MonoBehaviour
{
    public static GimmickManager instance;

    private PoolManager poolManager;

    private void Awake()
    {
        poolManager = GetComponent<PoolManager>();

        if (GimmickManager.instance == null)
        {
            instance = this;
        }

        
    }

    [SerializeField]
    private float firstSpawn = 1f; // 첫 생성 시간

    [SerializeField]
    private float spawnCycle; // 생성 주기(생성 주기부분을 캐릭터 스피드와 연결시켜서 게임 스피드가 빨라지면 스폰도 빨라지게 구현)

    private bool gimmickActive=false;

    private void Update()
    {
        if (BossManager.instance.bossSpawnActive && !gimmickActive)
        {
            gimmickActive = true;

            if (gimmickActive)
            {
                InvokeRepeating("GimmickSpawn", firstSpawn, spawnCycle);
                
            }
            
        }
    }

    public void GimmickSpawn()
    {
        GimmickScript newGimmick = poolManager.GetFromPool<GimmickScript>(0);
    }

    public void GimmickReturnPool(GimmickScript clone) // TakeToPool은 다시 돌려준다는 행위
    {
        
        poolManager.TakeToPool<GimmickScript>(clone); //TakeToPool : 지정된 풀에 반환
    }

}
