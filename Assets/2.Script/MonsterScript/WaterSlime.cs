using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSlime : EnemyScript
{
    

    public override void SpawnPos()
    {
        // 스폰 영역 제한
        xMax = spawnerPos.position.x + 2f; // 적이 나타날 구간 최대치(좌우)
        xMin = spawnerPos.position.x - 2f; // 적이 나타날 구간 최소치(좌우)

        float rand = Random.Range(xMin, xMax);

        //int randIndex = Random.Range(0, 9); // 풀링 매니저의 오브젝트 해당 인덱스 풀 (예시 => 0번 몬스터 : 일반몬스터, 1번 몬스터 : 보스 몬스터)

        transform.position = new Vector3(rand, 0, spawnerPos.position.z);

        //Debug.Log("스폰 실행");
    }
}
