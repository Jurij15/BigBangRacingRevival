using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200027A RID: 634
public class PsUITournamentLeaderboardEntry : UICanvas
{
	// Token: 0x06001323 RID: 4899 RVA: 0x000BD304 File Offset: 0x000BB704
	public PsUITournamentLeaderboardEntry(UIComponent _parent, HighscoreDataEntry _data, int _position, int _topTimeScore)
		: base(_parent, true, "LeaderboardEntry", null, string.Empty)
	{
		this.m_TAC.m_letTouchesThrough = true;
		this.m_touchAreaSizeMultipler = 0.8f;
		this.m_highscoreData = _data;
		this.m_position = _position;
		this.m_player = _data.playerId == PlayerPrefsX.GetUserId();
		this.m_currentTimescore = _data.time;
		this.m_topTimeScore = _topTimeScore;
		this.CreateUI();
	}

	// Token: 0x06001324 RID: 4900 RVA: 0x000BD38C File Offset: 0x000BB78C
	public virtual void CreateUI()
	{
		this.SetDrawHandler(new UIDrawDelegate(this.DrawHandlerSprite));
		UIComponent uicomponent = new UIComponent(this, false, string.Empty, null, null, string.Empty);
		uicomponent.SetHeight(1f, RelativeTo.ParentHeight);
		uicomponent.SetWidth(0f, RelativeTo.ParentWidth);
		uicomponent.RemoveDrawHandler();
		uicomponent.SetHorizontalAlign(0.125f);
		this.m_uiPosition = new UIText(uicomponent, false, string.Empty, this.m_position + ".", PsFontManager.GetFont(PsFonts.HurmeSemiBoldMN), 0.85f, RelativeTo.ParentHeight, "#FFFFFF", null);
		this.m_uiPosition.m_tmc.m_horizontalAlign = Align.Right;
		this.m_uiPosition.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
		this.m_uiPosition.SetDepthOffset(-1f);
		UIComponent uicomponent2 = new UIComponent(this, false, string.Empty, null, null, string.Empty);
		uicomponent2.SetHeight(0.7f, RelativeTo.ParentHeight);
		uicomponent2.SetWidth(0f, RelativeTo.ParentWidth);
		uicomponent2.RemoveDrawHandler();
		uicomponent2.SetHorizontalAlign(0.295f);
		string text = ((!PsMetagameManager.IsFriend(this.m_highscoreData.playerId)) ? "#ffffff" : "#ABFF2A");
		this.m_uiName = new UIText(uicomponent2, false, string.Empty, this.m_highscoreData.name, PsFontManager.GetFont(PsFonts.HurmeSemiBoldMN), 1f, RelativeTo.ParentHeight, text, null);
		this.m_uiName.m_tmc.m_horizontalAlign = Align.Left;
		this.m_uiName.SetDepthOffset(-1f);
		this.m_uiName.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
		UIComponent uicomponent3 = new UIComponent(this, false, string.Empty, null, null, string.Empty);
		uicomponent3.SetHeight(1f, RelativeTo.ParentHeight);
		uicomponent3.SetWidth(0f, RelativeTo.ParentWidth);
		uicomponent3.RemoveDrawHandler();
		uicomponent3.SetHorizontalAlign(1f);
		string timeString = this.GetTimeString();
		string font = PsFontManager.GetFont(PsFonts.HurmeBoldMN);
		this.m_uiTime = new UIText(uicomponent3, false, string.Empty, timeString, font, 0.85f, RelativeTo.ParentHeight, "#FFFFFF", null);
		this.m_uiTime.m_tmc.m_horizontalAlign = Align.Right;
		this.m_uiTime.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
		this.m_uiTime.SetDepthOffset(-1f);
		this.m_prizeDisabled = this.m_highscoreData.time == 0 || this.m_highscoreData.time == int.MaxValue;
		UIComponent uicomponent4 = uicomponent3;
		int position = this.m_position;
		bool prizeDisabled = this.m_prizeDisabled;
		this.m_prize = this.GetPrizeUI(uicomponent4, position, true, string.Empty, true, prizeDisabled);
		string timeDifference = this.GetTimeDifference(this.m_topTimeScore);
		this.m_difference = new UIText(uicomponent3, false, "Gap", timeDifference, PsFontManager.GetFont(PsFonts.HurmeSemiBoldMN), 0.85f, RelativeTo.ParentHeight, "#FFFFFF", null);
		this.m_difference.m_tmc.m_horizontalAlign = Align.Right;
		this.m_difference.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
		this.m_difference.SetDepthOffset(-1f);
		this.m_difference.m_TC.transform.localRotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
		if (!string.IsNullOrEmpty(this.m_highscoreData.facebookId))
		{
			this.CreateProfilePic();
		}
		this.m_countryCode = this.m_highscoreData.country;
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame(this.m_countryCode, "_alien");
		this.m_flag = new UIFittedSprite(this, false, string.Empty, PsState.m_uiSheet, frame, true, true);
		this.m_flag.SetHorizontalAlign(0.225f);
		this.m_flag.SetDepthOffset(-5f);
		this.m_flag.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_rollingInfoElements = new UIComponent[] { this.m_uiTime, this.m_prize, this.m_difference };
	}

