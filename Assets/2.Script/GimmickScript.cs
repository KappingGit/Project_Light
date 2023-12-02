using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class GimmickScript : MonoBehaviour, IPoolObject
{

    [SerializeField]
    public string idName; // 풀링작업에 사용될 오브젝트 닉네임   

    public static GimmickScript instance;

    private Rigidbody gimmickRig;

    private void Awake()
    {
        gimmickRig = GetComponent<Rigidbody>();

        if (GimmickScript.instance == null)
        {
            instance = this;
        }
               
    }

    
    private void Update()
    {
        Gimmick01(); // 맵기믹 속도부여

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tile"))
        {
            GimmickManager.instance.GimmickReturnPool(this);
        }

    }

    [SerializeField]
    public float gimmickSpeed = 20.0f; // 해당변수는 바람 보스 패턴에서도 사용중이다

    //private int turn;

    protected virtual void Gimmick01() // 보스 미출현 맵기믹 함수
    {
        if (!BossManager.instance.bossSpawnActive) //보스 미출현 맵기믹
        {
            gimmickRig.velocity = new Vector3(0, 0, -gimmickSpeed); // 토네이도의 속도 부여
        }

        // 보스가 죽게 되면 모든 기믹 토네이도가 반환되게함
        if (BossManager.instance.bossSpawnActive)
        {
            if (BossScript.instance.isTimeToReturn) //isTimeToReturn는 보스가 죽어서 맵기믹 반환하는 시간입니다~ 하는 변수이다
            {
                GimmickManager.instance.GimmickReturnPool(this);
            }
        }
        
                        
    }

    protected float xMax;
    protected float xMin;

    protected float[] xLoad = new float[3]; // x축 차선을 활용할때

    [SerializeField]
    protected Transform spawnerPos; // 스폰되는 좌표

    protected virtual void GimmickSpawnPos() // 스폰되는 영역
    {
        #region 스폰 방식 1 : x축 제한 범위 안에서 랜덤하게 적을 스폰

        // 스폰 영역 제한
        //xMax = spawnerPos.position.x + 2f; // 적이 나타날 구간 최대치(좌우)
        //xMin = spawnerPos.position.x - 2f; // 적이 나타날 구간 최소치(좌우)

        //float rand = Random.Range(xMin, xMax);

        ////int randIndex = Random.Range(0, 9); // 풀링 매니저의 오브젝트 해당 인덱스 풀 (예시 => 0번 몬스터 : 일반몬스터, 1번 몬스터 : 보스 몬스터)

        //transform.position = new Vector3(rand, spawnerPos.position.y, spawnerPos.position.z);

        //Debug.Log("스폰 실행");

        #endregion

        #region 스폰 방식 2 : 차선 도로와 같은 방식의 스폰

        xLoad[0] = spawnerPos.position.x - 1.6f;
        xLoad[1] = spawnerPos.position.x;
        xLoad[2] = spawnerPos.position.x + 1.6f;

        int rand = Random.Range(0, 3);

        transform.position = new Vector3(xLoad[rand], spawnerPos.position.y, 55f);

        #endregion

    }

    // 해당 함수(보스패턴02)는 패턴 스크립트에서 사용중
    //보스 패턴 스폰 좌표는 보스패턴 스크립트에서 진행 (해당 함수는 사용하지 않음 / 일단 보존)
    private void SpawnPatternPos(int index01, int index02) // 보스 패턴용 토네이도 위치값
    {
        int randPattern = Random.Range(0, 2);

        if (randPattern == 1)
        {
            // 왼쪽 오른쪽 하나씩 오게끔...
            xLoad[0] = spawnerPos.position.x - 1.6f;
            xLoad[2] = spawnerPos.position.x + 1.6f;

            
            transform.position = new Vector3(xLoad[0], spawnerPos.position.y, 55f);
        }
        else if (randPattern == 2)
        {


            
        }
        xLoad[0] = spawnerPos.position.x - 1.6f;
        xLoad[1] = spawnerPos.position.x; //가운데
        xLoad[2] = spawnerPos.position.x + 1.6f;

        int rand = Random.Range(0, 3);

        transform.position = new Vector3(xLoad[rand], spawnerPos.position.y, 55f);

    }

    // 해당 오브젝트가 처음 생성됐을때 실행 함수
    public void OnCreatedInPool()
    {
        
    }

    // 해당 오브젝트가 가져올때마다 실행
    public virtual void OnGettingFromPool() //virtual을 해도 IPoolObject의 인터페이스에 문제가 안생기는듯
    {
        if (!BossManager.instance.bossSpawnActive)
        {
            GimmickSpawnPos();
        }
        else if (BossManager.instance.bossSpawnActive)
        {
            //SpawnPatternPos(0,2); 보스 패턴 스폰 좌표는 보스패턴 스크립트에서 진행
        }
        
    }
}
