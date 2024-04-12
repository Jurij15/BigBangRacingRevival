using System;
using UnityEngine;

// Token: 0x020002F9 RID: 761
public class PsUICreatorChallengeBanner : UIHorizontalList
{
	// Token: 0x0600165E RID: 5726 RVA: 0x000EA04C File Offset: 0x000E844C
	public PsUICreatorChallengeBanner(UIComponent _parent, string _tag, string _levelId, string _levelName, string _levelCreator, string _videoUrl, bool _winner = false, int _winIndex = -1, int _runnerIndex = -1)
		: base(_parent, _tag)
	{
		this.m_videoUrl = _videoUrl;
		this.m_levelId = _levelId;
		this.m_levelName = _levelName;
		this.m_levelCreator = _levelCreator;
		this.SetHeight(0.1f, RelativeTo.ScreenHeight);
		this.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.CreatorChallengeBannerDrawhandler));
		if (_winner)
		{
			UICanvas uicanvas = new UICanvas(this, false, string.Empty, null, string.Empty);
			uicanvas.SetRogue();
			uicanvas.SetSize(1.2f, 1.2f, RelativeTo.ParentHeight);
			uicanvas.SetAlign(0f, 0.5f);
			uicanvas.SetMargins(-0.5f, 0.5f, -0.575f, -0.575f, RelativeTo.OwnWidth);
			uicanvas.RemoveDrawHandler();
			UIFittedSprite uifittedSprite = new UIFittedSprite(uicanvas, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_creatorchallengewinner_badge", null), true, true);
			uifittedSprite.SetHeight(1.5f, RelativeTo.ParentHeight);
		}
		UICanvas uicanvas2 = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas2.SetSize(0.45f, 1f, RelativeTo.ParentHeight);
		uicanvas2.RemoveDrawHandler();
		UIVerticalList uiverticalList = new UIVerticalList(this, string.Empty);
		uiverticalList.SetWidth(0.35f, RelativeTo.ScreenHeight);
		uiverticalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
		uiverticalList.SetMargins(0.03f, 0.03f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		if (_winner || _runnerIndex == 0)
		{
			UICanvas uicanvas3 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
			uicanvas3.SetRogue();
			uicanvas3.SetHeight(0.0225f, RelativeTo.ScreenHeight);
			uicanvas3.SetVerticalAlign(1f);
			uicanvas3.SetHorizontalAlign(0f);
			uicanvas3.SetMargins(0f, 0f, -0.0415f, 0.0415f, RelativeTo.ScreenHeight);
			uicanvas3.RemoveDrawHandler();
			string text = PsStrings.Get(StringID.CHALLENGE_CATEGORY_BEST_ADV);
			if (_winIndex == 1)
			{
				text = PsStrings.Get(StringID.CHALLENGE_CATEGORY_BEST_RACE);
			}
			else if (_runnerIndex == 0)
			{
				text = PsStrings.Get(StringID.CHALLENGE_CATEGORY_HONORABLE);
			}
			UIFittedText uifittedText = new UIFittedText(uicanvas3, false, string.Empty, text, PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, "#000000");
			uifittedText.SetAlign(0f, 1f);
		}
		UICanvas uicanvas4 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas4.SetHeight(0.03f, RelativeTo.ScreenHeight);
		uicanvas4.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas4.RemoveDrawHandler();
		UIFittedText uifittedText2 = new UIFittedText(uicanvas4, false, string.Empty, _levelName, PsFontManager.GetFont(PsFonts.KGSecondChances), true, "#B4FF3A", "#000000");
		uifittedText2.SetHorizontalAlign(0f);
		UICanvas uicanvas5 = new UICanvas(uiverticalList, false, string.Empty, null, string.Empty);
		uicanvas5.SetHeight(0.0215f, RelativeTo.ScreenHeight);
		uicanvas5.SetWidth(1f, RelativeTo.ParentWidth);
		uicanvas5.RemoveDrawHandler();
		UIFittedText uifittedText3 = new UIFittedText(uicanvas5, false, string.Empty, PsStrings.Get(StringID.CREATOR_TEXT) + " " + _levelCreator, PsFontManager.GetFont(PsFonts.KGSecondChances), true, null, "#000000");
		uifittedText3.SetHorizontalAlign(0f);
		this.m_screenshot = new PsUICreatorChallengeBanner.PsUIScreenShotPlayButton(this, _levelId);
		this.m_screenshot.SetHeight(1f, RelativeTo.ParentHeight);
		this.m_screenshot.SetWidth(1.5f, RelativeTo.ParentHeight);
		UICanvas uicanvas6 = new UICanvas(this, false, string.Empty, null, string.Empty);
		uicanvas6.SetHeight(0.85f, RelativeTo.ParentHeight);
		uicanvas6.SetWidth(0.85f, RelativeTo.ParentHeight);
		uicanvas6.RemoveDrawHandler();
		uicanvas6.SetMargins(0.4f, -0.4f, 0f, 0f, RelativeTo.OwnHeight);
		this.m_yt = new UIRectSpriteButton(uicanvas6, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame("menu_icon_youtube", null), true, false);
		this.m_yt.SetHeight(1f, RelativeTo.ParentHeight);
	}

	// Token: 0x0600165F RID: 5727 RVA: 0x000EA424 File Offset: 0x000E8824
	public override void Step()
	{
		if (this.m_yt.m_hit)
		{
			PsMetrics.EntryVideoOpened("creatorChallenge", this.m_levelId, this.m_levelName, this.m_levelCreator);
			Application.OpenURL(this.m_videoUrl);
			TouchAreaS.CancelAllTouches(null);
		}
		else if (this.m_screenshot.m_hit)
		{
			PsMetrics.SubmittedLevelSearchOpened("creatorChallenge", this.m_levelId, this.m_levelName, this.m_levelCreator);
			PsUICenterSearch.m_sharedLevel = this.m_levelId;
			PsUITabbedCreate.m_selectedTab = 3;
			PsMainMenuState.ChangeToCreateState();
			(this.GetRoot() as PsUIBasePopup).CallAction("Continue");
			TouchAreaS.CancelAllTouches(null);
		}
		base.Step();
	}

	// Token: 0x0400191B RID: 6427
	private string m_videoUrl;

	// Token: 0x0400191C RID: 6428
	private string m_levelId;

	// Token: 0x0400191D RID: 6429
	private string m_levelName;

	// Token: 0x0400191E RID: 6430
	private string m_levelCreator;

	// Token: 0x0400191F RID: 6431
	private PsUICreatorChallengeBanner.PsUIScreenShotPlayButton m_screenshot;

	// Token: 0x04001920 RID: 6432
	private UIRectSpriteButton m_yt;

	// Token: 0x020002FA RID: 762
	private class PsUIScreenShotPlayButton : UICanvas
	{
		// Token: 0x06001660 RID: 5728 RVA: 0x000EA4D8 File Offset: 0x000E88D8
		public PsUIScreenShotPlayButton(UIComponent _parent, string _gameId)
			: base(_parent, true, string.Empty, null, string.Empty)
		{
			this.RemoveDrawHandler();
			this.m_scaleOnTouch = true;
			PsUIScreenshot psUIScreenshot = new PsUIScreenshot(this, false, string.Empty, Vector2.zero, null, false, true, 0.03f, false);
			psUIScreenshot.m_gameId = _gameId;
			psUIScreenshot.m_forceLoad = true;
			psUIScreenshot.LoadPicture();
			psUIScreenshot.SetDrawHandler(new UIDrawDelegate(PsUIScreenshot.BasicDrawHandler));
			psUIScreenshot.SetHeight(1f, RelativeTo.ParentHeight);
			psUIScreenshot.SetWidth(1f, RelativeTo.ParentWidth);
			UICanvas uicanvas = new UICanvas(psUIScreenshot, false, string.Empty, null, string.Empty);
			uicanvas.SetHeight(0.0285f, RelativeTo.ScreenHeight);
			uicanvas.SetWidth(1f, RelativeTo.ParentWidth);
			uicanvas.SetMargins(0.015f, 0.015f, 0f, 0f, RelativeTo.ScreenHeight);
			uicanvas.RemoveDrawHandler();
			UIFittedText uifittedText = new UIFittedText(uicanvas, false, string.Empty, PsStrings.Get(StringID.PLAY), PsFontManager.GetFont(PsFonts.KGSecondChances), true, "ffffff", "000000");
			uifittedText.SetMargins(0.01f, 0.01f, 0f, 0f, RelativeTo.ScreenHeight);
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x000EA5FC File Offset: 0x000E89FC
		protected override void OnTouchRollIn(TLTouch _touch, bool _secondary)
		{
			base.OnTouchRollIn(_touch, _secondary);
			if (this.m_scaleOnTouch)
			{
				if (this.m_touchScaleTween != null)
				{
					TweenS.RemoveComponent(this.m_touchScaleTween);
					this.m_touchScaleTween = null;
				}
				this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1.05f, 1.05f, 1.05f), 0.1f, 0f, false);
			}
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x000EA66C File Offset: 0x000E8A6C
		protected override void OnTouchBegan(TLTouch _touch)
		{
			base.OnTouchBegan(_touch);
			if (this.m_scaleOnTouch)
			{
				if (this.m_touchScaleTween != null)
				{
					TweenS.RemoveComponent(this.m_touchScaleTween);
					this.m_touchScaleTween = null;
				}
				this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1.05f, 1.05f, 1.05f), 0.1f, 0f, false);
			}
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x000EA6DC File Offset: 0x000E8ADC
		protected override void OnTouchRollOut(TLTouch _touch, bool _secondary)
		{
			base.OnTouchRollOut(_touch, _secondary);
			if (this.m_scaleOnTouch)
			{
				if (this.m_touchScaleTween != null)
				{
					TweenS.RemoveComponent(this.m_touchScaleTween);
					this.m_touchScaleTween = null;
				}
				this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1f, 1f, 1f), 0.1f, 0f, false);
			}
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x000EA74C File Offset: 0x000E8B4C
		protected override void OnTouchRelease(TLTouch _touch, bool _inside)
		{
			base.OnTouchRelease(_touch, _inside);
			if (this.m_scaleOnTouch)
			{
				if (this.m_touchScaleTween != null)
				{
					TweenS.RemoveComponent(this.m_touchScaleTween);
					this.m_touchScaleTween = null;
				}
				this.m_touchScaleTween = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, new Vector3(1f, 1f, 1f), 0.1f, 0f, false);
			}
		}

		// Token: 0x04001922 RID: 6434
		public bool m_scaleOnTouch;

		// Token: 0x04001923 RID: 6435
		public TweenC m_touchScaleTween;
	}
}
