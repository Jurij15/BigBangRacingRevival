using System;

// Token: 0x020004F6 RID: 1270
public class CameraLayer : IPoolable
{
	// Token: 0x170000BF RID: 191
	// (get) Token: 0x0600248A RID: 9354 RVA: 0x00193443 File Offset: 0x00191843
	// (set) Token: 0x0600248B RID: 9355 RVA: 0x0019344B File Offset: 0x0019184B
	public int m_index
	{
		get
		{
			return this._index;
		}
		set
		{
			this._index = value;
		}
	}

	// Token: 0x0600248C RID: 9356 RVA: 0x00193454 File Offset: 0x00191854
	public void Reset()
	{
	}

	// Token: 0x0600248D RID: 9357 RVA: 0x00193456 File Offset: 0x00191856
	public void Destroy()
	{
	}

	// Token: 0x04002A68 RID: 10856
	private int _index;
}
