using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tilePrefabs; // 사용될 지면 오브젝트 리스트화

    [SerializeField]
    private Transform playerTransform;

    private float spawnZ = -10.0f; // 첫 스폰되는 위치(기준치)
    
    private float tileLenght = 150.0f; // 앞으로 나올 스폰 거리(각각 타일의 사이 길이) 변경전:296.0f

    private int tileOnScreen = 1; // 앞으로 소환될 타일 개수 => 코드 새로 작성하면서 소환될 횟수로 변경됨(for문의 횟수에 불과함, 변수 이름과 의미 없어짐)

    private List<GameObject> activeTiles;

    private float safeZone = 155.0f;

    private int lastPrefabsIndex = 0; // 지면 플랫폼 랜덤성을 부여하기 위한 인덱스

    //[SerializeField]
    //private Rigidbody tileRig;

    [SerializeField]
    private float tileSpeed = 35f;

    private void Awake()
    {
        activeTiles = new List<GameObject>();
        
        for (int i = 0; i < tileOnScreen; i++) 
        {
            SpawnTile();
            spawnZ += tileLenght; // 먼저 소환된 해당 타일보다 앞에 생성하기위함
            SpawnTile();
            spawnZ += tileLenght;
            SpawnTile();
        }
    }


    private void Update()
    {
        float mean = (spawnZ - tileOnScreen * tileLenght); //(첫 스폰되는 정면 위치 - (스폰되는 오브젝트 전체 길이))

        //Debug.Log("     spawnZ:    " + spawnZ + "    tileOnScreen:    "+ tileOnScreen + "   tileLenght:    " + tileLenght+ "   mean:   "+ mean);

        if (playerTransform.position.z - safeZone > activeTiles[0].transform.position.z) // playerTransform.position.z - safeZone > mean : 플레이어의 z위치값이  [](첫 스폰 - 보여지는 타일 개수 * 스폰거리) 보다 크다면
        {
            // safeZone을 활용하여 타일의 생성 조건을 나타냄
            //Debug.Log("타일 생성 및 삭제 조건"+"  SpawnZ값   " + spawnZ);

            SpawnTile();

            DeleteTile();
        }
        //activeTiles[0].transform.position.z => 인덱스 0의 타일 z포지션 값
        // 끝없이 길이 생성


    }

    private GameObject go;

    private void SpawnTile(int prefabsIndex = -1)
    {
        #region 기존 스폰 방식
        
        go = Instantiate(tilePrefabs[RandomTilesIndex()]) as GameObject; // RandomTilesIndex() 랜덤성 인덱스 부여

        go.transform.SetParent(transform); // 부모 객체의 위치 값이 자식으로 들어(객체 관리)

        go.transform.position = Vector3.forward * spawnZ; // 해당 오브젝트의 정면 z축 방향 위치
       
        //spawnZ += tileLenght; // z축 위치에 타일의 길이만큼 계속 더함 (앞으로 나아가게 만듬)
        
        activeTiles.Add(go); // activeTiles 리스트 인덱스를 호출
        Rigidbody tileRig = go.GetComponent<Rigidbody>(); // 순서 주의

        tileRig.velocity = new Vector3(0, 0, -tileSpeed); // clone 타일 속도 부여
                
        #endregion


    }

    private void DeleteTile()
    {
        #region 기존 삭제 방식

        Destroy(activeTiles[0]); // activeTiles 오브젝트 인덱스 0부터 지운다.
        activeTiles.RemoveAt(0); // 위에서 스폰된 activeTiles 리스트 인덱스를 지운다. 이것을 없애면 첫 인덱스[0]만 destroy됨

        #endregion

    }

    private int RandomTilesIndex()
    {
        if (tilePrefabs.Length <= 1) //인덱스 반환
        {
            return 0;
        }

        int randomIndex = lastPrefabsIndex;

        while (randomIndex == lastPrefabsIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }

        lastPrefabsIndex = randomIndex;
        return randomIndex;

    }

}
