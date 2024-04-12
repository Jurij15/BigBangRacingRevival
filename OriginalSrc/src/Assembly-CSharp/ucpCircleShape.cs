using System;
using UnityEngine;

// Token: 0x020004FE RID: 1278
public class ucpCircleShape : ucpShape
{
	// Token: 0x060024C9 RID: 9417 RVA: 0x00196048 File Offset: 0x00194448
	public ucpCircleShape(float _radius, Vector2 _offset, uint _layers, float _mass = 10f, float _elasticity = 0.5f, float _friction = 0.5f, ucpCollisionType _collisionType = ucpCollisionType.None, bool _sensor = false)
		: base(ucpShapeType.Circle, _offset, _layers, _mass, _elasticity, _friction, _collisionType, _sensor)
	{
		this.innerRadius = 0f;
		this.outerRadius = _radius;
		this.area = ChipmunkProWrapper.ucpAreaForCircle(this.innerRadius, this.outerRadius);
	}

	// Token: 0x060024CA RID: 9418 RVA: 0x00196094 File Offset: 0x00194494
	public ucpCircleShape(float _innerRadius, float _outerRadius, Vector2 _offset, uint _layers, float _mass = 10f, float _elasticity = 0.5f, float _friction = 0.5f, ucpCollisionType _collisionType = ucpCollisionType.None, bool _sensor = false)
		: base(ucpShapeType.Circle, _offset, _layers, _mass, _elasticity, _friction, _collisionType, _sensor)
	{
		this.innerRadius = _innerRadius;
		this.outerRadius = _outerRadius;
		this.area = ChipmunkProWrapper.ucpAreaForCircle(this.innerRadius, this.outerRadius);
	}

	// Token: 0x04002ACD RID: 10957
	public float innerRadius;

	// Token: 0x04002ACE RID: 10958
	public float outerRadius;
}
