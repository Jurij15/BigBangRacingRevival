using System;
using System.Collections.Generic;
using Server;
using UnityEngine;

// Token: 0x0200030B RID: 779
public class PsUICenterYoutubeLink : PsUIHeaderedCanvas
{
	// Token: 0x06001706 RID: 5894 RVA: 0x000F78A8 File Offset: 0x000F5CA8
	public PsUICenterYoutubeLink(UIComponent _parent)
		: base(_parent, string.Empty, true, 0.125f, RelativeTo.ScreenHeight, 0.065f, RelativeTo.ScreenHeight)
	{
		this.m_banners = new List<PsUIYoutubeChannelBanner>();
		this.m_searchInputId = string.Empty;
		this.m_searchInput = string.Empty;
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(this.BGDrawhandler));
		this.SetWidth(0.7f, RelativeTo.ScreenWidth);
		this.SetHeight(0.7f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0.012f, 0.06f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.05f, 0.05f, 0.03f, 0.03f, RelativeTo.ScreenHeight);
		this.m_footer.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIFooter));
		this.m_footer.SetMargins(0.06f, 0.06f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		this.CreateContent(this);
		this.CreateHeaderContent(this.m_header);
	}

	// Token: 0x06001707 RID: 5895 RVA: 0x000F7A0C File Offset: 0x000F5E0C
	public void CreateHeaderContent(UIComponent _parent)
	{
		UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetMargins(0.05f, 0.05f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		string text = PsStrings.Get(StringID.LINK_HEADER);
		text = text.Replace("%1", "YouTube");
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#95e5ff", null);
	}

	// Token: 0x06001708 RID: 5896 RVA: 0x000F7A9C File Offset: 0x000F5E9C
	public void BGDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect((float)Screen.width * 1.5f, (float)Screen.height * 1.5f, Vector2.zero);
		Color black = Color.black;
		black.a = 0.65f;
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), this.m_camera);
	}

	// Token: 0x06001709 RID: 5897 RVA: 0x000F7B1C File Offset: 0x000F5F1C
	public void CreateContent(UIComponent _parent)
	{
		this.m_content = new UIScrollableCanvas(_parent, string.Empty);
		this.m_content.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_content.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_content.RemoveDrawHandler();
		this.m_content.m_passTouchesToScrollableParents = true;
		this.m_contentArea = new UIVerticalList(this.m_content, string.Empty);
		this.m_contentArea.SetMargins(0f, 0f, 0f, 0f, RelativeTo.ParentWidth);
		this.m_contentArea.SetSpacing(0.04f, RelativeTo.ScreenHeight);
		this.m_contentArea.SetMargins(0.05f, RelativeTo.ScreenHeight);
		this.m_contentArea.RemoveDrawHandler();
		string text = PsStrings.Get(StringID.LINK_TEXT_LARGE);
		text = text.Replace("%1", "Youtube");
		UITextbox uitextbox = new UITextbox(this.m_contentArea, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0275f, RelativeTo.ScreenHeight, true, Align.Center, Align.Top, null, true, null);
		UITextbox uitextbox2 = new UITextbox(this.m_contentArea, false, string.Empty, PsStrings.Get(StringID.LINK_PROFILE), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0275f, RelativeTo.ScreenHeight, true, Align.Center, Align.Top, null, true, null);
		UIVerticalList uiverticalList = new UIVerticalList(this.m_contentArea, string.Empty);
		uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList.SetSpacing(0.005f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		text = PsStrings.Get(StringID.LINK_USERNAME);
		text = text.Replace("%1", "YouTube");
		UIText uitext = new UIText(uiverticalList, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.023f, RelativeTo.ScreenHeight, "95e5ff", null);
		uitext.SetHorizontalAlign(0f);
		this.m_field = new PsUIPlainTextField(uiverticalList);
		this.m_field.SetMinMaxCharacterCount(1, 100);
		this.m_field.SetCallbacks(delegate(string search)
		{
			this.InputDone(search, false);
		}, null);
		this.m_field.SetText(this.m_searchInput);
		this.m_field.m_textField.m_TAC.m_letTouchesThrough = true;
		uitext = new UIText(uiverticalList, false, string.Empty, PsStrings.Get(StringID.YOUTUBE_ID), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.023f, RelativeTo.ScreenHeight, "95e5ff", null);
		uitext.SetHorizontalAlign(0f);
		this.m_fieldId = new PsUIPlainTextField(uiverticalList);
		this.m_fieldId.SetMinMaxCharacterCount(1, 100);
		this.m_fieldId.SetCallbacks(delegate(string search)
		{
			this.InputDone(search, true);
		}, null);
		this.m_fieldId.SetText(this.m_searchInputId);
		this.m_fieldId.m_textField.m_TAC.m_letTouchesThrough = true;
		if (!string.IsNullOrEmpty(PlayerPrefsX.GetYoutubeId()) && !string.IsNullOrEmpty(PlayerPrefsX.GetYoutubeName()))
		{
			UIText uitext2 = new UIText(this.m_contentArea, false, string.Empty, PsStrings.Get(StringID.LINK_CURRENT_CHANNEL), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0265f, RelativeTo.ScreenHeight, "#84D8F7", null);
			this.m_current = new PsUIYoutubeChannelBanner(this.m_contentArea, PlayerPrefsX.GetYoutubeName(), PlayerPrefsX.GetYoutubeId(), PsMetagameManager.m_playerStats.youtubeSubscriberCount, true);
			this.m_selected = this.m_current;
			this.CreateUnselect(this.m_selected);
		}
		UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas.RemoveDrawHandler();
		uicanvas.SetSize(0.05f, 0.05f, RelativeTo.ScreenHeight);
		uicanvas.SetAlign(0.5f, 0f);
		uicanvas.SetMargins(0f, 0f, 0.08f, -0.08f, RelativeTo.ScreenHeight);
		this.m_done = new PsUIGenericButton(uicanvas, 0.25f, 0.25f, 0.005f, "Button");
		this.m_done.SetGreenColors(true);
		this.m_done.SetTextWithMinWidth(PsStrings.Get(StringID.FACEBOOK_BUTTON_DONE), 0.04f, 0.2f, RelativeTo.ScreenHeight, true, RelativeTo.ScreenShortest);
		this.m_done.SetAlign(0.5f, 1f);
	}

	// Token: 0x0600170A RID: 5898 RVA: 0x000F7EF0 File Offset: 0x000F62F0
	private void InputDone(string _input, bool _id = false)
	{
		if (_id)
		{
			this.m_searchInputId = _input;
			this.m_fieldId.SetText(this.m_searchInputId);
			this.m_fieldId.Update();
			this.m_searchInput = string.Empty;
			this.m_field.SetText(string.Empty);
		}
		else
		{
			this.m_searchInput = _input;
			this.m_field.SetText(this.m_searchInput);
			this.m_field.Update();
			this.m_searchInputId = string.Empty;
			this.m_fieldId.SetText(string.Empty);
		}
		this.m_created = false;
		this.m_searched = false;
		this.m_contentArea.DestroyChildren((this.m_current == null) ? 3 : 5);
		if (this.m_selected != null && this.m_selected != this.m_current)
		{
			this.m_unselectHolder = null;
			this.m_selected = null;
			this.m_unselect = null;
		}
		this.m_banners.Clear();
		if (this.m_current != null)
		{
			this.m_current.Select(true);
			this.m_selected = this.m_current;
			this.CreateUnselect(this.m_selected);
		}
		new PsUILoadingAnimation(this.m_contentArea, false);
		if (_id)
		{
			PsMetagameManager.GetYoutubeChannelById(this.m_searchInputId, new Action<List<YoutubeChannelInfo>>(this.YoutubeGetSUCCEED), new Action<HttpC>(this.YoutubeGetFAILED), null);
		}
		else
		{
			PsMetagameManager.GetYoutubeChannels(this.m_searchInput, new Action<List<YoutubeChannelInfo>>(this.YoutubeGetSUCCEED), new Action<HttpC>(this.YoutubeGetFAILED), null);
		}
		this.Update();
	}

	// Token: 0x0600170B RID: 5899 RVA: 0x000F8082 File Offset: 0x000F6482
	private void YoutubeGetSUCCEED(List<YoutubeChannelInfo> _channels)
	{
		Debug.Log("YOUTUBE GET SUCCEED", null);
		this.m_channels = _channels;
		this.m_searched = true;
	}

	// Token: 0x0600170C RID: 5900 RVA: 0x000F80A0 File Offset: 0x000F64A0
	private void YoutubeGetFAILED(HttpC _c)
	{
		Debug.Log("YOUTUBE GET FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerGetYoutubeChannels((string)_c.objectData, new Action<List<YoutubeChannelInfo>>(this.YoutubeGetSUCCEED), new Action<HttpC>(this.YoutubeGetFAILED), null), null);
	}

	// Token: 0x0600170D RID: 5901 RVA: 0x000F80F4 File Offset: 0x000F64F4
	private void YoutubeGetByIdFAILED(HttpC _c)
	{
		Debug.Log("YOUTUBE GET FAILED", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => PsServerRequest.ServerGetYoutubeChannelById((string)_c.objectData, new Action<List<YoutubeChannelInfo>>(this.YoutubeGetSUCCEED), new Action<HttpC>(this.YoutubeGetFAILED), null), null);
	}

	// Token: 0x0600170E RID: 5902 RVA: 0x000F8148 File Offset: 0x000F6548
	public void CreateChannelList()
	{
		this.m_contentArea.DestroyChildren((this.m_current == null) ? 3 : 5);
		if (this.m_channels.Count > 0)
		{
			new UIText(this.m_contentArea, false, string.Empty, PsStrings.Get(StringID.LINK_CHANNELS_FOUND), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0265f, RelativeTo.ScreenHeight, "#84D8F7", null);
			for (int i = 0; i < this.m_channels.Count; i++)
			{
				PsUIYoutubeChannelBanner psUIYoutubeChannelBanner = new PsUIYoutubeChannelBanner(this.m_contentArea, this.m_channels[i].title, this.m_channels[i].id, this.m_channels[i].subscribers, false);
				this.m_banners.Add(psUIYoutubeChannelBanner);
			}
		}
		else
		{
			new UIText(this.m_contentArea, false, string.Empty, PsStrings.Get(StringID.LINK_CHANNEL_NOT_FOUND), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.04f, RelativeTo.ScreenHeight, "fffff", "313131");
		}
		this.Update();
		if (this.m_channels.Count > 0)
		{
			float num = this.m_banners[this.m_banners.Count - 1].m_TC.transform.position.y - this.m_banners[this.m_banners.Count - 1].m_actualHeight * 0.6f;
			float num2 = this.m_content.m_scrollTC.transform.position.y - this.m_content.m_actualHeight * 0.5f;
			Vector2 vector = this.m_content.m_scrollTC.transform.position + new Vector2(0f, num - num2);
			this.m_content.ScrollToPosition(vector, null);
		}
		this.m_created = true;
	}

	// Token: 0x0600170F RID: 5903 RVA: 0x000F8340 File Offset: 0x000F6740
	public void CreateUnselect(UIComponent _parent)
	{
		if (this.m_unselectHolder != null)
		{
			this.m_unselectHolder.Destroy();
		}
		this.m_unselectHolder = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		this.m_unselectHolder.SetSize(1f, 1f, RelativeTo.ParentHeight);
		this.m_unselectHolder.SetHorizontalAlign(1f);
		this.m_unselectHolder.SetMargins(0.5f, -0.5f, 0f, 0f, RelativeTo.OwnHeight);
		this.m_unselectHolder.RemoveDrawHandler();
		this.m_unselect = new PsUIGenericButton(this.m_unselectHolder, 0.25f, 0.25f, 0.005f, "Button");
		this.m_unselect.SetRedColors();
		this.m_unselect.SetHorizontalAlign(0f);
		this.m_unselect.SetIcon("menu_icon_close", 0.05f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_unselectHolder.Update();
	}

	// Token: 0x06001710 RID: 5904 RVA: 0x000F843C File Offset: 0x000F683C
	public override void Step()
	{
		if (!this.m_created && this.m_searched)
		{
			this.CreateChannelList();
		}
		if (this.m_unselect != null && this.m_unselect.m_hit)
		{
			this.m_selected.Select(false);
			this.m_selected = null;
			this.m_unselectHolder.Destroy();
			this.m_unselectHolder = null;
			this.m_unselect = null;
		}
		if (this.m_current != null && this.m_current.m_hit && !this.m_current.m_selected)
		{
			if (this.m_selected != null)
			{
				this.m_selected.Select(false);
			}
			this.m_current.Select(true);
			this.m_selected = this.m_current;
			this.CreateUnselect(this.m_selected);
		}
		else
		{
			for (int i = 0; i < this.m_banners.Count; i++)
			{
				if (this.m_banners[i].m_hit && !this.m_banners[i].m_selected)
				{
					if (this.m_selected != null)
					{
						this.m_selected.Select(false);
					}
					this.m_banners[i].Select(true);
					this.m_selected = this.m_banners[i];
					this.CreateUnselect(this.m_selected);
					break;
				}
			}
		}
		if (this.m_done.m_hit)
		{
			if (this.m_selected != this.m_current)
			{
				if (this.m_selected != null)
				{
					PlayerPrefsX.SetYoutubeId(this.m_selected.m_id);
					PlayerPrefsX.SetYoutubeName(this.m_selected.m_name);
					PsMetagameManager.m_playerStats.youtubeSubscriberCount = this.m_selected.m_subscriberCount;
				}
				else
				{
					PlayerPrefsX.SetYoutubeId(string.Empty);
					PlayerPrefsX.SetYoutubeName(string.Empty);
					PsMetagameManager.m_playerStats.youtubeSubscriberCount = 0;
				}
				PsMetagameManager.ChangeYoutuber(PlayerPrefsX.GetYoutubeName(), PlayerPrefsX.GetYoutubeId(), PsMetagameManager.m_playerStats.youtubeSubscriberCount, new Action<HttpC>(PsMetagameManager.ChangeYoutuberSUCCEED), new Action<HttpC>(PsMetagameManager.ChangeYoutuberFAILED), null);
			}
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		base.Step();
	}

	// Token: 0x040019C9 RID: 6601
	private PsUIPlainTextField m_field;

	// Token: 0x040019CA RID: 6602
	private PsUIPlainTextField m_fieldId;

	// Token: 0x040019CB RID: 6603
	private PsUIGenericButton m_done;

	// Token: 0x040019CC RID: 6604
	private UIVerticalList m_contentArea;

	// Token: 0x040019CD RID: 6605
	private List<YoutubeChannelInfo> m_channels;

	// Token: 0x040019CE RID: 6606
	private List<PsUIYoutubeChannelBanner> m_banners;

	// Token: 0x040019CF RID: 6607
	private UIScrollableCanvas m_content;

	// Token: 0x040019D0 RID: 6608
	private string m_searchInput;

	// Token: 0x040019D1 RID: 6609
	private string m_searchInputId;

	// Token: 0x040019D2 RID: 6610
	private bool m_created;

	// Token: 0x040019D3 RID: 6611
	private bool m_searched;

	// Token: 0x040019D4 RID: 6612
	private PsUIYoutubeChannelBanner m_current;

	// Token: 0x040019D5 RID: 6613
	private PsUIYoutubeChannelBanner m_selected;

	// Token: 0x040019D6 RID: 6614
	private PsUIGenericButton m_unselect;

	// Token: 0x040019D7 RID: 6615
	private UICanvas m_unselectHolder;
}
