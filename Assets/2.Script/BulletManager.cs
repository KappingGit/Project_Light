using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class BulletManager : MonoBehaviour
{

    public static BulletManager instance; // 싱글톤화

    private PoolManager poolManager; //풀매니저 스크립트에 접근

    [SerializeField]
    private DB_Status statusDB; // 에셋화 되어있는 데이터테이블 가져오기

    // 플레이어의 공격력
    private float playerATK;

    //딕셔너리 테스트
    Dictionary<int, NomalAttack_WindSlash> nomalAttack_Wind;

    Dictionary<int, NomalAttack_WaterSlash> nomalAttack_Water;

    Dictionary<int, NomalAttack_FireSlash> nomalAttack_Fire;

    private void Awake()
    {
        poolManager = GetComponent<PoolManager>();

        if (BulletManager.instance == null)
        {
            Debug.Log("BulletManager.instance가 null상태입니다.");
            instance = this;
        }

        
        // 기본 평타 딕셔너리 처리
        WindSlash_TypeDictionary(); // 딕셔너리 추가

        WaterSlash_TypeDictionary();

        FireSlash_TypeDictionary();

        #region 딕셔너리 테스트 용 코드들 (일단 그때그때 어떻게 구현했나 확인하기 위함)

        //int nomalAttack_UID;

        //nomalAttack_UID = 0;

        // 바람 공격 데이터값 저장
        //for (int nomalAttack_UID = 0; nomalAttack_UID < 6; nomalAttack_UID++)
        //{
        //    // 가독성 높이기
        //    int indexLevel = nomalAttack_UID;

        //    int indexName = nomalAttack_UID;

        //    int indexDamage = nomalAttack_UID;

        //    nomalAttack_Wind.Add(nomalAttack_UID, new NomalAttack_WindSlash(statusDB.NomalAttack[indexLevel].typeLevel, statusDB.NomalAttack[indexName].name, statusDB.NomalAttack[indexDamage].singleDamage));

        //}

        //// 물 공격 데이터값 저장
        //for (int nomalAttack_UID = 6; nomalAttack_UID < 12; nomalAttack_UID++)
        //{

        //    int indexLevel = nomalAttack_UID;

        //    int indexName = nomalAttack_UID;

        //    int indexDamage = nomalAttack_UID;

        //    nomalAttack_Water.Add(nomalAttack_UID, new NomalAttack_WaterSlash(statusDB.NomalAttack[indexLevel].typeLevel, statusDB.NomalAttack[indexName].name, statusDB.NomalAttack[indexDamage].speedDown));

        //}

        //// 불 공격 데이터 값 저장
        //for (int nomalAttack_UID = 12; nomalAttack_UID < 18; nomalAttack_UID++)
        //{

        //    int indexLevel = nomalAttack_UID;

        //    int indexName = nomalAttack_UID;

        //    int indexDamage = nomalAttack_UID;

        //    nomalAttack_Fire.Add(nomalAttack_UID, new NomalAttack_FireSlash(statusDB.NomalAttack[indexLevel].typeLevel, statusDB.NomalAttack[indexName].name, statusDB.NomalAttack[indexDamage].spreadDamage));

        //}


        // 데이터 출력관련해서 아래 문단 참고할 것
        //NomalAttack_WindSlash windData = nomalAttack_Wind[1];

        //Debug.Log(windData.singleDamage);

        //windData.CheckData(); //데이터 확인용

        //NomalAttack_WaterSlash waterData = nomalAttack_Water[7];

        //waterData.CheckData();

        //Debug.Log("무기 UID 1의 데이터는? : ");

        //Debug.Log("무기 UID 7의 데이터는? : " + nomalAttack_Water[7]);

        //Debug.Log("무기 UID 13의 데이터는? : " + nomalAttack_Fire[13]);


        //Debug.Log("바람 기본 공격 레벨은? : " + nomalAttack_Wind[0]);

        //Debug.Log("바람 기본 공격 이름은? : " + nomalAttack_Wind[0]);

        //Debug.Log("바람 기본 공격 단일 공격 데미지는? : " + nomalAttack_Wind[0]);

        #endregion


    }



    private void Update()
    {

    }



    public GameObject GetPoolBullet(int bulletIndex)
    {
        // 풀링되어있는 총알 불러오기 0번째 인덱스 총알을 불러옴
        BulletScript newBullet = poolManager.GetFromPool<BulletScript>(bulletIndex);

        GameObject newBulletObj_01 = newBullet.gameObject;

        return newBulletObj_01;
    }

    public void ReturnBullet(BulletScript clone)
    {
        poolManager.TakeToPool<BulletScript>(clone.idName, clone);
    }





    #region 바람평타 딕셔너리화

    private void WindSlash_TypeDictionary() // 윈드 슬래쉬의 데이터를 뽑아온 다음 리스트화 시킴
    {
        nomalAttack_Wind = new Dictionary<int, NomalAttack_WindSlash>();

        // 바람 공격 데이터값 저장
        for (int nomalAttack_UID = 0; nomalAttack_UID < 6; nomalAttack_UID++)
        {
            // 가독성 높이기
            int indexLevel = nomalAttack_UID;

            int indexName = nomalAttack_UID;

            int indexDamage = nomalAttack_UID;

            nomalAttack_Wind.Add(nomalAttack_UID, new NomalAttack_WindSlash(statusDB.NomalAttack[indexLevel].typeLevel, statusDB.NomalAttack[indexName].name, statusDB.NomalAttack[indexDamage].singleDamage));

        }
    }

    public float WindSlashTypeDamage(int indexNum)
    {
        // 바람 속성 기본 공격(평타)의 효과
        // 단일 대상에서 공격력*퍼센트의 단일 대미지를 준다라는 형식이 필요

        playerATK = statusDB.PlayerStatus[0].playerDamage; // 플레이어의 공격력 패시브로 얻는 선택지는 아직 미구현이니 인덱스 0으로 고정

        NomalAttack_WindSlash windData = nomalAttack_Wind[indexNum];

        //최종 데미지
        float finalDamage = playerATK * windData.singleDamage;

        //if (target.TryGetComponent<IDamage>(out IDamage damage))
        //{
        //    damage.TargetDamage(finalDamage);
        //}

        //임시 반환
        
        Debug.Log("BulletManager 스크립트의 WindSlashTypeDamage() 함수 실행");
        Debug.Log("바람 기본 평타 최종 데미지 : " + finalDamage);

        return finalDamage;

    }

    #endregion

    #region 물평타 딕셔너리화


    private void WaterSlash_TypeDictionary() // 윈드 슬래쉬의 데이터를 뽑아온 다음 리스트화 시킴
    {
        nomalAttack_Water = new Dictionary<int, NomalAttack_WaterSlash>();

        // 물 공격 데이터값 저장
        for (int nomalAttack_UID = 6; nomalAttack_UID < 12; nomalAttack_UID++)
        {

            int indexLevel = nomalAttack_UID;

            int indexName = nomalAttack_UID;

            int indexDamage = nomalAttack_UID;

            nomalAttack_Water.Add(nomalAttack_UID, new NomalAttack_WaterSlash(statusDB.NomalAttack[indexLevel].typeLevel, statusDB.NomalAttack[indexName].name, statusDB.NomalAttack[indexDamage].speedDown));

        }
    }

    public float WaterSlashTypeDamage(int indexNum) // 여기 값이 7~11이 들어가야지 유효(6은 없는 디폴트 물평타)
    {

        playerATK = statusDB.PlayerStatus[0].playerDamage; // 플레이어의 공격력 패시브로 얻는 선택지는 아직 미구현이니 인덱스 0으로 고정

        //최종 데미지
        float finalDamage = playerATK;
        //float finalDamage = 1f;
        //임시 반환
        //float path = 0.5f;

        Debug.Log("BulletManager 스크립트의 WaterSlashTypeDamage() 함수 실행");
        Debug.Log("물 기본 평타 최종 데미지 : " + finalDamage);


        return finalDamage;

    }

    public float WaterSlashType_SlowEffect(int indexNum) //물 평타의 슬로우 효과
    {
        NomalAttack_WaterSlash waterData = nomalAttack_Water[indexNum];


        float speedDownEffect = waterData.speedDown;
        Debug.Log("물 기본 평타 슬로우 : " + speedDownEffect);

        return speedDownEffect;
    }

    #endregion


    #region 불평타 딕셔너리화

    private void FireSlash_TypeDictionary() // 파이어 슬래쉬의 데이터를 뽑아온 다음 리스트화 시킴
    {
        nomalAttack_Fire = new Dictionary<int, NomalAttack_FireSlash>();

        // 불 공격 데이터 값 저장
        for (int nomalAttack_UID = 12; nomalAttack_UID < 18; nomalAttack_UID++)
        {

            int indexLevel = nomalAttack_UID;

            int indexName = nomalAttack_UID;

            int indexDamage = nomalAttack_UID;

            nomalAttack_Fire.Add(nomalAttack_UID, new NomalAttack_FireSlash(statusDB.NomalAttack[indexLevel].typeLevel, statusDB.NomalAttack[indexName].name, statusDB.NomalAttack[indexDamage].spreadDamage));

        }
    }

    public float FireSlashTypeDamage(int indexNum) // 여기 값이 13~17이 들어가야지 유효
    {

        playerATK = statusDB.PlayerStatus[0].playerDamage; // 플레이어의 공격력 패시브로 얻는 선택지는 아직 미구현이니 인덱스 0으로 고정

        NomalAttack_FireSlash fireData = nomalAttack_Fire[indexNum];

        //최종 데미지
        float finalDamage = playerATK + (playerATK * fireData.spreadDamage);

        //임시 반환
        //float path = 0.5f;
        Debug.Log("스플데미지 수치   " + fireData.spreadDamage);

        Debug.Log("BulletManager 스크립트의 FireSlashTypeDamage() 함수 실행");
        Debug.Log("불 기본 평타 최종 데미지 : " + finalDamage);


        return finalDamage;

    }

    public float FireSlash_SpreadDamage(int indexNum, Vector3 center, float radius)
    {
        playerATK = statusDB.PlayerStatus[0].playerDamage; // 플레이어의 공격력 패시브로 얻는 선택지는 아직 미구현이니 인덱스 0으로 고정

        NomalAttack_FireSlash fireData = nomalAttack_Fire[indexNum];

        //최종 데미지
        float finalDamage = playerATK + (playerATK * fireData.spreadDamage);

        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            Debug.Log("범위 색출");
            //finalDamage = playerATK + (playerATK * fireData.spreadDamage);
            i++;
        }

        //임시 반환
        //float path = 0.5f;
        Debug.Log("스플데미지 수치   " + fireData.spreadDamage);

        Debug.Log("BulletManager 스크립트의 FireSlashTypeDamage() 함수 실행");
        Debug.Log("불 기본 평타 최종 데미지 : " + finalDamage);


        return finalDamage;
    }

    #endregion


}
