using System;
using UnityEngine;

// Token: 0x020004C2 RID: 1218
[Serializable]
public class Vertex3
{
	// Token: 0x060022A6 RID: 8870 RVA: 0x00190AA1 File Offset: 0x0018EEA1
	public Vertex3()
	{
	}

	// Token: 0x060022A7 RID: 8871 RVA: 0x00190AA9 File Offset: 0x0018EEA9
	public Vertex3(Vector2 _v2)
	{
		this.x = _v2.x;
		this.y = _v2.y;
		this.z = 0f;
	}

	// Token: 0x060022A8 RID: 8872 RVA: 0x00190AD6 File Offset: 0x0018EED6
	public Vertex3(Vector3 _v3)
	{
		this.x = _v3.x;
		this.y = _v3.y;
		this.z = _v3.z;
	}

	// Token: 0x060022A9 RID: 8873 RVA: 0x00190B05 File Offset: 0x0018EF05
	public Vector2 ToVector2()
	{
		return new Vector2(this.x, this.y);
	}

	// Token: 0x060022AA RID: 8874 RVA: 0x00190B18 File Offset: 0x0018EF18
	public Vector3 ToVector3()
	{
		return new Vector3(this.x, this.y, this.z);
	}

	// Token: 0x040028BF RID: 10431
	public float x;

	// Token: 0x040028C0 RID: 10432
	public float y;

	// Token: 0x040028C1 RID: 10433
	public float z;
}
