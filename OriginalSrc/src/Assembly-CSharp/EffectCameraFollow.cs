using System;
using UnityEngine;

// Token: 0x02000180 RID: 384
public class EffectCameraFollow : MonoBehaviour
{
	// Token: 0x06000CA3 RID: 3235 RVA: 0x0007A8FB File Offset: 0x00078CFB
	private void Start()
	{
		this.sceneCamera = GameObject.FindGameObjectWithTag("MainCamera");
		this.transPos = base.transform.position;
		this.transPos.z = this.zOffset;
	}

	// Token: 0x06000CA4 RID: 3236 RVA: 0x0007A930 File Offset: 0x00078D30
	private void Update()
	{
		if (this.sceneCamera != null)
		{
			this.transPos.x = this.sceneCamera.transform.position.x;
			if (this.vertical)
			{
				this.transPos.y = this.sceneCamera.transform.position.y;
			}
			base.transform.position = this.transPos;
		}
	}

	// Token: 0x04000DAE RID: 3502
	private GameObject sceneCamera;

	// Token: 0x04000DAF RID: 3503
	private Vector3 transPos;

	// Token: 0x04000DB0 RID: 3504
	public float yOffset;

	// Token: 0x04000DB1 RID: 3505
	public float zOffset;

	// Token: 0x04000DB2 RID: 3506
	public bool vertical;
}
