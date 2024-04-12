using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000569 RID: 1385
public class GGData
{
	// Token: 0x06002865 RID: 10341 RVA: 0x001AD7DB File Offset: 0x001ABBDB
	public GGData()
	{
	}

	// Token: 0x06002866 RID: 10342 RVA: 0x001AD7F0 File Offset: 0x001ABBF0
	public GGData(Vector2[] _vertices)
	{
		for (int i = 0; i < _vertices.Length; i++)
		{
			GGVertex ggvertex = new GGVertex();
			ggvertex.vertex = _vertices[i];
			this.m_vertices.Add(ggvertex);
		}
	}

	// Token: 0x06002867 RID: 10343 RVA: 0x001AD84C File Offset: 0x001ABC4C
	public GGData(Vector3[] _vertices)
	{
		for (int i = 0; i < _vertices.Length; i++)
		{
			GGVertex ggvertex = new GGVertex();
			ggvertex.vertex = _vertices[i];
			this.m_vertices.Add(ggvertex);
		}
	}

	// Token: 0x06002868 RID: 10344 RVA: 0x001AD8A4 File Offset: 0x001ABCA4
	public GGData Copy()
	{
		GGData ggdata = new GGData();
		for (int i = 0; i < this.m_vertices.Count; i++)
		{
			GGVertex ggvertex = this.m_vertices[i].Copy(false);
			ggdata.m_vertices.Add(ggvertex);
		}
		return ggdata;
	}

	// Token: 0x06002869 RID: 10345 RVA: 0x001AD8F4 File Offset: 0x001ABCF4
	public Vector2[] ToVector2Array()
	{
		Vector2[] array = new Vector2[this.m_vertices.Count];
		for (int i = 0; i < this.m_vertices.Count; i++)
		{
			array[i] = this.m_vertices[i].vertex;
		}
		return array;
	}

	// Token: 0x0600286A RID: 10346 RVA: 0x001AD954 File Offset: 0x001ABD54
	public void Add(GGData _gg)
	{
		for (int i = 0; i < _gg.m_vertices.Count; i++)
		{
			GGVertex ggvertex = _gg.m_vertices[i].Copy(false);
			this.m_vertices.Add(ggvertex);
		}
	}

	// Token: 0x0600286B RID: 10347 RVA: 0x001AD99C File Offset: 0x001ABD9C
	public void EnsureLoop()
	{
		if (!this.IsLoop())
		{
			this.m_vertices.Add(this.m_vertices[0].Copy(false));
		}
	}

	// Token: 0x0600286C RID: 10348 RVA: 0x001AD9C6 File Offset: 0x001ABDC6
	public void RemoveLoop()
	{
		if (this.IsLoop())
		{
			this.m_vertices.Remove(this.m_vertices[this.m_vertices.Count - 1]);
		}
	}

	// Token: 0x0600286D RID: 10349 RVA: 0x001AD9F8 File Offset: 0x001ABDF8
	public bool IsLoop()
	{
		return !(this.m_vertices[0].vertex != this.m_vertices[this.m_vertices.Count - 1].vertex) || this.m_vertices[this.m_vertices.Count - 1].isDuplicate;
	}

	// Token: 0x0600286E RID: 10350 RVA: 0x001ADA64 File Offset: 0x001ABE64
	public void SetOffset(Vector3 _offset)
	{
		for (int i = 0; i < this.m_vertices.Count; i++)
		{
			this.m_vertices[i].vertex += _offset;
		}
	}

	// Token: 0x0600286F RID: 10351 RVA: 0x001ADAAA File Offset: 0x001ABEAA
	public void ReverseVertexOrder()
	{
		this.m_vertices.Reverse();
	}

	// Token: 0x06002870 RID: 10352 RVA: 0x001ADAB8 File Offset: 0x001ABEB8
	public void Expand(float _amount)
	{
		if (this.m_vertices.Count > 2)
		{
			for (int i = 0; i < this.m_vertices.Count; i++)
			{
				Vector3 vertex = this.m_vertices[i].vertex;
				Vector3 vector;
				Vector3 vector2;
				if (i == 0)
				{
					vector = this.m_vertices[i].vertex;
					vector2 = this.m_vertices[i + 1].vertex;
				}
				else if (i == this.m_vertices.Count - 1)
				{
					vector = this.m_vertices[i - 1].vertex;
					vector2 = this.m_vertices[i].vertex;
				}
				else
				{
					vector = this.m_vertices[i - 1].vertex;
					vector2 = this.m_vertices[i + 1].vertex;
				}
				Vector2 vector3 = vector2 - vector;
				float num = Mathf.Atan2(-vector3.y, vector3.x);
				float num2 = Mathf.Sin(num) * _amount;
				float num3 = Mathf.Cos(num) * _amount;
				this.m_vertices[i].vertex = new Vector3(vertex.x + num2, vertex.y + num3, this.m_vertices[i].vertex.z);
			}
		}
		else
		{
			Debug.LogError("Cannot expand data under 3 verts.");
		}
	}

