using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickWarningLine : MonoBehaviour
{

    private MeshRenderer warningLineMesh;

    private bool isWarnnigLineActive;

    private void Awake()
    {
        warningLineMesh = GetComponent<MeshRenderer>();

        isWarnnigLineActive = false;

        StartCoroutine(FlashingLight());
    }

    private void Update()
    {
        if (!isWarnnigLineActive)
        {
            isWarnnigLineActive = true; // 코루틴 브레이킹
            StartCoroutine(FlashingLight());
        }
    }

   
    private float transparencyCount; // 투명도 카운트

    private int flashCount;

    IEnumerator FlashingLight() // 깜빡거리는 효과
    {
        //Debug.Log("경고 라인 코루틴 실행");

        flashCount = 0; // 몇번정도 깜빡일 것이냐

        while (flashCount < 3) // while문 조심히 다룰것...
        {
            transparencyCount = 1.0f;
            while (transparencyCount < 1.0f)
            {
                transparencyCount += 0.1f;
                yield return YieldInstuctionCash.WaitForSeconds(0.05f);
                warningLineMesh.material.color = new Color(245f / 255f, 96f / 255f, 96f / 255f, transparencyCount);
            }

            while (transparencyCount > 0.0f)
            {
                transparencyCount -= 0.1f;
                yield return YieldInstuctionCash.WaitForSeconds(0.05f);
                warningLineMesh.material.color = new Color(245f / 255f, 96f / 255f, 96f / 255f, transparencyCount);
            }
            flashCount++;
        }

        isWarnnigLineActive = false;

        gameObject.SetActive(false);

        //Debug.Log("경고 라인 코루틴 종료");
        yield return YieldInstuctionCash.WaitForSeconds(0.1f);

        StopCoroutine(FlashingLight());
    }
}
