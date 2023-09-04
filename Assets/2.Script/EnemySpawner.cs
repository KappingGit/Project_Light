using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Transform playerTrans;

    private float settingPos; // 에너미 스폰의 위치 값

    private void Awake()
    {
        settingPos = transform.position.z - playerTrans.position.z; // 해당 좌표의 거리

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
