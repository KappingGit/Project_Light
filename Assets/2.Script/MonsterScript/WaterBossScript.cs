using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBossScript : BossScript
{
    protected override void BossPatternSetting() // 여기서 보스 패턴 관련으로 작업
    {
        if (!isPurification) // 보스가 죽는 연출동안 3번째 패턴 못나오게 하기
        {

            
            if (BossPattern_Wind.instance.isStun) // 보스 기절 상태시...
            {
                bossAnim.SetBool("isStun", true);
            }
            else if (!BossPattern_Wind.instance.isStun)
            {
                bossAnim.SetBool("isStun", false);
            }

            
        }
    }

    
}
