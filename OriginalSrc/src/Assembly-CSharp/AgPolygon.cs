using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004A7 RID: 1191
public class AgPolygon : IPoolable
{
	// Token: 0x060021F3 RID: 8691 RVA: 0x0018A294 File Offset: 0x00188694
	public AgPolygon()
	{
		this.vertices = new List<Vector2>();
		this.extraData = new List<Vector3>();
	}

	// Token: 0x060021F4 RID: 8692 RVA: 0x0018A2BC File Offset: 0x001886BC
	public AgPolygon(int _capacity)
	{
		this.vertices = new List<Vector2>();
		this.extraData = new List<Vector3>();
		if (_capacity > 0)
		{
			this.vertices.Capacity = _capacity;
			this.extraData.Capacity = _capacity;
		}
	}

	// Token: 0x170000A9 RID: 169
	// (get) Token: 0x060021F5 RID: 8693 RVA: 0x0018A30B File Offset: 0x0018870B
	// (set) Token: 0x060021F6 RID: 8694 RVA: 0x0018A313 File Offset: 0x00188713
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

	// Token: 0x060021F7 RID: 8695 RVA: 0x0018A31C File Offset: 0x0018871C
	public virtual void Reset()
	{
		if (this.vertices != null)
		{
			this.vertices.Clear();
		}
		if (this.extraData != null)
		{
			this.extraData.Clear();
		}
		this.isHole = false;
	}

	// Token: 0x060021F8 RID: 8696 RVA: 0x0018A351 File Offset: 0x00188751
	public virtual void Destroy()
	{
		if (this.vertices != null)
		{
			this.vertices.Clear();
		}
		if (this.extraData != null)
		{
			this.extraData.Clear();
		}
		this.isHole = false;
	}

	// Token: 0x060021F9 RID: 8697 RVA: 0x0018A386 File Offset: 0x00188786
	public void Clear()
	{
		this.vertices.Clear();
		this.extraData.Clear();
		this.isHole = false;
	}

	// Token: 0x04002825 RID: 10277
	public bool isHole;

	// Token: 0x04002826 RID: 10278
	public List<Vector2> vertices;

	// Token: 0x04002827 RID: 10279
	public List<Vector3> extraData;

	// Token: 0x04002828 RID: 10280
	private int _index = -1;
}
