using System;
using UnityEngine;

// Token: 0x02000050 RID: 80
public class CCPlatform3 : CCPlatform
{
	// Token: 0x060001F4 RID: 500 RVA: 0x00016E18 File Offset: 0x00015218
	public CCPlatform3(GraphElement _graphElement)
		: base(_graphElement)
	{
		this.m_motorcycleOffset = new Vector3(1f, 14.4f, 0f);
		this.m_offroaderOffset = new Vector3(0f, 17.2f, 0f);
		this.CalculateOffsets();
		this.m_animator.Play(this.m_level3);
		if (this.m_minigame.m_editing)
		{
			this.m_overrideCC = 500f;
			base.SetOverrideCC(this.m_overrideCC);
		}
	}
}
