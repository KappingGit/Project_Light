using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] // 작성된 스크립트가 바로 적용되게끔 하는 것
public class WorldCurver : MonoBehaviour
{
	[Range(-0.1f, 0.1f)]
	public float curveStrength = 0.01f;

    int m_CurveStrengthID;

    private void OnEnable()
    {
        m_CurveStrengthID = Shader.PropertyToID("_CurveStrength");
    }

	void Update()
	{
		Shader.SetGlobalFloat(m_CurveStrengthID, curveStrength);
	}
}
