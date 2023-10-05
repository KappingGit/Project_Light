using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class EffectScript : MonoBehaviour, IPoolObject
{
    [SerializeField]
    public string idName; // 풀링작업에 사용될 오브젝트 닉네임

    private GameObject effectObj;

    public static EffectScript instance;

    private void Awake()
    {
        // 해당 자신의 이펙트를 지정 (나중에 하나의 스크립트에서 여러개 사용할 거면 Serialize로 해결할 것)
        //effectObj = GetComponent<GameObject>();

        if (EffectScript.instance == null)
        {
            instance = this;
        }
        //effectObj = GetComponent<GameObject>();
    }

    private void Update()
    {
        //effectObj.gameObject.SetActive(true);

        //임시방편
        //Destroy(this, destroyTime);

    }

    private void EffectInit()
    {

        EffectPos(); // 아직은 현재 좌표를 보려고 만든 함수임(위치값 초기화 함수 아님 유의)

        StartCoroutine(EffectCoroutine()); // 일정 시간 지난후 다시 반환

        //IDie monsterIsDie = GetComponent<IDie>();

        //monsterIsDie.Die();

    }

    // 오브젝트 실행 함수
    private void EffectPos() // 이펙트가 발생되는 몬스터 위치값 함수
    {

        //transform.position = EnemyScript.instance.nowPos; // 이펙트 위치
        Debug.Log("이펙트 좌표값: x" + transform.position.x + "   y" + transform.position.y + "   z" + transform.position.z);
        
    }


    private void OnTargetReached() // 반환 작업용 함수
    {
        // 해당 오브젝트를 다시 반환 시켜준다
        
    }


    // 인터페이스 IPoolObject을 명시적으로 구현
    // 해당 오브젝트가 처음 생성됐을때 실행 함수
    public void OnCreatedInPool()
    {
        
    }

    // 해당 오브젝트가 가져올때마다 실행
    public void OnGettingFromPool()
    {
        // 파티클 속성값 초기화
        EffectInit();
        
    }

    IEnumerator EffectCoroutine() // 일정 시간에 따른 이펙트 반환
    {
        yield return YieldInstuctionCash.WaitForSeconds(1f);
        //EffectManager.instance.EffectReturnPool(this);
        
        StopCoroutine(EffectCoroutine());
    }

}
