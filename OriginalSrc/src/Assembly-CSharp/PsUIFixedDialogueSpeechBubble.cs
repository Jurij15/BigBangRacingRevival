using System;
using UnityEngine;

// Token: 0x02000243 RID: 579
public class PsUIFixedDialogueSpeechBubble : PsUIDialogueSpeechBubble
{
	// Token: 0x06001193 RID: 4499 RVA: 0x000AA8C4 File Offset: 0x000A8CC4
	public PsUIFixedDialogueSpeechBubble(Vector2 _rootPos, string _text, string _okText, string _cancelText, Action _proceedOk, Action _proceedCancel, SpeechBubbleHandlePosition _handlePosition = SpeechBubbleHandlePosition.Left, SpeechBubbleHandleType _handleType = SpeechBubbleHandleType.SmallToLeft, Camera _camera = null)
	{
		if (_camera != null)
		{
			this.m_camera = _camera;
		}
		this.m_rootPos = _rootPos;
		this.m_text = _text;
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
		this.m_handlePosition = _handlePosition;
		this.m_handleType = _handleType;
		this.SetWidth(0.65f, RelativeTo.ScreenHeight);
		this.SetSpacing(-0.05f, RelativeTo.ScreenHeight);
		this.SetDepthOffset(-20f);
		this.SetAlign(0.5f, 0f);
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
		TransformS.ParentComponent(this.m_TC, this.m_rootTC);
		TransformS.SetScale(this.m_rootTC, 0f);
		TweenC tweenC = TweenS.AddTransformTween(this.m_rootTC, TweenedProperty.Scale, TweenStyle.CubicOut, Vector3.zero, Vector3.one, 0.25f, 0f, true);
		SoundS.PlaySingleShot("/UI/SpeechBubbleAppear", Vector3.zero, 1f);
	}

	// Token: 0x06001194 RID: 4500 RVA: 0x000AAC3C File Offset: 0x000A903C
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

	// Token: 0x06001195 RID: 4501 RVA: 0x000AACA4 File Offset: 0x000A90A4
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
				else if (!(text == "Booster"))
				{
					if (text == "PlanetNode2")
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
					else
					{
						new PsUITutorialArrow("Tutorial", text, 0);
					}
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

	// Token: 0x06001196 RID: 4502 RVA: 0x000AAF08 File Offset: 0x000A9308
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

	// Token: 0x06001197 RID: 4503 RVA: 0x000AAF7C File Offset: 0x000A937C
	private void TweenEventHandler(TweenC _c)
	{
		this.Destroy();
	}

	// Token: 0x06001198 RID: 4504 RVA: 0x000AAF84 File Offset: 0x000A9384
	public override void Destroy()
	{
		TransformS.UnparentComponent(this.m_TC, true);
		EntityManager.RemoveEntity(this.m_rootTC.p_entity);
		base.Destroy();
	}

	// Token: 0x06001199 RID: 4505 RVA: 0x000AAFA8 File Offset: 0x000A93A8
	public void BubbleDrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] array = DebugDraw.GetBezierRect(_c.m_actualWidth, _c.m_actualHeight, 0.025f * (float)Screen.height, 10, Vector2.zero);
		array = DebugDraw.AddSpeechHandleToVectorArray(array, this.m_handlePosition, this.m_handleType);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -5f, array, (float)Screen.width * 0.005f, DebugDraw.HexToColor("#4dcfff"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -3f, array, (float)Screen.width * 0.008f, new Color(1f, 1f, 1f, 0.5f), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Gradient2Mat_Material), this.m_camera, Position.Inside, true);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.zero, array, DebugDraw.HexToUint("#FFFFFF"), DebugDraw.HexToUint("#FFFFFF"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
	}

	// Token: 0x04001486 RID: 5254
	private PsUIGenericButton m_ok;

	// Token: 0x04001487 RID: 5255
	private PsUIGenericButton m_cancel;

	// Token: 0x04001488 RID: 5256
	private Action m_proceedOk;

	// Token: 0x04001489 RID: 5257
	private Action m_proceedCancel;

	// Token: 0x0400148A RID: 5258
	private string m_text;

	// Token: 0x0400148B RID: 5259
	private string m_okText;

	// Token: 0x0400148C RID: 5260
	private string m_okUNLOCK;

	// Token: 0x0400148D RID: 5261
	private string m_okACTION;

	// Token: 0x0400148E RID: 5262
	private string m_cancelText;

	// Token: 0x0400148F RID: 5263
	private Vector2 m_rootPos;

	// Token: 0x04001490 RID: 5264
	private TransformC m_rootTC;

	// Token: 0x04001491 RID: 5265
	private bool m_exiting;

	// Token: 0x04001492 RID: 5266
	private SpeechBubbleHandlePosition m_handlePosition;

	// Token: 0x04001493 RID: 5267
	private SpeechBubbleHandleType m_handleType;
}
