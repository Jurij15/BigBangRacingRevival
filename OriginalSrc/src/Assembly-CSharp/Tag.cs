using System;

// Token: 0x02000515 RID: 1301
public class Tag : IPoolable
{
	// Token: 0x0600262E RID: 9774 RVA: 0x001A4942 File Offset: 0x001A2D42
	public Tag()
	{
	}

	// Token: 0x0600262F RID: 9775 RVA: 0x001A494A File Offset: 0x001A2D4A
	public Tag(string _tag, Entity _e)
	{
		this.m_tag = _tag;
		this.p_entity = _e;
	}

	// Token: 0x170000C1 RID: 193
	// (get) Token: 0x06002630 RID: 9776 RVA: 0x001A4960 File Offset: 0x001A2D60
	// (set) Token: 0x06002631 RID: 9777 RVA: 0x001A4968 File Offset: 0x001A2D68
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

	// Token: 0x170000C2 RID: 194
	// (get) Token: 0x06002632 RID: 9778 RVA: 0x001A4971 File Offset: 0x001A2D71
	// (set) Token: 0x06002633 RID: 9779 RVA: 0x001A4979 File Offset: 0x001A2D79
	public bool m_active
	{
		get
		{
			return this._active;
		}
		set
		{
			this._active = value;
		}
	}

	// Token: 0x06002634 RID: 9780 RVA: 0x001A4982 File Offset: 0x001A2D82
	public void Reset()
	{
	}

	// Token: 0x06002635 RID: 9781 RVA: 0x001A4984 File Offset: 0x001A2D84
	public void Destroy()
	{
	}

	// Token: 0x04002B57 RID: 11095
	private int _index;

	// Token: 0x04002B58 RID: 11096
	private bool _active;

	// Token: 0x04002B59 RID: 11097
	public string m_tag;

	// Token: 0x04002B5A RID: 11098
	public Entity p_entity;
}
