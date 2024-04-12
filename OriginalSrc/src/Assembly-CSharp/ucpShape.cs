using System;
using UnityEngine;

// Token: 0x020004FD RID: 1277
public class ucpShape
{
	// Token: 0x060024C8 RID: 9416 RVA: 0x00195FC8 File Offset: 0x001943C8
	public ucpShape(ucpShapeType _shapeType, Vector2 _offset, uint _layers, float _mass, float _elasticity, float _friction, ucpCollisionType _collisionType, bool _sensor)
	{
		this.shapeType = _shapeType;
		this.shapePtr = IntPtr.Zero;
		this.offset = _offset;
		this.elasticity = _elasticity;
		this.friction = _friction;
		this.collisionType = _collisionType;
		this.sensor = _sensor;
		this.group = 0U;
		this.layers = _layers;
		this.surfaceVelocity = Vector2.zero;
		this.mass = _mass;
		this.area = 1f;
		this.tag = null;
	}

	// Token: 0x04002AC0 RID: 10944
	public ucpShapeType shapeType;

	// Token: 0x04002AC1 RID: 10945
	public IntPtr shapePtr;

	// Token: 0x04002AC2 RID: 10946
	public Vector2 offset;

	// Token: 0x04002AC3 RID: 10947
	public float elasticity;

	// Token: 0x04002AC4 RID: 10948
	public float friction;

	// Token: 0x04002AC5 RID: 10949
	public ucpCollisionType collisionType;

	// Token: 0x04002AC6 RID: 10950
	public bool sensor;

	// Token: 0x04002AC7 RID: 10951
	public uint group;

	// Token: 0x04002AC8 RID: 10952
	public uint layers;

	// Token: 0x04002AC9 RID: 10953
	public Vector2 surfaceVelocity;

	// Token: 0x04002ACA RID: 10954
	public float mass;

	// Token: 0x04002ACB RID: 10955
	public float area;

	// Token: 0x04002ACC RID: 10956
	public string tag;
}
