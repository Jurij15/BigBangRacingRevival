using System;

// Token: 0x020002E6 RID: 742
public class PsUICenterWaitForHighscoresAndGhost : UICanvas
{
	// Token: 0x060015FA RID: 5626 RVA: 0x000E68A0 File Offset: 0x000E4CA0
	public PsUICenterWaitForHighscoresAndGhost(UIComponent _parent)
		: base(_parent, false, "CenterContent", null, string.Empty)
	{
		this.SetWidth(1f, RelativeTo.ScreenWidth);
		this.SetHeight(1f, RelativeTo.ParentHeight);
		this.SetVerticalAlign(0f);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.DebriefBackground));
		UIVerticalList uiverticalList = new UIVerticalList(this, "Center");
		uiverticalList.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uiverticalList.RemoveDrawHandler();
		UIText uitext = new UIText(uiverticalList, false, string.Empty, "Resetting Track", PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.03f, RelativeTo.ScreenHeight, null, null);
		new PsUILoadingAnimation(uiverticalList, false);
	}

	// Token: 0x060015FB RID: 5627 RVA: 0x000E694C File Offset: 0x000E4D4C
	public override void Step()
	{
		base.Step();
		if (!PsState.m_activeGameLoop.m_gameMode.m_waitForHighscoreAndGhost && !PsState.m_activeGameLoop.m_gameMode.m_waitForNextGhost)
		{
			PsState.m_activeGameLoop.m_gameMode.DestroyGhostLoaderEntity();
			(this.GetRoot() as PsUIBasePopup).CallAction("Done");
			this.DestroyRoot();
		}
	}
}
