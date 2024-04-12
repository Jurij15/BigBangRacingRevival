using System;
using UnityEngine;

// Token: 0x020005BE RID: 1470
public class TouchAreaTestState : BasicState
{
	// Token: 0x06002AEA RID: 10986 RVA: 0x001BC28C File Offset: 0x001BA68C
	public override void Enter(IStatedObject _parent)
	{
		Entity entity = EntityManager.AddEntity();
		TransformC transformC = TransformS.AddComponent(entity);
		TransformS.SetPosition(transformC, new Vector3(0f, 100f, -20f));
		TouchAreaC touchAreaC = TouchAreaS.AddCircleArea(transformC, "circle", 100f, CameraS.m_uiCamera, null);
		TouchAreaS.AddTouchEventListener(touchAreaC, new TouchEventDelegate(this.Handler));
		DebugDraw.CreateCircle(CameraS.m_uiCamera, transformC, Vector2.zero, 100f, false, 16);
		touchAreaC.m_maxTouches = 2;
		entity = EntityManager.AddEntity();
		transformC = TransformS.AddComponent(entity);
		TransformS.SetPosition(transformC, new Vector3(-50f, 0f, 0f));
		touchAreaC = TouchAreaS.AddRectArea(transformC, "rect LEFT", 100f, 100f, CameraS.m_uiCamera, null, default(Vector2));
		TouchAreaS.AddTouchEventListener(touchAreaC, new TouchEventDelegate(this.Handler));
		DebugDraw.CreateBox(CameraS.m_uiCamera, transformC, Vector2.zero, 100f, 100f, false);
		touchAreaC.m_allowSecondary = true;
		entity = EntityManager.AddEntity();
		transformC = TransformS.AddComponent(entity);
		TransformS.SetPosition(transformC, new Vector3(50f, 0f, 0f));
		touchAreaC = TouchAreaS.AddRectArea(transformC, "rect RIGHT", 100f, 100f, CameraS.m_uiCamera, null, default(Vector2));
		TouchAreaS.AddTouchEventListener(touchAreaC, new TouchEventDelegate(this.Handler));
		DebugDraw.CreateBox(CameraS.m_uiCamera, transformC, Vector2.zero, 100f, 100f, false);
		touchAreaC.m_allowPrimary = false;
		touchAreaC.m_allowSecondary = true;
		entity = EntityManager.AddEntity();
		transformC = TransformS.AddComponent(entity);
		TransformS.SetPosition(transformC, new Vector3(25f, 50f, -40f));
		touchAreaC = TouchAreaS.AddRectArea(transformC, "rect SMALL", 50f, 50f, CameraS.m_uiCamera, null, default(Vector2));
		TouchAreaS.AddTouchEventListener(touchAreaC, new TouchEventDelegate(this.Handler));
		DebugDraw.CreateBox(CameraS.m_uiCamera, transformC, Vector2.zero, 50f, 50f, false);
		touchAreaC.m_allowPrimary = true;
		touchAreaC.m_allowSecondary = true;
		TweenC tweenC = TweenS.AddTransformTween(transformC, TweenedProperty.Position, TweenStyle.CubicInOut, new Vector3(25f, -200f, -10f), 3f, 0f, true);
		TweenS.SetAdditionalTweenProperties(tweenC, -1, true, TweenStyle.CubicInOut);
	}

