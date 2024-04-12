using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

// Token: 0x0200057E RID: 1406
public static class ToolBox
{
	// Token: 0x060028D2 RID: 10450 RVA: 0x001B175A File Offset: 0x001AFB5A
	public static Color GetRandomColor()
	{
		return new Color(Random.value * 0.5f, Random.value * 0.5f, Random.value * 0.5f);
	}

	// Token: 0x060028D3 RID: 10451 RVA: 0x001B1784 File Offset: 0x001AFB84
	public static string GetTimerStringFromSeconds(int _seconds, bool _space = true)
	{
		string text = ((!_space) ? string.Empty : " ");
		int num = Mathf.FloorToInt((float)_seconds / 60f);
		if (num <= 0)
		{
			return _seconds + "s";
		}
		int num2 = _seconds - num * 60;
		int num3 = Mathf.FloorToInt((float)num / 60f);
		if (num3 <= 0)
		{
			return string.Concat(new object[] { num, "m", text, num2, "s" });
		}
		num -= num3 * 60;
		int num4 = Mathf.FloorToInt((float)num3 / 24f);
		if (num4 > 0)
		{
			num3 -= num4 * 24;
			return string.Concat(new object[] { num4, "d", text, num3, "h" });
		}
		return string.Concat(new object[] { num3, "h", text, num, "m" });
	}

	// Token: 0x060028D4 RID: 10452 RVA: 0x001B18B0 File Offset: 0x001AFCB0
	public static Array ConvertArray<S, T>(S[] _source)
	{
		Array array = new Array[_source.Length];
		for (int i = 0; i < _source.Length; i++)
		{
			if (typeof(T) == typeof(string))
			{
				array.SetValue(_source[i].ToString(), i);
			}
			else
			{
				array.SetValue(_source[i], i);
			}
		}
		return array;
	}

