using System;
using UnityEngine;

// Token: 0x02000188 RID: 392
public class DebugShowObjectBounds : MonoBehaviour
{
	// Token: 0x06000CB7 RID: 3255 RVA: 0x0007ACD1 File Offset: 0x000790D1
	private void Start()
	{
		this.rend = base.GetComponent<Renderer>();
	}

	// Token: 0x06000CB8 RID: 3256 RVA: 0x0007ACE0 File Offset: 0x000790E0
	private void OnDrawGizmosSelected()
	{
		Vector3 center = this.rend.bounds.center;
		Vector3 vector;
		vector..ctor(this.rend.bounds.extents.x, this.rend.bounds.extents.y, this.rend.bounds.extents.z);
		Gizmos.color = Color.white;
		Gizmos.DrawWireCube(center, vector * 2f);
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(center, this.pivotSize);
	}

	// Token: 0x04000DC2 RID: 3522
	public Renderer rend;

	// Token: 0x04000DC3 RID: 3523
	public float pivotSize = 1f;
}
