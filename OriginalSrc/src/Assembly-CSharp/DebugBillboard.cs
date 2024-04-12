using System;
using UnityEngine;

// Token: 0x02000185 RID: 389
public class DebugBillboard : MonoBehaviour
{
	// Token: 0x06000CAF RID: 3247 RVA: 0x0007AB24 File Offset: 0x00078F24
	private void Update()
	{
		foreach (Transform transform in this.billboards)
		{
			transform.LookAt(this.planetCamera.transform.position);
		}
	}

	// Token: 0x04000DB9 RID: 3513
	public Transform[] billboards;

	// Token: 0x04000DBA RID: 3514
	public Camera planetCamera;

	// Token: 0x04000DBB RID: 3515
	public Vector3 lookAtDirection;
}
