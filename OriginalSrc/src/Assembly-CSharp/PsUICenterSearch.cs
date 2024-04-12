using System;
using System.Collections.Generic;
using Server;
using UnityEngine;

// Token: 0x0200030A RID: 778
public class PsUICenterSearch : UIVerticalList
{
	// Token: 0x060016ED RID: 5869 RVA: 0x000F6554 File Offset: 0x000F4954
	public PsUICenterSearch(UIComponent _parent)
		: base(_parent, string.Empty)
	{
		PsUITabbedCreate.m_selectedTab = 3;
		this.m_opened = false;
		this.m_createAreas = false;
		this.m_searching = false;
		(this.m_parent as UIScrollableCanvas).SetScrollPosition(0f, 0f);
		this.m_players = new List<PsUIProfileBanner>();
		this.m_buttons = new List<PsUIProfileLevelButton>();
		this.SetVerticalAlign(1f);
		this.SetWidth(0.75f, RelativeTo.ParentWidth);
		this.SetMargins(0f, 0f, 0.05f, 0.06f, RelativeTo.ScreenHeight);
		this.SetSpacing(0.035f, RelativeTo.ScreenHeight);
		this.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.SetHorizontalAlign(0.5f);
		uihorizontalList.SetSpacing(0f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetHeight(0.16f, RelativeTo.ScreenHeight);
		this.m_input = new PsUIGenericInputField(uihorizontalList);
		this.m_input.SetTextAreaDrawhandler(new UIDrawDelegate(UIDrawHandlers.TextfieldOutlined));
		this.m_input.SetMinMaxCharacterCount(3, 40);
		this.m_input.ChangeTitleText(PsStrings.Get(StringID.SOCIAL_SEARCH_PLAYERS_INFO));
		this.m_input.SetCallbacks(new Action<string>(this.DoneWriting), null);
		this.m_input.SetVerticalAlign(1f);
		this.m_searchButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_searchButton.SetBlueColors(true);
		this.m_searchButton.SetIcon("menu_icon_search", 0.07f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_searchButton.SetVerticalAlign(0f);
		this.m_searchButton.SetDepthOffset(-15f);
		if (!string.IsNullOrEmpty(PsUICenterSearch.m_sharedLevel))
		{
			this.GetLevelById(PsUICenterSearch.m_sharedLevel);
			PsUICenterSearch.m_sharedLevel = string.Empty;
		}
		else if (!string.IsNullOrEmpty(PsUICenterSearch.m_sharedUser))
		{
			this.GetUserById(PsUICenterSearch.m_sharedUser);
			PsUICenterSearch.m_sharedUser = string.Empty;
		}
		else if (PsCaches.m_searchedLevelList.GetItemCount() > 0 || PsCaches.m_searchedPlayersList.GetItemCount() > 0)
		{
			this.m_foundPlayers = PsCaches.m_searchedPlayersList.GetContents();
			this.CreatePlayerArea(this.m_foundPlayers);
			this.m_foundLevels = PsCaches.m_searchedLevelList.GetContents();
			this.CreateLevelArea(this.m_foundLevels);
		}
	}

	// Token: 0x060016EE RID: 5870 RVA: 0x000F67C7 File Offset: 0x000F4BC7
	public void DoneWriting(string _input)
	{
		this.m_searchInput = _input;
		this.SearchContent();
	}

	// Token: 0x060016EF RID: 5871 RVA: 0x000F67D8 File Offset: 0x000F4BD8
	private void InitializeSearch()
	{
		this.m_searching = true;
		this.m_opened = false;
		this.m_openedBanner = null;
		this.m_currentIndex = 0;
		this.m_currentRowIndex = 0;
		this.m_rowCount = 0;
		this.m_buttons.Clear();
		PsCaches.m_searchedLevelList.Clear();
		PsCaches.m_searchedPlayersList.Clear();
		PsCaches.m_searchedPlayersLevelList.Clear();
		PsUICenterSearch.m_lastPlayerId = null;
		PsUICenterSearch.m_lastLevelId = null;
		this.DestroyChildren(1);
		this.m_players.Clear();
		this.m_foundPlayers = null;
		this.m_foundLevels = null;
		this.m_createLevelArea = false;
		new PsUILoadingAnimation(this, false);
		this.m_parent.Update();
	}

	// Token: 0x060016F0 RID: 5872 RVA: 0x000F6880 File Offset: 0x000F4C80
	public void GetLevelById(string _minigameId)
	{
		this.InitializeSearch();
		HttpC httpC = MiniGame.Get(_minigameId, new Action<PsMinigameMetaData>(this.GetLevelByIdSUCCEED), new Action<HttpC>(this.GetLevelByIdFAILED), new Action(this.GetUserContentErrorCallback));
		EntityManager.AddComponentToEntity(this.m_TC.p_entity, httpC);
	}

	// Token: 0x060016F1 RID: 5873 RVA: 0x000F68D0 File Offset: 0x000F4CD0
	private void GetLevelByIdSUCCEED(PsMinigameMetaData _metadata)
	{
		this.m_searching = false;
		this.m_input.SetText(_metadata.name);
		PsCaches.m_searchedLevelList.AddItem(_metadata.id, _metadata);
		this.m_createAreas = true;
		this.m_foundLevels = new PsMinigameMetaData[] { _metadata };
	}

	// Token: 0x060016F2 RID: 5874 RVA: 0x000F6920 File Offset: 0x000F4D20
	private void GetLevelByIdFAILED(HttpC _c)
	{
		Debug.Log("Search: GET MINIGAME BY ID FAILED", null);
		string networkError = ServerErrors.GetNetworkError(_c.www.error);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), networkError, delegate
		{
			HttpC httpC = MiniGame.Get((string)_c.objectData, new Action<PsMinigameMetaData>(this.GetLevelByIdSUCCEED), new Action<HttpC>(this.GetLevelByIdFAILED), new Action(this.GetUserContentErrorCallback));
			EntityManager.AddComponentToEntity(this.m_TC.p_entity, httpC);
			return httpC;
		}, null, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x060016F3 RID: 5875 RVA: 0x000F6984 File Offset: 0x000F4D84
	public void GetUserById(string _userId)
	{
		this.InitializeSearch();
		HttpC playerProfile = Player.GetPlayerProfile(_userId, new Action<PlayerData>(this.GetUserByIdSUCCEED), new Action<HttpC>(this.GetUserByIdFAILED), new Action(this.GetUserContentErrorCallback));
		EntityManager.AddComponentToEntity(this.m_TC.p_entity, playerProfile);
	}

	// Token: 0x060016F4 RID: 5876 RVA: 0x000F69D4 File Offset: 0x000F4DD4
	private void GetUserByIdSUCCEED(PlayerData _data)
	{
		this.m_searching = false;
		this.m_input.SetText(_data.name);
		PsCaches.m_searchedPlayersList.AddItem(_data.playerId, _data);
		this.m_createAreas = true;
		this.m_foundPlayers = new PlayerData[] { _data };
	}

	// Token: 0x060016F5 RID: 5877 RVA: 0x000F6A30 File Offset: 0x000F4E30
	private void GetUserByIdFAILED(HttpC _c)
	{
		Debug.Log("Search: GET USER BY ID FAILED", null);
		string networkError = ServerErrors.GetNetworkError(_c.www.error);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), networkError, delegate
		{
			HttpC playerProfile = Player.GetPlayerProfile((string)_c.objectData, new Action<PlayerData>(this.GetUserByIdSUCCEED), new Action<HttpC>(this.GetUserByIdFAILED), new Action(this.GetUserContentErrorCallback));
			EntityManager.AddComponentToEntity(this.m_TC.p_entity, playerProfile);
			return playerProfile;
		}, null, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x060016F6 RID: 5878 RVA: 0x000F6A94 File Offset: 0x000F4E94
	private void GetUserContentErrorCallback()
	{
		Debug.LogError("not found from server");
		this.m_searching = false;
		this.m_createAreas = true;
	}

	// Token: 0x060016F7 RID: 5879 RVA: 0x000F6AB0 File Offset: 0x000F4EB0
	public void SearchContent()
	{
		if (!string.IsNullOrEmpty(this.m_searchInput) && !this.m_searching)
		{
			this.InitializeSearch();
			HttpC httpC = Search.SearchGamesAndPlayers(this.m_searchInput, new Action<HttpC>(this.SearchSUCCEED), new Action<HttpC>(this.SearchFAILED), null);
			EntityManager.AddComponentToEntity(this.m_TC.p_entity, httpC);
		}
	}

	// Token: 0x060016F8 RID: 5880 RVA: 0x000F6B14 File Offset: 0x000F4F14
	private void SearchSUCCEED(HttpC _c)
	{
		this.m_searching = false;
		this.m_searchInput = string.Empty;
		this.m_input.SetText(this.m_searchInput);
		Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
		List<PlayerData> list = new List<PlayerData>();
		if (dictionary.ContainsKey("players"))
		{
			List<object> list2 = dictionary["players"] as List<object>;
			list.AddRange(ClientTools.ParseFriendList(list2));
		}
		if (dictionary.ContainsKey("tag"))
		{
			List<object> list3 = dictionary["tag"] as List<object>;
			list.AddRange(ClientTools.ParseFriendList(list3));
		}
		if (list.Count > 0)
		{
			for (int i = 0; i < list.Count; i++)
			{
				PsCaches.m_searchedPlayersList.AddItem(list[i].playerId, list[i]);
			}
			this.m_foundPlayers = list.ToArray();
		}
		else
		{
			this.m_foundPlayers = null;
		}
		PsMinigameMetaData[] array = null;
		if (dictionary.ContainsKey("games"))
		{
			List<object> list4 = dictionary["games"] as List<object>;
			array = new PsMinigameMetaData[list4.Count];
			for (int j = 0; j < list4.Count; j++)
			{
				Dictionary<string, object> dictionary2 = list4[j] as Dictionary<string, object>;
				PsMinigameMetaData psMinigameMetaData = ClientTools.ParseMinigameMetaData(dictionary2);
				array[j] = psMinigameMetaData;
				PsCaches.m_searchedLevelList.AddItem(psMinigameMetaData.id, psMinigameMetaData);
			}
		}
		this.m_createAreas = true;
		this.m_foundLevels = array;
	}

	// Token: 0x060016F9 RID: 5881 RVA: 0x000F6CAC File Offset: 0x000F50AC
	public void CreatePlayerArea(PlayerData[] _players)
	{
		this.m_playerList = new UIVerticalList(this, string.Empty);
		this.m_playerList.SetSpacing(0.02f, RelativeTo.ScreenHeight);
		this.m_playerList.RemoveDrawHandler();
		UIText uitext = new UIText(this.m_playerList, false, string.Empty, PsStrings.Get(StringID.SOCIAL_SEARCH_PLAYERS_FOUND), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.05f, RelativeTo.ScreenHeight, null, "313131");
		if (this.m_foundPlayers == null || this.m_foundPlayers.Length == 0)
		{
			this.m_createLevelArea = true;
			UIText uitext2 = new UIText(this.m_playerList, false, string.Empty, PsStrings.Get(StringID.SOCIAL_SEARCH_NO_PLAYERS_FOUND), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0385f, RelativeTo.ScreenHeight, null, null);
		}
	}

	// Token: 0x060016FA RID: 5882 RVA: 0x000F6D58 File Offset: 0x000F5158
	public void CreateLevelArea(PsMinigameMetaData[] _levels)
	{
		this.m_levelList = new UIVerticalList(this, string.Empty);
		this.m_levelList.SetWidth(0.65f, RelativeTo.ScreenWidth);
		this.m_levelList.SetSpacing(0.035f, RelativeTo.OwnWidth);
		this.m_levelList.SetMargins(0.04f, 0.04f, 0f, 0f, RelativeTo.ScreenHeight);
		this.m_levelList.RemoveDrawHandler();
		UIText uitext = new UIText(this.m_levelList, false, string.Empty, PsStrings.Get(StringID.SOCIAL_SEARCH_LEVELS_FOUND), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.05f, RelativeTo.ScreenHeight, null, "313131");
		if (_levels == null || _levels.Length == 0)
		{
			UITextbox uitextbox = new UITextbox(this.m_levelList, false, string.Empty, PsStrings.Get(StringID.SOCIAL_NO_LEVELS_FOUND), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.0385f, RelativeTo.ScreenHeight, false, Align.Center, Align.Middle, null, true, null);
			uitextbox.SetDepthOffset(-3f);
		}
		else
		{
			this.m_rowCount = Mathf.CeilToInt((float)_levels.Length / 3f);
		}
	}

	// Token: 0x060016FB RID: 5883 RVA: 0x000F6E50 File Offset: 0x000F5250
	public void CreateLevelBatch()
	{
		int num = Mathf.Min(this.m_rowCount, this.m_currentRowIndex + 2);
		for (int i = this.m_currentRowIndex; i < num; i++)
		{
			PsUIProfileLevelRow psUIProfileLevelRow = new PsUIProfileLevelRow(this.m_levelList, string.Empty, 3, i, this.m_foundLevels, typeof(PsGameLoopSocial), ref this.m_buttons, 0.035f, true, false, false);
			psUIProfileLevelRow.Update();
		}
		this.m_levelList.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_levelList.CalculateReferenceSizes();
		this.m_levelList.UpdateSize();
		this.m_levelList.ArrangeContents();
		this.m_levelList.UpdateDimensions();
		this.m_levelList.UpdateSize();
		this.m_levelList.ArrangeContents();
		this.SetHeight(1f, RelativeTo.ParentHeight);
		this.CalculateReferenceSizes();
		this.UpdateSize();
		this.ArrangeContents();
		base.UpdateDimensions();
		this.UpdateSize();
		this.UpdateAlign();
		this.UpdateChildrenAlign();
		this.ArrangeContents();
		(this.m_parent as UIScrollableCanvas).ArrangeContents();
		this.m_currentRowIndex = num;
		if (num == this.m_rowCount)
		{
			this.ScrollToPreviousPosition();
		}
	}

	// Token: 0x060016FC RID: 5884 RVA: 0x000F6F78 File Offset: 0x000F5378
	public void ScrollToPreviousPosition()
	{
		bool flag = false;
		if (!string.IsNullOrEmpty(PsUICenterSearch.m_lastPlayerId))
		{
			for (int i = 0; i < this.m_players.Count; i++)
			{
				if (this.m_players[i].m_user.playerId == PsUICenterSearch.m_lastPlayerId)
				{
					if (this.m_players[i].m_userLevels != null)
					{
						flag = true;
						this.m_players[i].ShowLevels(delegate
						{
							this.Arrange(true);
						});
						(this.m_parent as UIScrollableCanvas).SetScrollPositionToChild(this.m_players[i]);
						if (!string.IsNullOrEmpty(PsUICenterSearch.m_lastLevelId))
						{
							for (int j = 0; j < this.m_players[i].m_buttons.Count; j++)
							{
								if (PsUICenterSearch.m_lastLevelId == this.m_players[i].m_buttons[j].m_gameloop.GetGameId())
								{
									(this.m_parent as UIScrollableCanvas).SetScrollPositionToChild(this.m_players[i].m_buttons[j]);
									break;
								}
							}
						}
					}
					break;
				}
			}
		}
		if (!flag && !string.IsNullOrEmpty(PsUICenterSearch.m_lastLevelId))
		{
			for (int k = 0; k < this.m_buttons.Count; k++)
			{
				if (PsUICenterSearch.m_lastLevelId == this.m_buttons[k].m_gameloop.GetGameId())
				{
					(this.m_parent as UIScrollableCanvas).SetScrollPositionToChild(this.m_buttons[k]);
					break;
				}
			}
		}
	}

	// Token: 0x060016FD RID: 5885 RVA: 0x000F7138 File Offset: 0x000F5538
	private void SearchFAILED(HttpC _c)
	{
		Debug.Log("SEARCH PLAYERS AND MINIGAMES FAILED", null);
		string networkError = ServerErrors.GetNetworkError(_c.www.error);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), networkError, delegate
		{
			HttpC httpC = Search.SearchGamesAndPlayers(this.m_searchInput, new Action<HttpC>(this.SearchSUCCEED), new Action<HttpC>(this.SearchFAILED), null);
			EntityManager.AddComponentToEntity(this.m_TC.p_entity, httpC);
			return httpC;
		}, null, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x060016FE RID: 5886 RVA: 0x000F7184 File Offset: 0x000F5584
	private void CreateNextBatch()
	{
		int num = this.m_currentIndex + 10;
		num = Mathf.Min(num, this.m_foundPlayers.Length);
		for (int i = this.m_currentIndex; i < num; i++)
		{
			PsUIProfileBanner psUIProfileBanner = new PsUIProfileBanner(this.m_playerList, this.m_foundPlayers[i], true, false);
			psUIProfileBanner.SetHorizontalAlign(0.5f);
			psUIProfileBanner.m_userLevels = PsCaches.m_searchedPlayersLevelList.GetContent(this.m_foundPlayers[i].playerId);
			psUIProfileBanner.Update();
			this.m_players.Add(psUIProfileBanner);
		}
		this.m_playerList.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_playerList.CalculateReferenceSizes();
		this.m_playerList.UpdateSize();
		this.m_playerList.ArrangeContents();
		this.m_playerList.UpdateDimensions();
		this.m_playerList.UpdateSize();
		this.m_playerList.ArrangeContents();
		this.SetHeight(1f, RelativeTo.ParentHeight);
		this.CalculateReferenceSizes();
		this.UpdateSize();
		this.ArrangeContents();
		base.UpdateDimensions();
		this.UpdateSize();
		this.UpdateAlign();
		this.UpdateChildrenAlign();
		this.ArrangeContents();
		(this.m_parent as UIScrollableCanvas).ArrangeContents();
		if (num == this.m_foundPlayers.Length)
		{
			this.m_createLevelArea = true;
		}
		this.m_currentIndex = num;
	}

	// Token: 0x060016FF RID: 5887 RVA: 0x000F72DC File Offset: 0x000F56DC
	public override void Step()
	{
		bool flag = false;
		if (this.m_createAreas)
		{
			this.DestroyChildren(1);
			this.CreatePlayerArea(this.m_foundPlayers);
			this.CreateLevelArea(this.m_foundLevels);
			this.Update();
			this.m_createAreas = false;
		}
		if (this.m_openedBanner != null && !this.m_opened)
		{
			this.m_opened = true;
			Vector3 vector = (this.m_parent as UIScrollableCanvas).m_scrollTC.transform.position + new Vector3(0f, this.m_parent.m_actualHeight * 0.25f);
			float num = this.m_openedBanner.m_TC.transform.position.y - vector.y;
			Vector2 vector2 = (this.m_parent as UIScrollableCanvas).m_scrollTC.transform.position + new Vector2(0f, num);
			(this.m_parent as UIScrollableCanvas).ScrollToPosition(vector2, delegate
			{
				this.m_openedBanner.ShowLevels(delegate
				{
					this.Arrange(true);
				});
				this.Arrange(true);
			});
		}
		if (this.m_foundPlayers != null && this.m_foundPlayers.Length > 0 && this.m_currentIndex < this.m_foundPlayers.Length)
		{
			this.CreateNextBatch();
		}
		else if (this.m_createLevelArea && this.m_foundLevels != null && this.m_foundLevels.Length > 0 && this.m_currentRowIndex < this.m_rowCount)
		{
			this.CreateLevelBatch();
		}
		if (this.m_searchButton != null && this.m_searchButton.m_hit && !string.IsNullOrEmpty(this.m_searchInput) && !this.m_searching)
		{
			this.SearchContent();
		}
		bool flag2 = false;
		int num2 = -1;
		for (int i = 0; i < this.m_players.Count; i++)
		{
			if (this.m_players[i].m_banner.m_hit && this.m_players[i].m_user.publishedMinigameCount > 0)
			{
				SoundS.PlaySingleShot("/UI/ButtonNormal", Vector3.zero, 1f);
				if (!this.m_players[i].m_levelsShown)
				{
					this.m_openedBanner = this.m_players[i];
				}
				else if (this.m_players[i].m_levelsShown)
				{
					this.m_players[i].HideLevels();
					this.m_openedBanner = null;
				}
				this.m_opened = false;
				num2 = i;
				flag2 = true;
				break;
			}
		}
		if (flag2)
		{
			for (int j = 0; j < this.m_players.Count; j++)
			{
				if (this.m_players[j].m_levelsShown && j != num2)
				{
					this.m_players[j].HideLevels();
				}
			}
			flag = true;
		}
		if (flag)
		{
			this.Arrange(true);
		}
		base.Step();
	}

	// Token: 0x06001700 RID: 5888 RVA: 0x000F75F0 File Offset: 0x000F59F0
	public void Arrange(bool _updateScrollable = false)
	{
		this.m_playerList.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_playerList.CalculateReferenceSizes();
		this.m_playerList.UpdateSize();
		this.m_playerList.ArrangeContents();
		this.m_playerList.UpdateDimensions();
		this.m_playerList.UpdateSize();
		this.m_playerList.ArrangeContents();
		this.m_levelList.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_levelList.CalculateReferenceSizes();
		this.m_levelList.UpdateSize();
		this.m_levelList.ArrangeContents();
		this.m_levelList.UpdateDimensions();
		this.m_levelList.UpdateSize();
		this.m_levelList.ArrangeContents();
		this.SetHeight(1f, RelativeTo.ParentHeight);
		this.CalculateReferenceSizes();
		this.UpdateSize();
		this.ArrangeContents();
		base.UpdateDimensions();
		this.UpdateSize();
		this.UpdateAlign();
		this.ArrangeContents();
		if (_updateScrollable)
		{
			(this.m_parent as UIScrollableCanvas).CalculateReferenceSizes();
			(this.m_parent as UIScrollableCanvas).UpdateSize();
			(this.m_parent as UIScrollableCanvas).ArrangeContents();
		}
	}

	// Token: 0x06001701 RID: 5889 RVA: 0x000F770F File Offset: 0x000F5B0F
	public override void Destroy()
	{
		(this.m_parent as UIScrollableCanvas).ResetScroll();
		if (PsState.m_activeGameLoop == null)
		{
			PsUICenterSearch.m_lastPlayerId = null;
			PsUICenterSearch.m_lastLevelId = null;
		}
		base.Destroy();
	}

	// Token: 0x040019B3 RID: 6579
	private PlayerData[] m_foundPlayers;

	// Token: 0x040019B4 RID: 6580
	private PsMinigameMetaData[] m_foundLevels;

	// Token: 0x040019B5 RID: 6581
	private List<PsUIProfileBanner> m_players;

	// Token: 0x040019B6 RID: 6582
	private PsUIGenericInputField m_input;

	// Token: 0x040019B7 RID: 6583
	private PsUIGenericButton m_searchButton;

	// Token: 0x040019B8 RID: 6584
	private string m_searchInput;

	// Token: 0x040019B9 RID: 6585
	public static string m_lastPlayerId;

	// Token: 0x040019BA RID: 6586
	public static string m_lastLevelId;

	// Token: 0x040019BB RID: 6587
	public static string m_sharedLevel;

	// Token: 0x040019BC RID: 6588
	public static string m_sharedUser;

	// Token: 0x040019BD RID: 6589
	private UIVerticalList m_playerList;

	// Token: 0x040019BE RID: 6590
	private UIVerticalList m_levelList;

	// Token: 0x040019BF RID: 6591
	private int m_currentIndex;

	// Token: 0x040019C0 RID: 6592
	private bool m_createLevelArea;

	// Token: 0x040019C1 RID: 6593
	private PsUIProfileBanner m_openedBanner;

	// Token: 0x040019C2 RID: 6594
	private bool m_opened;

	// Token: 0x040019C3 RID: 6595
	private bool m_createAreas;

	// Token: 0x040019C4 RID: 6596
	private bool m_searching;

	// Token: 0x040019C5 RID: 6597
	private int m_currentRowIndex;

	// Token: 0x040019C6 RID: 6598
	private int m_rowCount;

	// Token: 0x040019C7 RID: 6599
	private List<PsUIProfileLevelButton> m_buttons;
}
