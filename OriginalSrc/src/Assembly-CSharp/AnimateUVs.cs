using System;
using UnityEngine;

// Token: 0x0200017E RID: 382
public class AnimateUVs : MonoBehaviour
{
	// Token: 0x06000C9F RID: 3231 RVA: 0x0007A790 File Offset: 0x00078B90
	private void Update()
	{
		if (PsState.m_activeMinigame != null && PsState.m_activeMinigame.m_gamePaused)
		{
			return;
		}
		this.xOffset = ToolBox.getRolledValue(this.xOffset + Main.m_gameDeltaTime * this.xPan, 0f, 1f);
		this.yOffset = ToolBox.getRolledValue(this.yOffset + Main.m_gameDeltaTime * this.yPan, 0f, 1f);
		base.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(this.xOffset, this.yOffset));
	}

	// Token: 0x04000DA8 RID: 3496
	public float xPan;

	// Token: 0x04000DA9 RID: 3497
	public float yPan;

	// Token: 0x04000DAA RID: 3498
	private float xOffset;

	// Token: 0x04000DAB RID: 3499
	private float yOffset;
}
