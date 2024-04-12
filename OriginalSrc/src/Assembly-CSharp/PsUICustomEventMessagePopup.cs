using System;
using UnityEngine;

// Token: 0x02000349 RID: 841
public class PsUICustomEventMessagePopup : PsUIEventMessagePopup
{
	// Token: 0x060018A6 RID: 6310 RVA: 0x0010C02C File Offset: 0x0010A42C
	public PsUICustomEventMessagePopup(UIComponent _parent)
		: base(_parent, string.Empty, true, 0.1f, RelativeTo.ScreenHeight, 0.065f, RelativeTo.ScreenHeight)
	{
		PsMetrics.LoveUsPopupShown();
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(base.BGDrawhandler));
		this.SetWidth(0.78f, RelativeTo.ScreenHeight);
		this.SetHeight(0.875f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.04f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.07f, 0.07f, 0.03f, 0.03f, RelativeTo.ScreenHeight);
	}

	// Token: 0x060018A7 RID: 6311 RVA: 0x0010C118 File Offset: 0x0010A518
	public override void CreateHeaderContent(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.SetSpacing(0.015f, RelativeTo.ScreenHeight);
		uihorizontalList.SetDepthOffset(-5f);
		uihorizontalList.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_heart", null), true, true);
		uifittedSprite.SetHeight(1f, RelativeTo.ParentHeight);
		uifittedSprite.SetOverrideShader(Shader.Find("WOE/Unlit/ColorUnlitTransparent"));
		uifittedSprite.SetColor(DebugDraw.HexToColor("#ee483c"));
		UIFittedText uifittedText = new UIFittedText(uihorizontalList, false, string.Empty, this.m_eventMessage.header, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#ffffff", null);
		UIFittedSprite uifittedSprite2 = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_heart", null), true, true);
		uifittedSprite2.SetHeight(1f, RelativeTo.ParentHeight);
		uifittedSprite2.SetOverrideShader(Shader.Find("WOE/Unlit/ColorUnlitTransparent"));
		uifittedSprite2.SetColor(DebugDraw.HexToColor("#ee483c"));
	}

	// Token: 0x060018A8 RID: 6312 RVA: 0x0010C220 File Offset: 0x0010A620
	public override void CreateContent(UIComponent _parent)
	{
		string[] array = this.m_eventMessage.message.Split(new char[] { '%' });
		UIVerticalList uiverticalList = new UIVerticalList(_parent, string.Empty);
		uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList.SetMargins(0f, 0f, 0.005f, 0f, RelativeTo.ScreenHeight);
		uiverticalList.SetVerticalAlign(1f);
		uiverticalList.RemoveDrawHandler();
		if (array.Length >= 1)
		{
			UITextbox uitextbox = new UITextbox(uiverticalList, false, string.Empty, array[0], PsFontManager.GetFont(PsFonts.KGSecondChances), 0.024f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, null, true, null);
			uitextbox.SetMargins(0.03f, RelativeTo.ScreenHeight);
		}
		UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		Material material = new Material(Shader.Find("Framework/VertexColorUnlitDouble"));
		material.mainTexture = ResourceManager.GetTexture(RESOURCE.menu_best_of_2016_illustration_Texture2D);
		UISprite uisprite = new UISprite(uihorizontalList, false, string.Empty, material, true);
		uisprite.SetSize(1f, (float)uisprite.m_textureMaterial.mainTexture.height / (float)uisprite.m_textureMaterial.mainTexture.width, RelativeTo.ParentWidth);
		uisprite.SetDepthOffset(5f);
		if (array.Length > 1)
		{
			UITextbox uitextbox2 = new UITextbox(uiverticalList, false, string.Empty, array[1], PsFontManager.GetFont(PsFonts.KGSecondChances), 0.024f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, null, true, null);
			uitextbox2.SetMargins(0.03f, RelativeTo.ScreenHeight);
		}
		new PsUILoveUs(_parent).SetVerticalAlign(0f);
	}

	// Token: 0x060018A9 RID: 6313 RVA: 0x0010C390 File Offset: 0x0010A790
	public override void Destroy()
	{
		PsMetrics.LoveUsPopupClosed();
		(this.GetRoot() as PsUIBasePopup).CallAction("Claim");
		base.Destroy();
	}
}
