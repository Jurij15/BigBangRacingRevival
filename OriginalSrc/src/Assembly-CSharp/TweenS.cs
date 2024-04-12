using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000514 RID: 1300
public static class TweenS
{
	// Token: 0x06002607 RID: 9735 RVA: 0x001A3001 File Offset: 0x001A1401
	public static void Initialize()
	{
		TweenS.m_components = new DynamicArray<TweenC>(100, 0.5f, 0.25f, 0.5f);
		TweenS.m_removeList = new List<TweenC>();
	}

	// Token: 0x06002608 RID: 9736 RVA: 0x001A3028 File Offset: 0x001A1428
	public static void AddTweenEndEventListener(TweenC _tweenComponent, TweenEventDelegate _tweenEventHandler)
	{
		_tweenComponent.tweenEndEvent = (TweenEventDelegate)Delegate.Combine(_tweenComponent.tweenEndEvent, _tweenEventHandler);
	}

	// Token: 0x06002609 RID: 9737 RVA: 0x001A3041 File Offset: 0x001A1441
	public static void RemoveTweenEndEventListener(TweenC _tweenComponent, TweenEventDelegate _tweenEventHandler)
	{
		_tweenComponent.tweenEndEvent = (TweenEventDelegate)Delegate.Remove(_tweenComponent.tweenEndEvent, _tweenEventHandler);
	}

	// Token: 0x0600260A RID: 9738 RVA: 0x001A305A File Offset: 0x001A145A
	public static void AddTweenStartEventListener(TweenC _tweenComponent, TweenEventDelegate _tweenEventHandler)
	{
		_tweenComponent.tweenStartEvent = (TweenEventDelegate)Delegate.Combine(_tweenComponent.tweenStartEvent, _tweenEventHandler);
	}

	// Token: 0x0600260B RID: 9739 RVA: 0x001A3073 File Offset: 0x001A1473
	public static void RemoveTweenStartEventListener(TweenC _tweenComponent, TweenEventDelegate _tweenEventHandler)
	{
		_tweenComponent.tweenStartEvent = (TweenEventDelegate)Delegate.Remove(_tweenComponent.tweenStartEvent, _tweenEventHandler);
	}

	// Token: 0x0600260C RID: 9740 RVA: 0x001A308C File Offset: 0x001A148C
	public static TweenC AddTween(Entity _entity, TweenStyle _style, float _startValue, float _endValue, float _duration, float _delay)
	{
		TweenC tweenC = TweenS.m_components.AddItem();
		tweenC.currentTweenStyle = _style;
		tweenC.startValue = Vector3.one * _startValue;
		tweenC.endValue = Vector3.one * _endValue;
		tweenC.currentValue = tweenC.startValue;
		tweenC.duration = _duration;
		tweenC.delay = _delay;
		tweenC.startTime = Main.m_resettingGameTime + _delay;
		tweenC.currentTime = Main.m_resettingGameTime;
		EntityManager.AddComponentToEntity(_entity, tweenC);
		if (!TweenS.m_createdList.Contains(tweenC))
		{
			TweenS.m_createdList.Add(tweenC);
		}
		return tweenC;
	}

	// Token: 0x0600260D RID: 9741 RVA: 0x001A3128 File Offset: 0x001A1528
	public static TweenC AddTween(Entity _entity, TweenStyle _style, Vector3 _startValue, Vector3 _endValue, float _duration, float _delay)
	{
		TweenC tweenC = TweenS.m_components.AddItem();
		tweenC.currentTweenStyle = _style;
		tweenC.startValue = _startValue;
		tweenC.endValue = _endValue;
		tweenC.currentValue = _startValue;
		tweenC.duration = _duration;
		tweenC.delay = _delay;
		tweenC.startTime = Main.m_resettingGameTime + _delay;
		tweenC.currentTime = Main.m_resettingGameTime;
		EntityManager.AddComponentToEntity(_entity, tweenC);
		if (!TweenS.m_createdList.Contains(tweenC))
		{
			TweenS.m_createdList.Add(tweenC);
		}
		return tweenC;
	}

