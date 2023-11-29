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

    }

    private void Update()
    {
        //테스트
        //Sellect_WindSlash();
        //Sellect_WaterSlash();
        //Sellect_FireSlash();

        
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

    public void Sellect_WindSlash()
    {
        Debug.Log("교체시도 - 바람 평타");

        yameInstall04_WindSlash.gameObject.SetActive(true);
        yameInstall05_WaterSlash.gameObject.SetActive(false);
        yameInstall06_FireSlash.gameObject.SetActive(false);

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

        //installSkillLevel.text = statusDB.NomalAttack[0].typeLevel;

        installSkillExplanation.text = statusDB.NomalAttack[0].attackExplanation;

        decisionBtn_WindSlash.gameObject.SetActive(true);
    }

    public void ExplanationPopup_WaterSlash()
    {
        installPopup.gameObject.SetActive(true);

        installSkillName.text = statusDB.NomalAttack[6].attackName;

        installSkillImg.sprite = waterSlashImg;

        //installSkillLevel.text = statusDB.NomalAttack[0].typeLevel;

        installSkillExplanation.text = statusDB.NomalAttack[6].attackExplanation;

        decisionBtn_WaterSlash.gameObject.SetActive(true);
    }

    public void ExplanationPopup_FireSlash()
    {
        installPopup.gameObject.SetActive(true);

        installSkillName.text = statusDB.NomalAttack[12].attackName;

        installSkillImg.sprite = fireSlashImg;

        //installSkillLevel.text = statusDB.NomalAttack[0].typeLevel;

        installSkillExplanation.text = statusDB.NomalAttack[12].attackExplanation;

        decisionBtn_FireSlash.gameObject.SetActive(true);
    }


    // 인스톨 팝업창 관련 서브 스킬
    public void ExplanationPopup_WindDrill()
    {
        installPopup.gameObject.SetActive(true);

        installSkillName.text = statusDB.SubSkill[0].subSkillName;

        installSkillImg.sprite = windDrillImg;

        //installSkillLevel.text = statusDB.NomalAttack[0].typeLevel;

        installSkillExplanation.text = statusDB.SubSkill[0].subSkillExplanation;

        decisionBtn_WindDrill.gameObject.SetActive(true);
    }

    public void ExplanationPopup_WaterBarrier()
    {
        installPopup.gameObject.SetActive(true);

        installSkillName.text = statusDB.SubSkill[6].subSkillName;

        installSkillImg.sprite = waterBarrierImg;

        //installSkillLevel.text = statusDB.NomalAttack[0].typeLevel;

        installSkillExplanation.text = statusDB.SubSkill[6].subSkillExplanation;

        decisionBtn_WaterBarrier.gameObject.SetActive(true);
    }

    public void ExplanationPopup_FireBall()
    {
        installPopup.gameObject.SetActive(true);

        installSkillName.text = statusDB.SubSkill[12].subSkillName;

        installSkillImg.sprite = fireBallImg;

        //installSkillLevel.text = statusDB.NomalAttack[0].typeLevel;

        installSkillExplanation.text = statusDB.SubSkill[12].subSkillExplanation;

        decisionBtn_FireBall.gameObject.SetActive(true);
    }

}
