using System;
using UnityEngine;

// Token: 0x02000366 RID: 870
public class PsUICenterKickedFromTeam : PsUIHeaderedCanvas
{
	// Token: 0x0600193A RID: 6458 RVA: 0x00112090 File Offset: 0x00110490
	public PsUICenterKickedFromTeam(UIComponent _parent)
		: base(_parent, string.Empty, false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(this.BGDrawhandler));
		this.SetWidth(0.5f, RelativeTo.ScreenWidth);
		this.SetHeight(0.5f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.05f, 0.05f, 0.03f, 0.03f, RelativeTo.ScreenHeight);
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x0600193B RID: 6459 RVA: 0x0011218C File Offset: 0x0011058C
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIFittedText uifittedText = new UIFittedText(_parent, false, string.Empty, PsStrings.Get(StringID.KICK_NOTIFY_HEADER).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#95e5ff", null);
	}

	// Token: 0x0600193C RID: 6460 RVA: 0x001121C4 File Offset: 0x001105C4
	public void BGDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect((float)Screen.width * 1.5f, (float)Screen.height * 1.5f, Vector2.zero);
		Color black = Color.black;
		black.a = 0.65f;
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), this.m_camera);
	}

	// Token: 0x0600193D RID: 6461 RVA: 0x00112244 File Offset: 0x00110644
	public void CreateContent(UIComponent _parent)
	{
		UIVerticalList uiverticalList = new UIVerticalList(_parent, string.Empty);
		uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList.SetMargins(0f, 0f, 0.02f, 0.02f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		_parent.RemoveTouchAreas();
		string text = PsStrings.Get(StringID.KICK_REASON_INFO);
		text = text.Replace("%kickReason%", PsMetagameManager.m_playerStats.m_teamKickReason);
		UITextbox uitextbox = new UITextbox(uiverticalList, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0275f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, null, true, null);
		uitextbox.SetMargins(0.05f, RelativeTo.ScreenHeight);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(0.5f, 0f);
		uihorizontalList.SetMargins(0f, 0f, 0.07f, -0.07f, RelativeTo.ScreenHeight);
		this.m_continue = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_continue.SetAlign(1f, 1f);
		this.m_continue.SetMargins(0.02f, 0.03f, 0.02f, 0.02f, RelativeTo.ScreenHeight);
		this.m_continue.SetGreenColors(true);
		this.m_continue.SetText(PsStrings.Get(StringID.CONTINUE), 0.04f, 0f, RelativeTo.ScreenHeight, true, RelativeTo.ScreenShortest);
	}

	// Token: 0x0600193E RID: 6462 RVA: 0x0011239C File Offset: 0x0011079C
	public override void Step()
	{
		if (this.m_continue.m_hit)
		{
			PsMetagameManager.ClaimTeamKick(PsMetagameManager.m_playerStats.m_teamKickReason, new Action<HttpC>(PsMetagameManager.ClaimTeamKickSUCCEED), new Action<HttpC>(PsMetagameManager.ClaimTeamKickFAILED), null);
			PsMetagameManager.m_playerStats.m_teamKickReason = null;
			PsMetagameManager.m_team = null;
			PlayerPrefsX.SetTeamId(null);
			PlayerPrefsX.SetTeamName(null);
			(this.GetRoot() as PsUIBasePopup).CallAction("Continue");
		}
		base.Step();
	}

	// Token: 0x04001BCD RID: 7117
	private PsUIGenericButton m_continue;
}
