using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003D3 RID: 979
public class PsUICenterRoulette : PsUIHeaderedCanvas
{
	// Token: 0x06001B96 RID: 7062 RVA: 0x00133CA8 File Offset: 0x001320A8
	public PsUICenterRoulette(UIComponent _parent)
		: base(_parent, string.Empty, true, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.m_container.CreateTouchAreas();
		this.SetSize();
		this.SetVerticalAlign(0.5f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetPopupBackground();
		this.SetHeader();
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header, false);
		TweenS.AddTransformTween(this.m_container.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, new Vector3(0.75f, 0.75f, 1f), Vector3.one, 0.75f, 0f, true);
	}

	// Token: 0x06001B97 RID: 7063 RVA: 0x00133D90 File Offset: 0x00132190
	public virtual void SetHeader()
	{
		this.m_header.SetDrawHandler(new UIDrawDelegate(this.RouletteHeaderDrawHandler));
		this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
	}

	// Token: 0x06001B98 RID: 7064 RVA: 0x00133DC9 File Offset: 0x001321C9
	public virtual void SetPopupBackground()
	{
		this.SetDrawHandler(new UIDrawDelegate(PsUICenterRoulette.RoulettePopupDrawHandler));
	}

	// Token: 0x06001B99 RID: 7065 RVA: 0x00133DEE File Offset: 0x001321EE
	protected virtual void SetSize()
	{
		this.SetWidth(0.67859995f, RelativeTo.ScreenHeight);
		this.SetHeight(0.72800004f, RelativeTo.ScreenHeight);
	}

	// Token: 0x06001B9A RID: 7066 RVA: 0x00133E08 File Offset: 0x00132208
	public void CreateHeaderContent(UIComponent _parent, bool _prizeName = false)
	{
		this.CreateHeaderStars(_parent, false);
		string text = this.GetHeaderText();
		if (_prizeName)
		{
			text = this.GetPrizeText();
		}
		UIVerticalList uiverticalList = new UIVerticalList(_parent, string.Empty);
		uiverticalList.SetSpacing(0.001f, RelativeTo.ScreenHeight);
		uiverticalList.SetWidth(0.575f, RelativeTo.ParentWidth);
		uiverticalList.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uiverticalList, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FFFFFF", null);
	}

	// Token: 0x06001B9B RID: 7067 RVA: 0x00133E78 File Offset: 0x00132278
	private void CreateHeaderStars(UIComponent _parent, bool _fullLength = false)
	{
		this.m_starUnlitFrame = PsState.m_uiSheet.m_atlas.GetFrame(this.m_starUnlit, null);
		this.m_starLitFrame = PsState.m_uiSheet.m_atlas.GetFrame(this.m_starLit, null);
		if (this.m_headerStarList != null && this.m_headerStarList.Count > 0)
		{
			for (int i = 0; i < this.m_headerStarList.Count; i++)
			{
				this.m_headerStarList[i].Destroy();
			}
		}
		this.m_headerStarList = new List<UIFittedSprite>();
		int num = 12;
		int num2 = 4;
		if (_fullLength)
		{
			num2 = num;
		}
		float num3 = 0.05f;
		float num4 = (1f - num3 * 2f) / (float)(num - 1);
		for (int j = 0; j < num2; j++)
		{
			float num5;
			if (j < num2 / 2)
			{
				num5 = (float)j * num4 + num3;
			}
			else
			{
				num5 = 1f - (float)(num2 - 1 - j) * num4 - num3;
			}
			UIFittedSprite uifittedSprite = new UIFittedSprite(_parent, false, string.Empty, PsState.m_uiSheet, this.m_starLitFrame, true, true);
			uifittedSprite.SetHeight(0.45f, RelativeTo.ParentHeight);
			uifittedSprite.SetHorizontalAlign(num5);
			this.m_headerStarList.Add(uifittedSprite);
		}
	}

	// Token: 0x06001B9C RID: 7068 RVA: 0x00133FC0 File Offset: 0x001323C0
	public virtual string GetHeaderText()
	{
		return "ROULETTE";
	}

	// Token: 0x06001B9D RID: 7069 RVA: 0x00133FC7 File Offset: 0x001323C7
	public virtual string GetPrizeText()
	{
		Debug.LogError("Override");
		return "Prize won";
	}