	// Token: 0x06002AEB RID: 10987 RVA: 0x001BC4E0 File Offset: 0x001BA8E0
	public void Handler(TouchAreaC _touchArea, TouchAreaPhase _touchPhase, bool _touchIsSecondary, int _touchCount, TLTouch[] _touches)
	{
		Debug.Log(_touchArea.m_touchCount, null);
		if (!_touchIsSecondary)
		{
			if (_touchArea.m_touchCount == 1)
			{
				if (_touchPhase == TouchAreaPhase.Began || _touchPhase == TouchAreaPhase.RollIn)
				{
					SpriteS.SetColorByTransformComponent(_touchArea.m_TC, Color.green, false, false);
				}
				else if (_touchPhase == TouchAreaPhase.RollOut)
				{
					SpriteS.SetColorByTransformComponent(_touchArea.m_TC, Color.yellow, false, false);
				}
			}
			else if (_touchArea.m_touchCount == 0 && (_touchPhase == TouchAreaPhase.ReleaseIn || _touchPhase == TouchAreaPhase.ReleaseOut))
			{
				SpriteS.SetColorByTransformComponent(_touchArea.m_TC, Color.grey, false, false);
			}
			if (_touchPhase == TouchAreaPhase.Began)
			{
				Debug.Log(string.Concat(new object[] { "primary: ", _touchArea.m_name, ": ", _touchPhase }), null);
			}
			else if (_touchPhase == TouchAreaPhase.RollIn)
			{
				Debug.Log(string.Concat(new object[] { "primary: ", _touchArea.m_name, ": ", _touchPhase }), null);
			}
			else if (_touchPhase == TouchAreaPhase.RollOut)
			{
				Debug.Log(string.Concat(new object[] { "primary: ", _touchArea.m_name, ": ", _touchPhase }), null);
			}
			else if (_touchPhase == TouchAreaPhase.ReleaseIn)
			{
				Debug.Log(string.Concat(new object[] { "primary: ", _touchArea.m_name, ": ", _touchPhase }), null);
			}
			else if (_touchPhase == TouchAreaPhase.ReleaseOut)
			{
				Debug.Log(string.Concat(new object[] { "primary: ", _touchArea.m_name, ": ", _touchPhase }), null);
			}
			else if (_touchPhase == TouchAreaPhase.DragStart)
			{
				Debug.Log(string.Concat(new object[] { "primary: ", _touchArea.m_name, ": ", _touchPhase }), null);
			}
			else if (_touchPhase == TouchAreaPhase.DragEnd)
			{
				Debug.Log(string.Concat(new object[] { "primary: ", _touchArea.m_name, ": ", _touchPhase }), null);
			}
		}
		else
		{
			if (_touchArea.m_touchCount == 1)
			{
				if (_touchPhase == TouchAreaPhase.RollIn)
				{
					SpriteS.SetColorByTransformComponent(_touchArea.m_TC, Color.cyan, false, false);
				}
			}
			else if (_touchArea.m_touchCount == 0 && (_touchPhase == TouchAreaPhase.RollOut || _touchPhase == TouchAreaPhase.ReleaseOut))
			{
				SpriteS.SetColorByTransformComponent(_touchArea.m_TC, Color.grey, false, false);
			}
			if (_touchPhase == TouchAreaPhase.RollIn)
			{
				Debug.Log(string.Concat(new object[] { "secondary: ", _touchArea.m_name, ": ", _touchPhase }), null);
			}
			else if (_touchPhase == TouchAreaPhase.RollOut)
			{
				Debug.Log(string.Concat(new object[] { "secondary: ", _touchArea.m_name, ": ", _touchPhase }), null);
			}
			else if (_touchPhase == TouchAreaPhase.DragStart)
			{
				Debug.Log(string.Concat(new object[] { "secondary: ", _touchArea.m_name, ": ", _touchPhase }), null);
			}
			else if (_touchPhase == TouchAreaPhase.DragEnd)
			{
				Debug.Log(string.Concat(new object[] { "secondary: ", _touchArea.m_name, ": ", _touchPhase }), null);
			}
			else if (_touchPhase == TouchAreaPhase.ReleaseOut)
			{
				Debug.Log(string.Concat(new object[] { "secondary: ", _touchArea.m_name, ": ", _touchPhase }), null);
			}
		}
	}

	// Token: 0x06002AEC RID: 10988 RVA: 0x001BC8C4 File Offset: 0x001BACC4
	public override void Execute()
	{
	}

	// Token: 0x06002AED RID: 10989 RVA: 0x001BC8C6 File Offset: 0x001BACC6
	public override void Exit()
	{
		EntityManager.RemoveAllEntities();
	}
}
