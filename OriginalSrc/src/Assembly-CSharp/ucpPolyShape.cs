using System;
using UnityEngine;

// Token: 0x020004FF RID: 1279
public class ucpPolyShape : ucpShape
{
	// Token: 0x060024CB RID: 9419 RVA: 0x001960DC File Offset: 0x001944DC
	public ucpPolyShape(Vector2[] _verts, Vector2 _offset, uint _layers, float _mass = 10f, float _elasticity = 0.5f, float _friction = 0.5f, ucpCollisionType _collisionType = ucpCollisionType.None, bool _sensor = false)
		: base(ucpShapeType.Poly, _offset, _layers, _mass, _elasticity, _friction, _collisionType, _sensor)
	{
		this.verts = _verts;
		this.numVerts = _verts.Length;
		float num = 999999f;
		float num2 = -999999f;
		float num3 = -999999f;
		float num4 = 999999f;
		for (int i = 0; i < this.numVerts; i++)
		{
			Vector2 vector = this.verts[i];
			if (vector.x > num2)
			{
				num2 = vector.x;
			}
			if (vector.x <= num)
			{
				num = vector.x;
			}
			if (vector.y > num3)
			{
				num3 = vector.y;
			}
			if (vector.y <= num4)
			{
				num4 = vector.y;
			}
		}
		this.width = num2 - num;
		this.height = num3 - num4;
		this.area = ChipmunkProWrapper.ucpAreaForPoly(this.numVerts, this.verts, 0f);
	}

	// Token: 0x060024CC RID: 9420 RVA: 0x001961D8 File Offset: 0x001945D8
	public ucpPolyShape(float _width, float _height, Vector2 _offset, uint _layers, float _mass = 10f, float _elasticity = 0.5f, float _friction = 0.5f, ucpCollisionType _collisionType = ucpCollisionType.None, bool _sensor = false)
		: base(ucpShapeType.Poly, _offset, _layers, _mass, _elasticity, _friction, _collisionType, _sensor)
	{
		this.verts = new Vector2[4];
		this.verts[0].x = _width * -0.5f;
		this.verts[0].y = _height * 0.5f;
		this.verts[1].x = _width * 0.5f;
		this.verts[1].y = _height * 0.5f;
		this.verts[2].x = _width * 0.5f;
		this.verts[2].y = _height * -0.5f;
		this.verts[3].x = _width * -0.5f;
		this.verts[3].y = _height * -0.5f;
		this.numVerts = 4;
		this.width = _width;
		this.height = _height;
		this.area = ChipmunkProWrapper.ucpAreaForPoly(this.numVerts, this.verts, 0f);
	}

	// Token: 0x04002ACF RID: 10959
	public Vector2[] verts;

	// Token: 0x04002AD0 RID: 10960
	public int numVerts;

	// Token: 0x04002AD1 RID: 10961
	public float width;

	// Token: 0x04002AD2 RID: 10962
	public float height;
}
