using System;
using DeepLink;
using Server;
using UnityEngine;

// Token: 0x020003BC RID: 956
public class PsUITournamentCreatorInfo : UIVerticalList
{
	// Token: 0x06001B42 RID: 6978 RVA: 0x00130544 File Offset: 0x0012E944
	public PsUITournamentCreatorInfo(UIComponent _parent, bool _levelName = false, bool _followbutton = false, bool _shareButton = false, bool _ratingScreen = false, bool _attentionYoutube = false)
		: base(_parent, "creatorInfoBanner")
	{
		this.m_creatorData = PsState.m_activeGameLoop.GetCreator();
		this.m_creatorId = PsState.m_activeGameLoop.GetCreatorId();
		this.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.SetSpacing(0.05f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		UIComponent uicomponent = new UIComponent(uihorizontalList, false, string.Empty, null, null, string.Empty);
		uicomponent.SetSize(1f, 1f, RelativeTo.ParentHeight);
		uicomponent.RemoveDrawHandler();
		PsUIProfileImage psUIProfileImage = new PsUIProfileImage(uicomponent, false, string.Empty, PsState.m_activeGameLoop.GetFacebookId(), PsState.m_activeGameLoop.GetGamecenterId(), -1, true, false, true, 0.1f, 0.06f, "fff9e6", null, false, true);
		psUIProfileImage.SetDepthOffset(-10f);
		psUIProfileImage.SetSize(1f, 1f, RelativeTo.ParentHeight);
		TransformS.SetRotation(psUIProfileImage.m_TC, new Vector3(0f, 0f, 1.7f));
		UIComponent uicomponent2 = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
		uicomponent2.SetWidth(0.45f, RelativeTo.ScreenHeight);
		uicomponent2.SetMargins(-0.1f, 0.1f, 0f, 0f, RelativeTo.ScreenHeight);
		uicomponent2.SetHorizontalAlign(0f);
		uicomponent2.SetVerticalAlign(1f);
		uicomponent2.RemoveDrawHandler();
		UIHorizontalList uihorizontalList2 = new UIHorizontalList(uicomponent2, string.Empty);
		uihorizontalList2.SetMargins(0.08f, 0.01f, 0.005f, 0.005f, RelativeTo.ScreenHeight);
		uihorizontalList2.SetHeight(0.35f, RelativeTo.ParentHeight);
		uihorizontalList2.SetAlign(0f, 1f);
		uihorizontalList2.SetSpacing(0.2f, RelativeTo.OwnHeight);
		uihorizontalList2.RemoveDrawHandler();
		uihorizontalList2.SetDrawHandler(new UIDrawDelegate(this.TiltedDrawhandler));
		string creatorName = PsState.m_activeGameLoop.GetCreatorName();
		this.m_name = new UITextbox(uihorizontalList2, false, string.Empty, creatorName, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.9f, RelativeTo.ParentHeight, true, Align.Left, Align.Top, null, true, null);
		this.m_name.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_name.UseDotsWhenWrapping(true);
		this.m_name.SetMaxRows(1);
		this.m_name.SetShadowShift(new Vector2(0.1f, -0.1f), 1f);
		this.m_name.SetHorizontalAlign(0f);
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
		uifittedSprite.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_created = true;
	}

	// Token: 0x06001B43 RID: 6979 RVA: 0x00130814 File Offset: 0x0012EC14
	public void OpenVideoURL()
	{
		if (!string.IsNullOrEmpty(PsState.m_activeGameLoop.m_minigameMetaData.videoUrl))
		{
			PsMetrics.LevelMakingOfVideoOpened(PsState.m_activeGameLoop.m_minigameMetaData.id, PsState.m_activeGameLoop.m_minigameMetaData.name, PsState.m_activeGameLoop.m_playerData.playerId, PsState.m_activeGameLoop.m_playerData.name, PsState.m_activeGameLoop.m_playerData.youtubeId, PsState.m_activeGameLoop.m_playerData.youtubeName);
			Application.OpenURL(PsState.m_activeGameLoop.m_minigameMetaData.videoUrl);
		}
	}

	// Token: 0x06001B44 RID: 6980 RVA: 0x001308B0 File Offset: 0x0012ECB0
	public void YoutubePressed()
	{
		if (!string.IsNullOrEmpty(PsState.m_activeGameLoop.m_playerData.youtubeId))
		{
			PsMetrics.YoutubePageOpened("ratingScreen", PsState.m_activeGameLoop.m_playerData.youtubeId, PsState.m_activeGameLoop.m_playerData.youtubeName, PsState.m_activeGameLoop.m_playerData.playerId, PsState.m_activeGameLoop.m_playerData.name);
			Application.OpenURL("https://www.youtube.com/channel/" + PsState.m_activeGameLoop.m_playerData.youtubeId);
		}
	}

	// Token: 0x06001B45 RID: 6981 RVA: 0x0013093A File Offset: 0x0012ED3A
	private void SetNameColorFriend()
	{
		if (this.m_name != null)
		{
			this.m_name.SetColor("#89FF2E", null);
		}
	}

	// Token: 0x06001B46 RID: 6982 RVA: 0x00130958 File Offset: 0x0012ED58
	private void SetNameColorStranger()
	{
		if (this.m_name != null)
		{
			this.m_name.SetColor("#61CEFF", null);
		}
	}

	// Token: 0x06001B47 RID: 6983 RVA: 0x00130978 File Offset: 0x0012ED78
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

	// Token: 0x06001B48 RID: 6984 RVA: 0x001309E0 File Offset: 0x0012EDE0
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

	// Token: 0x06001B49 RID: 6985 RVA: 0x00130AD6 File Offset: 0x0012EED6
	public void FollowDataFailed(HttpC _c)
	{
		Debug.Log("FollowData failed", null);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _c.www, () => this.SendFollowData(), null);
	}

	// Token: 0x06001B4A RID: 6986 RVA: 0x00130B05 File Offset: 0x0012EF05
	public void FollowDataSucceed(HttpC _c)
	{
		PsMetagameManager.m_friends.FollowPlayer(this.m_creatorData);
		Debug.Log("FollowData succeed", null);
	}

	// Token: 0x06001B4B RID: 6987 RVA: 0x00130B22 File Offset: 0x0012EF22
	public void UnfollowDataSucceed(HttpC _c)
	{
		PsMetagameManager.m_friends.UnfollowPlayer(this.m_creatorData.playerId);
		Debug.Log("Unfollow succeed", null);
	}

	// Token: 0x06001B4C RID: 6988 RVA: 0x00130B44 File Offset: 0x0012EF44
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

	// Token: 0x06001B4D RID: 6989 RVA: 0x00130D7C File Offset: 0x0012F17C
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

	// Token: 0x04001DA7 RID: 7591
	private UITextbox m_name;

	// Token: 0x04001DA8 RID: 7592
	private PsUIGenericButton m_share;

	// Token: 0x04001DA9 RID: 7593
	private PsUIGenericButton m_followButton;

	// Token: 0x04001DAA RID: 7594
	private PsUIGenericButton m_youtubeUser;

	// Token: 0x04001DAB RID: 7595
	private bool m_isFollowing;

	// Token: 0x04001DAC RID: 7596
	private bool m_toFollow;

	// Token: 0x04001DAD RID: 7597
	private bool m_created;

	// Token: 0x04001DAE RID: 7598
	private string m_creatorId;

	// Token: 0x04001DAF RID: 7599
	private PlayerData m_creatorData;
}
