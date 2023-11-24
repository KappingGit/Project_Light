using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class TileScript : MonoBehaviour, IPoolObject
{
    private float spawnZ = -10.0f;

    private float tileLenght = 150.0f; // 앞으로 나올 스폰 거리(각각 타일의 사이 길이) 변경전:296.0f

    private int tileOnScreen = 1; // 앞으로 소환될 타일 개수 => 코드 새로 작성하면서 소환될 횟수로 변경됨(for문의 횟수에 불과함, 변수 이름과 의미 없어짐)

    private float safeZone = 155.0f;

    private int lastPrefabsIndex = 0; // 지면 플랫폼 랜덤성을 부여하기 위한 인덱스

    [SerializeField]
    private float tileSpeed = 35f;

    [SerializeField]
    private Transform playerTrans;

    private Rigidbody tileRig;

    private void Awake()
    {
        tileRig = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        TileSpeed();
        // playerTransform.position.z - safeZone > mean : 플레이어의 z위치값이  [](첫 스폰 - 보여지는 타일 개수 * 스폰거리) 보다 크다면

        if (0f - safeZone > transform.position.z) // 0f부분에서 플레이어 위치값이 안들어가면 오류가 생긴다...
        {
            TileManagerPool.instance.TileSpawn();
            // safeZone을 활용하여 타일의 생성 조건을 나타냄
            //Debug.Log("타일 생성 및 삭제 조건"+"  SpawnZ값   " + spawnZ);

            TileManagerPool.instance.ReturnTilePool(this);
        }

    }

    private void TileSpeed()
    {
        tileRig.velocity = new Vector3(0, 0, -tileSpeed); // clone 타일 속도 부여
    }

    private void TileSpawnPos() // 스폰되는 위치
    {
        transform.position = Vector3.forward * spawnZ;
    }


    public void OnCreatedInPool()
    {
        // 해당 오브젝트가 처음 생성됐을때 실행 함수
        
    }

    public void OnGettingFromPool()
    {
        // 해당 오브젝트가 가져올때마다 실행
        //TileSpawnPos();
        
    }

    


}
