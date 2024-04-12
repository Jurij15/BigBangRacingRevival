using System;
using UnityEngine;

// Token: 0x0200036C RID: 876
public class PsUICenterTeamLocked : PsUIHeaderedCanvas
{
	// Token: 0x06001978 RID: 6520 RVA: 0x00116BFC File Offset: 0x00114FFC
	public PsUICenterTeamLocked(UIComponent _parent)
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

	// Token: 0x06001979 RID: 6521 RVA: 0x00116CF8 File Offset: 0x001150F8
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIFittedText uifittedText = new UIFittedText(_parent, false, string.Empty, PsStrings.Get(StringID.TEAM_LOCKED_INFO_HEADER).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#95e5ff", null);
	}

	// Token: 0x0600197A RID: 6522 RVA: 0x00116D30 File Offset: 0x00115130
	public void BGDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect((float)Screen.width * 1.5f, (float)Screen.height * 1.5f, Vector2.zero);
		Color black = Color.black;
		black.a = 0.65f;
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), this.m_camera);
	}

	// Token: 0x0600197B RID: 6523 RVA: 0x00116DB0 File Offset: 0x001151B0
	public void CreateContent(UIComponent _parent)
	{
		UIVerticalList uiverticalList = new UIVerticalList(_parent, string.Empty);
		uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList.SetSpacing(0.04f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		_parent.RemoveTouchAreas();
		string text = PsStrings.Get(StringID.TEAM_LOCKED_INFO_TEXT);
		text = text.Replace("%1", PsMetagameData.GetLeague(2).m_name);
		text = text.Replace("%2", PsMetagameData.GetLeague(2).m_trophyLimit + "+ ");
		UITextbox uitextbox = new UITextbox(uiverticalList, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, null, true, null);
		uitextbox.SetMargins(0.05f, RelativeTo.ScreenHeight);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetAlign(0.5f, 0f);
		uihorizontalList.SetMargins(0f, 0f, 0.07f, -0.07f, RelativeTo.ScreenHeight);
		this.m_ok = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_ok.SetAlign(1f, 1f);
		this.m_ok.SetGreenColors(true);
		this.m_ok.SetText(PsStrings.Get(StringID.OK), 0.04f, 0f, RelativeTo.ScreenHeight, true, RelativeTo.ScreenShortest);
	}

	// Token: 0x0600197C RID: 6524 RVA: 0x00116F00 File Offset: 0x00115300
	public override void Step()
	{
		if (this.m_ok != null && this.m_ok.m_TC.p_entity != null && this.m_ok.m_TC.p_entity.m_active && (this.m_ok.m_hit || Main.AndroidBackButtonPressed((this.GetRoot() as PsUIBasePopup).m_guid)))
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		base.Step();
	}

	// Token: 0x04001BFE RID: 7166
	private PsUIGenericButton m_ok;
}
