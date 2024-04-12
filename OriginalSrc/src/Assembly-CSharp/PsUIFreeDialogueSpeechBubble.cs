using System;
using UnityEngine;

// Token: 0x02000247 RID: 583
public class PsUIFreeDialogueSpeechBubble : PsUIDialogueSpeechBubble
{
	// Token: 0x060011B0 RID: 4528 RVA: 0x000AB8F8 File Offset: 0x000A9CF8
	public PsUIFreeDialogueSpeechBubble(Vector2 _rootPos, string _text, string _okText, string _cancelText, Action _proceedOk, Action _proceedCancel, Camera _camera = null)
	{
		this.m_rootPos = _rootPos;
		this.m_text = _text;
		if (_camera != null)
		{
			this.SetCamera(_camera, true, false);
		}
		if (_okText.Contains(";"))
		{
			string[] array = _okText.Split(new char[] { ';' }, 1);
			this.m_okText = array[0];
			for (int i = 1; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[] { ':' }, 1);
				if (array2[0] == "UNLOCK")
				{
					this.m_okUNLOCK = array2[1];
				}
				else if (array2[0] == "ACTION")
				{
					this.m_okACTION = array2[1];
				}
			}
		}
		else
		{
			this.m_okText = _okText;
			this.m_okUNLOCK = null;
			this.m_okACTION = null;
		}
		this.m_cancelText = _cancelText;
		this.m_proceedOk = _proceedOk;
		this.m_proceedCancel = _proceedCancel;
		if (this.m_rootPos.x > 0f)
		{
			if (this.m_rootPos.y > 0f)
			{
				this.m_handlePosition = SpeechBubbleHandlePosition.TopRight;
				this.m_handleType = SpeechBubbleHandleType.SmallToLeft;
			}
			else
			{
				this.m_handlePosition = SpeechBubbleHandlePosition.BottomRight;
				this.m_handleType = SpeechBubbleHandleType.SmallToRight;
			}
		}
		else if (this.m_rootPos.y > 0f)
		{
			this.m_handlePosition = SpeechBubbleHandlePosition.TopLeft;
			this.m_handleType = SpeechBubbleHandleType.SmallToRight;
		}
		else
		{
			this.m_handlePosition = SpeechBubbleHandlePosition.BottomLeft;
			this.m_handleType = SpeechBubbleHandleType.SmallToLeft;
		}
		this.SetWidth(0.7f, RelativeTo.ScreenHeight);
		this.SetSpacing(-0.05f, RelativeTo.ScreenHeight);
		this.SetDepthOffset(-20f);
		this.RemoveDrawHandler();
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.SetMargins(0.1f, 0.1f, 0.05f, 0.05f, RelativeTo.ScreenHeight);
		uiverticalList.SetDrawHandler(new UIDrawDelegate(this.BubbleDrawHandler));
		new UITextbox(uiverticalList, false, string.Empty, this.m_text, PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.035f, RelativeTo.ScreenShortest, false, Align.Left, Align.Top, null, true, null)
		{
			m_tmc = 
			{
				m_renderer = 
				{
					material = 
					{
						shader = Shader.Find("Framework/FontShader")
					}
				}
			}
		}.m_tmc.m_textMesh.GetComponent<Renderer>().material.SetColor("_Color", DebugDraw.HexToColor("2aaded"));
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.SetSpacing(0.05f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.05f, RelativeTo.ScreenHeight);
		if (this.m_handlePosition == SpeechBubbleHandlePosition.BottomRight || this.m_handlePosition == SpeechBubbleHandlePosition.TopRight)
		{
			uihorizontalList.SetHorizontalAlign(0f);
		}
		else
		{
			uihorizontalList.SetHorizontalAlign(1f);
		}
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetDepthOffset(-20f);
		if (this.m_okText != string.Empty)
		{
			this.m_ok = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
			this.m_ok.SetText(this.m_okText, 0.04f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			this.m_ok.SetHeight(0.075f, RelativeTo.ScreenHeight);
			this.m_ok.SetGreenColors(true);
		}
		if (this.m_cancelText != string.Empty)
		{
			this.m_cancel = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
			this.m_cancel.SetText(this.m_cancelText, 0.04f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			this.m_cancel.SetHeight(0.075f, RelativeTo.ScreenHeight);
		}
		this.Update();
		Entity entity = EntityManager.AddEntity();
		this.m_rootTC = TransformS.AddComponent(entity, this.m_rootPos);
		Vector2 vector;
		vector..ctor(this.m_actualWidth * 0.5f, this.m_actualHeight * 0.5f + (float)Screen.height * 0.025f);
		if (this.m_rootPos.x > 0f)
		{
			vector.x = this.m_actualWidth * -0.5f;
			if (this.m_rootPos.y > 0f)
			{
				vector.y = this.m_actualHeight * -0.5f - (float)Screen.height * 0.025f;
			}
		}
		else if (this.m_rootPos.y > 0f)
		{
			vector.y = this.m_actualHeight * -0.5f - (float)Screen.height * 0.025f;
		}
		TransformS.ParentComponent(this.m_TC, this.m_rootTC, vector);
		TransformS.SetScale(this.m_rootTC, 0f);
		TweenC tweenC = TweenS.AddTransformTween(this.m_rootTC, TweenedProperty.Scale, TweenStyle.CubicOut, Vector3.zero, Vector3.one, 0.25f, 0f, true);
		SoundS.PlaySingleShot("/UI/SpeechBubbleAppear", Vector3.zero, 1f);
	}

	// Token: 0x060011B1 RID: 4529 RVA: 0x000ABDD8 File Offset: 0x000AA1D8
	public override void Step()
	{
		if (this.m_exiting)
		{
			return;
		}
		if (this.m_ok != null && this.m_ok.m_hit)
		{
			this.Proceed();
		}
		else if (this.m_cancel != null && this.m_cancel.m_hit)
		{
			this.ProceedCancel();
		}
		base.Step();
	}

	// Token: 0x060011B2 RID: 4530 RVA: 0x000ABE40 File Offset: 0x000AA240
	public override void Proceed()
	{
		this.m_exiting = true;
		if (this.m_proceedOk != null)
		{
			this.m_proceedOk.Invoke();
		}
		TransformS.Move(this.m_rootTC, Vector3.forward * 50f);
		TweenC tweenC = TweenS.AddTransformTween(this.m_rootTC, TweenedProperty.Scale, TweenStyle.CubicIn, Vector3.zero, 0.25f, 0f, false);
		TweenS.AddTweenEndEventListener(tweenC, new TweenEventDelegate(this.TweenEventHandler));
		if (this.m_okUNLOCK != null)
		{
			PsMetagameData.TemporarilyUnlockUnlockable(this.m_okUNLOCK);
		}
		if (this.m_okACTION != null)
		{
			if (this.m_okACTION.Contains("Tutorial>"))
			{
				TouchAreaS.Disable();
				string text = this.m_okACTION.Substring(9);
				Debug.Log("Tutorial key is: " + text, null);
				if (text == "MechanicButton")
				{
					Debug.LogWarning("setting mechianic");
				}
				else if (text == "PlanetNode2")
				{
					if (PsPlanetManager.GetCurrentPlanet().GetMainPath().m_currentNodeId <= 2)
					{
						TimerS.AddComponent(PsPlanetManager.GetCurrentPlanet().m_spaceEntity, "Tutorial finger timer", 0f, 2f, false, delegate(TimerC _c)
						{
							TimerS.RemoveComponent(_c);
							PsUITutorialArrowNode psUITutorialArrowNode = new PsUITutorialArrowNode(2);
						});
					}
					else
					{
						TouchAreaS.Enable();
					}
				}
				else if (text == "CurrentSidePathNode")
				{
					new PsUITutorialArrowNodeSide();
				}
			}
			else if (this.m_okACTION == "Restart")
			{
				PsState.m_activeGameLoop.RestartMinigame();
			}
			else if (this.m_okACTION == "FreeBooster")
			{
				PsState.m_activeGameLoop.m_freeConsumableUnlock = "Booster";
			}
			else if (this.m_okACTION == "FreeBoosterAndRestart")
			{
				PsState.m_activeGameLoop.m_freeConsumableUnlock = "Booster";
				PsState.m_activeGameLoop.RestartMinigame();
			}
			else if (this.m_okACTION == "FreeRent")
			{
				PsState.m_activeGameLoop.m_freeConsumableUnlock = "Rent";
			}
			else if (this.m_okACTION == "SpawnFreshNode")
			{
				PsFloaters.CreateFreshAndFreePlanetForced();
			}
		}
	}

	// Token: 0x060011B3 RID: 4531 RVA: 0x000AC07C File Offset: 0x000AA47C
	public override void ProceedCancel()
	{
		this.m_exiting = true;
		if (this.m_proceedCancel != null)
		{
			this.m_proceedCancel.Invoke();
		}
		TransformS.Move(this.m_rootTC, Vector3.forward * 50f);
		TweenC tweenC = TweenS.AddTransformTween(this.m_rootTC, TweenedProperty.Scale, TweenStyle.CubicIn, Vector3.zero, 0.25f, 0f, false);
		TweenS.AddTweenEndEventListener(tweenC, new TweenEventDelegate(this.TweenEventHandler));
	}

	// Token: 0x060011B4 RID: 4532 RVA: 0x000AC0F0 File Offset: 0x000AA4F0
	private void TweenEventHandler(TweenC _c)
	{
		this.Destroy();
	}

	// Token: 0x060011B5 RID: 4533 RVA: 0x000AC0F8 File Offset: 0x000AA4F8
	public override void Destroy()
	{
		TransformS.UnparentComponent(this.m_TC, true);
		EntityManager.RemoveEntity(this.m_rootTC.p_entity);
		base.Destroy();
	}

	// Token: 0x060011B6 RID: 4534 RVA: 0x000AC11C File Offset: 0x000AA51C
	public void BubbleDrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] array = DebugDraw.GetBezierRect(_c.m_actualWidth, _c.m_actualHeight, 0.025f * (float)Screen.height, 10, Vector2.zero);
		array = DebugDraw.AddSpeechHandleToVectorArray(array, this.m_handlePosition, this.m_handleType);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -5f, array, (float)Screen.width * 0.005f, DebugDraw.HexToColor("#4dcfff"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -3f, array, (float)Screen.width * 0.008f, new Color(1f, 1f, 1f, 0.5f), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Gradient2Mat_Material), this.m_camera, Position.Inside, true);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, array, DebugDraw.HexToUint("#FFFFFF"), DebugDraw.HexToUint("#FFFFFF"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
	}

	// Token: 0x040014A0 RID: 5280
	private PsUIGenericButton m_ok;

	// Token: 0x040014A1 RID: 5281
	private PsUIGenericButton m_cancel;

	// Token: 0x040014A2 RID: 5282
	private Action m_proceedOk;

	// Token: 0x040014A3 RID: 5283
	private Action m_proceedCancel;

	// Token: 0x040014A4 RID: 5284
	private string m_text;

	// Token: 0x040014A5 RID: 5285
	private string m_okText;

	// Token: 0x040014A6 RID: 5286
	private string m_okUNLOCK;

	// Token: 0x040014A7 RID: 5287
	private string m_okACTION;

	// Token: 0x040014A8 RID: 5288
	private string m_cancelText;

	// Token: 0x040014A9 RID: 5289
	private Vector2 m_rootPos;

	// Token: 0x040014AA RID: 5290
	private TransformC m_rootTC;

	// Token: 0x040014AB RID: 5291
	private bool m_exiting;

	// Token: 0x040014AC RID: 5292
	private SpeechBubbleHandlePosition m_handlePosition;

	// Token: 0x040014AD RID: 5293
	private SpeechBubbleHandleType m_handleType;
}
