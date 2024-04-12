using System;
using UnityEngine;

// Token: 0x0200004E RID: 78
public class CCPlatform1 : CCPlatform
{
	// Token: 0x060001F2 RID: 498 RVA: 0x00016D08 File Offset: 0x00015108
	public CCPlatform1(GraphElement _graphElement)
		: base(_graphElement)
	{
		this.m_animator.Play(this.m_level1);
		this.m_motorcycleOffset = new Vector3(1f, 12.4f, 0f);
		this.m_offroaderOffset = new Vector3(0f, 14.4f, 0f);
		this.CalculateOffsets();
		if (this.m_minigame.m_editing)
		{
			this.m_overrideCC = 100f;
			base.SetOverrideCC(this.m_overrideCC);
		}
	}
}
