using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBarrierScript : SubSkillScript
{

    protected override void SkillSpawnPos()
    {
        transform.position = new Vector3(shootPos.position.x, 1f, shootPos.position.z); // 베리어 위치

        StartCoroutine(DurationPos());

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
        //int i = 0;
        //Debug.Log("워터 쉴드 위치 조정");
        //while (i < SubSkillManager.instance.WaterBarrierType_Duration(7))
        //{
        //    transform.position = new Vector3(shootPos.position.x, 1f, shootPos.position.z); // 베리어 위치
        //    yield return YieldInstuctionCash.WaitForSeconds(0.1f);

        //    i++;
            
        //}

        yield return YieldInstuctionCash.WaitForSeconds(3f);

        SubSkillManager.instance.ReturnSkill(this); //베리어가 안깨지면 지속시간 끝나면 종료

        //yield return YieldInstuctionCash.WaitForSeconds(3f);


        StopCoroutine(DurationPos());
    }

    protected override void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Tile")) // 어느 곳에서 충돌하면 총알 사라짐
        //{
        //    OnTargetReached();
        //    Debug.Log("한계점 벽에 닿았습니다.");
        //}

        if (other.gameObject.CompareTag("Enemy")) // 어느 곳에서 충돌하면 총알 사라짐
        {
            

            StartCoroutine(Barrier_Effect());

            HitEffect(skillTypeBtn); //해당 스킬 타입에 맞는 히트 이펙트를 소환


        }

        if (other.gameObject.CompareTag("Missile")) // 어느 곳에서 충돌하면 총알 사라짐
        {
            //SubSkillManager.instance.ReturnSkill(this);

            StartCoroutine(Barrier_Effect());

            HitEffect(skillTypeBtn); //해당 스킬 타입에 맞는 히트 이펙트를 소환


        }

    }

    IEnumerator Barrier_Effect()
    {
        PlayerStatus.instance.barrierInvincible.enabled = false;

        yield return YieldInstuctionCash.WaitForSeconds(0.1f);

        PlayerStatus.instance.barrierInvincible.enabled = true;

        SubSkillManager.instance.ReturnSkill(this);

        StopCoroutine(Barrier_Effect());
    }

}
