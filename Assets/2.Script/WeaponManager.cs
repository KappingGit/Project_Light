using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class WeaponManager : MonoBehaviour
{

    [SerializeField]
    private DB_Status statusDB;

    public static WeaponManager instance;

    private int selectWeapon_NomalAttack;

    private int selectWeapon_SubSkill;

    // 서브 스킬 배치장소
    [HideInInspector]
    public bool isLeft;

    [HideInInspector]
    public bool isRight;

    private void Awake()
    {
        if (WeaponManager.instance == null)
        {
            instance = this;
        }

        isLeft = false;

        isRight = false;

        windDrillSlot.gameObject.SetActive(false);
        waterBarrierSlot.gameObject.SetActive(false);
        fireBallSlot.gameObject.SetActive(false);

        //RandomSkillData(); // 테스트를 위해 awake

        SkillLevelIndex_Initialization();

    }

    private void Update()
    {
        //테스트
        //Sellect_WindSlash();
        //Sellect_WaterSlash();
        //Sellect_FireSlash();

        skillSolt_Level01.text = "Lv " + windDrill_CurTypeLevel; // 슬롯 순서가 꼬여있음...
        skillSolt_Level02.text = "Lv " + waterBarrier_CurTypeLevel; // 슬롯 순서가 꼬여있음...
        skillSolt_Level03.text = "Lv " + fireBall_CurTypeLevel; // 슬롯 순서가 꼬여있음...
        skillSolt_Level04.text = "Lv " + windSlash_CurTypeLevel; // 슬롯 순서가 꼬여있음...
        skillSolt_Level05.text = "Lv " + waterSlash_CurTypeLevel; // 슬롯 순서가 꼬여있음...
        skillSolt_Level06.text = "Lv " + fireSlash_CurTypeLevel; // 슬롯 순서가 꼬여있음...
    }


    private int nomalAttack_UID; // 가지고 있는 기본공격 저장하는 용도

    private int subSkill_UID;

    private int mainSkill_UID;

    private int passive_UID;

    private void WeaponDataSaveArchive() // 데이터를 모두저장
    {
        BulletManager.instance.WindSlashTypeDamage(1);




    }

    // 레벨업시 스킬 획득
    private void SelectWeapon()
    {

    }


    #region Get Set 프로퍼티 활용 코드 (비효율적임 그저 공부용)

    // Get Set 프로퍼티의 오류 사항 해당 프로퍼티를 ()를 사용해서 함수로 만들면 안된다...

    //public bool ChangeSkillPos_Left
    //{
    //    get{
    //        return isLeft;
    //    }

    //    set{
    //        isLeft = true;
    //    }
    //}

    //public bool ChangeSkillPos_Right
    //{
    //    get
    //    {
    //        return isRight;
    //    }

    //    set
    //    {
    //        isRight = true;

    //    }

    //}

    #endregion


    // 교체할 버튼을 눌렀을 때 버튼 활성화 이펙트 오브젝트
    [SerializeField]
    private GameObject sellectEffectBtn_NomalAttack;

    [SerializeField]
    private GameObject sellectEffectBtn_SubSkill01; // 왼쪽 서브 스킬

    [SerializeField]
    private GameObject sellectEffectBtn_SubSkill02; // 왼쪽 서브 스킬

    [SerializeField]
    private Image selectBtn_NomalAttackImg; // 교체할 위치의 이미지 교체용(일반 공격은 하나밖에 없지만 이렇게 설명해둠)

    // 밑에 있는 해당 함수는 야매로 만든 스킬 장착 함수이다 나중에 수정할 것

    // 스킬 슬롯 오브젝트
    [SerializeField]
    private GameObject windDrillSlot;

    [SerializeField]
    private GameObject waterBarrierSlot;

    [SerializeField]
    private GameObject fireBallSlot;

    [SerializeField]
    private GameObject windSlashSlot;

    [SerializeField]
    private GameObject warterSlashSlot;

    [SerializeField]
    private GameObject fireSlashSlot;

    // 서브 스킬
    [SerializeField]
    private GameObject yameInstall01_WindDrill;

    [SerializeField]
    private GameObject yameInstall02_WarterBarrier;
    
    [SerializeField]
    private GameObject yameInstall03_FireBall;


    // 일반 스킬
    [SerializeField]
    private GameObject yameInstall04_WindSlash;

    [SerializeField]
    private GameObject yameInstall05_WaterSlash;

    [SerializeField]
    private GameObject yameInstall06_FireSlash;

    // 인게임안에서 스킬창
    [SerializeField]
    private GameObject yameInstall_GrimLeft_01;

    [SerializeField]
    private GameObject yameInstall_GrimLeft_02;

    [SerializeField]
    private GameObject yameInstall_GrimLeft_03;

    [SerializeField]
    private GameObject yameInstall_GrimRight_01;

    [SerializeField]
    private GameObject yameInstall_GrimRight_02;

    [SerializeField]
    private GameObject yameInstall_GrimRight_03;


    //스테이터스 서브스킬 왼쪽 오른쪽 위치

    // 인게임안에서 스킬창
    [SerializeField]
    private GameObject yameInstall_StatusLeft_01; // 왼쪽 윈드드릴

    [SerializeField]
    private GameObject yameInstall_StatusLeft_02;

    [SerializeField]
    private GameObject yameInstall_StatusLeft_03;

    [SerializeField]
    private GameObject yameInstall_StatusRight_01; // 오른쪽 윈드드릴

    [SerializeField]
    private GameObject yameInstall_StatusRight_02;

    [SerializeField]
    private GameObject yameInstall_StatusRight_03;


    // 장착 여부 팝업 관련
    [SerializeField]
    private GameObject installPopup;

    [SerializeField]
    private TextMeshProUGUI installSkillName; // 장착 여부 스킬이름

    [SerializeField]
    private Image installSkillImg; // 장착 여부 스킬 이미지

    [SerializeField]
    private TextMeshProUGUI installSkillLevel; // 장착여부 스킬 레벨

    [SerializeField]
    private TextMeshProUGUI installSkillExplanation; // 장착 여부 스킬 설명

    [SerializeField]
    private TextMeshProUGUI installSkillProperty; // 장착 여부 소유하고 있는 상태 (액티브 이냐 패시브이냐)

    [SerializeField]
    private TextMeshProUGUI skillSolt_Level01;

    [SerializeField]
    private TextMeshProUGUI skillSolt_Level02;

    [SerializeField]
    private TextMeshProUGUI skillSolt_Level03;

    [SerializeField]
    private TextMeshProUGUI skillSolt_Level04;

    [SerializeField]
    private TextMeshProUGUI skillSolt_Level05;

    [SerializeField]
    private TextMeshProUGUI skillSolt_Level06;

    [SerializeField]
    private Sprite windSlashImg;

    [SerializeField]
    private Sprite waterSlashImg;

    [SerializeField]
    private Sprite fireSlashImg;

    [SerializeField]
    private Sprite windDrillImg;

    [SerializeField]
    private Sprite waterBarrierImg;

    [SerializeField]
    private Sprite fireBallImg;

    [SerializeField]
    private GameObject decisionBtn_WindSlash; // 결정한 스킬 변경 버튼

    [SerializeField]
    private GameObject decisionBtn_WaterSlash;

    [SerializeField]
    private GameObject decisionBtn_FireSlash;

    [SerializeField]
    private GameObject decisionBtn_WindDrill;

    [SerializeField]
    private GameObject decisionBtn_WaterBarrier;

    [SerializeField]
    private GameObject decisionBtn_FireBall;


    private bool nomalAttackClick;

    private bool subSkill01Click;

    private bool subSkill02Click;

    // 버튼의 값 기입
    public void ChageWeapon_NA() // 일반공격 버튼 위치 클릭
    {
        nomalAttackClick = true;

        subSkill01Click = false;

        subSkill02Click = false;

        sellectEffectBtn_NomalAttack.gameObject.SetActive(true);
        sellectEffectBtn_SubSkill01.gameObject.SetActive(false);
        sellectEffectBtn_SubSkill02.gameObject.SetActive(false);

        // 일반 스킬 키기
        windSlashSlot.gameObject.SetActive(true);
        warterSlashSlot.gameObject.SetActive(true);
        fireSlashSlot.gameObject.SetActive(true);

        // 서브 스킬 끄기
        windDrillSlot.gameObject.SetActive(false);
        waterBarrierSlot.gameObject.SetActive(false);
        fireBallSlot.gameObject.SetActive(false);

    }

    public void ChageWeapon_SS01_Btn() // 서브 스킬1 버튼 클릭
    {
        isLeft = true; // 임시로 설정한것 나중에 배제할거면 배제할것
        isRight = false;

        nomalAttackClick = false;

        subSkill01Click = true;

        subSkill02Click = false;

        sellectEffectBtn_NomalAttack.gameObject.SetActive(false);
        sellectEffectBtn_SubSkill01.gameObject.SetActive(true);
        sellectEffectBtn_SubSkill02.gameObject.SetActive(false);

        // 서브 스킬 키기
        windDrillSlot.gameObject.SetActive(true);
        waterBarrierSlot.gameObject.SetActive(true);
        fireBallSlot.gameObject.SetActive(true);

        // 일반 스킬 끄기
        windSlashSlot.gameObject.SetActive(false);
        warterSlashSlot.gameObject.SetActive(false);
        fireSlashSlot.gameObject.SetActive(false);
    }

    public void ChageWeapon_SS02_Btn() // 서브 스킬2 버튼 클릭
    {
        isLeft = false;
        isRight = true;

        nomalAttackClick = false;

        subSkill01Click = false;

        subSkill02Click = true;

        sellectEffectBtn_NomalAttack.gameObject.SetActive(false);
        sellectEffectBtn_SubSkill01.gameObject.SetActive(false);
        sellectEffectBtn_SubSkill02.gameObject.SetActive(true);

        // 서브 스킬 키기
        windDrillSlot.gameObject.SetActive(true);
        waterBarrierSlot.gameObject.SetActive(true);
        fireBallSlot.gameObject.SetActive(true);

        // 일반 스킬 끄기
        windSlashSlot.gameObject.SetActive(false);
        warterSlashSlot.gameObject.SetActive(false);
        fireSlashSlot.gameObject.SetActive(false);

    }

    [HideInInspector]
    public bool isChange_SS;

    
    private void ChageWeapon_SS() // 서브 스킬 교체
    {

        // 교체하려고 하는 스킬을 누른다(눌렀을 때 해당 오브젝트의 정보를 가져와서 교체할 것인지 여부를 묻는다)

        
    }



    // 버튼 나열
    public void Sellect_WindDrill()
    {
        
        //yameInstall_GrimRight_01.gameObject.SetActive(true);
        //yameInstall_GrimRight_02.gameObject.SetActive(false);
        //if (isLeft) // 왼쪽이냐 오른쪽이냐 확인
        //{
        //    PlayerShooting.intance.index_WeaponType_SubSkill_01 = 0;
        //}
        //else if(isRight)
        //{
        //    PlayerShooting.intance.index_WeaponType_SubSkill_02 = 0;
        //}

        

        if (subSkill01Click) //왼쪽 버튼
        {
            // 임시테스트
            

            if (PlayerShooting.intance.index_WeaponType_SubSkill_01 ==1) // 만약 왼쪽자리에 물베리어가 있다면...
            {
                yameInstall02_WarterBarrier.gameObject.SetActive(false); // 물베리어의 장착중 UI를 끄고

                yameInstall_StatusLeft_02.gameObject.SetActive(false); // 왼쪽 버튼 장착 위치 UI를 끈다 (밑에 있는 것들도 이런 방식으로 작동)

                PlayerShooting.intance.index_WeaponType_SubSkill_01 = 0; //왼쪽
            }
            else if (PlayerShooting.intance.index_WeaponType_SubSkill_01 == 2) 
            {
                yameInstall03_FireBall.gameObject.SetActive(false);

                yameInstall_StatusLeft_03.gameObject.SetActive(false);

                PlayerShooting.intance.index_WeaponType_SubSkill_01 = 0; //왼쪽
            }

            yameInstall01_WindDrill.gameObject.SetActive(true);

            yameInstall_StatusLeft_01.gameObject.SetActive(true);

            yameInstall_GrimLeft_01.gameObject.SetActive(true); // 인게임에서의 버튼 오브젝트 UI이다
            yameInstall_GrimLeft_02.gameObject.SetActive(false);
            yameInstall_GrimLeft_03.gameObject.SetActive(false);
        }
        else if(subSkill02Click) // 오른쪽 버튼
        {
            // 임시테스트
            
            if (PlayerShooting.intance.index_WeaponType_SubSkill_02 == 1) // 만약 오른쪽 자리에 물베리어가 있다면...
            {
                yameInstall02_WarterBarrier.gameObject.SetActive(false); // 물베리어의 장착중 UI를 끄고

                yameInstall_StatusRight_02.gameObject.SetActive(false); // 오른쪽 버튼 장착 위치에 UI를 끝다

                PlayerShooting.intance.index_WeaponType_SubSkill_02 = 0; //오른쪽
            }
            else if (PlayerShooting.intance.index_WeaponType_SubSkill_02 == 2)
            {
                yameInstall03_FireBall.gameObject.SetActive(false);

                yameInstall_StatusRight_03.gameObject.SetActive(false);

                PlayerShooting.intance.index_WeaponType_SubSkill_02 = 0; //오른쪽
            }

            yameInstall01_WindDrill.gameObject.SetActive(true);

            yameInstall_StatusRight_01.gameObject.SetActive(true);

            yameInstall_GrimRight_01.gameObject.SetActive(true);
            yameInstall_GrimRight_02.gameObject.SetActive(false);
            yameInstall_GrimRight_03.gameObject.SetActive(false);
        }

        installPopup.gameObject.SetActive(false);

        decisionBtn_WindDrill.gameObject.SetActive(false);

    }

    public void Sellect_Barrier()
    {
        
        //yameInstall_GrimRight_01.gameObject.SetActive(false);
        //yameInstall_GrimRight_02.gameObject.SetActive(true);

        //if (isLeft) // 왼쪽이냐 오른쪽이냐 확인
        //{
        //    PlayerShooting.intance.index_WeaponType_SubSkill_01 = 1;
        //}
        //else if (isRight)
        //{
        //    PlayerShooting.intance.index_WeaponType_SubSkill_02 = 1;
        //}

        // 임시 테스트
        //PlayerShooting.intance.index_WeaponType_SubSkill_02 = 1;

        if (subSkill01Click)
        {
            // 임시테스트
            

            if (PlayerShooting.intance.index_WeaponType_SubSkill_01 == 0)
            {
                yameInstall01_WindDrill.gameObject.SetActive(false);

                yameInstall_StatusLeft_01.gameObject.SetActive(false);

                PlayerShooting.intance.index_WeaponType_SubSkill_01 = 1;
            }
            else if (PlayerShooting.intance.index_WeaponType_SubSkill_01 == 2)
            {
                yameInstall03_FireBall.gameObject.SetActive(false);

                yameInstall_StatusLeft_03.gameObject.SetActive(false);

                PlayerShooting.intance.index_WeaponType_SubSkill_01 = 1;
            }

            yameInstall02_WarterBarrier.gameObject.SetActive(true);

            yameInstall_StatusLeft_02.gameObject.SetActive(true);

            yameInstall_GrimLeft_01.gameObject.SetActive(false);
            yameInstall_GrimLeft_02.gameObject.SetActive(true);
            yameInstall_GrimLeft_03.gameObject.SetActive(false);
        }
        else if (subSkill02Click)
        {
            // 임시테스트
            //PlayerShooting.intance.index_WeaponType_SubSkill_02 = 1;

            if (PlayerShooting.intance.index_WeaponType_SubSkill_02 == 0)
            {
                yameInstall01_WindDrill.gameObject.SetActive(false);

                yameInstall_StatusRight_01.gameObject.SetActive(false);

                PlayerShooting.intance.index_WeaponType_SubSkill_02 = 1;
            }
            else if (PlayerShooting.intance.index_WeaponType_SubSkill_02 == 2)
            {
                yameInstall03_FireBall.gameObject.SetActive(false);

                yameInstall_StatusRight_03.gameObject.SetActive(false);

                PlayerShooting.intance.index_WeaponType_SubSkill_02 = 1;
            }

            yameInstall02_WarterBarrier.gameObject.SetActive(true);

            yameInstall_StatusRight_02.gameObject.SetActive(true);

            yameInstall_GrimRight_01.gameObject.SetActive(false);
            yameInstall_GrimRight_02.gameObject.SetActive(true);
            yameInstall_GrimRight_03.gameObject.SetActive(false);
        }

        installPopup.gameObject.SetActive(false);

        decisionBtn_WaterBarrier.gameObject.SetActive(false);

    }


    public void Sellect_FireBall()
    {
        

        //if (isLeft) // 왼쪽이냐 오른쪽이냐 확인
        //{
        //    PlayerShooting.intance.index_WeaponType_SubSkill_01 = 2;
        //}
        //else if (isRight)
        //{
        //    PlayerShooting.intance.index_WeaponType_SubSkill_02 = 2;
        //}

        if (subSkill01Click)
        {
            // 임시테스트
            

            if (PlayerShooting.intance.index_WeaponType_SubSkill_01 == 0)
            {
                yameInstall01_WindDrill.gameObject.SetActive(false);

                yameInstall_StatusLeft_01.gameObject.SetActive(false);

                PlayerShooting.intance.index_WeaponType_SubSkill_01 = 2;
            }
            else if (PlayerShooting.intance.index_WeaponType_SubSkill_01 == 1)
            {
                yameInstall02_WarterBarrier.gameObject.SetActive(false);

                yameInstall_StatusLeft_02.gameObject.SetActive(false);

                PlayerShooting.intance.index_WeaponType_SubSkill_01 = 2;
            }

            yameInstall03_FireBall.gameObject.SetActive(true);

            yameInstall_StatusLeft_03.gameObject.SetActive(true);

            yameInstall_GrimLeft_01.gameObject.SetActive(false);
            yameInstall_GrimLeft_02.gameObject.SetActive(false);
            yameInstall_GrimLeft_03.gameObject.SetActive(true);
        }
        else if (subSkill02Click)
        {
            // 임시테스트
            //PlayerShooting.intance.index_WeaponType_SubSkill_02 = 2;

            if (PlayerShooting.intance.index_WeaponType_SubSkill_02 == 0)
            {
                yameInstall01_WindDrill.gameObject.SetActive(false);

                yameInstall_StatusRight_01.gameObject.SetActive(false);

                PlayerShooting.intance.index_WeaponType_SubSkill_02 = 2;
            }
            else if (PlayerShooting.intance.index_WeaponType_SubSkill_02 == 1)
            {
                yameInstall02_WarterBarrier.gameObject.SetActive(false);

                yameInstall_StatusRight_02.gameObject.SetActive(false);

                PlayerShooting.intance.index_WeaponType_SubSkill_02 = 2;
            }

            yameInstall03_FireBall.gameObject.SetActive(true);

            yameInstall_StatusRight_03.gameObject.SetActive(true);

            yameInstall_GrimRight_01.gameObject.SetActive(false);
            yameInstall_GrimRight_02.gameObject.SetActive(false);
            yameInstall_GrimRight_03.gameObject.SetActive(true);
        }

        installPopup.gameObject.SetActive(false);

        decisionBtn_FireBall.gameObject.SetActive(false);

    }

    


    // 기본공격 관련 정보

    [HideInInspector]
    public bool isChange_NA;

    public void Sellect_WindSlash() // 결정 버튼 누르면 활성화
    {
        Debug.Log("교체시도 - 바람 평타");

        yameInstall04_WindSlash.gameObject.SetActive(true);
        yameInstall05_WaterSlash.gameObject.SetActive(false);
        yameInstall06_FireSlash.gameObject.SetActive(false);

        selectBtn_NomalAttackImg.sprite = windSlashImg; // 교체할 위치의 이미지 교체용(일반 공격은 하나밖에 없지만 이렇게 설명해둠)

        isChange_NA = true;

        PlayerShooting.intance.index_WeaponType_Nomal = 0;

        installPopup.gameObject.SetActive(false);

        decisionBtn_WindSlash.gameObject.SetActive(false);

    }

    public void Sellect_WaterSlash()
    {
        Debug.Log("교체시도 - 물 평타");

        yameInstall04_WindSlash.gameObject.SetActive(false);
        yameInstall05_WaterSlash.gameObject.SetActive(true);
        yameInstall06_FireSlash.gameObject.SetActive(false);

        selectBtn_NomalAttackImg.sprite = waterSlashImg;

        isChange_NA = true;

        PlayerShooting.intance.index_WeaponType_Nomal = 1;

        installPopup.gameObject.SetActive(false);

        decisionBtn_WaterSlash.gameObject.SetActive(false);
    }

    public void Sellect_FireSlash()
    {
        Debug.Log("교체시도 - 불 평타");

        yameInstall04_WindSlash.gameObject.SetActive(false);
        yameInstall05_WaterSlash.gameObject.SetActive(false);
        yameInstall06_FireSlash.gameObject.SetActive(true);

        selectBtn_NomalAttackImg.sprite = fireSlashImg;

        isChange_NA = true;

        PlayerShooting.intance.index_WeaponType_Nomal = 2;

        installPopup.gameObject.SetActive(false);

        decisionBtn_FireSlash.gameObject.SetActive(false);
    }


    // 인스톨 팝업창 관련
    public void ExitInstallPopup()
    {
        installPopup.gameObject.SetActive(false);

        decisionBtn_WindSlash.gameObject.SetActive(false);
        decisionBtn_WaterSlash.gameObject.SetActive(false);
        decisionBtn_FireSlash.gameObject.SetActive(false);
        decisionBtn_WindDrill.gameObject.SetActive(false);
        decisionBtn_WaterBarrier.gameObject.SetActive(false);
        decisionBtn_FireBall.gameObject.SetActive(false);
    }

    // 인스톨 팝업창 관련 일반 공격
    public void ExplanationPopup_WindSlash()
    {
        installPopup.gameObject.SetActive(true);

        installSkillName.text = statusDB.NomalAttack[0].attackName;

        installSkillImg.sprite = windSlashImg;

        installSkillLevel.text = "Lv." + windSlash_CurTypeLevel;

        installSkillExplanation.text = statusDB.NomalAttack[0].attackExplanation;

        installSkillProperty.text = "[기본 공격]";

        decisionBtn_WindSlash.gameObject.SetActive(true);
    }

    public void ExplanationPopup_WaterSlash()
    {
        installPopup.gameObject.SetActive(true);

        installSkillName.text = statusDB.NomalAttack[6].attackName;

        installSkillImg.sprite = waterSlashImg;

        installSkillLevel.text = "Lv." + waterSlash_CurTypeLevel;

        installSkillExplanation.text = statusDB.NomalAttack[6].attackExplanation;

        installSkillProperty.text = "[기본 공격]";

        decisionBtn_WaterSlash.gameObject.SetActive(true);
    }

    public void ExplanationPopup_FireSlash()
    {
        installPopup.gameObject.SetActive(true);

        installSkillName.text = statusDB.NomalAttack[12].attackName;

        installSkillImg.sprite = fireSlashImg;

        installSkillLevel.text = "Lv." + fireSlash_CurTypeLevel;

        installSkillExplanation.text = statusDB.NomalAttack[12].attackExplanation;

        installSkillProperty.text = "[기본 공격]";

        decisionBtn_FireSlash.gameObject.SetActive(true);
    }


    // 인스톨 팝업창 관련 서브 스킬
    public void ExplanationPopup_WindDrill()
    {
        installPopup.gameObject.SetActive(true);

        installSkillName.text = statusDB.SubSkill[0].subSkillName;

        installSkillImg.sprite = windDrillImg;

        installSkillLevel.text = "Lv." + windDrill_CurTypeLevel;

        installSkillExplanation.text = statusDB.SubSkill[0].subSkillExplanation;

        installSkillProperty.text = "[액티브]";

        decisionBtn_WindDrill.gameObject.SetActive(true);
    }

    public void ExplanationPopup_WaterBarrier()
    {
        installPopup.gameObject.SetActive(true);

        installSkillName.text = statusDB.SubSkill[6].subSkillName;

        installSkillImg.sprite = waterBarrierImg;

        installSkillLevel.text = "Lv." + waterBarrier_CurTypeLevel;

        installSkillExplanation.text = statusDB.SubSkill[6].subSkillExplanation;

        installSkillProperty.text = "[액티브]";

        decisionBtn_WaterBarrier.gameObject.SetActive(true);
    }

    public void ExplanationPopup_FireBall()
    {
        installPopup.gameObject.SetActive(true);

        installSkillName.text = statusDB.SubSkill[12].subSkillName;

        installSkillImg.sprite = fireBallImg;

        installSkillLevel.text = "Lv." + fireBall_CurTypeLevel;

        installSkillExplanation.text = statusDB.SubSkill[12].subSkillExplanation;

        installSkillProperty.text = "[액티브]";

        decisionBtn_FireBall.gameObject.SetActive(true);
    }


    

    [HideInInspector]
    public int randomIndex01; // 인덱스 데이터 저장용

    [HideInInspector]
    public int randomIndex02;

    [HideInInspector]
    public int randomIndex03;

    private bool getSkillData01; // 데이터가 저장되어있는지에 대한 여부

    private bool getSkillData02;

    private bool getSkillData03;

    [SerializeField]
    private Image attributeBtn_Img01; // 스킬 획득 이미지

    [SerializeField]
    private Image attributeBtn_Img02;

    [SerializeField]
    private Image attributeBtn_Img03;

    [SerializeField]
    private TextMeshProUGUI attributeBtn_Name01; // 스킬 획득 이름

    [SerializeField]
    private TextMeshProUGUI attributeBtn_Name02;

    [SerializeField]
    private TextMeshProUGUI attributeBtn_Name03;

    [SerializeField]
    private TextMeshProUGUI attributeBtn_Explanation01; // 스킬 획득 설명

    [SerializeField]
    private TextMeshProUGUI attributeBtn_Explanation02;

    [SerializeField]
    private TextMeshProUGUI attributeBtn_Explanation03;

    [SerializeField]
    private TextMeshProUGUI attributeBtn_TypeLevel01; // 해당 스킬의 레벨

    [SerializeField]
    private TextMeshProUGUI attributeBtn_TypeLevel02;

    [SerializeField]
    private TextMeshProUGUI attributeBtn_TypeLevel03;

    [SerializeField]
    private TextMeshProUGUI attributeBtn_Property01;

    [SerializeField]
    private TextMeshProUGUI attributeBtn_Property02;

    [SerializeField]
    private TextMeshProUGUI attributeBtn_Property03;

    // 레벨업으로 스킬 얻을때 관리되는 데이터

    #region 데이터 인덱스에 바로 직렬화 방법 일단 주석처리 나중에 활용한다면 활용

    // 밑의 함수 일단 안사용함
    // 랜덤 데이터
    //private int randomIndexGet; // 데이터 분산용
    //private void RandomSkillData() // 운좋게 데이터 테이블의 데이터를 정리해서 일반 공격, 서브 스킬의 인덱스가 엇비슷해서 변수가 적게 사용(해당 함수를 쓸때는 브레이킹 걸어야함)()
    //{
    //    getSkillData01 = false; // 스킬 선택창 나올때마다 초기화

    //    getSkillData02 = false;

    //    getSkillData03 = false;

    //    while (!getSkillData01 || !getSkillData02 || !getSkillData03)
    //    {
    //        do
    //        {
    //            randomIndexGet = Random.Range(0, 18);
    //            Debug.Log("무기 인덱스 랜덤 함수 : 확인용  " + randomIndexGet);
    //        }
    //        while (randomIndexGet == 0 || randomIndexGet == 6 || randomIndexGet == 12);//무기 인덱스 0 6 12는 무기데이터가 없음(없는 상태의 데이터이다) 


    //        if (!getSkillData01)
    //        {
    //            getSkillData01 = true;

    //            randomIndex01 = randomIndexGet; // 뽑은 데이터 넣기

    //            Debug.Log("무기 인덱스 첫번째 : 확인용  " + randomIndex01);

    //        }
    //        else if (!getSkillData02)
    //        {
    //            getSkillData02 = true;

    //            randomIndex02 = randomIndexGet; // 뽑은 데이터 넣기

    //            Debug.Log("무기 인덱스 두번째 : 확인용  " + randomIndex02);

    //        }
    //        else if (!getSkillData03)
    //        {
    //            getSkillData03 = true;

    //            randomIndex03 = randomIndexGet; // 뽑은 데이터 넣기

    //            Debug.Log("무기 인덱스 세번째 : 확인용  " + randomIndex03);
    //        }
    //    }


    //}

    #endregion


    //private void AttributeSkill01_Data()
    //{

    //}

    //private void AttributeSkill02_Data()
    //{

    //}

    //private void AttributeSkill03_Data()
    //{

    //}



    // 밑에는 비효율적으로 제작된 함수

    private int getWeaponType;

    public void GetSkillRandom() // 상당히 비 효율적이지만 일단 제작(UI_Script에서 지금 사용중)
    {
        getSkillData01 = false; // 스킬 선택창 나올때마다 초기화

        getSkillData02 = false;

        getSkillData03 = false;

        int randCount = 0; 

        while (randCount < 1) //랜덤 돌리는 횟수 
        {
            while (!getSkillData01 || !getSkillData02 || !getSkillData03)
            {
                getWeaponType = Random.Range(0, 6); // 바람 기본 = 0, 물 기본 = 1, 불 기본 = 2, 위드드릴 = 3, 워터베리어 = 4, 파이어볼 = 5

                if (!getSkillData01)
                {
                    getSkillData01 = true;

                    randomIndex01 = getWeaponType; // 뽑은 데이터 넣기

                    Debug.Log("무기 인덱스 첫번째 : 확인용  " + randomIndex01);

                    //이름 이미지 레벨등을 넣기위함
                    switch (randomIndex01) // 이부분은 열거형으로 변경하면 가독성이 업될거임 ㅇㅇ
                    {
                        case 0: // 바람 공격 레벨업 준비
                            attributeBtn_Img01.sprite = windSlashImg;
                            attributeBtn_Name01.text = statusDB.NomalAttack[0].attackName;
                            attributeBtn_Explanation01.text = statusDB.NomalAttack[0].attackExplanation;
                            attributeBtn_TypeLevel01.text = "Lv " + windSlash_CurTypeLevel;
                            attributeBtn_Property01.text = "[기본 공격]";
                            
                            break;

                        case 1: // 물 공격
                            attributeBtn_Img01.sprite = waterSlashImg;
                            attributeBtn_Name01.text = statusDB.NomalAttack[6].attackName;
                            attributeBtn_Explanation01.text = statusDB.NomalAttack[6].attackExplanation;
                            attributeBtn_TypeLevel01.text = "Lv " + waterSlash_CurTypeLevel;
                            attributeBtn_Property01.text = "[기본 공격]";

                            break;

                        case 2: // 불 공격
                            attributeBtn_Img01.sprite = fireSlashImg;
                            attributeBtn_Name01.text = statusDB.NomalAttack[12].attackName;
                            attributeBtn_Explanation01.text = statusDB.NomalAttack[12].attackExplanation;
                            attributeBtn_TypeLevel01.text = "Lv " + fireSlash_CurTypeLevel;
                            attributeBtn_Property01.text = "[기본 공격]";

                            break;

                        case 3: // 윈드 드릴
                            attributeBtn_Img01.sprite = windDrillImg;
                            attributeBtn_Name01.text = statusDB.SubSkill[0].subSkillName;
                            attributeBtn_Explanation01.text = statusDB.SubSkill[0].subSkillExplanation;
                            attributeBtn_TypeLevel01.text = "Lv " + windDrill_CurTypeLevel;
                            attributeBtn_Property01.text = "[액티브]";

                            break;

                        case 4: // 워터 베리어
                            attributeBtn_Img01.sprite = waterBarrierImg;
                            attributeBtn_Name01.text = statusDB.SubSkill[6].subSkillName;
                            attributeBtn_Explanation01.text = statusDB.SubSkill[6].subSkillExplanation;
                            attributeBtn_TypeLevel01.text = "Lv " + waterBarrier_CurTypeLevel;
                            attributeBtn_Property01.text = "[액티브]";

                            break;

                        case 5: // 파이어볼
                            attributeBtn_Img01.sprite = fireBallImg;
                            attributeBtn_Name01.text = statusDB.SubSkill[12].subSkillName;
                            attributeBtn_Explanation01.text = statusDB.SubSkill[12].subSkillExplanation;
                            attributeBtn_TypeLevel01.text = "Lv " + fireBall_CurTypeLevel;
                            attributeBtn_Property01.text = "[액티브]";

                            break;

                    }

                }
                else if (!getSkillData02)
                {
                    getSkillData02 = true;

                    randomIndex02 = getWeaponType; // 뽑은 데이터 넣기

                    Debug.Log("무기 인덱스 두번째 : 확인용  " + randomIndex02);
                    //이름 이미지 레벨등을 넣기위함
                    switch (randomIndex02) // 이부분은 열거형으로 변경하면 가독성이 업될거임 ㅇㅇ
                    {
                        case 0: // 바람 공격 레벨업 준비
                            attributeBtn_Img02.sprite = windSlashImg;
                            attributeBtn_Name02.text = statusDB.NomalAttack[0].attackName;
                            attributeBtn_Explanation02.text = statusDB.NomalAttack[0].attackExplanation;
                            attributeBtn_TypeLevel02.text = "Lv " + windSlash_CurTypeLevel;
                            attributeBtn_Property02.text = "[기본 공격]";

                            break;

                        case 1: // 물 공격
                            attributeBtn_Img02.sprite = waterSlashImg;
                            attributeBtn_Name02.text = statusDB.NomalAttack[6].attackName;
                            attributeBtn_Explanation02.text = statusDB.NomalAttack[6].attackExplanation;
                            attributeBtn_TypeLevel02.text = "Lv " + waterSlash_CurTypeLevel;
                            attributeBtn_Property02.text = "[기본 공격]";

                            break;

                        case 2: // 불 공격
                            attributeBtn_Img02.sprite = fireSlashImg;
                            attributeBtn_Name02.text = statusDB.NomalAttack[12].attackName;
                            attributeBtn_Explanation02.text = statusDB.NomalAttack[12].attackExplanation;
                            attributeBtn_TypeLevel02.text = "Lv " + fireSlash_CurTypeLevel;
                            attributeBtn_Property02.text = "[기본 공격]";

                            break;

                        case 3: // 윈드 드릴
                            attributeBtn_Img02.sprite = windDrillImg;
                            attributeBtn_Name02.text = statusDB.SubSkill[0].subSkillName;
                            attributeBtn_Explanation02.text = statusDB.SubSkill[0].subSkillExplanation;
                            attributeBtn_TypeLevel02.text = "Lv " + windDrill_CurTypeLevel;
                            attributeBtn_Property02.text = "[액티브]";

                            break;

                        case 4: // 워터 베리어
                            attributeBtn_Img02.sprite = waterBarrierImg;
                            attributeBtn_Name02.text = statusDB.SubSkill[6].subSkillName;
                            attributeBtn_Explanation02.text = statusDB.SubSkill[6].subSkillExplanation;
                            attributeBtn_TypeLevel02.text = "Lv " + waterBarrier_CurTypeLevel;
                            attributeBtn_Property02.text = "[액티브]";

                            break;

                        case 5: // 파이어볼
                            attributeBtn_Img02.sprite = fireBallImg;
                            attributeBtn_Name02.text = statusDB.SubSkill[12].subSkillName;
                            attributeBtn_Explanation02.text = statusDB.SubSkill[12].subSkillExplanation;
                            attributeBtn_TypeLevel02.text = "Lv " + fireBall_CurTypeLevel;
                            attributeBtn_Property02.text = "[액티브]";

                            break;
                    }

                }
                else if (!getSkillData03)
                {
                    getSkillData03 = true;

                    randomIndex03 = getWeaponType; // 뽑은 데이터 넣기

                    Debug.Log("무기 인덱스 세번째 : 확인용  " + randomIndex03);
                    //이름 이미지 레벨등을 넣기위함
                    switch (randomIndex03) // 이부분은 열거형으로 변경하면 가독성이 업될거임 ㅇㅇ
                    {
                        case 0: // 바람 공격 레벨업 준비
                            attributeBtn_Img03.sprite = windSlashImg;
                            attributeBtn_Name03.text = statusDB.NomalAttack[0].attackName;
                            attributeBtn_Explanation03.text = statusDB.NomalAttack[0].attackExplanation;
                            attributeBtn_TypeLevel03.text = "Lv " + windSlash_CurTypeLevel;
                            attributeBtn_Property03.text = "[기본 공격]";

                            break;

                        case 1: // 물 공격
                            attributeBtn_Img03.sprite = waterSlashImg;
                            attributeBtn_Name03.text = statusDB.NomalAttack[6].attackName;
                            attributeBtn_Explanation03.text = statusDB.NomalAttack[6].attackExplanation;
                            attributeBtn_TypeLevel03.text = "Lv " + waterSlash_CurTypeLevel;
                            attributeBtn_Property03.text = "[기본 공격]";

                            break;

                        case 2: // 불 공격
                            attributeBtn_Img03.sprite = fireSlashImg;
                            attributeBtn_Name03.text = statusDB.NomalAttack[12].attackName;
                            attributeBtn_Explanation03.text = statusDB.NomalAttack[12].attackExplanation;
                            attributeBtn_TypeLevel03.text = "Lv " + fireSlash_CurTypeLevel;
                            attributeBtn_Property03.text = "[기본 공격]";

                            break;

                        case 3: // 윈드 드릴
                            attributeBtn_Img03.sprite = windDrillImg;
                            attributeBtn_Name03.text = statusDB.SubSkill[0].subSkillName;
                            attributeBtn_Explanation03.text = statusDB.SubSkill[0].subSkillExplanation;
                            attributeBtn_TypeLevel03.text = "Lv " + windDrill_CurTypeLevel;
                            attributeBtn_Property03.text = "[액티브]";

                            break;

                        case 4: // 워터 베리어
                            attributeBtn_Img03.sprite = waterBarrierImg;
                            attributeBtn_Name03.text = statusDB.SubSkill[6].subSkillName;
                            attributeBtn_Explanation03.text = statusDB.SubSkill[6].subSkillExplanation;
                            attributeBtn_TypeLevel03.text = "Lv " + waterBarrier_CurTypeLevel;
                            attributeBtn_Property03.text = "[액티브]";

                            break;

                        case 5: // 파이어볼
                            attributeBtn_Img03.sprite = fireBallImg;
                            attributeBtn_Name03.text = statusDB.SubSkill[12].subSkillName;
                            attributeBtn_Explanation03.text = statusDB.SubSkill[12].subSkillExplanation;
                            attributeBtn_TypeLevel03.text = "Lv " + fireBall_CurTypeLevel;
                            attributeBtn_Property03.text = "[액티브]";

                            break;
                    }
                }
            }

            randCount++;
        }

        

        

    }

    private void SkillLevelIndex_Initialization() // 게임 시작시 스킬 레벨 초기화
    {
        // 일반 공격 인덱스 값
        windSlash_CurLevelIndex = 1;

        waterSlash_CurLevelIndex = 7;

        fireSlash_CurLevelIndex = 13;

        //일반 공격 레벨 값
        windSlash_CurTypeLevel = 1;

        waterSlash_CurTypeLevel = 1;

        fireSlash_CurTypeLevel = 1;

        // 서브 스킬
        windDrill_CurLevelIndex = 1;

        waterBarrier_CurLevelIndex = 7;

        fireBall_CurLevelIndex = 13;

        //서브 스킬 레벨 값
        windDrill_CurTypeLevel = 1;

        waterBarrier_CurTypeLevel = 1;

        fireBall_CurTypeLevel = 1;

    }

    [HideInInspector]
    public int windSlash_CurLevelIndex; // 초기 인덱스 (초기화)

    private int windSlash_CurTypeLevel; // 해당 스킬의 레벨

    // 일반공격 레벨업
    public void WindSlash_LevelUp() // 바람 기본공격 인덱스 (1~5)
    {
        //_windSlash_CurLevelIndex = 1; // 초기 레벨

        if (windSlash_CurLevelIndex != 5)
        {
            windSlash_CurLevelIndex++;
            windSlash_CurTypeLevel++;
        }

        //Debug.Log("현재 바람 공격 레벨 : "+ windSlash_CurLevelIndex);


    }

    [HideInInspector]
    public int waterSlash_CurLevelIndex;  // 초기 인덱스 (초기화)

    private int waterSlash_CurTypeLevel; // 해당 스킬의 레벨

    public void WaterSlash_LevelUp() // 물 기본공격 인덱스 (7~11)
    {
        if (waterSlash_CurLevelIndex != 11)
        {
            waterSlash_CurLevelIndex++;
            waterSlash_CurTypeLevel++;
        }

        //Debug.Log("현재 물 공격 레벨 : " + windSlash_CurLevelIndex);


    }

    [HideInInspector]
    public int fireSlash_CurLevelIndex;  // 초기 인덱스 (초기화)

    private int fireSlash_CurTypeLevel; // 해당 스킬의 레벨

    public void FireSlash_LevelUp() // 불 기본공격 인덱스 (13~17)
    {
        if (fireSlash_CurLevelIndex != 17)
        {
            fireSlash_CurLevelIndex++;
            fireSlash_CurTypeLevel++;
        }

        
    }

    // 서브 스킬 레벨업

    [HideInInspector]
    public int windDrill_CurLevelIndex; // 초기 인덱스 (초기화)

    private int windDrill_CurTypeLevel; // 해당 스킬의 레벨

    public void WindDrill_LevelUp()// 윈드 드릴 기본공격 인덱스 (1~5)
    {
        if (windDrill_CurLevelIndex != 5)
        {
            windDrill_CurLevelIndex++;
            windDrill_CurTypeLevel++;
        }

        
    }

    [HideInInspector]
    public int waterBarrier_CurLevelIndex;  // 초기 인덱스 (초기화)

    private int waterBarrier_CurTypeLevel; // 해당 스킬의 레벨

    public void WaterBarrier_LevelUp()// 워터 베리어 기본공격 인덱스 (7~11)
    {
        if (waterBarrier_CurLevelIndex != 11)
        {
            waterBarrier_CurLevelIndex++;
            waterBarrier_CurTypeLevel++;
        }

        
    }

    [HideInInspector]
    public int fireBall_CurLevelIndex;  // 초기 인덱스 (초기화)

    private int fireBall_CurTypeLevel; // 해당 스킬의 레벨

    public void FireBall_LevelUp()// 파이어볼 기본공격 인덱스 (13~17)
    {
        if (fireBall_CurLevelIndex != 17)
        {
            fireBall_CurLevelIndex++;
            fireBall_CurTypeLevel++;
        }

        
    }



}
