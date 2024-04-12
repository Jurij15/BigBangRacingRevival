using System;
using DeepLink;
using Server;
using UnityEngine;

// Token: 0x020003B0 RID: 944
public class PsUICreatorInfo : UIVerticalList
{
	// Token: 0x06001B07 RID: 6919 RVA: 0x0012E310 File Offset: 0x0012C710
	public PsUICreatorInfo(UIComponent _parent, bool _levelName = true, bool _speechBubble = false, bool _followbutton = false, bool _shareButton = false, bool _ratingScreen = false, bool _attentionYoutube = false)
		: base(_parent, "creatorInfoBanner")
	{
		this.m_creatorData = PsState.m_activeGameLoop.GetCreator();
		this.m_creatorId = PsState.m_activeGameLoop.GetCreatorId();
		this.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.SetHeight(0.15f + ((!_speechBubble) ? 0f : 0.12f), RelativeTo.ScreenHeight);
		uihorizontalList.SetSpacing(0.05f, RelativeTo.ScreenHeight);
		if (_speechBubble)
		{
			uihorizontalList.SetMargins(0f, 0f, 0.12f, 0f, RelativeTo.ScreenHeight);
		}
		uihorizontalList.RemoveDrawHandler();
		UIComponent uicomponent = new UIComponent(uihorizontalList, false, string.Empty, null, null, string.Empty);
		uicomponent.SetSize(1f, 1f, RelativeTo.ParentHeight);
		uicomponent.RemoveDrawHandler();
		if (_levelName)
		{
			uicomponent.SetMargins(0f, 0f, 0.025f, -0.025f, RelativeTo.ScreenShortest);
		}
		PsUIProfileImage psUIProfileImage = new PsUIProfileImage(uicomponent, false, string.Empty, PsState.m_activeGameLoop.GetFacebookId(), PsState.m_activeGameLoop.GetGamecenterId(), -1, true, false, true, 0.1f, 0.06f, "fff9e6", null, false, true);
		psUIProfileImage.SetDepthOffset(-10f);
		psUIProfileImage.SetSize(1f, 1f, RelativeTo.ParentHeight);
		TransformS.SetRotation(psUIProfileImage.m_TC, new Vector3(0f, 0f, 1.7f));
		if (_speechBubble)
		{
			float num = 0.05f;
			UIComponent uicomponent2 = new UIComponent(uicomponent, false, string.Empty, null, null, string.Empty);
			uicomponent2.SetMargins(0f, 0f, -(0.12f + num), 0.12f + num, RelativeTo.ScreenHeight);
			UICanvas uicanvas = new UICanvas(uicomponent2, false, string.Empty, null, string.Empty);
			uicanvas.SetWidth(0.55f, RelativeTo.ScreenShortest);
			uicanvas.SetHeight(0.12f, RelativeTo.ScreenHeight);
			uicanvas.SetMargins(0f, 0f, 0.01f, 0f, RelativeTo.ScreenHeight);
			uicanvas.SetHorizontalAlign(0f);
			uicanvas.SetDepthOffset(-10f);
			uicanvas.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.SpeechBubbleBottomLeft));
			UIText uitext = new UIText(uicanvas, false, string.Empty, PsStrings.Get(StringID.RATING_QUESTION), PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.035f, RelativeTo.ScreenHeight, "000000", null);
			uitext.SetVerticalAlign(0.5f);
		}
		UIVerticalList uiverticalList = new UIVerticalList(uihorizontalList, string.Empty);
		uiverticalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		uiverticalList.SetVerticalAlign(1f);
		if (_levelName)
		{
			UIText uitext2 = new UIText(uiverticalList, false, string.Empty, PsState.m_activeGameLoop.GetName(), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.07f, RelativeTo.ScreenHeight, "89FF2E", "313131");
			uitext2.SetShadowShift(new Vector2(0.1f, -0.1f), 1f);
			uitext2.SetHorizontalAlign(0f);
			if (_shareButton)
			{
				UICanvas uicanvas2 = new UICanvas(uitext2, false, string.Empty, null, string.Empty);
				uicanvas2.SetSize(1f, 1f, RelativeTo.ParentHeight);
				uicanvas2.SetHorizontalAlign(1f);
				uicanvas2.SetMargins(0.12f, -0.12f, 0f, 0f, RelativeTo.ScreenHeight);
				uicanvas2.RemoveDrawHandler();
				this.m_share = new PsUIGenericButton(uicanvas2, 0.25f, 0.25f, 0.005f, "Button");
				this.m_share.SetBlueColors(true);
				string text = "menu_icon_share";
				this.m_share.SetIcon(text, 0.04f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
				this.m_share.SetDepthOffset(-5f);
			}
		}
		UIComponent uicomponent3 = new UIHorizontalList(uiverticalList, string.Empty);
		uicomponent3.SetMargins(-0.1f, 0.1f, 0f, 0f, RelativeTo.ScreenHeight);
		uicomponent3.SetHorizontalAlign(0f);
		uicomponent3.RemoveDrawHandler();
		UIHorizontalList uihorizontalList2 = new UIHorizontalList(uicomponent3, string.Empty);
		uihorizontalList2.SetMargins(0.1f, 0.08f, 0f, 0f, RelativeTo.ScreenHeight);
		uihorizontalList2.SetHeight(0.065f, RelativeTo.ScreenHeight);
		uihorizontalList2.SetHorizontalAlign(0f);
		uihorizontalList2.SetSpacing(0.03f, RelativeTo.ScreenHeight);
		uihorizontalList2.RemoveDrawHandler();
		uihorizontalList2.SetDrawHandler(new UIDrawDelegate(this.TiltedDrawhandler));
		if (_levelName)
		{
			new UIText(uihorizontalList2, false, string.Empty, PsStrings.Get(StringID.CREATOR_TEXT), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, null, null);
		}
		string creatorName = PsState.m_activeGameLoop.GetCreatorName();
		this.m_name = new UIText(uihorizontalList2, false, string.Empty, creatorName, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.06f, RelativeTo.ScreenHeight, null, null);
		this.m_name.SetShadowShift(new Vector2(0.1f, -0.1f), 1f);
		bool flag = PsMetagameManager.m_friends.IsFriend(this.m_creatorId) || PsMetagameManager.m_friends.IsFollowee(this.m_creatorId);
		if (flag)
		{
			this.SetNameColorFriend();
		}
		else
		{
			this.SetNameColorStranger();
		}
		UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList2, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(PsState.m_activeGameLoop.GetCreatorCountryCode(), null), true, true);
		uifittedSprite.SetHeight(0.05f, RelativeTo.ScreenHeight);
		if (_followbutton && this.m_creatorId != PlayerPrefsX.GetUserId())
		{
			UIComponent uicomponent4 = new UIComponent(uihorizontalList2, false, string.Empty, null, null, string.Empty);
			uicomponent4.RemoveDrawHandler();
			uicomponent4.SetRogue();
			uicomponent4.SetHorizontalAlign(1f);
			uicomponent4.SetSize(1f, 1f, RelativeTo.ParentHeight);
			uicomponent4.SetMargins(1.2f, -1.2f, 0f, 0f, RelativeTo.OwnHeight);
			PsUIFollowButton psUIFollowButton = new PsUIFollowButton(uicomponent4, PsState.m_activeGameLoop.GetCreator(), 0.25f, 0.25f, 0.005f, 0.175f, RelativeTo.ScreenHeight);
			psUIFollowButton.SetHorizontalAlign(0f);
			psUIFollowButton.SetMargins(0.015f, 0.015f, 0.007f, 0.007f, RelativeTo.ScreenHeight);
			psUIFollowButton.SetCustomOkCallbackAction(delegate
			{
				PsMetagameManager.GetFriends(null, true);
			});
			psUIFollowButton.SetVisualFollowAction(new Action(this.SetNameColorFriend));
			psUIFollowButton.SetVisualUnfollowAction(new Action(this.SetNameColorStranger));
		}
		UIHorizontalList uihorizontalList3 = new UIHorizontalList(uiverticalList, "videoList");
		uihorizontalList3.SetSpacing(0.025f, RelativeTo.ScreenHeight);
		uihorizontalList3.RemoveDrawHandler();
		if (!string.IsNullOrEmpty(PsState.m_activeGameLoop.m_playerData.youtubeId) && _ratingScreen)
		{
			string youtubeName = PsState.m_activeGameLoop.m_playerData.youtubeName;
			int youtubeSubscriberCount = PsState.m_activeGameLoop.m_playerData.youtubeSubscriberCount;
			UIFixedYoutubeButton uifixedYoutubeButton = new UIFixedYoutubeButton(uihorizontalList3, youtubeName, youtubeSubscriberCount, 0.3f, RelativeTo.ScreenWidth, 0.25f, 0.25f, 0.001f, "YoutubeButton");
			uifixedYoutubeButton.SetHeight(0.06f, RelativeTo.ScreenHeight);
			uifixedYoutubeButton.SetReleaseAction(new Action(this.YoutubePressed));
			PsMetrics.YoutubeLinkOffered("ratingScreen", PsState.m_activeGameLoop.m_playerData.youtubeId, PsState.m_activeGameLoop.m_playerData.youtubeName, PsState.m_activeGameLoop.m_playerData.playerId, PsState.m_activeGameLoop.m_playerData.name);
		}
		if (!string.IsNullOrEmpty(PsState.m_activeGameLoop.m_minigameMetaData.videoUrl) && _ratingScreen)
		{
			PsUIGenericButton psUIGenericButton = new PsUIGenericButton(uihorizontalList3, 0.25f, 0.25f, 0.005f, "Button");
			psUIGenericButton.SetHeight(0.075f, RelativeTo.ScreenHeight);
			psUIGenericButton.SetHorizontalAlign(0f);
			psUIGenericButton.SetSandColors();
			psUIGenericButton.SetReleaseAction(new Action(this.OpenVideoURL));
			string text2 = PsStrings.Get(StringID.CHALLENGE_BUTTON_WATCH_VIDEO);
			string text3 = "D53228";
			PsUIGenericButton psUIGenericButton2 = psUIGenericButton;
			bool flag2 = false;
			string empty = string.Empty;
			string text4 = text2;
			string font = PsFontManager.GetFont(PsFonts.KGSecondChances);
			float num2 = 0.025f;
			RelativeTo relativeTo = RelativeTo.ScreenHeight;
			bool flag3 = false;
			Align align = Align.Center;
			string text5 = text3;
			UITextbox uitextbox = new UITextbox(psUIGenericButton2, flag2, empty, text4, font, num2, relativeTo, flag3, align, Align.Top, text5, true, null);
			uitextbox.SetMaxRows(2);
			uitextbox.UseDotsWhenWrapping(true);
			uitextbox.SetHeight(0.035f, RelativeTo.ScreenHeight);
			uitextbox.SetWidth(0.2f, RelativeTo.ScreenHeight);
		}
		this.m_created = true;
	}

	// Token: 0x06001B08 RID: 6920 RVA: 0x0012EB60 File Offset: 0x0012CF60
	public void OpenVideoURL()
	{
		if (!string.IsNullOrEmpty(PsState.m_activeGameLoop.m_minigameMetaData.videoUrl))
		{
			PsMetrics.LevelMakingOfVideoOpened(PsState.m_activeGameLoop.m_minigameMetaData.id, PsState.m_activeGameLoop.m_minigameMetaData.name, PsState.m_activeGameLoop.m_playerData.playerId, PsState.m_activeGameLoop.m_playerData.name, PsState.m_activeGameLoop.m_playerData.youtubeId, PsState.m_activeGameLoop.m_playerData.youtubeName);
			Application.OpenURL(PsState.m_activeGameLoop.m_minigameMetaData.videoUrl);
		}
	}

	// Token: 0x06001B09 RID: 6921 RVA: 0x0012EBFC File Offset: 0x0012CFFC
	public void YoutubePressed()
	{
		if (PsState.m_activeGameLoop != null && !string.IsNullOrEmpty(PsState.m_activeGameLoop.m_playerData.youtubeId))
		{
			PsMetrics.YoutubePageOpened("ratingScreen", PsState.m_activeGameLoop.m_playerData.youtubeId, PsState.m_activeGameLoop.m_playerData.youtubeName, PsState.m_activeGameLoop.m_playerData.playerId, PsState.m_activeGameLoop.m_playerData.name);
			Application.OpenURL("https://www.youtube.com/channel/" + PsState.m_activeGameLoop.m_playerData.youtubeId);
		}
	}

	// Token: 0x06001B0A RID: 6922 RVA: 0x0012EC90 File Offset: 0x0012D090
	private void SetNameColorFriend()
	{
		if (this.m_name != null)
		{
			this.m_name.SetColor("#89FF2E", null);
		}
	}

	// Token: 0x06001B0B RID: 6923 RVA: 0x0012ECAE File Offset: 0x0012D0AE
	private void SetNameColorStranger()
	{
		if (this.m_name != null)
		{
			this.m_name.SetColor("#61CEFF", null);
		}
	}

	// Token: 0x06001B0C RID: 6924 RVA: 0x0012ECCC File Offset: 0x0012D0CC
	private void ChangeFollowState()
	{
		this.m_toFollow = !this.m_toFollow;
		if (this.m_toFollow)
		{
			this.m_followButton.m_UItext.SetText(PsStrings.Get(StringID.UNFOLLOW));
			this.SetNameColorFriend();
		}
		else
		{
			this.m_followButton.m_UItext.SetText(PsStrings.Get(StringID.FOLLOW));
			this.SetNameColorStranger();
		}
	}

	// Token: 0x06001B0D RID: 6925 RVA: 0x0012ED34 File Offset: 0x0012D134
	public HttpC SendFollowData()
	{
		HttpC httpC = null;
		if (this.m_isFollowing && !this.m_toFollow && (PsMetagameManager.m_friends.IsFriend(this.m_creatorId) || PsMetagameManager.m_friends.IsFollowee(this.m_creatorId)))
		{
			Debug.Log("Send FollowData: unfollow", null);
			httpC = Player.UnFollow(this.m_creatorId, new Action<HttpC>(this.UnfollowDataSucceed), new Action<HttpC>(this.FollowDataFailed), null);
		}
		else if (!this.m_isFollowing && this.m_toFollow && !PsMetagameManager.m_friends.IsFriend(this.m_creatorId) && !PsMetagameManager.m_friends.IsFollowee(this.m_creatorId))
		{
			Debug.Log("Send FollowData: follow", null);
			httpC = Player.Follow(this.m_creatorId, null, new Action<HttpC>(this.FollowDataSucceed), new Action<HttpC>(this.FollowDataFailed), null);
		}
		return httpC;
	}

	// Token: 0x06001B0E RID: 6926 RVA: 0x0012EE2A File Offset: 0x0012D22A
	public void FollowDataFailed(HttpC _c)
	{
		Debug.Log("FollowData failed", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => this.SendFollowData(), null);
	}

	// Token: 0x06001B0F RID: 6927 RVA: 0x0012EE59 File Offset: 0x0012D259
	public void FollowDataSucceed(HttpC _c)
	{
		PsMetagameManager.m_friends.FollowPlayer(this.m_creatorData);
		Debug.Log("FollowData succeed", null);
	}

	// Token: 0x06001B10 RID: 6928 RVA: 0x0012EE76 File Offset: 0x0012D276
	public void UnfollowDataSucceed(HttpC _c)
	{
		PsMetagameManager.m_friends.UnfollowPlayer(this.m_creatorData.playerId);
		Debug.Log("Unfollow succeed", null);
	}

	// Token: 0x06001B11 RID: 6929 RVA: 0x0012EE98 File Offset: 0x0012D298
	public void TiltedDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		float num = 0.625f * (float)Screen.height * 0.02f;
		float actualWidth = _c.m_actualWidth;
		float actualHeight = _c.m_actualHeight;
		float num2 = _c.m_actualHeight * 0.04f;
		Vector2 zero = Vector2.zero;
		Vector2[] array = new Vector2[41];
		Vector2[] arc = DebugDraw.GetArc(num2, 10, 85f, 255f, new Vector2(actualWidth * 0.5f - num2, actualHeight * -0.5f + num2) + zero);
		Vector2[] arc2 = DebugDraw.GetArc(num2, 10, 60f, 180f, new Vector2(actualWidth * -0.5f + num2, actualHeight * -0.5f + num2) + zero);
		Vector2[] arc3 = DebugDraw.GetArc(num2, 10, 85f, 75f, new Vector2(actualWidth * -0.5f + num2 + num, actualHeight * 0.5f - num2) + zero);
		Vector2[] arc4 = DebugDraw.GetArc(num2, 10, 60f, 0f, new Vector2(actualWidth * 0.5f - num2 + num, actualHeight * 0.5f - num2) + zero);
		arc.CopyTo(array, 0);
		arc2.CopyTo(array, 10);
		arc3.CopyTo(array, 20);
		arc4.CopyTo(array, 30);
		array[array.Length - 1] = arc[0];
		Color color = DebugDraw.HexToColor("#000000");
		color.a = 0.95f;
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 3f + new Vector3(0f, 0f, 0f), array, (float)Screen.height * 0.0075f, color, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		GGData ggdata = new GGData(array);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 5f + new Vector3(0f, 0f, 0f), ggdata, color, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x06001B12 RID: 6930 RVA: 0x0012F0D0 File Offset: 0x0012D4D0
	public override void Step()
	{
		if (this.m_followButton != null && this.m_followButton.m_hit)
		{
			this.ChangeFollowState();
		}
		else if (this.m_share != null && this.m_share.m_hit && PsState.m_activeGameLoop != null)
		{
			TouchAreaS.CancelAllTouches(null);
			string levelLinkUrl = PsUrlLaunch.GetLevelLinkUrl(PsState.m_activeGameLoop.m_minigameId, PsState.m_activeGameLoop.GetName(), PsState.m_activeGameLoop.GetCreatorName());
			Share.ShareTextOnPlatform(levelLinkUrl);
			PsMetrics.LevelShared("ratingScreen", PsState.m_activeGameLoop.m_minigameId, PsState.m_activeGameLoop.GetName(), PsState.m_activeGameLoop.GetCreatorId(), PsState.m_activeGameLoop.GetCreatorName());
		}
		base.Step();
	}

	// Token: 0x04001D7C RID: 7548
	private UIText m_name;

	// Token: 0x04001D7D RID: 7549
	private PsUIGenericButton m_share;

	// Token: 0x04001D7E RID: 7550
	private PsUIGenericButton m_followButton;

	// Token: 0x04001D7F RID: 7551
	private PsUIGenericButton m_youtubeUser;

	// Token: 0x04001D80 RID: 7552
	private bool m_isFollowing;

	// Token: 0x04001D81 RID: 7553
	private bool m_toFollow;

	// Token: 0x04001D82 RID: 7554
	private bool m_created;

	// Token: 0x04001D83 RID: 7555
	private string m_creatorId;

	// Token: 0x04001D84 RID: 7556
	private PlayerData m_creatorData;

	// Token: 0x04001D85 RID: 7557
	private const float BubbleHeight = 0.12f;
}
