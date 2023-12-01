using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern_Water : BossPattern_Wind
{
    protected override void Pattern01()
    {
        base.Pattern01();
    }

    protected override void Pattern02()
    {
        
    }

    protected override void Pattern03()
    {
        
    }

    protected override GameObject SpawnPattern()
    {
        GameObject newEnemy01 = EnemyManager.instance.Spawn(); // 물슬라임 소환

        //GameObject newEnemyObj01 = newEnemy01.gameObject;

        return newEnemy01; // BossPattern_Wind.cs - SpawnPattertn()의 지역함수
    }
}
