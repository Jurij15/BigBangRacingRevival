using System;
using UnityEngine;

// Token: 0x02000374 RID: 884
public class PsUICenterTeamUp : UICanvas
{
	// Token: 0x0600199D RID: 6557 RVA: 0x00119134 File Offset: 0x00117534
	public PsUICenterTeamUp(UIComponent _parent)
		: base(_parent, false, string.Empty, null, string.Empty)
	{
		PsUITabbedTeam.m_selectedTab = 1;
		(this.m_parent as UIScrollableCanvas).m_maxScrollInertialY = 0f;
		(this.m_parent as UIScrollableCanvas).SetScrollPosition(0f, 0f);
		this.SetWidth(1f, RelativeTo.ParentWidth);
		this.SetHeight(1f, RelativeTo.ParentHeight);
		this.SetMargins(0.15f, 0.15f, 0.05f, 0.05f, RelativeTo.ScreenHeight);
		this.RemoveDrawHandler();
		UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(0.15f, RelativeTo.ScreenHeight);
		uicanvas.SetWidth(0.4f, RelativeTo.ScreenWidth);
		uicanvas.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.DarkBlueBGDrawhandler));
		uicanvas.SetVerticalAlign(1f);
		uicanvas.SetMargins(0.05f, 0.05f, 0.02f, 0.02f, RelativeTo.ScreenHeight);
		string text = PsStrings.Get(StringID.TEAM_INFO_FREECOINS);
		UITextbox uitextbox = new UITextbox(uicanvas, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.025f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, "#82E0FE", true, null);
		UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
		uicanvas2.SetAlign(0f, 1f);
		uicanvas2.SetSize(0.05f, 0.05f, RelativeTo.ScreenHeight);
		uicanvas2.SetMargins(-0.07f, 0.07f, -0.04f, 0.04f, RelativeTo.ScreenHeight);
		uicanvas2.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_info_button", null), true, true);
		uifittedSprite.SetHeight(1f, RelativeTo.ParentHeight);
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.SetVerticalAlign(1f);
		uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList.SetMargins(0f, 0f, 0.175f, 0.03f, RelativeTo.ScreenHeight);
		uiverticalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		UIText uitext = new UIText(uiverticalList, false, string.Empty, PsStrings.Get(StringID.TEAM_HEADER_SUGGESTIONS), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.045f, RelativeTo.ScreenHeight, null, null);
		uitext.SetHorizontalAlign(0f);
		PsUITeamArea psUITeamArea = new PsUITeamArea(uiverticalList, false, true, false, null, false, null);
		this.m_create = new PsUIGenericButton(uiverticalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_create.SetGreenColors(true);
		this.m_create.SetFittedText(PsStrings.Get(StringID.TEAM_BUTTON_CREATE_TEAM), 0.03f, 0.285f, RelativeTo.ScreenHeight, true);
		this.m_create.SetHeight(0.1f, RelativeTo.ScreenHeight);
		this.m_create.SetMargins(0.05f, 0.01f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		this.m_create.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_create, string.Empty);
		uihorizontalList.SetSpacing(0.015f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		UIFittedSprite uifittedSprite2 = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_resources_coin_price", null), true, true);
		uifittedSprite2.SetHeight(0.035f, RelativeTo.ScreenHeight);
		UIText uitext2 = new UIText(uihorizontalList, false, string.Empty, "1000", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, "#FEE43A", "#1f6500");
	}

	// Token: 0x0600199E RID: 6558 RVA: 0x001194A4 File Offset: 0x001178A4
	public override void Step()
	{
		if (this.m_create.m_hit)
		{
			this.m_popup = new PsUIBasePopup(typeof(PsUICenterCreateNewTeam), null, null, null, true, true, InitialPage.Center, false, false, false);
			this.m_popup.SetAction("Exit", delegate
			{
				this.m_popup.Destroy();
				this.m_popup = null;
			});
			this.m_popup.SetAction("Create", delegate
			{
				PsMainMenuState.ChangeToTeamState();
			});
			TweenS.AddTransformTween(this.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
		}
		base.Step();
	}

	// Token: 0x0600199F RID: 6559 RVA: 0x0011956C File Offset: 0x0011796C
	public override void Destroy()
	{
		if (this.m_popup != null)
		{
			this.m_popup.Destroy();
		}
		this.m_popup = null;
		(this.m_parent as UIScrollableCanvas).m_maxScrollInertialY = 50f / (1024f / (float)Screen.width);
		base.Destroy();
	}

	// Token: 0x04001C16 RID: 7190
	private PsUIGenericButton m_create;

	// Token: 0x04001C17 RID: 7191
	private PsUIBasePopup m_popup;
}
