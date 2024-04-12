using System;
using System.Collections.Generic;

// Token: 0x02000552 RID: 1362
public class BasicResourceGroup : IResourceGroup
{
	// Token: 0x060027C5 RID: 10181 RVA: 0x001AA927 File Offset: 0x001A8D27
	public BasicResourceGroup(string _identifier)
	{
		this.identifier = _identifier;
		this.resources = new List<IResource>();
		BasicResourceGroup._instanceCount++;
	}

	// Token: 0x170000D2 RID: 210
	// (get) Token: 0x060027C6 RID: 10182 RVA: 0x001AA94D File Offset: 0x001A8D4D
	// (set) Token: 0x060027C7 RID: 10183 RVA: 0x001AA955 File Offset: 0x001A8D55
	public string identifier
	{
		get
		{
			return this._identifier;
		}
		set
		{
			this._identifier = value;
		}
	}

	// Token: 0x170000D3 RID: 211
	// (get) Token: 0x060027C8 RID: 10184 RVA: 0x001AA95E File Offset: 0x001A8D5E
	// (set) Token: 0x060027C9 RID: 10185 RVA: 0x001AA966 File Offset: 0x001A8D66
	public List<IResource> resources
	{
		get
		{
			return this._resources;
		}
		set
		{
			this._resources = value;
		}
	}

	// Token: 0x060027CA RID: 10186 RVA: 0x001AA970 File Offset: 0x001A8D70
	~BasicResourceGroup()
	{
		BasicResourceGroup._instanceCount--;
	}

	// Token: 0x04002D61 RID: 11617
	private static int _instanceCount;

	// Token: 0x04002D62 RID: 11618
	private string _identifier;

	// Token: 0x04002D63 RID: 11619
	private List<IResource> _resources;
}
