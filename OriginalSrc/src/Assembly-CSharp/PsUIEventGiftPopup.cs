using System;
using UnityEngine;

// Token: 0x02000350 RID: 848
public class PsUIEventGiftPopup : PsUIEventMessagePopup
{
	// Token: 0x060018CD RID: 6349 RVA: 0x0010E731 File Offset: 0x0010CB31
	public PsUIEventGiftPopup(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x060018CE RID: 6350 RVA: 0x0010E748 File Offset: 0x0010CB48
	public override void CreateContent(UIComponent _parent)
	{
		float num = 0.55f;
		string message = this.m_eventMessage.message;
		string header = this.m_eventMessage.header;
		string label = this.m_eventMessage.label;
		string name = this.m_eventMessage.giftContent.GetName();
		_parent.SetMargins(0.05f, RelativeTo.OwnWidth);
		UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas.SetWidth(1f - num, RelativeTo.ParentWidth);
		uicanvas.SetHorizontalAlign(0f);
		uicanvas.SetHeight(0.618f, RelativeTo.ParentHeight);
		uicanvas.SetVerticalAlign(1f);
		uicanvas.RemoveDrawHandler();
		UIComponent uicomponent = new UIComponent(uicanvas, false, "shine base", null, null, string.Empty);
		uicomponent.SetDepthOffset(4f);
		uicomponent.RemoveDrawHandler();
		uicomponent.SetMargins(-0.1f, -0.1f, -0.1f, -0.1f, RelativeTo.ParentHeight);
		uicomponent.RemoveDrawHandler();
		uicomponent.SetDepthOffset(10f);
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicomponent, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_shine", null), true, true);
		uifittedSprite.SetOverrideShader(Shader.Find("Framework/VertexColorUnlitDouble"));
		Color white = Color.white;
		white.a = 0.5f;
		uifittedSprite.SetColor(white);
		TweenC tweenC = TweenS.AddTransformTween(uifittedSprite.m_TC, TweenedProperty.Rotation, TweenStyle.Linear, new Vector3(0f, 0f, -360f), 25f, 0f, false);
		TweenS.SetAdditionalTweenProperties(tweenC, -1, false, TweenStyle.Linear);
		UIComponent uicomponent2 = new UIComponent(uicanvas, false, string.Empty, null, null, string.Empty);
		uicomponent2.SetWidth(0.6f, RelativeTo.ParentWidth);
		uicomponent2.SetHeight(0.7f, RelativeTo.ParentHeight);
		uicomponent2.RemoveDrawHandler();
		this.m_eventMessage.giftContent.CreateUI(uicomponent2);
		UIFittedSprite uifittedSprite2 = new UIFittedSprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_hat_shadow", null), true, true);
		uifittedSprite2.SetSize(0.5f, 0.5f, RelativeTo.ParentHeight);
		uifittedSprite2.SetVerticalAlign(0.15f);
		UICanvas uicanvas2 = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas2.SetWidth(num, RelativeTo.ParentWidth);
		uicanvas2.SetHorizontalAlign(1f);
		uicanvas2.SetHeight(0.618f, RelativeTo.ParentHeight);
		uicanvas2.SetVerticalAlign(1f);
		uicanvas2.RemoveDrawHandler();
		UIVerticalList uiverticalList = new UIVerticalList(uicanvas2, string.Empty);
		uiverticalList.SetSpacing(-0.0225f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		UITextbox uitextbox = new UITextbox(uiverticalList, false, string.Empty, label, PsFontManager.GetFont(PsFonts.HurmeBold), 0.0465f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, null, true, "#000000");
		uitextbox.SetMargins(0.01f, RelativeTo.ScreenHeight);
		UITextbox uitextbox2 = new UITextbox(uiverticalList, false, string.Empty, name, PsFontManager.GetFont(PsFonts.HurmeBold), 0.0615f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, "#73F102", true, "#000000");
		uitextbox2.SetMargins(0.01f, RelativeTo.ScreenHeight);
		UICanvas uicanvas3 = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas3.SetHeight(0.38200003f, RelativeTo.ParentHeight);
		uicanvas3.SetVerticalAlign(0f);
		uicanvas3.RemoveDrawHandler();
		UITextbox uitextbox3 = new UITextbox(uicanvas3, false, string.Empty, message, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, null, true, "#000000");
		uitextbox3.SetVerticalAlign(0.65f);
		uitextbox3.SetMargins(0.01f, RelativeTo.ScreenHeight);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(0.5f, 0f);
		uihorizontalList.SetMargins(0f, 0f, 0.09f, -0.09f, RelativeTo.ScreenHeight);
		this.m_continue = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_continue.SetAlign(1f, 1f);
		this.m_continue.SetMargins(0.02f, 0.03f, 0.02f, 0.02f, RelativeTo.ScreenHeight);
		this.m_continue.SetGreenColors(true);
		this.m_continue.SetText(PsStrings.Get(StringID.BUTTON_CLAIM_GIFT), 0.055f, 0f, RelativeTo.ScreenHeight, true, RelativeTo.ScreenShortest);
	}

	// Token: 0x060018CF RID: 6351 RVA: 0x0010EB78 File Offset: 0x0010CF78
	public virtual void CreateBorders()
	{
		float num = 0.525f;
		UIComponent uicomponent = new UIComponent(this.m_container, false, string.Empty, null, null, string.Empty);
		uicomponent.SetMargins(-num, num, 0f, 0f, RelativeTo.ParentWidth);
		uicomponent.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicomponent, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(this.m_borderName, null), true, true);
		uifittedSprite.SetDepthOffset(21f);
		uicomponent = new UIComponent(this.m_container, false, string.Empty, null, null, string.Empty);
		uicomponent.SetMargins(num, -num, 0f, 0f, RelativeTo.ParentWidth);
		uicomponent.RemoveDrawHandler();
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame(this.m_borderName, null);
		frame.flipX = true;
		frame.flipY = true;
		UIFittedSprite uifittedSprite2 = new UIFittedSprite(uicomponent, false, string.Empty, PsState.m_uiSheet, frame, true, true);
		uifittedSprite2.SetDepthOffset(21f);
		num = 0.435f;
		uicomponent = new UIComponent(this.m_container, false, string.Empty, null, null, string.Empty);
		uicomponent.SetMargins(0f, 0f, -num, num, RelativeTo.ParentWidth);
		uicomponent.RemoveDrawHandler();
		Frame frame2 = PsState.m_uiSheet.m_atlas.GetFrame(this.m_borderName, null);
		UIFittedSprite uifittedSprite3 = new UIFittedSprite(uicomponent, false, string.Empty, PsState.m_uiSheet, frame2, true, true);
		TransformS.Rotate(uifittedSprite3.m_TC, new Vector3(0f, 0f, -90f));
		uifittedSprite3.SetDepthOffset(21f);
	}

	// Token: 0x04001B65 RID: 7013
	private string m_borderName = "menu_holiday_border";
}
