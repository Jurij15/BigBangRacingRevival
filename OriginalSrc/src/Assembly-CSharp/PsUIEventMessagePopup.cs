using System;
using UnityEngine;

// Token: 0x02000351 RID: 849
public class PsUIEventMessagePopup : PsUIHeaderedCanvas
{
	// Token: 0x060018D0 RID: 6352 RVA: 0x000EA7BC File Offset: 0x000E8BBC
	public PsUIEventMessagePopup(UIComponent _parent)
		: this(_parent, string.Empty, true, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
	{
	}

	// Token: 0x060018D1 RID: 6353 RVA: 0x000EA7D8 File Offset: 0x000E8BD8
	public PsUIEventMessagePopup(UIComponent _parent, string _tag, bool _hasCloseButton = true, float _headerHeight = 0.125f, RelativeTo _headerHeightRelativeTo = RelativeTo.ScreenHeight, float _footerHeight = 0f, RelativeTo _footerHeightRelativeTo = RelativeTo.ScreenHeight)
		: base(_parent, _tag, _hasCloseButton, _headerHeight, _headerHeightRelativeTo, _footerHeight, _footerHeightRelativeTo)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(this.BGDrawhandler));
		this.SetWidth(0.94f, RelativeTo.ScreenHeight);
		this.SetHeight(0.72f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.5f);
		this.SetMargins(0.0125f, 0.0125f, 0.0125f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.1f, 0.1f, 0.03f, 0.03f, RelativeTo.ScreenHeight);
	}

	// Token: 0x060018D2 RID: 6354 RVA: 0x000EA8B6 File Offset: 0x000E8CB6
	public virtual void SetEventMessage(EventMessage _msg, bool _newsPage = false)
	{
		this.m_eventMessage = _msg;
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x060018D3 RID: 6355 RVA: 0x000EA8D4 File Offset: 0x000E8CD4
	public virtual void CreateHeaderContent(UIComponent _parent)
	{
		UIFittedText uifittedText = new UIFittedText(_parent, false, string.Empty, this.m_eventMessage.header, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#95e5ff", null);
	}

	// Token: 0x060018D4 RID: 6356 RVA: 0x000EA908 File Offset: 0x000E8D08
	public void BGDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect((float)Screen.width * 1.5f, (float)Screen.height * 1.5f, Vector2.zero);
		Color black = Color.black;
		black.a = 0.65f;
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), this.m_camera);
	}

	// Token: 0x060018D5 RID: 6357 RVA: 0x000EA988 File Offset: 0x000E8D88
	public virtual void CreateContent(UIComponent _parent)
	{
		this.m_contentScroll = new UIScrollableCanvas(_parent, string.Empty);
		this.m_contentScroll.RemoveDrawHandler();
		this.m_contentScroll.DisableTouchAreas(true);
		this.m_contentArea = new UIVerticalList(this.m_contentScroll, string.Empty);
		this.m_contentArea.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_contentArea.SetMargins(0.04f, 0.04f, 0.04f, 0.04f, RelativeTo.ScreenHeight);
		this.m_contentArea.SetVerticalAlign(1f);
		this.m_contentArea.SetSpacing(0.03f, RelativeTo.ScreenHeight);
		this.m_contentArea.RemoveDrawHandler();
		if (this.m_eventMessage.liveOps != null)
		{
			if (!string.IsNullOrEmpty(this.m_eventMessage.liveOps.pictureUrl))
			{
				UICanvas uicanvas = new UICanvas(this.m_contentArea, false, string.Empty, null, string.Empty);
				uicanvas.SetHeight(0.45f, RelativeTo.ParentHeight);
				uicanvas.RemoveDrawHandler();
				if (this.m_eventMessage.liveOps.minigameId != null)
				{
					this.m_minigameid = this.m_eventMessage.liveOps.minigameId;
				}
				this.m_picture = new PsUIFittedUrlPicture(uicanvas, true, string.Empty, this.m_eventMessage.liveOps.pictureUrl);
			}
			else if (!string.IsNullOrEmpty(this.m_eventMessage.liveOps.minigameId))
			{
				UICanvas uicanvas2 = new UICanvas(this.m_contentArea, false, string.Empty, null, string.Empty);
				uicanvas2.SetHeight(0.45f, RelativeTo.ParentHeight);
				uicanvas2.RemoveDrawHandler();
				this.m_minigameid = this.m_eventMessage.liveOps.minigameId;
				PsUIScreenshot psUIScreenshot = new PsUIScreenshot(uicanvas2, true, string.Empty, Vector2.zero, null, false, true, 0.03f, false);
				psUIScreenshot.m_gameId = this.m_minigameid;
				psUIScreenshot.m_forceLoad = true;
				psUIScreenshot.LoadPicture();
				psUIScreenshot.SetDrawHandler(new UIDrawDelegate(PsUIScreenshot.BasicDrawHandler));
				psUIScreenshot.SetWidth(1.5f, RelativeTo.OwnHeight);
				this.m_picture = psUIScreenshot;
			}
		}
		UITextbox uitextbox = new UITextbox(this.m_contentArea, false, string.Empty, this.m_eventMessage.message, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, null, true, null);
		uitextbox.SetMargins(0.01f, RelativeTo.ScreenHeight);
		if (this.m_eventMessage.uris.Count > 0)
		{
			UIVerticalList uiverticalList = new UIVerticalList(this.m_contentArea, string.Empty);
			uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
			uiverticalList.SetSpacing(0.025f, RelativeTo.ScreenHeight);
			uiverticalList.RemoveDrawHandler();
			for (int i = 0; i < this.m_eventMessage.uris.Count; i++)
			{
				PsUILinkUrl psUILinkUrl = new PsUILinkUrl(uiverticalList, true, "url", this.m_eventMessage.uris[i], PsFontManager.GetFont(PsFonts.KGSecondChances), 0.025f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, "38a6ea", true, null);
				psUILinkUrl.SetMargins(0.01f, 0.01f, 0f, 0f, RelativeTo.ScreenHeight);
			}
		}
	}

	// Token: 0x060018D6 RID: 6358 RVA: 0x000EAC98 File Offset: 0x000E9098
	public override void Update()
	{
		base.Update();
		if (this.m_contentScroll != null && this.m_contentArea != null && this.m_contentScroll.m_actualHeight < this.m_contentArea.m_actualHeight)
		{
			this.m_contentScroll.EnableTouchAreas(true);
		}
	}

	// Token: 0x060018D7 RID: 6359 RVA: 0x000EACE8 File Offset: 0x000E90E8
	public override void Step()
	{
		if (this.m_minigameid != null && this.m_picture != null && this.m_picture.m_hit)
		{
			PsUICenterSearch.m_sharedLevel = this.m_minigameid;
			PsUITabbedCreate.m_selectedTab = 3;
			PsMainMenuState.ChangeToCreateState();
			(this.GetRoot() as PsUIBasePopup).CallAction("Continue");
			TouchAreaS.CancelAllTouches(null);
		}
		if (this.m_exitButton != null && this.m_exitButton.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Continue");
		}
		if (this.m_continue != null && this.m_continue.m_hit)
		{
			(this.GetRoot() as PsUIBasePopup).CallAction("Continue");
		}
		base.Step();
	}

	// Token: 0x04001B66 RID: 7014
	protected PsUIGenericButton m_continue;

	// Token: 0x04001B67 RID: 7015
	protected EventMessage m_eventMessage;

	// Token: 0x04001B68 RID: 7016
	private UIScrollableCanvas m_contentScroll;

	// Token: 0x04001B69 RID: 7017
	private UIVerticalList m_contentArea;

	// Token: 0x04001B6A RID: 7018
	private string m_minigameid;

	// Token: 0x04001B6B RID: 7019
	private UIComponent m_picture;
}
