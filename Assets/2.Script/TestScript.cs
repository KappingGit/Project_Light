using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class TestScript : MonoBehaviour
{
    [SerializeField]
    private string idName;

    //[SerializeField]
    //private Animator anim;

    //[SerializeField]
    //private Vector3 targetPos;

    //private bool isAtDestination;

    public static TestScript instance;

    //NavMeshAgent ai;

    private void Awake()
    {
        //ai = GetComponent<NavMeshAgent>(); // Ai에 접근
        // 해당 스크립트 인스턴스
        if (TestScript.instance = null)
        {
            instance = this;
        }
       
    }

    private void Update()
    {
        //Vector3 reVelocity = transform.InverseTransformDirection(ai.velocity); // 각각의 AI객체에 anim 추가
        //reVelocity = 0;
        //anim.SetFloat("NormalizedSpeed", reVelocity.magnitude / anim.transform.lossyScale.x); // 애니메이션 추가
        //                                                                                      //애니메이션 쪽
        //if (ai.remainingDistance < 2f)
        //{
        //    if (!isAtDestination)
        //        OnTargetReached();

        //    isAtDestination = true;
        //}
        //else
        //{
        //    isAtDestination = false;
        //}

    }

    private void OnTargetReached()
    {

    }

    private void OnEnable()
    {
        
    }

    private float xMax; // 스폰 x범위(최대)
    private float xMin; // 스폰 x범위(최소)


    public void Init() // 생성되는 기본 정보
    {
        // todo: 기본정보

        // todo: Gamemanger 싱글톤 작업 아직 미진행
        //Transform[] spawnPos = GameManger.instance.points; //  스폰 포인트를 지정

        //ai.SetDestination(spawnPos[Random.Range(0, spawnPos.Length)].position); //해당 스폰 포인트로 이동
    }



}