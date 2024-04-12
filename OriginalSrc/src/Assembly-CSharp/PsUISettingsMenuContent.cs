using System;
using AdMediation;
using Server;
using UnityEngine;

// Token: 0x0200027E RID: 638
public class PsUISettingsMenuContent : UIScrollableCanvas
{
	// Token: 0x06001353 RID: 4947 RVA: 0x000C18FC File Offset: 0x000BFCFC
	public PsUISettingsMenuContent(UIComponent _parent)
		: base(_parent, "SettingsMenuContent")
	{
		this.m_TAC.m_letTouchesThrough = true;
		this.m_passTouchesToScrollableParents = true;
		this.m_passwordModel = new UIModel(new PasswordModel(string.Empty), null);
		this.m_utilityEntity = EntityManager.AddEntity();
		this.RemoveDrawHandler();
		this.SetWidth(1f, RelativeTo.ParentWidth);
		this.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_verticalArea = new UIVerticalList(this, "VerticalArea");
		this.m_verticalArea.SetVerticalAlign(1f);
		this.m_verticalArea.SetHorizontalAlign(0.5f);
		this.m_verticalArea.SetMargins(0.025f, 0.025f, 0.075f, 0.075f, RelativeTo.ScreenShortest);
		this.m_verticalArea.SetSpacing(0.05f, RelativeTo.ScreenShortest);
		this.m_verticalArea.RemoveDrawHandler();
		this.m_handedness = new PsUIGenericButton(this.m_verticalArea, 0.25f, 0.25f, 0.005f, "Button");
		this.m_handedness.SetText("Left handed: " + ((!PsState.m_editorIsLefty) ? "<color=#e83808>" : "<color=#a0ff00>") + PsState.m_editorIsLefty.ToString() + "</color>", 0.04f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_handedness.SetOrangeColors(true);
		this.m_handedness.SetHeight(0.06f, RelativeTo.ScreenHeight);
		this.m_handedness.Update();
		if (EveryplayManager.IsSupported())
		{
			this.m_everyPlayEnabled = new PsUIGenericButton(this.m_verticalArea, 0.25f, 0.25f, 0.005f, "Button");
			this.m_everyPlayEnabled.SetText("Everyplay: " + ((!PsState.m_everyplayEnabled) ? "<color=#e83808>" : "<color=#a0ff00>") + ((!PsState.m_everyplayEnabled) ? "Disabled" : "Enabled") + "</color>", 0.04f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			this.m_everyPlayEnabled.SetOrangeColors(true);
			this.m_everyPlayEnabled.SetHeight(0.06f, RelativeTo.ScreenHeight);
			this.m_everyPlayEnabled.Update();
		}
		else
		{
			this.m_everyPlayEnabled = null;
		}
		this.m_changeName = new PsUIGenericButton(this.m_verticalArea, 0.25f, 0.25f, 0.005f, "Button");
		this.m_changeName.SetText("Change your name", 0.04f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_changeName.SetOrangeColors(true);
		this.m_changeName.SetHeight(0.06f, RelativeTo.ScreenHeight);
		this.m_changeName.Update();
		if (PlayerPrefsX.GetFacebookId() != null)
		{
			this.m_facebookButton = new PsUIGenericButton(this.m_verticalArea, 0.25f, 0.25f, 0.005f, "Button");
			this.m_facebookButton.SetText("Disconnect from Facebook", 0.04f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			this.m_facebookButton.SetHeight(0.06f, RelativeTo.ScreenHeight);
		}
		this.m_resetGame = new PsUIGenericButton(this.m_verticalArea, 0.25f, 0.25f, 0.005f, "Button");
		this.m_resetGame.SetText("RESET GAME", 0.04f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_resetGame.SetHeight(0.06f, RelativeTo.ScreenHeight);
	}

	// Token: 0x06001354 RID: 4948 RVA: 0x000C1C48 File Offset: 0x000C0048
	private void FacebookLoginDone()
	{
		Debug.Log("Facebook disconnected", null);
		this.m_facebookId = PlayerPrefsX.GetFacebookId();
		UIComponent parent = this.m_facebookButton.m_parent;
		this.m_facebookButton.Destroy();
		parent.Update();
		if (this.m_waitingPopup != null)
		{
			this.m_waitingPopup.Destroy();
			this.m_waitingPopup = null;
		}
	}

	// Token: 0x06001355 RID: 4949 RVA: 0x000C1CA5 File Offset: 0x000C00A5
	public override void Destroy()
	{
		base.Destroy();
		EntityManager.RemoveEntity(this.m_utilityEntity);
	}

	// Token: 0x06001356 RID: 4950 RVA: 0x000C1CB8 File Offset: 0x000C00B8
	private void DoneCallback(string _value)
	{
		Debug.Log("DONE: " + _value, null);
		this.m_adminPass.SetValue(_value);
	}

	// Token: 0x06001357 RID: 4951 RVA: 0x000C1CD7 File Offset: 0x000C00D7
	private void KeyPressedCallback(string _value)
	{
		Debug.Log("PRESSED: " + _value, null);
	}

	// Token: 0x06001358 RID: 4952 RVA: 0x000C1CEA File Offset: 0x000C00EA
	private void CancelCallback()
	{
		Debug.Log("CANCEL", null);
	}

	// Token: 0x06001359 RID: 4953 RVA: 0x000C1CF8 File Offset: 0x000C00F8
	public override void Step()
	{
		if (this.m_adminPass != null && this.m_adminPass.m_hit)
		{
			UITextInput uitextInput = new UITextInput("Admin password", new Action<string>(this.DoneCallback), new Action(this.CancelCallback), new Action<string>(this.KeyPressedCallback), 1, true, 128);
			uitextInput.SetText((string)this.m_adminPass.GetValue());
			uitextInput.Update();
		}
		else if (this.m_facebookButton != null && this.m_facebookButton.m_hit)
		{
			this.m_waitingPopup = new PsUIBasePopup(typeof(PsUISettingsMenuContent.PsUIPopupFacebookWaiting), null, null, null, false, true, InitialPage.Center, false, false, false);
			FacebookManager.Logout(new Action(this.FacebookLoginDone), true);
		}
		if (this.m_handedness.m_hit)
		{
			PsState.m_editorIsLefty = !PsState.m_editorIsLefty;
			this.m_handedness.SetText("Left handed: " + ((!PsState.m_editorIsLefty) ? "<color=#e83808>" : "<color=#a0ff00>") + PsState.m_editorIsLefty.ToString() + "</color>", 0.04f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			this.m_handedness.Update();
			PlayerPrefsX.SetLefty(PsState.m_editorIsLefty);
		}
		if (this.m_everyPlayEnabled != null && this.m_everyPlayEnabled.m_hit)
		{
			PsState.m_everyplayEnabled = !PsState.m_everyplayEnabled;
			this.m_everyPlayEnabled.SetText("Everyplay: " + ((!PsState.m_everyplayEnabled) ? "<color=#e83808>" : "<color=#a0ff00>") + ((!PsState.m_everyplayEnabled) ? "Disabled" : "Enabled") + "</color>", 0.04f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			this.m_everyPlayEnabled.Update();
			PlayerPrefsX.SetEveryplayEnabled(PsState.m_everyplayEnabled);
			EveryplayManager.SetEnabled(PsState.m_everyplayEnabled);
		}
		if (this.m_changeName.m_hit)
		{
			PsUIBasePopup psUIBasePopup = new PsUIBasePopup(typeof(PsUIPopupChangeName), null, null, null, false, true, InitialPage.Center, false, false, false);
			psUIBasePopup.SetAction("Proceed", new Action(this.ChangeName));
			(psUIBasePopup.m_mainContent as PsUIPopupChangeName).CreateUI(false);
			psUIBasePopup.Update();
		}
		if (this.m_resetKeychain != null && this.m_resetKeychain.m_hit)
		{
			PsUIBasePopup psUIBasePopup2 = new PsUIBasePopup(typeof(PsUISettingsMenuContent.PsUIPopupKeychainConfirmation), null, null, null, false, true, InitialPage.Center, false, false, false);
			psUIBasePopup2.SetAction("Proceed", new Action(this.ResetKeychain));
		}
		if (this.m_disableAdminMode != null && this.m_disableAdminMode.m_hit)
		{
			PsState.m_adminMode = false;
			this.Update();
		}
		if (this.m_resetGame != null && this.m_resetGame.m_hit)
		{
			PsUIBasePopup psUIBasePopup3 = new PsUIBasePopup(typeof(PsUISettingsMenuContent.PsUIPopupProgressionConfirmation), null, null, null, false, true, InitialPage.Center, false, false, false);
			psUIBasePopup3.SetAction("Proceed", new Action(this.ResetGame));
		}
		if (this.m_resetStats != null && this.m_resetStats.m_hit)
		{
			PsUIBasePopup psUIBasePopup4 = new PsUIBasePopup(typeof(PsUISettingsMenuContent.PsUIPopupStatsConfirmation), null, null, null, false, true, InitialPage.Center, false, false, false);
			psUIBasePopup4.SetAction("Proceed", new Action(this.ResetStats));
		}
		if (this.m_resetPerformance != null && this.m_resetPerformance.m_hit)
		{
			PlayerPrefsX.SetLowEndPrompt(false);
		}
		else if (this.m_builder != null && this.m_builder.m_hit)
		{
			if (PlayerPrefsX.GetUserId() == "5582757b3800005834b7897b")
			{
				PsUIBasePopup psUIBasePopup5 = new PsUIBasePopup(typeof(PsUISettingsMenuContent.PsUIPopupUserConfirmation), null, null, null, false, true, InitialPage.Center, false, false, false);
				psUIBasePopup5.SetAction("Proceed", new Action(this.ChangeToOriginal));
			}
			else
			{
				PsUIBasePopup psUIBasePopup6 = new PsUIBasePopup(typeof(PsUISettingsMenuContent.PsUIPopupBuilderConfirmation), null, null, null, false, true, InitialPage.Center, false, false, false);
				psUIBasePopup6.SetAction("Proceed", new Action(this.ChangeToBuilder));
			}
		}
		else if (this.m_testElasticSearch != null && this.m_testElasticSearch.m_hit)
		{
			MinigameSearchParametres minigameSearchParametres = new MinigameSearchParametres(null, null, PsGameMode.Any, null, PsGameDifficulty.Any);
			HttpC httpC = MiniGame.SearchMinigames(minigameSearchParametres, new Action<PsMinigameMetaData[]>(this.SearchOnlyMetaDataSUCCESS), new Action<HttpC>(this.SearchOnlyMetaDataFAILED), null, 300);
			httpC.objectData = minigameSearchParametres;
			Debug.LogWarning("SEARCH METADATA");
		}
		else if (this.m_testAds != null && this.m_testAds.m_hit && PsAdMediation.adsAvailable())
		{
			TouchAreaS.Disable();
			PsAdMediation.ShowAd(new Action<AdResult>(this.adCallback), null);
		}
		else
		{
			if (this.m_graphEditor != null)
			{
				bool hit = this.m_graphEditor.m_hit;
			}
			if (this.m_newPlanetEditor != null)
			{
				bool hit2 = this.m_newPlanetEditor.m_hit;
			}
			if (this.m_planetList == null)
			{
				if (this.m_resetLocalPath != null && this.m_resetLocalPath.m_hit)
				{
					PsPlanetManager.GetCurrentPlanet().ResetLocalProgression();
					Main.m_currentGame.m_sceneManager.ChangeScene(new StartupScene("StartupScene"), null);
				}
				else if (this.m_adminPassOK != null)
				{
					bool hit3 = this.m_adminPassOK.m_hit;
				}
			}
		}
		base.Step();
	}

	// Token: 0x0600135A RID: 4954 RVA: 0x000C2258 File Offset: 0x000C0658
	private void ResetKeychain()
	{
		TouchAreaS.Disable();
		PlayerPrefsX.SetUserId(string.Empty);
		PlayerPrefsX.SetGameCenterId(string.Empty);
		TouchAreaS.Enable();
	}

	// Token: 0x0600135B RID: 4955 RVA: 0x000C2278 File Offset: 0x000C0678
	private void ResetGame()
	{
		ResetFlow.Start(true);
	}

	// Token: 0x0600135C RID: 4956 RVA: 0x000C2280 File Offset: 0x000C0680
	private void ResetOK(HttpC _c)
	{
	}

	// Token: 0x0600135D RID: 4957 RVA: 0x000C2284 File Offset: 0x000C0684
	private void ResetStats()
	{
		PsMetagamePlayerData.m_playerData.Clear();
		PsMetagameManager.m_playerStats.upgrades.Clear();
		Player.ResetPlayer(new Action<HttpC>(this.ResetSUCCEED), new Action<HttpC>(this.ResetFAILED), null);
		this.m_resetStats.DisableTouchAreas(true);
		this.m_resetStats.SetText("Resetting", 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_resetStats.Update();
	}

	// Token: 0x0600135E RID: 4958 RVA: 0x000C2300 File Offset: 0x000C0700
	private void ChangeToBuilder()
	{
		PlayerPrefs.SetString("ORIG_USER", PlayerPrefsX.GetUserId());
		PlayerPrefs.SetString("ORIG_NAME", PlayerPrefsX.GetUserName());
		PlayerPrefsX.SetUserId("5582757b3800005834b7897b");
		PlayerPrefsX.SetUserName("The Builder");
		Debug.Log("KCID: " + PlayerPrefsX.GetUserId(), null);
		Main.m_currentGame.m_sceneManager.ChangeScene(new StartupScene("StartupScene"), null);
	}

	// Token: 0x0600135F RID: 4959 RVA: 0x000C236E File Offset: 0x000C076E
	private void ChangeToOriginal()
	{
		PlayerPrefsX.SetUserId(PlayerPrefs.GetString("ORIG_USER"));
		PlayerPrefsX.SetUserName(PlayerPrefs.GetString("ORIG_NAME"));
		Main.m_currentGame.m_sceneManager.ChangeScene(new StartupScene("StartupScene"), null);
	}

	// Token: 0x06001360 RID: 4960 RVA: 0x000C23A8 File Offset: 0x000C07A8
	private void ChangeName()
	{
		this.m_waitingPopup = new PsUIBasePopup(typeof(PsUISettingsMenuContent.PsUIPopupNameWaiting), null, null, null, false, true, InitialPage.Center, false, false, false);
		new PsServerQueueFlow(null, delegate
		{
			PsServerRequest.ServerChangeName(PlayerPrefsX.GetUserName(), new Action<HttpC>(this.NameChangeSUCCEED), new Action<HttpC>(this.NameChangeFAILED), null);
		}, null);
	}

	// Token: 0x06001361 RID: 4961 RVA: 0x000C23E7 File Offset: 0x000C07E7
	private void NameChangeSUCCEED(HttpC _c)
	{
		Debug.Log("NAME CHANGE SUCCEED", null);
		PsAchievementManager.Complete("uniqueSnowflake");
		this.m_waitingPopup.Destroy();
		this.m_waitingPopup = null;
	}

	// Token: 0x06001362 RID: 4962 RVA: 0x000C2410 File Offset: 0x000C0810
	private void NameChangeFAILED(HttpC _c)
	{
		Debug.Log("NAME CHANGE FAILED", null);
		string networkError = ServerErrors.GetNetworkError(_c.www.error);
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), networkError, () => PsServerRequest.ServerChangeName(PlayerPrefsX.GetUserName(), new Action<HttpC>(this.NameChangeSUCCEED), new Action<HttpC>(this.NameChangeFAILED), null), null, StringID.TRY_AGAIN_SERVER);
	}

	// Token: 0x06001363 RID: 4963 RVA: 0x000C245B File Offset: 0x000C085B
	private void ResetSUCCEED(HttpC _c)
	{
		PsMetagameManager.HideResources();
		Debug.Log("RESET SUCCEED - or not", null);
		Application.Quit();
		Main.m_currentGame.m_sceneManager.ChangeScene(new StartupScene("StartupScene"), null);
	}

	// Token: 0x06001364 RID: 4964 RVA: 0x000C248C File Offset: 0x000C088C
	private void ResetFAILED(HttpC _c)
	{
	}

	// Token: 0x06001365 RID: 4965 RVA: 0x000C248E File Offset: 0x000C088E
	private void adCallback(AdResult _result)
	{
		TouchAreaS.Enable();
		Debug.Log("Ad display result: " + _result.ToString(), null);
	}

	// Token: 0x06001366 RID: 4966 RVA: 0x000C24B2 File Offset: 0x000C08B2
	protected void SearchOnlyMetaDataFAILED(HttpC _c)
	{
		Debug.LogError("ELASTIC SEARCH FAILED " + _c.www);
	}

	// Token: 0x06001367 RID: 4967 RVA: 0x000C24CC File Offset: 0x000C08CC
	protected virtual void SearchOnlyMetaDataSUCCESS(PsMinigameMetaData[] _minigameMetaData)
	{
		if (_minigameMetaData.Length > 0)
		{
			for (int i = 0; i < _minigameMetaData.Length; i++)
			{
				Debug.Log(string.Concat(new object[]
				{
					(!_minigameMetaData[i].played) ? string.Empty : "PLAYED - ",
					_minigameMetaData[i].name,
					"/",
					_minigameMetaData[i].difficulty,
					"/",
					_minigameMetaData[i].gameMode,
					"/",
					_minigameMetaData[i].playerUnit,
					"/",
					_minigameMetaData[i].quality,
					"/ liked+rated: ",
					_minigameMetaData[i].timesLiked,
					"+",
					_minigameMetaData[i].timesRated,
					"(",
					(float)_minigameMetaData[i].timesLiked / (float)_minigameMetaData[i].timesRated,
					")"
				}), null);
			}
		}
		else
		{
			Debug.LogError("SERVER RETURNED EMPTY LEVEL LIST");
		}
	}

	// Token: 0x04001637 RID: 5687
	private Entity m_utilityEntity;

	// Token: 0x04001638 RID: 5688
	private UIVerticalList m_verticalArea;

	// Token: 0x04001639 RID: 5689
	private PsUIGenericButton m_facebookButton;

	// Token: 0x0400163A RID: 5690
	private const string FACEBOOK_LOGOUT = "Disconnect from Facebook";

	// Token: 0x0400163B RID: 5691
	private const string FACEBOOK_LOGIN = "Connect to Facebook";

	// Token: 0x0400163C RID: 5692
	private const string GAMECENTER_LOGIN = "Connect to GameCenter";

	// Token: 0x0400163D RID: 5693
	private const string GAMECENTER_LOGOUT = "Disconnect from GameCenter";

	// Token: 0x0400163E RID: 5694
	private string m_facebookId;

	// Token: 0x0400163F RID: 5695
	private PsUIGenericButton m_handedness;

	// Token: 0x04001640 RID: 5696
	private PsUIGenericButton m_mute;

	// Token: 0x04001641 RID: 5697
	private PsUIGenericButton m_resetGame;

	// Token: 0x04001642 RID: 5698
	private PsUIGenericButton m_resetStats;

	// Token: 0x04001643 RID: 5699
	private PsUIGenericButton m_resetLocalPath;

	// Token: 0x04001644 RID: 5700
	private PsUIGenericButton m_resetServerPath;

	// Token: 0x04001645 RID: 5701
	private PsUIGenericButton m_resetKeychain;

	// Token: 0x04001646 RID: 5702
	private PsUIGenericButton m_disableAdminMode;

	// Token: 0x04001647 RID: 5703
	private PsUIGenericButton m_increaseResources;

	// Token: 0x04001648 RID: 5704
	private PsUIGenericButton m_decreaseResources;

	// Token: 0x04001649 RID: 5705
	private PsUIGenericButton m_testElasticSearch;

	// Token: 0x0400164A RID: 5706
	private PsUIGenericButton m_testAds;

	// Token: 0x0400164B RID: 5707
	private PsUIGenericButton m_everyPlayEnabled;

	// Token: 0x0400164C RID: 5708
	private PsUIGenericButton m_changeName;

	// Token: 0x0400164D RID: 5709
	private PsUIGenericButton m_graphEditor;

	// Token: 0x0400164E RID: 5710
	private PsUIGenericButton m_newPlanetEditor;

	// Token: 0x0400164F RID: 5711
	private UIHorizontalList m_planetList;

	// Token: 0x04001650 RID: 5712
	private PsUIGenericButton m_builder;

	// Token: 0x04001651 RID: 5713
	private PsUIGenericButton m_resetPerformance;

	// Token: 0x04001652 RID: 5714
	private UITextArea m_adminPass;

	// Token: 0x04001653 RID: 5715
	private PsUIGenericButton m_adminPassOK;

	// Token: 0x04001654 RID: 5716
	private UIText m_adminPassStatus;

	// Token: 0x04001655 RID: 5717
	private UIModel m_passwordModel;

	// Token: 0x04001656 RID: 5718
	private UIVerticalList m_passwordArea;

	// Token: 0x04001657 RID: 5719
	private PsUIBasePopup m_waitingPopup;

	// Token: 0x0200027F RID: 639
	private class CreateNewPlanetPopup : UICanvas
	{
		// Token: 0x0600136A RID: 4970 RVA: 0x000C264C File Offset: 0x000C0A4C
		public CreateNewPlanetPopup(UIComponent _parent)
			: base(_parent, false, string.Empty, null, string.Empty)
		{
			this.m_inputField = new PsUIGenericInputField(this);
			this.m_inputField.SetWidth(0.6f, RelativeTo.ScreenWidth);
			this.m_inputField.ChangeTitleText("Planet name: ");
			this.m_proceed = new PsUIGenericButton(this, 0.25f, 0.25f, 0.005f, "Button");
			this.m_proceed.SetGreenColors(true);
			this.m_proceed.SetHorizontalAlign(0.4f);
			this.m_proceed.SetVerticalAlign(0.3f);
			this.m_proceed.SetText("Done!", 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			this.m_cancel = new PsUIGenericButton(this, 0.25f, 0.25f, 0.005f, "Button");
			this.m_cancel.SetRedColors();
			this.m_cancel.SetHorizontalAlign(0.6f);
			this.m_cancel.SetVerticalAlign(0.3f);
			this.m_cancel.SetText("Cancel", 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x000C276C File Offset: 0x000C0B6C
		public override void Step()
		{
			if (this.m_proceed == null || !this.m_proceed.m_hit)
			{
				if (this.m_cancel != null && this.m_cancel.m_hit)
				{
					(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
				}
			}
			base.Step();
		}

		// Token: 0x04001658 RID: 5720
		private PsUIGenericButton m_proceed;

		// Token: 0x04001659 RID: 5721
		private PsUIGenericButton m_cancel;

		// Token: 0x0400165A RID: 5722
		private PsUIGenericInputField m_inputField;

		// Token: 0x0400165B RID: 5723
		private Action<string> m_proceedAction;

		// Token: 0x0400165C RID: 5724
		private Action m_cancelAction;
	}

	// Token: 0x02000280 RID: 640
	private class PsUIPopupConfirmation : PsUIHeaderedCanvas
	{
		// Token: 0x0600136C RID: 4972 RVA: 0x000C27D0 File Offset: 0x000C0BD0
		public PsUIPopupConfirmation(UIComponent _parent)
			: base(_parent, string.Empty, false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
		{
			(this.GetRoot() as PsUIBasePopup).m_scrollableCanvas.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.MenuPopupBackground));
			this.SetWidth(0.65f, RelativeTo.ScreenWidth);
			this.SetHeight(0.45f, RelativeTo.ScreenHeight);
			this.SetVerticalAlign(0.4f);
			this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
			this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
			this.m_header.Destroy();
			UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
			uihorizontalList.SetSpacing(0.05f, RelativeTo.ScreenHeight);
			uihorizontalList.RemoveDrawHandler();
			uihorizontalList.SetVerticalAlign(0f);
			uihorizontalList.SetMargins(0f, 0f, 0.075f, -0.075f, RelativeTo.ScreenHeight);
			this.m_ok = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
			this.m_ok.SetText("Yes!", 0.04f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			this.m_ok.SetHeight(0.075f, RelativeTo.ScreenHeight);
			this.m_ok.SetGreenColors(true);
			this.m_cancel = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
			this.m_cancel.SetText("Cancel", 0.04f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			this.m_cancel.SetHeight(0.075f, RelativeTo.ScreenHeight);
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x000C2980 File Offset: 0x000C0D80
		public override void Step()
		{
			if (this.m_ok.m_hit)
			{
				(this.GetRoot() as PsUIBasePopup).CallAction("Proceed");
				(this.GetRoot() as PsUIBasePopup).Destroy();
			}
			else if (this.m_cancel.m_hit)
			{
				(this.GetRoot() as PsUIBasePopup).Destroy();
			}
			base.Step();
		}

		// Token: 0x0400165D RID: 5725
		private PsUIGenericButton m_ok;

		// Token: 0x0400165E RID: 5726
		private PsUIGenericButton m_cancel;
	}

	// Token: 0x02000281 RID: 641
	private class PsUIPopupKeychainConfirmation : PsUISettingsMenuContent.PsUIPopupConfirmation
	{
		// Token: 0x0600136E RID: 4974 RVA: 0x000C29F0 File Offset: 0x000C0DF0
		public PsUIPopupKeychainConfirmation(UIComponent _parent)
			: base(_parent)
		{
			new UITextbox(this, false, string.Empty, "Are you sure you want to RESET keychain settings?", PsFontManager.GetFont(PsFonts.HurmeRegular), 0.03f, RelativeTo.ScreenShortest, false, Align.Center, Align.Middle, null, true, null);
		}
	}

	// Token: 0x02000282 RID: 642
	private class PsUIPopupProgressionConfirmation : PsUISettingsMenuContent.PsUIPopupConfirmation
	{
		// Token: 0x0600136F RID: 4975 RVA: 0x000C2A28 File Offset: 0x000C0E28
		public PsUIPopupProgressionConfirmation(UIComponent _parent)
			: base(_parent)
		{
			new UITextbox(this, false, string.Empty, "Are you sure you want to RESET your progression?", PsFontManager.GetFont(PsFonts.HurmeRegular), 0.03f, RelativeTo.ScreenShortest, false, Align.Center, Align.Middle, null, true, null);
		}
	}

	// Token: 0x02000283 RID: 643
	private class PsUIPopupStatsConfirmation : PsUISettingsMenuContent.PsUIPopupConfirmation
	{
		// Token: 0x06001370 RID: 4976 RVA: 0x000C2A60 File Offset: 0x000C0E60
		public PsUIPopupStatsConfirmation(UIComponent _parent)
			: base(_parent)
		{
			new UITextbox(this, false, string.Empty, "Are you sure you want to RESET your stats?", PsFontManager.GetFont(PsFonts.HurmeRegular), 0.03f, RelativeTo.ScreenShortest, false, Align.Center, Align.Middle, null, true, null);
		}
	}

	// Token: 0x02000284 RID: 644
	private class PsUIPopupBuilderConfirmation : PsUISettingsMenuContent.PsUIPopupConfirmation
	{
		// Token: 0x06001371 RID: 4977 RVA: 0x000C2A98 File Offset: 0x000C0E98
		public PsUIPopupBuilderConfirmation(UIComponent _parent)
			: base(_parent)
		{
			new UITextbox(this, false, string.Empty, "Are you sure you want to switch to builder account?", PsFontManager.GetFont(PsFonts.HurmeRegular), 0.03f, RelativeTo.ScreenShortest, false, Align.Center, Align.Middle, null, true, null);
		}
	}

	// Token: 0x02000285 RID: 645
	private class PsUIPopupUserConfirmation : PsUISettingsMenuContent.PsUIPopupConfirmation
	{
		// Token: 0x06001372 RID: 4978 RVA: 0x000C2AD0 File Offset: 0x000C0ED0
		public PsUIPopupUserConfirmation(UIComponent _parent)
			: base(_parent)
		{
			new UITextbox(this, false, string.Empty, "Are you sure you want to switch back to player account?", PsFontManager.GetFont(PsFonts.HurmeRegular), 0.03f, RelativeTo.ScreenShortest, false, Align.Center, Align.Middle, null, true, null);
		}
	}

	// Token: 0x02000286 RID: 646
	private class PsUIPopupWaiting : PsUIHeaderedCanvas
	{
		// Token: 0x06001373 RID: 4979 RVA: 0x000C2B08 File Offset: 0x000C0F08
		public PsUIPopupWaiting(UIComponent _parent)
			: base(_parent, string.Empty, false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
		{
			(this.GetRoot() as PsUIBasePopup).m_scrollableCanvas.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.MenuPopupBackground));
			this.SetWidth(0.65f, RelativeTo.ScreenWidth);
			this.SetHeight(0.45f, RelativeTo.ScreenHeight);
			this.SetVerticalAlign(0.4f);
			this.SetMargins(0.0125f, 0.0125f, 0f, 0.0125f, RelativeTo.ScreenHeight);
			this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
			this.m_header.Destroy();
		}
	}

	// Token: 0x02000287 RID: 647
	private class PsUIPopupFacebookWaiting : PsUISettingsMenuContent.PsUIPopupWaiting
	{
		// Token: 0x06001374 RID: 4980 RVA: 0x000C2BCC File Offset: 0x000C0FCC
		public PsUIPopupFacebookWaiting(UIComponent _parent)
			: base(_parent)
		{
			new UITextbox(this, false, string.Empty, "Disconnecting from Facebook...", PsFontManager.GetFont(PsFonts.HurmeRegular), 0.03f, RelativeTo.ScreenShortest, false, Align.Center, Align.Middle, null, true, null);
		}
	}

	// Token: 0x02000288 RID: 648
	private class PsUIPopupNameWaiting : PsUISettingsMenuContent.PsUIPopupWaiting
	{
		// Token: 0x06001375 RID: 4981 RVA: 0x000C2C04 File Offset: 0x000C1004
		public PsUIPopupNameWaiting(UIComponent _parent)
			: base(_parent)
		{
			new UITextbox(this, false, string.Empty, "Changing your name...", PsFontManager.GetFont(PsFonts.HurmeRegular), 0.03f, RelativeTo.ScreenShortest, false, Align.Center, Align.Middle, null, true, null);
		}
	}
}
