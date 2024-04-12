using System;
using UnityEngine;

// Token: 0x02000183 RID: 387
public class SetAmbientColor : MonoBehaviour
{
	// Token: 0x06000CAB RID: 3243 RVA: 0x0007AAD4 File Offset: 0x00078ED4
	private void Start()
	{
		RenderSettings.ambientLight = this.ambientColor;
	}

	// Token: 0x04000DB8 RID: 3512
	public Color ambientColor = new Color(0f, 0f, 0f, 0f);
}