	// Token: 0x06002871 RID: 10353 RVA: 0x001ADC24 File Offset: 0x001AC024
	public void RadialShift(float _multiplier)
	{
		Vector3 vector = Vector2.zero;
		for (int i = 0; i < this.m_vertices.Count; i++)
		{
			vector += this.m_vertices[i].vertex;
		}
		vector /= (float)this.m_vertices.Count;
		for (int j = 0; j < this.m_vertices.Count; j++)
		{
			this.m_vertices[j].vertex += (this.m_vertices[j].vertex - vector) * _multiplier;
		}
	}

	// Token: 0x06002872 RID: 10354 RVA: 0x001ADCD8 File Offset: 0x001AC0D8
	public void RadialShiftRandom(float _minMult, float _maxMult)
	{
		Vector3 vector = Vector2.zero;
		for (int i = 0; i < this.m_vertices.Count; i++)
		{
			vector += this.m_vertices[i].vertex;
		}
		vector /= (float)this.m_vertices.Count;
		for (int j = 0; j < this.m_vertices.Count; j++)
		{
			this.m_vertices[j].vertex += (this.m_vertices[j].vertex - vector) * Random.Range(_minMult, _maxMult);
		}
	}

	// Token: 0x06002873 RID: 10355 RVA: 0x001ADD94 File Offset: 0x001AC194
	public void SetUvMapPlanar(float _xScale, float _yScale, Vector2 _offset, UVRect _normalizeRect = null)
	{
		GGDimensions ggdimensions = null;
		if (_normalizeRect != null)
		{
			ggdimensions = this.GetDimensions();
		}
		for (int i = 0; i < this.m_vertices.Count; i++)
		{
			Vector2 vector = this.m_vertices[i].vertex;
			if (_normalizeRect != null)
			{
				float num = (vector.x - ggdimensions.minX) / ggdimensions.width * _normalizeRect.width + _normalizeRect.left;
				float num2 = (vector.y - ggdimensions.minY) / ggdimensions.height * _normalizeRect.height + _normalizeRect.bottom;
				this.m_vertices[i].uv = new Vector2(num * _xScale, num2 * _yScale) + _offset;
			}
			else
			{
				this.m_vertices[i].uv = new Vector2(vector.x * _xScale, vector.y * _yScale) + _offset;
			}
		}
	}

	// Token: 0x06002874 RID: 10356 RVA: 0x001ADE90 File Offset: 0x001AC290
	public void SetUvMapBelt(float _scale, float v, bool _ensureLoop)
	{
		if (_ensureLoop)
		{
			this.EnsureLoop();
		}
		float num = 0f;
		Vector2 zero = Vector2.zero;
		for (int i = 0; i < this.m_vertices.Count; i++)
		{
			if (this.IsLoop())
			{
				if (i > 0 && i < this.m_vertices.Count - 1)
				{
					num += (this.m_vertices[i].vertex - this.m_vertices[i - 1].vertex).magnitude;
				}
				else if (i == this.m_vertices.Count - 1)
				{
					num += (this.m_vertices[i].vertex - this.m_vertices[i - 2].vertex).magnitude;
				}
			}
			else if (i > 0)
			{
				num += (this.m_vertices[i].vertex - this.m_vertices[i - 1].vertex).magnitude;
			}
			this.m_vertices[i].uv = new Vector2(num * _scale, v);
		}
	}

	// Token: 0x06002875 RID: 10357 RVA: 0x001ADFE0 File Offset: 0x001AC3E0
	public void SetUvMapBeltV(float _scale, float _u, bool _ensureLoop)
	{
		if (_ensureLoop)
		{
			this.EnsureLoop();
		}
		float num = 0f;
		Vector2 zero = Vector2.zero;
		for (int i = 0; i < this.m_vertices.Count; i++)
		{
			if (this.IsLoop())
			{
				if (i > 0 && i < this.m_vertices.Count - 1)
				{
					num += (this.m_vertices[i].vertex - this.m_vertices[i - 1].vertex).magnitude;
				}
				else if (i == this.m_vertices.Count - 1)
				{
					num += (this.m_vertices[i].vertex - this.m_vertices[i - 2].vertex).magnitude;
				}
			}
			else if (i > 0)
			{
				num += (this.m_vertices[i].vertex - this.m_vertices[i - 1].vertex).magnitude;
			}
			this.m_vertices[i].uv = new Vector2(_u, num * _scale);
		}
	}

