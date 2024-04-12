using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000511 RID: 1297
public static class TouchAreaS
{
	// Token: 0x060025B6 RID: 9654 RVA: 0x001A01AC File Offset: 0x0019E5AC
	public static void Initialize()
	{
		TouchAreaS.m_beginTouchDelegates = new List<Func<TouchAreaC, bool>>();
		TouchAreaS.m_endTouchDelegates = new List<Func<TouchAreaC, bool>>();
		TouchAreaS.m_touches = new DynamicArray<TLTouch>(10, 0.5f, 0.25f, 0.5f);
		TouchAreaS.m_touchRemoveList = new List<TLTouch>();
		TouchAreaS.m_areas = new DynamicArray<TouchAreaC>(100, 0.5f, 0.25f, 0.5f);
		TouchAreaS.m_mouseTouches = new Touch2[10];
		for (int i = 0; i < 10; i++)
		{
			TouchAreaS.m_mouseTouches[i] = new Touch2();
		}
		TouchAreaS.m_touchEvents = new TEvent[50];
		for (int j = 0; j < TouchAreaS.m_touchEvents.Length; j++)
		{
			TouchAreaS.m_touchEvents[j] = new TEvent();
			TouchAreaS.m_touchEvents[j].m_touches = new TLTouch[10];
			TouchAreaS.m_touchEvents[j].m_touchSecondaries = new bool[10];
			TouchAreaS.m_touchEvents[j].m_touchPhases = new TouchAreaPhase[10];
			TouchAreaS.m_touchEvents[j].m_touchCount = 0;
		}
	}

	// Token: 0x060025B7 RID: 9655 RVA: 0x001A02AE File Offset: 0x0019E6AE
	public static void AddBeginTouchDelegate(Func<TouchAreaC, bool> _delegate)
	{
		TouchAreaS.m_beginTouchDelegates.Add(_delegate);
	}

	// Token: 0x060025B8 RID: 9656 RVA: 0x001A02BB File Offset: 0x0019E6BB
	public static void RemoveBeginTouchDelegates()
	{
		TouchAreaS.m_beginTouchDelegates.Clear();
	}

	// Token: 0x060025B9 RID: 9657 RVA: 0x001A02C7 File Offset: 0x0019E6C7
	public static void AddEndTouchDelegate(Func<TouchAreaC, bool> _delegate)
	{
		TouchAreaS.m_endTouchDelegates.Add(_delegate);
	}

	// Token: 0x060025BA RID: 9658 RVA: 0x001A02D4 File Offset: 0x0019E6D4
	public static List<TLTouch> GetTouchesOnTAC(TouchAreaC _c)
	{
		List<TLTouch> list = new List<TLTouch>();
		for (int i = 0; i < TouchAreaS.m_touches.m_array.Length; i++)
		{
			if (TouchAreaS.m_touches.m_array[i].m_primaryArea == _c || TouchAreaS.m_touches.m_array[i].m_secondaryArea == _c)
			{
				list.Add(TouchAreaS.m_touches.m_array[i]);
			}
		}
		return list;
	}

	// Token: 0x060025BB RID: 9659 RVA: 0x001A0348 File Offset: 0x0019E748
	private static void CreateColliderMeshes()
	{
		Debug.LogError("Creating entity with collider mesh");
		Vector2[] circle = DebugDraw.GetCircle(1f, 16, Vector2.zero);
		TouchAreaS.m_circleMesh = GeometryGenerator.GenerateFlatMesh(new GGData(circle));
		Vector2[] rect = DebugDraw.GetRect(1f, 1f, Vector2.zero);
		TouchAreaS.m_rectMesh = GeometryGenerator.GenerateFlatMesh(new GGData(rect));
		TouchAreaS.m_colliderMeshesCreated = true;
	}

	// Token: 0x060025BC RID: 9660 RVA: 0x001A03AC File Offset: 0x0019E7AC
	private static Mesh GetCircleMesh(float _radius)
	{
		Vector2[] circle = DebugDraw.GetCircle(_radius, 16, Vector2.zero);
		return GeometryGenerator.GenerateFlatMesh(new GGData(circle));
	}

	// Token: 0x060025BD RID: 9661 RVA: 0x001A03D4 File Offset: 0x0019E7D4
	private static Mesh GetRectMesh(float _width, float _height, Vector3 _offset)
	{
		Vector2[] rect = DebugDraw.GetRect(_width, _height, _offset);
		return GeometryGenerator.GenerateFlatMesh(new GGData(rect));
	}

	// Token: 0x060025BE RID: 9662 RVA: 0x001A03FC File Offset: 0x0019E7FC
	public static void ResizeRectCollider(TouchAreaC _c, float _width, float _height, Vector2 _offset = default(Vector2))
	{
		if (_c.m_colliderShape != ColliderShape.Rect)
		{
			Debug.LogWarning("Trying to resize non-rect collider");
			return;
		}
		Object.DestroyImmediate(_c.m_collider.sharedMesh);
		_c.m_collider.sharedMesh = TouchAreaS.GetRectMesh(_width, _height, _offset);
	}

	// Token: 0x060025BF RID: 9663 RVA: 0x001A0448 File Offset: 0x0019E848
	public static void ResizeCircleCollider(TouchAreaC _c, float _radius)
	{
		if (_c.m_colliderShape != ColliderShape.Circle)
		{
			Debug.LogWarning("Trying to resize non-circle collider");
			return;
		}
		Object.DestroyImmediate(_c.m_collider.sharedMesh);
		_c.m_collider.sharedMesh = TouchAreaS.GetCircleMesh(_radius);
	}

	// Token: 0x060025C0 RID: 9664 RVA: 0x001A0481 File Offset: 0x0019E881
	public static void AddTouchEventListener(TouchAreaC _c, TouchEventDelegate _touchEventHandler)
	{
		if (_c.m_delegatedCount == 0)
		{
			_c.d_TouchEventDelegate = _touchEventHandler;
		}
		else
		{
			_c.d_TouchEventDelegate = (TouchEventDelegate)Delegate.Combine(_c.d_TouchEventDelegate, _touchEventHandler);
		}
		_c.m_delegatedCount++;
	}

