using System;
using UnityEngine;

// Token: 0x020001BE RID: 446
public class VisualsReindeerHat : MonoBehaviour
{
	// Token: 0x06000DAB RID: 3499 RVA: 0x000805CC File Offset: 0x0007E9CC
	private void ToggleNose()
	{
		if (this.noseLit.activeSelf)
		{
			this.noseLit.SetActive(false);
			this.noseUnlit.SetActive(true);
		}
		else
		{
			this.noseLit.SetActive(true);
			this.noseUnlit.SetActive(false);
		}
	}

	// Token: 0x06000DAC RID: 3500 RVA: 0x00080620 File Offset: 0x0007EA20
	private void Update()
	{
		if (this.blinkTimer <= 0f)
		{
			this.blinkTimer += this.noseBlinkSpeed;
			this.ToggleNose();
		}
		else
		{
			this.blinkTimer -= Main.GetDeltaTime();
		}
	}

	// Token: 0x04001041 RID: 4161
	public GameObject noseLit;

	// Token: 0x04001042 RID: 4162
	public GameObject noseUnlit;

	// Token: 0x04001043 RID: 4163
	public float noseBlinkSpeed = 1f;

	// Token: 0x04001044 RID: 4164
	private float blinkTimer;
}
