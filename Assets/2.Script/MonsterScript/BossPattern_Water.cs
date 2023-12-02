using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern_Water : BossPattern_Wind
{
    //protected override void Pattern01() //몹소환 패턴은 부모 스크립트에서 불러오기
    //{
    //    base.Pattern01();
    //}

    protected override void Pattern02()
    {
        StartCoroutine(Pattern02_CoolTime());
    }

    protected override void Pattern03()
    {

    }

    //protected override GameObject SpawnPattern() // 해당 함수는 원래 스테이지에따라 몬스터 스폰이 안되서 재정의를 한건되 해결되어서 일단 주석처리함
    //{
    //    //Debug.Log("보스패턴의 스폰");

    //    GameObject newEnemy01 = EnemyManager.instance.Spawn(); // 물슬라임 소환

    //    //GameObject newEnemyObj01 = newEnemy01.gameObject;

    //    return newEnemy01; // BossPattern_Wind.cs - SpawnPattertn()의 지역함수
    //}

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private GameObject warningLine; // 보스 패턴에서 사용되는 경고라인(맵기믹에서 사용되는거랑 동일..)

    IEnumerator Pattern02_CoolTime() // 플레이어 위치로 하이드로 펌프 발사
    {
        while (!isCoolTime02)
        {
            isCoolTime02 = true;

            GameObject newGimmick_Obj01 = GimmickManager.instance.GimmickSpawn(); // 기믹 매니저에서 스테이지별 스폰 종류를 최신화시키는 코드로 바꿔놔서 그냥 써도 해당 스테이지의 맵기믹이 나옴

            CapsuleCollider newGimmick_ObjRig01 = newGimmick_Obj01.gameObject.GetComponent<CapsuleCollider>();

            warningLine.gameObject.SetActive(true); // 경고라인

            warningLine.gameObject.transform.position = new Vector3(playerTransform.position.x, 0.1f, 4.5f);

            newGimmick_Obj01.gameObject.transform.position = new Vector3(playerTransform.position.x, 1f, 14f);

            newGimmick_ObjRig01.height = 0f; //0에서 시작

            while (newGimmick_ObjRig01.height <= 20f) // 물의 효과에 따라 콜라이더의 길이를 조절해서 날라오는 투사체와 같은 효과를 표현
            {
                newGimmick_ObjRig01.height += 5;

                yield return YieldInstuctionCash.WaitForSeconds(0.3f); // 이거를 조절함에따라서 하이드로펌프의 애니메이션(효과)과 딱 떨어짐
            }

            yield return YieldInstuctionCash.WaitForSeconds(1.7f); // 워터빔이 유지되는 시간(애니메이션이 끝나기까지 시간 버는 정도)


            Debug.Log("워터빔 반환 작업");
            newGimmick_ObjRig01.height = 0f; // 반환할때 값 초기화
            //GimmickManager.instance.GimmickReturnPool(newGimmick_Obj01.gameObject); // 이부분에서 오류;; clone의 다른 방법을 모색해야하는데 지식이 부족 스크립트에서 반환시키게할거임

            yield return YieldInstuctionCash.WaitForSeconds(0.5f);
        }
        //임시 방지
        //yield return YieldInstuctionCash.WaitForSeconds(5f); // 이걸로 토네이도가 미친듯이 오는 것을 방지

        if (isCoolTime02)
        {

            yield return YieldInstuctionCash.WaitForSeconds(coolTime02); // 3초 쿨타임

            isCoolTime02 = false;
        }

        StopCoroutine(Pattern02_CoolTime());
    }


}
