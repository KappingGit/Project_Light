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
        
    [SerializeField]
    private Renderer rend;

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
        isTimeToReturn = false; // 보스가 사망하는 시점 모든 기믹 몬스터 패턴등 반환하는 작업들어가는 용도
        isBossDie = false; // 보스 사망 여부

        isPurification = false; // 보스 정화 여부

    }

    private void Update()
    {
        BossPatternSetting();

        //Debug.Log("정화 작업 테스트 디버그" + isPurification);

        //Debug.Log("보스 현재 체력 디버그" + bossCurHP);

        UI_Script.instance.bossCurHP_Data = bossCurHP; // 싱글톤으로 인해 데이터가 중구난방이여서 반대로 데이터를 불러오게 함
        UI_Script.instance.bossMaxHP_Data = bossMaxHP; // 싱글톤으로 인해 데이터가 중구난방이여서 반대로 데이터를 불러오게 함
        UI_Script.instance.isBossDie_Data = isBossDie;
        PlayerShooting.intance.isBossPurification_stopShooting = isPurification;
    }

    protected virtual void BossPatternSetting()
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

        isTimeToReturn = false; // 보스가 사망하는 시점 모든 기믹 몬스터 패턴등 반환하는 작업들어가는 용도

        isBossDie = false; // 보스 사망 여부

        isPurification = false; // 보스 정화 여부

    }

    [SerializeField]
    private GameObject dieDirect_Effect;

    [HideInInspector]
    public bool isPurification;

    private void BossDirectControl() //공격하는 레이어의 접근 함수
    {
        //dieDirect_Effect.gameObject.SetActive(true);
        Debug.Log("정화 연출에 사용되는 오브젝트 작업 함수");
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


    #region 기본 공격 3가지 묶음(미친 짓)

    private void Hit_WindSlash() // 최종 데미지 피격 함수
    {
        if (bossCurHP > 0)
        {


            //Debug.Log("몬스터가 피격 받기전 체력입니다 - 바람 기본공격  " + bossCurHP);

            bossCurHP -= BulletManager.instance.WindSlashTypeDamage(WeaponManager.instance.windSlash_CurLevelIndex);

            //Debug.Log("몬스터가 피격을 받았습니다.- 바람 기본공격  " + bossCurHP);
            StartCoroutine(BossHitEffect());

        }

        if (bossCurHP <= 0) // 현재 체력이 떨어지면...
        {
            BossDirectControl(); // 정화 연출 작업

            if (isPurification)
            {
                StartCoroutine(DieDelay());// 정화 연출 딜레이 걸기(여기 안에 반환 함수 들어가있음), 승리 UI를 위한 여부도 포함
            }

            PlayerStatus.instance.GetGold(); // 골드 획득(플레이어가 죽이면 골드 얻게 함)
            PlayerStatus.instance.GetEXP(); // 경험치 획득(플레이어가 죽이면 경험치 얻게 함)
        }

    }

    private void Hit_WaterSlash() // 최종 데미지 피격 함수
    {
        if (bossCurHP > 0)
        {


            //Debug.Log("몬스터가 피격 받기전 체력입니다 - 물 기본공격  " + bossCurHP);

            bossCurHP -= BulletManager.instance.WaterSlashTypeDamage(WeaponManager.instance.waterSlash_CurLevelIndex);

            //slowEffect = BulletScript.instance.WaterSlashType_SlowEffect(7); //슬로우

            //Debug.Log("몬스터가 피격을 받았습니다. - 물 기본공격  " + bossCurHP);

            StartCoroutine(BossHitEffect());
        }

        if (bossCurHP <= 0) // 현재 체력이 떨어지면...
        {
            BossDirectControl(); // 정화 연출 작업

            if (isPurification)
            {
                StartCoroutine(DieDelay());// 정화 연출 딜레이 걸기(여기 안에 반환 함수 들어가있음), 승리 UI를 위한 여부도 포함
            }

            PlayerStatus.instance.GetGold(); // 골드 획득(플레이어가 죽이면 골드 얻게 함)
            PlayerStatus.instance.GetEXP(); // 경험치 획득(플레이어가 죽이면 경험치 얻게 함)
        }

    }

    private void Hit_FireSlash() // 최종 데미지 피격 함수
    {
        if (bossCurHP > 0)
        {


            //Debug.Log("몬스터가 피격 받기전 체력입니다 - 불 기본공격  " + bossCurHP);

            bossCurHP -= BulletManager.instance.FireSlashTypeDamage(WeaponManager.instance.fireSlash_CurLevelIndex);

            //Debug.Log("몬스터가 피격을 받았습니다.- 불 기본공격  " + bossCurHP);

            //BulletManager.instance.FireSlash_SpreadDamage(14, transform.position, 1f);

            StartCoroutine(BossHitEffect());
        }

        if (bossCurHP <= 0) // 현재 체력이 떨어지면...
        {
            BossDirectControl(); // 정화 연출 작업

            if (isPurification)
            {
                StartCoroutine(DieDelay());// 정화 연출 딜레이 걸기(여기 안에 반환 함수 들어가있음), 승리 UI를 위한 여부도 포함
            }

            PlayerStatus.instance.GetGold(); // 골드 획득(플레이어가 죽이면 골드 얻게 함)
            PlayerStatus.instance.GetEXP(); // 경험치 획득(플레이어가 죽이면 경험치 얻게 함)
        }

    }

    #endregion

    #region 서브 스킬 3가지 묶음(개미친짓)

    private void Hit_WindDrill()
    {
        if (bossCurHP > 0)
        {
            //Debug.Log("드릴 횟수 확인"+SubSkillScript.instance.WindDrillType_Count(1));

            //Debug.Log("몬스터가 피격 받기전 체력입니다 - 바람 서브스킬  " + bossCurHP);

            for (int i = 0; i < SubSkillManager.instance.WindDrillType_Count(WeaponManager.instance.windDrill_CurLevelIndex); i++) // 타격 횟수
            {
                //Debug.Log("단타 확인");
                bossCurHP -= SubSkillManager.instance.WindDrillType_Damage(WeaponManager.instance.windDrill_CurLevelIndex);
            }


            //Debug.Log("몬스터가 피격을 받았습니다.- 바람 서브스킬  " + bossCurHP);

            StartCoroutine(BossHitEffect());
        }

        if (bossCurHP <= 0) // 현재 체력이 떨어지면...
        {
            BossDirectControl(); // 정화 연출 작업

            if (isPurification)
            {
                StartCoroutine(DieDelay());// 정화 연출 딜레이 걸기(여기 안에 반환 함수 들어가있음), 승리 UI를 위한 여부도 포함
            }

            PlayerStatus.instance.GetGold(); // 골드 획득(플레이어가 죽이면 골드 얻게 함)
            PlayerStatus.instance.GetEXP(); // 경험치 획득(플레이어가 죽이면 경험치 얻게 함)
        }
    }

    private void Hit_WaterBarrier()
    {

    }

    private void Hit_FireBall()
    {
        if (bossCurHP > 0)
        {


            //Debug.Log("몬스터가 피격 받기전 체력입니다 - 불 서브 스킬  " + bossCurHP);

            bossCurHP -= SubSkillManager.instance.FireBallType_PenetDamage(WeaponManager.instance.fireBall_CurLevelIndex);

            //Debug.Log("몬스터가 피격을 받았습니다.- 불 서브 스킬  " + bossCurHP);

            StartCoroutine(BossHitEffect());
        }

        if (bossCurHP <= 0) // 현재 체력이 떨어지면...
        {
            BossDirectControl(); // 정화 연출 작업

            if (isPurification)
            {
                StartCoroutine(DieDelay());// 정화 연출 딜레이 걸기(여기 안에 반환 함수 들어가있음), 승리 UI를 위한 여부도 포함
            }

            PlayerStatus.instance.GetGold(); // 골드 획득(플레이어가 죽이면 골드 얻게 함)
            PlayerStatus.instance.GetEXP(); // 경험치 획득(플레이어가 죽이면 경험치 얻게 함)
        }
    }

    #endregion



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            bossCurHP -= 1f; //todo : 보스 공격의 데미지 스크립트를 따로 제작하기
            //Debug.Log("보스 현재 체력 : " + bossCurHP);

            if (bossCurHP > 0f) // 히트 이펙트
            {
               
                //Debug.Log("보스가 피격당했습니다");
                StartCoroutine(BossHitEffect());
            }

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


        ///////////////////////////////////////////

        if (other.gameObject.CompareTag("WindSlash"))
        {
            Hit_WindSlash();
        }

        if (other.gameObject.CompareTag("WaterSlash"))
        {
            Hit_WaterSlash();
        }

        if (other.gameObject.CompareTag("FireSlash"))
        {
            Hit_FireSlash();
        }

        if (other.gameObject.CompareTag("WindDrill"))
        {

            Hit_WindDrill();
        }

        if (other.gameObject.CompareTag("WaterBarrier"))
        {
            Hit_WaterBarrier();
        }

        if (other.gameObject.CompareTag("FireBall"))
        {

            Hit_FireBall();
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

    [HideInInspector]
    public bool isTimeToReturn; // 보스가 사망하는 시점 모든 기믹 몬스터 패턴등 반환하는 작업들어가는 용도

    IEnumerator DieDelay() // 해당 코루틴에는 반환 작업과 승리 UI작업이 들어가 있을거임
    {
        isTimeToReturn = true; // 보스가 죽을때 바로 반환시키게끔 하는 변수

        yield return YieldInstuctionCash.WaitForSeconds(7f);

        //todo: 여기다가 승리 UI실행하기
        Debug.Log("승리했습니다");
        isBossDie = true; // 찐 죽음
        
        yield return YieldInstuctionCash.WaitForSeconds(5f);

        isBossDie = false;

        yield return YieldInstuctionCash.WaitForSeconds(4.5f); // 여기서 다른 마을 맵 이어지는 구간을 좁힐 수 있는 시간을 설정가능


        Debug.Log("보스 스크립트에서 반환 작업 시작");
        OnTargetReached(); //반환

        StopCoroutine(DieDelay());
    }

    IEnumerator PatternEffectDelay() // 윈드 보스 패턴 오브젝트 비활성화 작업
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
            else if (isPurification) // 정화작업들어가면 비활성화
            {
                pattern03_Effect.gameObject.SetActive(false);

                break;
            }

            yield return YieldInstuctionCash.WaitForSeconds(0.5f);// 반복문 딜레이
        }

        StopCoroutine(PatternEffectDelay());
    }

    IEnumerator BossHitEffect()
    {
        // 메테리얼 파라미터 값 접근하는 것이 쉽지 않아서 애니메이션으로 처리하기로 결정
        //bossMaterial.HasProperty("Hit_On-Off"); // 해당 프로퍼티가 있는지 확인하는 코드임

        //bossMaterial.IsKeywordEnabled("Hit_On-Off"); // 해당 프로퍼티가 활성화가 되어있는지 확인하는 코드

        //bossMaterial.EnableKeyword("Hit_On-Off");

        //bossMaterial.SetShaderPassEnabled("Hit_On-Off", true);

        // 렌더러로 접근한다
        //rend.material.SetFloat("Hit_Effect_On-Off", 1.0f); // 메테리얼 파라미터 값에는 bool값 파라미터가 존재하지 않는다 그렇기에 SetInt로 1(true), 0(false)로 표현

        bossAnim.SetBool("isHit", true);

        yield return YieldInstuctionCash.WaitForSeconds(0.01f);

        bossAnim.SetBool("isHit", false);

        //rend.material.SetFloat("Hit_Effect_On-Off", 0.0f);

        //bossMaterial.DisableKeyword("Hit_On-Off");

        StopCoroutine(BossHitEffect());
    }

}
