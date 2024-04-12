using System;
using UnityEngine;

// Token: 0x02000367 RID: 871
public class PsUICenterLeaveTeam : PsUIHeaderedCanvas
{
	// Token: 0x0600193F RID: 6463 RVA: 0x00112440 File Offset: 0x00110840
	public PsUICenterLeaveTeam(UIComponent _parent)
		: base(_parent, string.Empty, true, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(this.BGDrawhandler));
		this.SetWidth(0.55f, RelativeTo.ScreenWidth);
		this.SetHeight(0.5f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0.0125f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.085f, 0.085f, 0.03f, 0.03f, RelativeTo.ScreenHeight);
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x06001940 RID: 6464 RVA: 0x0011253C File Offset: 0x0011093C
	public void CreateHeaderContent(UIComponent _parent)
	{
		UIFittedText uifittedText = new UIFittedText(_parent, false, string.Empty, PsStrings.Get(StringID.TEAM_LEAVE_HEADER), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#95e5ff", null);
	}

	// Token: 0x06001941 RID: 6465 RVA: 0x00112570 File Offset: 0x00110970
	public void BGDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect((float)Screen.width * 1.5f, (float)Screen.height * 1.5f, Vector2.zero);
		Color black = Color.black;
		black.a = 0.65f;
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), this.m_camera);
	}

	// Token: 0x06001942 RID: 6466 RVA: 0x001125F0 File Offset: 0x001109F0
	public void CreateContent(UIComponent _parent)
	{
		string text = PsStrings.Get(StringID.TEAM_LEAVE_CONFIRMATION);
		text = text.Replace("%1", "<color=#fde349>" + PlayerPrefsX.GetTeamName() + "</color>");
		UITextbox uitextbox = new UITextbox(_parent, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, null, true, null);
		uitextbox.SetMargins(0.05f, RelativeTo.ScreenHeight);
		uitextbox.SetVerticalAlign(1f);
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.SetVerticalAlign(0f);
		uihorizontalList.SetMargins(0f, 0f, 0.07f, -0.07f, RelativeTo.ScreenHeight);
		uihorizontalList.SetSpacing(0.08f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		this.m_yes = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_yes.SetGreenColors(true);
		this.m_yes.SetFittedText(PsStrings.Get(StringID.YES), 0.04f, 0.2f, RelativeTo.ScreenHeight, false);
		this.m_yes.SetHeight(0.08f, RelativeTo.ScreenHeight);
		this.m_no = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_no.SetRedColors();
		this.m_no.SetFittedText(PsStrings.Get(StringID.CANCEL), 0.05f, 0.2f, RelativeTo.ScreenHeight, false);
		this.m_no.SetHeight(0.1f, RelativeTo.ScreenHeight);
	}

	// Token: 0x06001943 RID: 6467 RVA: 0x00112758 File Offset: 0x00110B58
	public override void Step()
	{
		if (this.m_no.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		else if (this.m_yes.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Proceed");
		}
		base.Step();
	}

	// Token: 0x04001BD2 RID: 7122
	private PsUIGenericButton m_yes;

	// Token: 0x04001BD3 RID: 7123
	private PsUIGenericButton m_no;
}
