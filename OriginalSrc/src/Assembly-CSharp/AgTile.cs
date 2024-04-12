using System;
using UnityEngine;

// Token: 0x020004A6 RID: 1190
public class AgTile
{
	// Token: 0x060021F0 RID: 8688 RVA: 0x0018A184 File Offset: 0x00188584
	public AgTile(int _shapeSpace, AutoGeometryLayer _layer)
	{
		this.agLayer = _layer;
		this.shapes = new IntPtr[_shapeSpace];
		this.shapeCount = 0;
		this.dirty = false;
		this.edgeVerts = null;
		this.regenerateCollisionShapes = true;
		this.props = null;
		this.pos = Vector3.zero;
		AgTile.m_instanceCount++;
	}

	// Token: 0x060021F1 RID: 8689 RVA: 0x0018A1EC File Offset: 0x001885EC
	public void Destroy()
	{
		this.props.Destroy();
		this.props = null;
		for (int i = 0; i < this.shapeCount; i++)
		{
			ChipmunkProWrapper.ucpRemoveShape(this.shapes[i]);
		}
		this.shapeCount = 0;
		this.shapes = null;
		this.edgeVerts = null;
		EntityManager.RemoveEntity(this.TC.p_entity);
		this.TC = null;
	}

	// Token: 0x060021F2 RID: 8690 RVA: 0x0018A25C File Offset: 0x0018865C
	~AgTile()
	{
		AgTile.m_instanceCount--;
	}

	// Token: 0x04002819 RID: 10265
	private static int m_instanceCount;

	// Token: 0x0400281A RID: 10266
	public IntPtr tilePtr;

	// Token: 0x0400281B RID: 10267
	public IntPtr[] shapes;

	// Token: 0x0400281C RID: 10268
	public int shapeCount;

	// Token: 0x0400281D RID: 10269
	public AgTileEdgeVert[] edgeVerts;

	// Token: 0x0400281E RID: 10270
	public TransformC TC;

	// Token: 0x0400281F RID: 10271
	public Vector2 pos;

	// Token: 0x04002820 RID: 10272
	public cpBB bb;

	// Token: 0x04002821 RID: 10273
	public bool dirty;

	// Token: 0x04002822 RID: 10274
	public bool regenerateCollisionShapes;

	// Token: 0x04002823 RID: 10275
	public AutoGeometryProps props;

	// Token: 0x04002824 RID: 10276
	public AutoGeometryLayer agLayer;
}
