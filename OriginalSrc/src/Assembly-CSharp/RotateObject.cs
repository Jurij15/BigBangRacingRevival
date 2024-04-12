using System;
using UnityEngine;

// Token: 0x02000182 RID: 386
public class RotateObject : MonoBehaviour
{
	// Token: 0x06000CA9 RID: 3241 RVA: 0x0007AA30 File Offset: 0x00078E30
	private void Update()
	{
		if (base.GetComponent<Renderer>() != null && !base.GetComponent<Renderer>().isVisible)
		{
			return;
		}
		if (PsState.m_activeMinigame != null && PsState.m_activeMinigame.m_gamePaused)
		{
			return;
		}
		base.transform.Rotate(new Vector3(this.rotateX * Main.m_gameDeltaTime, this.rotateY * Main.m_gameDeltaTime, this.rotateZ * Main.m_gameDeltaTime));
	}

	// Token: 0x04000DB5 RID: 3509
	public float rotateX;

	// Token: 0x04000DB6 RID: 3510
	public float rotateY;

	// Token: 0x04000DB7 RID: 3511
	public float rotateZ;
}
