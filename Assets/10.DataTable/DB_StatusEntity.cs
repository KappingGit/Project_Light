[System.Serializable]
//엑셀 테이블에 저장된 데이터의 행과 동일한 변수타입, 
//열과 동일한 변수들을 정의하는 직렬화 클래스

public class DB_StatusEntity
{
    // 각 테이블별 수치 정수, 실수 데이터값(해당 값을 만들지 않으면)
    // 엑셀 데이터 에셋화(DB_Status오브젝트 에셋)에 데이터가 기입이 안된다.
    public int indexNum01; // 데이터 테이블 열 번호

    public int playerHP; // 캐릭터 hp

    public float attackRate; // 공격속도

    public float addEXP; // 추가 경험치 획득

    public float coolDownTime; // 쿨타임

    public float attackDamage; // 공격력

    // 몬스터 수치

    public int indexNum02;

    public int monsterHP;

    public float monsterSpeed;

    public int monsterGold;

    public float monsterEXP;

    // 열거형
    //public enum monsterState
    //{
    //    indexNum02,

    //    monsterHP,

    //    monsterSpeed,

    //    monsterGold,

    //    monsterEXP,
    //}

}
