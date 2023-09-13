using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class BulletScript : MonoBehaviour, IPoolObject
{
    
    public static BulletScript instance;

    private Rigidbody bulletRig;

    private float bulletSpeed = 40.0f; // 투사체 속도

    private void Awake()
    {
        bulletRig = GetComponent<Rigidbody>();

        if (BulletScript.instance == null)
        {
            Debug.Log("BulletScript.instance가 Null상태입니다");
            instance = this;
        }
        //Debug.Log("총알 위치: x = " + transform.position.x + "    y = " + transform.position.y + "    z = " + transform.position.z);
        

    }

    private void Update()
    {
        BulletSpeed();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon")) // 어느 곳에서 충돌하면 총알 사라짐
        {
            OnTargetReached();
            Debug.Log("한계점 벽에 닿았습니다.");
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            OnTargetReached();
            Debug.Log("몬스터와 충돌했습니다");
        }
    }

    [SerializeField]
    private Transform shootPos; // 총알이 발사될 위치 문제점 해당 transform이 윈드 슬래쉬에 겹쳐지는 문제점이 발생

    private void BulletSpawnPos()
    {
        transform.position = new Vector3(shootPos.position.x, shootPos.position.y, shootPos.position.z); // 총알이 생성되었을때 위치
    }

    private void BulletSpeed()// 총알의 투사체 힘
    {
        bulletRig.velocity = new Vector3(0, 0, bulletSpeed);
    }

    private void InitBullet() // 초기화 로직 함수
    {
        BulletSpawnPos();
        
    }

    private void OnTargetReached() // 반환 작업용 함수
    {
        BulletManager.instance.ReturnBullet(this);
        
    }

    // 인터페이스 IPoolObject을 명시적으로 구현
    public void OnCreatedInPool()
    {
        // 해당 오브젝트가 처음 생성됐을때 실행 함수
        
    }

    // 인터페이스 IPoolObject을 명시적으로 구현
    public void OnGettingFromPool() //풀에서 관련된 풀 오브젝트를 가져올때...
    {
        // 해당 오브젝트가 가져올때마다 실행

        InitBullet(); // 재사용하기 위해 초기화 로직(총알 기본 상태값)을 작성
        //Debug.Log("총알을 초기화시켰습니다.");

    }
}
