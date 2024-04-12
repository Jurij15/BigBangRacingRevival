using System;
using UnityEngine;

// Token: 0x0200004F RID: 79
public class CCPlatform2 : CCPlatform
{
	// Token: 0x060001F3 RID: 499 RVA: 0x00016D90 File Offset: 0x00015190
	public CCPlatform2(GraphElement _graphElement)
		: base(_graphElement)
	{
		this.m_motorcycleOffset = new Vector3(1f, 13.2f, 0f);
		this.m_offroaderOffset = new Vector3(0f, 15.5f, 0f);
		this.CalculateOffsets();
		this.m_animator.Play(this.m_level2);
		if (this.m_minigame.m_editing)
		{
			this.m_overrideCC = 250f;
			base.SetOverrideCC(this.m_overrideCC);
		}
	}
}