	// Token: 0x06001B9E RID: 7070 RVA: 0x00133FD8 File Offset: 0x001323D8
	public virtual void CreateContent(UIComponent _parent)
	{
		_parent.RemoveTouchAreas();
		this.CreateRouletteContent(_parent);
	}

	// Token: 0x06001B9F RID: 7071 RVA: 0x00133FE8 File Offset: 0x001323E8
	public void CreateContinueButton()
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(0.5f, 0f);
		uihorizontalList.SetMargins(0f, 0f, 0.06f, -0.06f, RelativeTo.ScreenHeight);
		this.m_openButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_openButton.SetDepthOffset(-5f);
		this.m_openButton.SetText(this.GetContinueButtonString(), 0.05f, 0.2f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_openButton.SetGreenColors(true);
		uihorizontalList.Update();
		TweenS.AddTransformTween(this.m_openButton.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, new Vector3(0f, 0f, 1f), Vector3.one, 0.6f, 0f, true);
	}

	// Token: 0x06001BA0 RID: 7072 RVA: 0x001340CA File Offset: 0x001324CA
	protected virtual string GetContinueButtonString()
	{
		return PsStrings.Get(StringID.CONTINUE);
	}

	// Token: 0x06001BA1 RID: 7073 RVA: 0x001340D4 File Offset: 0x001324D4
	public void CreateRouletteContent(UIComponent _parent)
	{
		this.m_contentBaseList = new UIVerticalList(_parent, string.Empty);
		this.m_contentBaseList.SetMargins(0f, 0f, 0f, 0.015f, RelativeTo.ScreenShortest);
		this.m_contentBaseList.SetSpacing(0f, RelativeTo.ScreenShortest);
		this.m_contentBaseList.RemoveDrawHandler();
		this.m_contentBaseList.SetVerticalAlign(1f);
		UIVerticalList uiverticalList = new UIVerticalList(this.m_contentBaseList, string.Empty);
		uiverticalList.SetDrawHandler(new UIDrawDelegate(this.RouletteDrawHandler));
		this.m_spinnerCanvas = this.GetSpinner(uiverticalList);
		this.m_spinnerButtonCanvas = new UICanvas(this.m_contentBaseList, false, "spinnerButtonParent", null, string.Empty);
		this.m_spinnerButtonCanvas.RemoveDrawHandler();
		this.m_spinnerButtonCanvas.SetHeight(0.6f, RelativeTo.ParentHeight);
		this.m_startRoulette = this.CreateSpinButton(this.m_spinnerButtonCanvas);
	}

	// Token: 0x06001BA2 RID: 7074 RVA: 0x001341BC File Offset: 0x001325BC
	protected virtual PsUIGenericButton CreateSpinButton(UIComponent _parent)
	{
		PsUIGenericButton psUIGenericButton = new PsUIGenericButton(this.m_spinnerButtonCanvas, 0.25f, 0.25f, 0.005f, "Button");
		psUIGenericButton.SetFittedText(PsStrings.Get(StringID.BUTTON_SPIN), 0.08f, 0.3325f, RelativeTo.ScreenHeight, true);
		psUIGenericButton.SetGreenColors(true);
		return psUIGenericButton;
	}

	// Token: 0x06001BA3 RID: 7075 RVA: 0x0013420A File Offset: 0x0013260A
	public override void Destroy()
	{
		this.m_closingPopup = true;
		base.Destroy();
	}

	// Token: 0x06001BA4 RID: 7076 RVA: 0x0013421C File Offset: 0x0013261C
	public override void Step()
	{
		if (this.m_startRoulette != null && this.m_startRoulette.m_hit)
		{
			this.StartRoulette();
		}
		if (this.m_openButton != null && this.m_openButton.m_hit)
		{
			this.m_closingPopup = true;
			(this.GetRoot() as PsUIBasePopup).CallAction("Confirm");
		}
		if (this.m_exitButton != null && this.m_exitButton.m_hit)
		{
			this.m_closingPopup = true;
		}
		base.Step();
		if (this.m_animationOn)
		{
			float needleValue = this.m_spinnerCanvas.GetNeedleValue();
			if (this.m_needle != null)
			{
				this.m_needle.SetNeedleAngle(needleValue, this.m_spinnerCanvas.m_hitCenter, this.m_spinnerCanvas.m_tween.currentValue.x * this.m_spinnerCanvas.m_startTween.currentValue.x);
			}
			this.AnimateStarsBasic(this.m_headerStarList, 5);
		}
		else
		{
			this.AnimateStarsBasic(this.m_headerStarList, 30);
		}
	}