	// Token: 0x06002876 RID: 10358 RVA: 0x001AE130 File Offset: 0x001AC530
	public GGDimensions GetDimensions()
	{
		GGDimensions ggdimensions = new GGDimensions();
		ggdimensions.minY = float.MaxValue;
		ggdimensions.maxY = float.MinValue;
		ggdimensions.minX = float.MaxValue;
		ggdimensions.maxX = float.MinValue;
		for (int i = 0; i < this.m_vertices.Count; i++)
		{
			if (this.m_vertices[i].vertex.y < ggdimensions.minY)
			{
				ggdimensions.minY = this.m_vertices[i].vertex.y;
			}
			if (this.m_vertices[i].vertex.y > ggdimensions.maxY)
			{
				ggdimensions.maxY = this.m_vertices[i].vertex.y;
			}
			if (this.m_vertices[i].vertex.x < ggdimensions.minX)
			{
				ggdimensions.minX = this.m_vertices[i].vertex.x;
			}
			if (this.m_vertices[i].vertex.x > ggdimensions.maxX)
			{
				ggdimensions.maxX = this.m_vertices[i].vertex.x;
			}
		}
		ggdimensions.width = ggdimensions.maxX - ggdimensions.minX;
		ggdimensions.height = ggdimensions.maxY - ggdimensions.minY;
		return ggdimensions;
	}

	// Token: 0x06002877 RID: 10359 RVA: 0x001AE2A8 File Offset: 0x001AC6A8
	public void SetColorGradientVertical(uint _top, uint _bottom)
	{
		Color color = DebugDraw.UIntToColor(_bottom);
		Color color2 = DebugDraw.UIntToColor(_top);
		GGDimensions dimensions = this.GetDimensions();
		for (int i = 0; i < this.m_vertices.Count; i++)
		{
			float num = (this.m_vertices[i].vertex.y - dimensions.minY) / dimensions.height;
			Color color3 = color2 * num + color * (1f - num);
			this.m_vertices[i].color = color3;
		}
	}

	// Token: 0x06002878 RID: 10360 RVA: 0x001AE33C File Offset: 0x001AC73C
	public void SplitVertices(float _smoothingAngle = 180f, bool _ensureLoop = true)
	{
		if (_smoothingAngle < 180f)
		{
			if (_ensureLoop)
			{
				this.EnsureLoop();
			}
			bool flag = this.IsLoop();
			List<GGVertex> list = new List<GGVertex>();
			for (int i = 0; i < this.m_vertices.Count; i++)
			{
				Vector3 vector;
				Vector3 vector2;
				if (flag)
				{
					int num = ToolBox.getRolledValue(i - 1, 0, this.m_vertices.Count - 1);
					int num2 = ToolBox.getRolledValue(i + 1, 0, this.m_vertices.Count - 1);
					vector = this.m_vertices[num].vertex - this.m_vertices[i].vertex;
					vector2 = this.m_vertices[i].vertex - this.m_vertices[num2].vertex;
				}
				else
				{
					int num = ToolBox.limitBetween(i - 1, 0, this.m_vertices.Count - 1);
					int num2 = ToolBox.limitBetween(i + 1, 0, this.m_vertices.Count - 1);
					if (i > 0)
					{
						vector = this.m_vertices[num].vertex - this.m_vertices[i].vertex;
					}
					else
					{
						vector = this.m_vertices[num].vertex - this.m_vertices[i + 1].vertex;
					}
					if (i < this.m_vertices.Count - 1)
					{
						vector2 = this.m_vertices[i].vertex - this.m_vertices[num2].vertex;
					}
					else
					{
						vector2 = this.m_vertices[i - 1].vertex - this.m_vertices[num2].vertex;
					}
				}
				float num3 = Vector2.Angle(vector, vector2);
				if (num3 >= _smoothingAngle)
				{
					list.Add(this.m_vertices[i]);
					list.Add(this.m_vertices[i].Copy(true));
				}
				else
				{
					list.Add(this.m_vertices[i]);
				}
			}
			this.m_vertices = list;
		}
	}

	// Token: 0x04002D9A RID: 11674
	public List<GGVertex> m_vertices = new List<GGVertex>();
}
