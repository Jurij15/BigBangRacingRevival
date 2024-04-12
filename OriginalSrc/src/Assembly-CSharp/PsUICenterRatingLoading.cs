using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003A9 RID: 937
public class PsUICenterRatingLoading : UICanvas
{
	// Token: 0x06001ACB RID: 6859 RVA: 0x0012A680 File Offset: 0x00128A80
	public PsUICenterRatingLoading(UIComponent _parent)
		: base(_parent, false, "CenterRatingLoading", null, string.Empty)
	{
		this.m_ratingPressed = false;
		this.m_sceneChanged = false;
		this.m_scene = Main.m_currentGame.m_sceneManager.m_loadingScene as PsRatingLoadingScene;
		CameraS.CreateBlur(CameraS.m_mainCamera, null);
		this.SetWidth(1f, RelativeTo.ScreenWidth);
		this.SetHeight(1f, RelativeTo.ScreenHeight);
		this.RemoveDrawHandler();
		this.m_creatorList = new UIVerticalList(this, "LoadingList");
		this.m_creatorList.SetAlign(0.5f, 1f);
		this.m_creatorList.SetMargins(0f, 0f, 0.05f, 0.3f, RelativeTo.ScreenHeight);
		this.m_creatorList.SetSpacing(0.025f, RelativeTo.ScreenHeight);
		this.m_creatorList.RemoveDrawHandler();
		this.CreateCreatorInfo(this.m_creatorList);
		if (PsState.m_activeGameLoop.m_minigameMetaData.rating == PsRating.Unrated && PsState.m_activeGameLoop.m_path != null && PsState.m_activeGameLoop.m_nodeId + 1 == PsState.m_activeGameLoop.m_path.m_currentNodeId)
		{
			new PsMinigameDialogueFlow(PsState.m_activeGameLoop, PsNodeEventTrigger.Rating, 0f, delegate(string s)
			{
				this.DialogueCallback(s, true);
			}, delegate(string s)
			{
				this.DialogueCallback(s, false);
			});
		}
		else
		{
			this.CreateRatingArea(false);
		}
		this.m_loadingText = new UIText(this, false, string.Empty, PsStrings.Get(StringID.LOADING), PsFontManager.GetFont(PsFonts.HurmeBold), 0.065f, RelativeTo.ScreenHeight, "#ffffff", null);
		this.m_loadingText.SetAlign(1f, 0f);
		EntityManager.SetActivityOfEntity(this.m_loadingText.m_TC.p_entity, false, true, true, true, true, true);
		this.m_topBanner = new UICanvas(null, false, "BANNER", null, string.Empty);
		this.m_topBanner.SetCamera(this.m_camera, false, true);
		this.m_topBanner.SetHeight(0.06f, RelativeTo.ScreenHeight);
		this.m_topBanner.SetWidth(1f, RelativeTo.ScreenWidth);
		this.m_topBanner.SetAlign(0.5f, 1f);
		this.m_topBanner.SetDrawHandler(new UIDrawDelegate(this.BannerDrawhandler));
		this.m_topBanner.SetDepthOffset(10f);
		this.m_topBanner.Update();
		this.m_bottomBanner = new UICanvas(null, false, "BANNER", null, string.Empty);
		this.m_bottomBanner.SetCamera(this.m_camera, false, true);
		this.m_bottomBanner.SetHeight(0.06f, RelativeTo.ScreenHeight);
		this.m_bottomBanner.SetWidth(1f, RelativeTo.ScreenWidth);
		this.m_bottomBanner.SetAlign(0.5f, 0f);
		this.m_bottomBanner.SetDrawHandler(new UIDrawDelegate(this.BannerDrawhandler));
		this.m_bottomBanner.SetDepthOffset(10f);
		this.m_bottomBanner.Update();
	}

	// Token: 0x06001ACC RID: 6860 RVA: 0x0012A95C File Offset: 0x00128D5C
	private void DialogueCallback(string _dialogue, bool _ok)
	{
		this.CreateRatingArea(!string.IsNullOrEmpty(_dialogue));
		if (!(_dialogue == "youtube_ad_01") || _ok)
		{
		}
	}