	// Token: 0x06001325 RID: 4901 RVA: 0x000BD7A8 File Offset: 0x000BBBA8
	public void SetHost(bool _host, bool _update = false)
	{
		if (_host && !this.m_host)
		{
			this.m_profilePic = null;
			this.DestroyChildren();
			this.m_rollingInfoElements = new UIComponent[0];
			this.m_prize = null;
			this.m_triangle = null;
			this.CreateTournamentHostInfo();
			this.SetDrawHandler(new UIDrawDelegate(this.DrawHandlerHost));
			if (_update)
			{
				this.Update();
			}
		}
		if (!_host && this.m_host)
		{
			this.m_profilePic = null;
			this.DestroyChildren();
			this.m_triangle = null;
			this.CreateUI();
			this.SetDrawHandler(new UIDrawDelegate(this.DrawHandlerSprite));
			if (_update)
			{
				this.Update();
			}
		}
		this.m_host = _host;
	}

	// Token: 0x06001326 RID: 4902 RVA: 0x000BD864 File Offset: 0x000BBC64
	private void CreateTournamentHostInfo()
	{
		string text = PsStrings.Get(StringID.TOUR_TOURNAMENT_HOST);
		UIText uitext = new UIText(this, false, "Host text", text, PsFontManager.GetFont(PsFonts.HurmeSemiBoldMN), 0.55f, RelativeTo.ParentHeight, "#000000", null);
		uitext.SetDepthOffset(-1f);
		uitext.SetHorizontalAlign(0.05f);
		uitext.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
		UIComponent uicomponent = new UIComponent(this, false, string.Empty, null, null, string.Empty);
		uicomponent.SetHeight(0.7f, RelativeTo.ParentHeight);
		uicomponent.SetWidth(0f, RelativeTo.ParentWidth);
		uicomponent.RemoveDrawHandler();
		uicomponent.SetHorizontalAlign(0.295f);
		this.m_uiName = new UIText(uicomponent, false, string.Empty, this.m_highscoreData.name, PsFontManager.GetFont(PsFonts.HurmeSemiBoldMN), 1f, RelativeTo.ParentHeight, "#FFFFFF", null);
		this.m_uiName.m_tmc.m_horizontalAlign = Align.Left;
		this.m_uiName.SetDepthOffset(-1f);
		this.m_uiName.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
		UIComponent uicomponent2 = new UIComponent(this, false, string.Empty, null, null, string.Empty);
		uicomponent2.SetHeight(1f, RelativeTo.ParentHeight);
		uicomponent2.SetWidth(0f, RelativeTo.ParentWidth);
		uicomponent2.RemoveDrawHandler();
		uicomponent2.SetHorizontalAlign(0.975f);
		string timeString = this.GetTimeString();
		string font = PsFontManager.GetFont(PsFonts.HurmeBoldMN);
		this.m_uiTime = new UIText(uicomponent2, false, string.Empty, timeString, font, 0.85f, RelativeTo.ParentHeight, "#FFFFFF", null);
		this.m_uiTime.m_tmc.m_horizontalAlign = Align.Right;
		this.m_uiTime.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
		this.m_uiTime.SetDepthOffset(-1f);
		this.m_prizeDisabled = this.m_highscoreData.time == 0 || this.m_highscoreData.time == int.MaxValue;
		UIComponent uicomponent3 = uicomponent2;
		int position = this.m_position;
		bool prizeDisabled = this.m_prizeDisabled;
		this.m_prize = this.GetPrizeUI(uicomponent3, position, true, string.Empty, true, prizeDisabled);
		string timeDifference = this.GetTimeDifference(this.m_topTimeScore);
		this.m_difference = new UIText(uicomponent2, false, "Gap", timeDifference, PsFontManager.GetFont(PsFonts.HurmeSemiBoldMN), 0.85f, RelativeTo.ParentHeight, "#FFFFFF", null);
		this.m_difference.m_tmc.m_horizontalAlign = Align.Right;
		this.m_difference.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
		this.m_difference.SetDepthOffset(-1f);
		this.m_difference.m_TC.transform.localRotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
		this.m_rollingInfoElements = new UIComponent[] { this.m_uiTime, null, this.m_difference };
	}

	// Token: 0x06001327 RID: 4903 RVA: 0x000BDB52 File Offset: 0x000BBF52
	public void UnsubscribeFromPrizeChange()
	{
		this.m_prizeChangeCallback = null;
	}

	// Token: 0x06001328 RID: 4904 RVA: 0x000BDB5B File Offset: 0x000BBF5B
	public void SubscribeToPrizeChange(Action<string, string, bool> _callback)
	{
		this.m_prizeChangeCallback = _callback;
	}