	// Token: 0x060025C1 RID: 9665 RVA: 0x001A04BF File Offset: 0x0019E8BF
	public static void RemoveTouchEventListener(TouchAreaC _c, TouchEventDelegate _touchEventHandler)
	{
		if (_c.m_delegatedCount > 0)
		{
			_c.d_TouchEventDelegate = (TouchEventDelegate)Delegate.Remove(_c.d_TouchEventDelegate, _touchEventHandler);
			_c.m_delegatedCount--;
		}
	}

	// Token: 0x060025C2 RID: 9666 RVA: 0x001A04F4 File Offset: 0x0019E8F4
	public static void RemoveAllTouchEventListeners(TouchAreaC _c)
	{
		if (_c.m_delegatedCount > 0)
		{
			Delegate[] invocationList = _c.d_TouchEventDelegate.GetInvocationList();
			foreach (Delegate @delegate in invocationList)
			{
				_c.d_TouchEventDelegate = (TouchEventDelegate)Delegate.Remove(_c.d_TouchEventDelegate, (TouchEventDelegate)@delegate);
			}
			_c.m_delegatedCount = 0;
		}
	}

	// Token: 0x060025C3 RID: 9667 RVA: 0x001A0558 File Offset: 0x0019E958
	private static TLTouch AddTouch(Vector2 _pos, int _fingerId, float _pressure, TouchPhase _phase, TouchType _type)
	{
		TLTouch tltouch = TouchAreaS.m_touches.AddItem();
		tltouch.m_currentPosition = _pos;
		tltouch.m_startPosition = _pos;
		tltouch.m_fingerId = _fingerId;
		tltouch.m_phase = _phase;
		tltouch.m_cancelled = false;
		tltouch.m_pressure = _pressure;
		tltouch.m_type = _type;
		return tltouch;
	}

	// Token: 0x060025C4 RID: 9668 RVA: 0x001A05A3 File Offset: 0x0019E9A3
	private static void RemoveTouch(TLTouch _t)
	{
		_t.m_primaryArea = null;
		_t.m_secondaryArea = null;
		_t.m_consumed = false;
		TouchAreaS.m_touches.RemoveItem(_t);
	}

	// Token: 0x060025C5 RID: 9669 RVA: 0x001A05C8 File Offset: 0x0019E9C8
	public static TouchAreaC AddRectArea(TransformC _tc, string _name, float _width, float _height, Camera _camera, IComponent _customComponent = null, Vector2 _offset = default(Vector2))
	{
		TouchAreaC touchAreaC = TouchAreaS.m_areas.AddItem();
		touchAreaC.m_TC = _tc;
		touchAreaC.m_TC.transform.gameObject.layer = _camera.gameObject.layer;
		touchAreaC.m_customComponent = _customComponent;
		touchAreaC.m_camera = _camera;
		touchAreaC.m_name = _name;
		GameObject gameObject = touchAreaC.m_TC.transform.gameObject;
		MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
		meshCollider.sharedMesh = TouchAreaS.GetRectMesh(_width, _height, _offset);
		touchAreaC.m_collider = meshCollider;
		touchAreaC.m_colliderShape = ColliderShape.Rect;
		TouchAreaBootstrap touchAreaBootstrap = gameObject.AddComponent<TouchAreaBootstrap>();
		touchAreaBootstrap.m_TAC = touchAreaC;
		EntityManager.AddComponentToEntity(_tc.p_entity, touchAreaC);
		return touchAreaC;
	}

	// Token: 0x060025C6 RID: 9670 RVA: 0x001A0674 File Offset: 0x0019EA74
	public static TouchAreaC AddCircleArea(TransformC _tc, string _name, float _radius, Camera _camera, IComponent _customComponent = null)
	{
		TouchAreaC touchAreaC = TouchAreaS.m_areas.AddItem();
		touchAreaC.m_TC = _tc;
		touchAreaC.m_TC.transform.gameObject.layer = _camera.gameObject.layer;
		touchAreaC.m_customComponent = _customComponent;
		touchAreaC.m_camera = _camera;
		touchAreaC.m_name = _name;
		GameObject gameObject = touchAreaC.m_TC.transform.gameObject;
		MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
		meshCollider.sharedMesh = TouchAreaS.GetCircleMesh(_radius);
		touchAreaC.m_collider = meshCollider;
		touchAreaC.m_colliderShape = ColliderShape.Circle;
		TouchAreaBootstrap touchAreaBootstrap = gameObject.AddComponent<TouchAreaBootstrap>();
		touchAreaBootstrap.m_TAC = touchAreaC;
		EntityManager.AddComponentToEntity(_tc.p_entity, touchAreaC);
		return touchAreaC;
	}

	// Token: 0x060025C7 RID: 9671 RVA: 0x001A0718 File Offset: 0x0019EB18
	public static TouchAreaC AddMeshArea(TransformC _tc, string _name, Mesh _mesh, Camera _camera, IComponent _customComponent = null)
	{
		if (!TouchAreaS.m_colliderMeshesCreated)
		{
			TouchAreaS.CreateColliderMeshes();
		}
		TouchAreaC touchAreaC = TouchAreaS.m_areas.AddItem();
		touchAreaC.m_TC = _tc;
		touchAreaC.m_TC.transform.gameObject.layer = _camera.gameObject.layer;
		touchAreaC.m_customComponent = _customComponent;
		touchAreaC.m_camera = _camera;
		touchAreaC.m_name = _name;
		GameObject gameObject = touchAreaC.m_TC.transform.gameObject;
		MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
		meshCollider.sharedMesh = _mesh;
		touchAreaC.m_collider = meshCollider;
		touchAreaC.m_colliderShape = ColliderShape.Mesh;
		TouchAreaBootstrap touchAreaBootstrap = gameObject.AddComponent<TouchAreaBootstrap>();
		touchAreaBootstrap.m_TAC = touchAreaC;
		EntityManager.AddComponentToEntity(_tc.p_entity, touchAreaC);
		return touchAreaC;
	}

	// Token: 0x060025C8 RID: 9672 RVA: 0x001A07C4 File Offset: 0x0019EBC4
	public static void RemoveArea(TouchAreaC _c)
	{
		GameObject gameObject = _c.m_TC.transform.gameObject;
		TouchAreaBootstrap component = gameObject.GetComponent<TouchAreaBootstrap>();
		MeshCollider component2 = gameObject.GetComponent<MeshCollider>();
		Object.DestroyImmediate(component2.sharedMesh);
		Object.DestroyImmediate(component2);
		Object.DestroyImmediate(component);
		if (_c.m_delegatedCount > 0)
		{
			Delegate[] invocationList = _c.d_TouchEventDelegate.GetInvocationList();
			foreach (Delegate @delegate in invocationList)
			{
				_c.d_TouchEventDelegate = (TouchEventDelegate)Delegate.Remove(_c.d_TouchEventDelegate, (TouchEventDelegate)@delegate);
			}
			_c.m_delegatedCount = 0;
		}
		EntityManager.RemoveComponentFromEntity(_c);
		TouchAreaS.m_areas.RemoveItem(_c);
	}

