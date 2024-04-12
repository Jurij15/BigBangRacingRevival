using System;
using System.Collections.Generic;

// Token: 0x02000553 RID: 1363
public interface IResourceGroup
{
	// Token: 0x170000D4 RID: 212
	// (get) Token: 0x060027CB RID: 10187
	// (set) Token: 0x060027CC RID: 10188
	string identifier { get; set; }

	// Token: 0x170000D5 RID: 213
	// (get) Token: 0x060027CD RID: 10189
	// (set) Token: 0x060027CE RID: 10190
	List<IResource> resources { get; set; }
}
