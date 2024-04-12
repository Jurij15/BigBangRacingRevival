using System;

// Token: 0x020002BC RID: 700
public class PsUITopPauseRace : PsUITopPause
{
	// Token: 0x060014C8 RID: 5320 RVA: 0x000D9102 File Offset: 0x000D7502
	public PsUITopPauseRace(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x060014C9 RID: 5321 RVA: 0x000D910B File Offset: 0x000D750B
	public override void CreateLeftArea()
	{
		base.CreateLeftArea();
	}

	// Token: 0x060014CA RID: 5322 RVA: 0x000D9114 File Offset: 0x000D7514
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
