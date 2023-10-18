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
        //GetEXP();

        playerGetGold = 0; // 골드 0으로 초기화

    }

    private void Update()
    {
        //Debug.Log("현재hp테스트 : " + currHP);
        //Debug.Log("hp테스트 : " + maxHP+ "    공격력 테스트 : " + playerATK +"    추가 경험치 테스트 :"+playerAddEXP+ "    공속 테스트 : " + playerAR + "     쿨타임 테스트 : " + playerCDT);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // 일반 몬스터 피격시 피 1깍임
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

    // 플레이어 피격 함수
    public void Hit()
    {
        if (currHP > 0)
        {
            currHP -= 1; // 일반 몬스터의 피격은 -1
            Debug.Log("피격 당했습니다. " + currHP);
        }
        else if (currHP <= 0)
        {
            Debug.Log("플레이어가 죽었습니다.");
        }
    }

    private float playerATK;

    // 플레이어 공격력
    private void AttackDamage()
    {
        playerATK = statusDB.PlayerStatus[0].attackDamage;
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

    // 플레이어 추가 경험치 획득
    [HideInInspector]
    public float playerAddEXP;

    public void GetEXP()
    {
        float getEXP = statusDB.PlayerStatus[0].addEXP;
        playerAddEXP += getEXP;
    }

    // 플레이어 레벨
    private void PlayerLevel()
    {

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

    // 피격시스템에 대한 방향 두가지(고려사항 적어두기):
    // 1. 게임매니저 스크립트에서 처리 => 만약 몬스터와 플레이어가 부딪혔을때 피가 까이며, 보스패턴의 경우 각 패턴별 태그를 붙여서 피격 관리...
    // 2. 각 몬스터, 보스 패턴에서 처리 => 몬스터에서 플레이어와 부딪히면 플레이어의 currHp에 간섭하게 하는 방법

    //1번 방향으로 제작해보기



}
