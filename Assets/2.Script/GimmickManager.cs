using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class GimmickManager : MonoBehaviour
{
    public static GimmickManager instance;

    private PoolManager poolManager;

    private void Awake()
    {
        poolManager = GetComponent<PoolManager>();

        if (GimmickManager.instance == null)
        {
            instance = this;
        }

        //InvokeRepeating("GimmickSpawn", firstSpawn, spawnCycle);

    }

    
    private float firstSpawn = 1f; // 첫 생성 시간(코루틴으로 변경되면서 안사용할 듯)

    [SerializeField]
    private float spawnCycle; // 생성 주기(생성 주기부분을 캐릭터 스피드와 연결시켜서 게임 스피드가 빨라지면 스폰도 빨라지게 구현)

    [SerializeField]
    private float warningLineTime; // 경고 시간 조정

    private bool gimmickActive = false;

    private void Update()
    {
        //if (BossManager.instance.bossSpawnActive && gimmickActive) // 보스 등장시 맵기믹 종료
        //{
        //    gimmickActive = false;

        //    if (!gimmickActive)
        //    {
        //        CancelInvoke("GimmickSpawn"); //맵 기믹 끄기
        //    }
        //}

        if (!gimmickActive) // 기믹이 안켜졌으면...
        {
            if (!BossManager.instance.bossSpawnActive) // 보스가 등장안했다면...
            {
                gimmickActive = true;
                Debug.Log("맵기믹 시작");
                StartCoroutine(MapGimmickSpawn());

            }
        }

        gimmickIndexNum = ChangeSceneManager.instance.stageNum - 1; // 실시간 최신화 (이걸 하면 다른 스크립트에서 기믹 스폰(GimmickSpawn())을 호출할 때 해당 스테이지에 맞게 호출될거임)
        //(-1의 이유 : 오브젝트 풀링의 인덱스와 스테이지 번호의 차이로 인해 조정한것이다. 토네이도 기믹은 인덱스 0부터 시작)
    }

    private int gimmickIndexNum;

    public GameObject GimmickSpawn()
    {
        GimmickScript newGimmick01 = poolManager.GetFromPool<GimmickScript>(gimmickIndexNum);

        GameObject newGimmick_Obj01 = newGimmick01.gameObject;

        return newGimmick_Obj01;
    }

    public void GimmickReturnPool(GimmickScript clone) // TakeToPool은 다시 돌려준다는 행위
    {
        poolManager.TakeToPool<GimmickScript>(clone.idName, clone); //TakeToPool : 지정된 풀에 반환

    }

    IEnumerator MapGimmickSpawn() // 스폰 관련된 코루틴
    {
        yield return YieldInstuctionCash.WaitForSeconds(1f); // 첫 생성 시간 (그때그때 수정이 필요하면 위에 변수 사용 firstSpawn)

        while (true)
        {
            // 위에 업데이트에서 gimmickIndexNum를 최신화 선언해서 코드를 단순화 할 수 있을 거임 일단 내비둠
            if (ChangeSceneManager.instance.stageNum == 1) // 1스테이지라면 토네이도 기믹
            {
                gimmickIndexNum = 0;
                //Debug.Log("맵기믹 코루틴 작업 중");

                if (BossManager.instance.bossSpawnActive)
                {
                    //Debug.Log("맵기믹 코루틴 종료");
                    break;
                }

                GimmickSpawn();

                yield return YieldInstuctionCash.WaitForSeconds(spawnCycle); // 스폰싸이클
            }

            if (ChangeSceneManager.instance.stageNum == 2) // 2스테이지의 맵기믹(워터빔)
            {
                gimmickIndexNum = 1;

                //Debug.Log("발사전");
                yield return YieldInstuctionCash.WaitForSeconds(warningLineTime); // 기믹이 생성되기전 경고 시간
                //Debug.Log("발사전");
                //Debug.Log("맵기믹 코루틴 작업 중 : 경고 시간 => "+ warningLineTime);

                if (BossManager.instance.bossSpawnActive)
                {
                    //Debug.Log("맵기믹 코루틴 종료");
                    break;
                }

                GimmickSpawn();
                yield return YieldInstuctionCash.WaitForSeconds(spawnCycle); // 스폰싸이클
            }
            
        }

        gimmickActive = false;

        yield return YieldInstuctionCash.WaitForSeconds(1f);

        StopCoroutine(MapGimmickSpawn());
    }

}
