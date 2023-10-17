using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    float timer;

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            instance = this;
        }

    }

    private void Update()
    {
        
        //timer += Time.deltaTime;

        //if (timer > 0.1f)
        //{
        //    timer = 0f;
        //    //Spawn();
        //}

    }

    

    // 여기서 난이도를 관리할 듯

    //public virtual void Spawn()
    //{
    //    //..
    //}

    //public virtual void ReturnPool(EnemyScript clone)
    //{
    //    //..
    //}



}
