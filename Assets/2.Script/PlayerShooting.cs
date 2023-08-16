using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab; // 총알 오브젝트 프리팹

    private GameObject bullet;

    [SerializeField]
    private Transform shotPos; // 총알이 발사될 위치

    private float shotSpeed = 40.0f; // 공격 속도

    private void Awake()
    {
        bullet = GetComponent<GameObject>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bullet = Instantiate(bulletPrefab); //bullet 게임 오브젝트에 bulletPrefab의 오브젝트를 클론화

            bullet.transform.position = shotPos.transform.position;

            bullet.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, shotSpeed), ForceMode.Impulse); //해당 오브젝트에 Rigidbody에 접근 
        }
    }

}
