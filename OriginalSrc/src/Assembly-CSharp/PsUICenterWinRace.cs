using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002EF RID: 751
public class PsUICenterWinRace : UICanvas
{
	// Token: 0x06001622 RID: 5666 RVA: 0x000E7154 File Offset: 0x000E5554
	public PsUICenterWinRace(UIComponent _parent)
		: base(_parent, false, "CenterContent", null, string.Empty)
	{
		this.Preset();
		this.m_rating = PsState.m_activeGameLoop.GetRating();
		if (this.m_rating == PsRating.Unrated)
		{
			this.m_rating = PsRating.Neutral;
		}
		this.m_oldScore = PsState.m_activeGameLoop.m_rewardOld;
		this.m_rewardScoreAmount = PsState.m_activeGameLoop.m_currentRunScore - PsState.m_activeGameLoop.m_rewardOld;
		this.SetVerticalAlign(0f);
		this.GetRoot().SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.DebriefBackground));
		this.RemoveDrawHandler();
		UIVerticalList uiverticalList = new UIVerticalList(this, "Center");
		uiverticalList.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uiverticalList.SetWidth(1f, RelativeTo.ParentWidth);
		uiverticalList.SetVerticalAlign(0.55f);
		uiverticalList.RemoveDrawHandler();
		UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList, string.Empty);
		uihorizontalList.SetHeight(0.42f, RelativeTo.ScreenHeight);
		uihorizontalList.SetSpacing(this.m_spacing, RelativeTo.ScreenHeight);
		uihorizontalList.SetVerticalAlign(1f);
		uihorizontalList.RemoveDrawHandler();
		this.m_leftSlot = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(this.m_bottomFrameName, null), true, true);
		this.m_leftSlot.SetHeight(0.35f, RelativeTo.ScreenHeight);
		this.m_leftSlot.SetVerticalAlign(0f);
		this.m_leftSlot.SetDepthOffset(3f);
		this.m_leftSlot.RemoveDrawHandler();
		this.CreateReward(this.m_leftSlot, 1);
		if (PsState.m_activeGameLoop.m_currentRunScore > 0)
		{
			this.m_leftStar = new UIFittedSprite(this.m_leftSlot, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(this.m_topFrameName, null), true, true);
			this.m_leftStar.SetHeight(1f, RelativeTo.ParentHeight);
			TweenS.AddTransformTween(this.m_leftStar.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, Vector3.zero, Vector3.one, this.m_starAnimDuration, 0f, true);
			TimerS.AddComponent(this.m_leftSlot.m_TC.p_entity, "EffectTimer", 0f, this.m_starEffectDelay, false, delegate(TimerC _c)
			{
				this.CreateStarEffect(this.m_leftSlot.m_TC, 1);
				TimerS.RemoveComponent(_c);
				if (this.m_oldScore < 1)
				{
					this.FlyerTimerEvent();
				}
			});
		}
		this.m_centerSlot = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(this.m_bottomFrameName, null), true, true);
		this.m_centerSlot.SetHeight(0.35f, RelativeTo.ScreenHeight);
		this.m_centerSlot.SetVerticalAlign(this.m_centerVAlign);
		this.m_centerSlot.RemoveDrawHandler();
		this.CreateReward(this.m_centerSlot, 2);
		if (PsState.m_activeGameLoop.m_currentRunScore > 1)
		{
			this.m_centerStar = new UIFittedSprite(this.m_centerSlot, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(this.m_topFrameName, null), true, true);
			this.m_centerStar.SetHeight(1f, RelativeTo.ParentHeight);
			TweenS.AddTransformTween(this.m_centerStar.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, Vector3.zero, Vector3.one, this.m_starAnimDuration, this.m_starAnimDuration, true);
			TimerS.AddComponent(this.m_centerSlot.m_TC.p_entity, "EffectTimer", 0f, this.m_starAnimDuration + this.m_starEffectDelay, false, delegate(TimerC _c)
			{
				this.CreateStarEffect(this.m_centerSlot.m_TC, 2);
				TimerS.RemoveComponent(_c);
				if (this.m_oldScore < 2)
				{
					this.FlyerTimerEvent();
				}
			});
		}
		this.m_rightSlot = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(this.m_bottomFrameName, null), true, true);
		this.m_rightSlot.SetHeight(0.35f, RelativeTo.ScreenHeight);
		this.m_rightSlot.SetVerticalAlign(0f);
		this.m_rightSlot.SetDepthOffset(3f);
		this.m_rightSlot.RemoveDrawHandler();
		this.CreateReward(this.m_rightSlot, 3);
		if (PsState.m_activeGameLoop.m_currentRunScore > 2)
		{
			this.m_rightStar = new UIFittedSprite(this.m_rightSlot, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(this.m_topFrameName, null), true, true);
			this.m_rightStar.SetHeight(1f, RelativeTo.ParentHeight);
			TweenS.AddTransformTween(this.m_rightStar.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, Vector3.zero, Vector3.one, this.m_starAnimDuration, this.m_starAnimDuration * 2f, true);
			TimerS.AddComponent(this.m_rightSlot.m_TC.p_entity, "EffectTimer", 0f, this.m_starAnimDuration * 2f + this.m_starEffectDelay, false, delegate(TimerC _c)
			{
				this.CreateStarEffect(this.m_rightSlot.m_TC, 3);
				TimerS.RemoveComponent(_c);
				if (this.m_oldScore < 3)
				{
					this.FlyerTimerEvent();
				}
			});
		}
		UIVerticalList uiverticalList2 = new UIVerticalList(uiverticalList, string.Empty);
		uiverticalList2.RemoveDrawHandler();
		if (PsState.m_activeGameLoop.m_timeScoreCurrent == PsState.m_activeGameLoop.m_timeScoreBest)
		{
			UIText uitext = new UIText(uiverticalList2, false, string.Empty, "New Record!", PsFontManager.GetFont(PsFonts.HurmeBold), 0.03f, RelativeTo.ScreenHeight, "#FFFFFF", "#000000");
			uitext.SetShadowShift(new Vector2(0f, -1f), 0.035f);
			UIText uitext2 = new UIText(uiverticalList2, false, string.Empty, HighScores.TimeScoreToTimeString(PsState.m_activeGameLoop.m_timeScoreCurrent), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.07f, RelativeTo.ScreenHeight, "#2ce768", "#121313");
			uitext2.SetShadowShift(new Vector2(0f, -1f), 0.035f);
		}
		else
		{
			UIText uitext3 = new UIText(uiverticalList2, false, string.Empty, HighScores.TimeScoreToTimeString(PsState.m_activeGameLoop.m_timeScoreCurrent), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.07f, RelativeTo.ScreenHeight, "#2ce768", "#121313");
			uitext3.SetShadowShift(new Vector2(0f, -1f), 0.035f);
			UIText uitext4 = new UIText(uiverticalList2, false, string.Empty, "Your best time is: " + HighScores.TimeScoreToTimeString(PsState.m_activeGameLoop.m_timeScoreBest), PsFontManager.GetFont(PsFonts.HurmeBold), 0.03f, RelativeTo.ScreenHeight, "#DFDFDF", "#121313");
			uitext4.SetShadowShift(new Vector2(0f, -1f), 0.035f);
		}
		this.CreateVideoReplayButton();
		this.CreateRetryButton();
		UIVerticalList uiverticalList3 = new UIVerticalList(this, string.Empty);
		uiverticalList3.SetMargins(0.035f, RelativeTo.ScreenShortest);
		uiverticalList3.SetAlign(1f, 0f);
		uiverticalList3.SetSpacing(0.0275f, RelativeTo.ScreenHeight);
		uiverticalList3.RemoveDrawHandler();
		this.m_continueButton = new PsUIGenericButton(uiverticalList3, 0.25f, 0.25f, 0.0085f, "Button");
		this.m_continueButton.SetText(PsStrings.Get(StringID.CONTINUE), 0.07f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		UIVerticalList uiverticalList4 = new UIVerticalList(this, string.Empty);
		uiverticalList4.SetMargins(0.03f, RelativeTo.ScreenShortest);
		uiverticalList4.SetAlign(0f, 0f);
		uiverticalList4.SetSpacing(0.025f, RelativeTo.ScreenHeight);
		uiverticalList4.RemoveDrawHandler();
		if (PsMetagameData.GetUnlockableByIdentifier("Editor").m_unlocked)
		{
			this.CreateLikeButtons(uiverticalList4);
			if (!(this is PsUICenterWinRaceFresh))
			{
				this.m_offensiveButton = new PsUIGenericButton(uiverticalList4, 0.25f, 0.25f, 0.005f, "Button");
				this.m_offensiveButton.SetText("Offensive", 0.026f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
				this.m_offensiveButton.SetRedColors();
				this.m_offensiveButton.SetHorizontalAlign(0.03f);
			}
		}
	}

	// Token: 0x06001623 RID: 5667 RVA: 0x000E78D0 File Offset: 0x000E5CD0
	public virtual void CreateFriendRaceButton()
	{
		this.m_pvpFriendButton = new PsUIGenericButton(this, 0.25f, 0.25f, 0.005f, "Button");
		this.m_pvpFriendButton.SetText("Challenge\nFriend", 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
		this.m_pvpFriendButton.SetAlign(0.965f, 0.3f);
	}

	// Token: 0x06001624 RID: 5668 RVA: 0x000E7930 File Offset: 0x000E5D30
	public virtual void Preset()
	{
		PsMetagameManager.ShowResources(this.m_camera, false, true, false, false, 0.15f, false, false, false);
		this.m_topFrameName = "hud_big_star_top";
		this.m_bottomFrameName = "hud_big_star_bottom";
		this.m_spacing = -0.07f;
		this.m_centerVAlign = 1f;
	}

	// Token: 0x06001625 RID: 5669 RVA: 0x000E7980 File Offset: 0x000E5D80
	public virtual void CreateStarEffect(TransformC _tc, int _starEffectNumber)
	{
		PrefabC prefabC = PrefabS.AddComponent(_tc, Vector3.zero, ResourceManager.GetGameObject("StarReward" + _starEffectNumber + "_GameObject"));
		prefabC.p_gameObject.transform.position -= new Vector3(0f, 0f, 250f);
		prefabC.p_gameObject.transform.Rotate(90f, 0f, 0f);
		PrefabS.SetCamera(prefabC, this.m_camera);
		Debug.Log("EFFECT NUMBER: " + _starEffectNumber, null);
		ParticleSystem[] array = prefabC.p_gameObject.GetComponents<ParticleSystem>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Play();
		}
		array = prefabC.p_gameObject.GetComponentsInChildren<ParticleSystem>();
		for (int j = 0; j < array.Length; j++)
		{
			array[j].Play();
		}
		if (this.m_topFrameName.Equals("hud_big_diamond_top"))
		{
			SoundS.PlaySingleShotWithParameter("/Ingame/Events/GameEnd_DiamondGain", Vector3.zero, "Result", (float)_starEffectNumber, 1f);
		}
		else if (this.m_topFrameName.Equals("hud_big_key_top"))
		{
			SoundS.PlaySingleShotWithParameter("/Ingame/Events/GameEnd_KeyGain", Vector3.zero, "Result", (float)_starEffectNumber, 1f);
		}
		else
		{
			SoundS.PlaySingleShotWithParameter("/Ingame/Events/GameEnd_StarGain", Vector3.zero, "Result", (float)_starEffectNumber, 1f);
		}
	}

	// Token: 0x06001626 RID: 5670 RVA: 0x000E7AF8 File Offset: 0x000E5EF8
	public virtual void CreateReward(UIComponent _parent, int _score)
	{
		UIVerticalList uiverticalList = new UIVerticalList(_parent, string.Empty);
		uiverticalList.SetVerticalAlign(0.385f);
		uiverticalList.RemoveDrawHandler();
		if (PsState.m_activeGameLoop.m_rewardOld < _score)
		{
			UIHorizontalList uihorizontalList = new UIHorizontalList(uiverticalList, string.Empty);
			uihorizontalList.SetHeight(0.175f, RelativeTo.ParentHeight);
			uihorizontalList.RemoveDrawHandler();
			if (PsState.m_activeGameLoop.m_currentRunScore < _score)
			{
				UIText uitext = new UIText(uihorizontalList, false, string.Empty, "+" + PsMetagameManager.GetGoalCoinReward(1, PsState.m_activeGameLoop.GetDifficulty()), PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.8f, RelativeTo.ParentHeight, "#ffe025", null);
				UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_resources_coin_icon", null), true, true);
				uifittedSprite.SetHeight(1f, RelativeTo.ParentHeight);
			}
			if (PsState.m_activeGameLoop.m_currentRunScore >= _score)
			{
				this.m_flyers.Add(uihorizontalList);
			}
		}
		if (PsState.m_activeGameLoop.m_currentRunScore < _score)
		{
			UIVerticalList uiverticalList2 = new UIVerticalList(uiverticalList, string.Empty);
			uiverticalList2.RemoveDrawHandler();
			PsGameModeRace psGameModeRace = PsState.m_activeGameLoop.m_gameMode as PsGameModeRace;
			float num = ((_score != 1) ? ((_score != 2) ? psGameModeRace.m_threeMedalTime : psGameModeRace.m_twoMedalTime) : psGameModeRace.m_oneMedalTime);
			UIText uitext2 = new UIText(uiverticalList2, false, string.Empty, (HighScores.TimeScoreToTime(PsState.m_activeGameLoop.m_timeScoreCurrent) - num).ToString("F3"), PsFontManager.GetFont(PsFonts.KGSecondChances), 0.07875f, RelativeTo.ParentHeight, "#52a7ad", null);
			UIText uitext3 = new UIText(uiverticalList2, false, string.Empty, "faster", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.07875f, RelativeTo.ParentHeight, "#52a7ad", null);
		}
	}

	// Token: 0x06001627 RID: 5671 RVA: 0x000E7CBC File Offset: 0x000E60BC
	public virtual void CreateLikeButtons(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, string.Empty);
		uihorizontalList.SetSpacing(0.03f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		PsUICharacter psUICharacter = new PsUICharacter(uihorizontalList, PsDialogueCharacter.Architect, PsDialogueCharacterPosition.Left, 1f);
		psUICharacter.SetHeight(0.25f, RelativeTo.ScreenHeight);
		psUICharacter.LookAtCamera();
		UICanvas uicanvas = new UICanvas(uihorizontalList, false, "bubblecanvas", null, string.Empty);
		uicanvas.SetHeight(0.13f, RelativeTo.ScreenHeight);
		uicanvas.SetWidth(2.15f, RelativeTo.OwnHeight);
		uicanvas.SetMargins(0.02f, 0.02f, 0.0125f, -0.06f, RelativeTo.ScreenHeight);
		uicanvas.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.RatingSpeechBubble));
		uicanvas.SetVerticalAlign(0f);
		new UITextbox(uicanvas, false, string.Empty, "Liked this level?", PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.03f, RelativeTo.ScreenHeight, false, Align.Left, Align.Top, null, true, null)
		{
			m_tmc = 
			{
				m_textMesh = 
				{
					color = DebugDraw.HexToColor("#00b6ea")
				}
			}
		}.SetAlign(0.5f, 1f);
		this.m_likeArea = new UIHorizontalList(uicanvas, string.Empty);
		this.m_likeArea.SetSpacing(0f, RelativeTo.ScreenHeight);
		this.m_likeArea.SetHeight(0.15f, RelativeTo.ScreenHeight);
		this.m_likeArea.SetMargins(0.02f, 0.02f, 0f, 0f, RelativeTo.ScreenHeight);
		this.m_likeArea.SetAlign(0.5f, 0f);
		this.m_likeArea.RemoveDrawHandler();
		string text = "menu_thumbs_up_off";
		string text2 = "menu_thumbs_down_off";
		if (this.m_rating == PsRating.Positive || this.m_rating == PsRating.Elated)
		{
			text = "menu_thumbs_up_on";
		}
		else if (this.m_rating == PsRating.Negative || this.m_rating == PsRating.Rejecting)
		{
			text2 = "menu_thumbs_down_on";
		}
		this.m_thumbsDownButton = new UIRectSpriteButton(this.m_likeArea, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text2, null), true, false);
		this.m_thumbsDownButton.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_thumbsUpButton = new UIRectSpriteButton(this.m_likeArea, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text, null), true, false);
		this.m_thumbsUpButton.SetHeight(1f, RelativeTo.ParentHeight);
	}

	// Token: 0x06001628 RID: 5672 RVA: 0x000E7F10 File Offset: 0x000E6310
	public virtual void CreateRetryButton()
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, "Retry");
		uihorizontalList.SetAlign(1f, 1f);
		uihorizontalList.SetMargins(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.RemoveDrawHandler();
		this.m_restartButton = new PsUIGenericButton(uihorizontalList, 0.25f, 0.25f, 0.005f, "Button");
		this.m_restartButton.SetIcon("hud_icon_restart", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_restartButton.SetOrangeColors(true);
		this.m_restartButton.SetMargins(0.035f, 0.035f, 0.02f, 0.02f, RelativeTo.ScreenHeight);
	}

	// Token: 0x06001629 RID: 5673 RVA: 0x000E7FBC File Offset: 0x000E63BC
	public virtual void CreateVideoReplayButton()
	{
		if (EveryplayManager.IsEnabled())
		{
			this.m_everyplayButton = new PsUIGenericButton(this, 0.25f, 0.25f, 0.005f, "Button");
			this.m_everyplayButton.SetIcon("hud_button_replay", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
			this.m_everyplayButton.SetText("Watch\nReplay!", 0.03f, 0f, RelativeTo.ScreenHeight, false, RelativeTo.ScreenShortest);
			this.m_everyplayButton.SetBlueColors(true);
			this.m_everyplayButton.SetMargins(0.035f, 0.035f, 0.02f, 0.02f, RelativeTo.ScreenHeight);
			this.m_everyplayButton.SetAlign(0.5f, 0.025f);
		}
	}

	// Token: 0x0600162A RID: 5674 RVA: 0x000E8078 File Offset: 0x000E6478
	public virtual void FlyerTimerEvent()
	{
		PsMetagameManager.m_menuResourceView.CreateFlyingResources(PsMetagameManager.GetGoalCoinReward(1, PsState.m_activeGameLoop.GetDifficulty()), this.m_camera.WorldToScreenPoint(this.m_flyers[0].m_TC.transform.position) - new Vector3((float)Screen.width, (float)Screen.height, 0f) * 0.5f, ResourceType.Coins, 0f, null, null, null, null, default(Vector2));
		this.m_flyers.RemoveAt(0);
	}

	// Token: 0x0600162B RID: 5675 RVA: 0x000E8110 File Offset: 0x000E6510
	public override void Step()
	{
		if (this.m_restartButton.m_hit)
		{
			this.RateLevel();
			(this.GetRoot() as PsUIBasePopup).CallAction("Restart");
		}
		else if (this.m_continueButton.m_hit)
		{
			this.RateLevel();
			(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
		}
		else if (this.m_everyplayButton != null && this.m_everyplayButton.m_hit)
		{
			Debug.Log("Opening everyplay...", null);
			EveryplayManager.StopRecording(0f);
			EveryplayManager.PlayLastRecording();
		}
		else if (this.m_thumbsUpButton != null && this.m_thumbsUpButton.m_hit)
		{
			if (this.m_rating == PsRating.Positive)
			{
				this.m_ratingDone = true;
				this.m_rating = PsRating.Neutral;
				this.m_thumbsUpButton.SetFrame(PsState.m_uiSheet.m_atlas.GetFrame("menu_thumbs_up_off", null));
				this.m_thumbsDownButton.SetFrame(PsState.m_uiSheet.m_atlas.GetFrame("menu_thumbs_down_off", null));
				this.m_likeArea.Update();
			}
			else
			{
				this.m_ratingDone = true;
				this.m_rating = PsRating.Positive;
				this.m_thumbsUpButton.SetFrame(PsState.m_uiSheet.m_atlas.GetFrame("menu_thumbs_up_on", null));
				this.m_thumbsDownButton.SetFrame(PsState.m_uiSheet.m_atlas.GetFrame("menu_thumbs_down_off", null));
				this.m_likeArea.Update();
			}
		}
		else if (this.m_thumbsDownButton != null && this.m_thumbsDownButton.m_hit)
		{
			if (this.m_rating == PsRating.Negative)
			{
				this.m_ratingDone = true;
				this.m_rating = PsRating.Neutral;
				this.m_thumbsUpButton.SetFrame(PsState.m_uiSheet.m_atlas.GetFrame("menu_thumbs_up_off", null));
				this.m_thumbsDownButton.SetFrame(PsState.m_uiSheet.m_atlas.GetFrame("menu_thumbs_down_off", null));
				this.m_likeArea.Update();
			}
			else
			{
				this.m_ratingDone = true;
				this.m_rating = PsRating.Negative;
				this.m_thumbsUpButton.SetFrame(PsState.m_uiSheet.m_atlas.GetFrame("menu_thumbs_up_off", null));
				this.m_thumbsDownButton.SetFrame(PsState.m_uiSheet.m_atlas.GetFrame("menu_thumbs_down_on", null));
				this.m_likeArea.Update();
			}
		}
		else if (this.m_offensiveButton != null && this.m_offensiveButton.m_hit)
		{
			this.m_offensive = new PsUIBasePopup(typeof(PsUICenterOffensive), typeof(PsUITopBanner), null, null, true, false, InitialPage.Center, false, false, false);
			this.m_offensive.SetAction("Ok", new Action(this.OkOffensive));
			this.m_offensive.SetAction("Cancel", new Action(this.CancelOffensive));
		}
		base.Step();
	}

	// Token: 0x0600162C RID: 5676 RVA: 0x000E83FF File Offset: 0x000E67FF
	private void RateLevel()
	{
		if (this.m_ratingDone)
		{
			PsMetagameManager.SendRating(this.m_rating, PsState.m_activeGameLoop, PsState.m_activeMinigame, false);
		}
	}

	// Token: 0x0600162D RID: 5677 RVA: 0x000E8422 File Offset: 0x000E6822
	public void OkOffensive()
	{
		this.m_offensive.Destroy();
		this.m_offensive = null;
		(this.GetRoot() as PsUIBasePopup).CallAction("Exit");
	}

	// Token: 0x0600162E RID: 5678 RVA: 0x000E844C File Offset: 0x000E684C
	public void CancelOffensive()
	{
		this.m_offensive.Destroy();
		this.m_offensive = null;
	}

	// Token: 0x040018E8 RID: 6376
	private PsUIGenericButton m_restartButton;

	// Token: 0x040018E9 RID: 6377
	private PsUIGenericButton m_continueButton;

	// Token: 0x040018EA RID: 6378
	private PsUIGenericButton m_everyplayButton;

	// Token: 0x040018EB RID: 6379
	private PsUIGenericButton m_pvpFriendButton;

	// Token: 0x040018EC RID: 6380
	private UIRectSpriteButton m_thumbsUpButton;

	// Token: 0x040018ED RID: 6381
	private UIRectSpriteButton m_thumbsDownButton;

	// Token: 0x040018EE RID: 6382
	private UIHorizontalList m_likeArea;

	// Token: 0x040018EF RID: 6383
	private PsRating m_rating = PsRating.Neutral;

	// Token: 0x040018F0 RID: 6384
	private bool m_ratingDone;

	// Token: 0x040018F1 RID: 6385
	protected bool m_animationsDone;

	// Token: 0x040018F2 RID: 6386
	private int m_rewardScoreAmount;

	// Token: 0x040018F3 RID: 6387
	private int m_oldScore;

	// Token: 0x040018F4 RID: 6388
	protected List<UIComponent> m_flyers = new List<UIComponent>();

	// Token: 0x040018F5 RID: 6389
	protected UIFittedSprite m_leftStar;

	// Token: 0x040018F6 RID: 6390
	protected UIFittedSprite m_centerStar;

	// Token: 0x040018F7 RID: 6391
	protected UIFittedSprite m_rightStar;

	// Token: 0x040018F8 RID: 6392
	protected UIFittedSprite m_leftSlot;

	// Token: 0x040018F9 RID: 6393
	protected UIFittedSprite m_centerSlot;

	// Token: 0x040018FA RID: 6394
	protected UIFittedSprite m_rightSlot;

	// Token: 0x040018FB RID: 6395
	private UIFittedSprite m_trophy;

	// Token: 0x040018FC RID: 6396
	protected string m_topFrameName;

	// Token: 0x040018FD RID: 6397
	protected string m_bottomFrameName;

	// Token: 0x040018FE RID: 6398
	protected float m_starAnimDuration = 0.4f;

	// Token: 0x040018FF RID: 6399
	protected float m_starEffectDelay = 0.1f;

	// Token: 0x04001900 RID: 6400
	protected float m_spacing;

	// Token: 0x04001901 RID: 6401
	protected float m_centerVAlign;

	// Token: 0x04001902 RID: 6402
	private PsUIGenericButton m_offensiveButton;

	// Token: 0x04001903 RID: 6403
	private PsUIBasePopup m_offensive;
}
