using System;
using System.Collections.Generic;
using DeepLink;
using Server;
using UnityEngine;

// Token: 0x02000317 RID: 791
public class PsUIProfileLevelCard : UIVerticalList
{
	// Token: 0x06001759 RID: 5977 RVA: 0x000FD0C4 File Offset: 0x000FB4C4
	public PsUIProfileLevelCard(UIComponent _parent, PsGameLoop _loop, Action _closeAction = null, bool _friend = false)
		: base(_parent, "levelCard")
	{
		PsUIProfileLevelCard.m_loop = _loop;
		this.m_closeAction = _closeAction;
		this.SetWidth(1.225f, RelativeTo.ParentWidth);
		this.SetDepthOffset(-10f);
		this.SetVerticalAlign(0.8f);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ProfileCardBackgroundDrawhandler));
		this.CreateTouchAreas();
		this.SetMargins(0.015f, 0.015f, 0.01f, 0.02f, RelativeTo.ScreenHeight);
		this.SetSpacing(-0.03f, RelativeTo.ScreenHeight);
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
		uihorizontalList.SetSpacing(0.0185f, RelativeTo.ScreenHeight);
		uihorizontalList.SetHorizontalAlign(0f);
		uihorizontalList.RemoveDrawHandler();
		uihorizontalList.SetDepthOffset(-5f);
		PsUIProfileImage psUIProfileImage = new PsUIProfileImage(uihorizontalList, false, string.Empty, PsUIProfileLevelCard.m_loop.GetFacebookId(), PsUIProfileLevelCard.m_loop.GetGamecenterId(), -1, true, false, false, 0.1f, 0.06f, "fff9e6", null, true, true);
		psUIProfileImage.SetSize(0.225f, 0.225f, RelativeTo.ParentWidth);
		UICanvas uicanvas = new UICanvas(uihorizontalList, false, string.Empty, null, string.Empty);
		uicanvas.SetHeight(0.13925f, RelativeTo.ParentWidth);
		uicanvas.SetWidth(0.7f, RelativeTo.ParentWidth);
		uicanvas.RemoveDrawHandler();
		UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, PsUIProfileLevelCard.m_loop.GetCreatorName(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, (!_friend) ? "#FFFFF6" : "#A6FF32", "313131");
		uifittedText.SetHorizontalAlign(0f);
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.RemoveDrawHandler();
		PsUIScreenshot psUIScreenshot = new PsUIScreenshot(uiverticalList, false, string.Empty, Vector2.zero, PsUIProfileLevelCard.m_loop, true, true, 0.03f, false);
		psUIScreenshot.SetDrawHandler(new UIDrawDelegate(PsUIScreenshot.BasicDrawHandler));
		psUIScreenshot.SetWidth(0.8f, RelativeTo.ParentWidth);
		psUIScreenshot.SetHeight(0.625f, RelativeTo.ParentWidth);
		psUIScreenshot.SetDepthOffset(-3f);
		string text = "item_mode_adventure";
		if (PsUIProfileLevelCard.m_loop.GetGameMode() == PsGameMode.Race)
		{
			text = "item_mode_timeattack";
		}
		UIFittedSprite uifittedSprite = new UIFittedSprite(uiverticalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text, null), true, true);
		uifittedSprite.SetHeight(0.5f, RelativeTo.ParentHeight);
		uifittedSprite.SetAlign(0f, 0.75f);
		uifittedSprite.SetRogue();
		uifittedSprite.SetDepthOffset(-5f);
		UICanvas uicanvas2 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas2.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas2.SetHeight(0.115f, RelativeTo.ParentWidth);
		uicanvas2.SetMargins(0f, 0f, -0.075f, 0.075f, RelativeTo.ParentWidth);
		uicanvas2.RemoveDrawHandler();
		UIFittedText uifittedText2 = new UIFittedText(uicanvas2, false, string.Empty, PsUIProfileLevelCard.m_loop.GetName(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#AFFF2E", "#13245E");
		this.m_positive = PsUIProfileLevelCard.m_loop.GetVisualLikes();
		this.m_negative = PsUIProfileLevelCard.m_loop.GetVisualDislikes();
		UICanvas uicanvas3 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas3.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas3.SetHeight(0.08f, RelativeTo.ParentWidth);
		uicanvas3.SetMargins(0f, 0f, -0.075f, 0.075f, RelativeTo.ParentWidth);
		uicanvas3.RemoveDrawHandler();
		UICanvas uicanvas4 = new UICanvas(uicanvas3, false, string.Empty, null, string.Empty);
		uicanvas4.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas4.SetHeight(1f, RelativeTo.ParentHeight);
		uicanvas4.SetDepthOffset(-5f);
		uicanvas4.SetDrawHandler(new UIDrawDelegate(this.SimpleLikeDrawhandler));
		UIHorizontalList uihorizontalList2 = new UIHorizontalList(uiverticalList, string.Empty);
		uihorizontalList2.SetSpacing(0.075f, RelativeTo.ParentWidth);
		uihorizontalList2.RemoveDrawHandler();
		uihorizontalList2.SetMargins(0f, 0f, -0.075f, 0.075f, RelativeTo.ParentWidth);
		UIHorizontalList uihorizontalList3 = new UIHorizontalList(uihorizontalList2, string.Empty);
		uihorizontalList3.RemoveDrawHandler();
		uihorizontalList3.SetHorizontalAlign(0f);
		UIFittedSprite uifittedSprite2 = new UIFittedSprite(uihorizontalList3, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_thumbs_up_off", null), true, true);
		uifittedSprite2.SetHeight(0.2f, RelativeTo.ParentWidth);
		UICanvas uicanvas5 = new UICanvas(uihorizontalList3, false, string.Empty, null, string.Empty);
		uicanvas5.SetWidth(0.285f, RelativeTo.ParentWidth);
		uicanvas5.SetHeight(0.1f, RelativeTo.ParentWidth);
		uicanvas5.RemoveDrawHandler();
		UIFittedText uifittedText3 = new UIFittedText(uicanvas5, false, string.Empty, PsUIProfileLevelCard.m_loop.GetVisualLikes().ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#68FF38", null);
		uifittedText3.SetHorizontalAlign(0f);
		UIHorizontalList uihorizontalList4 = new UIHorizontalList(uihorizontalList2, string.Empty);
		uihorizontalList4.RemoveDrawHandler();
		uihorizontalList4.SetHorizontalAlign(1f);
		UICanvas uicanvas6 = new UICanvas(uihorizontalList4, false, string.Empty, null, string.Empty);
		uicanvas6.SetWidth(0.285f, RelativeTo.ParentWidth);
		uicanvas6.SetHeight(0.1f, RelativeTo.ParentWidth);
		uicanvas6.RemoveDrawHandler();
		UIFittedText uifittedText4 = new UIFittedText(uicanvas6, false, string.Empty, PsUIProfileLevelCard.m_loop.GetVisualDislikes().ToString(), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#DF492F", null);
		uifittedText4.SetHorizontalAlign(1f);
		UIFittedSprite uifittedSprite3 = new UIFittedSprite(uihorizontalList4, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_thumbs_down_off", null), true, true);
		uifittedSprite3.SetHeight(0.2f, RelativeTo.ParentWidth);
		UIVerticalList uiverticalList2 = new UIVerticalList(this, string.Empty);
		uiverticalList2.SetSpacing(0.08f, RelativeTo.OwnWidth);
		uiverticalList2.RemoveDrawHandler();
		UIHorizontalList uihorizontalList5 = new UIHorizontalList(uiverticalList2, string.Empty);
		uihorizontalList5.SetSpacing(0.075f, RelativeTo.ParentWidth);
		uihorizontalList5.SetMargins(0f, 0f, 0.01f, -0.01f, RelativeTo.ScreenHeight);
		uihorizontalList5.SetDepthOffset(-10f);
		uihorizontalList5.RemoveDrawHandler();
		this.m_share = new PsUIGenericButton(uihorizontalList5, 0.25f, 0.25f, 0.005f, "Button");
		this.m_share.SetHeight(0.3f, RelativeTo.ParentWidth);
		this.m_share.SetMargins(0.015f, 0.015f, 0f, 0f, RelativeTo.ScreenHeight);
		UIComponent uicomponent = new UIComponent(this.m_share, false, string.Empty, null, null, string.Empty);
		uicomponent.RemoveDrawHandler();
		uicomponent.SetSize(0.07f, 0.05f, RelativeTo.ScreenHeight);
		string text2 = "menu_icon_share";
		UIFittedSprite uifittedSprite4 = new UIFittedSprite(uicomponent, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text2, null), true, true);
		this.m_play = new PsUIGenericButton(uihorizontalList5, 0.25f, 0.25f, 0.005f, "Button");
		this.m_play.SetGreenColors(true);
		this.m_play.SetMargins(0.015f, 0.015f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		this.m_play.SetFittedText(PsStrings.Get(StringID.PLAY), 0.3f, 0.45f, RelativeTo.ParentWidth, true);
		this.m_play.SetHeight(0.3f, RelativeTo.ParentWidth);
		if (PsUIProfileLevelCard.m_loop.GetCreatorId() == PlayerPrefsX.GetUserId())
		{
			UIHorizontalList uihorizontalList6 = new UIHorizontalList(uiverticalList2, string.Empty);
			uihorizontalList6.SetSpacing(0.075f, RelativeTo.ParentWidth);
			uihorizontalList6.SetDepthOffset(-10f);
			uihorizontalList6.RemoveDrawHandler();
			this.m_edit = new PsUIGenericButton(uihorizontalList6, 0.25f, 0.25f, 0.005f, "Button");
			this.m_edit.SetFittedText(PsStrings.Get(StringID.WORD_EDIT), 0.045f, 3f, RelativeTo.OwnHeight, false);
			this.m_edit.SetHeight(0.4f, RelativeTo.ParentHeight);
			this.m_edit.SetHorizontalAlign(0f);
			this.m_edit.SetRedColors();
			this.m_delete = new PsUIGenericButton(uihorizontalList6, 0.25f, 0.25f, 0.003f, "Button");
			this.m_delete.SetMargins(0.01f, 0.01f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
			this.m_delete.SetHeight(0.38f, RelativeTo.ParentHeight);
			this.m_delete.SetIcon("menu_icon_delete", 1.2f, RelativeTo.ParentHeight, "#FFFFFF", default(cpBB));
			this.m_delete.SetHorizontalAlign(1f);
			this.m_delete.SetRedColors();
		}
	}

	// Token: 0x0600175A RID: 5978 RVA: 0x000FD940 File Offset: 0x000FBD40
	public void SimpleLikeDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		float num = 0.5f;
		if (this.m_positive + this.m_negative != 0)
		{
			num = (float)this.m_positive / (float)(this.m_positive + this.m_negative);
		}
		float num2 = _c.m_actualWidth * num;
		if (num2 <= _c.m_actualHeight * 0.5f)
		{
			num2 = _c.m_actualHeight * 1.01f;
		}
		Vector2[] roundedRect = DebugDraw.GetRoundedRect(_c.m_actualWidth, _c.m_actualHeight, _c.m_actualHeight * 0.5f, 6, Vector2.zero);
		Vector2[] roundedRect2 = DebugDraw.GetRoundedRect(num2, _c.m_actualHeight, _c.m_actualHeight * 0.5f, 6, Vector2.zero);
		Vector2[] roundedRect3 = DebugDraw.GetRoundedRect(_c.m_actualWidth - _c.m_actualHeight * 0.4f, _c.m_actualHeight * 0.35f, _c.m_actualHeight * 0.35f, 6, Vector2.up * _c.m_actualHeight * 0.325f);
		if (_c.m_actualWidth > num2)
		{
			int num3 = 12;
			for (int i = num3; i < 6 + num3; i++)
			{
				roundedRect2[i] = new Vector2(num2 / 2f, _c.m_actualHeight / 2f);
			}
			num3 = 18;
			for (int j = num3; j < 6 + num3; j++)
			{
				roundedRect2[j] = new Vector2(num2 / 2f, -_c.m_actualHeight / 2f);
			}
		}
		GGData ggdata = new GGData(roundedRect);
		GGData ggdata2 = new GGData(roundedRect2);
		GGData ggdata3 = new GGData(roundedRect3);
		Color color = DebugDraw.HexToColor("#DF492F");
		Color color2 = DebugDraw.HexToColor("#68FF38");
		Color white = Color.white;
		white.a = 0.35f;
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.zero, ggdata, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -1f, roundedRect, 0.1f * _c.m_actualHeight, color, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		if (num > 0f)
		{
			PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, new Vector3(-_c.m_actualWidth / 2f + _c.m_actualWidth * num / 2f, 0f, 0f) + Vector3.forward * -2f, ggdata2, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
			PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, new Vector3(-_c.m_actualWidth / 2f + _c.m_actualWidth * num / 2f, 0f, 0f) + Vector3.forward * -3f, roundedRect2, 0.1f * _c.m_actualHeight, color2, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		}
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.forward * -4f, ggdata3, white, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -5f, roundedRect3, 0.05f * _c.m_actualHeight, white, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
	}

	// Token: 0x0600175B RID: 5979 RVA: 0x000FDCC0 File Offset: 0x000FC0C0
	public override void Step()
	{
		if (this.m_hit)
		{
			TouchAreaS.CancelAllTouches(null);
			this.Destroy();
			if (this.m_closeAction != null)
			{
				this.m_closeAction.Invoke();
			}
		}
		else if (this.m_play.m_hit)
		{
			TouchAreaS.CancelAllTouches(null);
			if (!PlayerPrefsX.GetNameChanged() && PsUIProfileLevelCard.m_loop.GetGameMode() == PsGameMode.Race)
			{
				PsUserNameInputState psUserNameInputState = new PsUserNameInputState();
				psUserNameInputState.m_lastState = Main.m_currentGame.m_currentScene.GetCurrentState();
				psUserNameInputState.m_continueAction = new Action(PsUIProfileLevelCard.m_loop.StartLoop);
				PsMenuScene.m_lastIState = psUserNameInputState;
				PsMenuScene.m_lastState = null;
				Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(psUserNameInputState);
			}
			else
			{
				PsUIProfileLevelCard.m_loop.StartLoop();
			}
		}
		else if (this.m_share != null && this.m_share.m_hit)
		{
			TouchAreaS.CancelAllTouches(null);
			string levelLinkUrl = PsUrlLaunch.GetLevelLinkUrl(PsUIProfileLevelCard.m_loop.m_minigameId, PsUIProfileLevelCard.m_loop.GetName(), PsUIProfileLevelCard.m_loop.GetCreatorName());
			Share.ShareTextOnPlatform(levelLinkUrl);
			PsMetrics.LevelShared("levelCard", PsUIProfileLevelCard.m_loop.m_minigameId, PsUIProfileLevelCard.m_loop.GetName(), PsUIProfileLevelCard.m_loop.GetCreatorId(), PsUIProfileLevelCard.m_loop.GetCreatorName());
		}
		else if (this.m_edit != null && this.m_edit.m_hit)
		{
			TouchAreaS.CancelAllTouches(null);
			this.OpenEditPopup();
		}
		else if (this.m_delete != null && this.m_delete.m_hit)
		{
			TouchAreaS.CancelAllTouches(null);
			this.OpenDeletePopup();
		}
		base.Step();
	}

	// Token: 0x0600175C RID: 5980 RVA: 0x000FDE74 File Offset: 0x000FC274
	private void OpenEditPopup()
	{
		PsUICenterOwnLevels.m_pause = true;
		if (PsUICenterOwnLevels.m_freeSlots.Count > 0)
		{
			this.m_popup = new PsUIBasePopup(typeof(PsUIProfileLevelCard.PsUIPopupConfirmEdit), null, null, null, true, true, InitialPage.Center, false, false, false);
			this.m_popup.SetAction("Proceed", new Action(this.Purchase));
			this.m_popup.SetAction("Exit", delegate
			{
				this.DestroyPopup(false);
			});
			PsMetagameManager.ShowResources(null, true, true, false, false, 0.03f, false, false, false);
		}
		else
		{
			this.m_popup = new PsUIBasePopup(typeof(PsUIProfileLevelCard.PsUIPopupSlotsFull), null, null, null, true, true, InitialPage.Center, false, false, false);
			this.m_popup.SetAction("Exit", delegate
			{
				this.DestroyPopup(false);
			});
		}
	}

	// Token: 0x0600175D RID: 5981 RVA: 0x000FDF40 File Offset: 0x000FC340
	private void OpenDeletePopup()
	{
		PsUICenterOwnLevels.m_pause = true;
		PsUIBasePopup psUIBasePopup = new PsUIBasePopup(typeof(PsUIProfileLevelCard.PsUIPopupConfirmation), null, null, null, true, true, InitialPage.Center, false, false, false);
		psUIBasePopup.SetAction("Proceed", new Action(this.DeleteLevel));
		TweenS.AddTransformTween(psUIBasePopup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
	}

	// Token: 0x0600175E RID: 5982 RVA: 0x000FDFBC File Offset: 0x000FC3BC
	private void Purchase()
	{
		int totalCoinsEarned = PsUIProfileLevelCard.m_loop.m_minigameMetaData.totalCoinsEarned;
		if (PsMetagameManager.m_playerStats.coins >= totalCoinsEarned)
		{
			this.PurchaseLevelEdit(totalCoinsEarned, false);
			this.DestroyPopup(true);
		}
		else
		{
			this.OpenCoinPopup(totalCoinsEarned);
		}
	}

	// Token: 0x0600175F RID: 5983 RVA: 0x000FE004 File Offset: 0x000FC404
	private void DestroyPopup(bool pause = true)
	{
		if (this.m_popup != null)
		{
			this.m_popup.Destroy();
			this.m_popup = null;
		}
		if (!pause)
		{
			PsUICenterOwnLevels.m_pause = false;
		}
		if (this.m_loadingPopup != null)
		{
			this.m_loadingPopup.Destroy();
			this.m_loadingPopup = null;
		}
	}

	// Token: 0x06001760 RID: 5984 RVA: 0x000FE058 File Offset: 0x000FC458
	private void DestroyCoinPopup()
	{
		PsMetagameManager.ShowResources(null, true, true, false, false, 0.03f, false, false, false);
		if (this.m_coinPopup != null)
		{
			this.m_coinPopup.Destroy();
			this.m_coinPopup = null;
		}
		PsMetagameManager.m_coinsToDiamondsConvertAmount = 0;
	}

	// Token: 0x06001761 RID: 5985 RVA: 0x000FE09C File Offset: 0x000FC49C
	private void OpenCoinPopup(int _price)
	{
		TouchAreaS.Enable();
		PsMetagameManager.m_coinsToDiamondsConvertAmount = _price - PsMetagameManager.m_playerStats.coins;
		this.m_coinPopup = new PsUIBasePopup(typeof(PsUICenterNotEnoughCoinsConversion), null, null, null, true, true, InitialPage.Center, false, false, false);
		this.m_coinPopup.SetAction("Exit", new Action(this.DestroyCoinPopup));
		this.m_coinPopup.SetAction("Upgrade", delegate
		{
			this.PurchaseLevelEdit(_price, true);
		});
		TweenS.AddTransformTween(this.m_popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
		PsMetagameManager.ShowResources(null, true, true, true, false, 0.03f, false, false, false);
	}

	// Token: 0x06001762 RID: 5986 RVA: 0x000FE180 File Offset: 0x000FC580
	private void PurchaseLevelEdit(int _price, bool _withDiamonds)
	{
		if (_withDiamonds)
		{
			int num = _price - PsMetagameManager.m_playerStats.coins;
			int coins = PsMetagameManager.m_playerStats.coins;
			int num2 = PsMetagameManager.CoinsToDiamonds(num, true);
			if (PsMetagameManager.m_playerStats.diamonds >= num2)
			{
				TouchAreaS.Disable();
				PsMetagameManager.m_playerStats.CumulateCoins(-PsMetagameManager.m_playerStats.coins);
				PsMetagameManager.m_playerStats.CumulateDiamonds(-PsMetagameManager.CoinsToDiamonds(num, true));
				this.BackToSaved();
			}
			else
			{
				new PsGetDiamondsFlow(null, null, null);
			}
		}
		else
		{
			TouchAreaS.Disable();
			PsMetagameManager.m_playerStats.CumulateCoins(-_price);
			this.BackToSaved();
		}
		PsMetagameManager.m_coinsToDiamondsConvertAmount = 0;
	}

	// Token: 0x06001763 RID: 5987 RVA: 0x000FE228 File Offset: 0x000FC628
	private void BackToSaved()
	{
		this.m_loadingPopup = new PsUIBasePopup(typeof(PsUIProfileLevelCard.PsUILoadingPopup), null, null, null, true, true, InitialPage.Center, false, false, false);
		PsUIProfileLevelCard.m_loop.m_minigameMetaData.name = "Saved level " + PsUICenterOwnLevels.m_freeSlots[0];
		PsState.m_activeGameLoop = PsUIProfileLevelCard.m_loop;
		Debug.Log("E_Test PsUIProfileLevelcard.BackToSaved", null);
		HttpC httpC = MiniGame.BackToSaved(PsUIProfileLevelCard.m_loop, new Dictionary<string, int>(), new Action<HttpC>(this.BackToSavedOK), new Action<HttpC>(this.BackToSavedFailed), null);
		PsState.m_activeGameLoop = null;
	}

	// Token: 0x06001764 RID: 5988 RVA: 0x000FE2C0 File Offset: 0x000FC6C0
	private void BackToSavedOK(HttpC _httpc)
	{
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new PsCreateState());
		this.DestroyCoinPopup();
		this.DestroyPopup(true);
		PsMetrics.PublishedLevelBackToSaved(PsUIProfileLevelCard.m_loop.m_minigameId);
		TouchAreaS.Enable();
	}

	// Token: 0x06001765 RID: 5989 RVA: 0x000FE2FC File Offset: 0x000FC6FC
	private void BackToSavedFailed(HttpC _httpc)
	{
		ServerManager.ThrowServerErrorException(PsStrings.Get(StringID.CONNECTION_ERROR_HEADER), _httpc.www, delegate
		{
			HttpC httpC = MiniGame.BackToSaved(PsUIProfileLevelCard.m_loop, new Dictionary<string, int>(), new Action<HttpC>(this.BackToSavedOK), new Action<HttpC>(this.BackToSavedFailed), null);
			httpC.objectData = _httpc.objectData;
			return httpC;
		}, null);
	}

	// Token: 0x06001766 RID: 5990 RVA: 0x000FE344 File Offset: 0x000FC744
	private void DeleteLevel()
	{
		TouchAreaS.Disable();
		this.m_loadingPopup = new PsUIBasePopup(typeof(PsUIProfileLevelCard.PsUILoadingPopup), null, null, null, true, true, InitialPage.Center, false, false, false);
		Debug.Log("E_Test PsUIProfileLevelcard.Delete", null);
		PsMetagameManager.DeleteMinigame(PsUIProfileLevelCard.m_loop.m_minigameId, new Dictionary<string, int>(), new Action<HttpC>(this.DeleteOK));
	}

	// Token: 0x06001767 RID: 5991 RVA: 0x000FE39F File Offset: 0x000FC79F
	private void DeleteOK(HttpC _httpc)
	{
		Main.m_currentGame.m_currentScene.m_stateMachine.ChangeState(new PsCreateState());
		this.DestroyCoinPopup();
		this.DestroyPopup(true);
		PsMetrics.PublishedLevelDeleted(PsUIProfileLevelCard.m_loop.m_minigameId);
		TouchAreaS.Enable();
	}

	// Token: 0x06001768 RID: 5992 RVA: 0x000FE3DC File Offset: 0x000FC7DC
	protected override void OnTouchRollIn(TLTouch _touch, bool _secondary)
	{
		base.OnTouchRollIn(_touch, _secondary);
		if (this.m_touchScaleTween != null)
		{
			TweenS.RemoveComponent(this.m_touchScaleTween);
			this.m_touchScaleTween = null;
		}
		this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1.05f, 1.05f, 1.05f), 0.1f, 0f, false);
	}

	// Token: 0x06001769 RID: 5993 RVA: 0x000FE444 File Offset: 0x000FC844
	protected override void OnTouchBegan(TLTouch _touch)
	{
		base.OnTouchBegan(_touch);
		if (this.m_touchScaleTween != null)
		{
			TweenS.RemoveComponent(this.m_touchScaleTween);
			this.m_touchScaleTween = null;
		}
		this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1.05f, 1.05f, 1.05f), 0.1f, 0f, false);
	}

	// Token: 0x0600176A RID: 5994 RVA: 0x000FE4A8 File Offset: 0x000FC8A8
	protected override void OnTouchRollOut(TLTouch _touch, bool _secondary)
	{
		base.OnTouchRollOut(_touch, _secondary);
		if (this.m_touchScaleTween != null)
		{
			TweenS.RemoveComponent(this.m_touchScaleTween);
			this.m_touchScaleTween = null;
		}
		this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1f, 1f, 1f), 0.1f, 0f, false);
	}

	// Token: 0x0600176B RID: 5995 RVA: 0x000FE510 File Offset: 0x000FC910
	protected override void OnTouchRelease(TLTouch _touch, bool _inside)
	{
		base.OnTouchRelease(_touch, _inside);
		if (this.m_touchScaleTween != null)
		{
			TweenS.RemoveComponent(this.m_touchScaleTween);
			this.m_touchScaleTween = null;
		}
		this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1f, 1f, 1f), 0.1f, 0f, false);
		if (_inside)
		{
			SoundS.PlaySingleShot("/UI/UpgradeClose", Vector3.zero, 1f);
		}
	}

	// Token: 0x04001A20 RID: 6688
	public static PsGameLoop m_loop;

	// Token: 0x04001A21 RID: 6689
	private PsUIGenericButton m_play;

	// Token: 0x04001A22 RID: 6690
	private PsUIGenericButton m_share;

	// Token: 0x04001A23 RID: 6691
	private PsUIGenericButton m_edit;

	// Token: 0x04001A24 RID: 6692
	private PsUIGenericButton m_delete;

	// Token: 0x04001A25 RID: 6693
	public TweenC m_touchScaleTween;

	// Token: 0x04001A26 RID: 6694
	private Action m_closeAction;

	// Token: 0x04001A27 RID: 6695
	private int m_positive;

	// Token: 0x04001A28 RID: 6696
	private int m_negative;

	// Token: 0x04001A29 RID: 6697
	private PsUIBasePopup m_popup;

	// Token: 0x04001A2A RID: 6698
	private PsUIBasePopup m_coinPopup;

	// Token: 0x04001A2B RID: 6699
	private PsUIBasePopup m_loadingPopup;

	// Token: 0x02000318 RID: 792
	private class PsUIPopupSlotsFull : PsUIHeaderedCanvas
	{
		// Token: 0x0600176E RID: 5998 RVA: 0x000FE5A4 File Offset: 0x000FC9A4
		public PsUIPopupSlotsFull(UIComponent _parent)
			: base(_parent, string.Empty, false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
		{
			(this.GetRoot() as PsUIBasePopup).m_scrollableCanvas.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.MenuPopupBackground));
			this.SetWidth(0.65f, RelativeTo.ScreenWidth);
			this.SetHeight(0.45f, RelativeTo.ScreenHeight);
			this.SetMargins(0.0225f, 0.0225f, 0f, 0.0125f, RelativeTo.ScreenHeight);
			this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
			this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
			UICanvas uicanvas = new UICanvas(this.m_header, false, string.Empty, null, string.Empty);
			uicanvas.SetMargins(0.15f, 0.15f, 0.25f, 0.15f, RelativeTo.OwnHeight);
			uicanvas.RemoveDrawHandler();
			UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, PsStrings.Get(StringID.SAVED_SLOTS_FULL), PsFontManager.GetFont(PsFonts.HurmeBold), true, null, null);
			string text = PsStrings.Get(StringID.DELETE_TO_MAKE_ROOM);
			string name = PsUIProfileLevelCard.m_loop.GetName();
			text = text.Replace("%1", "''" + name + "''");
			this.m_message = new UITextbox(this, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.045f, RelativeTo.ScreenShortest, false, Align.Center, Align.Middle, null, true, null);
			UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
			uihorizontalList.SetSpacing(0.05f, RelativeTo.ScreenHeight);
			uihorizontalList.RemoveDrawHandler();
			uihorizontalList.SetVerticalAlign(0f);
			uihorizontalList.SetMargins(0f, 0f, 0.075f, -0.075f, RelativeTo.ScreenHeight);
			this.m_ok = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
			this.m_ok.SetText(PsStrings.Get(StringID.OK), 0.045f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			this.m_ok.SetHeight(0.095f, RelativeTo.ScreenHeight);
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x000FE7C5 File Offset: 0x000FCBC5
		public override void Step()
		{
			if (this.m_ok.m_hit)
			{
				(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
			}
			base.Step();
		}

		// Token: 0x04001A2E RID: 6702
		private PsUIGenericButton m_ok;

		// Token: 0x04001A2F RID: 6703
		private UITextbox m_message;
	}

	// Token: 0x02000319 RID: 793
	private class PsUIPopupConfirmEdit : PsUIHeaderedCanvas
	{
		// Token: 0x06001770 RID: 6000 RVA: 0x000FE7F4 File Offset: 0x000FCBF4
		public PsUIPopupConfirmEdit(UIComponent _parent)
			: base(_parent, string.Empty, false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
		{
			this.SetWidth(0.65f, RelativeTo.ScreenWidth);
			this.SetHeight(0.65f, RelativeTo.ScreenHeight);
			this.SetMargins(0.0225f, 0.0225f, 0f, 0.0125f, RelativeTo.ScreenHeight);
			this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
			this.m_header.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIHeader));
			UICanvas uicanvas = new UICanvas(this.m_header, false, string.Empty, null, string.Empty);
			uicanvas.SetMargins(0.15f, 0.15f, 0.25f, 0.15f, RelativeTo.OwnHeight);
			uicanvas.RemoveDrawHandler();
			UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, PsStrings.Get(StringID.EDIT_LIVE_LEVEL), PsFontManager.GetFont(PsFonts.HurmeBold), true, null, null);
			UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
			uiverticalList.SetSpacing(0.025f, RelativeTo.ScreenHeight);
			uiverticalList.RemoveDrawHandler();
			new UITextbox(uiverticalList, false, string.Empty, PsStrings.Get(StringID.EDIT_LOSE_PROGRESSION), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenShortest, false, Align.Center, Align.Middle, null, true, null);
			new UITextbox(uiverticalList, false, string.Empty, PsStrings.Get(StringID.TO_AVOID_ABUSE), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.035f, RelativeTo.ScreenShortest, false, Align.Center, Align.Middle, "#D20D07", true, null);
			UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
			uihorizontalList.SetSpacing(0.05f, RelativeTo.ScreenHeight);
			uihorizontalList.RemoveDrawHandler();
			uihorizontalList.SetVerticalAlign(0f);
			uihorizontalList.SetMargins(0f, 0f, 0.075f, -0.075f, RelativeTo.ScreenHeight);
			int totalCoinsEarned = PsUIProfileLevelCard.m_loop.m_minigameMetaData.totalCoinsEarned;
			this.m_ok = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
			this.m_ok.SetText(PsStrings.Get(StringID.WORD_EDIT), 0.045f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			this.m_ok.SetHeight(0.095f, RelativeTo.ScreenHeight);
			this.m_ok.SetCoinPrice(totalCoinsEarned);
			this.m_cancel = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
			this.m_cancel.SetText(PsStrings.Get(StringID.CANCEL), 0.045f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			this.m_cancel.SetHeight(0.095f, RelativeTo.ScreenHeight);
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x000FEA6C File Offset: 0x000FCE6C
		public override void Step()
		{
			if (this.m_ok != null && this.m_ok.m_hit)
			{
				(this.GetRoot() as PsUIBasePopup).CallAction("Proceed");
			}
			else if (this.m_cancel != null && this.m_cancel.m_hit)
			{
				(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
			}
			base.Step();
		}

		// Token: 0x04001A33 RID: 6707
		private PsUIGenericButton m_ok;

		// Token: 0x04001A34 RID: 6708
		private PsUIGenericButton m_cancel;
	}

	// Token: 0x0200031A RID: 794
	private class PsUILoadingPopup : PsUIHeaderedCanvas
	{
		// Token: 0x06001772 RID: 6002 RVA: 0x000FEAE8 File Offset: 0x000FCEE8
		public PsUILoadingPopup(UIComponent _parent)
			: base(_parent, string.Empty, false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
		{
			(this.GetRoot() as PsUIBasePopup).m_scrollableCanvas.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.MenuPopupBackground));
			this.RemoveDrawHandler();
			PsUILoadingAnimation psUILoadingAnimation = new PsUILoadingAnimation(this, true);
		}
	}

	// Token: 0x0200031B RID: 795
	private class PsUIPopupConfirmation : PsUIHeaderedCanvas
	{
		// Token: 0x06001773 RID: 6003 RVA: 0x000FEB50 File Offset: 0x000FCF50
		public PsUIPopupConfirmation(UIComponent _parent)
			: base(_parent, string.Empty, false, 0.125f, RelativeTo.ScreenHeight, 0f, RelativeTo.ScreenHeight)
		{
			(this.GetRoot() as PsUIBasePopup).m_scrollableCanvas.SetDrawHandler(new UIDrawDelegate(UIDrawHandlers.MenuPopupBackground));
			this.SetWidth(0.65f, RelativeTo.ScreenWidth);
			this.SetHeight(0.45f, RelativeTo.ScreenHeight);
			this.SetVerticalAlign(0.4f);
			this.SetMargins(0.0225f, 0.0225f, 0.0225f, 0.0225f, RelativeTo.ScreenHeight);
			this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.ScrollingUIBackground));
			this.m_header.Destroy();
			UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
			uiverticalList.SetSpacing(0.025f, RelativeTo.ScreenHeight);
			uiverticalList.RemoveDrawHandler();
			string text = PsStrings.Get(StringID.ARE_YOU_SURE);
			text = text.Replace("%1", PsUIProfileLevelCard.m_loop.GetName());
			new UITextbox(uiverticalList, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), 0.045f, RelativeTo.ScreenShortest, false, Align.Center, Align.Middle, null, true, null);
			new UITextbox(uiverticalList, false, string.Empty, PsStrings.Get(StringID.ITEMS_WILL_BE_RESTORED), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.045f, RelativeTo.ScreenShortest, false, Align.Center, Align.Middle, null, true, null);
			UIHorizontalList uihorizontalList = new UIHorizontalList(this, string.Empty);
			uihorizontalList.SetSpacing(0.05f, RelativeTo.ScreenHeight);
			uihorizontalList.RemoveDrawHandler();
			uihorizontalList.SetVerticalAlign(0f);
			uihorizontalList.SetMargins(0f, 0f, 0.075f, -0.075f, RelativeTo.ScreenHeight);
			this.m_ok = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
			this.m_ok.SetText(PsStrings.Get(StringID.DELETE).ToUpper(), 0.045f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			this.m_ok.SetHeight(0.095f, RelativeTo.ScreenHeight);
			this.m_ok.SetRedColors();
			this.m_cancel = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
			this.m_cancel.SetText(PsStrings.Get(StringID.CANCEL), 0.045f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			this.m_cancel.SetHeight(0.095f, RelativeTo.ScreenHeight);
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x000FED90 File Offset: 0x000FD190
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

		// Token: 0x04001A38 RID: 6712
		private PsUIGenericButton m_ok;

		// Token: 0x04001A39 RID: 6713
		private PsUIGenericButton m_cancel;
	}
}
