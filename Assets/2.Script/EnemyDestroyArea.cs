using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyArea : MonoBehaviour
{

    private void Awake()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        Debug.Log("충돌 났습니다.");
        if (other.gameObject.CompareTag("Enemy")) // 해당 오브젝트에 닿으면 적이 사라지게 구현
        {
            Destroy(other.gameObject); // other.gameObject를 하면 부딪히는 해당(other) 오브젝트를 가리킨다.
        }
    }

}