	// Token: 0x060025C9 RID: 9673 RVA: 0x001A0877 File Offset: 0x0019EC77
	public static void SetCamera(TouchAreaC _c, Camera _camera)
	{
		_c.m_camera = _camera;
		_c.m_TC.transform.gameObject.layer = _camera.gameObject.layer;
	}

	// Token: 0x060025CA RID: 9674 RVA: 0x001A08A0 File Offset: 0x0019ECA0
	public static void SetTouchCameraFilter(Camera _camera)
	{
		TouchAreaS.m_touchCameraFilter = _camera;
	}

	// Token: 0x060025CB RID: 9675 RVA: 0x001A08A8 File Offset: 0x0019ECA8
	public static void RemoveTouchCameraFilter()
	{
		TouchAreaS.m_touchCameraFilter = null;
	}

	// Token: 0x060025CC RID: 9676 RVA: 0x001A08B0 File Offset: 0x0019ECB0
	public static void SetClip(TouchAreaC _c, float _left, float _right, float _bottom, float _top)
	{
		_c.m_clip = true;
		_c.m_clipBB.r = _right;
		_c.m_clipBB.l = _left;
		_c.m_clipBB.t = _top;
		_c.m_clipBB.b = _bottom;
	}

	// Token: 0x060025CD RID: 9677 RVA: 0x001A08EC File Offset: 0x0019ECEC
	public static bool IsTouchInside(Vector2 touchPos, Vector2 position, float radius)
	{
		Vector2 vector = touchPos - position;
		float num = vector.x * vector.x + vector.y * vector.y;
		return num < radius * radius;
	}

	// Token: 0x060025CE RID: 9678 RVA: 0x001A0930 File Offset: 0x0019ED30
	public static bool IsTouchInside(Vector2 touchPos, Vector2 position, Vector2 dimensions, float rotAngle)
	{
		float num = touchPos.x;
		float num2 = touchPos.y;
		if (rotAngle != 0f)
		{
			Vector2 vector = new Vector2(num, num2) - position;
			float num3 = ToolBox.getAngleFromVector2(vector) * 57.29578f;
			float num4 = rotAngle - num3;
			num = position.x + Mathf.Cos(num4 * 0.017453292f) * vector.magnitude;
			num2 = position.y + Mathf.Sin(num4 * 0.017453292f) * vector.magnitude;
		}
		position.x -= dimensions.x * 0.5f;
		position.y -= dimensions.y * 0.5f;
		return num > position.x && num < position.x + dimensions.x && num2 > position.y && num2 < position.y + dimensions.y;
	}

	// Token: 0x060025CF RID: 9679 RVA: 0x001A0A34 File Offset: 0x0019EE34
	public static Vector3 GetTouchWorldPos(Camera _camera, Vector2 _screenPos, float _zOffset = 0f)
	{
		if (_camera.rect.width < 0.01f || _camera.rect.height < 0.01f)
		{
			return Vector3.one * float.MaxValue;
		}
		Vector3 vector = -_camera.ScreenToWorldPoint(new Vector3(_screenPos.x, _screenPos.y, _camera.transform.position.z + _zOffset));
		vector += _camera.transform.position * 2f;
		vector.z = 0f;
		return vector;
	}

	// Token: 0x060025D0 RID: 9680 RVA: 0x001A0ADE File Offset: 0x0019EEDE
	public static void Disable()
	{
		TouchAreaS.m_disabled = true;
		Debug.Log("Touches DISABLED", null);
	}

	// Token: 0x060025D1 RID: 9681 RVA: 0x001A0AF1 File Offset: 0x0019EEF1
	public static void Enable()
	{
		TouchAreaS.m_disabled = false;
		Debug.Log("Touches ENABLED", null);
	}

	// Token: 0x060025D2 RID: 9682 RVA: 0x001A0B04 File Offset: 0x0019EF04
	public static void ConvertSecondaryAreaToPrimary(TLTouch _touch)
	{
		if (_touch.m_primaryArea != null)
		{
			TouchAreaC primaryArea = _touch.m_primaryArea;
			TouchAreaPhase primaryPhase = _touch.m_primaryPhase;
			primaryArea.m_touchCount--;
			primaryArea.m_wasDragged = false;
			primaryArea.m_isDragged = false;
		}
		_touch.m_primaryArea = _touch.m_secondaryArea;
		_touch.m_primaryPhase = _touch.m_secondaryPhase;
		_touch.m_secondaryArea = null;
		_touch.m_secondaryPhase = TouchAreaPhase.ReleaseOut;
	}

	// Token: 0x060025D3 RID: 9683 RVA: 0x001A0B70 File Offset: 0x0019EF70
	public static void SwitchTouchToAnotherTouchArea(TLTouch _touch, TouchAreaC _target)
	{
		TouchAreaPhase touchAreaPhase = TouchAreaPhase.Began;
		if (_touch.m_primaryArea != null)
		{
			TouchAreaC primaryArea = _touch.m_primaryArea;
			primaryArea.m_touchCount--;
			primaryArea.m_wasDragged = false;
			primaryArea.m_isDragged = false;
		}
		if (_touch.m_secondaryArea != null)
		{
			TouchAreaC secondaryArea = _touch.m_secondaryArea;
			secondaryArea.m_touchCount--;
			secondaryArea.m_wasDragged = false;
			secondaryArea.m_isDragged = false;
		}
		_touch.m_consumingCamera = _target.m_camera;
		_touch.m_primaryArea = _target;
		_touch.m_primaryPhase = touchAreaPhase;
		_touch.m_secondaryArea = null;
		_touch.m_secondaryPhase = TouchAreaPhase.ReleaseOut;
	}

	// Token: 0x060025D4 RID: 9684 RVA: 0x001A0C03 File Offset: 0x0019F003
	public static void LockSecondaryTouchArea(TLTouch _touch)
	{
		_touch.m_lockedSecondary = _touch.m_secondaryArea;
	}

