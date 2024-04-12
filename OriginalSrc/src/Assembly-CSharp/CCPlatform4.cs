using System;
using UnityEngine;

// Token: 0x02000051 RID: 81
public class CCPlatform4 : CCPlatform
{
	// Token: 0x060001F5 RID: 501 RVA: 0x00016EA0 File Offset: 0x000152A0
	public CCPlatform4(GraphElement _graphElement)
		: base(_graphElement)
	{
		this.m_motorcycleOffset = new Vector3(1f, 16.1f, 0f);
		this.m_offroaderOffset = new Vector3(0f, 19.3f, 0f);
		this.CalculateOffsets();
		this.m_animator.Play(this.m_level4);
		if (this.m_minigame.m_editing)
		{
			this.m_overrideCC = 800f;
			base.SetOverrideCC(this.m_overrideCC);
		}
	}
}
