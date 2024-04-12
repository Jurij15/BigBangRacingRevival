using System;

// Token: 0x020004E7 RID: 1255
public interface IPoolable
{
	// Token: 0x170000BE RID: 190
	// (get) Token: 0x0600234F RID: 9039
	// (set) Token: 0x06002350 RID: 9040
	int m_index { get; set; }

	// Token: 0x06002351 RID: 9041
	void Reset();

	// Token: 0x06002352 RID: 9042
	void Destroy();
}