	// Token: 0x060025D5 RID: 9685 RVA: 0x001A0C14 File Offset: 0x0019F014
	public static void CancelAllTouches(TLTouch _touch = null)
	{
		for (int i = 0; i < TouchAreaS.m_areas.m_aliveCount; i++)
		{
			TouchAreaC touchAreaC = TouchAreaS.m_areas.m_array[TouchAreaS.m_areas.m_aliveIndices[i]];
			for (int j = 0; j < TouchAreaS.m_touches.m_aliveCount; j++)
			{
				TLTouch tltouch = TouchAreaS.m_touches.m_array[TouchAreaS.m_touches.m_aliveIndices[j]];
				if ((_touch == null || tltouch.m_fingerId != _touch.m_fingerId) && (tltouch.m_primaryArea == touchAreaC || tltouch.m_secondaryArea == touchAreaC))
				{
					TouchAreaS.CancelTouch(tltouch);
				}
			}
		}
	}

	// Token: 0x060025D6 RID: 9686 RVA: 0x001A0CBC File Offset: 0x0019F0BC
	public static void SwitchAllTouchesToTouchArea(TouchAreaC _target)
	{
		for (int i = 0; i < TouchAreaS.m_areas.m_aliveCount; i++)
		{
			TouchAreaC touchAreaC = TouchAreaS.m_areas.m_array[TouchAreaS.m_areas.m_aliveIndices[i]];
			for (int j = 0; j < TouchAreaS.m_touches.m_aliveCount; j++)
			{
				TLTouch tltouch = TouchAreaS.m_touches.m_array[TouchAreaS.m_touches.m_aliveIndices[j]];
				TouchAreaS.SwitchTouchToAnotherTouchArea(tltouch, _target);
			}
		}
	}

	// Token: 0x060025D7 RID: 9687 RVA: 0x001A0D38 File Offset: 0x0019F138
	public static void CancelTouch(TLTouch _touch)
	{
		if (_touch.m_phase == 4 || _touch.m_cancelled)
		{
			return;
		}
		if (_touch.m_primaryArea != null && _touch.m_primaryPhase != TouchAreaPhase.ReleaseOut)
		{
			_touch.m_primaryPhase = TouchAreaPhase.ReleaseOut;
			if (_touch.m_primaryArea.m_touchCount > 0)
			{
				_touch.m_primaryArea.m_touchCount--;
			}
			TouchAreaS.HandleTouch(_touch.m_primaryArea, _touch, false);
		}
		if (_touch.m_secondaryArea != null && _touch.m_primaryArea != _touch.m_secondaryArea && _touch.m_secondaryPhase != TouchAreaPhase.RollOut)
		{
			_touch.m_secondaryPhase = TouchAreaPhase.RollOut;
			if (_touch.m_secondaryArea.m_touchCount > 0)
			{
				_touch.m_secondaryArea.m_touchCount--;
			}
			TouchAreaS.HandleTouch(_touch.m_secondaryArea, _touch, true);
		}
		_touch.m_primaryArea = null;
		_touch.m_secondaryArea = null;
		_touch.m_lockedSecondary = null;
		_touch.m_phase = 4;
		_touch.m_cancelled = true;
	}