	// Token: 0x06001329 RID: 4905 RVA: 0x000BDB64 File Offset: 0x000BBF64
	public UICanvas GetPrizeUI(UIComponent _parent, int _position, bool _hidden = true, string _prizeString = "", bool _local = true, bool _prizeDisabled = false)
	{
		UIFittedSprite uifittedSprite = null;
		UIText uitext = null;
		UICanvas uicanvas = new UICanvas(_parent, false, "Prize", null, string.Empty);
		uicanvas.SetWidth(2.6f, RelativeTo.ParentHeight);
		uicanvas.SetDepthOffset(-1f);
		uicanvas.SetHorizontalAlign(1f);
		uicanvas.SetMargins(0f, 0.3f, 0.075f, 0.075f, RelativeTo.OwnHeight);
		uicanvas.RemoveDrawHandler();
		UICanvas uicanvas2 = new UICanvas(uicanvas, false, string.Empty, null, string.Empty);
		uicanvas2.SetMargins(-0.555f, 0.555f, 0f, 0f, RelativeTo.OwnWidth);
		uicanvas2.RemoveDrawHandler();
		UIText uitext2 = new UIText(uicanvas2, false, string.Empty, PsUITournamentLeaderboardEntry.GetPrizeString(_position, -1), "HurmeBoldMN_Font", 1f, RelativeTo.ParentHeight, "#FFFFFF", null);
		uitext2.m_tmc.m_horizontalAlign = Align.Right;
		uitext2.m_tmc.m_renderer.material.shader = Shader.Find("Framework/ColorFontShader");
		UIFittedSprite uifittedSprite2 = new UIFittedSprite(uicanvas, false, "CoinIcon", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_resources_coin_icon", null), true, true);
		uifittedSprite2.SetHeight(0.75f, RelativeTo.ParentHeight);
		uifittedSprite2.SetHorizontalAlign(0f);
		UIText uitext3 = new UIText(uicanvas, false, "Plus1", "+", PsFontManager.GetFont(PsFonts.HurmeBold), 0.85f, RelativeTo.ParentHeight, null, null);
		uitext3.SetHorizontalAlign(0.38f);
		string chestIcon = PsUITournamentLeaderboardEntry.GetChestIcon(_position, -1);
		UIFittedSprite uifittedSprite3 = new UIFittedSprite(uicanvas, false, "ChestIcon", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(chestIcon, null), true, true);
		uifittedSprite3.SetHorizontalAlign(1f);
		if (_position == 1)
		{
			uifittedSprite3.SetHorizontalAlign(0.43f);
			uicanvas.SetWidth(4.35f, RelativeTo.ParentHeight);
			uitext3.SetHorizontalAlign(0.2f);
			uitext = new UIText(uicanvas, false, "Plus1", "+", PsFontManager.GetFont(PsFonts.HurmeBold), 0.85f, RelativeTo.ParentHeight, null, null);
			uitext.SetHorizontalAlign(0.67f);
			uifittedSprite = new UIFittedSprite(uicanvas, false, "HatIcon", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_hat_icon_GoldenCarHelmet", null), true, true);
			uifittedSprite.SetHorizontalAlign(1f);
		}
		if (_local)
		{
			this.m_plus1 = uitext3;
			this.m_plus2 = uitext;
			this.m_chestFrameName = chestIcon;
			this.m_coin = uifittedSprite2;
			this.m_chestIcon = uifittedSprite3;
			this.m_prizeContainer = uicanvas;
			this.m_hatIcon = uifittedSprite;
		}
		if (_prizeDisabled)
		{
			if (uifittedSprite != null)
			{
				uifittedSprite.m_TC.transform.localRotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
			}
			uifittedSprite2.m_TC.transform.localRotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
			uifittedSprite3.m_TC.transform.localRotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
			uicanvas.m_TC.transform.localRotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
			EntityManager.SetActivityOfEntity(uicanvas.m_TC.p_entity, false, true, true, true, true, true);
		}
		if (_hidden)
		{
			uicanvas.m_TC.transform.localRotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
		}
		return uicanvas;
	}

	// Token: 0x0600132A RID: 4906 RVA: 0x000BDECC File Offset: 0x000BC2CC
	public void UpdateIconHolder(bool _first)
	{
		if (this.m_prizeContainer != null)
		{
			if (_first)
			{
				this.m_prizeContainer.SetWidth(4.35f, RelativeTo.ParentHeight);
				if (this.m_plus2 == null)
				{
					this.m_plus2 = new UIText(this.m_prizeContainer, false, "Plus1", "+", PsFontManager.GetFont(PsFonts.HurmeBold), 0.85f, RelativeTo.ParentHeight, null, null);
				}
				this.m_plus2.SetHorizontalAlign(0.67f);
				this.m_plus2.m_TC.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
				if (this.m_hatIcon == null)
				{
					this.m_hatIcon = new UIFittedSprite(this.m_prizeContainer, false, "HatIcon", PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_hat_icon_GoldenCarHelmet", null), true, true);
				}
				this.m_hatIcon.SetHorizontalAlign(1f);
				this.m_hatIcon.m_TC.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
				if (this.m_plus1 != null)
				{
					this.m_plus1.SetHorizontalAlign(0.2f);
				}
				if (this.m_chestIcon != null)
				{
					this.m_chestIcon.SetHorizontalAlign(0.43f);
				}
			}
			else
			{
				this.m_prizeContainer.SetWidth(2.6f, RelativeTo.ParentHeight);
				if (this.m_hatIcon != null)
				{
					this.m_hatIcon.Destroy();
					this.m_hatIcon = null;
				}
				if (this.m_plus2 != null)
				{
					this.m_plus2.Destroy();
					this.m_plus2 = null;
				}
				if (this.m_plus1 != null)
				{
					this.m_plus1.SetHorizontalAlign(0.5f);
				}
				if (this.m_chestIcon != null)
				{
					this.m_chestIcon.SetHorizontalAlign(1f);
				}
			}
			this.m_prizeContainer.Update();
		}
	}

	// Token: 0x0600132B RID: 4907 RVA: 0x000BE0B4 File Offset: 0x000BC4B4
	public void ShowPrize()
	{
		this.m_prizeDisabled = false;
		if (this.m_chestIcon != null)
		{
			this.m_chestIcon.m_TC.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
		}
		if (this.m_hatIcon != null)
		{
			this.m_hatIcon.m_TC.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
		}
		if (this.m_coin != null)
		{
			this.m_coin.m_TC.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
		}
		EntityManager.SetActivityOfEntity(this.m_prize.m_TC.p_entity, true, true, true, true, true, true);
	}

	// Token: 0x0600132C RID: 4908 RVA: 0x000BE190 File Offset: 0x000BC590
	public void HidePrize()
	{
		this.m_prizeDisabled = true;
		if (this.m_chestIcon != null)
		{
			this.m_chestIcon.m_TC.transform.localRotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
		}
		if (this.m_hatIcon != null)
		{
			this.m_hatIcon.m_TC.transform.localRotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
		}
		if (this.m_coin != null)
		{
			this.m_coin.m_TC.transform.localRotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
		}
		EntityManager.SetActivityOfEntity(this.m_prize.m_TC.p_entity, false, true, true, true, true, true);
	}

	// Token: 0x0600132D RID: 4909 RVA: 0x000BE26C File Offset: 0x000BC66C
	private void CreateProfilePic()
	{
		this.m_profilePic = new PsUIProfileImage(this, false, string.Empty, this.m_highscoreData.facebookId, this.m_highscoreData.gameCenterId, -1, true, false, false, 0.1f, 0.06f, "fff9e6", null, false, false);
		this.m_profilePic.SetSize(1f, 1f, RelativeTo.ParentHeight);
		this.m_profilePic.SetHorizontalAlign(0.15f);
	}

	// Token: 0x0600132E RID: 4910 RVA: 0x000BE2E0 File Offset: 0x000BC6E0
	private string GetTimeDifference(int _topTimeScore)
	{
		string text = this.GetTimeString();
		if (this.m_position != 1 && this.m_highscoreData.time != 2147483647)
		{
			text = "+" + HighScores.TimeScoreToTimeString(this.m_highscoreData.time - _topTimeScore) + " ";
		}
		return text;
	}

	// Token: 0x0600132F RID: 4911 RVA: 0x000BE338 File Offset: 0x000BC738
	private string GetTimeString()
	{
		string text = "--.---";
		if (this.m_highscoreData.time != 2147483647)
		{
			text = HighScores.TimeScoreToTimeString(this.m_highscoreData.time);
		}
		return text + " ";
	}

	// Token: 0x06001330 RID: 4912 RVA: 0x000BE37C File Offset: 0x000BC77C
	private string GetNameString(string _playerId, string _name)
	{
		string text = ((!PsMetagameManager.IsFriend(_playerId)) ? "#ffffff" : "#ABFF2A");
		return string.Concat(new string[] { "<color=", text, ">", _name, "</color>" });
	}

	// Token: 0x06001331 RID: 4913 RVA: 0x000BE3D0 File Offset: 0x000BC7D0
	public void SetNewPlayerInfo(HighscoreDataEntry _entry)
	{
		this.m_highscoreData = _entry;
		this.SetFlagFrame(true);
		this.SetProfilePic();
		TextMeshS.SetTextOptimized(this.m_uiName.m_tmc, _entry.name);
		if (this.m_player)
		{
			if (_entry.playerId != PlayerPrefsX.GetUserId())
			{
				this.m_player = false;
				this.d_Draw(this);
			}
		}
		else if (_entry.playerId == PlayerPrefsX.GetUserId())
		{
			this.m_player = true;
			this.d_Draw(this);
		}
		if (this.m_prizeDisabled && this.m_highscoreData.time != 0 && this.m_highscoreData.time != 2147483647)
		{
			this.ShowPrize();
		}
		else if (!this.m_prizeDisabled && (this.m_highscoreData.time == 0 || this.m_highscoreData.time == 2147483647))
		{
			this.HidePrize();
		}
	}

	// Token: 0x06001332 RID: 4914 RVA: 0x000BE4D8 File Offset: 0x000BC8D8
	public void SetInfo(HighscoreDataEntry _entry, int _position, int _entryCount, int _topTimeScore)
	{
		this.m_highscoreData = _entry;
		this.m_position = _position;
		TextMeshS.SetTextOptimized(this.m_uiTime.m_tmc, this.GetTimeString());
		TextMeshS.SetTextOptimized(this.m_difference.m_tmc, this.GetTimeDifference(_topTimeScore));
		if (!this.m_host)
		{
			this.SetPositionText(_position);
			TextMeshS.SetTextOptimized((this.m_prize.m_childs[0].m_childs[0] as UIText).m_tmc, PsUITournamentLeaderboardEntry.GetPrizeString(this.m_position, _entryCount));
			this.SetChestIcon(_entryCount);
		}
		if (this.m_moving)
		{
			this.MakeTransparent();
			this.d_Draw(this);
		}
	}

	// Token: 0x06001333 RID: 4915 RVA: 0x000BE58F File Offset: 0x000BC98F
	private void SetPositionText(int _position)
	{
		if (!this.m_host)
		{
			TextMeshS.SetTextOptimized(this.m_uiPosition.m_tmc, _position + ".");
		}
	}

	// Token: 0x06001334 RID: 4916 RVA: 0x000BE5BC File Offset: 0x000BC9BC
	private void SetFlagFrame(bool _update = false)
	{
		if (this.m_countryCode != this.m_highscoreData.country && !this.m_host)
		{
			this.m_countryCode = this.m_highscoreData.country;
			this.m_flag.m_frame = PsState.m_uiSheet.m_atlas.GetFrame(this.m_countryCode, "_alien");
			if (_update)
			{
				this.m_flag.Update();
			}
		}
	}

	// Token: 0x06001335 RID: 4917 RVA: 0x000BE638 File Offset: 0x000BCA38
	private void SetProfilePic()
	{
		if (string.IsNullOrEmpty(this.m_highscoreData.facebookId))
		{
			if (this.m_profilePic != null)
			{
				this.m_profilePic.Destroy();
				this.m_profilePic = null;
			}
		}
		else if (this.m_profilePic != null)
		{
			this.m_profilePic.m_facebookId = this.m_highscoreData.facebookId;
			this.m_profilePic.LoadPicture();
			this.m_profilePic.Update();
		}
		else
		{
			this.CreateProfilePic();
			this.m_profilePic.Update();
		}
	}

	// Token: 0x06001336 RID: 4918 RVA: 0x000BE6CC File Offset: 0x000BCACC
	private static string GetPrizeString(int _position, int _leaderboardCount = -1)
	{
		int coinPrize = Tournaments.GetCoinPrize(_position, (_leaderboardCount == -1) ? PsUITournamentLeaderboard.GetNewestLeaderboard().Count : _leaderboardCount);
		string text = string.Empty;
		int num = 8 - (int)Math.Floor(Math.Log10((double)coinPrize) + 1.0);
		text = text.PadLeft(num, ' ');
		return text + coinPrize;
	}

	// Token: 0x06001337 RID: 4919 RVA: 0x000BE730 File Offset: 0x000BCB30
	private static string GetChestIcon(int _position, int _leaderboardCount = -1)
	{
		GachaType gachaReward = Tournaments.GetGachaReward(_position, (_leaderboardCount == -1) ? PsUITournamentLeaderboard.GetNewestLeaderboard().Count : _leaderboardCount);
		return PsGachaManager.GetChestIconName(gachaReward);
	}

	// Token: 0x06001338 RID: 4920 RVA: 0x000BE764 File Offset: 0x000BCB64
	private string SetChestIcon(int _leaderboardCount = -1)
	{
		string chestIcon = PsUITournamentLeaderboardEntry.GetChestIcon(this.m_position, _leaderboardCount);
		if (this.m_chestFrameName != chestIcon)
		{
			this.m_chestFrameName = chestIcon;
			Frame frame = PsState.m_uiSheet.m_atlas.GetFrame(chestIcon, null);
			this.m_chestIcon.SetFrame(frame);
			this.m_chestIcon.Update();
		}
		return chestIcon;
	}

	// Token: 0x06001339 RID: 4921 RVA: 0x000BE7C0 File Offset: 0x000BCBC0
	public void RollInfo(float _delay)
	{
		int num = ((this.currentElement < this.m_rollingInfoElements.Length - 1) ? (this.currentElement + 1) : 0);
		this.RollInfo(_delay, num);
	}

	// Token: 0x0600133A RID: 4922 RVA: 0x000BE7FC File Offset: 0x000BCBFC
	public void RollInfo(float _delay, int _targetIndex)
	{
		if (this.m_rollingInfoElements.Length < 1)
		{
			return;
		}
		int num = ((_targetIndex != 0) ? (_targetIndex - 1) : (this.m_rollingInfoElements.Length - 1));
		UIComponent uicomponent = this.m_rollingInfoElements[num];
		UIComponent next = this.m_rollingInfoElements[_targetIndex];
		this.currentElement = _targetIndex;
		if (uicomponent != null)
		{
			this.m_rollTween = TweenS.AddTransformTween(uicomponent.m_TC, TweenedProperty.Rotation, TweenStyle.Linear, new Vector3(0f, 0f, 0f), new Vector3(-90f, 0f, 0f), 0.2f, _delay, false, true);
			TweenS.AddTweenEndEventListener(this.m_rollTween, delegate(TweenC _c)
			{
				this.m_rollTween = null;
				if (next != null)
				{
					this.m_rollTween = TweenS.AddTransformTween(next.m_TC, TweenedProperty.Rotation, TweenStyle.Linear, new Vector3(90f, 0f, 0f), new Vector3(0f, 0f, 0f), 0.2f, 0f, false, true);
					TweenS.AddTweenEndEventListener(this.m_rollTween, delegate(TweenC _twc)
					{
						this.m_rollTween = null;
					});
				}
			});
		}
		else if (next != null)
		{
			this.m_rollTween = TweenS.AddTransformTween(next.m_TC, TweenedProperty.Rotation, TweenStyle.Linear, new Vector3(90f, 0f, 0f), new Vector3(0f, 0f, 0f), 0.2f, _delay + 0.2f, false, true);
			TweenS.AddTweenEndEventListener(this.m_rollTween, delegate(TweenC _twc)
			{
				this.m_rollTween = null;
			});
		}
	}

	// Token: 0x0600133B RID: 4923 RVA: 0x000BE934 File Offset: 0x000BCD34
	public void SetRollIndex(int _targetIndex)
	{
		if (this.m_rollTween != null)
		{
			TweenS.RemoveComponent(this.m_rollTween);
			this.m_rollTween = null;
		}
		for (int i = 0; i < this.m_rollingInfoElements.Length; i++)
		{
			if (this.m_rollingInfoElements[i] != null)
			{
				TransformS.SetRotation(this.m_rollingInfoElements[i].m_TC, new Vector3(90f, 0f, 0f));
			}
		}
		if (this.m_rollingInfoElements.Length > _targetIndex && this.m_rollingInfoElements[_targetIndex] != null)
		{
			TransformS.SetRotation(this.m_rollingInfoElements[_targetIndex].m_TC, new Vector3(0f, 0f, 0f));
		}
		this.currentElement = _targetIndex;
	}

	// Token: 0x0600133C RID: 4924 RVA: 0x000BE9F4 File Offset: 0x000BCDF4
	public void CreateTriangle(int _index, bool _update = false)
	{
		this.m_triangle = new PsUITournamentLeaderboardEntryTriangle(this, _index);
		this.m_triangle.SetSize(0.6f, 0.6f, RelativeTo.ParentHeight);
		this.m_triangle.SetDepthOffset(-5f);
		this.m_triangle.SetHorizontalAlign(-0.0125f);
		if (_update)
		{
			this.m_triangle.Update();
		}
	}

	// Token: 0x0600133D RID: 4925 RVA: 0x000BEA55 File Offset: 0x000BCE55
	public void RemoveTriangle()
	{
		if (this.m_triangle != null)
		{
			this.m_triangle.Destroy();
		}
		this.m_triangle = null;
	}

	// Token: 0x0600133E RID: 4926 RVA: 0x000BEA74 File Offset: 0x000BCE74
	public override void DrawHandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero);
		GGData ggdata = new GGData(rect);
		Color fillTop = this.m_fillTop;
		Color fillBottom = this.m_fillBottom;
		Color color = DebugDraw.HexToColor("#636363");
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * 2f, ggdata, fillBottom, fillTop, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward, rect, 0.0025f * (float)Screen.height, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line8Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x0600133F RID: 4927 RVA: 0x000BEB2C File Offset: 0x000BCF2C
	public void DrawHandlerHost(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Color black = Color.black;
		black.a = 0.7f;
		string text = "#C04E09";
		string text2 = "#FBAA02";
		string text3 = "#EE8402";
		string text4 = "#F3980E";
		if (!PsUICenterTournament.m_isHost)
		{
			text2 = text;
			text3 = text;
			text4 = text;
		}
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth * 1f, _c.m_actualHeight * 1f, Vector2.zero);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 11f, rect, DebugDraw.ColorToUInt(black), DebugDraw.ColorToUInt(black), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth * 0.95f, _c.m_actualHeight * 0.6f, (float)Screen.width * 0.03f, 8, Vector2.zero);
		PrefabS.CreateFlatPrefabComponentsFromVectorArray(_c.m_TC, Vector3.forward * 10f, roundedRect, DebugDraw.HexToUint(text3), DebugDraw.HexToUint(text2), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera, string.Empty, null);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * 4f, roundedRect, (float)Screen.height * 0.005f, DebugDraw.HexToColor(text4), DebugDraw.HexToColor(text4), ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x06001340 RID: 4928 RVA: 0x000BECA8 File Offset: 0x000BD0A8
	public void DrawHandlerSprite(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Frame frame;
		if (this.m_player)
		{
			frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_leaderboard_row_yellow", null);
		}
		else if (this.m_chosen || this.m_moving)
		{
			frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_leaderboard_row_black", null);
		}
		else
		{
			frame = PsState.m_uiSheet.m_atlas.GetFrame("menu_tournament_leaderboard_row_gray", null);
		}
		float num = frame.width / 2f;
		Frame frame2 = new Frame(frame.x, frame.y, num, frame.height);
		Frame frame3 = new Frame(frame.x + num / 2f, frame.y, num, frame.height);
		Frame frame4 = new Frame(frame.x, frame.y, num, frame.height);
		frame4.flipX = true;
		float num2 = _c.m_actualHeight * (frame2.width / frame2.height);
		SpriteC spriteC = SpriteS.AddComponent(_c.m_TC, frame2, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC, _c.m_actualHeight * (frame2.width / frame2.height), _c.m_actualHeight);
		SpriteS.SetOffset(spriteC, Vector3.left * (_c.m_actualWidth - num2) / 2f, 0f);
		float num3 = _c.m_actualHeight * (frame2.width / frame2.height);
		SpriteC spriteC2 = SpriteS.AddComponent(_c.m_TC, frame4, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC2, num3, _c.m_actualHeight);
		SpriteS.SetOffset(spriteC2, Vector3.right * (_c.m_actualWidth - num3) / 2f, 0f);
		float num4 = _c.m_actualWidth - num2 - num3;
		SpriteC spriteC3 = SpriteS.AddComponent(_c.m_TC, frame3, PsState.m_uiSheet);
		SpriteS.SetDimensions(spriteC3, num4, _c.m_actualHeight);
		SpriteS.ConvertSpritesToPrefabComponent(_c.m_TC, _c.m_camera, true, null);
	}

	// Token: 0x06001341 RID: 4929 RVA: 0x000BEEB8 File Offset: 0x000BD2B8
	private void ShowGhostButton()
	{
		this.m_replayParent = new UICanvas(this, false, string.Empty, null, string.Empty);
		this.m_replayParent.SetMargins(-1f, 1f, 0f, 0f, RelativeTo.OwnWidth);
		this.m_replayParent.RemoveDrawHandler();
		TouchAreaS.AddBeginTouchDelegate(new Func<TouchAreaC, bool>(this.HideGhostButton));
		PsUIGenericButton psUIGenericButton = new PsUIGenericButton(this.m_replayParent, 0f, 0f, 0f, "Button");
		psUIGenericButton.SetHorizontalAlign(1.02f);
		psUIGenericButton.SetMargins(0.15f, 0.15f, 0.15f, 0.15f, RelativeTo.OwnHeight);
		psUIGenericButton.m_drawGlare = false;
		psUIGenericButton.m_drawShine = false;
		psUIGenericButton.SetDepthOffset(-5f);
		psUIGenericButton.SetReleaseAction(delegate
		{
			(PsState.m_activeGameLoop.m_gameMode as PsGameModeTournament).LoadSpectateGhosts(this.m_highscoreData.playerId, false);
			this.HideGhostButton(null);
			TouchAreaS.RemoveBeginTouchDelegates();
		});
		UIFittedSprite uifittedSprite = new UIFittedSprite(psUIGenericButton, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_ghostreplay", null), true, true);
		uifittedSprite.SetSize(1.2f, 1.2f, RelativeTo.ParentHeight);
		uifittedSprite.RemoveDrawHandler();
		this.m_replayParent.Update();
	}

	// Token: 0x06001342 RID: 4930 RVA: 0x000BEFD8 File Offset: 0x000BD3D8
	private bool HideGhostButton(TouchAreaC _touchArea)
	{
		if ((this.m_TAC != null && _touchArea == this.m_TAC) || _touchArea == this.m_replayParent.m_childs[0].m_TAC)
		{
			return false;
		}
		if (this.m_replayParent != null)
		{
			this.m_replayParent.Destroy();
			this.m_replayParent = null;
		}
		return true;
	}

	// Token: 0x06001343 RID: 4931 RVA: 0x000BF038 File Offset: 0x000BD438
	public override void Step()
	{
		if (this.m_highscoreData.time != 0 && this.m_highscoreData.time != 2147483647 && this.m_hit && this.m_replayParent == null)
		{
			this.ShowGhostButton();
		}
		if (this.m_animationTween != null && !this.m_animationTween.hasFinished)
		{
			float num = (this.m_animationTween.currentValue.y - this.m_animationTween.startValue.y) / (this.m_animationTween.endValue.y - this.m_animationTween.startValue.y);
			float num2 = (float)(this.m_current - this.m_target);
			int num3;
			if (num2 > 1f)
			{
				num3 = (int)((float)this.m_current - num * ((float)(this.m_current - this.m_target) + 1f) + 1f);
			}
			else
			{
				num3 = (int)((float)this.m_current - num * (float)(this.m_current - this.m_target) + 0.5f + this.m_e);
			}
			if (num3 != this.m_rollingPosition)
			{
				this.m_rollingPosition = num3;
				this.m_position = num3 + 1;
				if (this.m_rollingPosition == 0 && this.m_current != 0)
				{
					this.UpdateIconHolder(true);
				}
				else if (this.m_rollingPosition == 1 && this.m_current == 0)
				{
					this.UpdateIconHolder(false);
				}
				string prizeString = PsUITournamentLeaderboardEntry.GetPrizeString(this.m_position, -1);
				string text = this.SetChestIcon(-1);
				TextMeshS.SetTextOptimized((this.m_prize.m_childs[0].m_childs[0] as UIText).m_tmc, prizeString);
				(this.m_prize.m_childs[0].m_childs[0] as UIText).m_text = prizeString;
				if (this.m_prizeChangeCallback != null)
				{
					if (this.m_rollingPosition == 0 && this.m_current != 0)
					{
						this.m_prizeChangeCallback.Invoke(prizeString, text, true);
					}
					else if (this.m_rollingPosition == 1 && this.m_current == 0)
					{
						this.m_prizeChangeCallback.Invoke(prizeString, text, true);
					}
					else
					{
						this.m_prizeChangeCallback.Invoke(prizeString, text, false);
					}
				}
				this.SetPositionText(this.m_rollingPosition + 1);
				if (this.m_rollingPosition == this.m_target)
				{
					this.m_animationTween = null;
				}
			}
		}
		if (this.m_timeScoreRollTween != null && !this.m_timeScoreRollTween.hasFinished)
		{
			this.m_currentTimescore = (int)this.m_timeScoreRollTween.currentValue.x;
			this.SetTime(this.m_currentTimescore);
		}
		base.Step();
	}

	// Token: 0x06001344 RID: 4932 RVA: 0x000BF2F0 File Offset: 0x000BD6F0
	public void RemoveAnimations()
	{
		if (this.m_animationTween != null)
		{
			TweenS.RemoveComponent(this.m_animationTween);
		}
		if (this.m_timeScoreRollTween != null)
		{
			TweenS.RemoveComponent(this.m_timeScoreRollTween);
		}
		if (this.m_rollTween != null)
		{
			TweenS.RemoveComponent(this.m_rollTween);
		}
		this.m_animationTween = null;
		this.m_timeScoreRollTween = null;
		this.m_rollTween = null;
		List<IComponent> componentsByEntity = EntityManager.GetComponentsByEntity(ComponentType.Tween, this.m_TC.p_entity);
		foreach (IComponent component in componentsByEntity)
		{
			TweenC tweenC = component as TweenC;
			TweenS.RemoveComponent(tweenC);
		}
	}

	// Token: 0x06001345 RID: 4933 RVA: 0x000BF3B8 File Offset: 0x000BD7B8
	public void AnimatePositionNumber(int _target, int _current, TweenC _tween, float _e = 0f)
	{
		this.m_e = _e;
		this.m_rollingPosition = this.m_position;
		this.m_target = _target;
		this.m_current = _current;
		this.m_animationTween = _tween;
	}

	// Token: 0x06001346 RID: 4934 RVA: 0x000BF3E4 File Offset: 0x000BD7E4
	public void SetGapToBest(int _bestTime)
	{
		if (this.m_highscoreData.time == 2147483647)
		{
			return;
		}
		int time = this.m_highscoreData.time;
		bool flag = _bestTime != 0 && _bestTime < time;
		int num = ((!flag) ? time : (time - _bestTime));
		TextMeshS.SetTextOptimized(this.m_difference.m_tmc, ((!flag) ? string.Empty : "+") + HighScores.TimeScoreToTimeString(num) + " ");
	}

	// Token: 0x06001347 RID: 4935 RVA: 0x000BF468 File Offset: 0x000BD868
	public void AnimateTimescore(int _newTimescore, float _scrollTime, Action _callback)
	{
		if (this.m_prizeDisabled)
		{
			this.ShowPrize();
		}
		this.m_timeScoreRollTween = TweenS.AddTween(this.m_TC.p_entity, TweenStyle.CubicOut, (float)this.m_currentTimescore, (float)_newTimescore, _scrollTime, 0f);
		TweenS.AddTweenEndEventListener(this.m_timeScoreRollTween, delegate(TweenC c)
		{
			this.m_highscoreData.time = _newTimescore;
			this.SetTime(_newTimescore);
			this.m_currentTimescore = (int)this.m_timeScoreRollTween.currentValue.x;
			_callback.Invoke();
			this.m_timeScoreRollTween = null;
			TweenS.RemoveComponent(c);
		});
	}

	// Token: 0x06001348 RID: 4936 RVA: 0x000BF4E3 File Offset: 0x000BD8E3
	private void SetTime(int _newTimescore)
	{
		TextMeshS.SetTextOptimized(this.m_uiTime.m_tmc, HighScores.TimeScoreToTimeString(_newTimescore) + " ");
	}

	// Token: 0x06001349 RID: 4937 RVA: 0x000BF508 File Offset: 0x000BD908
	public virtual void MakeSolid()
	{
		this.m_moving = true;
		this.m_fillTop = new Color(0f, 0f, 0f, 1f);
		this.m_fillBottom = new Color(0f, 0f, 0f, 1f);
	}

	// Token: 0x0600134A RID: 4938 RVA: 0x000BF55C File Offset: 0x000BD95C
	public virtual void MakeTransparent()
	{
		this.m_moving = false;
		this.m_fillTop = new Color(0f, 0f, 0f, 0.15f);
		this.m_fillBottom = new Color(0f, 0f, 0f, 0.45f);
	}

	// Token: 0x04001605 RID: 5637
	public HighscoreDataEntry m_highscoreData;

	// Token: 0x04001606 RID: 5638
	private UIText m_uiName;

	// Token: 0x04001607 RID: 5639
	private UIText m_uiPosition;

	// Token: 0x04001608 RID: 5640
	private UIText m_plus1;

	// Token: 0x04001609 RID: 5641
	private UIText m_plus2;

	// Token: 0x0400160A RID: 5642
	private UIText m_uiTime;

	// Token: 0x0400160B RID: 5643
	private UICanvas m_prize;

	// Token: 0x0400160C RID: 5644
	private UIText m_difference;

	// Token: 0x0400160D RID: 5645
	private UICanvas m_prizeContainer;

	// Token: 0x0400160E RID: 5646
	private UIFittedSprite m_coin;

	// Token: 0x0400160F RID: 5647
	private UIFittedSprite m_hatIcon;

	// Token: 0x04001610 RID: 5648
	private UIFittedSprite m_chestIcon;

	// Token: 0x04001611 RID: 5649
	private string m_chestFrameName;

	// Token: 0x04001612 RID: 5650
	public int m_position;

	// Token: 0x04001613 RID: 5651
	private bool m_player;

	// Token: 0x04001614 RID: 5652
	private bool m_chosen;

	// Token: 0x04001615 RID: 5653
	private bool m_moving;

	// Token: 0x04001616 RID: 5654
	private bool m_ghost;

	// Token: 0x04001617 RID: 5655
	private int m_currentTimescore;

	// Token: 0x04001618 RID: 5656
	public int m_topTimeScore;

	// Token: 0x04001619 RID: 5657
	private bool m_host;

	// Token: 0x0400161A RID: 5658
	private Action<string, string, bool> m_prizeChangeCallback;

	// Token: 0x0400161B RID: 5659
	private bool m_prizeDisabled;

	// Token: 0x0400161C RID: 5660
	private string m_countryCode = string.Empty;

	// Token: 0x0400161D RID: 5661
	private UIFittedSprite m_flag;

	// Token: 0x0400161E RID: 5662
	private PsUIProfileImage m_profilePic;

	// Token: 0x0400161F RID: 5663
	private PsUITournamentLeaderboardEntryTriangle m_triangle;

	// Token: 0x04001620 RID: 5664
	private UIComponent[] m_rollingInfoElements;

	// Token: 0x04001621 RID: 5665
	private int currentElement;

	// Token: 0x04001622 RID: 5666
	private TweenC m_rollTween;

	// Token: 0x04001623 RID: 5667
	private Color m_fillTopSolid;

	// Token: 0x04001624 RID: 5668
	private Color m_fillBottomSolid;

	// Token: 0x04001625 RID: 5669
	private Color m_fillTop;

	// Token: 0x04001626 RID: 5670
	private Color m_fillBottom;

	// Token: 0x04001627 RID: 5671
	private int m_rollingPosition = -1;

	// Token: 0x04001628 RID: 5672
	private UICanvas m_replayParent;

	// Token: 0x04001629 RID: 5673
	private TweenC m_animationTween;

	// Token: 0x0400162A RID: 5674
	private int m_target;

	// Token: 0x0400162B RID: 5675
	private int m_current;

	// Token: 0x0400162C RID: 5676
	private float m_e;

	// Token: 0x0400162D RID: 5677
	private TweenC m_timeScoreRollTween;
}
