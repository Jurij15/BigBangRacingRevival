using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using UnityEngine;

// Token: 0x020002E4 RID: 740
public class PsUITournamentHeader : UICanvas
{
	// Token: 0x060015E0 RID: 5600 RVA: 0x000E3F0C File Offset: 0x000E230C
	public PsUITournamentHeader(UIComponent _parent)
		: base(_parent, false, string.Empty, null, string.Empty)
	{
		if (this.m_endingSoonColor != null && this.m_endingSoonColor == "#ffffff")
		{
			this.m_endingSoonColor = null;
		}
		PsUITournamentHeader.m_hideUI = false;
		this.m_tournament = PsState.m_activeGameLoop as PsGameLoopTournament;
		this.m_tournamentEvent = this.m_tournament.m_eventMessage.tournament;
		float ccCap = this.m_tournament.GetCcCap();
		this.m_timeLeft = (int)Math.Ceiling((double)PsMetagameManager.m_activeTournament.localEndTime - Main.m_EPOCHSeconds);
		this.m_creator = this.m_tournament.GetCreator();
		string text;
		if (this.m_timeLeft <= 0)
		{
			text = PsStrings.Get(StringID.TOUR_TOURNAMENT_OVER);
			this.m_tournamentEnded = true;
		}
		else
		{
			text = PsMetagameManager.GetTimeStringFromSeconds(this.m_timeLeft, true, true);
		}
		string bannerString = this.m_tournament.GetBannerString();
		this.m_leftContainer = new UICanvas(this, false, string.Empty, null, string.Empty);
		this.m_leftContainer.SetWidth(0.3f, RelativeTo.ParentWidth);
		this.m_leftContainer.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_leftContainer.SetAlign(0f, 1f);
		this.m_leftContainer.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.TournamentBannerLeft));
		this.m_leftContainerTweenParent = new UICanvas(this, false, string.Empty, null, string.Empty);
		this.m_leftContainerTweenParent.SetWidth(0.5f, RelativeTo.ParentWidth);
		this.m_leftContainerTweenParent.SetAlign(0f, 1f);
		this.m_leftContainerTweenParent.SetMargins(-1f, 1f, 0f, 0f, RelativeTo.OwnWidth);
		this.m_leftContainerTweenParent.RemoveDrawHandler();
		this.m_leftChatDrawhandler = new UICanvas(this.m_leftContainerTweenParent, false, string.Empty, null, string.Empty);
		this.m_leftChatDrawhandler.SetAlign(0f, 1f);
		this.m_leftChatDrawhandler.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.TournamentBannerLeftChat));
		this.m_leftChatDrawhandler.SetDepthOffset(1f);
		this.m_mainContainer = new UICanvas(this, false, string.Empty, null, string.Empty);
		this.m_mainContainer.SetWidth(0.6f, RelativeTo.ParentWidth);
		this.m_mainContainer.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_mainContainer.SetDepthOffset(-10f);
		this.m_mainContainer.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.TournamentBannerCenter));
		UICanvas uicanvas = new UICanvas(this.m_mainContainer, false, string.Empty, null, string.Empty);
		uicanvas.SetVerticalAlign(1f);
		uicanvas.SetWidth(0.8f, RelativeTo.ParentWidth);
		uicanvas.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas.RemoveDrawHandler();
		this.m_headerCreatorHolder = new UICanvas(uicanvas, true, string.Empty, null, string.Empty);
		this.m_headerCreatorHolder.SetWidth(0.25f, RelativeTo.ParentWidth);
		this.m_headerCreatorHolder.SetHorizontalAlign(0f);
		this.m_headerCreatorHolder.SetMargins(-0.4f, 0.1f, 0.05f, -0.05f, RelativeTo.OwnWidth);
		this.m_headerCreatorHolder.RemoveDrawHandler();
		UIFittedSprite uifittedSprite = new UIFittedSprite(this.m_headerCreatorHolder, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_logo_wreath", null), true, true);
		uifittedSprite.SetVerticalAlign(0f);
		uifittedSprite.SetDepthOffset(-4f);
		PsUIProfileImage psUIProfileImage = new PsUIProfileImage(this.m_headerCreatorHolder, false, string.Empty, this.m_creator.facebookId, this.m_creator.playerId, -1, true, false, false, 0.05f, 0.06f, "fff9e6", null, false, true);
		psUIProfileImage.SetSize(0.055f, 0.055f, RelativeTo.ScreenWidth);
		psUIProfileImage.SetVerticalAlign(1f);
		UIFittedSprite uifittedSprite2 = new UIFittedSprite(this.m_headerCreatorHolder, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_logo_wings", null), true, true);
		uifittedSprite2.SetVerticalAlign(0f);
		if (!string.IsNullOrEmpty(this.m_creator.youtubeName))
		{
			this.CreateYoutubeButton(this.m_headerCreatorHolder);
		}
		this.m_headerTextHolder = new UICanvas(uicanvas, true, string.Empty, null, string.Empty);
		this.m_headerTextHolder.SetHeight(0.8f, RelativeTo.ParentHeight);
		this.m_headerTextHolder.SetWidth(0.5f, RelativeTo.ParentWidth);
		this.m_headerTextHolder.SetMargins(0f, 0.1f, 0.1f, 0.1f, RelativeTo.OwnHeight);
		this.m_headerTextHolder.SetAlign(0.5f, 1f);
		this.m_headerTextHolder.RemoveDrawHandler();
		UICanvas uicanvas2 = new UICanvas(this.m_headerTextHolder, false, string.Empty, null, string.Empty);
		uicanvas2.SetHeight(0.6f, RelativeTo.ParentHeight);
		uicanvas2.SetVerticalAlign(1f);
		uicanvas2.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas2, false, string.Empty, this.m_creator.name, PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, null);
		UICanvas uicanvas3 = new UICanvas(this.m_headerTextHolder, false, string.Empty, null, string.Empty);
		uicanvas3.SetHeight(0.35f, RelativeTo.ParentHeight);
		uicanvas3.SetAlign(0f, 0f);
		uicanvas3.RemoveDrawHandler();
		UIFittedText uifittedText2 = new UIFittedText(uicanvas3, false, string.Empty, PsStrings.Get(StringID.TOUR_TOURNAMENT).ToUpper(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#FCA629", "#000");
		UICanvas uicanvas4 = new UICanvas(uifittedText2, false, string.Empty, null, string.Empty);
		uicanvas4.SetSize(0.025f, 0.025f, RelativeTo.ScreenWidth);
		uicanvas4.SetMargins(1.2f, -1.2f, 0f, 0f, RelativeTo.OwnHeight);
		uicanvas4.SetHorizontalAlign(1f);
		uicanvas4.RemoveDrawHandler();
		this.m_infoButton = new UIRectSpriteButton(uicanvas4, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_info_button", null), true, false);
		this.m_timerContainer = new UIHorizontalList(uicanvas, string.Empty);
		this.m_timerContainer.SetMargins(0.14f, 0.14f, 0.07f, 0.07f, RelativeTo.ParentHeight);
		this.m_timerContainer.SetHeight(0.2f, RelativeTo.ParentHeight);
		this.m_timerContainer.SetVerticalAlign(0f);
		this.m_timerContainer.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.Black));
		if (this.m_timeLeft > 0)
		{
			this.m_endsIn = new UIText(this.m_timerContainer, false, string.Empty, PsStrings.Get(StringID.TOUR_ENDS_IN), PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.8f, RelativeTo.ParentHeight, "#6DD2FB", null);
			this.m_endsIn.SetHorizontalAlign(0.5f);
		}
		this.m_timeleftText = new UIText(this.m_timerContainer, false, "Timer", text, PsFontManager.GetFont(PsFonts.HurmeSemiBoldMN), 0.8f, RelativeTo.ParentHeight, null, null);
		this.m_timeleftText.SetHorizontalAlign(0.5f);
		this.m_followButtonHolder = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
		this.m_followButtonHolder.SetWidth(0.25f, RelativeTo.ParentWidth);
		this.m_followButtonHolder.SetHeight(0.8f, RelativeTo.ParentHeight);
		this.m_followButtonHolder.SetAlign(1f, 1f);
		this.m_followButtonHolder.SetMargins(0f, 0f, 0f, 0f, RelativeTo.OwnWidth);
		this.m_followButtonHolder.RemoveDrawHandler();
		this.SetFollowButton(this.m_followButtonHolder);
		this.m_rightContainer = new UICanvas(this, false, string.Empty, null, string.Empty);
		this.m_rightContainer.SetWidth(0.3f, RelativeTo.ParentWidth);
		this.m_rightContainer.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_rightContainer.SetAlign(1f, 1f);
		this.m_rightContainer.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.TournamentBannerRight));
		this.m_rightContainerTweenParent = new UICanvas(this.m_rightContainer, false, string.Empty, null, string.Empty);
		this.m_rightContainerTweenParent.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_rightContainerTweenParent.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_rightContainerTweenParent.SetHorizontalAlign(0f);
		this.m_rightContainerTweenParent.RemoveDrawHandler();
		this.m_rightContainerTitle = new UICanvas(this.m_rightContainerTweenParent, false, string.Empty, null, string.Empty);
		this.m_rightContainerTitle.SetAlign(0.5f, 1f);
		this.m_rightContainerTitle.SetHeight(0.3f, RelativeTo.ParentHeight);
		this.m_rightContainerTitle.SetWidth(1f, RelativeTo.ParentWidth);
		this.m_rightContainerTitle.SetMargins(0.7f, 0f, 0.07f, 0.07f, RelativeTo.ParentHeight);
		this.m_rightContainerTitle.RemoveDrawHandler();
		this.m_titleText = new UIFittedText(this.m_rightContainerTitle, false, string.Empty, PsStrings.Get(StringID.TOUR_TOTAL_POT), PsFontManager.GetFont(PsFonts.HurmeBold), true, "#AE9079", null);
		this.m_titleText.SetAlign(0.5f, 0.5f);
		this.m_priceBanner = new UICanvas(this.m_rightContainerTweenParent, false, string.Empty, null, string.Empty);
		this.m_priceBanner.SetWidth(0.98f, RelativeTo.ParentWidth);
		this.m_priceBanner.SetHeight(0.33f, RelativeTo.ParentHeight);
		this.m_priceBanner.SetAlign(0f, 0.45f);
		this.m_priceBanner.SetMargins(2.9f, 0f, 0f, 0f, RelativeTo.OwnHeight);
		this.m_priceBanner.RemoveDrawHandler();
		this.m_priceBannerBackground = new UICanvas(this.m_priceBanner, false, string.Empty, null, string.Empty);
		this.m_priceBannerBackground.SetMargins(0.2f, 0.6f, 0.03f, 0.075f, RelativeTo.OwnHeight);
		this.m_priceBannerBackground.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.TournamentBannerRightPriceBanner));
		this.m_coinIcon = new UISprite(this.m_priceBannerBackground, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_resources_coin_icon", null), true);
		this.m_coinIcon.SetSize(1.2f, 1.25f, RelativeTo.ParentHeight);
		this.m_coinIcon.SetAlign(-0.2f, 0.5f);
		this.m_coinIcon.SetDepthOffset(-30f);
		this.m_coinPriceContainer = new UICanvas(this.m_priceBannerBackground, false, string.Empty, null, string.Empty);
		this.m_coinPriceContainer.SetWidth(0f, RelativeTo.ParentWidth);
		this.m_coinPriceContainer.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_coinPriceContainer.SetAlign(1f, 0.5f);
		this.m_coinPriceContainer.RemoveDrawHandler();
		this.m_currentPot = this.GetPot();
		string text2 = string.Format("{0:n0}", this.m_currentPot).Replace(",", " ");
		this.m_coinPrice = new UIText(this.m_coinPriceContainer, false, string.Empty, text2, PsFontManager.GetFont(PsFonts.HurmeBoldMN), 1f, RelativeTo.ParentHeight, "#FDD444", null);
		this.m_coinPrice.m_tmc.m_horizontalAlign = Align.Right;
		this.PotTimer(1f);
		this.m_bottomTextContainer = new UICanvas(this.m_rightContainerTweenParent, false, string.Empty, null, string.Empty);
		this.m_bottomTextContainer.SetAlign(0f, 0.12f);
		this.m_bottomTextContainer.SetWidth(0.95f, RelativeTo.ParentWidth);
		this.m_bottomTextContainer.SetHeight(0.18f, RelativeTo.ParentHeight);
		this.m_bottomTextContainer.SetMargins(0.6f, 0.1f, 0f, 0f, RelativeTo.ParentHeight);
		this.m_bottomTextContainer.RemoveDrawHandler();
		this.m_bottomText = new UIFittedText(this.m_bottomTextContainer, false, string.Empty, PsStrings.Get(StringID.TOUR_SHARED_ACROSS_ALL_ROOMS), PsFontManager.GetFont(PsFonts.HurmeBold), true, "#FDD444", null);
		this.m_bottomText.SetAlign(0f, 1f);
	}

	// Token: 0x060015E1 RID: 5601 RVA: 0x000E4B58 File Offset: 0x000E2F58
	private void SetFollowButton(UIComponent _parent)
	{
		if (_parent != null)
		{
			if (this.m_followButton != null)
			{
				this.m_followButton.Destroy();
			}
			this.m_owner = default(PlayerData);
			this.m_owner.playerId = this.m_tournamentEvent.ownerId;
			this.m_owner.name = this.m_tournamentEvent.ownerName;
			this.m_followButton = new PsUIFollowButton(_parent, this.m_owner, 0.25f, 0.25f, 0.005f, 0.045f, RelativeTo.ScreenWidth);
			this.m_followButton.SetHorizontalAlign(1f);
			this.m_followButton.SetHeight(0.038f, RelativeTo.ScreenWidth);
			_parent.Update();
		}
	}

	// Token: 0x060015E2 RID: 5602 RVA: 0x000E4C0C File Offset: 0x000E300C
	private void CreateYoutubeButton(UIComponent _parent)
	{
		if (this.m_tournament != null && !string.IsNullOrEmpty(this.m_creator.youtubeId))
		{
			this.m_explosionHolder = new UICanvas(_parent, false, string.Empty, null, string.Empty);
			this.m_explosionHolder.SetHeight(0.021f, RelativeTo.ScreenWidth);
			this.m_explosionHolder.SetVerticalAlign(0f);
			this.m_explosionHolder.SetDepthOffset(-20f);
			this.m_explosionHolder.RemoveDrawHandler();
			this.m_youtubeUser = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.001f, "Button");
			this.m_youtubeUser.SetHeight(0.021f, RelativeTo.ScreenWidth);
			this.m_youtubeUser.SetVerticalAlign(0f);
			this.m_youtubeUser.SetSandColors();
			this.m_youtubeUser.SetReleaseAction(new Action(this.YoutubePressed));
			this.m_youtubeUser.SetDepthOffset(-10f);
			UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_youtubeUser, string.Empty);
			uihorizontalList.SetHeight(0.0055f, RelativeTo.ScreenWidth);
			uihorizontalList.SetMargins(-0.005f, -0.007f, 0f, 0f, RelativeTo.ScreenWidth);
			uihorizontalList.RemoveDrawHandler();
			UICanvas uicanvas = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
			uicanvas.SetHorizontalAlign(0f);
			uicanvas.SetSize(0.025f, 0.035f, RelativeTo.ScreenWidth);
			uicanvas.SetMargins(-0.005f, 0.005f, 0f, 0f, RelativeTo.ScreenHeight);
			uicanvas.RemoveDrawHandler();
			UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_youtube", null), true, true);
			uifittedSprite.SetHeight(1f, RelativeTo.ParentHeight);
			UIScaleToContentCanvas uiscaleToContentCanvas = new UIScaleToContentCanvas(uihorizontalList, string.Empty, true, false);
			uiscaleToContentCanvas.SetWidth(0.1f, RelativeTo.ScreenWidth);
			uiscaleToContentCanvas.SetHeight(0.025f, RelativeTo.ScreenWidth);
			uiscaleToContentCanvas.SetHorizontalAlign(0f);
			uiscaleToContentCanvas.RemoveDrawHandler();
			string youtubeName = this.m_creator.youtubeName;
			string text = "D53228";
			UIScaleToContentCanvas uiscaleToContentCanvas2 = uiscaleToContentCanvas;
			bool flag = false;
			string empty = string.Empty;
			string text2 = youtubeName;
			string font = PsFontManager.GetFont(PsFonts.KGSecondChances);
			float num = 0.4f;
			RelativeTo relativeTo = RelativeTo.ParentHeight;
			bool flag2 = true;
			string text3 = text;
			UITextbox uitextbox = new UITextbox(uiscaleToContentCanvas2, flag, empty, text2, font, num, relativeTo, flag2, Align.Left, Align.Top, text3, true, null);
			uitextbox.SetMaxRows(1);
			uitextbox.UseDotsWhenWrapping(true);
			uitextbox.SetHeight(0.55f, RelativeTo.ParentHeight);
			uitextbox.SetHorizontalAlign(0f);
			uitextbox.SetVerticalAlign(1f);
			uitextbox.SetDepthOffset(-5f);
			uitextbox.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
			UITextbox uitextbox2 = new UITextbox(uiscaleToContentCanvas, false, string.Empty, PsMetagameManager.NumberToString(this.m_creator.youtubeSubscriberCount) + " subscribers", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0985f, RelativeTo.ParentWidth, true, Align.Left, Align.Top, "79746c", true, null);
			uitextbox2.SetMaxRows(1);
			uitextbox2.UseDotsWhenWrapping(true);
			uitextbox2.SetHeight(0.45f, RelativeTo.ParentHeight);
			uitextbox2.SetHorizontalAlign(0f);
			uitextbox2.SetVerticalAlign(0f);
			uitextbox2.SetDepthOffset(-5f);
			uitextbox2.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
			if (!this.m_tournamentEvent.youtubeNitrosClaimed)
			{
				this.m_youtubeHolder = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
				this.m_youtubeHolder.SetSize(0.041f, 0.04f, RelativeTo.ScreenWidth);
				this.m_youtubeHolder.SetMargins(0.025f, 0f, 0f, 0f, RelativeTo.OwnWidth);
				this.m_youtubeHolder.RemoveDrawHandler();
				this.m_counter = new UICanvas(this.m_youtubeHolder, false, string.Empty, null, string.Empty);
				this.m_counter.SetSize(0.6f, 0.6f, RelativeTo.ParentHeight);
				this.m_counter.SetAlign(0f, 1f);
				this.m_counter.SetMargins(0.2f, 0.2f, 0.25f, 0.1f, RelativeTo.OwnWidth);
				this.m_counter.RemoveDrawHandler();
				int tournamentYoutuberNitroCount = PlayerPrefsX.GetTournamentYoutuberNitroCount();
				UIFittedText uifittedText = new UIFittedText(this.m_counter, false, string.Empty, "+" + tournamentYoutuberNitroCount, PsFontManager.GetFont(PsFonts.HurmeSemiBold), true, null, null);
				this.m_boosterIcon = new UIFittedSprite(this.m_youtubeHolder, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_plusboosts", null), true, true);
			}
		}
	}

	// Token: 0x060015E3 RID: 5603 RVA: 0x000E5098 File Offset: 0x000E3498
	private void YoutubePressed()
	{
		if (this.m_tournament != null && !string.IsNullOrEmpty(this.m_creator.youtubeId))
		{
			if (!this.m_tournamentEvent.youtubeNitrosClaimed)
			{
				this.m_tournamentEvent.youtubeNitrosClaimed = true;
				PsMetagameManager.m_playerStats.tournamentBoosters += PlayerPrefsX.GetTournamentYoutuberNitroCount();
				if (((this.GetRoot() as PsUIBasePopup).m_mainContent as PsUICenterTournament).m_footer.m_boosterButton != null)
				{
					this.BoosterFlying();
				}
				if (this.m_youtubeHolder != null)
				{
					UIComponent parent = this.m_youtubeHolder.m_parent.m_parent;
					this.m_youtubeHolder.Destroy();
					this.m_youtubeHolder = null;
					parent.Update();
				}
				Hashtable hashtable = new Hashtable();
				List<string> list = new List<string>();
				hashtable.Add("update", ClientTools.GeneratePlayerSetData(new Hashtable(), ref list));
				HttpC httpC = Tournament.ClaimYoutubeNitros(hashtable, new Action<HttpC>(this.YoutubeNitroClaimSucceed), new Action<HttpC>(this.YoutubeNitroClaimFailed), null);
				httpC.objectData = hashtable;
			}
			PsMetrics.YoutubePageOpened("tournamentLobby", this.m_creator.youtubeId, this.m_creator.youtubeName);
			Application.OpenURL("https://www.youtube.com/channel/" + this.m_creator.youtubeId);
		}
	}

	// Token: 0x060015E4 RID: 5604 RVA: 0x000E51D9 File Offset: 0x000E35D9
	private void YoutubeNitroClaimSucceed(HttpC _c)
	{
		this.m_tournamentEvent.youtubeNitrosClaimed = true;
		Debug.Log("Nitro claim succeed", null);
	}

	// Token: 0x060015E5 RID: 5605 RVA: 0x000E51F4 File Offset: 0x000E35F4
	private void YoutubeNitroClaimFailed(HttpC _c)
	{
		Debug.LogError("Nitro claim failed");
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, delegate
		{
			HttpC httpC = Tournament.ClaimYoutubeNitros(_c.objectData as Hashtable, new Action<HttpC>(this.YoutubeNitroClaimSucceed), new Action<HttpC>(this.YoutubeNitroClaimFailed), null);
			httpC.objectData = _c.objectData;
			return httpC;
		}, null);
	}

	// Token: 0x060015E6 RID: 5606 RVA: 0x000E5248 File Offset: 0x000E3648
	private void BoosterFlying()
	{
		PsUIBoosterButtonTournament boosterButton = ((this.GetRoot() as PsUIBasePopup).m_mainContent as PsUICenterTournament).m_footer.m_boosterButton;
		if (boosterButton != null && this.m_explosionHolder != null)
		{
			Vector3 vector = boosterButton.m_TC.transform.position;
			vector = this.m_explosionHolder.m_TC.transform.InverseTransformPoint(vector);
			Vector3 localPosition = this.m_explosionHolder.m_TC.transform.localPosition;
			int tournamentYoutuberNitroCount = PlayerPrefsX.GetTournamentYoutuberNitroCount();
			for (int i = 1; i <= tournamentYoutuberNitroCount; i++)
			{
				float num = 0f;
				float[] array = new float[] { -0.35f, 0.2f };
				float[] array2 = new float[] { -0.55f, -0.4f };
				float[] array3 = new float[] { -0.15f, 0.15f };
				float num2 = 0f;
				float num3 = 0f;
				float num4 = 0f;
				float num5 = 0f;
				float num6 = 0f;
				float num7 = 0f;
				this.CalculateControlMinMaxValues(array, array2, array3, ref num2, ref num3, ref num4, ref num5, ref num6, ref num7);
				float num8 = Random.Range(num2, num4);
				float num9 = Random.Range(num3, num5);
				float num10 = Random.Range(num6, num7);
				Vector3 normalized = (vector - localPosition).normalized;
				Vector3 vector2 = Vector3.Cross(normalized, -Vector3.forward);
				Vector3 vector3 = localPosition + normalized * (float)Screen.height * num8 + vector2 * (float)Screen.height * num10;
				Vector3 vector4 = vector + normalized * (float)Screen.height * num9 + vector2 * (float)Screen.height * num10;
				float num11 = 1f + Random.Range(-0.2f, 0.2f);
				UICanvas flyer = new UICanvas(this.m_explosionHolder, false, string.Empty, null, string.Empty);
				flyer.SetRogue();
				flyer.RemoveDrawHandler();
				flyer.SetSize(0.03f, 0.03f, RelativeTo.ScreenWidth);
				UIFittedSprite uifittedSprite = new UIFittedSprite(flyer, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("hud_tournament_boost", null), true, true);
				flyer.Update();
				TweenC tweenC = TweenS.AddTransformTween(flyer.m_TC, TweenedProperty.Alpha, TweenStyle.CubicIn, Vector3.one, Vector3.zero, 0.3f, num11 / 1.5f + num, true);
				TweenS.SetTweenAlphaProperties(tweenC, true, false, false, null);
				tweenC = TweenS.AddCurvedTransformTween(flyer.m_TC, TweenedProperty.Position, TweenStyle.Linear, vector, vector3, vector4, num11, num, true);
				TweenS.AddTweenEndEventListener(tweenC, delegate(TweenC _t)
				{
					if (flyer != null)
					{
						flyer.Destroy();
						flyer = null;
					}
					if (boosterButton != null)
					{
						boosterButton.AddBoost();
					}
				});
			}
		}
	}

	// Token: 0x060015E7 RID: 5607 RVA: 0x000E5554 File Offset: 0x000E3954
	public void CalculateControlMinMaxValues(float[] _control0, float[] _control1, float[] _height, ref float _min0, ref float _min1, ref float _max0, ref float _max1, ref float _minY, ref float _maxY)
	{
		float[] array = _control0;
		if (array == null)
		{
			array = new float[] { -0.45f, 0.35f };
		}
		float[] array2 = _control1;
		if (array2 == null)
		{
			array2 = new float[] { -0.95f, -0.75f };
		}
		float[] array3 = _height;
		if (array3 == null)
		{
			array3 = new float[] { -0.25f, 0.25f };
		}
		if (array.Length == 1)
		{
			_min0 = (_max0 = array[0]);
		}
		else
		{
			_min0 = array[0];
			_max0 = array[1];
		}
		if (array2.Length == 1)
		{
			_min1 = (_max1 = array2[0]);
		}
		else
		{
			_min1 = array2[0];
			_max1 = array2[1];
		}
		if (array3.Length == 1)
		{
			_minY = (_maxY = array3[0]);
		}
		else
		{
			_minY = array3[0];
			_maxY = array3[1];
		}
	}

	// Token: 0x060015E8 RID: 5608 RVA: 0x000E5630 File Offset: 0x000E3A30
	private void ToggleRightCorner()
	{
		if (this.firstTimeRight)
		{
			this.originalPosRight = this.m_rightContainerTweenParent.m_TC.transform.localPosition;
			this.targetXPosRight = this.originalPosRight.x + this.m_rightContainerTweenParent.m_actualWidth;
			this.firstTimeRight = false;
		}
		if (this.m_tweenRight != null)
		{
			TweenS.RemoveComponent(this.m_tweenRight);
			this.m_tweenRight = null;
		}
		if (!this.rightCornerHidden)
		{
			this.rightCornerHidden = true;
			this.m_tweenRight = TweenS.AddTransformTween(this.m_rightContainerTweenParent.m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, new Vector3(this.targetXPosRight, 0f, 0f), 0.3f, 0f, true);
			TweenS.AddTweenEndEventListener(this.m_tweenRight, delegate(TweenC _tween)
			{
				if (this.m_tweenRight != null)
				{
					TweenS.RemoveComponent(this.m_tweenRight);
					this.m_tweenRight = null;
				}
				this.rightCornerMoving = false;
			});
		}
		else
		{
			this.rightCornerHidden = false;
			this.m_tweenRight = TweenS.AddTransformTween(this.m_rightContainerTweenParent.m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, new Vector3(this.originalPosRight.x, 0f, 0f), 0.3f, 0f, true);
			TweenS.AddTweenEndEventListener(this.m_tweenRight, delegate(TweenC _tween)
			{
				if (this.m_tweenRight != null)
				{
					TweenS.RemoveComponent(this.m_tweenRight);
					this.m_tweenRight = null;
				}
				this.rightCornerMoving = false;
			});
		}
		this.rightCornerMoving = true;
	}

	// Token: 0x060015E9 RID: 5609 RVA: 0x000E576C File Offset: 0x000E3B6C
	public void ToggleLeftCorner()
	{
		if (this.firstTimeLeft)
		{
			this.originalPosLeft = this.m_leftChatDrawhandler.m_TC.transform.localPosition;
			this.targetXPosLeft = this.originalPosLeft.x + this.m_leftChatDrawhandler.m_actualWidth;
			this.firstTimeLeft = false;
		}
		if (this.m_tweenLeft != null)
		{
			TweenS.RemoveComponent(this.m_tweenLeft);
			this.m_tweenLeft = null;
		}
		if (!this.leftCornerHidden)
		{
			if (this.m_youtubeUser != null)
			{
				TweenS.AddTransformTween(this.m_youtubeUser.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, Vector3.one, 0.4f, 0f, true);
			}
			this.leftCornerHidden = true;
			this.m_tweenLeft = TweenS.AddTransformTween(this.m_leftChatDrawhandler.m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, new Vector3(this.originalPosLeft.x, 0f, 1f), 0.3f, 0f, true);
			TweenS.AddTweenEndEventListener(this.m_tweenLeft, delegate(TweenC _tween)
			{
				if (this.m_tweenLeft != null)
				{
					TweenS.RemoveComponent(this.m_tweenLeft);
					this.m_tweenLeft = null;
				}
				this.leftCornerMoving = false;
			});
		}
		else
		{
			if (this.m_youtubeUser != null)
			{
				TweenS.AddTransformTween(this.m_youtubeUser.m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, Vector3.zero, 0.4f, 0f, true);
			}
			this.leftCornerHidden = false;
			this.m_tweenLeft = TweenS.AddTransformTween(this.m_leftChatDrawhandler.m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, new Vector3(this.targetXPosLeft, 0f, 1f), 0.3f, 0f, true);
			TweenS.AddTweenEndEventListener(this.m_tweenLeft, delegate(TweenC _tween)
			{
				if (this.m_tweenLeft != null)
				{
					TweenS.RemoveComponent(this.m_tweenLeft);
					this.m_tweenLeft = null;
				}
				this.leftCornerMoving = false;
			});
		}
		this.leftCornerMoving = true;
	}

	// Token: 0x060015EA RID: 5610 RVA: 0x000E5904 File Offset: 0x000E3D04
	private void OpenPlayerProfilePopup()
	{
		if (this.m_tournamentEvent != null && !string.IsNullOrEmpty(this.m_tournamentEvent.ownerId))
		{
			SoundS.PlaySingleShot("/UI/Popup", Vector3.zero, 1f);
			this.m_profilePopup = new PsUIBasePopup(typeof(PsUICenterProfilePopup), null, null, null, true, true, InitialPage.Center, false, false, false);
			(this.m_profilePopup.m_mainContent as PsUICenterProfilePopup).SetUser(this.m_tournamentEvent.ownerId, false);
			this.m_profilePopup.SetAction("Exit", delegate
			{
				this.m_profilePopup.Destroy();
				if (this.m_followButtonHolder != null)
				{
					this.SetFollowButton(this.m_followButtonHolder);
				}
			});
			this.m_profilePopup.Update();
			TweenS.AddTransformTween(this.m_profilePopup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
		}
	}

	// Token: 0x060015EB RID: 5611 RVA: 0x000E59E8 File Offset: 0x000E3DE8
	public override void Step()
	{
		if (this.m_tournamentEvent != null && this.m_headerCreatorHolder != null && this.m_headerCreatorHolder.m_hit && this.m_tournamentEvent.ownerId != PlayerPrefsX.GetUserId())
		{
			this.m_headerCreatorHolder.m_hit = false;
			TouchAreaS.CancelAllTouches(null);
			this.OpenPlayerProfilePopup();
		}
		if (this.m_infoPopup == null && ((this.m_infoButton != null && this.m_infoButton.m_hit) || (this.m_headerTextHolder != null && this.m_headerTextHolder.m_hit)))
		{
			this.m_headerTextHolder.m_hit = false;
			this.m_infoButton.m_hit = false;
			CameraS.CreateBlur(null);
			this.m_infoPopup = new PsUIBasePopup(typeof(PsUITournamentInfo), null, null, null, true, true, InitialPage.Center, false, false, false);
			this.m_infoPopup.SetAction("Exit", delegate
			{
				CameraS.RemoveBlur();
				this.m_infoPopup.Destroy();
				this.m_infoPopup = null;
			});
		}
		int num = (int)Math.Ceiling((double)(PsState.m_activeGameLoop as PsGameLoopTournament).m_eventMessage.localEndTime - Main.m_EPOCHSeconds);
		if ((PsUITournamentHeader.m_hideUI && !this.rightCornerHidden) || (!PsUITournamentHeader.m_hideUI && this.rightCornerHidden))
		{
			this.ToggleRightCorner();
		}
		if (!this.m_tournamentEnded && num != this.m_timeLeft)
		{
			this.m_timeLeft = num;
			if (this.m_timeleftText != null && this.m_timeLeft >= 0)
			{
				string timeStringFromSeconds = PsMetagameManager.GetTimeStringFromSeconds(this.m_timeLeft, true, true);
				this.m_timeleftText.SetText(timeStringFromSeconds);
				if (this.m_timeLeft <= this.m_endingSoonTime && this.m_endingSoonColor != null && this.m_timeleftText.GetColor() != this.m_endingSoonColor)
				{
					this.m_timeleftText.SetColor(this.m_endingSoonColor, null);
				}
				else if (this.m_timeLeft > this.m_endingSoonTime && this.m_endingSoonColor != null && this.m_timeleftText.GetColor() == this.m_endingSoonColor)
				{
					this.m_timeleftText.SetColor("#FFF", null);
				}
			}
			else if (this.m_timeLeft < 0)
			{
				this.m_tournamentEnded = true;
				if (this.m_endsIn != null)
				{
					this.m_endsIn.Destroy();
					this.m_endsIn = null;
				}
				if (this.m_endingSoonColor != null && this.m_timeleftText.GetColor() == this.m_endingSoonColor)
				{
					this.m_timeleftText.SetColor("#FFF", null);
				}
				this.m_timeleftText.SetText(PsStrings.Get(StringID.TOUR_THIS_TOURNAMENT_IS_OVER));
				this.m_timerContainer.Update();
			}
		}
		if (this.m_totalPotTween != null && !this.m_totalPotTween.hasFinished)
		{
			int num2 = (int)this.m_totalPotTween.currentValue.x;
			string text = string.Concat(new string[]
			{
				"<color=#",
				this.m_coinPrice.GetColor(),
				">",
				string.Format("{0:n0}", num2).Replace(",", " "),
				"</color>"
			});
			TextMeshS.SetTextOptimized(this.m_coinPrice.m_tmc, text);
		}
		base.Step();
	}

	// Token: 0x060015EC RID: 5612 RVA: 0x000E5D44 File Offset: 0x000E4144
	public void UpdateTotalPot()
	{
		int totalPot = this.GetPot();
		if (totalPot == this.m_currentPot)
		{
			this.PotTimer(5f);
			return;
		}
		this.m_totalPotTween = TweenS.AddTween(this.m_TC.p_entity, TweenStyle.CubicInOut, (float)this.m_currentPot, (float)totalPot, 1f, this.GetRandomPotUpdateInterval());
		TweenS.AddTweenEndEventListener(this.m_totalPotTween, delegate(TweenC c)
		{
			this.m_totalPotTween = null;
			this.m_currentPot = totalPot;
			string text = string.Concat(new string[]
			{
				"<color=#",
				this.m_coinPrice.GetColor(),
				">",
				string.Format("{0:n0}", this.m_currentPot).Replace(",", " "),
				"</color>"
			});
			TextMeshS.SetTextOptimized(this.m_coinPrice.m_tmc, text);
			this.UpdateTotalPot();
		});
	}

	// Token: 0x060015ED RID: 5613 RVA: 0x000E5DD0 File Offset: 0x000E41D0
	public int GetPot()
	{
		TournamentInfo tournament = (PsState.m_activeGameLoop as PsGameLoopTournament).m_eventMessage.tournament;
		return Tournaments.GetTotalPot(tournament.globalParticipants, tournament.prizeCoins, tournament.globalNitroPot);
	}

	// Token: 0x060015EE RID: 5614 RVA: 0x000E5E09 File Offset: 0x000E4209
	public float GetRandomPotUpdateInterval()
	{
		return 2f + Random.value * 8f;
	}

	// Token: 0x060015EF RID: 5615 RVA: 0x000E5E1C File Offset: 0x000E421C
	public void PotTimer(float _delay = 5f)
	{
		TimerS.AddComponent(this.m_TC.p_entity, string.Empty, 0f, _delay, false, delegate(TimerC c)
		{
			TimerS.RemoveComponent(c);
			this.UpdateTotalPot();
		});
	}

	// Token: 0x04001899 RID: 6297
	private UIVerticalList m_tournamentInfo;

	// Token: 0x0400189A RID: 6298
	private UIHorizontalList m_timerContainer;

	// Token: 0x0400189B RID: 6299
	private UICanvas m_leftContainer;

	// Token: 0x0400189C RID: 6300
	private UICanvas m_mainContainer;

	// Token: 0x0400189D RID: 6301
	private UICanvas m_rightContainer;

	// Token: 0x0400189E RID: 6302
	private UICanvas m_nameContainer;

	// Token: 0x0400189F RID: 6303
	private UICanvas m_rightContainerTitle;

	// Token: 0x040018A0 RID: 6304
	private UICanvas m_bottomTextContainer;

	// Token: 0x040018A1 RID: 6305
	private UICanvas m_priceBanner;

	// Token: 0x040018A2 RID: 6306
	private UICanvas m_priceBannerBackground;

	// Token: 0x040018A3 RID: 6307
	private UICanvas m_coinPriceContainer;

	// Token: 0x040018A4 RID: 6308
	private UICanvas m_infoButtonCanvas;

	// Token: 0x040018A5 RID: 6309
	private UICanvas m_rightContainerTweenParent;

	// Token: 0x040018A6 RID: 6310
	private UICanvas m_leftContainerTweenParent;

	// Token: 0x040018A7 RID: 6311
	private UICanvas m_leftChatDrawhandler;

	// Token: 0x040018A8 RID: 6312
	private UICanvas m_headerTextHolder;

	// Token: 0x040018A9 RID: 6313
	private UICanvas m_headerCreatorHolder;

	// Token: 0x040018AA RID: 6314
	private UICanvas m_followButtonHolder;

	// Token: 0x040018AB RID: 6315
	private UIText m_endsIn;

	// Token: 0x040018AC RID: 6316
	private UIText m_cc;

	// Token: 0x040018AD RID: 6317
	private UIText m_timeleftText;

	// Token: 0x040018AE RID: 6318
	private UIText m_coinPrice;

	// Token: 0x040018AF RID: 6319
	private UIFittedText m_name;

	// Token: 0x040018B0 RID: 6320
	private UIFittedText m_titleText;

	// Token: 0x040018B1 RID: 6321
	private UIFittedText m_bottomText;

	// Token: 0x040018B2 RID: 6322
	private PlayerData m_owner;

	// Token: 0x040018B3 RID: 6323
	private UISprite m_coinIcon;

	// Token: 0x040018B4 RID: 6324
	private PsUIGenericButton m_youtubeUser;

	// Token: 0x040018B5 RID: 6325
	private PsUIFollowButton m_followButton;

	// Token: 0x040018B6 RID: 6326
	private UIRectSpriteButton m_infoButton;

	// Token: 0x040018B7 RID: 6327
	private PsGameLoopTournament m_tournament;

	// Token: 0x040018B8 RID: 6328
	private PlayerData m_creator;

	// Token: 0x040018B9 RID: 6329
	private string m_endingSoonColor;

	// Token: 0x040018BA RID: 6330
	private int m_endingSoonTime = 60;

	// Token: 0x040018BB RID: 6331
	private int m_timeLeft;

	// Token: 0x040018BC RID: 6332
	public static bool m_hideUI;

	// Token: 0x040018BD RID: 6333
	private bool m_tournamentEnded;

	// Token: 0x040018BE RID: 6334
	private TournamentInfo m_tournamentEvent;

	// Token: 0x040018BF RID: 6335
	private UICanvas m_youtubeHolder;

	// Token: 0x040018C0 RID: 6336
	private UICanvas m_counter;

	// Token: 0x040018C1 RID: 6337
	private UIFittedSprite m_boosterIcon;

	// Token: 0x040018C2 RID: 6338
	private UICanvas m_explosionHolder;

	// Token: 0x040018C3 RID: 6339
	private bool rightCornerMoving;

	// Token: 0x040018C4 RID: 6340
	public bool rightCornerHidden;

	// Token: 0x040018C5 RID: 6341
	private bool firstTimeRight = true;

	// Token: 0x040018C6 RID: 6342
	private float targetXPosRight;

	// Token: 0x040018C7 RID: 6343
	private Vector3 originalPosRight = Vector3.zero;

	// Token: 0x040018C8 RID: 6344
	private TweenC m_tweenRight;

	// Token: 0x040018C9 RID: 6345
	private bool leftCornerMoving;

	// Token: 0x040018CA RID: 6346
	public bool leftCornerHidden = true;

	// Token: 0x040018CB RID: 6347
	private bool firstTimeLeft = true;

	// Token: 0x040018CC RID: 6348
	private float targetXPosLeft;

	// Token: 0x040018CD RID: 6349
	private Vector3 originalPosLeft = Vector3.zero;

	// Token: 0x040018CE RID: 6350
	private TweenC m_tweenLeft;

	// Token: 0x040018CF RID: 6351
	private PsUIBasePopup m_infoPopup;

	// Token: 0x040018D0 RID: 6352
	private PsUIBasePopup m_profilePopup;

	// Token: 0x040018D1 RID: 6353
	private TweenC m_totalPotTween;

	// Token: 0x040018D2 RID: 6354
	private int m_currentPot;
}
