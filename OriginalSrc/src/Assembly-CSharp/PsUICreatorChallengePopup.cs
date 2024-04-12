using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002FB RID: 763
public class PsUICreatorChallengePopup : PsUIEventMessagePopup
{
	// Token: 0x06001665 RID: 5733 RVA: 0x000EADB8 File Offset: 0x000E91B8
	public PsUICreatorChallengePopup(UIComponent _parent)
		: base(_parent, string.Empty, true, 0.1f, RelativeTo.ScreenHeight, 0.065f, RelativeTo.ScreenHeight)
	{
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(this.BGDrawhandler));
		this.SetWidth(0.8f, RelativeTo.ScreenHeight);
		this.SetHeight(0.875f, RelativeTo.ScreenHeight);
		this.SetVerticalAlign(0.4f);
		this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
		this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
		this.m_header.SetMargins(0.07f, 0.07f, 0.03f, 0.03f, RelativeTo.ScreenHeight);
	}

	// Token: 0x06001666 RID: 5734 RVA: 0x000EAEA0 File Offset: 0x000E92A0
	public override void SetEventMessage(EventMessage _msg, bool _newsPage = false)
	{
		this.m_newsPage = _newsPage;
		if (_msg.eventStates != null && _msg.eventStates.Count > 0)
		{
			for (int i = 0; i < _msg.eventStates.Count; i++)
			{
				int num = (int)((double)_msg.eventStates[i].localEndTime - Main.m_EPOCHSeconds);
				if (num > 0)
				{
					this.m_currentState = _msg.eventStates[i];
					this.m_stateIndex = i;
					break;
				}
				if (i == _msg.eventStates.Count - 1)
				{
					this.m_currentState = _msg.eventStates[i];
					this.m_stateIndex = i;
				}
			}
		}
		base.SetEventMessage(_msg, false);
	}

	// Token: 0x06001667 RID: 5735 RVA: 0x000EAF60 File Offset: 0x000E9360
	public override void CreateHeaderContent(UIComponent _parent)
	{
		UITextbox uitextbox = new UITextbox(_parent, false, string.Empty, this.m_currentState.title, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenHeight, true, Align.Center, Align.Top, null, true, null);
		uitextbox.SetWidth(1f, RelativeTo.ParentWidth);
		uitextbox.SetHeight(1f, RelativeTo.ParentHeight);
		uitextbox.SetMargins(0.05f, 0.05f, 0f, 0f, RelativeTo.ScreenHeight);
	}

	// Token: 0x06001668 RID: 5736 RVA: 0x000EAFCC File Offset: 0x000E93CC
	public new void BGDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect((float)Screen.width * 1.5f, (float)Screen.height * 1.5f, Vector2.zero);
		Color black = Color.black;
		black.a = 0.65f;
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward, ggdata, black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), this.m_camera);
	}

	// Token: 0x06001669 RID: 5737 RVA: 0x000EB04C File Offset: 0x000E944C
	public override void CreateContent(UIComponent _parent)
	{
		if (this.m_stateIndex == 0)
		{
			this.CreateFirstState(_parent);
		}
		else if (this.m_stateIndex == 1)
		{
			this.CreateSecondState(_parent);
		}
		else if (this.m_stateIndex == 2)
		{
			this.CreateThirdState(_parent);
		}
	}

	// Token: 0x0600166A RID: 5738 RVA: 0x000EB09C File Offset: 0x000E949C
	private void CreateFirstState(UIComponent _parent)
	{
		this.m_secondStageEnabled = false;
		this.m_currentTimeLeft = (int)((double)this.m_currentState.localEndTime - Main.m_EPOCHSeconds);
		UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(0.04f, RelativeTo.ScreenHeight);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetVerticalAlign(0.995f);
		uicanvas.SetDrawHandler(new UIDrawDelegate(this.RedDrawhandler));
		UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
		uicanvas2.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas2.SetHeight(0.02f, RelativeTo.ScreenHeight);
		uicanvas2.RemoveDrawHandler();
		uicanvas2.SetDepthOffset(-10f);
		this.m_timeText = new UIFittedText(uicanvas2, false, string.Empty, PsStrings.Get(StringID.CHALLENGE_TIMER_PARTICIPATE) + " " + PsMetagameManager.GetTimeStringFromSeconds(this.m_currentTimeLeft), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		this.m_timeText.SetMargins(0f, 0f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		UIScrollableCanvas uiscrollableCanvas = new UIScrollableCanvas(_parent, string.Empty);
		uiscrollableCanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uiscrollableCanvas.SetHeight(0.94f, RelativeTo.ParentHeight);
		uiscrollableCanvas.SetVerticalAlign(0f);
		uiscrollableCanvas.RemoveDrawHandler();
		uiscrollableCanvas.m_passTouchesToScrollableParents = true;
		UIVerticalList uiverticalList = new UIVerticalList(uiscrollableCanvas, string.Empty);
		uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList.SetMargins(0f, 0f, -0.015f, 0f, RelativeTo.ScreenHeight);
		uiverticalList.SetVerticalAlign(1f);
		uiverticalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uiverticalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_creation_challenge_illustration_1", null), true, true);
		uifittedSprite.SetWidth(1f, RelativeTo.ParentWidth);
		uifittedSprite.SetAlign(0f, 1f);
		UICanvas uicanvas3 = new UICanvas(uifittedSprite, false, string.Empty, null, string.Empty);
		uicanvas3.SetHeight(0.65f, RelativeTo.ParentHeight);
		uicanvas3.SetWidth(0.35f, RelativeTo.ParentWidth);
		uicanvas3.SetAlign(0.825f, 0.7f);
		uicanvas3.RemoveDrawHandler();
		UITextbox uitextbox = new UITextbox(uicanvas3, false, string.Empty, this.m_currentState.description, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, true, Align.Center, Align.Top, "000000", true, null);
		uitextbox.SetVerticalAlign(0f);
		UIVerticalList uiverticalList2 = new UIVerticalList(uiverticalList, string.Empty);
		uiverticalList2.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		uiverticalList2.SetDepthOffset(-10f);
		uiverticalList2.SetMargins(0f, 0f, -0.07f, 0.07f, RelativeTo.ScreenHeight);
		uiverticalList2.RemoveDrawHandler();
		uiverticalList2.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_videoButton = new PsUIGenericButton(uiverticalList2, 0.25f, 0.25f, 0.005f, "Button");
		this.m_videoButton.SetBlueWhiteColors(true);
		this.m_videoButton.SetHeight(0.19f, RelativeTo.ScreenHeight);
		UIVerticalList uiverticalList3 = new UIVerticalList(this.m_videoButton, string.Empty);
		uiverticalList3.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uiverticalList3.SetWidth(0.335f, RelativeTo.ScreenHeight);
		uiverticalList3.RemoveDrawHandler();
		UIFittedSprite uifittedSprite2 = new UIFittedSprite(uiverticalList3, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_youtube", null), true, true);
		uifittedSprite2.SetHeight(0.09f, RelativeTo.ScreenHeight);
		UICanvas uicanvas4 = new UICanvas(uiverticalList3, false, string.Empty, null, string.Empty);
		uicanvas4.SetHeight(0.0275f, RelativeTo.ScreenHeight);
		uicanvas4.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas4.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas4, false, string.Empty, PsStrings.Get(StringID.CHALLENGE_TEXT_FIND_OUT).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, "000000");
		UICanvas uicanvas5 = new UICanvas(uiverticalList3, false, string.Empty, null, string.Empty);
		uicanvas5.SetHeight(0.02f, RelativeTo.ScreenHeight);
		uicanvas5.SetWidth(1f, RelativeTo.ScreenHeight);
		uicanvas5.RemoveDrawHandler();
		UIFittedText uifittedText2 = new UIFittedText(uicanvas5, false, string.Empty, PsStrings.Get(StringID.CHALLENGE_TEXT_TRAPLIGHT_YT), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#040B13", null);
		UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList2, string.Empty);
		uihorizontalList.SetSpacing(0.03f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		if (!string.IsNullOrEmpty(this.m_currentState.mainSearchUrl))
		{
			this.m_searchButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
			this.m_searchButton.SetBlueColors(true);
			this.m_searchButton.SetHeight(0.15f, RelativeTo.ScreenHeight);
			UIVerticalList uiverticalList4 = new UIVerticalList(this.m_searchButton, string.Empty);
			uiverticalList4.SetSpacing(0.01f, RelativeTo.ScreenHeight);
			uiverticalList4.SetWidth(0.275f, RelativeTo.ScreenHeight);
			uiverticalList4.RemoveDrawHandler();
			UITextbox uitextbox2 = new UITextbox(uiverticalList4, false, string.Empty, PsStrings.Get(StringID.CHALLENGE_TEXT_WATCH_ENTRIES), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, null, true, "000000");
			uitextbox2.SetMargins(0.015f, 0.015f, 0f, 0f, RelativeTo.ScreenHeight);
			uitextbox2.SetMaxRows(2);
			UICanvas uicanvas6 = new UICanvas(uiverticalList4, false, string.Empty, null, string.Empty);
			uicanvas6.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas6.SetHeight(0.02f, RelativeTo.ScreenHeight);
			uicanvas6.RemoveDrawHandler();
			UIFittedText uifittedText3 = new UIFittedText(uicanvas6, false, string.Empty, "#BigBangWin", PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#040B13", null);
			uifittedText3.SetMargins(0.05f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
			UICanvas uicanvas7 = new UICanvas(uifittedText3, false, string.Empty, null, string.Empty);
			uicanvas7.SetSize(0.045f, 0.045f, RelativeTo.ScreenHeight);
			uicanvas7.SetAlign(0f, 0.5f);
			uicanvas7.SetMargins(-0.05f, 0.05f, 0f, 0f, RelativeTo.ScreenHeight);
			uicanvas7.RemoveDrawHandler();
			UIFittedSprite uifittedSprite3 = new UIFittedSprite(uicanvas7, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_youtube", null), true, true);
			uifittedSprite3.SetHeight(1f, RelativeTo.ParentHeight);
		}
		if (!string.IsNullOrEmpty(this.m_currentState.mainParticipateUrl))
		{
			this.m_participateButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
			this.m_participateButton.SetGreenColors(true);
			this.m_participateButton.SetHeight(0.15f, RelativeTo.ScreenHeight);
			UIVerticalList uiverticalList5 = new UIVerticalList(this.m_participateButton, string.Empty);
			uiverticalList5.SetSpacing(0.01f, RelativeTo.ScreenHeight);
			uiverticalList5.SetWidth(0.275f, RelativeTo.ScreenHeight);
			uiverticalList5.RemoveDrawHandler();
			UITextbox uitextbox3 = new UITextbox(uiverticalList5, false, string.Empty, PsStrings.Get(StringID.CHALLENGE_BUTTON_SUBMIT), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, null, true, "000000");
			uitextbox3.SetWidth(1f, RelativeTo.ParentWidth);
			uitextbox3.SetMaxRows(2);
		}
		PsUIGenericButton psUIGenericButton = new PsUIGenericButton(uiverticalList2, 0.25f, 0.25f, 0.005f, "Button");
		psUIGenericButton.SetText(PsStrings.Get(StringID.FORUM), 0.025f, 0f, RelativeTo.ScreenHeight, true, RelativeTo.ScreenShortest);
		psUIGenericButton.SetMargins(0.03f, 0.03f, 0.03f, 0.03f, RelativeTo.ScreenHeight);
		psUIGenericButton.SetReleaseAction(delegate
		{
			Application.OpenURL("http://forum.bigbangracinggame.com/categories/creation-challenge-2016");
		});
	}

	// Token: 0x0600166B RID: 5739 RVA: 0x000EB800 File Offset: 0x000E9C00
	private void CreateSecondState(UIComponent _parent)
	{
		this.m_secondStageEnabled = true;
		UIVerticalList uiverticalList = new UIVerticalList(_parent, string.Empty);
		uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList.SetMargins(0f, 0f, 0.005f, 0.04f, RelativeTo.ScreenHeight);
		uiverticalList.SetVerticalAlign(1f);
		uiverticalList.RemoveDrawHandler();
		uiverticalList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		this.m_currentTimeLeft = (int)((double)this.m_currentState.localEndTime - Main.m_EPOCHSeconds);
		UICanvas uicanvas = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(0.04f, RelativeTo.ScreenHeight);
		uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas.SetVerticalAlign(1f);
		uicanvas.SetDrawHandler(new UIDrawDelegate(this.GreenDrawhandler));
		UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
		uicanvas2.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas2.SetHeight(0.02f, RelativeTo.ScreenHeight);
		uicanvas2.RemoveDrawHandler();
		this.m_timeText = new UIFittedText(uicanvas2, false, string.Empty, PsStrings.Get(StringID.CHALLENGE_TIMER_RESULTS) + " " + PsMetagameManager.GetTimeStringFromSeconds(this.m_currentTimeLeft), PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		this.m_timeText.SetMargins(0f, 0f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		UIFittedSprite uifittedSprite = new UIFittedSprite(uiverticalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_creation_challenge_illustration_1", null), true, true);
		uifittedSprite.SetWidth(1f, RelativeTo.ParentWidth);
		uifittedSprite.SetAlign(0f, 1f);
		UICanvas uicanvas3 = new UICanvas(uifittedSprite, false, string.Empty, null, string.Empty);
		uicanvas3.SetHeight(0.65f, RelativeTo.ParentHeight);
		uicanvas3.SetWidth(0.35f, RelativeTo.ParentWidth);
		uicanvas3.SetAlign(0.825f, 0.7f);
		uicanvas3.RemoveDrawHandler();
		UITextbox uitextbox = new UITextbox(uicanvas3, false, string.Empty, this.m_currentState.description, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, true, Align.Center, Align.Top, "000000", true, null);
		uitextbox.SetVerticalAlign(0f);
		UICanvas uicanvas4 = new UIVerticalList(uiverticalList, string.Empty);
		uicanvas4.SetHeight(0.2f, RelativeTo.ScreenHeight);
		uicanvas4.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas4.SetMargins(0f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas4.RemoveDrawHandler();
		this.m_searchButton = new PsUIGenericButton(uicanvas4, 0.25f, 0.25f, 0.005f, "Button");
		this.m_searchButton.SetBlueWhiteColors(true);
		this.m_searchButton.SetHeight(0.19f, RelativeTo.ScreenHeight);
		UIVerticalList uiverticalList2 = new UIVerticalList(this.m_searchButton, string.Empty);
		uiverticalList2.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uiverticalList2.SetWidth(0.335f, RelativeTo.ScreenHeight);
		uiverticalList2.RemoveDrawHandler();
		UIFittedSprite uifittedSprite2 = new UIFittedSprite(uiverticalList2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_youtube", null), true, true);
		uifittedSprite2.SetHeight(0.09f, RelativeTo.ScreenHeight);
		UITextbox uitextbox2 = new UITextbox(uiverticalList2, false, string.Empty, PsStrings.Get(StringID.CHALLENGE_TEXT_WATCH_ENTRIES).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, false, Align.Center, Align.Top, null, true, "000000");
		uitextbox2.SetWidth(1f, RelativeTo.ParentWidth);
		uitextbox2.SetMaxRows(2);
		UICanvas uicanvas5 = new UICanvas(uiverticalList2, false, string.Empty, null, string.Empty);
		uicanvas5.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas5.SetHeight(0.02f, RelativeTo.ScreenHeight);
		uicanvas5.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas5, false, string.Empty, "#BigBangWin", PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#040B13", null);
		uifittedText.SetMargins(0.05f, 0f, 0f, 0f, RelativeTo.ScreenHeight);
		UICanvas uicanvas6 = new UICanvas(uifittedText, false, string.Empty, null, string.Empty);
		uicanvas6.SetSize(0.045f, 0.045f, RelativeTo.ScreenHeight);
		uicanvas6.SetAlign(0f, 0.5f);
		uicanvas6.SetMargins(-0.05f, 0.05f, 0f, 0f, RelativeTo.ScreenHeight);
		uicanvas6.RemoveDrawHandler();
		UIFittedSprite uifittedSprite3 = new UIFittedSprite(uicanvas6, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_youtube", null), true, true);
		uifittedSprite3.SetHeight(1f, RelativeTo.ParentHeight);
	}

	// Token: 0x0600166C RID: 5740 RVA: 0x000EBC5C File Offset: 0x000EA05C
	private void CreateThirdState(UIComponent _parent)
	{
		this.m_secondStageEnabled = false;
		this.SetMargins(0.0125f, 0.0125f, 0.0115f, 0.0125f, RelativeTo.ScreenHeight);
		this.m_lateExitSet = true;
		UIScrollableCanvas uiscrollableCanvas = new UIScrollableCanvas(_parent, string.Empty);
		uiscrollableCanvas.SetWidth(1f, RelativeTo.ParentWidth);
		uiscrollableCanvas.SetHeight(1f, RelativeTo.ParentHeight);
		uiscrollableCanvas.RemoveDrawHandler();
		uiscrollableCanvas.m_passTouchesToScrollableParents = true;
		UIVerticalList uiverticalList = new UIVerticalList(uiscrollableCanvas, string.Empty);
		uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList.SetMargins(0f, 0f, 0.07f, 0.04f, RelativeTo.ScreenHeight);
		uiverticalList.SetVerticalAlign(1f);
		uiverticalList.SetSpacing(0.05f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		this.m_videoButton = new PsUIGenericButton(uiverticalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_videoButton.SetBlueColors(true);
		this.m_videoButton.SetHeight(0.19f, RelativeTo.ScreenHeight);
		UIVerticalList uiverticalList2 = new UIVerticalList(this.m_videoButton, string.Empty);
		uiverticalList2.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uiverticalList2.SetWidth(0.335f, RelativeTo.ScreenHeight);
		uiverticalList2.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(uiverticalList2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_youtube", null), true, true);
		uifittedSprite.SetHeight(0.09f, RelativeTo.ScreenHeight);
		UITextbox uitextbox = new UITextbox(uiverticalList2, false, string.Empty, PsStrings.Get(StringID.CHALLENGE_WATCH_ANNOUNCEMENT).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0285f, RelativeTo.ScreenHeight, true, Align.Center, Align.Top, null, true, "000000");
		UIVerticalList uiverticalList3 = new UIVerticalList(uiverticalList, string.Empty);
		uiverticalList3.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList3.SetSpacing(0.015f, RelativeTo.ScreenHeight);
		uiverticalList3.RemoveDrawHandler();
		if (this.m_currentState.adventureWinner != null)
		{
			UICanvas uicanvas = new UICanvas(uiverticalList3, false, string.Empty, null, string.Empty);
			uicanvas.SetHeight(0.005f, RelativeTo.ScreenHeight);
			uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas.RemoveDrawHandler();
			new PsUICreatorChallengeBanner(uiverticalList3, string.Empty, this.m_currentState.adventureWinner.levelId, this.m_currentState.adventureWinner.levelName, this.m_currentState.adventureWinner.creatorName, this.m_currentState.adventureWinner.videoUrl, true, 0, -1);
		}
		if (this.m_currentState.raceWinner != null)
		{
			UICanvas uicanvas2 = new UICanvas(uiverticalList3, false, string.Empty, null, string.Empty);
			uicanvas2.SetHeight(0.005f, RelativeTo.ScreenHeight);
			uicanvas2.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas2.RemoveDrawHandler();
			new PsUICreatorChallengeBanner(uiverticalList3, string.Empty, this.m_currentState.raceWinner.levelId, this.m_currentState.raceWinner.levelName, this.m_currentState.raceWinner.creatorName, this.m_currentState.raceWinner.videoUrl, true, 1, -1);
		}
		if (this.m_currentState.entries != null)
		{
			UICanvas uicanvas3 = new UICanvas(uiverticalList3, false, string.Empty, null, string.Empty);
			uicanvas3.SetHeight(0.05f, RelativeTo.ScreenHeight);
			uicanvas3.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas3.RemoveDrawHandler();
			for (int i = 0; i < this.m_currentState.entries.Count; i++)
			{
				UIVerticalList uiverticalList4 = uiverticalList3;
				string empty = string.Empty;
				string levelId = this.m_currentState.entries[i].levelId;
				string levelName = this.m_currentState.entries[i].levelName;
				string creatorName = this.m_currentState.entries[i].creatorName;
				string videoUrl = this.m_currentState.entries[i].videoUrl;
				int num = i;
				new PsUICreatorChallengeBanner(uiverticalList4, empty, levelId, levelName, creatorName, videoUrl, false, -1, num);
			}
		}
	}

	// Token: 0x0600166D RID: 5741 RVA: 0x000EC038 File Offset: 0x000EA438
	private void RedDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero);
		GGData ggdata = new GGData(rect);
		Color color = DebugDraw.HexToColor("#A2250E");
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.zero, ggdata, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x0600166E RID: 5742 RVA: 0x000EC0A4 File Offset: 0x000EA4A4
	private void GreenDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero);
		GGData ggdata = new GGData(rect);
		Color color = DebugDraw.HexToColor("#66A70F");
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.zero, ggdata, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x0600166F RID: 5743 RVA: 0x000EC110 File Offset: 0x000EA510
	public override void Step()
	{
		if (this.m_lateExitSet)
		{
			(this.GetRoot() as PsUIBasePopup).SetAction("Exit", delegate
			{
				(this.GetRoot() as PsUIBasePopup).CallAction("Continue");
			});
			this.m_lateExitSet = false;
		}
		int num = (int)((double)this.m_currentState.localEndTime - Main.m_EPOCHSeconds);
		if (this.m_currentTimeLeft != num && this.m_timeText != null)
		{
			this.m_currentTimeLeft = num;
			string text = PsStrings.Get(StringID.CHALLENGE_TIMER_PARTICIPATE) + " " + PsMetagameManager.GetTimeStringFromSeconds(this.m_currentTimeLeft);
			if (this.m_secondStageEnabled)
			{
				text = PsStrings.Get(StringID.CHALLENGE_TIMER_RESULTS) + " " + PsMetagameManager.GetTimeStringFromSeconds(this.m_currentTimeLeft);
			}
			this.m_timeText.SetText(text);
			if (this.m_currentTimeLeft < 0 && this.m_stateIndex < 2)
			{
				this.m_header.DestroyChildren();
				this.DestroyChildren();
				this.m_timeText = null;
				this.m_searchButton = null;
				this.m_participateButton = null;
				this.m_videoButton = null;
				this.m_stateIndex++;
				this.m_currentState = this.m_eventMessage.eventStates[this.m_stateIndex];
				this.CreateHeaderContent(this.m_header);
				this.CreateContent(this);
				this.m_parent.Update();
			}
		}
		if (this.m_searchButton != null && this.m_searchButton.m_hit)
		{
			PsMetrics.EntryVideoListOpened(this.m_eventMessage.eventType, this.m_eventMessage.eventName, this.m_currentState.id.ToString(), this.m_currentState.title);
			if (!string.IsNullOrEmpty(this.m_currentState.mainSearchUrl))
			{
				Application.OpenURL(this.m_currentState.mainSearchUrl);
			}
			TouchAreaS.CancelAllTouches(null);
		}
		else if (this.m_videoButton != null && this.m_videoButton.m_hit)
		{
			PsMetrics.AnnouncementVideoOpened(this.m_eventMessage.eventType, this.m_eventMessage.eventName, this.m_currentState.id.ToString(), this.m_currentState.title);
			if (!string.IsNullOrEmpty(this.m_currentState.mainVideoUrl))
			{
				Application.OpenURL(this.m_currentState.mainVideoUrl);
			}
			TouchAreaS.CancelAllTouches(null);
		}
		else if (this.m_participateButton != null && this.m_participateButton.m_hit)
		{
			PsMetrics.ParticipationFormOpened(this.m_eventMessage.eventType, this.m_eventMessage.eventName);
			if (!string.IsNullOrEmpty(this.m_currentState.mainParticipateUrl))
			{
				Application.OpenURL(this.m_currentState.mainParticipateUrl);
			}
			TouchAreaS.CancelAllTouches(null);
		}
		base.Step();
	}

	// Token: 0x06001670 RID: 5744 RVA: 0x000EC3DB File Offset: 0x000EA7DB
	public override void Destroy()
	{
		base.Destroy();
	}

	// Token: 0x04001925 RID: 6437
	private List<UICanvas> m_secondaryLinks;

	// Token: 0x04001926 RID: 6438
	private EventState m_currentState;

	// Token: 0x04001927 RID: 6439
	private int m_stateIndex;

	// Token: 0x04001928 RID: 6440
	private UIFittedText m_timeText;

	// Token: 0x04001929 RID: 6441
	private PsUIGenericButton m_videoButton;

	// Token: 0x0400192A RID: 6442
	private PsUIGenericButton m_searchButton;

	// Token: 0x0400192B RID: 6443
	private PsUIGenericButton m_participateButton;

	// Token: 0x0400192C RID: 6444
	private int m_currentTimeLeft;

	// Token: 0x0400192D RID: 6445
	private bool m_lateExitSet;

	// Token: 0x0400192E RID: 6446
	private bool m_newsPage;

	// Token: 0x0400192F RID: 6447
	private bool m_secondStageEnabled;
}
