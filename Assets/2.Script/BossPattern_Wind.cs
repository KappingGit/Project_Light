using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class BossPattern_Wind : MonoBehaviour
{
    public static BossPattern_Wind instance;

    //private PoolManager poolManager;
    

    private void Awake()
    {
        if (BossPattern_Wind.instance == null)
        {
            instance = this;
        }
        patternIndex = Random.Range(1, 4);
        //InvokeRepeating("Pattern02", 1, 2); //테스트 코드

        
    }

    private int patternIndex; // 보스패턴 인덱스

    private void Update()
    {
        if (BossManager.instance.bossSpawnActive) //보스가 등장했다면...패턴 시작
        {
            if (!BossScript.instance.isPurification) // 정화중이 아니라면...
            {
                if (!isRandomDelay) // 랜덤을 딜레이 걸어서 패턴의 생성 속도를 조절
                {
                    isRandomDelay = true;
                    StartCoroutine(RandomDelay()); // 패턴 간격을 두는 코루틴
                }

                //Debug.Log("보스 등장");
                switch (patternIndex)
                {
                    case 1: // 패턴1
                        if (!isCoolTime01)
                        {
                            Debug.Log("보스 패턴1 시작");                        
                            Pattern01();
                            isCoolTime01 = true;

                        }
                        break;
                    case 2: // 패턴2
                        if (!isCoolTime02)
                        {
                            Debug.Log("보스패턴2 시작");
                            Pattern02();
                            isCoolTime02 = true;
                        }
                        break;
                    case 3: // 패턴3
                        if (!isCoolTime03)
                        {
                            BossScript.instance.isTrigger = true; // BossScript의 보스패턴03 코루틴 브레이킹용
                            Debug.Log("보스패턴3 시작");
                            Pattern03();
                            isCoolTime03 = true;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

    }

    protected virtual void Pattern01() // 몹 소환
    {        
        StartCoroutine(Pattern01_CoolTime());
    }

    protected virtual void Pattern02() // 흑풍 패턴
    {
        StartCoroutine(Pattern02_CoolTime());
    }

    protected virtual void Pattern03() // 거대 흑풍 패턴
    {
        StartCoroutine(Pattern03_CoolTime());
    }

    //보스 패턴01 스폰하는 패턴
    protected virtual GameObject SpawnPattern()
    {
        
        GameObject newEnemy01 = EnemyManager.instance.Spawn();

        //GameObject newEnemyObj01 = newEnemy01.gameObject;

        return newEnemy01; // BossPattern_Wind.cs - SpawnPattertn()의 지역함수
    }

    private int patternCurrDuration = 0; // 패턴 지속된 현재 시간

    private int patternDuration = 5; // 패턴 지속 시간 (몇초까지 패턴이 발생되는지...)

    private int spanwCount = 2; // 몇초에 '몇번'소환되는지

    private float delayTime = 1f; // '몇초'에 몇번 소환되는지

    private float coolTime01 = 15f; // 쿨타임

    private bool isCoolTime01 = false;

    private bool isDone;

    IEnumerator Pattern01_CoolTime() // 쿨타임 작업 쿨타임 15초, 5초에 10마리 소환
    {
       
        while (!isDone)
        {
            isDone = false;

            for (int i = 0; i < spanwCount; i++) // 2마리 소환
            {                
                SpawnPattern();
                yield return YieldInstuctionCash.WaitForSeconds(0.75f);
            }

            yield return YieldInstuctionCash.WaitForSeconds(delayTime); //1초의 시간이 흐르면...
            patternCurrDuration++;            //delayTime(딜레이 타임에 따라 지속시간 표현)
            //Debug.Log("지속 시간 :" + patternCurrDuration);

            if (patternCurrDuration == patternDuration) //0=>5 주의 : 5초의 지속시간이면 +1을 한 6을 기입하는 수식임
            {
                //Debug.Log("패턴01 종료");
                isDone = true;
                patternCurrDuration = 0;
                //todo : 쿨타임 적용

            }
        }
        
        if (isDone)
        {            
            yield return YieldInstuctionCash.WaitForSeconds(coolTime01); //15초 쿨타임

            isCoolTime01 = false;
            isDone = false;
        }

        StopCoroutine(Pattern01_CoolTime());

    }

    private float[] xLoad = new float[3]; // x축 차선을 활용할때

    [SerializeField]
    private Transform spawnerPos; // 스폰되는 좌표
         
    //보스패턴2 흑풍 패턴
    private GameObject TornadoSpawnPattern(int patternNum,int xPosIndex) // 보스 두번째 패턴의 몇번째 인덱스 , 해당 토네이도의 x위치의 스폰 인덱스
    {
        GameObject path = null; // 함수 빈호출용도

        if (patternNum == 0) // 보스패턴 2중 1패턴(왼쪽 오른쪽 패턴)
        {
           
            GameObject newGimmick_Obj01 = GimmickManager.instance.GimmickSpawn();

            xLoad[0] = spawnerPos.position.x - 1.6f;
            xLoad[1] = spawnerPos.position.x + 1.6f;

            newGimmick_Obj01.transform.position = new Vector3(xLoad[xPosIndex], spawnerPos.position.y, 55f);

            Rigidbody newGimmick_ObjRig01 = newGimmick_Obj01.gameObject.GetComponent<Rigidbody>();

            newGimmick_ObjRig01.velocity = new Vector3(0f, 0f, -1f * GimmickScript.instance.gimmickSpeed); // 왼쪽으로 속력부여

            return newGimmick_Obj01;
        }
        else if (patternNum == 1) // 보스패턴 2 중 2패턴(지그재그)
        {
            
            GameObject newGimmick_Obj01 = GimmickManager.instance.GimmickSpawn();

            xLoad[0] = spawnerPos.position.x - 1.6f; // 왼쪽
            xLoad[1] = spawnerPos.position.x + 1.6f; // 오른쪽
            xLoad[2] = spawnerPos.position.x; // 가운데는 무조건 나와야함으로

            newGimmick_Obj01.transform.position = new Vector3(xLoad[xPosIndex], spawnerPos.position.y, 55f);

            if (xPosIndex == 2) // 가운데 토네이도는 일직선으로 오게 설정
            {
                Rigidbody newGimmick_ObjRig01 = newGimmick_Obj01.gameObject.GetComponent<Rigidbody>();

                newGimmick_ObjRig01.velocity = new Vector3(0f, 0f, -1f * GimmickScript.instance.gimmickSpeed);

            }

            if (xPosIndex == 0 || xPosIndex == 1)
            {
                Rigidbody newGimmick_ObjRig01 = newGimmick_Obj01.gameObject.GetComponent<Rigidbody>();

                StartCoroutine(TurnCoroutine(newGimmick_Obj01, newGimmick_ObjRig01));

            }
           
            return newGimmick_Obj01;
        }

        return path; // 함수 빈호출용도
    }

    private float coolTime02 = 3f; // 쿨타임

    private bool isCoolTime02 = false;

    IEnumerator Pattern02_CoolTime()
    {
        while (!isCoolTime02)
        {
            int patternNum = Random.Range(0, 2); //0,1을 호출

            if (patternNum == 0)
            {
                isCoolTime02 = true;
                for (int i = 0; i < 2; i++)
                {
                    TornadoSpawnPattern(patternNum, i);
                }
                
            }
            else if (patternNum == 1)
            {
                isCoolTime02 = true;
                //for문 하나로 처리하는 방법 기회되면 모색해보기
                int xPosIndex = Random.Range(0, 2);
                TornadoSpawnPattern(patternNum, xPosIndex); //왼쪽 or 오른쪽스폰
                TornadoSpawnPattern(patternNum, 2); // 가운데는 무조건 나와야한다...
                
            }
            
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
        
    IEnumerator TurnCoroutine(GameObject newGimmick_Obj01, Rigidbody newGimmick_ObjRig01)
    {
        //.... 이게 되네....(완벽하진 않다....)
        newGimmick_ObjRig01 = newGimmick_Obj01.gameObject.GetComponent<Rigidbody>();

        //왼쪽에서 시작하는 토네이도
        do
        {
            if (newGimmick_Obj01.transform.position.x >= spawnerPos.position.x + 1.6f) //오른쪽 시작, 토네이도가 오른쪽범위를 벗어난다면...
            {
                int i = -1;
                //Debug.Log("속도 부여");

                newGimmick_ObjRig01.velocity = new Vector3(i * 5f, 0f, -1f * GimmickScript.instance.gimmickSpeed); // 왼쪽으로 속력부여

                yield return YieldInstuctionCash.WaitForSeconds(1f); // 이 수치에 따라 바운스가 결정된다....
            }
            else if (newGimmick_Obj01.transform.position.x <= spawnerPos.position.x - 1.6f)// 왼쪽 범위를 벗어난다면...
            {
                int i = 1;
                //Debug.Log("속도 부여");

                newGimmick_ObjRig01.velocity = new Vector3(i * 5f, 0f, -1f * GimmickScript.instance.gimmickSpeed); // 왼쪽으로 속력부여

                yield return YieldInstuctionCash.WaitForSeconds(1f);
            }
            
        }
        while (newGimmick_Obj01.transform.position.x >= spawnerPos.position.x + 1.6f); //토네이도가 오른쪽범위를 벗어난다면...

        //오른쪽에서 시작하는 토네이도
        do
        {
            if (newGimmick_Obj01.transform.position.x <= spawnerPos.position.x - 1.6f)// 왼쪽 범위를 벗어난다면...
            {
                int i = 1;

                //Debug.Log("속도 부여");

                newGimmick_ObjRig01.velocity = new Vector3(i * 5f, 0f, -1f * GimmickScript.instance.gimmickSpeed); // 오른쪽으로 속력부여

                yield return YieldInstuctionCash.WaitForSeconds(1f);
            }
            else if (newGimmick_Obj01.transform.position.x >= spawnerPos.position.x + 1.6f) //토네이도가 오른쪽범위를 벗어난다면...
            {
                int i = -1;
                //Debug.Log("속도 부여");

                newGimmick_ObjRig01.velocity = new Vector3(i * 5f, 0f, -1f * GimmickScript.instance.gimmickSpeed); // 왼쪽으로 속력부여

                yield return YieldInstuctionCash.WaitForSeconds(1f);
            }

        }
        while (newGimmick_Obj01.transform.position.x <= spawnerPos.position.x - 1.6f);// 왼쪽 범위를 벗어난다면...
               
    }



    //보스패턴3 거대 흑풍
    
    private float coolTime03 = 25f; // 쿨타임

    private bool isCoolTime03;
        
    private int chargeTime = 5; //차지하는 시간

    private int currChargeTime = 0; //현재 차지 정도

    [HideInInspector]
    public bool isCharge; //차지 상태중

    private bool isSaveData; // 데이터 세이브 여부

    private float saveBossCurrHP; // 패턴 시작했을 때 체력

    [HideInInspector]
    public bool isStun; // 보스 기믹 파훼 성공 후 잠시 스턴

    [HideInInspector]
    public bool isBossPattern03_Damage; // 보스데미지가 들어왔는가?

    IEnumerator Pattern03_CoolTime() //보스패턴03 NuClearPattern
    {
        isCharge = true;
        isSaveData = true;
        if (isSaveData)
        {
            saveBossCurrHP = BossScript.instance.bossCurHP; // 패턴 시작했을 때 현재보스체력 저장
            //Debug.Log("저장된 체력 : " + saveBossCurrHP);
            isSaveData = false;
        }

        while (isCharge)
        {

            yield return YieldInstuctionCash.WaitForSeconds(1f); //1초(패턴 작동 후 흐른시간)
            currChargeTime++;

            //todo: 보스패턴3 이펙트
            

            if (currChargeTime == chargeTime)
            {
                //todo: 플레이어 피격 시스템 구현 데미지 -2
                PlayerStatus.instance.currHP -= 1; // 데미지 1인 이유 : 실직적으로 -2로 들어갈텐데 코드 개판으로해서 플레이어 무적에 -1 여기서 -1 두개 합쳐서 -2 처리하는 방식으로 함 미친놈
                isBossPattern03_Damage = true;
                Debug.Log("패턴 종료 - 기믹 파훼 실패");
                isCharge = false; // 차지중 끝남
                isStun = false; // 기믹 파훼 실패 - 보스 기절안함
                currChargeTime = 0;
                yield return YieldInstuctionCash.WaitForSeconds(1f); // 패턴파훼 후 1초의 여백
            }
            else if (BossScript.instance.bossCurHP <= saveBossCurrHP - (BossScript.instance.bossMaxHP * 0.09)) //최대체력의 10%의 데미지를 받는다면...
            {
                //todo: 패턴 강제 종료
                Debug.Log("패턴 종료 - 기믹 파훼 성공");
                

                isCharge = false; // 차지중 끝남
                isStun = true; // 기믹 파훼 성공 - 보스 기절함
                currChargeTime = 0;
                yield return YieldInstuctionCash.WaitForSeconds(1f); // 패턴파훼 후 1초의 여백
                isStun = false;
            }
        }

        if (!isCharge)
        {
            if (isCoolTime03)
            {
                yield return YieldInstuctionCash.WaitForSeconds(coolTime03); //25초 쿨타임
                
                isCoolTime03 = false;
                
            }
        }
                
        StopCoroutine(Pattern03_CoolTime());
    }

    private bool isRandomDelay;

    IEnumerator RandomDelay()
    {
        patternIndex = Random.Range(1, 4);
        yield return YieldInstuctionCash.WaitForSeconds(3f);
        isRandomDelay = false;

        StopCoroutine(RandomDelay());
    }
    
}
