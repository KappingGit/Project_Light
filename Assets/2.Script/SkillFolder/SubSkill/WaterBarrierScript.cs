using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBarrierScript : SubSkillScript
{

    protected override void SkillSpawnPos()
    {
        transform.position = new Vector3(shootPos.position.x, 1f, shootPos.position.z); // 베리어 위치
        
    }

    //실시간으로 따라오게
    protected override void RealTimePos()
    {
        transform.position = new Vector3(shootPos.position.x, 1f, shootPos.position.z); // 베리어 위치
    }

    protected override void SkillSpeed()
    {
        skillRig.velocity = new Vector3(0, 0, 0);
    }

    IEnumerator DurationPos()
    {
        int i = 0;
        Debug.Log("워터 쉴드 위치 조정");
        while (0 < SubSkillManager.instance.WaterBarrierType_Duration(7))
        {
            transform.position = new Vector3(shootPos.position.x, 1f, shootPos.position.z); // 베리어 위치
            yield return YieldInstuctionCash.WaitForSeconds(0.1f);

            i++;
            
        }

        SubSkillManager.instance.ReturnSkill(this); //베리어가 안깨지면 지속시간 끝나면 종료

        //yield return YieldInstuctionCash.WaitForSeconds(3f);


        StopCoroutine(DurationPos());
    }

}
