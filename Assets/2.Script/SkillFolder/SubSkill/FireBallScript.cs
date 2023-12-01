using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : SubSkillScript
{
    
    protected override void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.CompareTag("Enemy")) // 파이어볼은 관통해야함으로 재선언
        {

            StartCoroutine(PenetTime());

            HitEffect(skillTypeBtn); //해당 스킬 타입에 맞는 히트 이펙트를 소환
                        
        }

    }


    IEnumerator PenetTime()
    {
        yield return YieldInstuctionCash.WaitForSeconds(3f);

        //Debug.Log("파이어볼 반환");

        SubSkillManager.instance.ReturnSkill(this);

        StopCoroutine(PenetTime());
    }
}
