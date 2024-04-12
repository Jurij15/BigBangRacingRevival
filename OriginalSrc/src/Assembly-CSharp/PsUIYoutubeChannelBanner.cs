using System;
using UnityEngine;

// Token: 0x0200031E RID: 798
public class PsUIYoutubeChannelBanner : UICanvas
{
	// Token: 0x06001777 RID: 6007 RVA: 0x000FF25C File Offset: 0x000FD65C
	public PsUIYoutubeChannelBanner(UIComponent _parent, string _name, string _id, int _subscribers, bool _selected)
		: base(_parent, true, string.Empty, null, string.Empty)
	{
		this.m_name = _name;
		this.m_id = _id;
		this.m_selected = _selected;
		this.m_subscriberCount = _subscribers;
		this.m_TAC.m_letTouchesThrough = true;
		this.SetWidth(1f, RelativeTo.ParentWidth);
		this.SetHeight(0.085f, RelativeTo.ScreenHeight);
		this.SetMargins(0.02f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(this.BannerDrawhandler));
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList.SetMargins(0.08f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
		uiverticalList.SetHorizontalAlign(0f);
		uiverticalList.RemoveDrawHandler();
		UIVerticalList uiverticalList2 = uiverticalList;
		bool flag = false;
		string empty = string.Empty;
		string name = this.m_name;
		string font = PsFontManager.GetFont(PsFonts.KGSecondChances);
		float num = 0.0385f;
		RelativeTo relativeTo = RelativeTo.ScreenHeight;
		bool flag2 = false;
		string text = ((!this.m_selected) ? "000000" : "#E23D2F");
		this.m_nameBox = new UITextbox(uiverticalList2, flag, empty, name, font, num, relativeTo, flag2, Align.Left, Align.Top, text, true, null);
		this.m_nameBox.SetMaxRows(1);
		this.m_nameBox.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_nameBox.UseDotsWhenWrapping(true);
		this.m_nameBox.SetHorizontalAlign(0f);
		this.m_nameBox.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
		UICanvas uicanvas = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(0.0385f, RelativeTo.ScreenHeight);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetHorizontalAlign(0f);
		uicanvas.SetMargins(0f, 0f, 0.0075f, 0.0075f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, _subscribers + " subscribers", PsFontManager.GetFont(PsFonts.KGSecondChances), true, "79746c", null);
		uifittedText.SetHorizontalAlign(0f);
		UIFittedSprite uifittedSprite = new UIFittedSprite(this, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_youtube", null), true, true);
		uifittedSprite.SetHeight(1f, RelativeTo.ParentHeight);
		uifittedSprite.SetHorizontalAlign(0f);
	}

	// Token: 0x06001778 RID: 6008 RVA: 0x000FF4B8 File Offset: 0x000FD8B8
	public void BannerDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, 0.01f * (float)Screen.height, 6, Vector2.zero);
		Vector2[] roundedRect2 = DebugDraw.GetRoundedRect(_c.m_actualWidth - 0.05f * _c.m_actualHeight, _c.m_actualHeight * 0.15f, _c.m_actualHeight * 0.075f, 6, Vector2.up * _c.m_actualHeight * 0.42f);
		GGData ggdata = new GGData(roundedRect);
		GGData ggdata2 = new GGData(roundedRect2);
		Color color = DebugDraw.HexToColor((!this.m_selected) ? "#85EFFF" : "#F3EAD9");
		Color color2 = DebugDraw.HexToColor((!this.m_selected) ? "#0E9EF6" : "#F5EBDB");
		Color color3 = DebugDraw.HexToColor((!this.m_selected) ? "#85EFFF" : "#F3EAD9");
		Color color4 = DebugDraw.HexToColor((!this.m_selected) ? "#0E9EF6" : "#F5EBDB");
		Color black = Color.black;
		black.a = 0.8f;
		Color white = Color.white;
		white.a = 0.35f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 3f, roundedRect, 0.01f * (float)Screen.height, color2, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 4f, ggdata, color4, color3, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 5f + Vector3.down * 0.0035f * (float)Screen.height, roundedRect, (float)Screen.height * 0.02f, black, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8GradientMat_Material), _c.m_camera, Position.Center, true);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 2f, ggdata2, white, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_button_shine", null);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC, new Vector3(-_c.m_actualWidth * 0.5f + 0.125f * _c.m_actualHeight, _c.m_actualHeight * 0.42f, 1f), 40f);
		SpriteS.SetDimensions(spriteC, _c.m_actualHeight * 0.2f, _c.m_actualHeight * 0.135f);
		SpriteC spriteC2 = SpriteS.AddComponent(_c.m_TC, frame, PsState.m_uiSheet);
		SpriteS.SetOffset(spriteC2, new Vector3(_c.m_actualWidth * 0.5f - 0.125f * _c.m_actualHeight, -_c.m_actualHeight * 0.4f, 1f), 205f);
		SpriteS.SetDimensions(spriteC2, _c.m_actualHeight * 0.15f, _c.m_actualHeight * 0.1f);
		SpriteS.SetColor(spriteC2, new Color(1f, 1f, 1f, 0.6f));
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x06001779 RID: 6009 RVA: 0x000FF820 File Offset: 0x000FDC20
	public void Select(bool _value)
	{
		this.m_selected = _value;
		this.m_nameBox.SetColor((!this.m_selected) ? "000000" : "#E23D2F", null);
		this.Update();
	}

	// Token: 0x0600177A RID: 6010 RVA: 0x000FF858 File Offset: 0x000FDC58
	protected override void OnTouchRollIn(TLTouch _touch, bool _secondary)
	{
		base.OnTouchRollIn(_touch, _secondary);
		if (this.m_touchScaleTween != null)
		{
			TweenS.RemoveComponent(this.m_touchScaleTween);
			this.m_touchScaleTween = null;
		}
		this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1.05f, 1.05f, 1.05f), 0.1f, 0f, false);
	}

	// Token: 0x0600177B RID: 6011 RVA: 0x000FF8C0 File Offset: 0x000FDCC0
	protected override void OnTouchBegan(TLTouch _touch)
	{
		base.OnTouchBegan(_touch);
		if (this.m_touchScaleTween != null)
		{
			TweenS.RemoveComponent(this.m_touchScaleTween);
			this.m_touchScaleTween = null;
		}
		this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1.05f, 1.05f, 1.05f), 0.1f, 0f, false);
	}

	// Token: 0x0600177C RID: 6012 RVA: 0x000FF924 File Offset: 0x000FDD24
	protected override void OnTouchRollOut(TLTouch _touch, bool _secondary)
	{
		base.OnTouchRollOut(_touch, _secondary);
		if (this.m_touchScaleTween != null)
		{
			TweenS.RemoveComponent(this.m_touchScaleTween);
			this.m_touchScaleTween = null;
		}
		this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1f, 1f, 1f), 0.1f, 0f, false);
	}

	// Token: 0x0600177D RID: 6013 RVA: 0x000FF98C File Offset: 0x000FDD8C
	protected override void OnTouchRelease(TLTouch _touch, bool _inside)
	{
		base.OnTouchRelease(_touch, _inside);
		if (this.m_touchScaleTween != null)
		{
			TweenS.RemoveComponent(this.m_touchScaleTween);
			this.m_touchScaleTween = null;
		}
		this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1f, 1f, 1f), 0.1f, 0f, false);
		if (_inside && this.m_hit)
		{
			SoundS.PlaySingleShot("/UI/ButtonNormal", Vector3.zero, 1f);
		}
	}

	// Token: 0x04001A42 RID: 6722
	public string m_name;

	// Token: 0x04001A43 RID: 6723
	public string m_id;

	// Token: 0x04001A44 RID: 6724
	public int m_subscriberCount;

	// Token: 0x04001A45 RID: 6725
	public bool m_selected;

	// Token: 0x04001A46 RID: 6726
	private UITextbox m_nameBox;

	// Token: 0x04001A47 RID: 6727
	public TweenC m_touchScaleTween;
}
