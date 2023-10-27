[System.Serializable]
//엑셀 테이블에 저장된 데이터의 행과 동일한 변수타입, 
//열과 동일한 변수들을 정의하는 직렬화 클래스

public class DB_StatusEntity
{
    //기존 클래스명 하나로 통일했었음... DB_StatusEntity
    // 클래스를 늘리는 과정에서 엑셀에서 reimport 과정을 거쳐라...
    // 각 테이블별 수치 정수, 실수 데이터값(해당 값을 만들지 않으면)
    // 엑셀 데이터 에셋화(DB_Status오브젝트 에셋)에 데이터가 기입이 안된다.
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

    public int indexLevel02;

    public int monsterHP;

    public float monsterSpeed;

    public int monsterGold;

    public float monsterEXP;

    public int monsterDamage; 

}

[System.Serializable]
public class DB_StatusEntity_03
{
    // 공격 타입 인덱스

    public int indexUID;

    public int weaponType;

    public string name;

    public float typeLevel;

    public float coolTime;

    public float typeAbility;

}