using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    [SerializeField]
    private Transform playerTrans;

    private float settingPos; // 에너미 스폰의 위치 값

    private void Awake()
    {
        settingPos = transform.position.z - playerTrans.position.z; // 해당 좌표의 거리

        InvokeRepeating("Spawn", 1f, 2f); // Invokerepeating은 해당 함수를 1초후에 2초 간격으로 실행하는 용도

    }

    private void Update()
    {
        FollowSpawn();

    }

    private void FollowSpawn()
    {
        transform.position = new Vector3(0f, 1.5f, playerTrans.position.z + settingPos); // 몬스터 스포너 위치값 = 플레이어 좌표 + 해당좌표의 거리 

    }

    [SerializeField]
    private GameObject[] enemyObject;

    private float xMax;
    private float xMin;

    private float[]xLoad = new float[3];

    // 기획적인 부분 : 만약 도로와 같이 1차선 2차선 3차선으로 할 경우 랜덤함수를 쓰는 것이 아닌 배열로 값으로 해당 포지션 값을 반환하는 것
    private void Spawn()
    {
        #region 스폰 방식 1 : x축 제한 범위 안에서 랜덤하게 적을 스폰

        // 스폰 영역 제한
        //xMax = transform.position.x + 5f; // 적이 나타날 구간 최대치(좌우)
        //xMin = transform.position.x - 5f; // 적이 나타날 구간 최소치(좌우)

        ////spawnTrans.x = Mathf.Clamp(spawn_X, xMax, xMin); // 해당 Mathf의 함수는  xMax와 xMin의 사이에서 Value값(spawn_X)을 반환받는 것을 사용

        //float rand = Random.Range(xMax, xMin);

        //Instantiate(enemyObject[0], new Vector3(rand, transform.position.y, transform.position.z), transform.rotation);

        #endregion

        #region 스폰 방식 2 : 차선 도로와 같은 방식의 스폰

        xLoad[0] = transform.position.x - 3f;
        xLoad[1] = transform.position.x;
        xLoad[2] = transform.position.x + 3f;

        int randInt = Random.Range(0,3);

        Instantiate(enemyObject[0], new Vector3(xLoad[randInt], transform.position.y, transform.position.z), transform.rotation);

        #endregion

    }

}