	// Token: 0x0600260E RID: 9742 RVA: 0x001A31A8 File Offset: 0x001A15A8
	public static void ReInitialize(TweenC _c, TweenStyle _style, Vector3 _startValue, Vector3 _endValue, float _duration, float _delay)
	{
		_c.m_active = true;
		_c.currentTweenStyle = _style;
		_c.startValue = _startValue;
		_c.endValue = _endValue;
		_c.currentValue = _startValue;
		_c.duration = _duration;
		_c.delay = _delay;
		_c.startTime = Main.m_resettingGameTime + _delay;
		_c.currentTime = Main.m_resettingGameTime;
		if (!TweenS.m_createdList.Contains(_c))
		{
			TweenS.m_createdList.Add(_c);
		}
	}

	// Token: 0x0600260F RID: 9743 RVA: 0x001A321C File Offset: 0x001A161C
	public static TweenC AddTransformTween(TransformC _tc, TweenedProperty _component, TweenStyle _style, Vector3 _endValue, float _duration, float _delay, bool _removeComponentAtFinish)
	{
		return TweenS.AddTransformTween(_tc, _component, _style, _endValue, _duration, _delay, false, _removeComponentAtFinish);
	}

	// Token: 0x06002610 RID: 9744 RVA: 0x001A3230 File Offset: 0x001A1630
	public static TweenC AddTransformTween(TransformC _tc, TweenedProperty _component, TweenStyle _style, Vector3 _endValue, float _duration, float _delay, bool _globalTarget, bool _removeComponentAtFinish)
	{
		Vector3 vector = Vector3.zero;
		if (_component == TweenedProperty.Position)
		{
			vector = _tc.transform.localPosition;
		}
		else if (_component == TweenedProperty.Rotation)
		{
			if (_tc.forceRotation)
			{
				vector = _tc.forcedRotation.eulerAngles;
			}
			else
			{
				vector = _tc.transform.localRotation.eulerAngles;
			}
		}
		else if (_component == TweenedProperty.Scale)
		{
			if (_tc.forceScale)
			{
				vector = _tc.forcedScale;
			}
			else
			{
				vector = _tc.transform.localScale;
			}
		}
		else if (_component == TweenedProperty.Alpha)
		{
			vector = Vector3.one;
		}
		return TweenS.AddTransformTween(_tc, _component, _style, vector, _endValue, _duration, _delay, _globalTarget, _removeComponentAtFinish);
	}

	// Token: 0x06002611 RID: 9745 RVA: 0x001A32E8 File Offset: 0x001A16E8
	public static TweenC AddTransformTween(TransformC _tc, TweenedProperty _component, TweenStyle _style, Vector3 _startValue, Vector3 _endValue, float _duration, float _delay, bool _removeComponentAtFinish)
	{
		return TweenS.AddTransformTween(_tc, _component, _style, _startValue, _endValue, _duration, _delay, false, _removeComponentAtFinish);
	}

	// Token: 0x06002612 RID: 9746 RVA: 0x001A3308 File Offset: 0x001A1708
	public static TweenC AddTransformTween(TransformC _tc, TweenedProperty _component, TweenStyle _style, Vector3 _startValue, Vector3 _endValue, float _duration, float _delay, bool _globalTarget, bool _removeComponentAtFinish)
	{
		TweenC tweenC = TweenS.m_components.AddItem();
		tweenC.p_TC = _tc;
		tweenC.tweenedProperty = _component;
		tweenC.currentTweenStyle = _style;
		tweenC.startValue = _startValue;
		if (_globalTarget && _component == TweenedProperty.Position)
		{
			_endValue = _tc.transform.position + (_endValue - _tc.transform.position);
		}
		tweenC.endValue = _endValue;
		tweenC.currentValue = _startValue;
		tweenC.duration = _duration;
		tweenC.delay = _delay;
		tweenC.startTime = Main.m_resettingGameTime + _delay;
		tweenC.currentTime = Main.m_resettingGameTime;
		tweenC.removeComponentAtFinish = _removeComponentAtFinish;
		tweenC.alphaSprite = true;
		tweenC.alphaPrefabs = false;
		tweenC.alphaText = false;
		if (_tc != null)
		{
			EntityManager.AddComponentToEntity(_tc.p_entity, tweenC);
		}
		else
		{
			tweenC.m_active = true;
			tweenC.m_wasActive = true;
		}
		if (!TweenS.m_createdList.Contains(tweenC))
		{
			TweenS.m_createdList.Add(tweenC);
		}
		return tweenC;
	}

