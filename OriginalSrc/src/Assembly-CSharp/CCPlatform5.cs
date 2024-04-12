using System;
using UnityEngine;

// Token: 0x02000052 RID: 82
public class CCPlatform5 : CCPlatform
{
	// Token: 0x060001F6 RID: 502 RVA: 0x00016F28 File Offset: 0x00015328
	public CCPlatform5(GraphElement _graphElement)
		: base(_graphElement)
	{
		this.m_motorcycleOffset = new Vector3(1f, 18.3f, 0f);
		this.m_offroaderOffset = new Vector3(0f, 22.2f, 0f);
		this.CalculateOffsets();
		this.m_animator.Play(this.m_level5);
		if (this.m_minigame.m_editing)
		{
			this.m_overrideCC = 1200f;
			base.SetOverrideCC(this.m_overrideCC);
		}
	}
}
