using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 오브젝트 풀 매니저 코드리뷰
enum objectType // 오브젝트 타입 인덱스 설정
{
    
};

public class MyObjectPoolManager : MonoBehaviour
{
    private static MyObjectPoolManager instance; // 해당 오브젝트의 인스터스 값
    public static MyObjectPoolManager Inst
    {
        get
        {
            return instance; // 인스턴스 값을 반환
        }
    }

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }

    //public List<MyObjectPool> pools; // 오브젝트 풀
}
