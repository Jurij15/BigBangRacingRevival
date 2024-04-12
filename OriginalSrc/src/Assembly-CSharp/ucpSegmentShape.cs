using System;
using UnityEngine;

// Token: 0x02000500 RID: 1280
public class ucpSegmentShape : ucpShape
{
	// Token: 0x060024CD RID: 9421 RVA: 0x001962F8 File Offset: 0x001946F8
	public ucpSegmentShape(Vector2 _a, Vector2 _b, float _radius, Vector2 _offset, uint _layers, float _mass = 10f, float _elasticity = 0.5f, float _friction = 0.5f, ucpCollisionType _collisionType = ucpCollisionType.None, bool _sensor = false)
		: base(ucpShapeType.Segment, _offset, _layers, _mass, _elasticity, _friction, _collisionType, _sensor)
	{
		this.a = _a;
		this.b = _b;
		this.radius = _radius;
	}

	// Token: 0x04002AD3 RID: 10963
	public Vector2 a;

	// Token: 0x04002AD4 RID: 10964
	public Vector2 b;

	// Token: 0x04002AD5 RID: 10965
	public float radius;
}
