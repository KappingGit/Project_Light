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
        
       

    }

    

    



}
