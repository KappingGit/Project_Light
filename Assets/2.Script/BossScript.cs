using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class BossScript : MonoBehaviour, IPoolObject
{

    [SerializeField]
    public string idName; // 풀링작업에 사용될 오브젝트 닉네임

    public static BossScript instance;

    //private PoolManager poolManager;

    private Animator bossAnim;

    private Transform bossTrans;

    //private Vector3 target = new Vector3(bossTrans.position.x, 3f, bossTrans.position.z);

    private bool isBossPatternStop; 

    private void Awake()
    {
        //해당 스크립트 인스턴스
        if (BossScript.instance == null)
        {
            instance = this;
        }

        // 해당 몬스터 오브젝트 바라보는 방향 조정
        transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        bossAnim = GetComponent<Animator>();
        bossTrans = GetComponent<Transform>();
        isBossDie = false;
    }
        
    private void Update()
    {
        
        if (!isPurification) // 보스가 죽는 연출동안 패턴 못나오게 하기
        {
            
            //보스 패턴3 기믹 파훼후 애니메니션 송출
            if (BossPattern_Wind.instance.isStun)
            {
                bossAnim.SetBool("isStun", true);
            }
            else if (!BossPattern_Wind.instance.isStun)
            {
                bossAnim.SetBool("isStun", false);
            }

            if (isTrigger)
            {
                isTrigger = false; // 코루틴 브레이킹용
                StartCoroutine(PatternEffectDelay()); // 보스패턴03의 코루틴
            }

            //보스 패턴03 이펙트 코루틴에서 떼어옴(PatternEffectDelay())
            if (BossPattern_Wind.instance.isStun) // 보스가 스턴을 먹는다면(기믹 파훼 성공)
            {
                //GameObject patternObj01 = transform.GetChild(3).gameObject;
                //patternObj01.gameObject.SetActive(false);

                pattern03_Effect.gameObject.SetActive(false);

            }
        }
        
    }

    [SerializeField]
    public float bossMaxHP; // 보스 최대체력

    [HideInInspector]
    public float bossCurHP; // 보스 현재체력

    private void BossInit()
    {
        BossSpawnPos();

        //todo: 보스 기본 정보 초기화 넣을 것

        bossCurHP = bossMaxHP; // 체력 초기화


    }

    [SerializeField]
    private GameObject dieDirect_Effect;

    private bool isPurification = false;

    private void BossDirectControl() //공격하는 레이어의 접근 함수
    {
        //dieDirect_Effect.gameObject.SetActive(true);

        bossAnim.SetBool("isBossPurification", true);
        
        bossAnim.SetLayerWeight(1, 1);

        dieDirect_Effect.gameObject.SetActive(true);

        isPurification = true;
    }

    [SerializeField]
    private GameObject pattern03_Effect;

    [HideInInspector]
    public bool isTrigger; // 코루틴 브레이킹용(BossPattern03_AnimControl 브레이킹)

    private void BossPattern03_AnimControl()
    {

        pattern03_Effect.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            bossCurHP -= 1f; //todo : 보스 공격의 데미지 스크립트를 따로 제작하기
            //Debug.Log("보스 현재 체력 : " + bossCurHP);
            if (bossCurHP < 0f)
            {
                //todo: 사망 처리

                // 사망했을때 연출

                BossDirectControl(); // 정화 연출 작업

                if (isPurification) 
                {
                    StartCoroutine(DieDelay());// 정화 연출 딜레이 걸기(여기 안에 반환 함수 들어가있음), 승리 UI를 위한 여부도 포함
                }

                //OnTargetReached();
                //BossManager.instance.bossSpawnActive = false;

                //Debug.Log("보스 죽음" + BossManager.instance.bossSpawnActive);
            }

        }
    }

    private void OnTargetReached() // 반환 작업용 함수
    {
        BossManager.instance.BossReturnPool(this);
    }

    [SerializeField]
    private Transform spawnerPos; // 스폰되는 좌표

    private void BossSpawnPos()
    {
        transform.position = new Vector3(spawnerPos.position.x, 1f, 10f);

    }

    

    // 인터페이스 IPoolObject을 명시적으로 구현

    // 해당 오브젝트가 처음 생성됐을때 실행 함수
    public void OnCreatedInPool()
    {
        BossInit();
    }

    // 해당 오브젝트가 가져올때마다 실행
    public void OnGettingFromPool()
    {
        BossInit();
        
    }

    [HideInInspector]
    public bool isBossDie;

    IEnumerator DieDelay() // 해당 코루틴에는 반환 작업과 승리 UI작업이 들어가 있을거임
    {
        yield return YieldInstuctionCash.WaitForSeconds(7f);

        //todo: 여기다가 승리 UI실행하기
        Debug.Log("승리했습니다");
        isBossDie = true;

        yield return YieldInstuctionCash.WaitForSeconds(5f);

        isBossDie = false;

        yield return YieldInstuctionCash.WaitForSeconds(2f);
        
                
        //Debug.Log("반환 작업 시작");
        OnTargetReached(); //반환

        StopCoroutine(DieDelay());
    }

    IEnumerator PatternEffectDelay() // 보스 패턴 오브젝트 비활성화 작업
    {
        BossPattern03_AnimControl();

        while (true)
        {
            
            if (!BossPattern_Wind.instance.isStun) // 보스가 스턴을 안먹는다면 (기믹 파훼 실패)
            {
                yield return YieldInstuctionCash.WaitForSeconds(7f);
                //GameObject patternObj02 = transform.GetChild(3).gameObject;
                //patternObj02.gameObject.SetActive(false);

                pattern03_Effect.gameObject.SetActive(false);

                break;

            }
            
            yield return YieldInstuctionCash.WaitForSeconds(0.5f);// 반복문 딜레이
        }
        
        StopCoroutine(PatternEffectDelay());
    }
}
