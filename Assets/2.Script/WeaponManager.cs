using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class IsLeft
//{

//    // 서브 스킬 배치장소
//    private bool isLeft;

//    private bool isRight;

//    public bool ChangeSkillPos()
//    {
//        get{
//            return isLeft;
//        }

//        set{
//            isLeft = true;
//        }


//    }
//}

public class WeaponManager : MonoBehaviour
{

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


    // 밑에 있는 해당 함수는 야매로 만든 스킬 장착 함수이다 나중에 수정할 것

    [SerializeField]
    private GameObject yameInstall01;

    [SerializeField]
    private GameObject yameInstall02;

    [SerializeField]
    private GameObject yameInstall03;

    [SerializeField]
    private GameObject yameInstall04;

    [SerializeField]
    private GameObject yameInstall05;

    [SerializeField]
    private GameObject yameInstall06;

    [SerializeField]
    private GameObject yameInstall_GrimRight_01;

    [SerializeField]
    private GameObject yameInstall_GrimRight_02;

    [SerializeField]
    private GameObject yameInstall_GrimRight_03;

    // 버튼의 값 기입
    public void ChageWeapon_NA()
    {

    }

    public void ChageWeapon_SS01_Btn() // 서브 스킬1 버튼 클릭
    {
        isLeft = true;
        isRight = false;
    }

    public void ChageWeapon_SS02_Btn() // 서브 스킬2 버튼 클릭
    {
        isLeft = false;
        isRight = true;
    }

    [HideInInspector]
    public bool isChange_SS;

    
    

    private void ChageWeapon_SS() // 서브 스킬 교체
    {

        // 교체하려고 하는 스킬을 누른다(눌렀을 때 해당 오브젝트의 정보를 가져와서 교체할 것인지 여부를 묻는다)

        if (isLeft) // 만약 왼쪽 버튼을 눌렀다면
        {

        }
    }



    // 버튼 나열
    public void Sellect_WindDrill()
    {
        yameInstall01.gameObject.SetActive(true);
        yameInstall03.gameObject.SetActive(false);

        yameInstall_GrimRight_01.gameObject.SetActive(true);
        yameInstall_GrimRight_02.gameObject.SetActive(false);
        //if (isLeft) // 왼쪽이냐 오른쪽이냐 확인
        //{
        //    PlayerShooting.intance.index_WeaponType_SubSkill_01 = 0;
        //}
        //else if(isRight)
        //{
        //    PlayerShooting.intance.index_WeaponType_SubSkill_02 = 0;
        //}

        // 임시테스트
        PlayerShooting.intance.index_WeaponType_SubSkill_02 = 0;





    }

    public void Sellect_FireBall()
    {
        yameInstall02.gameObject.SetActive(true);

        if (isLeft) // 왼쪽이냐 오른쪽이냐 확인
        {
            PlayerShooting.intance.index_WeaponType_SubSkill_01 = 2;
        }
        else if (isRight)
        {
            PlayerShooting.intance.index_WeaponType_SubSkill_02 = 2;
        }
    }

    public void Sellect_Barrier()
    {
        yameInstall01.gameObject.SetActive(false);
        yameInstall03.gameObject.SetActive(true);

        yameInstall_GrimRight_01.gameObject.SetActive(false);
        yameInstall_GrimRight_02.gameObject.SetActive(true);

        //if (isLeft) // 왼쪽이냐 오른쪽이냐 확인
        //{
        //    PlayerShooting.intance.index_WeaponType_SubSkill_01 = 1;
        //}
        //else if (isRight)
        //{
        //    PlayerShooting.intance.index_WeaponType_SubSkill_02 = 1;
        //}

        // 임시 테스트
        PlayerShooting.intance.index_WeaponType_SubSkill_02 = 1;
    }


    // 기본공격 관련 정보

    [HideInInspector]
    public bool isChange_NA;

    public void Sellect_WindSlash()
    {
        Debug.Log("교체시도 - 바람 평타");

        yameInstall04.gameObject.SetActive(true);
        yameInstall05.gameObject.SetActive(false);
        yameInstall06.gameObject.SetActive(false);

        isChange_NA = true;

        PlayerShooting.intance.index_WeaponType_Nomal = 0;
    }

    public void Sellect_WaterSlash()
    {
        Debug.Log("교체시도 - 물 평타");

        yameInstall04.gameObject.SetActive(false);
        yameInstall05.gameObject.SetActive(true);
        yameInstall06.gameObject.SetActive(false);

        isChange_NA = true;

        PlayerShooting.intance.index_WeaponType_Nomal = 1;
    }

    public void Sellect_FireSlash()
    {
        Debug.Log("교체시도 - 불 평타");

        yameInstall04.gameObject.SetActive(false);
        yameInstall05.gameObject.SetActive(false);
        yameInstall06.gameObject.SetActive(true);

        isChange_NA = true;

        PlayerShooting.intance.index_WeaponType_Nomal = 2;
    }

    // 무기 변환
    private void ChangeWeapon_Install(int slotNum)
    {


        switch (slotNum)
        {
            case 0:
                //todo: 1번 스킬창 선택

                if (yameInstall01.activeSelf == true)
                {
                    yameInstall01.gameObject.SetActive(false);

                }
                else if (yameInstall01.activeSelf == false)
                {

                    yameInstall01.gameObject.SetActive(true);

                }

                break;

            case 1:

                break;

            case 2:

                break;

            case 3: //todo: 4번 스킬창 선택(임시 : 바람 선택)

                break;

            case 4: //(임시 : 물 선택)

                break;

            case 5: // (임시 : 불 선택)

                break;
        }

    }

}
