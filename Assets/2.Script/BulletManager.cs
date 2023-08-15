using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private void Awake()
    {

    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // 만약 가상의 벽에 닿으면 삭제
        if (other.gameObject.CompareTag("Tile")) // 나중에 태그 바꿀 것 => DestroyArea
        {
            Destroy(gameObject);
        }
    }
}
