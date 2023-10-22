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
    }

    private bool isFire;

    // || Input.GetTouch(0).phase == TouchPhase.Moved
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("손뗌 : 공격중단");
            anim.SetBool("isFire", false);

            isFire = false;

        }

        #region 최적화 안한 코드

        if (Input.GetKeyDown(KeyCode.Space) && !isFire)  //터치 누르고 있을때...
        {            

            if (!ChangeSceneManager.instance.fadeInOuting) // 페이드 중이 아니라면 발사하게
            {
                anim.SetBool("isFire", true);

                //StartCoroutine(DelayTime());

                isFire = true;

                if (isFire)
                {
                    StartCoroutine(AttackRate());
                    
                }

            }
                   
        }

        #endregion


        #region 터치식 발사
        //Input.GetTouch(0).phase == TouchPhase.Ended => 손가락을 땠다면...

        // 터치 발사
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved && !isFire)
            {
                //todo: 총알이 발사되는 코드
                //BulletManager.instance.GetPoolBullet(); // 총알 오브젝트를 불러오는 코드
                if (!ChangeSceneManager.instance.fadeInOuting) // 페이드 중이 아니라면 발사하게
                {
                    anim.SetBool("isFire", true);

                    isFire = true;

                    if (isFire)
                    {
                        StartCoroutine(AttackRate());

                    }

                }

            }
            //else
            //{
            //    anim.SetBool("isFire", false);

            //        isFire = false;

            //        if (!isFire)
            //        {
            //            StopCoroutine(AttackRate());
            //        }

            //        Debug.Log("화면에서 손가락을 뗐습니다.");
            //}

        }

        //PlayerAnimControl();
        #endregion


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
    public int weaponType; // 공격 종류를 고르는 변수 0: 바람 공격, 1: 물 공격, 2: 불 공격

    // 공격 속도 지연시키기(내부에 while문을 집어넣어서 터치하고 있을때~~ StartCourutine을 시키고 터치에서 때면 StopCourutine을 시킨다.)
    IEnumerator AttackRate()
    {
        rateDB = statusDB.PlayerStatus[0].attackRate; // 공격속도 데이터 테이블 적용

        float rate = rateDB / 2; // 수식

        // 공격 종류를 고르는 [변수 0 => 바람 공격, 1 => 물 공격, 2 => 불 공격] 나중에 테이블데이터와 연결

        weaponType = 2; // 인스펙터와 지역변수의 실행 순서를 잘 이해를 해야 문제가 발생하지 않는다.
                        // 문제점 : 인스펙터weaponType의 값을 변화시켜도 바뀌지 않는 문제가 발생, 함수안에 집어넣으니 해결
                        //Debug.Log("장착된 무기 속성(인덱스 넘버) : " + weaponType);

        while (isFire)
        {

            BulletManager.instance.GetPoolBullet(weaponType); // 총알 오브젝트 불러오게하는 코드, 인덱스 번호에 따라 일반 공격을 불러옴
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

        isFire = false;

        StopCoroutine(AttackRate());
    }

    IEnumerator DelayTime()
    {
        yield return YieldInstuctionCash.WaitForSeconds(1f);
        isFire = true;
        StopCoroutine(DelayTime());
    }

}
