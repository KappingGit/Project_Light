using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLenghtTest : MonoBehaviour
{
    private BoxCollider boxCollider;

    // 오브젝트 길이 구하려고 만든 스크립트
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();

        
    }

    private void Update()
    {
        Debug.Log("Z축 길이 :  " + boxCollider.size.z);
    }
}
