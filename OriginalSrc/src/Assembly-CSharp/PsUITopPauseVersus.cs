using System;

// Token: 0x020002BD RID: 701
public class PsUITopPauseVersus : PsUITopPause
{
	// Token: 0x060014CB RID: 5323 RVA: 0x000D91FE File Offset: 0x000D75FE
	public PsUITopPauseVersus(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x060014CC RID: 5324 RVA: 0x000D9208 File Offset: 0x000D7608
	public override void CreateMiddleArea()
	{
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.SetSpacing(0.03f, RelativeTo.ScreenHeight);
		uiverticalList.SetAlign(0.5f, 1f);
		uiverticalList.RemoveDrawHandler();
		new PsUITopBanner(uiverticalList);
		UIVerticalList uiverticalList2 = new UIVerticalList(uiverticalList, string.Empty);
		uiverticalList2.SetWidth(0.55f, RelativeTo.ScreenHeight);
		uiverticalList2.SetMargins(0.15f, -0.15f, 0f, 0f, RelativeTo.ScreenHeight);
		uiverticalList2.RemoveDrawHandler();
		UIVerticalList uiverticalList3 = new UIVerticalList(uiverticalList2, string.Empty);
		uiverticalList3.SetWidth(0.55f, RelativeTo.ScreenHeight);
		uiverticalList3.SetMargins(0.025f, RelativeTo.ScreenHeight);
		uiverticalList3.SetDrawHandler(new UIDrawDelegate(base.DescriptionDrawHandler));
		UITextbox uitextbox = new UITextbox(uiverticalList3, false, string.Empty, PsState.m_activeGameLoop.GetDescription(), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, "343b2e", true, null);
		uitextbox.SetWidth(1f, RelativeTo.ParentWidth);
	}

	// Token: 0x060014CD RID: 5325 RVA: 0x000D92F2 File Offset: 0x000D76F2
	public override void Step()
	{
		base.Step();
	}
}
