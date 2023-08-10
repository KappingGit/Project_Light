using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tilePrefabs; // 사용될 지면 오브젝트 리스트화

    [SerializeField]
    private Transform playerTransform;

    private float spawnZ = 0.0f; // 

    private float tileLenght = 15.0f; // 앞으로 나올 스폰 거리(타일 길이)

    private int tileOnScreen = 7; // 앞으로 소환될 타일 개수

    private void Awake()
    {
        for (int i = 0; i < tileOnScreen; i++) 
        {
            SpawnTile();
        }
    }

    private void Update()
    {
        if (playerTransform.position.z > (spawnZ - tileOnScreen * tileLenght))
        {
            SpawnTile();
        }
    }

    private void SpawnTile(int prefabsIndex = -1)
    {
        GameObject go;

        go = Instantiate(tilePrefabs[0]) as GameObject;

        go.transform.SetParent(transform);

        go.transform.position = Vector3.forward * spawnZ;

        spawnZ += tileLenght;
    }

}
