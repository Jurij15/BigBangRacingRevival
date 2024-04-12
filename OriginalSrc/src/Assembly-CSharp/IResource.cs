using System;

// Token: 0x02000551 RID: 1361
public interface IResource
{
	// Token: 0x170000CF RID: 207
	// (get) Token: 0x060027BE RID: 10174
	// (set) Token: 0x060027BF RID: 10175
	IResourceGroup resourceGroup { get; set; }

	// Token: 0x170000D0 RID: 208
	// (get) Token: 0x060027C0 RID: 10176
	// (set) Token: 0x060027C1 RID: 10177
	string identifier { get; set; }

	// Token: 0x170000D1 RID: 209
	// (get) Token: 0x060027C2 RID: 10178
	// (set) Token: 0x060027C3 RID: 10179
	object ResourceObject { get; set; }

	// Token: 0x060027C4 RID: 10180
	void Unload();
}
