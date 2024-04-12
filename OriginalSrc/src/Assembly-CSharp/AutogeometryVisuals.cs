using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004B0 RID: 1200
public static class AutogeometryVisuals
{
	// Token: 0x06002245 RID: 8773 RVA: 0x0018E050 File Offset: 0x0018C450
	public static Mesh CreateBeltMeshFromVertexArray(AgPolygon _poly, float _width, Vector3 _offset, bool _loop, Vector2 _tileCenter, float _smoothingAngle)
	{
		bool flag = false;
		Mesh mesh = new Mesh();
		int num = _poly.vertices.Count - ((!_loop) ? 1 : 0);
		int num2 = num * 6;
		int[] array = new int[num2 * ((!flag) ? 1 : 2)];
		Vector3[] array2 = new Vector3[_poly.vertices.Count - ((!_loop) ? 0 : 1)];
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i] = new Vector3(_poly.vertices[i].x + _offset.x, _poly.vertices[i].y + _offset.y, _offset.z) - _tileCenter;
		}
		Color black = Color.black;
		AutogeometryVisuals.m_frontVertCount = 0;
		if (_smoothingAngle < 180f)
		{
			AutogeometryVisuals.m_newVerts.Clear();
			for (int j = 0; j < array2.Length; j++)
			{
				Vector3 vector;
				Vector3 vector2;
				if (_loop)
				{
					int num3 = ToolBox.getRolledValue(j - 1, 0, array2.Length - 1);
					int num4 = ToolBox.getRolledValue(j + 1, 0, array2.Length - 1);
					vector = array2[num3] - array2[j];
					vector2 = array2[j] - array2[num4];
				}
				else
				{
					int num3 = Mathf.Max(j - 1, 0);
					int num4 = Mathf.Min(j + 1, array2.Length - 1);
					if (j > 0)
					{
						vector = array2[num3] - array2[j];
					}
					else
					{
						vector = array2[num3] - array2[j + 1];
					}
					if (j < array2.Length - 1)
					{
						vector2 = array2[j] - array2[num4];
					}
					else
					{
						vector2 = array2[j - 1] - array2[num4];
					}
				}
				float num5 = Vector2.Angle(vector, vector2);
				if (num5 >= _smoothingAngle)
				{
					List<CVertex> newVerts = AutogeometryVisuals.m_newVerts;
					Vector3 vector3 = array2[j];
					Vector2 zero = Vector2.zero;
					Color color = black;
					Vector2 vector4;
					vector4..ctor(vector.y, -vector.x);
					newVerts.Add(new CVertex(vector3, zero, color, vector4.normalized, false));
					List<CVertex> newVerts2 = AutogeometryVisuals.m_newVerts;
					Vector3 vector5 = array2[j];
					Vector2 zero2 = Vector2.zero;
					Color color2 = black;
					Vector2 vector6;
					vector6..ctor(vector2.y, -vector2.x);
					newVerts2.Add(new CVertex(vector5, zero2, color2, vector6.normalized, true));
				}
				else
				{
					AutogeometryVisuals.m_newVerts.Add(new CVertex(array2[j], Vector2.zero, black, _poly.extraData[j], false));
				}
			}
			AutogeometryVisuals.m_frontVertCount = AutogeometryVisuals.m_newVerts.Count;
			for (int k = 0; k < AutogeometryVisuals.m_newVerts.Count; k++)
			{
				AutogeometryVisuals.m_frontVerts[k] = AutogeometryVisuals.m_newVerts[k];
			}
		}
		else
		{
			for (int l = 0; l < array2.Length; l++)
			{
				AutogeometryVisuals.m_frontVerts[l] = new CVertex(array2[l], Vector2.zero, black, _poly.extraData[l], false);
			}
			AutogeometryVisuals.m_frontVertCount = array2.Length;
		}
		int frontVertCount = AutogeometryVisuals.m_frontVertCount;
		int num6 = 0;
		int num7 = 0;
		for (int m = 0; m < frontVertCount; m++)
		{
			int num8 = (num7 + 1) % frontVertCount;
			if (!AutogeometryVisuals.m_frontVerts[num8].isDuplicate && num6 < num2)
			{
				array[num6] = num7;
				array[num6 + 1] = num7 + frontVertCount;
				array[num6 + 2] = num8;
				array[num6 + 3] = num8;
				array[num6 + 4] = num7 + frontVertCount;
				array[num6 + 5] = num8 + frontVertCount;
				num6 += 6;
			}
			num7++;
		}
		Vector3[] array3 = new Vector3[AutogeometryVisuals.m_frontVertCount * 2 * ((!flag) ? 1 : 2)];
		Vector2[] array4 = new Vector2[AutogeometryVisuals.m_frontVertCount * 2 * ((!flag) ? 1 : 2)];
		Vector3[] array5 = new Vector3[AutogeometryVisuals.m_frontVertCount * 2 * ((!flag) ? 1 : 2)];
		Vector3 vector7;
		vector7..ctor(0f, 0f, _width);
		for (int n = 0; n < AutogeometryVisuals.m_frontVertCount; n++)
		{
			Vector3 vert = AutogeometryVisuals.m_frontVerts[n].vert;
			Vector2 uv = AutogeometryVisuals.m_frontVerts[n].uv;
			Vector3 normal = AutogeometryVisuals.m_frontVerts[n].normal;
			array3[n] = vert;
			array4[n] = uv;
			array5[n] = normal;
			array3[n + AutogeometryVisuals.m_frontVertCount] = vert + vector7;
			array4[n + AutogeometryVisuals.m_frontVertCount] = uv;
			array5[n + AutogeometryVisuals.m_frontVertCount] = normal;
		}
		mesh.vertices = array3;
		mesh.triangles = array;
		mesh.uv = array4;
		mesh.normals = array5;
		return mesh;
	}

	// Token: 0x06002246 RID: 8774 RVA: 0x0018E628 File Offset: 0x0018CA28
	public static Mesh CreateFrontFaceMeshFromPolygons(AgPolygon[] _polygons, Vector3 _offset, Vector3 _tileCenter)
	{
		IntPtr intPtr = LibTessWrapper.tessNewTess(IntPtr.Zero);
		Vector2 vector = -_tileCenter;
		for (int i = 0; i < _polygons.Length; i++)
		{
			float[] array = new float[_polygons[i].vertices.Count * 3];
			for (int j = 0; j < _polygons[i].vertices.Count; j++)
			{
				int num = (_polygons[i].vertices.Count - 1 - j) * 3;
				array[num] = _polygons[i].vertices[j].x + vector.x;
				array[num + 1] = _polygons[i].vertices[j].y + vector.y;
				array[num + 2] = 0f;
			}
			LibTessWrapper.tessAddContour(intPtr, 3, array, 12, _polygons[i].vertices.Count);
		}
		LibTessWrapper.tessTesselate(intPtr, 0, 0, 3, 3, null);
		int num2 = LibTessWrapper.tessGetVertexCount(intPtr);
		int num3 = LibTessWrapper.tessGetElementCount(intPtr);
		float[] array2 = new float[num2 * 3];
		LibTessWrapper.wTessGetVertices(intPtr, array2, 3);
		int[] array3 = new int[num3 * 3];
		LibTessWrapper.wTessGetElements(intPtr, array3, 3);
		LibTessWrapper.tessDeleteTess(intPtr);
		Mesh mesh = new Mesh();
		if (num2 > 0)
		{
			int[] array4 = new int[num3 * 3];
			for (int k = 0; k < num3; k++)
			{
				int num4 = k * 3;
				for (int l = 0; l < 3; l++)
				{
					int num5 = array3[k * 3 + l];
					if (num5 != -1)
					{
						array4[num4 + 2 - l] = num5;
					}
				}
			}
			int num6 = num2;
			Vector3[] array5 = new Vector3[num6];
			Vector2[] array6 = new Vector2[num6];
			Vector2[] array7 = new Vector2[num6];
			Vector2 vector2 = _tileCenter / 200f;
			vector2.x = ToolBox.getRolledValue(vector2.x, -1f, 1f);
			vector2.y = ToolBox.getRolledValue(vector2.y, -1f, 1f);
			for (int m = 0; m < num6; m++)
			{
				int num7 = m * 3;
				Vector2 vector3;
				vector3..ctor(array2[num7], array2[num7 + 1]);
				array5[m] = new Vector3(vector3.x, vector3.y, 0f) + _offset;
				Vector2 vector4 = new Vector2(vector3.x, vector3.y) / 200f;
				array7[m] = vector2 + vector4;
				float positionBetween = ToolBox.getPositionBetween(vector3.x + _tileCenter.x + 8f, (float)(-(float)AutoGeometryManager.m_width) * 0.5f, (float)AutoGeometryManager.m_width * 0.5f);
				float positionBetween2 = ToolBox.getPositionBetween(vector3.y + _tileCenter.y + 8f, (float)(-(float)AutoGeometryManager.m_height) * 0.5f, (float)AutoGeometryManager.m_height * 0.5f);
				array6[m] = new Vector2(positionBetween, positionBetween2);
			}
			mesh.vertices = array5;
			mesh.uv = array6;
			mesh.uv2 = array7;
			mesh.triangles = array4;
		}
		return mesh;
	}

	// Token: 0x0400287E RID: 10366
	private static CVertex[] m_frontVerts = new CVertex[200];

	// Token: 0x0400287F RID: 10367
	private static List<CVertex> m_newVerts = new List<CVertex>(200);

	// Token: 0x04002880 RID: 10368
	private static int m_frontVertCount = 0;
}
