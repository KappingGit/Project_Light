using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이얄드 캐싱 기법 
internal static class YieldInstuctionCash
{
    public static readonly WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();  // 프레임이 종료될때까지만
    public static readonly WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate(); // 물리적인 업데이트까지만

    private static readonly Dictionary<float, WaitForSeconds> waitForSeconds = new Dictionary<float, WaitForSeconds>(); //맵과 같은것

    public static WaitForSeconds WaitForSeconds(float seconds)
    {
        WaitForSeconds wfs;
        if (!waitForSeconds.TryGetValue(seconds, out wfs)) // seconds는 키, 중복된 세컨드가 있으면
        {
            waitForSeconds.Add(seconds, wfs = new WaitForSeconds(seconds)); // Dictionary에 seconds가 없으면 추가해주는 장치
        }
        return wfs; // 중복된것을 리턴
    }
}
