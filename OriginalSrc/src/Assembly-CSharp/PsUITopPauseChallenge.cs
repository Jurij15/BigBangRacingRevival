using System;

// Token: 0x020002BA RID: 698
public class PsUITopPauseChallenge : PsUITopPause
{
	// Token: 0x060014C4 RID: 5316 RVA: 0x000D8F19 File Offset: 0x000D7319
	public PsUITopPauseChallenge(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x060014C5 RID: 5317 RVA: 0x000D8F24 File Offset: 0x000D7324
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
}
