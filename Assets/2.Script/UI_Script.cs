using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Script : MonoBehaviour
{
    public static UI_Script instance;

    [SerializeField]
    private GameObject stageUI;

    [SerializeField]
    private GameObject warningUI;

    [SerializeField]
    private DB_Status statusDB; // 에셋화 되어있는 데이터테이블 가져오기

    private void Awake()
    {
        StartCoroutine(StageUI());

        if (UI_Script.instance == null)
        {
            instance = this;
        }

    }

    private void Update()
    {
        if (!BossManager.instance.bossSpawnActive)
        {
            StageProgress();
        }

        if (BossManager.instance.bossAppearanceTime < bossProgress && bossProgress < BossManager.instance.bossAppearanceTime + 1f) // 해당 코루틴에 if문을 넣는 것으로 바꿀것
        {
            //todo : 보스가 나타날 시점
            Warnnig();
        }

        if (!BossManager.instance.bossSpawnActive)
        {
            bossHPBar.gameObject.SetActive(false);
        }
        else if (BossManager.instance.bossSpawnActive)
        {
            BossHPUI();
        }

        PausePopup();

        CurrentPlayerHP_UI(); // 현재 캐릭터의 hp UI 함수

        CurrentPlayerGold_UI(); // 현재 캐릭터의 골드 UI 함수

        CurrentPlayerEXP_UI(); // 현재 캐릭터의 경험치 UI 함수

        CharImformPopup();

        VictoryUI();

        DefeatUI(); // 주의 : 코루틴 걸려있음(브레이킹 해둠)

        Status_UI();
    }

    [SerializeField]
    private GameObject stageProgressbar; // 스테이지 진해도 바

    [SerializeField]
    private Image stageProgressFill; // 스테이지 진행도 fill이미지

    private float bossProgress;

    // 스테이지 진척도
    private void StageProgress()
    {
        stageProgressbar.gameObject.SetActive(true);

        bossProgress = BossManager.instance.curTime; // 보스매니저 스크립트에서 time변수를 가져옴

        stageProgressFill.fillAmount = bossProgress / BossManager.instance.bossAppearanceTime; //bossProgress: 현 시간 ,bossAppearanceTime: 보스 등장 시간

        if (BossManager.instance.bossAppearanceTime < bossProgress)
        {
            stageProgressbar.gameObject.SetActive(false);
        }

    }

    private void Warnnig() // 경고 문구
    {

        //Debug.Log("경고 문구 생성");
        StartCoroutine(BossWarning());
    }

    [SerializeField]
    private GameObject victoryUI;

    private void VictoryUI() // 승리 UI
    {
        if (BossManager.instance.bossSpawnActive)
        {
            if (BossScript.instance.isVictory)
            {
                victoryUI.gameObject.SetActive(true);
            }
            else
            {
                victoryUI.gameObject.SetActive(false);
            }
        }
        
    }

    [SerializeField]
    private GameObject defeatUI;

    private void DefeatUI() // 패배 UI
    {
        if (PlayerStatus.instance.isPlayerDie) // 코루틴 업데이트 상태(브레이킹검)
        {
            PlayerStatus.instance.isPlayerDie = false;
            //Debug.Log("패배 UI");
            defeatUI.gameObject.SetActive(true);

            if (!isGameOver)
            {
                StartCoroutine(DefeatDelay());
            }
            
        }
        
        
    }

    private bool pauseActive = false; // 일시정지 상태 여부

    //일시정지 버튼
    public void PauseBtn() // 재시작 버튼도 겸하고있음
    {

        if (pauseActive)
        {
            Time.timeScale = 1f;
            pauseActive = false;
        }
        else
        {
            Time.timeScale = 0f;
            pauseActive = true;
        }

    }

    [SerializeField]
    private GameObject pausePopup;

    private void PausePopup()
    {
        if (pauseActive)
        {
            pausePopup.gameObject.SetActive(true);
        }
        else if (!pauseActive)
        {
            pausePopup.gameObject.SetActive(false);
        }
    }

    public void GameOutBtn() // 일시정지 팝업에서 게임 나가는 버튼
    {
        isGameOver = true; // 여기 bool타입 선정은 메인 화면으로 나가기 위해 사용됨
        Time.timeScale = 1f;
        pausePopup.gameObject.SetActive(false);
    }

    [SerializeField]
    public GameObject bossHPBar;

    [SerializeField]
    private Image bossHPBarFill;

    private void BossHPUI()
    {
        bossHPBar.gameObject.SetActive(true);

        bossHPBarFill.fillAmount = BossScript.instance.bossCurHP / BossScript.instance.bossMaxHP;

        if (BossScript.instance.bossCurHP < 0f)
        {
            bossHPBar.gameObject.SetActive(false);
        }
    }

    [SerializeField]
    private TextMeshProUGUI playerHpUI;    

    //이부분 정리 - 여기서부터 반응형 UI
    //private int currentHP = PlayerStatus.instance.maxHP;

    private void CurrentPlayerHP_UI()
    {
        int currentHP = PlayerStatus.instance.currHP;
        playerHpUI.text = currentHP.ToString(); // 해당 텍스트의 자료에 .text를 접근하고 해당 정보를 수치  => 글자(ToString())으로 변환 기입을 하면된다.
    }

    // 획득 골드량(나중에 추가 골드 획득량을 조절할 수 도 있어서 분리함)
    [SerializeField]
    private TextMeshProUGUI playerGold;

    private void CurrentPlayerGold_UI()
    {
        int currentGold = PlayerStatus.instance.playerGetGold;
        playerGold.text = currentGold.ToString();
    }

    [SerializeField]
    private TextMeshProUGUI playerLevelText;

    [SerializeField]
    private Image expFill; // 경험치 채워지는 바 이미지

    [HideInInspector]
    public int playerLevel = 1;

    private int expMaxFill; // 이부분 다시 생각해보기
   
    [HideInInspector]
    public bool isLevelUp;

    [SerializeField]
    private GameObject popupAttribute; //popupAttribute는 레벨업했을 때 생성되는 팝업

    private void CurrentPlayerEXP_UI()
    {
        expMaxFill = PlayerStatus.instance.MaxEXP(playerLevel);
        expFill.fillAmount = PlayerStatus.instance.playerAddEXP / expMaxFill;

        //Debug.Log("AddEXP : " + PlayerStatus.instance.playerAddEXP);
        
        if (expFill.fillAmount == 1)
        {
            isLevelUp = true;

            PlayerStatus.instance.playerAddEXP = 0; // 레벨업하면 현재값 초기화
            //Debug.Log("AddEXP : " + PlayerStatus.instance.playerAddEXP);
            Debug.Log("레벨업 했습니다.");
            if (isLevelUp)
            {
                GetSkillPopup();

                isLevelUp = false;

                expFill.fillAmount = 0;

                playerLevel++;

                //expMaxFill = PlayerStatus.instance.MaxEXP(playerLevel);
                Debug.Log("플레이어 레벨 : " + playerLevel);
                //expMaxFill = 20; // 경험치 최대치를 엑셀 데이터 테이블로 바꿀 것

                playerLevelText.text = playerLevel.ToString(); // 현재 플레이어의 레벨
            }
                                               
        }
    }

    [SerializeField]
    private GameObject charImformPopup;

    private bool charImformActive;

    public void CharImformBtn() // 캐릭터 정보창 버튼, 나가는 버튼 포함
    {
        if (!charImformActive)
        {
            charImformActive = true;
            Time.timeScale = 0f;
        }
        else if (charImformActive)
        {
            charImformActive = false;
            Time.timeScale = 1f;
        }
    }

    private void CharImformPopup() // 캐릭터 정보창
    {
        if (charImformActive)
        {
            charImformPopup.gameObject.SetActive(true);
        }
        else if (!charImformActive)
        {
            charImformPopup.gameObject.SetActive(false);
        }
    }

    private bool isGetSkill;

    public void GetSkillBtn()
    {
        isGetSkill = true;
        Time.timeScale = 1f;

        if (isGetSkill) // 스킬을 얻었다면...
        {
            popupAttribute.gameObject.SetActive(false); // 해당 스킬 얻는 팝업 끄기
        }
    }

    //3개를 분리?
    public void GetSkillBtn02()
    {

    }
    public void GetSkillBtn03()
    {

    }
    private void GetSkillPopup() // 레벨업 후 스킬 획득 팝업관련
    {
        isGetSkill = false;
        popupAttribute.gameObject.SetActive(true); // 레벨업 하면 스킬 얻는 팝업창 띄우기

        Time.timeScale = 0f;
                
    }

    [HideInInspector]
    public bool isSubSkillBtn01; // 서브 스킬 버튼 01

    [HideInInspector]
    public bool isSubSkillBtn02; // 서브 스킬 버튼 02

    //화면 UI 서브 스킬 버튼1
    public void SubSkillBtn01()
    {
        isSubSkillBtn01 = true;


    }

    //화면 UI 서브 스킬 버튼2
    public void SubSkillBtn02()
    {
        isSubSkillBtn02 = true;

    }

    [SerializeField]
    private TextMeshProUGUI status_Level; //플레이어 상태창 UI - 레벨

    [SerializeField]
    private TextMeshProUGUI status_HP;//플레이어 상태창 UI - 체력

    [SerializeField]
    private TextMeshProUGUI status_ATK; //플레이어 상태창 UI - 공격력

    [SerializeField]
    private TextMeshProUGUI status_AttackSpeed; //플레이어 상태창 UI - 공격속도

    [SerializeField]
    private TextMeshProUGUI status_CoolDownTime; //플레이어 상태창 UI - 쿨타임 감소

    [SerializeField]
    private TextMeshProUGUI status_AddEXP; // 플레이어 상태창 UI - 추가 경험치 능력

    private void Status_UI() // 플레이어 상태창 UI
    {
        status_Level.text = playerLevel.ToString();
                
        int currentHP = PlayerStatus.instance.currHP;

        status_HP.text = currentHP.ToString();

        // 밑에 부분은 인덱스 0으로 처리 되어있는 것을 모두 실시간 연동시키게 해야함
        // 플레이어의 현재 공격력
        float currentATK = statusDB.PlayerStatus[0].playerDamage;

        status_ATK.text = currentATK.ToString();

        // 플레이어의 현재 공격속도
        float currentAttackRate = statusDB.PlayerStatus[0].attackRate;

        status_AttackSpeed.text = currentAttackRate.ToString();

        // 플레이어의 현재 쿨타임감소
        float currentCoolDownTime = statusDB.PlayerStatus[0].coolDownTime;

        status_CoolDownTime.text = currentCoolDownTime.ToString();

        // 플레이어의 현재 추가 경험치
        float currentAddEXP = statusDB.PlayerStatus[0].addEXP;

        status_AddEXP.text = currentAddEXP.ToString();

    }

    //YieldInstuctionCash 미리 캐싱해둔것
    IEnumerator StageUI() // 애니메이션 효과 시간 맞추기
    {
        yield return YieldInstuctionCash.WaitForSeconds(1.5f);
        stageUI.gameObject.SetActive(true);
        yield return YieldInstuctionCash.WaitForSeconds(2f); // 나중에 애니메이션 루프 조정하기
        stageUI.gameObject.SetActive(false);
        StopCoroutine(StageUI());
    }

    IEnumerator BossWarning()
    {
        warningUI.gameObject.SetActive(true);
        yield return YieldInstuctionCash.WaitForSeconds(2.0f); // 경고 UI 시간조율
        warningUI.gameObject.SetActive(false); // 페이드 및 씬전환 적용해보고 위치 조정

        //페이드 아웃과 씬전환을 넣을 것

        StopCoroutine(BossWarning());
    }

    [HideInInspector]
    public bool isGameOver; // 게임 오버 처리(ChangeSceneManager에서 메인씬으로 넘어가게 해줌)

    IEnumerator DefeatDelay()  // 패배UI나오기 걸리는 딜레이
    {

        yield return YieldInstuctionCash.WaitForSeconds(5f); // 패배UI나오기 걸리는 딜레이

        //isPlayerDie = false;
        isGameOver = true;
        yield return YieldInstuctionCash.WaitForSeconds(0.1f);

        StopCoroutine(DefeatDelay());
    }

}