	// Token: 0x06001ACD RID: 6861 RVA: 0x0012A984 File Offset: 0x00128D84
	private void CreateRatingArea(bool _update = false)
	{
		this.m_ratingList = new UIVerticalList(this, "RatingArea");
		this.m_ratingList.SetVerticalAlign(0f);
		this.m_ratingList.SetHorizontalAlign(0.5f);
		this.m_ratingList.SetMargins(0f, 0f, 0f, 0.1f, RelativeTo.ScreenHeight);
		this.m_ratingList.SetSpacing(0.03f, RelativeTo.ScreenHeight);
		this.m_ratingList.RemoveDrawHandler();
		this.CreateRatingBar(this.m_ratingList, PsState.m_activeGameLoop.GetVisualLikes(), PsState.m_activeGameLoop.GetVisualDislikes());
		this.CreateRatingButtonList(this.m_ratingList);
		if (_update)
		{
			this.m_ratingList.Update();
		}
	}

	// Token: 0x06001ACE RID: 6862 RVA: 0x0012AA3B File Offset: 0x00128E3B
	protected virtual void CreateRatingBar(UIComponent _parent, int _positive, int _negative)
	{
		this.m_ratingBar = new PsUIRatingBar(_parent, _positive, _negative);
		this.m_ratingBar.SetWidth(0.3f, RelativeTo.ScreenWidth);
		this.m_ratingBar.SetHeight(0.0675f, RelativeTo.ScreenHeight);
	}

	// Token: 0x06001ACF RID: 6863 RVA: 0x0012AA70 File Offset: 0x00128E70
	protected virtual void CreateCreatorInfo(UIComponent _parent)
	{
		bool flag = false;
		if (PsState.m_activeGameLoop.m_dialogues != null && PsState.m_activeGameLoop.m_dialogues.ContainsKey(PsNodeEventTrigger.Rating.ToString()))
		{
			string identifier = PsMetagameData.GetDialogueByIdentifier((string)PsState.m_activeGameLoop.m_dialogues[PsNodeEventTrigger.Rating.ToString()]).m_identifier;
			flag = identifier == "youtube_ad_01";
		}
		this.m_creator = new PsUICreatorInfo(this.m_creatorList, true, true, true, true, true, flag);
		this.m_creator.SetHorizontalAlign(0.5f);
	}

	// Token: 0x06001AD0 RID: 6864 RVA: 0x0012AB14 File Offset: 0x00128F14
	public virtual void CreateRatingButtonList(UIComponent _parent)
	{
		UIHorizontalList uihorizontalList = new UIHorizontalList(_parent, "RatingArea");
		uihorizontalList.SetVerticalAlign(0f);
		uihorizontalList.SetHorizontalAlign(0.5f);
		uihorizontalList.SetDepthOffset(-10f);
		uihorizontalList.SetSpacing(0.05f, RelativeTo.ScreenHeight);
		uihorizontalList.RemoveDrawHandler();
		this.CreateRatingButtons(uihorizontalList);
	}

	// Token: 0x06001AD1 RID: 6865 RVA: 0x0012AB68 File Offset: 0x00128F68
	public virtual void CreateRatingButtons(UIComponent _parent)
	{
		float num = 0.15f;
		float num2 = 0.11f;
		this.m_superLike = new PsUISuperLikeButton(_parent, num, num2);
		this.m_up = new PsUIRatingLikeButton(_parent, num, num2, "menu_thumbs_up_off");
		this.m_down = new PsUIRatingDislikeButton(_parent, num, num2, "menu_thumbs_down_off");
		UICanvas uicanvas = new UICanvas(_parent, false, string.Empty, null, string.Empty);
		uicanvas.SetSize(num + 0.0225f, num2, RelativeTo.ScreenHeight);
		uicanvas.RemoveDrawHandler();
		UICanvas uicanvas2 = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas2.SetWidth(0.1f, RelativeTo.ScreenHeight);
		uicanvas2.SetHeight(0.1f, RelativeTo.ScreenHeight);
		uicanvas2.SetAlign(1f, 1f);
		uicanvas2.SetMargins(0f, 0.05f, 0.02f, 0f, RelativeTo.ScreenHeight);
		uicanvas2.RemoveDrawHandler();
		uicanvas2.SetDepthOffset(-10f);
		this.m_offensiveButton = new PsUIRatingAbuseButton(uicanvas2, num, num2, null);
		this.m_offensiveButton.SetAlign(1f, 1f);
		this.m_ratingButtonList = new List<PsUIRatingButton>();
		this.m_ratingButtonList.Add(this.m_superLike);
		this.m_ratingButtonList.Add(this.m_up);
		this.m_ratingButtonList.Add(this.m_down);
		this.m_ratingButtonList.Add(this.m_offensiveButton);
	}