	// Token: 0x06001BA5 RID: 7077 RVA: 0x00134330 File Offset: 0x00132730
	protected virtual void AnimateStarsBasic(List<UIFittedSprite> _list, int _interval)
	{
		if (this.m_closingPopup || !this.m_TC.p_entity.m_active)
		{
			return;
		}
		if (Main.m_gameTicks % _interval == 0)
		{
			this.m_evens = !this.m_evens;
			for (int i = 0; i < _list.Count; i++)
			{
				if (this.m_evens)
				{
					if (i % 2 == 0)
					{
						_list[i].SetFrame(this.m_starLitFrame);
					}
					else
					{
						_list[i].SetFrame(this.m_starUnlitFrame);
					}
				}
				else if (i % 2 == 1)
				{
					_list[i].SetFrame(this.m_starLitFrame);
				}
				else
				{
					_list[i].SetFrame(this.m_starUnlitFrame);
				}
				_list[i].Update();
			}
			return;
		}
	}

	// Token: 0x06001BA6 RID: 7078 RVA: 0x00134418 File Offset: 0x00132818
	protected virtual void StartRoulette()
	{
		this.m_animationOn = true;
		this.m_spinnerButtonCanvas.Destroy();
		this.m_spinnerCanvas.StartAnimation(new Action(this.SpinnerAnimationDone));
		this.CreateNeedle();
		this.m_header.DestroyChildren();
		this.m_headerStarList = null;
		this.CreateHeaderStars(this.m_header, true);
		this.m_header.UpdateChildren();
	}

	// Token: 0x06001BA7 RID: 7079 RVA: 0x00134480 File Offset: 0x00132880
	protected virtual void CreateNeedle()
	{
		this.m_needle = new PsUICenterRoulette.PsUIRouletteNeedle(this.m_contentBaseList);
		this.m_needle.SetHeight(0.5f, RelativeTo.ParentHeight);
		this.m_contentBaseList.Update();
		TweenS.AddTransformTween(this.m_needle.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.zero, Vector3.one, 0.3f, 0f, true);
	}

	// Token: 0x06001BA8 RID: 7080 RVA: 0x001344E4 File Offset: 0x001328E4
	protected virtual void SpinnerAnimationDone()
	{
		if (this.m_needle != null)
		{
			this.m_needle.Destroy();
			this.m_needle = null;
		}
		this.m_animationOn = false;
		this.CreateContinueButton();
		this.CreatePrizeInformation(this.m_contentBaseList);
		this.m_header.DestroyChildren();
		this.m_headerStarList = null;
		this.CreateHeaderContent(this.m_header, true);
		this.m_header.UpdateChildren();
	}

	// Token: 0x06001BA9 RID: 7081 RVA: 0x00134551 File Offset: 0x00132951
	public virtual void CreatePrizeInformation(UIComponent _parent)
	{
		Debug.LogError("Override!");
	}

	// Token: 0x06001BAA RID: 7082 RVA: 0x0013455D File Offset: 0x0013295D
	public virtual PsUI3DCanvasGacha GetSpinner(UIComponent _parent)
	{
		Debug.LogError("Override!");
		return null;
	}