	// Token: 0x06002613 RID: 9747 RVA: 0x001A3404 File Offset: 0x001A1804
	public static void SetTweenAlphaProperties(TweenC _c, bool _alphaSprite, bool _alphaPrefabs, bool _alphaText, Shader _shader = null)
	{
		_c.alphaSprite = _alphaSprite;
		_c.alphaPrefabs = _alphaPrefabs;
		_c.alphaText = _alphaText;
		_c.alphaShader = _shader;
		TweenS.UpdateTween(_c, Mathf.Max(_c.currentTime - _c.startTime, 0f));
	}

	// Token: 0x06002614 RID: 9748 RVA: 0x001A3440 File Offset: 0x001A1840
	public static TweenC AddCurvedTransformTween(TransformC _tc, TweenedProperty _component, TweenStyle _style, Vector3 _endValue, Vector3 _control0, Vector3 _control1, float _duration, float _delay, bool _removeComponentAtFinish)
	{
		Vector3 vector = Vector3.zero;
		if (_component == TweenedProperty.Position)
		{
			vector = _tc.transform.localPosition;
		}
		else if (_component == TweenedProperty.Rotation)
		{
			if (_tc.forceRotation)
			{
				vector = _tc.forcedRotation.eulerAngles;
			}
			else
			{
				vector = _tc.transform.localRotation.eulerAngles;
			}
		}
		else if (_component == TweenedProperty.Scale)
		{
			if (_tc.forceScale)
			{
				vector = _tc.forcedScale;
			}
			else
			{
				vector = _tc.transform.localScale;
			}
		}
		return TweenS.AddCurvedTransformTween(_tc, _component, _style, vector, _endValue, _control0, _control1, _duration, _delay, _removeComponentAtFinish);
	}

	// Token: 0x06002615 RID: 9749 RVA: 0x001A34E8 File Offset: 0x001A18E8
	public static TweenC AddCurvedTransformTween(TransformC _tc, TweenedProperty _component, TweenStyle _style, Vector3 _startValue, Vector3 _endValue, Vector3 _control0, Vector3 _control1, float _duration, float _delay, bool _removeComponentAtFinish)
	{
		TweenC tweenC = TweenS.m_components.AddItem();
		tweenC.p_TC = _tc;
		tweenC.tweenedProperty = _component;
		tweenC.currentTweenStyle = _style;
		tweenC.startValue = _startValue;
		tweenC.endValue = _endValue;
		tweenC.currentValue = _startValue;
		tweenC.control0 = _control0;
		tweenC.control1 = _control1;
		tweenC.duration = _duration;
		tweenC.delay = _delay;
		tweenC.startTime = Main.m_resettingGameTime + _delay;
		tweenC.currentTime = Main.m_resettingGameTime;
		tweenC.curved = true;
		tweenC.removeComponentAtFinish = _removeComponentAtFinish;
		tweenC.alphaSprite = true;
		tweenC.alphaPrefabs = false;
		tweenC.alphaText = false;
		if (_tc != null)
		{
			EntityManager.AddComponentToEntity(_tc.p_entity, tweenC);
		}
		else
		{
			tweenC.m_active = true;
			tweenC.m_wasActive = true;
		}
		if (!TweenS.m_createdList.Contains(tweenC))
		{
			TweenS.m_createdList.Add(tweenC);
		}
		return tweenC;
	}

	// Token: 0x06002616 RID: 9750 RVA: 0x001A35CC File Offset: 0x001A19CC
	public static void RemoveComponent(TweenC _c)
	{
		if (_c.tweenEndEvent != null)
		{
			Delegate[] invocationList = _c.tweenEndEvent.GetInvocationList();
			foreach (Delegate @delegate in invocationList)
			{
				_c.tweenEndEvent = (TweenEventDelegate)Delegate.Remove(_c.tweenEndEvent, (TweenEventDelegate)@delegate);
			}
			_c.tweenEndEvent = null;
		}
		if (_c.tweenStartEvent != null)
		{
			Delegate[] invocationList2 = _c.tweenStartEvent.GetInvocationList();
			foreach (Delegate delegate2 in invocationList2)
			{
				_c.tweenStartEvent = (TweenEventDelegate)Delegate.Remove(_c.tweenStartEvent, (TweenEventDelegate)delegate2);
			}
			_c.tweenStartEvent = null;
		}
		EntityManager.RemoveComponentFromEntity(_c);
		if (!TweenS.m_updatingComponents)
		{
			TweenS.m_components.RemoveItem(_c);
		}
		else if (!TweenS.m_removeList.Contains(_c))
		{
			TweenS.m_removeList.Add(_c);
		}
	}

