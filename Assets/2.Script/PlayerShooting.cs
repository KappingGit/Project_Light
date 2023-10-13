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

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // || Input.GetTouch(0).phase == TouchPhase.Moved
    private void Update()
    {
        #region 최적화 안한 코드

        if (Input.GetKeyDown(KeyCode.Space))  //터치 누르고 있을때...
        {
            //Debug.Log("발사를 시도합니다.");

            if (!ChangeSceneManager.instance.fadeInOuting) // 페이드 중이 아니라면 발사하게
            {
                BulletManager.instance.GetPoolBullet(); // 총알 불러오기

                anim.SetBool("isFire", true);
            }
            
            //StartCoroutine(Attack());

            //bullet = Instantiate(bulletPrefab); //bullet 게임 오브젝트에 bulletPrefab의 오브젝트를 클론화

            //bullet.transform.position = shotPos.transform.position;

            //bullet.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, shotSpeed), ForceMode.Impulse); //해당 오브젝트에 Rigidbody에 접근 
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("isFire", false);
            //StopCoroutine(Attack());
        }

        // 액티브 스킬 관련 : UI 배치도에 맞게 키값 설정 나중에 주의...
        if (Input.GetKeyDown(KeyCode.J))
        {
            //파이어 드래곤 스킬
            SkillManager.instance.GetPoolSkill(0);
        }
        else if(Input.GetKeyDown(KeyCode.K))
        {
            //궁극기 스킬 (합성 스킬)
            SkillManager.instance.GetPoolSkill(2);
        }
        else if(Input.GetKeyDown(KeyCode.L))
        {
            // 윈드레이 스킬
            SkillManager.instance.GetPoolSkill(1);
        }



        #endregion

        // 터치 발사
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                //todo: 총알이 발사되는 코드

                anim.SetBool("isFire", true);

                BulletManager.instance.GetPoolBullet();

                //BulletManager.instance.GetPoolBullet(); // 총알 불러오기

                //bullet = Instantiate(bulletPrefab); //bullet 게임 오브젝트에 bulletPrefab의 오브젝트를 클론화

                //bullet.transform.position = shotPos.transform.position;

                //bullet.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, shotSpeed), ForceMode.Impulse); //해당 오브젝트에 Rigidbody에 접근 
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended) // Ended 손가락이 화면 위를 벗어나 떨어지게 되는 순간...
            {
                anim.SetBool("isFire", false);
                
                Debug.Log("화면에서 손가락을 뗐습니다.");
            }
        }

    }

    public void Init(GameObject projectile, float rate) // 초기화 작업
    {
        if (projectile != null && rate > 0.0f) //만약 Init에 있는 게임 오브젝트가 들어가있고 rate가 0보다 크다면 무기 상태가 초기화 되어있다...
        {
            //bulletPrefab = projectile; // projectile은 총알을 프리펩
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

    // 공격 속도 지연시키기(내부에 while문을 집어넣어서 터치하고 있을때~~ StartCourutine을 시키고 터치에서 때면 StopCourutine을 시킨다.)
    IEnumerator Attack()
    {
        BulletManager.instance.GetPoolBullet();
        yield return YieldInstuctionCash.WaitForSeconds(1.5f);
        
    }

}
