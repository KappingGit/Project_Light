using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGimmick : GimmickScript
{

    [SerializeField]
    private GameObject warningLine01; // 물 기믹 경고라인 1차선

    [SerializeField]
    private GameObject warningLine02; // 물 기믹 경고라인 2차선

    [SerializeField]
    private GameObject warningLine03; // 물 기믹 경고라인 3차선

    private CapsuleCollider waterBeamCollider;

    protected override void GimmickSpawnPos()
    {
        #region 스폰 방식 2 : 차선 도로와 같은 방식의 스폰

        xLoad[0] = spawnerPos.position.x - 1.6f;
        xLoad[1] = spawnerPos.position.x;
        xLoad[2] = spawnerPos.position.x + 1.6f;

        int rand = Random.Range(0, 3);

        // 밑에 있는 경고 라인 주의 사항 : 워터빔 오브젝트가 생성후에 경고가 나와서 난이도가 높아진다..(밖으로 빼야할 듯..., 얼추 해결 Collider를 조정해서 발사되는 것처럼 구현함)
        if (rand == 0)
        {
            warningLine01.gameObject.SetActive(true);
            warningLine01.gameObject.transform.position = new Vector3(spawnerPos.position.x - 1.6f, 0.1f, 4.5f);
        }

        if (rand == 1)
        {
            //warningLine02.gameObject.SetActive(true); // 하나의 변수로만 간략하게 구현해봄 일단 주석처리
            warningLine01.gameObject.SetActive(true);
            warningLine01.gameObject.transform.position = new Vector3(spawnerPos.position.x, 0.1f, 4.5f);
        }

        if (rand == 2)
        {
            //warningLine03.gameObject.SetActive(true);
            warningLine01.gameObject.SetActive(true);
            warningLine01.gameObject.transform.position = new Vector3(spawnerPos.position.x + 1.6f, 0.1f, 4.5f);
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
            if (BossScript.instance.isTimeToReturn) // 보스 사망 기믹 반환
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

        if (BossManager.instance.bossSpawnActive)
        {
            //SpawnPatternPos(0,2); 보스 패턴 스폰 좌표는 보스패턴 스크립트에서 진행
            if (!isTrigger)
            {
                isTrigger = true; // 코루틴 브레이킹용
                Debug.Log("수동 반환 작업 진행");
                StartCoroutine(ReturnManually()); //수동 반환 
            }

        }
    }

    IEnumerator WaterGimmickRetrunCor() // 워터빔은 시간에 따라 반환되게
    {
        Debug.Log("워터빔 리지드 바디 조절 코루틴 실행");
        waterBeamCollider.height = 0f; //

        while (waterBeamCollider.height <= 20f) // 물의 효과에 따라 콜라이더의 길이를 조절해서 날라오는 투사체와 같은 효과를 표현
        {
            waterBeamCollider.height += 5;

            yield return YieldInstuctionCash.WaitForSeconds(0.3f); // 이거를 조절함에따라서 하이드로펌프의 애니메이션(효과)과 딱 떨어짐
        }

        yield return YieldInstuctionCash.WaitForSeconds(1.7f); // 워터빔이 유지되는 시간(애니메이션이 끝나기까지 시간 버는 정도)


        Debug.Log("워터빔 반환 작업");
        waterBeamCollider.height = 0f; // 반환할때 값 초기화
        GimmickManager.instance.GimmickReturnPool(this);

        yield return YieldInstuctionCash.WaitForSeconds(0.5f);

        StopCoroutine(WaterGimmickRetrunCor());
    }

    private bool isTrigger = false; // 수동 반환 코루틴 ReturnManually()의 브레이킹용 (private이니깐 여기서 false로 첫 초기화)

    IEnumerator ReturnManually() // 지금 문제상황 : 물 보스 패턴 스크립트에서 반환 함수를 사용할 수 없어서 기믹 스크립트로 직접 수동으로 반환하는 작업을 진행
    {

        yield return YieldInstuctionCash.WaitForSeconds(2.5f); // 위의 맵기믹 코루틴과 WaitForSeconds 시간이 다른 이유는 맵기믹은 collider의 길이가 늘어나는 시간이 추가로 존재하기 때문


        GimmickManager.instance.GimmickReturnPool(this);

        isTrigger = false; // 이거 순서 위치 주의(위에 할지 아래에 할지...)

        yield return YieldInstuctionCash.WaitForSeconds(0.1f);

        StopCoroutine(ReturnManually());
    }
}
