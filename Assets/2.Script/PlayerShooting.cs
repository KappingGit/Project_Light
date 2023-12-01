using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    //[SerializeField]
    //private GameObject bulletPrefab; // 총알 오브젝트 프리팹

    private float shotSpeed = 40.0f; // 공격 속도 설정

    private bool isInit = false; // 초기화 작업용

    private Animator anim;

    public static PlayerShooting intance;

    [SerializeField]
    private DB_Status statusDB;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        if (PlayerShooting.intance == null)
        {
            intance = this;
        }

        isFire = false;
        isTrigger = false;

        isStopShoot = false;

        // 초반 무기 초기화(인덱스 번호 잘 볼것...// 해당 인덱스 번호는 Pooling되어있는 순서이다)
        // 평타 : 0 => 바람, 1 => 물, 2 => 불 해당 인덱스 번호는 오브젝트 풀링의 인덱스 번호이다...(DB인덱스랑 무관)
        index_WeaponType_Nomal = 0;


        // 서브 스킬 : 0 => 바람, 1 => 물, 2 => 불
        index_WeaponType_SubSkill_01 = 2;

        index_WeaponType_SubSkill_02 = 0;
    }

    private bool isFire;

    private bool isTrigger;

    private bool isStopShoot; // 공격 금지

    // 무기별 인덱스(awake에서 한번 초기화거쳐야함 퍼블릭이라서)
    [HideInInspector]
    public int index_WeaponType_Nomal;

    [HideInInspector]
    public int index_WeaponType_SubSkill_01;

    [HideInInspector]
    public int index_WeaponType_SubSkill_02;

    // || Input.GetTouch(0).phase == TouchPhase.Moved
    private void Update()
    {

        if (Input.GetKeyUp(KeyCode.K) && isTrigger)
        {
            //Debug.Log("손뗌 : 공격중단");
            isTrigger = false;
            anim.SetBool("isFire", false);
                                    
        }

        //Debug.Log("isFire 디버그 찍기 : " + isFire);

        //Debug.Log("isTrigger 디버그 찍기 : " + isTrigger);

        #region 키보드 코드

        // 기본 공격
        if (!isFire) // 발사중이 아니라면
        {
            #region 발사키 코드 였던거

            //if (Input.GetKey(KeyCode.K))  //키를 누르고 발사중이 아니라면...
            //{

            //    if (!ChangeSceneManager.instance.fadeInOuting) // 페이드 중이 아니라면 발사하게
            //    {
            //        anim.SetBool("isFire", true);

            //        isTrigger = true; // 발사버튼 클릭

            //        weaponType = 0; // 인스펙터와 지역변수의 실행 순서를 잘 이해를 해야 문제가 발생하지 않는다.
            //                        // 문제점 : 인스펙터weaponType의 값을 변화시켜도 바뀌지 않는 문제가 발생, 함수안에 집어넣으니 해결
            //                        //Debug.Log("장착된 무기 속성(인덱스 넘버) : " + weaponType);

            //        if (isTrigger) //발사버튼이 클릭이라면...
            //        {
            //            Debug.Log("발사했습니다");
            //            StartCoroutine(AttackRate(weaponType)); //딕셔너리로 하면 이부분과 딕셔너리 부분이 오류남

            //        }

            //    }

            //}

            #endregion


            if (!ChangeSceneManager.instance.fadeInOuting) // 페이드 중이 아니라면 발사하게
            {
                
                isTrigger = true;

                weaponType = index_WeaponType_Nomal;

                //Debug.Log("현재 무기 타입(코루틴 들어가기전) : " + weaponType);

                // 인스펙터와 지역변수의 실행 순서를 잘 이해를 해야 문제가 발생하지 않는다.
                // 문제점 : 인스펙터weaponType의 값을 변화시켜도 바뀌지 않는 문제가 발생, 함수안에 집어넣으니 해결
                //Debug.Log("장착된 무기 속성(인덱스 넘버) : " + weaponType);

                // 발사버튼 클릭

                if (isTrigger) //발사버튼이 클릭이라면...
                {
                    
                    //Debug.Log("발사했습니다");
                    StartCoroutine(AttackRate(weaponType)); 

                }

            }

        }

        // 발사 코드
        

        #endregion


        #region 터치식 발사
        //Input.GetTouch(0).phase == TouchPhase.Ended => 손가락을 땠다면...

        // 터치 발사
        //if (Input.touchCount > 0)
        //{
        //    if (Input.GetTouch(0).phase == TouchPhase.Moved && !isFire)
        //    {
        //        //todo: 총알이 발사되는 코드
        //        //BulletManager.instance.GetPoolBullet(); // 총알 오브젝트를 불러오는 코드
        //        if (!ChangeSceneManager.instance.fadeInOuting) // 페이드 중이 아니라면 발사하게
        //        {
        //            anim.SetBool("isFire", true);

        //            isFire = true;

        //            if (isFire)
        //            {
        //                StartCoroutine(AttackRate(weaponType));

        //            }

        //        }

        //    }
        //    //else
        //    //{
        //    //    anim.SetBool("isFire", false);

        //    //        isFire = false;

        //    //        if (!isFire)
        //    //        {
        //    //            StopCoroutine(AttackRate());
        //    //        }

        //    //        Debug.Log("화면에서 손가락을 뗐습니다.");
        //    //}

        //}

        //PlayerAnimControl();
        #endregion

        // 서브 스킬 1번 버튼
        if (UI_Script.instance.isSubSkillBtn01 || Input.GetKeyDown(KeyCode.A))// UI 서브 스킬 버튼을 눌렀으면...
        {
            
            if (BossManager.instance.bossSpawnActive)
            {
                if (isBossPurification_stopShooting)
                {

                    isStopShoot = true;
                }
            }


            UI_Script.instance.isSubSkillBtn01 = false;
            
            if (!ChangeSceneManager.instance.fadeInOuting) // 페이드 중이 아니라면 발사하게
            {
                if (!isStopShoot)
                {
                    if (!isCoolTime01)
                    {
                        
                        subSkillType = index_WeaponType_SubSkill_01; // 해당 버튼에 있는 스킬 속성 값

                        StartCoroutine(SkillAttackRate01(subSkillType));
                    }
                }
                                                
            }
            

        }

        // 서브 스킬 2번 버튼
        if (UI_Script.instance.isSubSkillBtn02 || Input.GetKeyDown(KeyCode.D))// UI 서브 스킬 버튼을 눌렀으면...
        {
            if (BossManager.instance.bossSpawnActive)
            {
                if (isBossPurification_stopShooting)
                {
                    isStopShoot = true;
                }
            }

            UI_Script.instance.isSubSkillBtn02 = false;
            
            if (!ChangeSceneManager.instance.fadeInOuting) // 페이드 중이 아니라면 발사하게
            {
                if (!isStopShoot)
                {
                    if (!isCoolTime02)
                    {
                        anim.SetBool("isFire", true);


                        subSkillType = index_WeaponType_SubSkill_02; // 해당 버튼에 있는 스킬 속성 값 0바람 1물 2불

                        StartCoroutine(SkillAttackRate02(subSkillType));
                    }
                }

            }
            

        }




    }

    public void Init(GameObject projectile, float rate) // 초기화 작업
    {
        if (projectile != null && rate > 0.0f) //만약 Init에 있는 게임 오브젝트가 들어가있고 rate가 0보다 크다면 무기 상태가 초기화 되어있다...
        {
            
            shotSpeed = rate; // 공격속도를 뜻함
            
            isInit = true; // 초기화 성공했으니 true로 변환
        }
        else // 검증을 항상 남겨두는 버릇을 들여놓기
        {
            isInit = false;
            Debug.Log(" 무기 초기화에 실패하였습니다."); // 디버그로 무기 초기화가 이루어져있는지 확인
        }
    }

    private bool isFiring; // 발사중인지 확인

    public bool Shooting // 총알을 발사하는 프로퍼티
    {
        set 
        {
            isFiring = value; // isFiring값은 value로 불러온다 

            if (isInit) // 기본적인 초기화가 이루어지면...
            {
                if (isFiring) // 발사 상태이면...
                {
                    // todo : 발사
                }
                else
                {
                    // todo : 발사 금지
                }
            }
            else //초기화가 이루어져있지 않습니다.
            {
                Debug.Log("Init이 초기화가 되어있지 않습니다.");
            }
        }

        get
        {
            return isFiring;
        }
       
    }

    private void PlayerAnimControl() //공격하는 레이어의 접근 함수
    {
        if (anim.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.5f)
        {
            anim.SetLayerWeight(1, 0);
        }
    }

    private float rateDB; // 데이터 테이블 적용

    [HideInInspector]
    public int weaponType; // 임시 공격 종류를 고르는 변수 0: 바람 공격, 1: 물 공격, 2: 불 공격

    [HideInInspector]
    public bool isBossPurification_stopShooting;

    // 공격 속도 지연시키기(내부에 while문을 집어넣어서 터치하고 있을때~~ StartCourutine을 시키고 터치에서 때면 StopCourutine을 시킨다.)
    IEnumerator AttackRate(int weaponType) // 기본 공격의 공격속도처리
    {
        rateDB = statusDB.PlayerStatus[0].attackRate; // 공격속도 데이터 테이블 적용

        float rate = rateDB; // 수식

        // 공격 종류를 고르는 [변수 0 => 바람 공격, 1 => 물 공격, 2 => 불 공격] 나중에 테이블데이터와 연결
                
        while (isTrigger) // 발사버튼이 클릭이라면..
        {
            if (BossManager.instance.bossSpawnActive)
            {
                if (isBossPurification_stopShooting) // 정화작업중일때 공격금지
                {
                    //Debug.Log("공격 중단 디버그 테스트: 보스 등장 여부 => " + BossManager.instance.bossSpawnActive + "   보스 정화 참 거짓 여부 => " + BossScript.instance.isPurification);
                    //Debug.Log("공격 중단 디버그(이게 실행되었다는 뜻은 보스가 생성 동시에 정화 작업중이라는 뜻)");
                    anim.SetBool("isFire", false);
                    isTrigger = false;
                    break;
                }
            }

            if (WeaponManager.instance.isChange_NA)
            {
                WeaponManager.instance.isChange_NA = false;
                isTrigger = false;
                break;
            }

            //Debug.Log("현재 무기 타임은.... :" + weaponType);
            anim.SetBool("isFire", true);

            isFire = true; // 발사중

            BulletManager.instance.GetPoolBullet(weaponType); // 총알 오브젝트 불러오게하는 코드, 인덱스 번호에 따라 일반 공격을 불러옴
            //SubSkillManager.instance.GetPoolSkill(weaponType); // 스킬 테스트
            
            yield return YieldInstuctionCash.WaitForSeconds(rate);
            //Debug.Log("rate" + rate);
            
            //터치 테스트
            //if (Input.GetTouch(0).phase == TouchPhase.Ended)
            //{
            //    anim.SetBool("isFire", false);
            //    isFire = false;
            //    break;
            //}
          
        }
        //Debug.Log("총알 발사 테스트");
        anim.SetBool("isFire", false);
        isFire = false; // 발사중단
        StopCoroutine(AttackRate(weaponType));
    }

    [HideInInspector]
    public float subSkillCool01;

    [HideInInspector]
    public float subSkillCool02;

    [HideInInspector]
    public int subSkillType;

    [HideInInspector]
    public bool isCoolTime01;

    [HideInInspector]
    public bool isCoolTime02;

    IEnumerator SkillAttackRate01(int indexNum) // 서브 스킬의 공격속도 및 발사 트리거 기능(해당 기능에서 쿨타임 조절할 것)
    {

        anim.SetBool("isFire", true);
        SubSkillManager.instance.GetPoolSkill(indexNum); // 스킬 불러오기

        //yield return YieldInstuctionCash.WaitForSeconds(0.1f);

        // ###################인덱스 번호 꼭꼭 잘확인할것...#########################

        subSkillCool01 = SubSkillManager.instance.FireBallType_CoolTime(13); // 공격속도 데이터 테이블 적용(나중에 이곳에다가 쿨타임 적용)

        //subSkillCool01 = 10f;

        float rate = subSkillCool01; // 수식

        // 공격 종류를 고르는 [변수 0 => 바람 공격, 1 => 물 공격, 2 => 불 공격] 나중에 테이블데이터와 연결

        //indexNum = 2; // 인스펙터와 지역변수의 실행 순서를 잘 이해를 해야 문제가 발생하지 않는다.
                        // 문제점 : 인스펙터weaponType의 값을 변화시켜도 바뀌지 않는 문제가 발생, 함수안에 집어넣으니 해결
                        //Debug.Log("장착된 무기 속성(인덱스 넘버) : " + weaponType);
        

        isCoolTime01 = true;
        yield return YieldInstuctionCash.WaitForSeconds(rate);
        isCoolTime01 = false;
        //Debug.Log("스킬 테스트");
        anim.SetBool("isFire", false);
        

        StopCoroutine(SkillAttackRate01(indexNum));
    }

    IEnumerator SkillAttackRate02(int indexNum) // 서브 스킬의 공격속도 및 발사 트리거 기능(해당 기능에서 쿨타임 조절할 것)
    {
        anim.SetBool("isFire", true);

        SubSkillManager.instance.GetPoolSkill(indexNum); // 스킬 테스트

        //yield return YieldInstuctionCash.WaitForSeconds(0.1f);
        subSkillCool02 = SubSkillManager.instance.WindDrillType_CoolTime(1);
        //subSkillCool02 = SubSkillScript.instance.WindDrillType_CoolTime(1); // 공격속도 데이터 테이블 적용(나중에 이곳에다가 쿨타임 적용)

        //subSkillCool02 = 10f;

        float rate = subSkillCool02; // 쿨타임

        // 공격 종류를 고르는 [변수 0 => 바람 공격, 1 => 물 공격, 2 => 불 공격] 나중에 테이블데이터와 연결

        //indexNum = 2; // 인스펙터와 지역변수의 실행 순서를 잘 이해를 해야 문제가 발생하지 않는다.
        // 문제점 : 인스펙터weaponType의 값을 변화시켜도 바뀌지 않는 문제가 발생, 함수안에 집어넣으니 해결
        //Debug.Log("장착된 무기 속성(인덱스 넘버) : " + weaponType);
                        

        isCoolTime02 = true;
        Debug.Log("쿨타임입니다.");
        yield return YieldInstuctionCash.WaitForSeconds(rate);
        Debug.Log("쿨타임 띁났습니다.");
        isCoolTime02 = false;
        //Debug.Log("스킬 테스트");
        anim.SetBool("isFire", false);


        StopCoroutine(SkillAttackRate02(indexNum));
    }

    IEnumerator DelayTime() // 마구 눌렀을 때 연속발사 못하게
    {
                
        yield return YieldInstuctionCash.WaitForSeconds(1f);

        StopCoroutine(DelayTime());
    }

}