	// Token: 0x060025D8 RID: 9688 RVA: 0x001A0E30 File Offset: 0x0019F230
	public static void Update()
	{
		if (TouchAreaS.m_disabled)
		{
			return;
		}
		TouchAreaS.m_tick++;
		int touchCount = Input.touchCount;
		List<Touch2> list = new List<Touch2>();
		for (int i = 0; i < touchCount; i++)
		{
			Touch2 touch = new Touch2();
			Touch touch2 = Input.GetTouch(i);
			touch.deltaPosition = touch2.deltaPosition;
			touch.fingerId = touch2.fingerId;
			touch.phase = touch2.phase;
			touch.type = touch2.type;
			if (touch.phase == 4)
			{
				touch.phase = 3;
			}
			touch.position = touch2.position;
			touch.tapCount = touch2.tapCount;
			touch.deltaTime = touch2.deltaTime;
			touch.pressure = touch2.pressure / touch2.maximumPossiblePressure;
			list.Add(touch);
		}
		while (TouchAreaS.m_touchRemoveList.Count > 0)
		{
			int num = TouchAreaS.m_touchRemoveList.Count - 1;
			TouchAreaS.RemoveTouch(TouchAreaS.m_touchRemoveList[num]);
			TouchAreaS.m_touchRemoveList.RemoveAt(num);
		}
		TouchAreaS.m_touches.Update();
		for (int j = 0; j < touchCount; j++)
		{
			TLTouch tltouch = null;
			Touch2 touch3 = list[j];
			bool flag = true;
			int aliveCount = TouchAreaS.m_touches.m_aliveCount;
			for (int k = 0; k < aliveCount; k++)
			{
				tltouch = TouchAreaS.m_touches.m_array[TouchAreaS.m_touches.m_aliveIndices[k]];
				if (tltouch.m_fingerId == touch3.fingerId)
				{
					flag = false;
					tltouch.m_deltaPosition = touch3.position - tltouch.m_currentPosition;
					if (touch3.phase == 1 || touch3.phase == 2)
					{
						tltouch.m_deltaPosition /= (float)Main.m_ticksPerFrame;
					}
					tltouch.m_currentPosition = touch3.position;
					tltouch.m_phase = touch3.phase;
					tltouch.m_tapCount = touch3.tapCount;
					tltouch.m_pressure = touch3.pressure;
					tltouch.m_type = touch3.type;
					break;
				}
			}
			if (flag)
			{
				tltouch = TouchAreaS.AddTouch(touch3.position, touch3.fingerId, touch3.pressure, 0, touch3.type);
			}
			int count = CameraS.m_cameras.Count;
			for (int l = count - 1; l > -1; l--)
			{
				Camera camera = CameraS.m_cameras[l];
				if (!(TouchAreaS.m_touchCameraFilter != null) || !(camera != TouchAreaS.m_touchCameraFilter))
				{
					if (!tltouch.m_consumed || !(tltouch.m_consumingCamera != camera))
					{
						if (tltouch.m_primaryArea == null || !(tltouch.m_primaryArea.m_camera != camera))
						{
							if (tltouch.m_secondaryArea == null || !(tltouch.m_secondaryArea.m_camera != camera))
							{
								Vector3 touchWorldPos = TouchAreaS.GetTouchWorldPos(camera, touch3.position, 0f);
								Vector3 position = camera.transform.position;
								Vector3 vector = touchWorldPos - position;
								Vector3 vector2 = position;
								RaycastHit[] array;
								if (camera.orthographic)
								{
									vector2 = -touchWorldPos + position * 2f;
									vector2.z = position.z;
									array = Physics.SphereCastAll(vector2, TouchAreaS.m_fingerRadius, Vector3.forward, Math.Abs(position.z) + 500f, 1 << camera.gameObject.layer);
								}
								else
								{
									array = Physics.SphereCastAll(vector2, TouchAreaS.m_fingerRadius, vector, Math.Abs(position.z) + 500f, 1 << camera.gameObject.layer);
									if (array.Length > 0)
									{
										Vector3 vector3 = Vector3.zero;
										for (int m = 0; m < array.Length; m++)
										{
											vector3 += array[m].point;
										}
										vector3 /= (float)array.Length;
										vector2 = vector3;
									}
								}
								List<RaycastHit> list2 = new List<RaycastHit>(array);
								list2.Sort((RaycastHit a, RaycastHit b) => a.transform.position.z.CompareTo(b.transform.position.z));
								TouchAreaC touchAreaC = null;
								TouchAreaC touchAreaC2 = null;
								bool flag2 = false;
								bool flag3 = false;
								for (int n = 0; n < list2.Count; n++)
								{
									RaycastHit raycastHit = list2[n];
									TouchAreaBootstrap touchAreaBootstrap = raycastHit.transform.GetComponent("TouchAreaBootstrap") as TouchAreaBootstrap;
									if (touchAreaBootstrap != null)
									{
										TouchAreaC tac = touchAreaBootstrap.m_TAC;
										if (camera.orthographic && tac.m_clip)
										{
											cpBB cpBB = new cpBB(touch3.position.x - TouchAreaS.m_fingerRadius, touch3.position.y - TouchAreaS.m_fingerRadius, touch3.position.x + TouchAreaS.m_fingerRadius, touch3.position.y + TouchAreaS.m_fingerRadius);
											if (!ChipmunkProWrapper.ucpBBIntersects(tac.m_clipBB, cpBB))
											{
												goto IL_71F;
											}
										}
										if (tac.m_active)
										{
											if (tac.m_allowPrimary)
											{
												if (touchAreaC2 == null)
												{
													touchAreaC2 = tac;
													goto IL_71F;
												}
												float num2 = Mathf.Abs(raycastHit.transform.position.z - touchAreaC2.m_TC.transform.position.z);
												if (num2 <= 1f || tac.m_ignoreDepth)
												{
													float num3 = Vector3.Distance(touchAreaC2.m_TC.transform.position, vector2);
													float num4 = Vector3.Distance(tac.m_TC.transform.position, vector2);
													if (num4 < num3)
													{
														if (touchAreaC2.m_allowSecondary && (touchAreaC == null || (touchAreaC != null && touchAreaC2.m_TC.transform.position.z < touchAreaC.m_TC.transform.position.z)) && touchAreaC2.m_letTouchesThrough)
														{
															touchAreaC = touchAreaC2;
														}
														touchAreaC2 = tac;
														goto IL_71F;
													}
												}
											}
											if (tac.m_allowSecondary)
											{
												if (touchAreaC == null)
												{
													if (touchAreaC2 == null || touchAreaC2.m_letTouchesThrough)
													{
														touchAreaC = tac;
													}
												}
												else
												{
													float num5 = Mathf.Abs(raycastHit.transform.position.z - touchAreaC.m_TC.transform.position.z);
													if (num5 <= 1f || tac.m_ignoreDepth)
													{
														float num6 = Vector3.Distance(touchAreaC.m_TC.transform.position, vector2);
														float num7 = Vector3.Distance(tac.m_TC.transform.position, vector2);
														if (num7 < num6)
														{
															if (touchAreaC2 == null || touchAreaC2.m_letTouchesThrough)
															{
																touchAreaC = tac;
															}
														}
													}
												}
											}
										}
									}
									IL_71F:;
								}
								if ((touchAreaC2 != null && !touchAreaC2.m_letTouchesThrough) || (touchAreaC != null && !touchAreaC.m_letTouchesThrough))
								{
									tltouch.m_consumed = true;
									tltouch.m_consumingCamera = camera;
								}
								if (tltouch.m_primaryArea != null && touchAreaC2 == tltouch.m_primaryArea)
								{
									flag2 = true;
								}
								if (tltouch.m_secondaryArea != null && touchAreaC2 == tltouch.m_secondaryArea)
								{
									flag3 = true;
								}
								else if (tltouch.m_secondaryArea != null && touchAreaC == tltouch.m_secondaryArea)
								{
									flag3 = true;
								}
								if (flag3 && touchAreaC2 != null && touchAreaC2.m_allowSecondary && touchAreaC2 != tltouch.m_secondaryArea && touchAreaC2 != tltouch.m_primaryArea)
								{
									flag3 = false;
								}
								if (tltouch.m_primaryArea == null && touchAreaC2 != null && tltouch.m_phase == null && touchAreaC2.m_touchCount < touchAreaC2.m_maxTouches)
								{
									if (touchAreaC2.m_cancelOtherTouches)
									{
										TouchAreaS.CancelAllTouches(tltouch);
									}
									if (touchAreaC2.m_touchCount == 0)
									{
										touchAreaC2.m_wasDragged = false;
									}
									tltouch.m_primaryArea = touchAreaC2;
									tltouch.m_primaryPhase = TouchAreaPhase.Began;
									tltouch.m_primaryArea.m_touchCount++;
									TouchAreaS.HandleTouch(tltouch.m_primaryArea, tltouch, false);
									flag2 = true;
								}
								if (!flag2 && tltouch.m_primaryArea != null)
								{
									if (tltouch.m_phase == 3 || tltouch.m_phase == 4)
									{
										if (tltouch.m_primaryArea.m_wasDragged)
										{
											tltouch.m_primaryPhase = TouchAreaPhase.DragEnd;
											TouchAreaS.HandleTouch(tltouch.m_primaryArea, tltouch, false);
										}
										tltouch.m_primaryPhase = TouchAreaPhase.ReleaseOut;
										tltouch.m_primaryArea.m_touchCount--;
										TouchAreaS.HandleTouch(tltouch.m_primaryArea, tltouch, false);
									}
									else
									{
										if (tltouch.m_primaryPhase == TouchAreaPhase.Began || tltouch.m_primaryPhase == TouchAreaPhase.RollIn || tltouch.m_primaryPhase == TouchAreaPhase.MoveIn || tltouch.m_primaryPhase == TouchAreaPhase.StationaryIn)
										{
											tltouch.m_primaryPhase = TouchAreaPhase.RollOut;
											TouchAreaS.HandleTouch(tltouch.m_primaryArea, tltouch, false);
										}
										if (tltouch.m_phase == 1)
										{
											if (!tltouch.m_dragged && Vector2.Distance(tltouch.m_startPosition, tltouch.m_currentPosition) > tltouch.m_primaryArea.m_dragThreshold)
											{
												tltouch.m_dragged = true;
												tltouch.m_primaryArea.m_wasDragged = true;
												tltouch.m_primaryPhase = TouchAreaPhase.DragStart;
												TouchAreaS.HandleTouch(tltouch.m_primaryArea, tltouch, false);
												if (tltouch.m_secondaryArea != null)
												{
													tltouch.m_secondaryArea.m_wasDragged = true;
													TouchAreaPhase secondaryPhase = tltouch.m_secondaryPhase;
													tltouch.m_secondaryPhase = TouchAreaPhase.DragStart;
													TouchAreaS.HandleTouch(tltouch.m_secondaryArea, tltouch, true);
													tltouch.m_secondaryPhase = secondaryPhase;
												}
											}
											if (tltouch.m_primaryArea.m_wasDragged)
											{
												tltouch.m_primaryArea.m_isDragged = true;
											}
											tltouch.m_primaryPhase = TouchAreaPhase.MoveOut;
											TouchAreaS.HandleTouch(tltouch.m_primaryArea, tltouch, false);
										}
										else if (tltouch.m_phase == 2)
										{
											tltouch.m_primaryPhase = TouchAreaPhase.StationaryOut;
											TouchAreaS.HandleTouch(tltouch.m_primaryArea, tltouch, false);
											if (tltouch.m_primaryArea.m_wasDragged)
											{
												tltouch.m_primaryArea.m_isDragged = false;
											}
										}
									}
								}
								else if (tltouch.m_primaryArea != null)
								{
									if (tltouch.m_phase == 3 || tltouch.m_phase == 4)
									{
										if (tltouch.m_primaryArea.m_wasDragged)
										{
											tltouch.m_primaryPhase = TouchAreaPhase.DragEnd;
											TouchAreaS.HandleTouch(tltouch.m_primaryArea, tltouch, false);
										}
										tltouch.m_primaryPhase = TouchAreaPhase.ReleaseIn;
										tltouch.m_primaryArea.m_touchCount--;
										TouchAreaS.HandleTouch(tltouch.m_primaryArea, tltouch, false);
									}
									else
									{
										if (tltouch.m_primaryPhase == TouchAreaPhase.RollOut || tltouch.m_primaryPhase == TouchAreaPhase.MoveOut || tltouch.m_primaryPhase == TouchAreaPhase.StationaryOut)
										{
											tltouch.m_primaryPhase = TouchAreaPhase.RollIn;
											TouchAreaS.HandleTouch(tltouch.m_primaryArea, tltouch, false);
										}
										if (tltouch.m_phase == 1)
										{
											if (!tltouch.m_dragged && Vector2.Distance(tltouch.m_startPosition, tltouch.m_currentPosition) > tltouch.m_primaryArea.m_dragThreshold)
											{
												tltouch.m_dragged = true;
												tltouch.m_primaryArea.m_wasDragged = true;
												tltouch.m_primaryPhase = TouchAreaPhase.DragStart;
												TouchAreaS.HandleTouch(tltouch.m_primaryArea, tltouch, false);
												if (tltouch.m_secondaryArea != null)
												{
													tltouch.m_secondaryArea.m_wasDragged = true;
													TouchAreaPhase secondaryPhase2 = tltouch.m_secondaryPhase;
													tltouch.m_secondaryPhase = TouchAreaPhase.DragStart;
													TouchAreaS.HandleTouch(tltouch.m_secondaryArea, tltouch, true);
													tltouch.m_secondaryPhase = secondaryPhase2;
												}
											}
											if (tltouch.m_primaryArea.m_wasDragged)
											{
												tltouch.m_primaryArea.m_isDragged = true;
											}
											tltouch.m_primaryPhase = TouchAreaPhase.MoveIn;
											TouchAreaS.HandleTouch(tltouch.m_primaryArea, tltouch, false);
										}
										else if (tltouch.m_phase == 2)
										{
											tltouch.m_primaryPhase = TouchAreaPhase.StationaryIn;
											TouchAreaS.HandleTouch(tltouch.m_primaryArea, tltouch, false);
											if (tltouch.m_primaryArea.m_wasDragged)
											{
												tltouch.m_primaryArea.m_isDragged = false;
											}
										}
									}
								}
								if (!flag3)
								{
									if (tltouch.m_secondaryArea != null)
									{
										if (tltouch.m_secondaryPhase != TouchAreaPhase.RollOut)
										{
											tltouch.m_secondaryPhase = TouchAreaPhase.RollOut;
											TouchAreaS.HandleTouch(tltouch.m_secondaryArea, tltouch, true);
											if (tltouch.m_secondaryArea != tltouch.m_lockedSecondary)
											{
												tltouch.m_secondaryArea.m_touchCount--;
												tltouch.m_secondaryArea = null;
											}
										}
										else if (tltouch.m_phase == 1)
										{
											TouchAreaPhase touchAreaPhase;
											if (!tltouch.m_dragged && Vector2.Distance(tltouch.m_startPosition, tltouch.m_currentPosition) > tltouch.m_secondaryArea.m_dragThreshold)
											{
												tltouch.m_dragged = true;
												tltouch.m_secondaryArea.m_wasDragged = true;
												touchAreaPhase = tltouch.m_secondaryPhase;
												tltouch.m_secondaryPhase = TouchAreaPhase.DragStart;
												TouchAreaS.HandleTouch(tltouch.m_secondaryArea, tltouch, true);
												tltouch.m_secondaryPhase = touchAreaPhase;
											}
											if (tltouch.m_secondaryArea.m_wasDragged)
											{
												tltouch.m_secondaryArea.m_isDragged = true;
											}
											touchAreaPhase = tltouch.m_secondaryPhase;
											tltouch.m_secondaryPhase = TouchAreaPhase.MoveOut;
											TouchAreaS.HandleTouch(tltouch.m_secondaryArea, tltouch, true);
											tltouch.m_secondaryPhase = touchAreaPhase;
										}
										else if (tltouch.m_phase == 2)
										{
											TouchAreaPhase secondaryPhase3 = tltouch.m_secondaryPhase;
											tltouch.m_secondaryPhase = TouchAreaPhase.StationaryOut;
											TouchAreaS.HandleTouch(tltouch.m_secondaryArea, tltouch, true);
											tltouch.m_secondaryPhase = secondaryPhase3;
											if (tltouch.m_secondaryArea.m_wasDragged)
											{
												tltouch.m_secondaryArea.m_isDragged = false;
											}
										}
									}
									if (tltouch.m_lockedSecondary == null)
									{
										if (touchAreaC2 != null && touchAreaC2.m_allowSecondary && touchAreaC2 != tltouch.m_primaryArea)
										{
											if (touchAreaC2.m_touchCount < touchAreaC2.m_maxTouches)
											{
												if (touchAreaC2.m_cancelOtherTouches)
												{
													TouchAreaS.CancelAllTouches(tltouch);
												}
												if (touchAreaC2.m_touchCount == 0)
												{
													touchAreaC2.m_wasDragged = false;
												}
												tltouch.m_secondaryArea = touchAreaC2;
												tltouch.m_secondaryArea.m_touchCount++;
												tltouch.m_secondaryPhase = TouchAreaPhase.RollIn;
												TouchAreaS.HandleTouch(tltouch.m_secondaryArea, tltouch, true);
											}
										}
										else if (touchAreaC != null && touchAreaC.m_allowSecondary && touchAreaC != tltouch.m_primaryArea && touchAreaC.m_touchCount < touchAreaC.m_maxTouches)
										{
											if (touchAreaC.m_cancelOtherTouches)
											{
												TouchAreaS.CancelAllTouches(tltouch);
											}
											if (touchAreaC.m_touchCount == 0)
											{
												touchAreaC.m_wasDragged = false;
											}
											tltouch.m_secondaryArea = touchAreaC;
											tltouch.m_secondaryArea.m_touchCount++;
											tltouch.m_secondaryPhase = TouchAreaPhase.RollIn;
											TouchAreaS.HandleTouch(tltouch.m_secondaryArea, tltouch, true);
										}
									}
									if ((tltouch.m_phase == 3 || tltouch.m_phase == 4) && tltouch.m_secondaryArea != null)
									{
										if (tltouch.m_dragged)
										{
											TouchAreaPhase secondaryPhase4 = tltouch.m_secondaryPhase;
											tltouch.m_secondaryPhase = TouchAreaPhase.DragEnd;
											TouchAreaS.HandleTouch(tltouch.m_secondaryArea, tltouch, true);
											tltouch.m_secondaryPhase = secondaryPhase4;
										}
										tltouch.m_secondaryArea.m_touchCount--;
										if (tltouch.m_lockedSecondary == tltouch.m_secondaryArea)
										{
											tltouch.m_secondaryPhase = TouchAreaPhase.ReleaseOut;
										}
										else
										{
											tltouch.m_secondaryPhase = TouchAreaPhase.RollOut;
										}
										TouchAreaS.HandleTouch(tltouch.m_secondaryArea, tltouch, true);
										tltouch.m_lockedSecondary = null;
										tltouch.m_secondaryArea = null;
									}
								}
								else if (tltouch.m_secondaryArea != null)
								{
									if (tltouch.m_phase == 3 || tltouch.m_phase == 4)
									{
										if (tltouch.m_dragged)
										{
											TouchAreaPhase secondaryPhase5 = tltouch.m_secondaryPhase;
											tltouch.m_secondaryPhase = TouchAreaPhase.DragEnd;
											TouchAreaS.HandleTouch(tltouch.m_secondaryArea, tltouch, true);
											tltouch.m_secondaryPhase = secondaryPhase5;
										}
										tltouch.m_secondaryArea.m_touchCount--;
										tltouch.m_secondaryPhase = TouchAreaPhase.RollOut;
										TouchAreaS.HandleTouch(tltouch.m_secondaryArea, tltouch, true);
										tltouch.m_lockedSecondary = null;
										tltouch.m_secondaryArea = null;
									}
									else
									{
										if (tltouch.m_secondaryPhase == TouchAreaPhase.RollOut && (tltouch.m_secondaryArea.m_touchCount < tltouch.m_secondaryArea.m_maxTouches || (tltouch.m_secondaryArea.m_touchCount == tltouch.m_secondaryArea.m_touchCount && tltouch.m_lockedSecondary == tltouch.m_secondaryArea)))
										{
											if (tltouch.m_secondaryArea != tltouch.m_lockedSecondary)
											{
												tltouch.m_secondaryArea.m_touchCount++;
											}
											tltouch.m_secondaryPhase = TouchAreaPhase.RollIn;
											TouchAreaS.HandleTouch(tltouch.m_secondaryArea, tltouch, true);
										}
										if (tltouch.m_phase == 1)
										{
											if (!tltouch.m_dragged && Vector2.Distance(tltouch.m_startPosition, tltouch.m_currentPosition) > tltouch.m_secondaryArea.m_dragThreshold)
											{
												tltouch.m_dragged = true;
												tltouch.m_secondaryArea.m_wasDragged = true;
												TouchAreaPhase secondaryPhase6 = tltouch.m_secondaryPhase;
												tltouch.m_secondaryPhase = TouchAreaPhase.DragStart;
												TouchAreaS.HandleTouch(tltouch.m_secondaryArea, tltouch, true);
												tltouch.m_secondaryPhase = secondaryPhase6;
											}
											if (tltouch.m_secondaryArea.m_wasDragged)
											{
												tltouch.m_secondaryArea.m_isDragged = true;
											}
											tltouch.m_secondaryPhase = TouchAreaPhase.MoveIn;
											TouchAreaS.HandleTouch(tltouch.m_secondaryArea, tltouch, true);
										}
										else if (tltouch.m_phase == 2)
										{
											tltouch.m_secondaryPhase = TouchAreaPhase.StationaryIn;
											TouchAreaS.HandleTouch(tltouch.m_secondaryArea, tltouch, true);
											if (tltouch.m_secondaryArea.m_wasDragged)
											{
												tltouch.m_secondaryArea.m_isDragged = false;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			if (tltouch.m_primaryPhase == TouchAreaPhase.Began)
			{
				for (int num8 = TouchAreaS.m_beginTouchDelegates.Count - 1; num8 >= 0; num8--)
				{
					bool flag4 = TouchAreaS.m_beginTouchDelegates[num8].Invoke(tltouch.m_primaryArea);
					if (flag4)
					{
						TouchAreaS.m_beginTouchDelegates.RemoveAt(num8);
					}
				}
			}
			else if (tltouch.m_primaryPhase == TouchAreaPhase.ReleaseIn || tltouch.m_primaryPhase == TouchAreaPhase.ReleaseOut)
			{
				for (int num9 = TouchAreaS.m_endTouchDelegates.Count - 1; num9 >= 0; num9--)
				{
					bool flag5 = TouchAreaS.m_endTouchDelegates[num9].Invoke(tltouch.m_primaryArea);
					if (flag5)
					{
						TouchAreaS.m_endTouchDelegates.RemoveAt(num9);
					}
				}
			}
			if (tltouch.m_phase == 3)
			{
				TouchAreaS.m_touchRemoveList.Add(tltouch);
			}
		}
		for (int num10 = 0; num10 < TouchAreaS.m_touchEventCount; num10++)
		{
			TEvent tevent = TouchAreaS.m_touchEvents[num10];
			if (tevent.m_touchArea.m_delegatedCount > 0 && !TouchAreaS.m_disabled)
			{
				TouchAreaPhase touchAreaPhase2 = tevent.m_touchPhases[0];
				int num11 = 1;
				if (Main.m_ticksPerFrame > 1 && (touchAreaPhase2 == TouchAreaPhase.MoveIn || touchAreaPhase2 == TouchAreaPhase.MoveOut || touchAreaPhase2 == TouchAreaPhase.StationaryIn || touchAreaPhase2 == TouchAreaPhase.StationaryOut))
				{
					num11 = Main.m_ticksPerFrame;
				}
				for (int num12 = 0; num12 < num11; num12++)
				{
					tevent.m_touchArea.d_TouchEventDelegate(tevent.m_touchArea, touchAreaPhase2, tevent.m_touchSecondaries[0], tevent.m_touchCount, tevent.m_touches);
				}
			}
			tevent.m_touchCount = 0;
		}
		TouchAreaS.m_touchEventCount = 0;
	}

	// Token: 0x060025D9 RID: 9689 RVA: 0x001A2220 File Offset: 0x001A0620
	public static void HandleTouch(TouchAreaC _touchArea, TLTouch _touch, bool _secondary)
	{
		if (TouchAreaS.m_disabled || _touchArea.m_delegatedCount == 0)
		{
			return;
		}
		TEvent tevent = null;
		for (int i = 0; i < TouchAreaS.m_touchEventCount; i++)
		{
			bool flag = false;
			for (int j = 0; j < TouchAreaS.m_touchEvents[i].m_touchCount; j++)
			{
				if (TouchAreaS.m_touchEvents[i].m_touches[j] == _touch)
				{
					flag = true;
					break;
				}
			}
			if (!flag && _touchArea == TouchAreaS.m_touchEvents[i].m_touchArea)
			{
				tevent = TouchAreaS.m_touchEvents[i];
				tevent.m_touches[tevent.m_touchCount] = _touch;
				tevent.m_touchPhases[tevent.m_touchCount] = ((!_secondary) ? _touch.m_primaryPhase : _touch.m_secondaryPhase);
				tevent.m_touchSecondaries[tevent.m_touchCount] = _secondary;
				tevent.m_touchCount++;
			}
		}
		if (tevent == null)
		{
			TEvent tevent2 = TouchAreaS.m_touchEvents[TouchAreaS.m_touchEventCount];
			tevent2.m_touchArea = _touchArea;
			tevent2.m_touches[tevent2.m_touchCount] = _touch;
			tevent2.m_touchPhases[tevent2.m_touchCount] = ((!_secondary) ? _touch.m_primaryPhase : _touch.m_secondaryPhase);
			tevent2.m_touchSecondaries[tevent2.m_touchCount] = _secondary;
			tevent2.m_touchCount++;
			TouchAreaS.m_touchEventCount++;
		}
	}

	// Token: 0x04002B3C RID: 11068
	public static DynamicArray<TLTouch> m_touches;

	// Token: 0x04002B3D RID: 11069
	private static List<TLTouch> m_touchRemoveList;

	// Token: 0x04002B3E RID: 11070
	public static DynamicArray<TouchAreaC> m_areas;

	// Token: 0x04002B3F RID: 11071
	public static bool m_abort;

	// Token: 0x04002B40 RID: 11072
	private static Vector2 m_prevMousePosition;

	// Token: 0x04002B41 RID: 11073
	private static Touch2[] m_mouseTouches;

	// Token: 0x04002B42 RID: 11074
	private static TransformC m_colliderHelperTC;

	// Token: 0x04002B43 RID: 11075
	private static bool m_colliderMeshesCreated;

	// Token: 0x04002B44 RID: 11076
	private static Mesh m_rectMesh;

	// Token: 0x04002B45 RID: 11077
	private static Mesh m_circleMesh;

	// Token: 0x04002B46 RID: 11078
	private static Camera m_touchCameraFilter;

	// Token: 0x04002B47 RID: 11079
	private static TEvent[] m_touchEvents;

	// Token: 0x04002B48 RID: 11080
	private static int m_touchEventCount;

	// Token: 0x04002B49 RID: 11081
	public static bool m_disabled = false;

	// Token: 0x04002B4A RID: 11082
	public static float m_fingerRadius = 12f;

	// Token: 0x04002B4B RID: 11083
	private static List<Func<TouchAreaC, bool>> m_beginTouchDelegates;

	// Token: 0x04002B4C RID: 11084
	private static List<Func<TouchAreaC, bool>> m_endTouchDelegates;

	// Token: 0x04002B4D RID: 11085
	private static int[] m_mouseButtonIds = new int[] { -1, -1 };

	// Token: 0x04002B4E RID: 11086
	private static bool[] m_mouseButtonDowns = new bool[2];

	// Token: 0x04002B4F RID: 11087
	private static int m_tick;
}