	// Token: 0x06002617 RID: 9751 RVA: 0x001A36C8 File Offset: 0x001A1AC8
	public static void RemoveAllTweensFromEntity(Entity _e)
	{
		List<IComponent> componentsByEntity = EntityManager.GetComponentsByEntity(ComponentType.Tween, _e);
		while (componentsByEntity.Count > 0)
		{
			TweenS.RemoveComponent(componentsByEntity[0] as TweenC);
			componentsByEntity.RemoveAt(0);
		}
	}

	// Token: 0x06002618 RID: 9752 RVA: 0x001A3708 File Offset: 0x001A1B08
	public static void SetAdditionalTweenProperties(TweenC _c, int _repeatCount, bool _mirror = false, TweenStyle _mirroredTweenStyle = TweenStyle.Linear)
	{
		_c.repeats = _repeatCount;
		_c.mirrored = _mirror;
		_c.mirroredTweenStyle = _mirroredTweenStyle;
		if (_mirror && _c.repeats != -1)
		{
			_c.repeats = _c.repeats * 2 + 1;
		}
		if (!TweenS.m_createdList.Contains(_c))
		{
			TweenS.m_createdList.Add(_c);
		}
	}

	// Token: 0x06002619 RID: 9753 RVA: 0x001A3767 File Offset: 0x001A1B67
	public static void SetRemoveEntityAtFinish(TweenC _c, bool _remove)
	{
		_c.removeEntityAtFinish = _remove;
	}

	// Token: 0x0600261A RID: 9754 RVA: 0x001A3770 File Offset: 0x001A1B70
	public static void SetRemoveComponentAtFinish(TweenC _c, bool _remove)
	{
		_c.removeComponentAtFinish = _remove;
	}

	// Token: 0x0600261B RID: 9755 RVA: 0x001A377C File Offset: 0x001A1B7C
	public static void Update()
	{
		TweenS.m_updatingComponents = true;
		int num = TweenS.m_components.m_aliveCount;
		for (int i = 0; i < num; i++)
		{
			TweenC tweenC = TweenS.m_components.m_array[TweenS.m_components.m_aliveIndices[i]];
			if (tweenC.m_active)
			{
				tweenC.currentTime += ((!tweenC.useUnscaledDeltaTime) ? Main.m_gameDeltaTime : Main.m_dt);
				float num2 = Mathf.Max(tweenC.currentTime - tweenC.startTime, 0f);
				if (!tweenC.hasStarted && num2 > 0f)
				{
					if (tweenC.tweenStartEvent != null)
					{
						tweenC.tweenStartEvent(tweenC);
					}
					tweenC.hasStarted = true;
				}
				if (num2 >= tweenC.duration)
				{
					if (tweenC.currentRepeat < tweenC.repeats || tweenC.repeats == -1)
					{
						num2 -= tweenC.duration;
						tweenC.startTime += tweenC.duration;
						tweenC.currentRepeat++;
						if (tweenC.mirrored)
						{
							Vector3 startValue = tweenC.startValue;
							tweenC.startValue = tweenC.endValue;
							tweenC.endValue = startValue;
							TweenStyle currentTweenStyle = tweenC.currentTweenStyle;
							tweenC.currentTweenStyle = tweenC.mirroredTweenStyle;
							tweenC.mirroredTweenStyle = currentTweenStyle;
						}
					}
					else
					{
						tweenC.m_active = false;
						tweenC.currentValue = tweenC.endValue;
						tweenC.hasFinished = true;
					}
				}
				if (tweenC.hasStarted)
				{
					TweenS.UpdateTween(tweenC, num2);
				}
				if (num2 >= tweenC.duration && tweenC.hasFinished)
				{
					if (tweenC.tweenEndEvent != null)
					{
						tweenC.tweenEndEvent(tweenC);
					}
					if (tweenC.removeComponentAtFinish)
					{
						TweenS.RemoveComponent(tweenC);
					}
				}
			}
			num = TweenS.m_components.m_aliveCount;
		}
		TweenS.m_updatingComponents = false;
		while (TweenS.m_removeList.Count > 0)
		{
			int num3 = TweenS.m_removeList.Count - 1;
			TweenC tweenC2 = TweenS.m_removeList[num3];
			if (tweenC2.removeEntityAtFinish)
			{
				EntityManager.RemoveEntity(tweenC2.p_entity);
			}
			else
			{
				TweenS.RemoveComponent(tweenC2);
			}
			TweenS.m_removeList.RemoveAt(num3);
		}
		TweenS.m_components.Update();
	}

