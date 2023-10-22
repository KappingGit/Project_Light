using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class BulletManager : MonoBehaviour
{

    public static BulletManager instance; // 싱글톤화

    private PoolManager poolManager; //풀매니저 스크립트에 접근

    private void Awake()
    {
        poolManager = GetComponent<PoolManager>();

        if (BulletManager.instance == null)
        {
            Debug.Log("BulletManager.instance가 null상태입니다.");
            instance = this;
        }

    }

    private void Update()
    {

    }

    

    public GameObject GetPoolBullet(int bulletIndex)
    {
        // 풀링되어있는 총알 불러오기 0번째 인덱스 총알을 불러옴
        BulletScript newBullet = poolManager.GetFromPool<BulletScript>(bulletIndex);

        GameObject newBulletObj_01 = newBullet.gameObject;

        return newBulletObj_01;
    }

    public void ReturnBullet(BulletScript clone)
    {
        poolManager.TakeToPool<BulletScript>(clone.idName, clone);
    }
}
