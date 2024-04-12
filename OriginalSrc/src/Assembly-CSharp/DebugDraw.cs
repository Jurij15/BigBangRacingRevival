using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000563 RID: 1379
public class DebugDraw
{
	// Token: 0x06002834 RID: 10292 RVA: 0x001AB8A8 File Offset: 0x001A9CA8
	public static void Initialize()
	{
		if (CameraS.m_mainCamera == null || CameraS.m_uiCamera == null)
		{
			Debug.LogError("Initialize CameraS first");
		}
		else
		{
			DebugDraw.p_spriteSheet = SpriteS.AddSpriteSheet(CameraS.m_mainCamera, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), 1f);
			DebugDraw.p_uiSpriteSheet = SpriteS.AddSpriteSheet(CameraS.m_uiCamera, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), 1f);
			DebugDraw.defaultColor = new Color(1f, 1f, 1f, 1f);
			DebugDraw.m_debugFrame = new Frame(0f, 0f, 128f, 128f);
			Entity entity = EntityManager.AddEntity("DebugDraw");
			DebugDraw.m_debugTC = TransformS.AddComponent(entity, "DebugDraw");
			entity.m_persistent = true;
		}
	}

	// Token: 0x06002835 RID: 10293 RVA: 0x001AB980 File Offset: 0x001A9D80
	public static void Clear(Camera _camera)
	{
		SpriteSheet spriteSheet = DebugDraw.p_spriteSheet;
		if (DebugDraw.p_uiSpriteSheet.m_camera == _camera)
		{
			spriteSheet = DebugDraw.p_uiSpriteSheet;
		}
		while (DebugDraw.p_spriteSheet.m_components.m_aliveCount > 0)
		{
			SpriteS.RemoveComponent(spriteSheet.m_components.m_array[spriteSheet.m_components.m_aliveIndices[0]]);
		}
	}

	// Token: 0x06002836 RID: 10294 RVA: 0x001AB9E8 File Offset: 0x001A9DE8
	public static void Clear(Camera _camera, TransformC _tc)
	{
		List<SpriteC> spritesByTransform = SpriteS.GetSpritesByTransform(_tc);
		while (spritesByTransform.Count > 0)
		{
			SpriteS.RemoveComponent(spritesByTransform[0]);
			spritesByTransform.RemoveAt(0);
		}
	}

	// Token: 0x06002837 RID: 10295 RVA: 0x001ABA20 File Offset: 0x001A9E20
	public static SpriteC CreateLine(Camera _camera, TransformC _transformComponent, float _length, Vector3 _offset, float _offsetAngle)
	{
		SpriteSheet spriteSheet = DebugDraw.p_spriteSheet;
		if (DebugDraw.p_uiSpriteSheet.m_camera == _camera)
		{
			spriteSheet = DebugDraw.p_uiSpriteSheet;
		}
		SpriteC spriteC = SpriteS.AddComponent(_transformComponent, DebugDraw.m_debugFrame, spriteSheet);
		SpriteS.SetOffset(spriteC, _offset, _offsetAngle);
		SpriteS.SetDimensions(spriteC, _length, DebugDraw.m_lineWidth);
		SpriteS.SetColor(spriteC, DebugDraw.defaultColor);
		return spriteC;
	}

	// Token: 0x06002838 RID: 10296 RVA: 0x001ABA7C File Offset: 0x001A9E7C
	public static SpriteC CreateLine(Camera _camera, TransformC _transformComponent, Vector3 _start, Vector3 _end)
	{
		SpriteSheet spriteSheet = DebugDraw.p_spriteSheet;
		if (DebugDraw.p_uiSpriteSheet.m_camera == _camera)
		{
			spriteSheet = DebugDraw.p_uiSpriteSheet;
		}
		SpriteC spriteC = SpriteS.AddComponent(_transformComponent, DebugDraw.m_debugFrame, spriteSheet);
		Vector3 vector = (_start + _end) * 0.5f;
		Vector3 vector2 = _end - _start;
		float num = Mathf.Atan2(vector2.y, vector2.x) * 57.29578f;
		SpriteS.SetOffset(spriteC, vector, num);
		SpriteS.SetDimensions(spriteC, vector2.magnitude, DebugDraw.m_lineWidth);
		SpriteS.SetColor(spriteC, DebugDraw.defaultColor);
		return spriteC;
	}

	// Token: 0x06002839 RID: 10297 RVA: 0x001ABB14 File Offset: 0x001A9F14
	public static void CreateBox(Camera _camera, TransformC _transformComponent, Vector3 _pos, float _width, float _height, bool _createAngleMarker = false)
	{
		Vector3 vector;
		vector..ctor(_width * 0.5f, 0f, 0f);
		Vector3 vector2;
		vector2..ctor(0f, _height * 0.5f, 0f);
		DebugDraw.CreateLine(_camera, _transformComponent, _width, vector2 + _pos, 0f);
		DebugDraw.CreateLine(_camera, _transformComponent, _width, -vector2 + _pos, 0f);
		DebugDraw.CreateLine(_camera, _transformComponent, _height, vector + _pos, 90f);
		DebugDraw.CreateLine(_camera, _transformComponent, _height, -vector + _pos, 90f);
		if (_createAngleMarker)
		{
			float num = _width * 0.5f * 0.5f;
			float num2 = _height * 0.5f * 0.5f;
			DebugDraw.CreateLine(_camera, _transformComponent, num, _pos + new Vector3(num * 0.5f, 0f, 0f), 0f);
			DebugDraw.CreateLine(_camera, _transformComponent, num2, _pos + new Vector3(0f, num2 * 0.5f, 0f), 90f);
		}
	}

	// Token: 0x0600283A RID: 10298 RVA: 0x001ABC2C File Offset: 0x001AA02C
	public static void CreateCircle(Camera _camera, TransformC _transformComponent, Vector3 pos, float radius, bool createAngleMarker = false, int segments = 16)
	{
		float num = 360f / (float)segments;
		float num2 = 3.1415927f * radius;
		num2 /= (float)segments;
		float num3 = 90f;
		for (int i = 0; i < segments + 1; i++)
		{
			Vector3 vector = pos + new Vector3(Mathf.Cos(num3 * 0.017453292f), Mathf.Sin(num3 * 0.017453292f), 0f) * radius;
			if (i > 0)
			{
				DebugDraw.CreateLine(_camera, _transformComponent, num2 * 2f, vector, num * (float)i);
			}
			num3 = ToolBox.getCappedAngle(num3 + num);
		}
		if (createAngleMarker)
		{
			DebugDraw.CreateLine(_camera, _transformComponent, radius, pos + new Vector3(0f, radius * 0.5f, 0f), 90f);
		}
	}

	// Token: 0x0600283B RID: 10299 RVA: 0x001ABCF4 File Offset: 0x001AA0F4
	public static void DrawVectorArray(Camera _camera, TransformC _tc, Vector2[] _points)
	{
		for (int i = 0; i < _points.Length - 1; i++)
		{
			Vector2 vector = _points[i];
			Vector2 vector2 = _points[i + 1];
			Vector2 vector3 = vector2 - vector;
			Vector2 vector4 = vector + vector3 * 0.5f;
			float num = Mathf.Atan2(vector3.y, vector3.x) * 57.29578f;
			DebugDraw.CreateLine(_camera, _tc, vector3.magnitude, vector4, num);
		}
	}

	// Token: 0x0600283C RID: 10300 RVA: 0x001ABD84 File Offset: 0x001AA184
	public static void AddRandom(Vector2[] _array, float _amount)
	{
		for (int i = 0; i < _array.Length; i++)
		{
			_array[i] += new Vector2(Random.Range(_amount * -0.5f, _amount * 0.5f), Random.Range(_amount * -0.5f, _amount * 0.5f));
		}
	}

	// Token: 0x0600283D RID: 10301 RVA: 0x001ABDF0 File Offset: 0x001AA1F0
	public static void AddRadialRandom(Vector2[] _array, float _amount)
	{
		Vector2 vector = Vector2.zero;
		for (int i = 0; i < _array.Length; i++)
		{
			vector += _array[i];
		}
		vector /= (float)_array.Length;
		for (int j = 0; j < _array.Length; j++)
		{
			_array[j] = (_array[j] - vector).normalized * (Random.Range(0f, _amount) - _amount) + _array[j];
		}
	}

	// Token: 0x0600283E RID: 10302 RVA: 0x001ABE94 File Offset: 0x001AA294
	public static void AddRadialRandomToOutside(Vector2[] _array, float _amount)
	{
		Vector2 vector = Vector2.zero;
		for (int i = 0; i < _array.Length; i++)
		{
			vector += _array[i];
		}
		vector /= (float)_array.Length;
		for (int j = 0; j < _array.Length; j++)
		{
			_array[j] = (_array[j] - vector).normalized * Random.Range(0f, _amount) + _array[j];
		}
	}

	// Token: 0x0600283F RID: 10303 RVA: 0x001ABF34 File Offset: 0x001AA334
	public static void AddRadialStarShift(Vector2[] _array, float _amount)
	{
		Vector2 vector = Vector2.zero;
		for (int i = 0; i < _array.Length; i++)
		{
			vector += _array[i];
		}
		vector /= (float)_array.Length;
		for (int j = 0; j < _array.Length; j++)
		{
			if (j % 2 == 0)
			{
				_array[j] = (_array[j] - vector).normalized * _amount + _array[j];
			}
		}
	}

	// Token: 0x06002840 RID: 10304 RVA: 0x001ABFD4 File Offset: 0x001AA3D4
	public static void ScaleVectorArray(Vector2[] _vectorArray, Vector2 _amount)
	{
		for (int i = 0; i < _vectorArray.Length; i++)
		{
			int num = i;
			_vectorArray[num].x = _vectorArray[num].x * _amount.x;
			int num2 = i;
			_vectorArray[num2].y = _vectorArray[num2].y * _amount.y;
		}
	}

	// Token: 0x06002841 RID: 10305 RVA: 0x001AC02C File Offset: 0x001AA42C
	public static Vector2[] ExpandVectorArray(Vector2[] _vectorArray, float _amount)
	{
		Vector2[] array = new Vector2[_vectorArray.Length];
		_vectorArray.CopyTo(array, 0);
		if (array.Length > 2)
		{
			for (int i = 0; i < array.Length; i++)
			{
				Vector2 vector = array[i];
				Vector2 vector2;
				Vector2 vector3;
				if (i == 0)
				{
					vector2 = array[array.Length - 1];
					vector3 = array[i + 1];
				}
				else if (i == array.Length - 1)
				{
					vector2 = array[i - 1];
					vector3 = array[0];
				}
				else
				{
					vector2 = array[i - 1];
					vector3 = array[i + 1];
				}
				Vector2 vector4 = vector3 - vector2;
				float num = Mathf.Atan2(-vector4.y, vector4.x);
				float num2 = Mathf.Sin(num) * _amount;
				float num3 = Mathf.Cos(num) * _amount;
				array[i] = new Vector2(vector.x + num2, vector.y + num3);
			}
		}
		return array;
	}

	// Token: 0x06002842 RID: 10306 RVA: 0x001AC148 File Offset: 0x001AA548
	public static Vector2[] ExtrudePath(Vector2[] _path, float _width)
	{
		Vector2[] array = new Vector2[_path.Length * 2];
		if (_path.Length > 1)
		{
			for (int i = 0; i < _path.Length; i++)
			{
				Vector2 vector = _path[i];
				Vector2 vector2;
				if (i == 0)
				{
					vector2 = _path[i + 1];
				}
				else if (i == _path.Length - 1)
				{
					vector2 = _path[i] + (_path[i] - _path[i - 1]);
				}
				else
				{
					vector2 = _path[i + 1];
				}
				Vector2 vector3 = vector2 - vector;
				float num = Mathf.Atan2(-vector3.y, vector3.x);
				float num2 = Mathf.Sin(num) * _width * 0.5f;
				float num3 = Mathf.Cos(num) * _width * 0.5f;
				array[i] = new Vector2(vector.x - num2, vector.y - num3);
				array[array.Length - i - 1] = new Vector2(vector.x + num2, vector.y + num3);
			}
		}
		return array;
	}

	// Token: 0x06002843 RID: 10307 RVA: 0x001AC286 File Offset: 0x001AA686
	public static Vector2[] GetCircle(float _radius, int _segments, Vector2 _offset)
	{
		return DebugDraw.GetCircle(_radius, _segments, _offset, false);
	}

	// Token: 0x06002844 RID: 10308 RVA: 0x001AC291 File Offset: 0x001AA691
	public static Vector2[] GetCircle(float _radius, int _segments, Vector2 _offset, bool _closed)
	{
		if (_closed)
		{
			return DebugDraw.GetArc(_radius, _segments, 360f, 0f, _offset);
		}
		return DebugDraw.GetArc(_radius, _segments, 360f - 360f / (float)_segments, 0f, _offset);
	}

	// Token: 0x06002845 RID: 10309 RVA: 0x001AC2C7 File Offset: 0x001AA6C7
	public static Vector2[] GetRect(float _width, float _height, Vector2 _offset)
	{
		return DebugDraw.GetRect(_width, _height, _offset, false);
	}

	// Token: 0x06002846 RID: 10310 RVA: 0x001AC2D2 File Offset: 0x001AA6D2
	public static Vector2[] GetRect(float _width, float _height, Vector2 _offset, bool _closed)
	{
		return DebugDraw.GetRoundedRect(_width, _height, 0f, 1, _offset, _closed);
	}

	// Token: 0x06002847 RID: 10311 RVA: 0x001AC2E4 File Offset: 0x001AA6E4
	public static Vector2[] GetLine(Vector2 _start, Vector2 _end, int _middlePointCount)
	{
		Vector2[] array = new Vector2[_middlePointCount + 2];
		Vector2 vector = _end - _start;
		int num = _middlePointCount + 1;
		Vector2 vector2 = vector / (float)num;
		array[0] = _start;
		array[array.Length - 1] = _end;
		for (int i = 1; i < array.Length - 1; i++)
		{
			array[i] = _start + vector2 * (float)i;
		}
		return array;
	}

	// Token: 0x06002848 RID: 10312 RVA: 0x001AC364 File Offset: 0x001AA764
	public static Vector2[] GetRoundedRect(float _width, float _height, float _radius, int _segments, Vector2 _offset)
	{
		return DebugDraw.GetRoundedRect(_width, _height, _radius, _segments, _offset, true);
	}

	// Token: 0x06002849 RID: 10313 RVA: 0x001AC374 File Offset: 0x001AA774
	public static Vector2[] GetRoundedRect(float _width, float _height, float _radius, int _segments, Vector2 _offset, bool _closed)
	{
		int num = 0;
		if (_closed)
		{
			num = 1;
		}
		Vector2[] array;
		if (_height > _radius * 2f && _width > _radius * 2f)
		{
			array = new Vector2[_segments * 4 + num];
			Vector2[] arc = DebugDraw.GetArc(_radius, _segments, 90f, 270f, new Vector2(_width * 0.5f - _radius, _height * -0.5f + _radius) + _offset);
			Vector2[] arc2 = DebugDraw.GetArc(_radius, _segments, 90f, 180f, new Vector2(_width * -0.5f + _radius, _height * -0.5f + _radius) + _offset);
			Vector2[] arc3 = DebugDraw.GetArc(_radius, _segments, 90f, 90f, new Vector2(_width * -0.5f + _radius, _height * 0.5f - _radius) + _offset);
			Vector2[] arc4 = DebugDraw.GetArc(_radius, _segments, 90f, 0f, new Vector2(_width * 0.5f - _radius, _height * 0.5f - _radius) + _offset);
			arc.CopyTo(array, 0);
			arc2.CopyTo(array, _segments);
			arc3.CopyTo(array, _segments * 2);
			arc4.CopyTo(array, _segments * 3);
			if (_closed)
			{
				array[array.Length - 1] = arc[0];
			}
		}
		else if (_height > _radius * 2f)
		{
			_radius = _width * 0.5f;
			array = new Vector2[_segments * 4 + num];
			Vector2[] arc5 = DebugDraw.GetArc(_radius, _segments * 2, 180f, 180f, new Vector2(0f, _height * -0.5f + _radius) + _offset);
			Vector2[] arc6 = DebugDraw.GetArc(_radius, _segments * 2, 180f, 0f, new Vector2(0f, _height * 0.5f - _radius) + _offset);
			arc5.CopyTo(array, 0);
			arc6.CopyTo(array, _segments * 2);
			if (_closed)
			{
				array[array.Length - 1] = arc5[0];
			}
		}
		else if (_width > _radius * 2f)
		{
			_radius = _height * 0.5f;
			array = new Vector2[_segments * 4 + num];
			Vector2[] arc7 = DebugDraw.GetArc(_radius, _segments * 2, 180f, 90f, new Vector2(_width * -0.5f + _radius, 0f) + _offset);
			Vector2[] arc8 = DebugDraw.GetArc(_radius, _segments * 2, 180f, 270f, new Vector2(_width * 0.5f - _radius, 0f) + _offset);
			arc7.CopyTo(array, 0);
			arc8.CopyTo(array, _segments * 2);
			if (_closed)
			{
				array[array.Length - 1] = arc7[0];
			}
		}
		else
		{
			array = new Vector2[_segments * 4 + num];
			Vector2[] arc9 = DebugDraw.GetArc(_radius, _segments * 4, 360f - 360f / (float)_segments * 4f, 0f, _offset);
			arc9.CopyTo(array, 0);
			if (_closed)
			{
				array[array.Length - 1] = arc9[0];
			}
		}
		return array;
	}

	// Token: 0x0600284A RID: 10314 RVA: 0x001AC69C File Offset: 0x001AAA9C
	public static Vector2[] GetArc(float radius, int _segments, float _arcAngle, float _startAngle, Vector2 _offset)
	{
		Vector2[] array = new Vector2[_segments];
		float num = _arcAngle / (float)(_segments - 1);
		float num2 = 3.1415927f * radius;
		num2 /= (float)_segments;
		float num3 = _startAngle;
		for (int i = _segments - 1; i > -1; i--)
		{
			array[i] = _offset + new Vector2(Mathf.Cos(num3 * 0.017453292f), Mathf.Sin(num3 * 0.017453292f)) * radius;
			num3 = ToolBox.getCappedAngle(num3 + num);
		}
		return array;
	}

	// Token: 0x0600284B RID: 10315 RVA: 0x001AC720 File Offset: 0x001AAB20
	public static Vector2[] GetBezierRect(float _width, float _height, float _cornerSize, int _segments, Vector2 _offset)
	{
		if (_width < 0f || _height < 0f)
		{
			Debug.LogWarning("Negative width or height!");
			return null;
		}
		Vector2[] array = new Vector2[_segments * 4];
		Vector2 vector;
		vector..ctor(_width * -0.5f, _height * 0.5f);
		Vector2 vector2;
		vector2..ctor(_width * 0.5f, _height * 0.5f);
		Vector2 vector3;
		vector3..ctor(_width * 0.5f, _height * -0.5f);
		Vector2 vector4;
		vector4..ctor(_width * -0.5f, _height * -0.5f);
		float num = 0.5f;
		Vector2 vector5 = vector + new Vector2(_cornerSize * num, _cornerSize);
		Vector2 vector6 = vector2 + new Vector2(-_cornerSize * num, _cornerSize);
		Vector2 vector7 = vector2 + new Vector2(_cornerSize, -_cornerSize * num);
		Vector2 vector8 = vector3 + new Vector2(_cornerSize, _cornerSize * num);
		Vector2 vector9 = vector3 + new Vector2(-_cornerSize * num, -_cornerSize);
		Vector2 vector10 = vector4 + new Vector2(_cornerSize * num, -_cornerSize);
		Vector2 vector11 = vector4 + new Vector2(-_cornerSize, _cornerSize * num);
		Vector2 vector12 = vector + new Vector2(-_cornerSize, -_cornerSize * num);
		for (int i = 0; i < _segments; i++)
		{
			array[i] = ToolBox.GetBezierPoint((float)i / (float)_segments, vector, vector5, vector6, vector2);
			array[i + _segments] = ToolBox.GetBezierPoint((float)i / (float)_segments, vector2, vector7, vector8, vector3);
			array[i + _segments * 2] = ToolBox.GetBezierPoint((float)i / (float)_segments, vector3, vector9, vector10, vector4);
			array[i + _segments * 3] = ToolBox.GetBezierPoint((float)i / (float)_segments, vector4, vector11, vector12, vector);
		}
		return array;
	}

	// Token: 0x0600284C RID: 10316 RVA: 0x001AC8E8 File Offset: 0x001AACE8
	public static Vector2[] AddSpeechHandleToVectorArray(Vector2[] _array, SpeechBubbleHandlePosition _handlePosition, SpeechBubbleHandleType _handleType)
	{
		int num = Mathf.RoundToInt((float)_array.Length * 0.25f);
		int num2 = num * 2 + Mathf.RoundToInt((float)num * 0.5f);
		if (_handlePosition == SpeechBubbleHandlePosition.BottomLeft)
		{
			num2 = num * 2 + Mathf.RoundToInt((float)num * 0.75f);
		}
		else if (_handlePosition == SpeechBubbleHandlePosition.BottomRight)
		{
			num2 = num * 2 + Mathf.RoundToInt((float)num * 0.25f);
		}
		else if (_handlePosition == SpeechBubbleHandlePosition.Left)
		{
			num2 = num * 3 + Mathf.RoundToInt((float)num * 0.382f);
		}
		else if (_handlePosition == SpeechBubbleHandlePosition.Right)
		{
			num2 = num + Mathf.RoundToInt((float)num * 0.618f);
		}
		else if (_handlePosition == SpeechBubbleHandlePosition.TopLeft)
		{
			num2 = num * 0 + Mathf.RoundToInt((float)num * 0.25f);
		}
		else if (_handlePosition == SpeechBubbleHandlePosition.TopRight)
		{
			num2 = num * 0 + Mathf.RoundToInt((float)num * 0.75f);
		}
		float num3 = Vector2.Distance(_array[0], _array[num * 3]);
		float num4 = num3 * 0.3f;
		float num5 = num4 * 0.25f;
		Vector2 vector = Vector2.zero;
		if (_handleType == SpeechBubbleHandleType.SmallToLeft)
		{
			num4 = (float)Screen.height * 0.05f;
			num5 = num4 * 0.25f;
			vector = Vector2.right * -num5;
		}
		else if (_handleType == SpeechBubbleHandleType.SmallToRight)
		{
			num4 = (float)Screen.height * 0.05f;
			num5 = num4 * 0.25f;
			vector = Vector2.right * num5;
		}
		List<Vector2> list = new List<Vector2>(_array);
		Vector2 vector2 = list[num2 - 1];
		Vector2 vector3 = list[num2];
		Vector2 vector4 = list[num2 + 1];
		Vector2 vector5 = vector3 + (vector2 - vector3).normalized * num5;
		Vector2 vector6 = vector3 + (vector4 - vector3).normalized * num5;
		Vector2 vector7 = Vector2.up * -num4 + vector;
		if (_handlePosition == SpeechBubbleHandlePosition.Left)
		{
			vector7 = Vector2.right * -num4 + new Vector2(vector.y, -vector.x);
		}
		else if (_handlePosition == SpeechBubbleHandlePosition.Right)
		{
			vector7 = Vector2.right * num4 + new Vector2(vector.y, vector.x);
		}
		else if (_handlePosition == SpeechBubbleHandlePosition.TopLeft || _handlePosition == SpeechBubbleHandlePosition.TopRight)
		{
			vector7 = Vector2.up * num4 + -vector;
		}
		List<Vector2> list2;
		int num6;
		(list2 = list)[num6 = num2] = list2[num6] + vector7;
		list.Insert(num2 + 1, vector6);
		list.Insert(num2, vector5);
		list.Insert(num2 + 1, list[num2 + 1]);
		return list.ToArray();
	}

	// Token: 0x0600284D RID: 10317 RVA: 0x001ACBB4 File Offset: 0x001AAFB4
	public static Vector2[] GetRoundedBezierRect(float _width, float _height, float _cornerSize = -1f, float _cornerRoundness = -1f, float _edgeRoundness = -1f, float _cornerRandom = 0.2f, bool _topLeftCorner = true, bool _topRightCorner = true, bool _bottomLeftCorner = true, bool _bottomRightCorner = true)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		if (_topLeftCorner)
		{
			num++;
			num2 = 1;
		}
		if (_topRightCorner)
		{
			num++;
			num3 = 1;
		}
		if (_bottomLeftCorner)
		{
			num++;
			num4 = 1;
		}
		if (_bottomRightCorner)
		{
			num++;
			num5 = 1;
		}
		float num6 = (float)Screen.height * 0.05f;
		if (_cornerSize != -1f)
		{
			num6 = _cornerSize;
		}
		float num7 = num6 * 0.618f;
		if (_cornerRoundness != -1f)
		{
			num7 = _cornerRoundness;
		}
		float num8 = (float)Screen.height * 0.005f;
		if (_edgeRoundness != -1f)
		{
			num8 = _edgeRoundness;
		}
		int num9 = 7;
		int num10 = 10;
		float num11 = 1f / (float)(num9 - 1);
		float num12 = 1f / (float)(num10 - 1);
		Vector2[] array = new Vector2[num * (num9 - 1) + 4 * (num10 - 1) + 1];
		Vector2 zero = Vector2.zero;
		Vector2 zero2 = Vector2.zero;
		Vector2 vector = Vector2.zero;
		Vector2 vector2 = Vector2.zero;
		Vector2 vector3 = Vector2.zero;
		Vector2 vector4 = Vector2.zero;
		int num13 = 1;
		int num14 = 1;
		for (int i = 0; i < 8; i++)
		{
			if (num14 == 1)
			{
				zero..ctor(num6 * Random.Range(-_cornerRandom, _cornerRandom), 0f);
				zero..ctor(num6 * Random.Range(-_cornerRandom, _cornerRandom), 0f);
				if (i == 0)
				{
					vector = new Vector2(_width * -0.5f + num6 * (float)num2, _height * 0.5f) + zero * (float)num2;
					vector4 = new Vector2(_width * 0.5f - num6 * (float)num3, _height * 0.5f) + zero * (float)num3;
					vector2 = new Vector2(vector.x + num7, vector.y + num8) + zero * (float)num2;
					vector3 = new Vector2(vector4.x - num7, vector4.y + num8) + zero * (float)num3;
					array[0] = vector;
				}
				else if (i == 2)
				{
					vector = array[num13 - 1];
					vector4 = new Vector2(_width * 0.5f, _height * -0.5f + num6 * (float)num5) + zero2 * (float)num5;
					vector2..ctor(vector.x + num8, vector.y - num7);
					vector3..ctor(vector4.x + num8, vector4.y + num7);
				}
				else if (i == 4)
				{
					vector = array[num13 - 1];
					vector4 = new Vector2(_width * -0.5f + num6 * (float)num4, _height * -0.5f) + zero * (float)num4;
					vector2..ctor(vector.x - num7, vector.y - num8);
					vector3..ctor(vector4.x + num7, vector4.y - num8);
				}
				else if (i == 6)
				{
					vector = array[num13 - 1];
					vector4 = new Vector2(_width * -0.5f, _height * 0.5f - num6 * (float)num2) + zero2 * (float)num2;
					vector2..ctor(vector.x - num8, vector.y + num7);
					vector3..ctor(vector4.x - num8, vector4.y - num7);
				}
				for (int j = 1; j < num10; j++)
				{
					array[num13] = ToolBox.GetBezierPoint((float)j * num12, vector, vector2, vector3, vector4);
					num13++;
				}
			}
			else if (i == 1)
			{
				vector = array[num13 - 1];
				vector4 = new Vector2(_width * 0.5f, _height * 0.5f - num6) + zero2;
				vector2..ctor(vector.x + num7, vector.y - num8);
				vector3..ctor(vector4.x - num8, vector4.y + num7);
				if (_topRightCorner)
				{
					for (int k = 1; k < num9; k++)
					{
						array[num13] = ToolBox.GetBezierPoint((float)k * num11, vector, vector2, vector3, vector4);
						num13++;
					}
				}
			}
			else if (i == 3)
			{
				vector = array[num13 - 1];
				vector4 = new Vector2(_width * 0.5f - num6, _height * -0.5f) + zero;
				vector2..ctor(vector.x - num8, vector.y - num7);
				vector3..ctor(vector4.x + num7, vector4.y + num8);
				if (_bottomRightCorner)
				{
					for (int l = 1; l < num9; l++)
					{
						array[num13] = ToolBox.GetBezierPoint((float)l * num11, vector, vector2, vector3, vector4);
						num13++;
					}
				}
			}
			else if (i == 5)
			{
				vector = array[num13 - 1];
				vector4 = new Vector2(_width * -0.5f, _height * -0.5f + num6) + zero2;
				vector2..ctor(vector.x - num7, vector.y + num8);
				vector3..ctor(vector4.x + num8, vector4.y - num7);
				if (_bottomLeftCorner)
				{
					for (int m = 1; m < num9; m++)
					{
						array[num13] = ToolBox.GetBezierPoint((float)m * num11, vector, vector2, vector3, vector4);
						num13++;
					}
				}
			}
			else if (i == 7)
			{
				vector = array[num13 - 1];
				vector4 = array[0];
				vector2..ctor(vector.x + num8, vector.y + num7);
				vector3..ctor(vector4.x - num7, vector4.y - num8);
				if (_topLeftCorner)
				{
					for (int n = 1; n < num9; n++)
					{
						array[num13] = ToolBox.GetBezierPoint((float)n * num11, vector, vector2, vector3, vector4);
						num13++;
					}
				}
			}
			num14 *= -1;
		}
		return array;
	}

	// Token: 0x0600284E RID: 10318 RVA: 0x001AD25E File Offset: 0x001AB65E
	public static Color GetColor(float _r, float _g, float _b)
	{
		return new Color(_r / 255f, _g / 255f, _b / 255f);
	}

	// Token: 0x0600284F RID: 10319 RVA: 0x001AD27A File Offset: 0x001AB67A
	public static Color GetColor(float _r, float _g, float _b, float _a)
	{
		return new Color(_r / 255f, _g / 255f, _b / 255f, _a / 255f);
	}

	// Token: 0x06002850 RID: 10320 RVA: 0x001AD2A0 File Offset: 0x001AB6A0
	public static uint ColorToUInt(Color _color)
	{
		return (uint)((Mathf.RoundToInt(_color.a * 255f) << 24) | (Mathf.RoundToInt(_color.r * 255f) << 16) | (Mathf.RoundToInt(_color.g * 255f) << 8) | Mathf.RoundToInt(_color.b * 255f));
	}

	// Token: 0x06002851 RID: 10321 RVA: 0x001AD302 File Offset: 0x001AB702
	public static Color UIntToColor(uint _uint)
	{
		return DebugDraw.GetColor((_uint >> 16) & 255U, (_uint >> 8) & 255U, _uint & 255U, (_uint >> 24) & 255U);
	}

	// Token: 0x06002852 RID: 10322 RVA: 0x001AD338 File Offset: 0x001AB738
	public static string ColorToHex(Color32 _color)
	{
		return _color.r.ToString("X2") + _color.g.ToString("X2") + _color.b.ToString("X2");
	}

	// Token: 0x06002853 RID: 10323 RVA: 0x001AD380 File Offset: 0x001AB780
	public static Color HexToColor(string _hex)
	{
		_hex = _hex.Replace("#", string.Empty);
		byte b = byte.Parse(_hex.Substring(0, 2), 515);
		byte b2 = byte.Parse(_hex.Substring(2, 2), 515);
		byte b3 = byte.Parse(_hex.Substring(4, 2), 515);
		return new Color32(b, b2, b3, byte.MaxValue);
	}

	// Token: 0x06002854 RID: 10324 RVA: 0x001AD3EA File Offset: 0x001AB7EA
	public static uint HexToUint(string _hex)
	{
		return DebugDraw.ColorToUInt(DebugDraw.HexToColor(_hex));
	}

	// Token: 0x06002855 RID: 10325 RVA: 0x001AD3F8 File Offset: 0x001AB7F8
	public static Vector2[] VerticalSkew(Vector2[] _points, float _skewCenterX, float _skewAmount, float _skewAmountRelativeTo)
	{
		Vector2[] array = new Vector2[_points.Length];
		_points.CopyTo(array, 0);
		for (int i = 0; i < array.Length; i++)
		{
			float num = (array[i].x - _skewCenterX) / _skewAmountRelativeTo * _skewAmount;
			Vector2[] array2 = array;
			int num2 = i;
			array2[num2].y = array2[num2].y + num;
		}
		return array;
	}

	// Token: 0x04002D85 RID: 11653
	public static float m_lineWidth = 1f;

	// Token: 0x04002D86 RID: 11654
	public static Color defaultColor;

	// Token: 0x04002D87 RID: 11655
	public static SpriteSheet p_spriteSheet;

	// Token: 0x04002D88 RID: 11656
	public static SpriteSheet p_uiSpriteSheet;

	// Token: 0x04002D89 RID: 11657
	public static Frame m_debugFrame;

	// Token: 0x04002D8A RID: 11658
	public static TransformC m_debugTC;
}
