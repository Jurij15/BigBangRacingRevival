using System;
using UnityEngine;

// Token: 0x02000473 RID: 1139
public class DebugParticleTester : MonoBehaviour
{
	// Token: 0x06001F01 RID: 7937 RVA: 0x0017D63C File Offset: 0x0017BA3C
	private void Update()
	{
		if (Input.GetKeyDown(32))
		{
			if (this.toggle)
			{
				if (this.go != null)
				{
					this.go.SetActive(true);
				}
				this.pSystem.Play();
				this.toggle = !this.toggle;
			}
			else
			{
				if (this.go != null)
				{
					this.go.SetActive(false);
				}
				this.pSystem.Stop();
				this.toggle = !this.toggle;
			}
		}
	}

	// Token: 0x04002697 RID: 9879
	public GameObject go;

	// Token: 0x04002698 RID: 9880
	public ParticleSystem pSystem;

	// Token: 0x04002699 RID: 9881
	private bool toggle;
}
