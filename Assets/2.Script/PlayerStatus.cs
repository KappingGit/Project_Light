using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    // 플레이어 스테이터스 관련

    // 싱글톤
    public static PlayerStatus instance;

    private int index; // 인덱스 넘버(일단 만들어둔것)

    [SerializeField]
    private DB_Status statusDB; // 에셋화 되어있는 데이터테이블 가져오기

    // 참고: statusDB.시트이름.(여기에 뭐가 들어가지?? 영상에서는 Count가 들어간다);
    // statusDB.시트 이름.playerHP; => 풀이 : 데이터테이블 에셋화 변수.에셋화 변수 시트이름.각 직렬화 되어있는 클래스의 변수

    private void Awake()
    {
        if (PlayerStatus.instance == null)
        {
            instance = this;
        }

        // 초기 스테이터스 설정할때 Awake에 기입(나중에 Init으로 묶어서 정리)
        MaxHP();
        AttackDamage();
        AttackRate();
        CoolDownTime();
        
        playerGetGold = 0; // 골드 0으로 초기화
        isPlayerDie = false;
    }

    private void Update()
    {               
        // 이부분 나중에 수정
        if (UI_Script.instance.isLevelUp)
        {
            Debug.Log("레벨업했습니다.");
        }
        //Debug.Log("현재hp테스트 : " + currHP);
        //Debug.Log("hp테스트 : " + maxHP+ "    공격력 테스트 : " + playerATK +"    추가 경험치 테스트 :"+playerAddEXP+ "    공속 테스트 : " + playerAR + "     쿨타임 테스트 : " + playerCDT);

        if (currHP <= 0) // 만약 0이 되자마자 죽게할거면 upDate문에 bool값 브레이킹을 걸고 넣을것..
        {
            if (!isPlayerDie)
            {
                
                isPlayerDie = true; // 플레이어 사망 상태
                //Debug.Log("플레이어가 죽었습니다.");
                
                StartCoroutine(PlayerDieEffect());
                                
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // 일반 몬스터 피격시 피 1깍임
            Hit();
        }

        if (other.gameObject.CompareTag("Missile")) // 기믹 또는 보스패턴 처리
        {
            Hit();
        }

        //todo: 보스 패턴에 피격되면(태그로 취급하기)
    }

    [HideInInspector]
    public int maxHP;

    // 플레이어 MaxHP
    private void MaxHP()
    {
        // 캐릭터 hp최대치
        maxHP = statusDB.PlayerStatus[0].playerHP; // 시트에서 몇번째 줄의 playerHP 데이터를 가져올지 모르니까 배열로 인덱스 값을 넣어준다

        currHP = maxHP; // 기본 최대치 hp에 맞게 초기화
    }

    [HideInInspector]
    public int currHP; // 현재HP 간섭 변수

    [HideInInspector]
    public bool isPlayerDie;

    // 플레이어 피격 함수
    public void Hit()
    {
        if (currHP > 0)
        {
            //currHP -= 1;
            StartCoroutine(Invincible()); // 일시 무적처리하기 위해 코루틴 안에서 피격 수치 들어가있음

            // 일반 몬스터의 피격은 -1
            //Debug.Log("피격 당했습니다. " + currHP);
        }
        
    }

    private float playerATK;

    // 플레이어 공격력
    private void AttackDamage()
    {
        playerATK = statusDB.PlayerStatus[0].playerDamage; // 플레이어 공격력
    }

    //AR : 어택레이트
    private float playerAR;

    // 플레이어 공격속도
    private void AttackRate()
    {
        playerAR = statusDB.PlayerStatus[0].attackRate;
    }

    //CDT : 쿨다운타임
    private float playerCDT;

    // 플레이어 쿨타임 감소
    private void CoolDownTime()
    {
        playerCDT = statusDB.PlayerStatus[0].coolDownTime;
    }

    

   
    public int MaxEXP(int index)
    {
        int maxEXP = statusDB.PlayerStatus[index].maxEXP;

        return maxEXP;
    }

    // 플레이어 추가 경험치 획득
    [HideInInspector]
    public float playerAddEXP;

    public void GetEXP()
    {
        int plusAddExp_Level = statusDB.PlayerStatus[0].indexLevel01; // 추가 경험치 구매, 업글 단계

        float plusAddExp = statusDB.PlayerStatus[plusAddExp_Level].addEXP; // 추가 경험치 수치

        //추가 경험치 기능 예) 최종 얻는 경험치 = 5 + (추가경험치 수식)
        float getEXP = statusDB.MonsterStatus[0].monsterEXP
            + (statusDB.MonsterStatus[0].monsterEXP * plusAddExp); //데이터 테이블 수식 적을때 유의
                
        playerAddEXP += getEXP;

        //Debug.Log("playerAddEXP : " + playerAddEXP);
    }

    private int playerLevel = 1; // PlayerStatus스크립트 부분

    // 플레이어 레벨
    private void PlayerLevel()
    {
        //추가 경험치 기능 예) 최종 얻는 경험치 = 5 + (추가경험치 수식)
        float getEXP = statusDB.PlayerStatus[playerLevel].addEXP;

        //float maxEXP = 테이블데이터 접근 statusDB.PlayerStatus[playerLevel].addEXP;

        playerLevel++;
        Debug.Log("플레이어 레벨 : " + playerLevel);
    }

    // 획득 골드량(나중에 추가 골드 획득량을 조절할 수 도 있어서 분리함)
    // 플레이어 골드 획득
    [HideInInspector]
    public int playerGetGold;

    public void GetGold()
    {
        int getGold = statusDB.MonsterStatus[0].monsterGold;
        playerGetGold += getGold; // 해당 숫자에 골드 획득량 변수를 집어넣으면된다.
    }

    //------------------------------------------------------------------------------------------------------------------------
    //###############################↓↓↓↓↓↓유동성 데이터 함수 영역↓↓↓↓↓↓###############################-----------
    //------------------------------------------------------------------------------------------------------------------------

    #region 전반적인 코드분석 및 설계

    // 해당 영역은 실시간으로 플레이어의 스테이터스와 가지고 있는 스킬의 레벨, 타입 등등
    // 데이터 테이블로 연동되는 허브 시스템으로 영역을 분리했다(가독성을 위해 분리)

    //대략적인 설계 방식으로...
    /*
        nomalAttackUID가 1이면 바람기본 공격 1레벨, 2이면 바람 기본공격 2레벨
        3이면 바람 기본공격 3레벨의 뜻이며
        만약 값이 6이면 물 기본 공격 0레벨(미획득)이고 값이 7이면 물 기본 공격 1레벨이라는 형식

        위 공식을 아래로 정리해서 나열하면

        nomalAttackUID = 0 => 바람 기본 공격 0레벨(미획득)
        nomalAttackUID = 1 => 바람 기본 공격 1레벨(획득)
        nomalAttackUID = 2 => 바람 기본 공격 2레벨(획득)
        nomalAttackUID = 3 => 바람 기본 공격 3레벨(획득)
        nomalAttackUID = 4 => 바람 기본 공격 4레벨(획득)
        nomalAttackUID = 5 => 바람 기본 공격 5레벨(획득)
        nomalAttackUID = 6 => 바람 기본 공격 0레벨(미획득)
        nomalAttackUID = 7 => 바람 기본 공격 1레벨(획득)

        이런 형태이다
     */

    #endregion





    //------------------------------------------------------------------------------------------------------------------------
    //###############################↑↑↑↑↑↑유동성 데이터 함수 영역↑↑↑↑↑↑↑###############################---------
    //------------------------------------------------------------------------------------------------------------------------

    // 피격시스템에 대한 방향 두가지(고려사항 적어두기):
    // 1. 게임매니저 스크립트에서 처리 => 만약 몬스터와 플레이어가 부딪혔을때 피가 까이며, 보스패턴의 경우 각 패턴별 태그를 붙여서 피격 관리...
    // 2. 각 몬스터, 보스 패턴에서 처리 => 몬스터에서 플레이어와 부딪히면 플레이어의 currHp에 간섭하게 하는 방법

    //1번 방향으로 제작해보기

    private bool isInvincible = false; // 일시 무적상태라면...

    IEnumerator Invincible() // 일시 무적 처리
    {
        if (!isInvincible)
        {
            currHP -= statusDB.MonsterStatus[0].monsterDamage;
            //Debug.Log(statusDB.MonsterStatus[0].monsterDamage); //몬스터 데미지 측정
            Debug.Log("무적상태입니다.");
            isInvincible = true;
            yield return YieldInstuctionCash.WaitForSeconds(2f);
            Debug.Log("무적상태가 끝났습니다.");
            isInvincible = false;
        }
               
        StopCoroutine(Invincible());
    }
        
    [SerializeField]
    private GameObject playerDieEffect;

    IEnumerator PlayerDieEffect() // Instantiate화로 작업함
    {
        
        playerDieEffect.gameObject.SetActive(true);

        Instantiate(playerDieEffect,new Vector3 (transform.position.x, 1f, transform.position.z), Quaternion.identity);

        gameObject.SetActive(false);

        yield return YieldInstuctionCash.WaitForSeconds(2f);

        Destroy(playerDieEffect);
                
        StopCoroutine(PlayerDieEffect());
    }

}
