using System;
using System.Runtime.Serialization;

// Token: 0x020004C4 RID: 1220
[Serializable]
public class BasicComponent : IComponent, IPoolable, ISerializable
{
	// Token: 0x060022AB RID: 8875 RVA: 0x000034BC File Offset: 0x000018BC
	public BasicComponent(ComponentType _componentType)
	{
		this.m_componentType = _componentType;
	}

	// Token: 0x060022AC RID: 8876 RVA: 0x000034D2 File Offset: 0x000018D2
	public BasicComponent(SerializationInfo info, StreamingContext ctxt)
	{
		this.m_componentType = (ComponentType)info.GetValue("componentType", typeof(ComponentType));
	}

	// Token: 0x170000AA RID: 170
	// (get) Token: 0x060022AD RID: 8877 RVA: 0x00003501 File Offset: 0x00001901
	// (set) Token: 0x060022AE RID: 8878 RVA: 0x00003509 File Offset: 0x00001909
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

	// Token: 0x170000AB RID: 171
	// (get) Token: 0x060022AF RID: 8879 RVA: 0x00003512 File Offset: 0x00001912
	// (set) Token: 0x060022B0 RID: 8880 RVA: 0x0000351A File Offset: 0x0000191A
	public int m_identifier
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

	// Token: 0x170000AC RID: 172
	// (get) Token: 0x060022B1 RID: 8881 RVA: 0x00003523 File Offset: 0x00001923
	// (set) Token: 0x060022B2 RID: 8882 RVA: 0x0000352B File Offset: 0x0000192B
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

	// Token: 0x170000AD RID: 173
	// (get) Token: 0x060022B3 RID: 8883 RVA: 0x00003534 File Offset: 0x00001934
	// (set) Token: 0x060022B4 RID: 8884 RVA: 0x0000353C File Offset: 0x0000193C
	public Entity p_entity
	{
		get
		{
			return this._entity;
		}
		set
		{
			this._entity = value;
		}
	}

	// Token: 0x170000AE RID: 174
	// (get) Token: 0x060022B5 RID: 8885 RVA: 0x00003545 File Offset: 0x00001945
	// (set) Token: 0x060022B6 RID: 8886 RVA: 0x0000354D File Offset: 0x0000194D
	public ComponentType m_componentType
	{
		get
		{
			return this._componentType;
		}
		set
		{
			this._componentType = value;
		}
	}

	// Token: 0x170000AF RID: 175
	// (get) Token: 0x060022B7 RID: 8887 RVA: 0x00003556 File Offset: 0x00001956
	// (set) Token: 0x060022B8 RID: 8888 RVA: 0x0000355E File Offset: 0x0000195E
	public bool m_wasActive
	{
		get
		{
			return this._wasActive;
		}
		set
		{
			this._wasActive = value;
		}
	}

	// Token: 0x060022B9 RID: 8889 RVA: 0x00003567 File Offset: 0x00001967
	public virtual void Reset()
	{
		this.m_active = false;
		this.m_wasActive = false;
		this.m_identifier = -1;
		this.p_entity = null;
	}

	// Token: 0x060022BA RID: 8890 RVA: 0x00003585 File Offset: 0x00001985
	public virtual void Destroy()
	{
	}

	// Token: 0x060022BB RID: 8891 RVA: 0x00003587 File Offset: 0x00001987
	public virtual void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		info.AddValue("componentType", this.m_componentType);
	}

	// Token: 0x040028C7 RID: 10439
	private bool _active;

	// Token: 0x040028C8 RID: 10440
	private int _identifier;

	// Token: 0x040028C9 RID: 10441
	private int _index = -1;

	// Token: 0x040028CA RID: 10442
	private Entity _entity;

	// Token: 0x040028CB RID: 10443
	private ComponentType _componentType;

	// Token: 0x040028CC RID: 10444
	private bool _wasActive;

	// Token: 0x040028CD RID: 10445
	private PoolableState m_state;
}
