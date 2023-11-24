using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class TileManagerPool : MonoBehaviour
{
    public static TileManagerPool instance;

    private PoolManager poolManager;

    private void Awake()
    {
        poolManager = GetComponent<PoolManager>();

        if (TileManagerPool.instance == null)
        {
            //Debug.Log("EnemyManager.instance가 null상태입니다.");
            instance = this;
        }
    }


    private void Update()
    {
        
    }

    public GameObject TileMap(int i)
    {
        TileScript newTile01 = poolManager.GetFromPool<TileScript>(i); // 0~2는 바람 마을

        GameObject newTileObj01 = newTile01.gameObject;

        return newTileObj01;

    }

    public void ReturnTilePool(TileScript clone)
    {
        //poolManager.TakeToPool<TileScript>(clone.idName, clone);
        poolManager.TakeToPool<TileScript>(clone);
    }

}
