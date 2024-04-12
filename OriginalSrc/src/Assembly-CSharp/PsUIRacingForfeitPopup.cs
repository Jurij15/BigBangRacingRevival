using System;
using UnityEngine;

// Token: 0x020003CF RID: 975
public class PsUIRacingForfeitPopup : PsUIHeaderedCanvas
{
	// Token: 0x06001B8A RID: 7050 RVA: 0x00133640 File Offset: 0x00131A40
	public PsUIRacingForfeitPopup(UIComponent _parent)
		: base(_parent, string.Empty, false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.SetWidth(0.65f, RelativeTo.ScreenWidth);
		this.SetHeight(0.45f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.0125f, 0.0125f, 0.0125f, 0f, RelativeTo.ScreenHeight);
		this.Initialize();
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x06001B8B RID: 7051 RVA: 0x00133728 File Offset: 0x00131B28
	public void Initialize()
	{
		int num = (PsState.m_activeGameLoop as PsGameLoopRacing).m_purchasedRuns + 6 - (PsState.m_activeGameLoop as PsGameLoopRacing).m_heatNumber;
		int num2 = Mathf.Abs((PsState.m_activeGameLoop.m_gameMode as PsGameModeRacing).m_trophyGhost.trophyLoss);
		this.m_headerText = PsStrings.Get(StringID.TROPHY_WARNING_TITLE).ToUpper();
		if (num == 1)
		{
			this.m_contentText = PsStrings.Get(StringID.TROPHY_WARNING_SINGLE);
		}
		else
		{
			string text = PsStrings.Get(StringID.TROPHY_WARNING_MULTIPLE);
			text = text.Replace("%1", num + string.Empty);
			this.m_contentText = text;
		}
	}

	// Token: 0x06001B8C RID: 7052 RVA: 0x001337D8 File Offset: 0x00131BD8
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0.025f, 0.025f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0.5f);
		UIText uitext = new UIText(uihorizontalList, false, string.Empty, this.m_headerText, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.055f, RelativeTo.ScreenHeight, "#95e5ff", null);
	}

	// Token: 0x06001B8D RID: 7053 RVA: 0x00133850 File Offset: 0x00131C50
	public void CreateContent(UIComponent _parent)
	{
		_parent.RemoveTouchAreas();
		UITextbox uitextbox = new UITextbox(_parent, false, string.Empty, this.m_contentText, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, false, Align.Center, Align.Middle, null, true, null);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(0.5f, 0f);
		uihorizontalList.SetSpacing(0.1f, RelativeTo.ScreenHeight);
		uihorizontalList.SetMargins(0f, 0f, 0.075f, -0.075f, RelativeTo.ScreenHeight);
		this.m_cancel = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_cancel.SetText(PsStrings.Get(StringID.CANCEL), 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_cancel.SetBlueColors(true);
		this.m_ok = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_ok.SetAlign(1f, 1f);
		this.m_ok.SetText(PsStrings.Get(StringID.RACE_END), 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_ok.SetRedColors();
	}

	// Token: 0x06001B8E RID: 7054 RVA: 0x0013397C File Offset: 0x00131D7C
	public override void Step()
	{
		if (this.m_ok != null && this.m_ok.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Forfeit");
		}
		else if (this.m_cancel != null && this.m_cancel.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		base.Step();
	}

	// Token: 0x04001DEB RID: 7659
	private PsUIGenericButton m_ok;

	// Token: 0x04001DEC RID: 7660
	private PsUIGenericButton m_cancel;

	// Token: 0x04001DED RID: 7661
	public VersusMetaData m_versusData;

	// Token: 0x04001DEE RID: 7662
	private string m_headerText;

	// Token: 0x04001DEF RID: 7663
	private string m_contentText;

	// Token: 0x04001DF0 RID: 7664
	private bool m_initialized;
}
