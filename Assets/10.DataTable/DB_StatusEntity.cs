[System.Serializable]
//엑셀 테이블에 저장된 데이터의 행과 동일한 변수타입, 
//열과 동일한 변수들을 정의하는 직렬화 클래스

public class DB_StatusEntity
{
    //기존 클래스명 하나로 통일했었음... DB_StatusEntity
    // 클래스를 늘리는 과정에서 엑셀에서 reimport 과정을 거쳐라...
    // 각 테이블별 수치 정수, 실수 데이터값(해당 값을 만들지 않으면)
    // 엑셀 데이터 에셋화(DB_Status오브젝트 에셋)에 데이터가 기입이 안된다.

    public int playerUID;

    public int indexLevel01; // 데이터 테이블 열 번호(=> 인덱스 레벨로 변경)

    public int playerHP; // 캐릭터 hp

    public float attackRate; // 공격속도

    public float addEXP; // 추가 경험치 획득

    public float coolDownTime; // 쿨타임

    public float playerDamage; // 공격력

    public int maxEXP; // 레벨당 최대 경험치
        
}

[System.Serializable]
public class DB_StatusEntity_02
{
    // 몬스터 수치

    public int monsterUID;

    public int indexLevel02;

    public float monsterHP;

    public float monsterSpeed;

    public int monsterGold;

    public float monsterEXP;

    public int monsterDamage;

}

// 초기 데이터 테이블 잔재
//[System.Serializable]
//public class DB_StatusEntity_03
//{
//    // 공격 타입 인덱스

//    public int indexUID;

//    public int weaponType;

//    public string name;

//    public float typeLevel;

//    public float coolTime;

//    public float typeAbility;

//}

[System.Serializable]
public class DB_StatusEntity_03
{
    // 기본 공격 타입 인덱스

    public int nomalAttackUID; // 기본 공격 UID

    public int weaponType; // 기본공격 속성(타입)

    public float typeLevel; // 기본공격 레벨

    public string name; // 기본공격 이름
        
    public float coolTime; // 기본공격 쿨타임(기본공격이므로 쿨타임은 존재하지 않음) <= 무기 공격속도 처리는 playerStatus에서 처리

    public float typeAbility; // 음... 일단 빈 인덱스

    public float singleDamage; // 바람 기본공격 단일 피해 데미지

    public float spreadDamage; // 불 기본공격 범위 피해 데미지

    public float spreadRange; // 불 기본 공격 범위 수치(얼만큼의 범위로 조절...)

    public float speedDown; // 물 기본공격의 이동수치 하락치
}


[System.Serializable]
public class DB_StatusEntity_04
{
    // 서브 스킬 타입 인덱스

    public int subSkillUID; // 서브 스킬 UID

    public int weaponType; // 서브 스킬 무기 속성

    public float typeLevel; // 서브 스킬의 무기 레벨

    public string name; // 서브 스킬 이름

    public float coolTime; // 서브 스킬 쿨타임

    public float typeAbility; // 인단 빈 인덱스

    public float drillDamage; //바람 서브 스킬의 데미지

    public int drillCount; // 바람 서브 스킬의 연속 단타 횟수(몇번의 피격)

    public float penetDamage; // 불 서브 스킬의 관통 데미지

    public float penetCount; // 불 서브 스킬의 최대 맞는 개체수(얼만큼 관통시킬지.... 기획상으론 끝까지이지만 일단 데이터 기입)

    public float barrierDuration; // 물 서브 스킬 베리어의 지속시간(시간을 표현해야함으로 float으로 선언)

    public int barrierCount; // 물 서브 스킬의 막는 횟수 (횟수는 정수 int로 선언)
}