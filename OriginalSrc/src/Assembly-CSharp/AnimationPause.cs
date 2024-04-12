using System;
using UnityEngine;

// Token: 0x0200017F RID: 383
public class AnimationPause : MonoBehaviour
{
	// Token: 0x06000CA1 RID: 3233 RVA: 0x0007A838 File Offset: 0x00078C38
	private void Update()
	{
		if (PsState.m_physicsPaused && !this.animPaused)
		{
			this.animSpeed = base.GetComponent<Animation>()[base.GetComponent<Animation>().clip.name].speed;
			base.GetComponent<Animation>()[base.GetComponent<Animation>().clip.name].speed = 0f;
			this.animPaused = true;
		}
		else if (!PsState.m_physicsPaused && this.animPaused)
		{
			base.GetComponent<Animation>()[base.GetComponent<Animation>().clip.name].speed = this.animSpeed;
			this.animPaused = false;
		}
	}

	// Token: 0x04000DAC RID: 3500
	private float animSpeed;

	// Token: 0x04000DAD RID: 3501
	private bool animPaused;
}