	// Token: 0x0600261C RID: 9756 RVA: 0x001A39C4 File Offset: 0x001A1DC4
	public static void LateUpdate()
	{
		while (TweenS.m_createdList.Count > 0)
		{
			int num = TweenS.m_createdList.Count - 1;
			TweenC tweenC = TweenS.m_createdList[num];
			if (tweenC.m_active && tweenC.p_entity != null && tweenC.currentTime - tweenC.startTime >= 0f)
			{
				TweenS.UpdateTween(tweenC, 0f);
			}
			TweenS.m_createdList.RemoveAt(num);
		}
	}

	// Token: 0x0600261D RID: 9757 RVA: 0x001A3A44 File Offset: 0x001A1E44
	public static void UpdateTween(TweenC c, float t)
	{
		if (c.p_TC == null)
		{
			if (c.m_active)
			{
				c.currentValue.x = TweenS.tween(c.currentTweenStyle, t, c.duration, c.startValue.x, c.endValue.x - c.startValue.x);
				if (c.tweenedProperty != TweenedProperty.None)
				{
					c.currentValue.y = TweenS.tween(c.currentTweenStyle, t, c.duration, c.startValue.y, c.endValue.y - c.startValue.y);
					c.currentValue.z = TweenS.tween(c.currentTweenStyle, t, c.duration, c.startValue.z, c.endValue.z - c.startValue.z);
				}
			}
		}
		else
		{
			TransformC p_TC = c.p_TC;
			if (c.tweenedProperty == TweenedProperty.Position)
			{
				if (c.m_active)
				{
					if (!c.curved)
					{
						c.currentValue.x = TweenS.tween(c.currentTweenStyle, t, c.duration, c.startValue.x, c.endValue.x - c.startValue.x);
						c.currentValue.y = TweenS.tween(c.currentTweenStyle, t, c.duration, c.startValue.y, c.endValue.y - c.startValue.y);
						c.currentValue.z = TweenS.tween(c.currentTweenStyle, t, c.duration, c.startValue.z, c.endValue.z - c.startValue.z);
						p_TC.transform.localPosition = c.currentValue;
					}
					else
					{
						c.currentValue = TweenS.curveTween(c.currentTweenStyle, t, c.duration, c.startValue, c.endValue, c.control0, c.control1);
						p_TC.transform.localPosition = c.currentValue;
					}
				}
				else
				{
					p_TC.transform.localPosition = c.endValue;
					c.currentValue = c.endValue;
				}
				p_TC.updatePosition = true;
			}
			else if (c.tweenedProperty == TweenedProperty.Rotation)
			{
				if (c.m_active)
				{
					c.currentValue.x = TweenS.tween(c.currentTweenStyle, t, c.duration, c.startValue.x, c.endValue.x - c.startValue.x);
					c.currentValue.y = TweenS.tween(c.currentTweenStyle, t, c.duration, c.startValue.y, c.endValue.y - c.startValue.y);
					c.currentValue.z = TweenS.tween(c.currentTweenStyle, t, c.duration, c.startValue.z, c.endValue.z - c.startValue.z);
					if (p_TC.forceRotation)
					{
						p_TC.forcedRotation = Quaternion.Euler(c.currentValue);
					}
					else
					{
						p_TC.transform.localRotation = Quaternion.Euler(c.currentValue);
					}
				}
				else
				{
					if (p_TC.forceRotation)
					{
						p_TC.forcedRotation = Quaternion.Euler(c.endValue);
					}
					else
					{
						p_TC.transform.localRotation = Quaternion.Euler(c.endValue);
					}
					c.currentValue = c.endValue;
					if (c.removeComponentAtFinish)
					{
						TweenS.m_removeList.Add(c);
					}
				}
				p_TC.updateRotation = true;
			}
			else if (c.tweenedProperty == TweenedProperty.Scale)
			{
				if (c.m_active)
				{
					c.currentValue.x = TweenS.tween(c.currentTweenStyle, t, c.duration, c.startValue.x, c.endValue.x - c.startValue.x);
					c.currentValue.y = TweenS.tween(c.currentTweenStyle, t, c.duration, c.startValue.y, c.endValue.y - c.startValue.y);
					c.currentValue.z = TweenS.tween(c.currentTweenStyle, t, c.duration, c.startValue.z, c.endValue.z - c.startValue.z);
					if (p_TC.forceScale)
					{
						p_TC.forcedScale = c.currentValue;
					}
					else
					{
						p_TC.transform.localScale = c.currentValue;
					}
				}
				else
				{
					if (p_TC.forceScale)
					{
						p_TC.forcedScale = c.endValue;
					}
					else
					{
						p_TC.transform.localScale = c.endValue;
					}
					c.currentValue = c.endValue;
				}
				p_TC.updateScale = true;
				p_TC.updateRotation = true;
			}
			else if (c.tweenedProperty == TweenedProperty.Alpha)
			{
				if (c.m_active)
				{
					c.currentValue.x = TweenS.tween(c.currentTweenStyle, t, c.duration, c.startValue.x, c.endValue.x - c.startValue.x);
				}
				else
				{
					c.currentValue = c.endValue;
				}
				if (c.alphaSprite)
				{
					SpriteS.SetAlphaByTransformComponent(c.p_TC, c.currentValue.x, false, false);
				}
				if (c.alphaText && c.p_TC.p_entity != null)
				{
					List<IComponent> componentsByEntity = EntityManager.GetComponentsByEntity(ComponentType.TextMesh, c.p_TC.p_entity);
					for (int i = 0; i < componentsByEntity.Count; i++)
					{
						TextMeshC textMeshC = componentsByEntity[i] as TextMeshC;
						if (c.alphaShader != null)
						{
							textMeshC.m_textMesh.GetComponent<Renderer>().material.shader = c.alphaShader;
						}
						Color color = textMeshC.m_textMesh.GetComponent<Renderer>().material.color;
						color.a = c.currentValue.x;
						textMeshC.m_textMesh.GetComponent<Renderer>().material.color = color;
					}
				}
				if (c.alphaPrefabs && c.p_TC.p_entity != null)
				{
					List<IComponent> componentsByEntity2 = EntityManager.GetComponentsByEntity(ComponentType.Prefab, c.p_TC.p_entity);
					for (int j = 0; j < componentsByEntity2.Count; j++)
					{
						PrefabC prefabC = componentsByEntity2[j] as PrefabC;
						if (prefabC.p_gameObject != null)
						{
							Renderer component = prefabC.p_gameObject.GetComponent<Renderer>();
							if (component != null && component.material != null)
							{
								if (c.alphaShader != null)
								{
									component.material.shader = c.alphaShader;
								}
								Color color2 = component.material.color;
								color2.a = c.currentValue.x;
								component.material.color = color2;
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x0600261E RID: 9758 RVA: 0x001A4198 File Offset: 0x001A2598
	public static float tween(TweenStyle _style, float _currentTime, float _duration, float _start, float _difference)
	{
		switch (_style)
		{
		case TweenStyle.Linear:
			return TweenS.linear(_currentTime, _start, _duration, _difference);
		case TweenStyle.QuadIn:
			return TweenS.easeInQuad(_currentTime, _start, _duration, _difference);
		case TweenStyle.QuadOut:
			return TweenS.easeOutQuad(_currentTime, _start, _duration, _difference);
		case TweenStyle.QuadInOut:
			return TweenS.easeInOutQuad(_currentTime, _start, _duration, _difference);
		case TweenStyle.CubicIn:
			return TweenS.easeInCubic(_currentTime, _start, _duration, _difference);
		case TweenStyle.CubicOut:
			return TweenS.easeOutCubic(_currentTime, _start, _duration, _difference);
		case TweenStyle.CubicInOut:
			return TweenS.easeInOutCubic(_currentTime, _start, _duration, _difference);
		case TweenStyle.ExpoIn:
			return TweenS.easeInExpo(_currentTime, _start, _duration, _difference);
		case TweenStyle.ExpoOut:
			return TweenS.easeOutExpo(_currentTime, _start, _duration, _difference);
		case TweenStyle.ExpoInOut:
			return TweenS.easeOutExpo(_currentTime, _start, _duration, _difference);
		case TweenStyle.BackOut:
			return TweenS.easeOutBack(_currentTime, _start, _duration, _difference);
		case TweenStyle.BounceOut:
			return TweenS.easeOutBounce(_currentTime, _start, _duration, _difference);
		case TweenStyle.ElasticOut:
			return TweenS.easeOutElastic(_currentTime, _start, _duration, _difference);
		default:
			return 0f;
		}
	}

	// Token: 0x0600261F RID: 9759 RVA: 0x001A4278 File Offset: 0x001A2678
	public static Vector3 curveTween(TweenStyle _style, float _currentTime, float _duration, Vector3 _start, Vector3 _end, Vector3 _control0, Vector3 _control1)
	{
		switch (_style)
		{
		case TweenStyle.Linear:
			return ToolBox.GetBezierPoint(TweenS.linear(_currentTime, 0f, _duration, 1f), _start, _control0, _control1, _end);
		case TweenStyle.QuadIn:
			return ToolBox.GetBezierPoint(TweenS.easeInQuad(_currentTime, 0f, _duration, 1f), _start, _control0, _control1, _end);
		case TweenStyle.QuadOut:
			return ToolBox.GetBezierPoint(TweenS.easeOutQuad(_currentTime, 0f, _duration, 1f), _start, _control0, _control1, _end);
		case TweenStyle.QuadInOut:
			return ToolBox.GetBezierPoint(TweenS.easeInOutQuad(_currentTime, 0f, _duration, 1f), _start, _control0, _control1, _end);
		case TweenStyle.CubicIn:
			return ToolBox.GetBezierPoint(TweenS.easeInCubic(_currentTime, 0f, _duration, 1f), _start, _control0, _control1, _end);
		case TweenStyle.CubicOut:
			return ToolBox.GetBezierPoint(TweenS.easeOutCubic(_currentTime, 0f, _duration, 1f), _start, _control0, _control1, _end);
		case TweenStyle.CubicInOut:
			return ToolBox.GetBezierPoint(TweenS.easeInOutCubic(_currentTime, 0f, _duration, 1f), _start, _control0, _control1, _end);
		case TweenStyle.ExpoIn:
			return ToolBox.GetBezierPoint(TweenS.easeInExpo(_currentTime, 0f, _duration, 1f), _start, _control0, _control1, _end);
		case TweenStyle.ExpoOut:
			return ToolBox.GetBezierPoint(TweenS.easeOutExpo(_currentTime, 0f, _duration, 1f), _start, _control0, _control1, _end);
		case TweenStyle.ExpoInOut:
			return ToolBox.GetBezierPoint(TweenS.easeInOutExpo(_currentTime, 0f, _duration, 1f), _start, _control0, _control1, _end);
		case TweenStyle.BackOut:
			return ToolBox.GetBezierPoint(TweenS.easeOutBack(_currentTime, 0f, _duration, 1f), _start, _control0, _control1, _end);
		case TweenStyle.BounceOut:
			return ToolBox.GetBezierPoint(TweenS.easeOutBounce(_currentTime, 0f, _duration, 1f), _start, _control0, _control1, _end);
		case TweenStyle.ElasticOut:
			return ToolBox.GetBezierPoint(TweenS.easeOutElastic(_currentTime, 0f, _duration, 1f), _start, _control0, _control1, _end);
		default:
			return Vector3.zero;
		}
	}

	// Token: 0x06002620 RID: 9760 RVA: 0x001A4594 File Offset: 0x001A2994
	public static float easeInQuad(float t, float b, float d, float c)
	{
		return c * (t /= d) * t + b;
	}

	// Token: 0x06002621 RID: 9761 RVA: 0x001A45A2 File Offset: 0x001A29A2
	public static float easeOutQuad(float t, float b, float d, float c)
	{
		return -c * (t /= d) * (t - 2f) + b;
	}

	// Token: 0x06002622 RID: 9762 RVA: 0x001A45B8 File Offset: 0x001A29B8
	public static float easeInOutQuad(float t, float b, float d, float c)
	{
		if ((t /= d / 2f) < 1f)
		{
			return c / 2f * t * t + b;
		}
		return -c / 2f * ((t -= 1f) * (t - 2f) - 1f) + b;
	}

	// Token: 0x06002623 RID: 9763 RVA: 0x001A460C File Offset: 0x001A2A0C
	public static float easeInCubic(float t, float b, float d, float c)
	{
		return c * (t /= d) * t * t + b;
	}

	// Token: 0x06002624 RID: 9764 RVA: 0x001A461C File Offset: 0x001A2A1C
	public static float easeOutCubic(float t, float b, float d, float c)
	{
		return c * ((t = t / d - 1f) * t * t + 1f) + b;
	}

	// Token: 0x06002625 RID: 9765 RVA: 0x001A4638 File Offset: 0x001A2A38
	public static float easeInOutCubic(float t, float b, float d, float c)
	{
		if ((t /= d / 2f) < 1f)
		{
			return c / 2f * t * t * t + b;
		}
		return c / 2f * ((t -= 2f) * t * t + 2f) + b;
	}

	// Token: 0x06002626 RID: 9766 RVA: 0x001A4689 File Offset: 0x001A2A89
	public static float easeInExpo(float t, float b, float d, float c)
	{
		return (t != 0f) ? (c * Mathf.Pow(2f, 10f * (t / d - 1f)) + b - c * 0.001f) : b;
	}

	// Token: 0x06002627 RID: 9767 RVA: 0x001A46C1 File Offset: 0x001A2AC1
	public static float easeOutExpo(float t, float b, float d, float c)
	{
		return (t != d) ? (c * 1.001f * (-Mathf.Pow(2f, -10f * t / d) + 1f) + b) : (b + c);
	}

	// Token: 0x06002628 RID: 9768 RVA: 0x001A46F8 File Offset: 0x001A2AF8
	public static float easeInOutExpo(float t, float b, float d, float c)
	{
		if (t == 0f)
		{
			return b;
		}
		if (t == d)
		{
			return b + c;
		}
		if ((t /= d / 2f) < 1f)
		{
			return c / 2f * Mathf.Pow(2f, 10f * (t - 1f)) + b - c * 0.0005f;
		}
		return c / 2f * 1.0005f * (-Mathf.Pow(2f, -10f * (t -= 1f)) + 2f) + b;
	}

	// Token: 0x06002629 RID: 9769 RVA: 0x001A4790 File Offset: 0x001A2B90
	public static float easeOutBack(float t, float b, float d, float c)
	{
		float num = 1.70158f;
		return c * ((t = t / d - 1f) * t * ((num + 1f) * t + num) + 1f) + b;
	}

	// Token: 0x0600262A RID: 9770 RVA: 0x001A47C8 File Offset: 0x001A2BC8
	public static float easeOutElastic(float t, float b, float d, float c)
	{
		float num = 6.2831855f;
		float num2 = 0.5f;
		float num3 = 0.5f;
		if (t == 0f)
		{
			return b;
		}
		if ((t /= d) == 1f)
		{
			return b + c;
		}
		if (num2 != 0f)
		{
			num2 = d * 0.3f;
		}
		float num4;
		if (num3 != 0f || (c > 0f && num3 < c) || (c < 0f && num3 < -c))
		{
			num3 = c;
			num4 = num2 / 4f;
		}
		else
		{
			num4 = num2 / num * Mathf.Asin(c / num3);
		}
		return num3 * Mathf.Pow(2f, -10f * t) * Mathf.Sin((t * d - num4) * num / num2) + c + b;
	}

	// Token: 0x0600262B RID: 9771 RVA: 0x001A4890 File Offset: 0x001A2C90
	public static float easeOutBounce(float t, float b, float d, float c)
	{
		if ((t /= d) < 0.36363637f)
		{
			return c * (7.5625f * t * t) + b;
		}
		if (t < 0.72727275f)
		{
			return c * (7.5625f * (t -= 0.54545456f) * t + 0.75f) + b;
		}
		if (t < 0.90909094f)
		{
			return c * (7.5625f * (t -= 0.8181818f) * t + 0.9375f) + b;
		}
		return c * (7.5625f * (t -= 0.95454544f) * t + 0.984375f) + b;
	}

	// Token: 0x0600262C RID: 9772 RVA: 0x001A4927 File Offset: 0x001A2D27
	public static float linear(float t, float b, float d, float c)
	{
		return c * t / d + b;
	}

	// Token: 0x04002B53 RID: 11091
	public static DynamicArray<TweenC> m_components;

	// Token: 0x04002B54 RID: 11092
	private static List<TweenC> m_removeList;

	// Token: 0x04002B55 RID: 11093
	private static List<TweenC> m_createdList = new List<TweenC>();

	// Token: 0x04002B56 RID: 11094
	public static bool m_updatingComponents = false;
}