	// Token: 0x060028D5 RID: 10453 RVA: 0x001B1928 File Offset: 0x001AFD28
	public static int GetEpochTime()
	{
		return (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
	}

	// Token: 0x060028D6 RID: 10454 RVA: 0x001B1956 File Offset: 0x001AFD56
	public static bool IsZero(float f)
	{
		return (double)Mathf.Abs(f) <= 0.0001;
	}

	// Token: 0x060028D7 RID: 10455 RVA: 0x001B196D File Offset: 0x001AFD6D
	public static bool IsLess(float f1, float f2)
	{
		return f1 < f2 && !ToolBox.IsZero(f1 - f2);
	}

	// Token: 0x060028D8 RID: 10456 RVA: 0x001B1984 File Offset: 0x001AFD84
	public static bool IsGreater(float f1, float f2)
	{
		return f1 > f2 && !ToolBox.IsZero(f1 - f2);
	}

	// Token: 0x060028D9 RID: 10457 RVA: 0x001B199B File Offset: 0x001AFD9B
	public static string GetTimestamp(DateTime value)
	{
		return value.ToString("yyyyMMddHHmmssffff");
	}

	// Token: 0x060028DA RID: 10458 RVA: 0x001B19AC File Offset: 0x001AFDAC
	public static bool linesIntersect(Vector2 p11, Vector2 p12, Vector2 p21, Vector2 p22)
	{
		ToolBox.lastOutDistLine1 = float.MaxValue;
		ToolBox.lastOutDistLine2 = float.MaxValue;
		float num = (p22.y - p21.y) * (p12.x - p11.x) - (p22.x - p21.x) * (p12.y - p11.y);
		if (ToolBox.IsZero(num))
		{
			return false;
		}
		float num2 = (p22.x - p21.x) * (p11.y - p21.y) - (p22.y - p21.y) * (p11.x - p21.x);
		float num3 = (p12.x - p11.x) * (p11.y - p21.y) - (p12.y - p11.y) * (p11.x - p21.x);
		ToolBox.lastOutDistLine1 = num2 / num;
		ToolBox.lastOutDistLine2 = num3 / num;
		return true;
	}

	// Token: 0x060028DB RID: 10459 RVA: 0x001B1AAC File Offset: 0x001AFEAC
	public static bool lineSegmentsIntersect(Vector2 p11, Vector2 p12, Vector2 p21, Vector2 p22, bool getIntersectionPoint)
	{
		if (ToolBox.linesIntersect(p11, p12, p21, p22))
		{
			bool flag = ToolBox.IsLess(ToolBox.lastOutDistLine1, 1f) && ToolBox.IsLess(ToolBox.lastOutDistLine2, 1f) && ToolBox.IsGreater(ToolBox.lastOutDistLine1, 0f) && ToolBox.IsGreater(ToolBox.lastOutDistLine2, 0f);
			if (flag && getIntersectionPoint)
			{
				float num = p11.x - p12.x;
				float num2 = p11.y - p12.y;
				num *= ToolBox.lastOutDistLine1;
				num2 *= ToolBox.lastOutDistLine1;
				ToolBox.lastLineIntersectionPoint.x = p11.x - num;
				ToolBox.lastLineIntersectionPoint.y = p11.y - num2;
			}
			return flag;
		}
		return false;
	}

	// Token: 0x060028DC RID: 10460 RVA: 0x001B1B7C File Offset: 0x001AFF7C
	public static bool DoLinesIntersect(Vector2 _l1a, Vector2 _l1b, Vector2 _l2a, Vector2 _l2b, ref Vector2 _pos)
	{
		float num = (_l2b.y - _l2a.y) * (_l1b.x - _l1a.x) - (_l2b.x - _l2a.x) * (_l1b.y - _l1a.y);
		if (num == 0f)
		{
			return false;
		}
		float num2 = (_l2b.x - _l2a.x) * (_l1a.y - _l2a.y) - (_l2b.y - _l2a.y) * (_l1a.x - _l2a.x);
		float num3 = (_l1b.x - _l1a.x) * (_l1a.y - _l2a.y) - (_l1b.y - _l1a.y) * (_l1a.x - _l2a.x);
		float num4 = num2 / num;
		float num5 = num3 / num;
		if (num4 >= 0f && num4 <= 1f && num5 >= 0f && num5 <= 1f)
		{
			_pos.x = _l1a.x + num4 * (_l1b.x - _l1a.x);
			_pos.y = _l1a.y + num4 * (_l1b.y - _l1a.y);
			return true;
		}
		return false;
	}

	// Token: 0x060028DD RID: 10461 RVA: 0x001B1CD0 File Offset: 0x001B00D0
	public static float Sign(Vector2 p1, Vector2 p2, Vector2 p3)
	{
		return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
	}

	// Token: 0x060028DE RID: 10462 RVA: 0x001B1D1C File Offset: 0x001B011C
	public static bool PointInTriangle(Vector2 pt, Vector2 v1, Vector2 v2, Vector2 v3)
	{
		bool flag = ToolBox.Sign(pt, v1, v2) < 0f;
		bool flag2 = ToolBox.Sign(pt, v2, v3) < 0f;
		bool flag3 = ToolBox.Sign(pt, v3, v1) < 0f;
		return flag == flag2 && flag2 == flag3;
	}

	// Token: 0x060028DF RID: 10463 RVA: 0x001B1D67 File Offset: 0x001B0167
	public static float getPositionBetween(float val, float min, float max)
	{
		return ToolBox.limitBetween((val - min) / (max - min), 0f, 1f);
	}

	// Token: 0x060028E0 RID: 10464 RVA: 0x001B1D7F File Offset: 0x001B017F
	public static float limitBetween(float val, float min, float max)
	{
		if (val < min)
		{
			val = min;
		}
		if (val > max)
		{
			val = max;
		}
		return val;
	}

	// Token: 0x060028E1 RID: 10465 RVA: 0x001B1D96 File Offset: 0x001B0196
	public static int limitBetween(int val, int min, int max)
	{
		if (val < min)
		{
			val = min;
		}
		if (val > max)
		{
			val = max;
		}
		return val;
	}

	// Token: 0x060028E2 RID: 10466 RVA: 0x001B1DAD File Offset: 0x001B01AD
	public static bool IsBetween(float _value, float _min, float _max)
	{
		return _value >= _min && _value <= _max;
	}

	// Token: 0x060028E3 RID: 10467 RVA: 0x001B1DC0 File Offset: 0x001B01C0
	public static float getCappedAngle(float angle)
	{
		return ToolBox.getRolledValue(angle, 0f, 360f);
	}

	// Token: 0x060028E4 RID: 10468 RVA: 0x001B1DD4 File Offset: 0x001B01D4
	public static float getRolledValue(float val, float minValue, float maxValue)
	{
		if (maxValue == minValue)
		{
			val = minValue;
		}
		float num = Mathf.Floor((val - minValue) / (maxValue - minValue));
		val -= num * (maxValue - minValue);
		return val;
	}

	// Token: 0x060028E5 RID: 10469 RVA: 0x001B1E04 File Offset: 0x001B0204
	public static int getRolledValue(int val, int minValue, int maxValue)
	{
		maxValue++;
		if (maxValue == minValue)
		{
			val = minValue;
		}
		int num = (int)Mathf.Floor((float)(val - minValue) / (float)(maxValue - minValue));
		val -= num * (maxValue - minValue);
		return val;
	}

	// Token: 0x060028E6 RID: 10470 RVA: 0x001B1E3C File Offset: 0x001B023C
	public static Vector3 interpolateVector3(Vector3 fromVector, Vector3 toVector, float pos)
	{
		Vector3 vector = toVector - fromVector;
		return fromVector + vector * pos;
	}

	// Token: 0x060028E7 RID: 10471 RVA: 0x001B1E5E File Offset: 0x001B025E
	public static float getAngleFromVector2(Vector2 vec)
	{
		return Mathf.Atan2(vec.y, vec.x);
	}

	// Token: 0x060028E8 RID: 10472 RVA: 0x001B1E74 File Offset: 0x001B0274
	public static float interpolateAngles(float angCur, float angNew, float pos)
	{
		float num = angNew - angCur;
		if (Mathf.Abs(num) > 180f)
		{
			num -= Mathf.Sign(num) * 360f;
		}
		return ToolBox.getCappedAngle(angCur + num * pos);
	}

	// Token: 0x060028E9 RID: 10473 RVA: 0x001B1EB0 File Offset: 0x001B02B0
	public static int[] sortTable(int[] table, float[] keys)
	{
		int num = table.Length;
		for (int i = 1; i < num; i++)
		{
			int num2 = table[i];
			float num3 = keys[i];
			int num4 = i;
			while (num4 > 0 && keys[num4 - 1] > num3)
			{
				table[num4] = table[num4 - 1];
				keys[num4] = keys[num4 - 1];
				num4--;
			}
			table[num4] = num2;
			keys[num4] = num3;
		}
		return table;
	}

	// Token: 0x060028EA RID: 10474 RVA: 0x001B1F14 File Offset: 0x001B0314
	public static void sortMeshOnZAxis(Mesh mesh)
	{
		int[] array = mesh.GetTriangles(0);
		float[] array2 = new float[array.Length / 3];
		for (int i = 0; i < array2.Length; i++)
		{
			int num = i * 3;
			int num2 = array[num];
			int num3 = array[num + 1];
			int num4 = array[num + 2];
			float num5 = (mesh.vertices[num2].z + mesh.vertices[num3].z + mesh.vertices[num4].z) / 3f;
			array2[i] = -num5;
		}
		array = ToolBox.sortTriangles(array, array2);
		mesh.SetTriangles(array, 0);
	}

	// Token: 0x060028EB RID: 10475 RVA: 0x001B1FB8 File Offset: 0x001B03B8
	public static Vector2 calculateNormal(Vector2 point1, Vector2 point2)
	{
		Vector2 vector = point2 - point1;
		vector..ctor(-vector.y, vector.x);
		return vector.normalized;
	}

	// Token: 0x060028EC RID: 10476 RVA: 0x001B1FEC File Offset: 0x001B03EC
	public static float getDistanceToCamera(Vector3 pos, Camera cam, bool useZ)
	{
		Vector3 position = cam.transform.position;
		if (!useZ)
		{
			pos.z = 0f;
			position.z = 0f;
		}
		return (pos - position).magnitude;
	}

	// Token: 0x060028ED RID: 10477 RVA: 0x001B2034 File Offset: 0x001B0434
	public static int[] sortTriangles(int[] triangles, float[] keys)
	{
		int num = triangles.Length / 3;
		Triangle[] array = new Triangle[num];
		for (int i = 0; i < num; i++)
		{
			int num2 = i * 3;
			array[i].v1 = triangles[num2];
			array[i].v2 = triangles[num2 + 1];
			array[i].v3 = triangles[num2 + 2];
		}
		int num3 = array.Length;
		for (int j = 1; j < num3; j++)
		{
			Triangle triangle = array[j];
			float num4 = keys[j];
			int num5 = j;
			while (num5 > 0 && keys[num5 - 1] > num4)
			{
				array[num5] = array[num5 - 1];
				keys[num5] = keys[num5 - 1];
				num5--;
			}
			array[num5] = triangle;
			keys[num5] = num4;
		}
		for (int k = 0; k < num; k++)
		{
			int num6 = k * 3;
			triangles[num6] = array[k].v1;
			triangles[num6 + 1] = array[k].v2;
			triangles[num6 + 2] = array[k].v3;
		}
		return triangles;
	}

	// Token: 0x060028EE RID: 10478 RVA: 0x001B217C File Offset: 0x001B057C
	public static Vector2[] shuffleArray(Vector2[] array)
	{
		for (int i = array.Length; i > 1; i--)
		{
			int num = Random.Range(0, i);
			Vector2 vector = array[num];
			array[num] = array[i - 1];
			array[i - 1] = vector;
		}
		return array;
	}

	// Token: 0x060028EF RID: 10479 RVA: 0x001B21DC File Offset: 0x001B05DC
	public static char[] shuffleArray(char[] array)
	{
		for (int i = array.Length; i > 1; i--)
		{
			int num = Random.Range(0, i);
			char c = array[num];
			array[num] = array[i - 1];
			array[i - 1] = c;
		}
		return array;
	}

	// Token: 0x060028F0 RID: 10480 RVA: 0x001B2218 File Offset: 0x001B0618
	public static string SHA256Sum(string strToEncrypt)
	{
		UTF8Encoding utf8Encoding = new UTF8Encoding();
		byte[] bytes = utf8Encoding.GetBytes(strToEncrypt);
		return ToolBox.SHA256Sum(bytes);
	}

	// Token: 0x060028F1 RID: 10481 RVA: 0x001B223C File Offset: 0x001B063C
	public static string SHA256Sum(byte[] _toEncrypt)
	{
		SHA256Managed sha256Managed = new SHA256Managed();
		byte[] array = sha256Managed.ComputeHash(_toEncrypt);
		string text = string.Empty;
		for (int i = 0; i < array.Length; i++)
		{
			text += Convert.ToString(array[i], 16).PadLeft(2, '0');
		}
		return text;
	}

	// Token: 0x060028F2 RID: 10482 RVA: 0x001B228C File Offset: 0x001B068C
	public static string getTimeStringFromSeconds(float seconds)
	{
		TimeSpan timeSpan = TimeSpan.FromSeconds((double)seconds);
		string text;
		if (timeSpan.Minutes > 0)
		{
			text = string.Format("{0:D2}:{1:D2}.{2:D3}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
		}
		else
		{
			text = string.Format("{0:D2}.{1:D3}", timeSpan.Seconds, timeSpan.Milliseconds);
		}
		return text;
	}

	// Token: 0x060028F3 RID: 10483 RVA: 0x001B2308 File Offset: 0x001B0708
	public static bool CircleSegmentIntersect(Vector3 circlePos, float radius, Vector3 segmentP1, Vector3 segmentP2)
	{
		Vector3 vector = circlePos - segmentP1;
		Vector3 vector2 = segmentP2 - segmentP1;
		float num = Vector3.Dot(vector2, vector2);
		float num2 = Vector3.Dot(vector, vector2);
		float num3 = num2 / num;
		if (num3 < 0f)
		{
			num3 = 0f;
		}
		else if (num3 > 1f)
		{
			num3 = 1f;
		}
		ToolBox.outVector3 = segmentP1 + num3 * vector2;
		Vector3 vector3 = ToolBox.outVector3 - circlePos;
		float num4 = Vector3.Dot(vector3, vector3);
		float num5 = radius * radius;
		return num4 <= num5;
	}

	// Token: 0x060028F4 RID: 10484 RVA: 0x001B23A4 File Offset: 0x001B07A4
	public static Vector2 PointOnBezierSpline(Vector2 p0_startHandle, Vector2 p1_start, Vector2 p2_end, Vector2 p3_endHandle, float t)
	{
		Vector2 vector = default(Vector2);
		float num = t * t;
		float num2 = num * t;
		vector.x = 0.5f * (2f * p1_start.x + (-p0_startHandle.x + p2_end.x) * t + (2f * p0_startHandle.x - 5f * p1_start.x + 4f * p2_end.x - p3_endHandle.x) * num + (-p0_startHandle.x + 3f * p1_start.x - 3f * p2_end.x + p3_endHandle.x) * num2);
		vector.y = 0.5f * (2f * p1_start.y + (-p0_startHandle.y + p2_end.y) * t + (2f * p0_startHandle.y - 5f * p1_start.y + 4f * p2_end.y - p3_endHandle.y) * num + (-p0_startHandle.y + 3f * p1_start.y - 3f * p2_end.y + p3_endHandle.y) * num2);
		return vector;
	}

	// Token: 0x060028F5 RID: 10485 RVA: 0x001B24E8 File Offset: 0x001B08E8
	public static Vector2 TangentOnBezier(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
	{
		float num = t * t;
		return -3f * Mathf.Pow(1f - t, 2f) * p0 + 3f * Mathf.Pow(1f - t, 2f) * p1 - 6f * t * (1f - t) * p1 - 3f * num * p2 + 6f * t * (1f - t) * p2 + 3f * num * p3;
	}

	// Token: 0x060028F6 RID: 10486 RVA: 0x001B2598 File Offset: 0x001B0998
	public static float LengthOfBezier(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
	{
		Vector2 vector;
		vector.x = p0.x - 2f * p1.x + p2.x;
		vector.y = p0.y - 2f * p1.y + p2.y;
		Vector2 vector2;
		vector2.x = 2f * p1.x - 2f * p0.x;
		vector2.y = 2f * p1.y - 2f * p0.y;
		float num = 4f * (vector.x * vector.x + vector.y * vector.y);
		float num2 = 4f * (vector.x * vector2.x + vector.y * vector2.y);
		float num3 = vector2.x * vector2.x + vector2.y * vector2.y;
		float num4 = 2f * Mathf.Sqrt(num + num2 + num3);
		float num5 = Mathf.Sqrt(num);
		float num6 = 2f * num * num5;
		float num7 = 2f * Mathf.Sqrt(num3);
		float num8 = num2 / num5;
		return (num6 * num4 + num5 * num2 * (num4 - num7) + (4f * num3 * num - num2 * num2) * Mathf.Log((2f * num5 + num8 + num4) / (num8 + num7))) / (4f * num6);
	}

	// Token: 0x060028F7 RID: 10487 RVA: 0x001B271C File Offset: 0x001B0B1C
	public static Vector2 GetBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
	{
		float num = 3f * (p1.x - p0.x);
		float num2 = 3f * (p2.x - p1.x) - num;
		float num3 = p3.x - p0.x - num - num2;
		float num4 = 3f * (p1.y - p0.y);
		float num5 = 3f * (p2.y - p1.y) - num4;
		float num6 = p3.y - p0.y - num4 - num5;
		float num7 = t * t * t;
		float num8 = t * t;
		float num9 = num3 * num7 + num2 * num8 + num * t + p0.x;
		float num10 = num6 * num7 + num5 * num8 + num4 * t + p0.y;
		return new Vector2(num9, num10);
	}

	// Token: 0x060028F8 RID: 10488 RVA: 0x001B27F8 File Offset: 0x001B0BF8
	public static Vector2 CosineInterpolate(float t, Vector2 p1, Vector2 p2)
	{
		float num = (1f - Mathf.Cos(t * 3.1415927f)) / 2f;
		return p1 * (1f - num) + p2 * num;
	}

	// Token: 0x060028F9 RID: 10489 RVA: 0x001B2838 File Offset: 0x001B0C38
	public static Vector2 GetCenterOfMass(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
	{
		float num = Mathf.Pow(1f - t, 3f);
		float num2 = 3f * t * Mathf.Pow(1f - t, 2f);
		float num3 = 3f * (t * t) * (1f - t);
		float num4 = t * t * t;
		return (p0 * num + p1 * num2 + p2 * num3 + p3 * num4) / (num + num2 + num3 + num4);
	}

	// Token: 0x060028FA RID: 10490 RVA: 0x001B28CC File Offset: 0x001B0CCC
	public static Mesh DuplicateMesh(Mesh _mesh)
	{
		Mesh mesh = new Mesh();
		Vector3[] vertices = _mesh.vertices;
		Vector3[] normals = _mesh.normals;
		Vector2[] uv = _mesh.uv;
		int[] triangles = _mesh.triangles;
		Color[] colors = _mesh.colors;
		mesh.vertices = vertices;
		mesh.normals = normals;
		mesh.triangles = triangles;
		mesh.uv = uv;
		mesh.colors = colors;
		mesh.RecalculateBounds();
		mesh.UploadMeshData(false);
		return mesh;
	}

	// Token: 0x060028FB RID: 10491 RVA: 0x001B2938 File Offset: 0x001B0D38
	public static void calculateMeshTangents(Mesh mesh, bool _calculateNormals)
	{
		if (_calculateNormals)
		{
			mesh.RecalculateNormals();
		}
		int[] triangles = mesh.triangles;
		Vector3[] vertices = mesh.vertices;
		Vector2[] uv = mesh.uv;
		Vector3[] normals = mesh.normals;
		int num = triangles.Length;
		int num2 = vertices.Length;
		Vector3[] array = new Vector3[num2];
		Vector3[] array2 = new Vector3[num2];
		Vector4[] array3 = new Vector4[num2];
		for (long num3 = 0L; num3 < (long)num; num3 += 3L)
		{
			long num4 = (long)triangles[(int)(checked((IntPtr)num3))];
			long num5 = (long)triangles[(int)(checked((IntPtr)(unchecked(num3 + 1L))))];
			long num6 = (long)triangles[(int)(checked((IntPtr)(unchecked(num3 + 2L))))];
			Vector3 vector;
			Vector3 vector2;
			Vector3 vector3;
			Vector2 vector4;
			Vector2 vector5;
			Vector2 vector6;
			checked
			{
				vector = vertices[(int)((IntPtr)num4)];
				vector2 = vertices[(int)((IntPtr)num5)];
				vector3 = vertices[(int)((IntPtr)num6)];
				vector4 = uv[(int)((IntPtr)num4)];
				vector5 = uv[(int)((IntPtr)num5)];
				vector6 = uv[(int)((IntPtr)num6)];
			}
			float num7 = vector2.x - vector.x;
			float num8 = vector3.x - vector.x;
			float num9 = vector2.y - vector.y;
			float num10 = vector3.y - vector.y;
			float num11 = vector2.z - vector.z;
			float num12 = vector3.z - vector.z;
			float num13 = vector5.x - vector4.x;
			float num14 = vector6.x - vector4.x;
			float num15 = vector5.y - vector4.y;
			float num16 = vector6.y - vector4.y;
			float num17 = 1f / (num13 * num16 - num14 * num15);
			Vector3 vector7;
			vector7..ctor((num16 * num7 - num15 * num8) * num17, (num16 * num9 - num15 * num10) * num17, (num16 * num11 - num15 * num12) * num17);
			Vector3 vector8;
			vector8..ctor((num13 * num8 - num14 * num7) * num17, (num13 * num10 - num14 * num9) * num17, (num13 * num12 - num14 * num11) * num17);
			checked
			{
				array[(int)((IntPtr)num4)] += vector7;
				array[(int)((IntPtr)num5)] += vector7;
				array[(int)((IntPtr)num6)] += vector7;
				array2[(int)((IntPtr)num4)] += vector8;
				array2[(int)((IntPtr)num5)] += vector8;
				array2[(int)((IntPtr)num6)] += vector8;
			}
		}
		for (int i = 0; i < num2; i++)
		{
			Vector3 vector9 = normals[i];
			Vector3 vector10 = array[i];
			Vector3.OrthoNormalize(ref vector9, ref vector10);
			array3[i].x = vector10.x;
			array3[i].y = vector10.y;
			array3[i].z = vector10.z;
			array3[i].w = ((Vector3.Dot(Vector3.Cross(vector9, vector10), array2[i]) >= 0f) ? 1f : (-1f));
		}
		mesh.tangents = array3;
	}

	// Token: 0x060028FC RID: 10492 RVA: 0x001B2CBC File Offset: 0x001B10BC
	public static Transform SearchHierarchyForBone(Transform current, string name)
	{
		if (current.name == name)
		{
			return current;
		}
		for (int i = 0; i < current.childCount; i++)
		{
			Transform transform = ToolBox.SearchHierarchyForBone(current.GetChild(i), name);
			if (transform != null)
			{
				return transform;
			}
		}
		return null;
	}

	// Token: 0x060028FD RID: 10493 RVA: 0x001B2D10 File Offset: 0x001B1110
	public static List<GameObject> GetAllChildrenTaggedStatic(GameObject _go, List<GameObject> _children = null)
	{
		if (_children == null)
		{
			_children = new List<GameObject>();
		}
		for (int i = 0; i < _go.transform.childCount; i++)
		{
			GameObject gameObject = _go.transform.GetChild(i).gameObject;
			if (gameObject.isStatic)
			{
				_children.Add(gameObject);
			}
			ToolBox.GetAllChildrenTaggedStatic(gameObject, _children);
		}
		return _children;
	}

	// Token: 0x060028FE RID: 10494 RVA: 0x001B2D74 File Offset: 0x001B1174
	public static float PointInsideEllipse(Vector2 _p, Vector2 _eCenter, Vector2 _eRad, float angle)
	{
		if (angle == 0f)
		{
			float x = _eRad.x;
			float y = _eRad.y;
			float num = _p.x - _eCenter.x;
			float num2 = _p.y - _eCenter.y;
			return num * num / (x * x) + num2 * num2 / (y * y);
		}
		float num3 = Mathf.Cos(angle);
		float num4 = Mathf.Sin(angle);
		float num5 = _eRad.x * _eRad.x;
		float num6 = _eRad.y * _eRad.y;
		float num7 = Mathf.Pow(num3 * (_p.x - _eCenter.x) + num4 * (_p.y - _eCenter.y), 2f);
		float num8 = Mathf.Pow(num4 * (_p.x - _eCenter.x) - num3 * (_p.y - _eCenter.y), 2f);
		return num7 / num5 + num8 / num6;
	}

	// Token: 0x060028FF RID: 10495 RVA: 0x001B2E6C File Offset: 0x001B126C
	public static string ColorToHex(Color32 color)
	{
		return color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
	}

	// Token: 0x06002900 RID: 10496 RVA: 0x001B2EB4 File Offset: 0x001B12B4
	public static Color HexToColor(string hex)
	{
		byte b = byte.Parse(hex.Substring(0, 2), 515);
		byte b2 = byte.Parse(hex.Substring(2, 2), 515);
		byte b3 = byte.Parse(hex.Substring(4, 2), 515);
		return new Color32(b, b2, b3, byte.MaxValue);
	}

	// Token: 0x06002901 RID: 10497 RVA: 0x001B2F0C File Offset: 0x001B130C
	public static string replaceCommasInsideBracketsAndRemoveBrackets(string line, string replaceWith)
	{
		string text = string.Empty + Convert.ToChar(34);
		for (int i = 0; i < line.Length; i++)
		{
			string text2 = line.Substring(i, 1);
			if (text2.Equals(text))
			{
				for (int j = i + 1; j < line.Length; j++)
				{
					string text3 = line.Substring(j, 1);
					if (text3.Equals(text))
					{
						for (int k = i + 1; k < j; k++)
						{
							string text4 = line.Substring(k, 1);
							if (text4.Equals(","))
							{
								line = line.Remove(k, 1);
								line = line.Insert(k, "|");
							}
						}
						line = line.Remove(j, 1);
						break;
					}
				}
				line = line.Remove(i, 1);
			}
		}
		return line;
	}

	// Token: 0x06002902 RID: 10498 RVA: 0x001B2FF4 File Offset: 0x001B13F4
	public static PerformanceClass GetDevicePerformanceClass()
	{
		if (Application.platform != 8)
		{
			return PerformanceClass.SLOW;
		}
		return PerformanceClass.SLOW;
	}

	// Token: 0x06002903 RID: 10499 RVA: 0x001B3004 File Offset: 0x001B1404
	public static string GetUTF8String(string _string)
	{
		if (_string != null)
		{
			byte[] bytes = Encoding.Default.GetBytes(_string);
			return Encoding.UTF8.GetString(bytes);
		}
		Debug.LogError("SHOULD NOT BE NULL!!!");
		return string.Empty;
	}

	// Token: 0x04002E0C RID: 11788
	public static float lastOutDistLine1;

	// Token: 0x04002E0D RID: 11789
	public static float lastOutDistLine2;

	// Token: 0x04002E0E RID: 11790
	public static Vector2 lastLineIntersectionPoint;

	// Token: 0x04002E0F RID: 11791
	public static Vector3 outVector3;
}