	// Token: 0x06001BAB RID: 7083 RVA: 0x0013456C File Offset: 0x0013296C
	public void RouletteDrawHandler(UIComponent _c)
	{
		float actualWidth = _c.m_actualWidth;
		float actualHeight = _c.m_actualHeight;
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Camera camera = CameraS.m_uiCamera;
		if (_c.m_camera != null)
		{
			camera = _c.m_camera;
		}
		else if (_c.m_parent != null)
		{
			camera = _c.m_parent.m_camera;
		}
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_roulette_slot_bg", null);
		frame.width -= 1f;
		frame.x += 1f;
		Frame frame2 = PsState.m_uiSheet.m_atlas.GetFrame("menu_roulette_slot_bg", null);
		frame2.width -= 1f;
		frame2.x += 1f;
		frame2.flipX = true;
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC, _c.m_actualWidth / 2f, _c.m_actualHeight);
		SpriteS.SetOffset(spriteC, new Vector3(_c.m_actualWidth / 2f / 2f, 0f, 0f), 0f);
		SpriteC spriteC2 = SpriteS.AddComponent(_c.m_TC, frame2, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC2, _c.m_actualWidth / 2f, _c.m_actualHeight);
		SpriteS.SetOffset(spriteC2, new Vector3(-_c.m_actualWidth / 2f / 2f, 0f, 0f), 0f);
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, camera, true, null);
		Vector2[] line = DebugDraw.GetLine(new Vector2(_c.m_actualWidth * -0.5f, 0f), new Vector2(_c.m_actualWidth * 0.5f, 0f), 0);
		Vector3 vector;
		vector..ctor(0f, -_c.m_actualHeight / 2f, 0f);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, vector, line, (float)Screen.height * 0.0075f, ToolBox.HexToColor(this.m_drawHandlerLineColor), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, false);
	}

	// Token: 0x06001BAC RID: 7084 RVA: 0x001347A0 File Offset: 0x00132BA0
	public static void RoulettePopupDrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedBezierRect = DebugDraw.GetRoundedBezierRect(_c.m_actualWidth, _c.m_actualHeight, (float)Screen.height * 0.05f, -1f, -1f, 0.2f, true, true, true, true);
		Vector2[] roundedBezierRect2 = DebugDraw.GetRoundedBezierRect(_c.m_actualWidth - (float)Screen.height * 0.02f, _c.m_actualHeight - (float)Screen.height * 0.02f, (float)Screen.height * 0.045f, -1f, -1f, 0.2f, true, true, true, true);
		string text = "#217eb3";
		string text2 = "#165882";
		string text3 = "#86d9f9";
		string text4 = "#1fb3e9";
		string text5 = "#3c90be";
		string text6 = "#114d71";
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 10f, roundedBezierRect, DebugDraw.HexToUint(text2), DebugDraw.HexToUint(text), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 4f, roundedBezierRect, (float)Screen.height * 0.0075f, DebugDraw.HexToColor(text6), DebugDraw.HexToColor(text5), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 7f + Vector3.down * 2f, roundedBezierRect, (float)Screen.height * 0.02f, DebugDraw.HexToColor("#0a3c5a"), DebugDraw.HexToColor("#0f689f"), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		Color gray = Color.gray;
		uint num = DebugDraw.ColorToUInt(gray);
		uint num2 = DebugDraw.HexToUint("474747");
		Material material = ResourceManager.GetMaterial(RESOURCE.RouletteBackgroundMaterial_Material);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 8f, roundedBezierRect2, num2, num, material, _c.m_camera, "BG", null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 0f, roundedBezierRect2, (float)Screen.height * 0.0075f, DebugDraw.HexToColor(text4), DebugDraw.HexToColor(text3), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 1f, roundedBezierRect2, (float)Screen.height * 0.025f, new Color(1f, 1f, 1f, 0.25f), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Gradient2Mat_Material), _c.m_camera, Position.Inside, true);
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x06001BAD RID: 7085 RVA: 0x00134A50 File Offset: 0x00132E50
	public void RouletteHeaderDrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		string drawHandlerLineColor = this.m_drawHandlerLineColor;
		string text = "#2C6A8C";
		Vector2[] roundedBezierRect = DebugDraw.GetRoundedBezierRect(_c.m_actualWidth - (float)Screen.height * 0.02f, _c.m_actualHeight - (float)Screen.height * 0.01f, (float)Screen.height * 0.045f, -1f, 0f, 0f, true, true, false, false);
		GGData ggdata = new GGData(roundedBezierRect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.up * (float)(-(float)Screen.height) * 0.01f + Vector3.forward * 6f, ggdata, DebugDraw.HexToColor(text), DebugDraw.HexToColor(text), ResourceManager.GetMaterial(RESOURCE.MenuPopupBackgroundMat_Material), _c.m_camera);
		Vector2[] line = DebugDraw.GetLine(new Vector2(_c.m_actualWidth * -0.5f + (float)Screen.height * 0.01f, _c.m_actualHeight * -0.55f), new Vector2(_c.m_actualWidth * 0.5f - (float)Screen.height * 0.01f, _c.m_actualHeight * -0.55f), 0);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -2f, line, (float)Screen.height * 0.0075f, DebugDraw.HexToColor(drawHandlerLineColor), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, false);
	}

	// Token: 0x04001DFA RID: 7674
	private string m_drawHandlerLineColor = "E3F7FF";

	// Token: 0x04001DFB RID: 7675
	protected UIVerticalList m_contentBaseList;

	// Token: 0x04001DFC RID: 7676
	protected PsUIGenericButton m_openButton;

	// Token: 0x04001DFD RID: 7677
	private PsUIGenericButton m_startRoulette;

	// Token: 0x04001DFE RID: 7678
	private PsUI3DCanvasGacha m_spinnerCanvas;

	// Token: 0x04001DFF RID: 7679
	private UICanvas m_spinnerButtonCanvas;

	// Token: 0x04001E00 RID: 7680
	private string m_starUnlit = "menu_roulette_star_unlit";

	// Token: 0x04001E01 RID: 7681
	private string m_starLit = "menu_roulette_star_lit";

	// Token: 0x04001E02 RID: 7682
	private Frame m_starUnlitFrame;

	// Token: 0x04001E03 RID: 7683
	private Frame m_starLitFrame;

	// Token: 0x04001E04 RID: 7684
	private List<UIFittedSprite> m_headerStarList = new List<UIFittedSprite>();

	// Token: 0x04001E05 RID: 7685
	private bool m_animationOn;

	// Token: 0x04001E06 RID: 7686
	private bool m_closingPopup;

	// Token: 0x04001E07 RID: 7687
	private PsUICenterRoulette.PsUIRouletteNeedle m_needle;

	// Token: 0x04001E08 RID: 7688
	private bool m_evens = true;

	// Token: 0x020003D4 RID: 980
	private class PsUIRouletteNeedle : UICanvas
	{
		// Token: 0x06001BAE RID: 7086 RVA: 0x00134BC4 File Offset: 0x00132FC4
		public PsUIRouletteNeedle(UIComponent _parent)
			: base(_parent, false, string.Empty, null, string.Empty)
		{
			this.SetMargins(0f, 0f, 0.15f, 0f, RelativeTo.ParentHeight);
			this.RemoveDrawHandler();
			this.m_pointerParent = new UIComponent(this, false, string.Empty, null, null, string.Empty);
			this.m_pointerParent.RemoveDrawHandler();
			this.m_pointerParent.SetSize(0f, 0f, RelativeTo.ParentHeight);
			this.m_pointerParent.SetMargins(0f, 0f, -0.15f, 0.15f, RelativeTo.ParentHeight);
			UIFittedSprite uifittedSprite = new UIFittedSprite(this.m_pointerParent, false, "pointer", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_roulette_pointer", null), true, true);
			uifittedSprite.SetSize(0.2f, 0.2f, RelativeTo.ScreenHeight);
			UIFittedSprite uifittedSprite2 = new UIFittedSprite(this, false, "pointer", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_roulette_pointer_pin", null), true, true);
			uifittedSprite2.SetSize(0.25f, 0.25f, RelativeTo.ParentHeight);
			uifittedSprite2.SetDepthOffset(-5f);
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x00134CF0 File Offset: 0x001330F0
		public void SetNeedleAngle(float _value, bool _hitCenter, float _pos)
		{
			if (_hitCenter)
			{
				if (this.m_needleAngle < 20f)
				{
					this.m_needleVel += ToolBox.limitBetween((1f - _pos) * 70f, 8f, 40f);
				}
				SoundS.PlaySingleShotWithParameter("/Metagame/SpinWheel", Vector3.zero, "SpinStep", 1f, 1f);
			}
			this.m_needleVel += 0f - this.m_needleAngle * 0.12f;
			this.m_needleAngle += this.m_needleVel;
			this.m_needleVel *= 0.77f;
			this.m_pointerParent.m_TC.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, this.m_needleAngle));
		}

		// Token: 0x04001E0A RID: 7690
		private UIComponent m_pointerParent;

		// Token: 0x04001E0B RID: 7691
		private float m_oldValue;

		// Token: 0x04001E0C RID: 7692
		private float m_translationAmount = 40f;

		// Token: 0x04001E0D RID: 7693
		private float m_needleAngle;

		// Token: 0x04001E0E RID: 7694
		private float m_needleVel;
	}
}
