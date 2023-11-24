using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class TileManagerPool : MonoBehaviour
{
    public static TileManagerPool instance;

    private PoolManager poolManager;

    private float spawnZ = -10.0f;

    private float safeZone = 155.0f;

    private float tileLenght = 150.0f; // 앞으로 나올 스폰 거리(각각 타일의 사이 길이) 변경전:296.0f

    // 앞으로 소환될 타일 개수 => 코드 새로 작성하면서 소환될 횟수로 변경됨(for문의 횟수에 불과함, 변수 이름과 의미 없어짐)
    private int tileOnScreen = 1; 

    [SerializeField]
    private Transform playerTrans;

    private void Awake()
    {
        poolManager = GetComponent<PoolManager>();

        if (TileManagerPool.instance == null)
        {
            //Debug.Log("EnemyManager.instance가 null상태입니다.");
            instance = this;
        }

        // 첫 생성은 여기서 처리
        for (int i = 0; i < tileOnScreen; i++)
        {
            TileSpawn();
            spawnZ += tileLenght; // 먼저 소환된 해당 타일보다 앞에 생성하기위함
            TileSpawn();
            spawnZ += tileLenght;
            TileSpawn();
        }

    }

    private int indexNum;

    private void Update()
    {
        //if (playerTrans.position.z - safeZone > -150f)
        //{
        //    // safeZone을 활용하여 타일의 생성 조건을 나타냄
        //    //Debug.Log("타일 생성 및 삭제 조건"+"  SpawnZ값   " + spawnZ);
        //    Debug.Log("타일 생성");
        //    TileSpawn();
        //}

        indexNum = Random.Range(0, 3);
    }

    public GameObject TileMap(int i)
    {

        TileScript newTile01 = poolManager.GetFromPool<TileScript>(i); // 0~2는 바람 마을

        GameObject newTileObj01 = newTile01.gameObject; // 인덱스 1,2로 설정하면 오류가 생긴다...

        return newTileObj01;

    }

    public void ReturnTilePool(TileScript clone)
    {
        //poolManager.TakeToPool<TileScript>(clone.idName, clone);
        poolManager.TakeToPool<TileScript>(clone);
    }

    // 타일 스폰 함수 이 함수는 타일 스크립트에서도 작동중이다.
    public GameObject TileSpawn()
    {

        GameObject newTile01 = TileMap(0);

        newTile01.transform.position = Vector3.forward * spawnZ;

        return newTile01;
    }

    

}
