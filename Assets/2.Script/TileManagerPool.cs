using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class TileManagerPool : MonoBehaviour
{
    public static TileManagerPool instance;

    private PoolManager poolManager;

    [HideInInspector]
    public float spawnZ = -10.0f;

    private float tileLenght = 147.0f; // 앞으로 나올 스폰 거리(각각 타일의 사이 길이) 변경전:296.0f

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
        spawnZ = -10.0f;
        // 첫 생성은 여기서 처리
        for (int i = 0; i < tileOnScreen; i++)
        {
            TileMap(0);
            spawnZ += tileLenght; // 먼저 소환된 해당 타일보다 앞에 생성하기위함
            TileMap(1);
            spawnZ += tileLenght;
            TileMap(2);
        }

    }

    private int indexNum;

    [SerializeField]
    private GameObject stage02_Rain;

    private void Update()
    {
        //if (playerTrans.position.z - safeZone > -150f)
        //{
        //    // safeZone을 활용하여 타일의 생성 조건을 나타냄
        //    //Debug.Log("타일 생성 및 삭제 조건"+"  SpawnZ값   " + spawnZ);
        //    Debug.Log("타일 생성");
        //    TileSpawn();
        //}

        //indexNum = Random.Range(0, 3);

        // 2스테이지 비내리게함
        if (!BossManager.instance.bossSpawnActive)
        {
            if (ChangeSceneManager.instance.stageNum == 2)
            {
                stage02_Rain.gameObject.SetActive(true);
            }
            else
            {
                stage02_Rain.gameObject.SetActive(false);
            }
        }

        
    }

    public GameObject TileMap(int i)
    {

        TileScript newTile01 = poolManager.GetFromPool<TileScript>(i); // 0~2는 바람 마을 3~5는 물 마을 6~8 불 마을

        GameObject newTileObj01 = newTile01.gameObject; 

        //newTileObj01.transform.position = Vector3.forward * spawnZ;

        return newTileObj01;

    }

    public void ReturnTilePool(TileScript clone)
    {
        //poolManager.TakeToPool<TileScript>(clone.idName, clone);
        poolManager.TakeToPool<TileScript>(clone.idName, clone);
    }

    
    //public GameObject TileSpawn()
    //{

    //    GameObject newTile01 = TileMap(0);

    //    newTile01.transform.position = Vector3.forward * spawnZ;

    //    return newTile01;
    //}

    

}
