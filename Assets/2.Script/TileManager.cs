using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tilePrefabs; // 사용될 지면 오브젝트 리스트화

    [SerializeField]
    private Transform playerTransform;

    private float spawnZ = 0.0f; // 첫 스폰되는 위치(기준치)

    private float tileLenght = 15.0f; // 앞으로 나올 스폰 거리(각각 타일의 사이 길이)

    private int tileOnScreen = 20; // 앞으로 소환될 타일 개수

    private List<GameObject> activeTiles;

    private float safeZone = 15.0f;

    private int lastPrefabsIndex = 0; // 지면 플랫폼 랜덤성을 부여하기 위한 인덱스

    private void Awake()
    {
        activeTiles = new List<GameObject>();
        
        for (int i = 0; i < tileOnScreen; i++) 
        {
            SpawnTile();
        }
    }


    private void Update()
    {
        float mean = (spawnZ - tileOnScreen * tileLenght);

        //Debug.Log("     spawnZ:    " + spawnZ + "    tileOnScreen:    "+ tileOnScreen + "   tileLenght:    " + tileLenght+ "   mean:   "+ mean);

        // 끝없이 길이 생성
        if (playerTransform.position.z - safeZone > mean) // 플레이어의 z위치값이  [](첫 스폰 - 보여지는 타일 개수 * 스폰거리) 보다 크다면
        {
            SpawnTile();

            DeleteTile();
        }
    }

    private void SpawnTile(int prefabsIndex = -1)
    {
        GameObject go;

        go = Instantiate(tilePrefabs[RandomTilesIndex()]) as GameObject; // RandomTilesIndex() 랜덤성 인덱스 부여

        go.transform.SetParent(transform);

        go.transform.position = Vector3.forward * spawnZ; // 해당 오브젝트의 정면 z축 방향 위치

        spawnZ += tileLenght; // z축 위치에 타일의 길이만큼 계속 더함 (앞으로 나아가게 만듬)

        activeTiles.Add(go); // activeTiles 리스트 인덱스를 호출
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]); // activeTiles 오브젝트 인덱스 0부터 지운다.
        activeTiles.RemoveAt(0); // 위에서 스폰된 activeTiles 리스트 인덱스를 지운다. 이것을 없애면 첫 인덱스[0]만 destroy됨
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
