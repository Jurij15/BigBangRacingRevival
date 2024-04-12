using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200056A RID: 1386
public static class GeometryGenerator
{
	// Token: 0x06002879 RID: 10361 RVA: 0x001AE578 File Offset: 0x001AC978
	public static Mesh GenerateBeltMesh(float _farOffset, GGData _ggFar, GGData _ggNear = null, bool _loop = true)
	{
		if (_ggNear != null && _ggNear.m_vertices.Count != _ggFar.m_vertices.Count)
		{
			Debug.LogError("Vertex arrays has to be of same length.");
			return null;
		}
		if (_loop)
		{
			_ggFar.EnsureLoop();
			if (_ggNear != null)
			{
				_ggNear.EnsureLoop();
			}
		}
		Mesh mesh = new Mesh();
		List<Vector3> list = new List<Vector3>(_ggFar.m_vertices.Count * 2);
		List<Vector2> list2 = new List<Vector2>(_ggFar.m_vertices.Count * 2);
		List<Color> list3 = new List<Color>(_ggFar.m_vertices.Count * 2);
		List<int> list4 = new List<int>((_ggFar.m_vertices.Count * 2 - 2) * 3);
		for (int i = 0; i < _ggFar.m_vertices.Count; i++)
		{
			GGVertex ggvertex = _ggFar.m_vertices[i];
			GGVertex ggvertex2;
			if (_ggNear != null)
			{
				ggvertex2 = _ggNear.m_vertices[i];
			}
			else
			{
				ggvertex2 = ggvertex;
			}
			list.AddRange(new Vector3[2]);
			list2.AddRange(new Vector2[2]);
			list3.AddRange(new Color[2]);
			list4.AddRange(new int[6]);
			list[i * 2] = new Vector3(ggvertex.vertex.x, ggvertex.vertex.y, ggvertex.vertex.z + _farOffset);
			list[i * 2 + 1] = new Vector3(ggvertex2.vertex.x, ggvertex2.vertex.y, ggvertex2.vertex.z);
			list2[i * 2] = ggvertex.uv;
			list2[i * 2 + 1] = ggvertex2.uv;
			list3[i * 2] = ggvertex.color;
			list3[i * 2 + 1] = ggvertex2.color;
		}
		int num = 1;
		for (int j = 0; j < _ggFar.m_vertices.Count; j++)
		{
			if (j > 0 && !_ggFar.m_vertices[j].isDuplicate)
			{
				list4[(num * 2 - 2) * 3] = j * 2 - 2;
				list4[(num * 2 - 2) * 3 + 1] = j * 2 - 2 + 2;
				list4[(num * 2 - 2) * 3 + 2] = j * 2 - 2 + 1;
				list4[(num * 2 - 1) * 3] = j * 2 - 1;
				list4[(num * 2 - 1) * 3 + 1] = j * 2 - 1 + 1;
				list4[(num * 2 - 1) * 3 + 2] = j * 2 - 1 + 2;
				num++;
			}
		}
		mesh.vertices = list.ToArray();
		mesh.triangles = list4.ToArray();
		mesh.uv = list2.ToArray();
		mesh.colors = list3.ToArray();
		return mesh;
	}

	// Token: 0x0600287A RID: 10362 RVA: 0x001AE85C File Offset: 0x001ACC5C
	public static Mesh GenerateFlatMesh(GGData _gg)
	{
		_gg.RemoveLoop();
		IntPtr intPtr = LibTessWrapper.tessNewTess(IntPtr.Zero);
		int count = _gg.m_vertices.Count;
		float[] array = new float[count * 3];
		for (int i = 0; i < count; i++)
		{
			int num = (count - 1 - i) * 3;
			array[num] = _gg.m_vertices[i].vertex.x;
			array[num + 1] = _gg.m_vertices[i].vertex.y;
			array[num + 2] = _gg.m_vertices[i].vertex.z;
		}
		LibTessWrapper.tessAddContour(intPtr, 3, array, 12, count);
		LibTessWrapper.tessTesselate(intPtr, 0, 0, 3, 3, null);
		int num2 = LibTessWrapper.tessGetVertexCount(intPtr);
		int num3 = LibTessWrapper.tessGetElementCount(intPtr);
		float[] array2 = new float[num2 * 3];
		LibTessWrapper.wTessGetVertices(intPtr, array2, 3);
		int[] array3 = new int[num2];
		LibTessWrapper.wTessGetVertexIndices(intPtr, array3);
		int[] array4 = new int[num3 * 3];
		LibTessWrapper.wTessGetElements(intPtr, array4, 3);
		LibTessWrapper.tessDeleteTess(intPtr);
		if (num2 > 0)
		{
			Mesh mesh = new Mesh();
			int num4 = num2;
			int[] array5 = new int[num3 * 3];
			for (int j = 0; j < num3; j++)
			{
				int num5 = j * 3;
				for (int k = 0; k < 3; k++)
				{
					int num6 = array4[j * 3 + k];
					if (num6 != -1)
					{
						array5[num5 + 2 - k] = num6;
					}
				}
			}
			Vector3[] array6 = new Vector3[num4];
			Vector2[] array7 = new Vector2[num4];
			Color[] array8 = new Color[num4];
			for (int l = 0; l < num4; l++)
			{
				int num7 = array3[l];
				int num8 = l * 3;
				GGVertex ggvertex = _gg.m_vertices[count - 1 - num7];
				array6[l] = new Vector3(array2[num8], array2[num8 + 1], array2[num8 + 2]);
				if (ggvertex != null)
				{
					array7[l] = ggvertex.uv;
					array8[l] = ggvertex.color;
				}
			}
			mesh.vertices = array6;
			mesh.uv = array7;
			mesh.colors = array8;
			mesh.triangles = array5;
			return mesh;
		}
		Debug.LogWarning("Error tesselating mesh. Vertex count 0.");
		return null;
	}
}