	// Token: 0x06001AD2 RID: 6866 RVA: 0x0012ACB7 File Offset: 0x001290B7
	public override void Destroy()
	{
		this.m_topBanner.Destroy();
		this.m_bottomBanner.Destroy();
		this.m_topBanner = null;
		this.m_bottomBanner = null;
		base.Destroy();
	}

	// Token: 0x06001AD3 RID: 6867 RVA: 0x0012ACE4 File Offset: 0x001290E4
	public override void Step()
	{
		if (!this.m_animationDone)
		{
			this.m_animationDone = true;
		}
		if (this.m_scene.m_outroStarted && !this.m_sceneChanged)
		{
			this.m_sceneChanged = true;
			TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.CubicOut, this.m_TC.transform.localScale, Vector3.zero, 0.2f, 0f, true);
			TweenS.AddTransformTween(this.m_topBanner.m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, this.m_topBanner.m_TC.transform.localPosition, this.m_topBanner.m_TC.transform.localPosition + Vector3.up * (float)Screen.height * 0.12f, 0.2f, 0f, true);
			TweenC tweenC = TweenS.AddTransformTween(this.m_bottomBanner.m_TC, TweenedProperty.Position, TweenStyle.CubicInOut, this.m_bottomBanner.m_TC.transform.localPosition, this.m_bottomBanner.m_TC.transform.localPosition + Vector3.down * (float)Screen.height * 0.12f, 0.2f, 0f, true);
			TweenS.AddTweenEndEventListener(tweenC, delegate(TweenC c)
			{
				(this.GetRoot() as PsUIBasePopup).CallAction("Continue");
			});
		}
		this.ButtonStep();
		base.Step();
	}

	// Token: 0x06001AD4 RID: 6868 RVA: 0x0012AE40 File Offset: 0x00129240
	protected virtual void ButtonStep()
	{
		if (this.m_donateButton != null && this.m_donateButton.m_hit)
		{
			PsUIBasePopup popup2 = new PsUIBasePopup(typeof(PsUICenterDonate), null, null, null, true, true, InitialPage.Center, false, false, false);
			popup2.SetAction("Exit", delegate
			{
				popup2.Destroy();
			});
		}
		if (this.m_superLike != null && this.m_superLike.m_hit && !this.m_ratingPressed && !this.m_offensiveOpened)
		{
			this.RatingBarEffect(new Action(this.SuperLikePressed));
			this.ButtonRating(PsRating.SuperLike, 10, 1, 0, this.m_superLike, false);
			PsState.m_activeGameLoop.m_minigameMetaData.timesSuperLiked++;
		}
		else if (this.m_up != null && this.m_up.m_hit && !this.m_ratingPressed && !this.m_offensiveOpened)
		{
			this.RatingBarEffect(new Action(this.ThumbUpPressed));
			this.ButtonRating(PsRating.Positive, 1, 1, 1, this.m_up, false);
		}
		else if (this.m_down != null && this.m_down.m_hit && !this.m_ratingPressed && !this.m_offensiveOpened)
		{
			this.RatingBarEffect(new Action(this.ThumbDownPressed));
			this.ButtonRating(PsRating.Negative, -1, 1, 0, this.m_down, false);
		}
		else if (this.m_offensiveButton != null && this.m_offensiveButton.m_hit && !this.m_ratingPressed && !this.m_offensiveOpened)
		{
			this.m_offensiveOpened = true;
			PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUICenterOffensive), null, null, null, false, true, InitialPage.Center, false, false, false);
			popup.SetAction("Continue", delegate
			{
				popup.Destroy();
				this.RatingBarEffect(new Action(this.ThumbDownPressed));
				this.ButtonRating(PsRating.Abuse, -1, 1, 0, this.m_offensiveButton, false);
			});
			popup.SetAction("Exit", delegate
			{
				popup.Destroy();
				this.m_offensiveOpened = false;
			});
			TweenS.AddTransformTween(popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
		}
	}

	// Token: 0x06001AD5 RID: 6869 RVA: 0x0012B0A3 File Offset: 0x001294A3
	protected void SuperLikePressed()
	{
		this.m_ratingBar.SuperLike();
	}

	// Token: 0x06001AD6 RID: 6870 RVA: 0x0012B0B0 File Offset: 0x001294B0
	protected void ThumbUpPressed()
	{
		this.m_ratingBar.ThumbsUp();
	}

	// Token: 0x06001AD7 RID: 6871 RVA: 0x0012B0BD File Offset: 0x001294BD
	protected void ThumbDownPressed()
	{
		this.m_ratingBar.ThumbsDown();
	}

	// Token: 0x06001AD8 RID: 6872 RVA: 0x0012B0CA File Offset: 0x001294CA
	protected virtual void RatingBarEffect(Action _postAction)
	{
		_postAction.Invoke();
	}

	// Token: 0x06001AD9 RID: 6873 RVA: 0x0012B0D4 File Offset: 0x001294D4
	protected virtual void ButtonRating(PsRating _rating, int _sound, int _timesRated, int _timesLiked, PsUIRatingButton _buttonPressed, bool skipped = false)
	{
		TouchAreaS.CancelAllTouches(null);
		TouchAreaS.Disable();
		PsState.m_activeGameLoop.m_minigameMetaData.timesRated += _timesRated;
		PsState.m_activeGameLoop.m_minigameMetaData.timesLiked += _timesLiked;
		this.m_ratingPressed = true;
		_buttonPressed.ButtonPressed();
		this.RemoveRatingButtons(_buttonPressed);
		this.SendRating(_rating, _sound, skipped);
	}

	// Token: 0x06001ADA RID: 6874 RVA: 0x0012B13C File Offset: 0x0012953C
	protected void RemoveRatingButtons(PsUIRatingButton _exception)
	{
		for (int i = 0; i < this.m_ratingButtonList.Count; i++)
		{
			this.m_ratingButtonList[i].DisableTouchAreas(true);
		}
		this.m_ratingButtonList.Remove(_exception);
		for (int j = 0; j < this.m_ratingButtonList.Count; j++)
		{
			TweenC tweenC = TweenS.AddTransformTween(this.m_ratingButtonList[j].m_TC, TweenedProperty.Scale, TweenStyle.CubicOut, Vector3.zero, 0.5f, 0f, true);
			if (j == 0)
			{
				this.m_thumbsZoomOutTween = tweenC;
			}
		}
	}

	// Token: 0x06001ADB RID: 6875 RVA: 0x0012B1D8 File Offset: 0x001295D8
	public bool WasFirstLevel()
	{
		return !PsMetagameManager.IsVehicleUnlocked(typeof(Motorcycle)) && PsState.m_activeGameLoop.m_path != null && PsState.m_activeGameLoop.m_path.m_planet == "AdventureOffroadCar" && PsState.m_activeGameLoop.m_path.m_currentNodeId <= 2;
	}

	// Token: 0x06001ADC RID: 6876 RVA: 0x0012B240 File Offset: 0x00129640
	protected virtual void SendRating(PsRating _rating, int _sound = 1, bool _skipped = false)
	{
		if (this.WasFirstLevel())
		{
			PsMetrics.FirstLevelRated();
		}
		PsMetagameManager.SendRating(_rating, PsState.m_activeGameLoop, PsState.m_activeMinigame, _skipped);
		PsState.m_activeGameLoop.m_minigameMetaData.rating = _rating;
		if (_sound < 3)
		{
			SoundS.PlaySingleShotWithParameter("/UI/Thumbs", Vector2.zero, "Rating", (float)_sound, 1f);
			Debug.Log("Played rating sound: " + _sound, null);
		}
		else
		{
			SoundS.PlaySingleShot("/UI/MegaLike", Vector3.zero, 1f);
			Debug.Log("Played megalike sound", null);
		}
		this.Proceed();
	}

	// Token: 0x06001ADD RID: 6877 RVA: 0x0012B2E8 File Offset: 0x001296E8
	protected virtual void Proceed()
	{
		if (this.m_creator != null)
		{
			this.m_creator.SendFollowData();
		}
		EntityManager.SetActivityOfEntity(this.m_loadingText.m_TC.p_entity, true, true, true, true, true, true);
		new PsServerQueueFlow(delegate
		{
			if (PsUIHiddenHatPopup.m_popupCount <= 0)
			{
				this.StartExit();
			}
			else
			{
				PsUIHiddenHatPopup.m_closeLastPopupCallback = new Action(this.StartExit);
			}
		}, null, new string[0]);
	}

	// Token: 0x06001ADE RID: 6878 RVA: 0x0012B340 File Offset: 0x00129740
	private void StartExit()
	{
		if (this.m_proceedTimer != null && !this.m_proceedTimer.isDone)
		{
			this.m_proceedTimer.timeoutHandler = delegate(TimerC c)
			{
				this.CallExit();
			};
		}
		else if (this.m_thumbsZoomOutTween != null && !this.m_thumbsZoomOutTween.hasFinished)
		{
			TweenS.AddTweenEndEventListener(this.m_thumbsZoomOutTween, delegate(TweenC c)
			{
				this.CallExit();
			});
		}
		else
		{
			this.CallExit();
		}
	}

	// Token: 0x06001ADF RID: 6879 RVA: 0x0012B3C1 File Offset: 0x001297C1
	private void CallExit()
	{
		(this.GetRoot() as PsUIBasePopup).CallAction("Rating");
	}

	// Token: 0x06001AE0 RID: 6880 RVA: 0x0012B3DC File Offset: 0x001297DC
	public void BannerDrawhandler(UIComponent _c)
	{
		PrefabS.RemoveComponentsByEntity(_c.m_TC.p_entity, true);
		Vector2[] rect = DebugDraw.GetRect(_c.m_actualWidth, _c.m_actualHeight, Vector2.zero, true);
		PrefabS.CreatePathPrefabComponentFromVectorArray(_c.m_TC, Vector3.forward * -2f, rect, (float)Screen.height * 0.015f, Color.black, Color.black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.Line4Mat_Material), _c.m_camera, Position.Center, true);
		GGData ggdata = new GGData(rect);
		PrefabS.CreateFlatPrefabComponentsFromPolygon(_c.m_TC, Vector3.zero, ggdata, Color.black, Color.black, ResourceManager.GetMaterial(RESOURCE_FRAMEWORK.SolidMat_Material), _c.m_camera);
	}

	// Token: 0x04001D3B RID: 7483
	private PsRatingLoadingScene m_scene;

	// Token: 0x04001D3C RID: 7484
	protected UIText m_loadingText;

	// Token: 0x04001D3D RID: 7485
	private UIVerticalList m_creatorList;

	// Token: 0x04001D3E RID: 7486
	protected PsUICreatorInfo m_creator;

	// Token: 0x04001D3F RID: 7487
	protected UIVerticalList m_ratingList;

	// Token: 0x04001D40 RID: 7488
	protected PsUIGenericButton m_continuebutton;

	// Token: 0x04001D41 RID: 7489
	protected PsUIRatingButton m_up;

	// Token: 0x04001D42 RID: 7490
	protected PsUIRatingButton m_down;

	// Token: 0x04001D43 RID: 7491
	protected PsUIRatingButton m_superLike;

	// Token: 0x04001D44 RID: 7492
	protected PsUIRatingButton m_upDouble;

	// Token: 0x04001D45 RID: 7493
	protected PsUIRatingButton m_downDouble;

	// Token: 0x04001D46 RID: 7494
	protected PsUIRatingButton m_offensiveButton;

	// Token: 0x04001D47 RID: 7495
	protected List<PsUIRatingButton> m_ratingButtonList;

	// Token: 0x04001D48 RID: 7496
	private PsUIGenericButton m_donateButton;

	// Token: 0x04001D49 RID: 7497
	protected bool m_ratingPressed;

	// Token: 0x04001D4A RID: 7498
	private bool m_sceneChanged;

	// Token: 0x04001D4B RID: 7499
	private UICanvas m_positioner;

	// Token: 0x04001D4C RID: 7500
	private bool m_animationDone;

	// Token: 0x04001D4D RID: 7501
	protected UICanvas m_topBanner;

	// Token: 0x04001D4E RID: 7502
	protected UICanvas m_bottomBanner;

	// Token: 0x04001D4F RID: 7503
	protected PsUIRatingBar m_ratingBar;

	// Token: 0x04001D50 RID: 7504
	private TweenC m_thumbsZoomOutTween;

	// Token: 0x04001D51 RID: 7505
	protected TimerC m_proceedTimer;

	// Token: 0x04001D52 RID: 7506
	protected bool m_offensiveOpened;
}
