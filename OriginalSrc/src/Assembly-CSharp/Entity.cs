using System;
using System.Collections.Generic;

// Token: 0x020004D9 RID: 1241
public class Entity : IPoolable
{
	// Token: 0x060022FC RID: 8956 RVA: 0x001918A1 File Offset: 0x0018FCA1
	public Entity()
	{
		this.m_components = new List<IComponent>();
		Entity.m_instanceCount++;
	}

	// Token: 0x170000B1 RID: 177
	// (get) Token: 0x060022FD RID: 8957 RVA: 0x001918C7 File Offset: 0x0018FCC7
	// (set) Token: 0x060022FE RID: 8958 RVA: 0x001918CF File Offset: 0x0018FCCF
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

	// Token: 0x170000B2 RID: 178
	// (get) Token: 0x060022FF RID: 8959 RVA: 0x001918D8 File Offset: 0x0018FCD8
	// (set) Token: 0x06002300 RID: 8960 RVA: 0x001918E0 File Offset: 0x0018FCE0
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

	// Token: 0x06002301 RID: 8961 RVA: 0x001918E9 File Offset: 0x0018FCE9
	public void Reset()
	{
		this.m_components.Clear();
		this.m_entityLogicCount = 0;
		this.m_componentsChecksum = 0;
		this.m_persistent = false;
		this.m_visible = true;
	}

	// Token: 0x06002302 RID: 8962 RVA: 0x00191912 File Offset: 0x0018FD12
	public void Destroy()
	{
	}

	// Token: 0x06002303 RID: 8963 RVA: 0x00191914 File Offset: 0x0018FD14
	~Entity()
	{
		Entity.m_instanceCount--;
	}

	// Token: 0x040029D7 RID: 10711
	public static int m_instanceCount;

	// Token: 0x040029D8 RID: 10712
	private int _index = -1;

	// Token: 0x040029D9 RID: 10713
	private bool _active;

	// Token: 0x040029DA RID: 10714
	public List<IComponent> m_components;

	// Token: 0x040029DB RID: 10715
	public int m_componentsChecksum;

	// Token: 0x040029DC RID: 10716
	public bool m_persistent;

	// Token: 0x040029DD RID: 10717
	public bool m_visible;

	// Token: 0x040029DE RID: 10718
	public int m_entityLogicCount;

	// Token: 0x040029DF RID: 10719
	public EntityLogicDelegate d_entityLogic;

	// Token: 0x040029E0 RID: 10720
	public PoolableState m_state;
}
