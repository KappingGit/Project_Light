using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGimmick : GimmickScript
{

    [SerializeField]
    private GameObject warningLine01;

    [SerializeField]
    private GameObject warningLine02;

    [SerializeField]
    private GameObject warningLine03;

    private CapsuleCollider waterBeamCollider;

    protected override void GimmickSpawnPos()
    {
        #region 스폰 방식 2 : 차선 도로와 같은 방식의 스폰

        xLoad[0] = spawnerPos.position.x - 1.6f;
        xLoad[1] = spawnerPos.position.x;
        xLoad[2] = spawnerPos.position.x + 1.6f;

        int rand = Random.Range(0, 3);

        // 밑에 있는 경고 라인 주의 사항 : 워터빔 오브젝트가 생성후에 경고가 나와서 난이도가 높아진다..(밖으로 빼야할 듯...)
        if (rand == 0)
        {
            warningLine01.gameObject.SetActive(true);
        }

        if (rand == 1)
        {
            warningLine02.gameObject.SetActive(true);
        }

        if (rand == 2)
        {
            warningLine03.gameObject.SetActive(true);
        }

        transform.position = new Vector3(xLoad[rand], 1f, 14f);

        #endregion
    }

    protected override void Gimmick01() // 바람기믹에서는 여기서 속도를 부여했음(업데이트에 들어가있음)
    {
        //if (!BossManager.instance.bossSpawnActive) //보스 미출현 맵기믹
        //{
            
        //}

        // 보스가 죽게 되면 모든 워터빔이 반환되게함
        if (BossManager.instance.bossSpawnActive)
        {
            if (BossScript.instance.isTimeToReturn)
            {
                GimmickManager.instance.GimmickReturnPool(this);
            }
        }
    }

    // 해당 오브젝트가 가져올때마다 실행
    public override void OnGettingFromPool()
    {
        if (!BossManager.instance.bossSpawnActive)
        {
            waterBeamCollider = GetComponent<CapsuleCollider>();
            StartCoroutine(WaterGimmickRetrunCor()); // 지속시간후 반환되는 코루틴
            GimmickSpawnPos();
        }
        else if (BossManager.instance.bossSpawnActive)
        {
            //SpawnPatternPos(0,2); 보스 패턴 스폰 좌표는 보스패턴 스크립트에서 진행
        }
    }

    IEnumerator WaterGimmickRetrunCor() // 워터빔은 시간에 따라 반환되게
    {

        waterBeamCollider.height = 0f; //

        while (waterBeamCollider.height <= 20f)
        {
            waterBeamCollider.height += 5;

            yield return YieldInstuctionCash.WaitForSeconds(0.3f); // 이거를 조절함에따라서 하이드로펌프의 애니메이션과 딱 떨어짐
        }

        yield return YieldInstuctionCash.WaitForSeconds(1.7f); // 워터빔이 유지되는 시간(애니메이션이 끝나기까지 시간 버는 정도)


        //Debug.Log("워터빔 반환 작업");

        GimmickManager.instance.GimmickReturnPool(this);

        yield return YieldInstuctionCash.WaitForSeconds(0.5f);

        StopCoroutine(WaterGimmickRetrunCor());
    }
}
