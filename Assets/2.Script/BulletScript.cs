using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class BulletScript : MonoBehaviour, IPoolObject
{
    public static BulletScript instance;

    private PoolManager poolManager;

    private Transform bulletTrans;

    private Rigidbody bulletRig;

    private float bulletSpeed = 40.0f; // 투사체 속도

    [SerializeField]
    private Transform shotPos; // 총알이 발사될 위치

    private void Awake()
    {
        bulletRig = GetComponent<Rigidbody>();

        if (BulletScript.instance == null)
        {
            Debug.Log("BulletScript.instance가 Null상태입니다");
            instance = this;
        }

       
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
        }
    }

    private void BulletSpeed()// 총알의 투사체 힘
    {
        bulletRig.AddForce(new Vector3(0, 0, bulletSpeed), ForceMode.Impulse); 
    }

    private void InitBullet() // 초기화 로직 함수
    {
        bulletTrans.transform.position = shotPos.transform.position; // 총알이 생성되었을때 위치
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
        
    }
}
