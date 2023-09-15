using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬전환에 사용
using UnityEngine.UI; // UI기능에 사용

public class ChangeSceneManager : MonoBehaviour
{
    [SerializeField]
    private Image fadeImage;

    [SerializeField]
    private GameObject stageUI;

    private void Awake()
    {
        fadeImage.color = new Color(0, 0, 0, 1.0f); // 기본 초기화
        StartCoroutine(FadeInOut()); // 쌩으로 넣는 것은 하면안된다(코루틴이 지속적으로 처리가 되는 문제가 발생 단, 조건문을 활용하면 가능)
        StartCoroutine(UIforTime());
    }

    private void Update()
    {
        
    }

    private float fadeCount = 1f; // 페이드 인아웃에 사용될 변수

    // 속도 조절에 사용될 코루틴 YieldInstuctionCash: 캐싱 작업해 놓은것 불러오기
    IEnumerator FadeInOut()
    {
        while (0.0f < fadeCount)
        {
            fadeCount -= 0.01f;
            yield return YieldInstuctionCash.WaitForSeconds(0.01f); // 캐싱 불러오기
            fadeImage.color = new Color(0, 0, 0, fadeCount); // 투명도(알파값)은 1이 최대치이다.
        }
    }

    IEnumerator UIforTime() // 애니메이션 효과 시간 맞추기
    {
        yield return YieldInstuctionCash.WaitForSeconds(1.5f);
        stageUI.gameObject.SetActive(true);
        yield return YieldInstuctionCash.WaitForSeconds(2f);
        stageUI.gameObject.SetActive(false);
    }
}
