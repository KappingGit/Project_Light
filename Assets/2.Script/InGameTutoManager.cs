using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 인게임용 튜토리얼
public class InGameTutoManager : TutorialManager
{
    public override void StartGame()
    {
        // 인게임에서는 아무 화면 버튼 클릭 이벤트는 필요없으니 재정의 시키기
    }

    public override void SkipBtn() // 여기서는 나가는 버튼으로 변경
    {
        
    }

}
