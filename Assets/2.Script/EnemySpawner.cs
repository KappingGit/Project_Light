using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Transform playerTrans;

    private float settingPos; // 에너미 스폰의 위치 값

    private PoolManager poolManager; //풀매니저 스크립트에 접근

    private float firstSpawn = 1f; // 첫 생성 시간

    private float spawnCycle = 3f; // 생성 주기(생성 주기부분을 캐릭터 스피드와 연결시켜서 게임 스피드가 빨라지면 스폰도 빨라지게 구현)

    private void Awake()
    {
        poolManager = GetComponent<PoolManager>();

        settingPos = transform.position.z - playerTrans.position.z; // 해당 좌표의 거리

        //InvokeRepeating("Spawn", firstSpawn, spawnCycle); // Invokerepeating은 해당 함수를 firstSpawn초후에 spawnCycle초 간격으로 실행하는 용도
        
    }

    private void Update()
    {
        FollowSpawn();
        
    }

    private void FollowSpawn()
    {
        transform.position = new Vector3(0f, 1f, playerTrans.position.z + settingPos); //y축 좌표에 따라 몬스터 스폰y축이 고정됨, 몬스터 스포너 위치값 = 플레이어 좌표 + 해당좌표의 거리 

    }

}
