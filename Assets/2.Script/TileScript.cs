using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class TileScript : MonoBehaviour, IPoolObject
{
    [SerializeField]
    public string idName; // 풀링작업에 사용될 오브젝트 닉네임

    private float safeZone = 155.0f;

    [SerializeField]
    private float tileSpeed = 35f;

    [SerializeField]
    private Transform playerTrans;

    private Rigidbody tileRig;

    private bool isSpanwTigger;

    private int randIndex;

    private void Awake()
    {
        tileRig = GetComponent<Rigidbody>();

        isSpanwTigger = false;
    }

    private void Update()
    {
        TileSpeed();
        // playerTransform.position.z - safeZone > mean : 플레이어의 z위치값이  [](첫 스폰 - 보여지는 타일 개수 * 스폰거리) 보다 크다면

        if (0f - safeZone > this.transform.position.z) // 0f부분에서 플레이어 위치값이 안들어가면 오류가 생긴다...
        {
            if (!isSpanwTigger)
            {
                isSpanwTigger = true;

                randIndex = Random.Range(0, 3); // 윈드 필드의 경우... 0~2
                //Debug.Log("반환 및 생성 작업 진행");
                TileManagerPool.instance.TileMap(randIndex);
                // safeZone을 활용하여 타일의 생성 조건을 나타냄
                //Debug.Log("타일 생성 및 삭제 조건"+"  SpawnZ값   " + spawnZ);

                TileManagerPool.instance.ReturnTilePool(this);

                
            }
            
        }

    }

    private void TileSpeed()
    {
        tileRig.velocity = new Vector3(0, 0, -tileSpeed); // clone 타일 속도 부여
    }

    private void TileSpawnPos() // 스폰되는 위치
    {
        transform.position = Vector3.forward * TileManagerPool.instance.spawnZ;
    }


    public void OnCreatedInPool()
    {
        // 해당 오브젝트가 처음 생성됐을때 실행 함수
        //TileSpawnPos();
    }

    public void OnGettingFromPool()
    {
        // 해당 오브젝트가 가져올때마다 실행
        TileSpawnPos();
        isSpanwTigger = false;

    }

    
    

}
