using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200035E RID: 862
public class PsUITopPlayRacing : PsUITopPlay, IPlayMenu
{
	// Token: 0x0600191A RID: 6426 RVA: 0x0010D858 File Offset: 0x0010BC58
	public PsUITopPlayRacing(Action _restartAction = null, Action _pauseAction = null)
		: base(_restartAction, _pauseAction)
	{
		this.m_restartAction = _restartAction;
		this.m_pauseAction = _pauseAction;
		this.m_race = PsState.m_activeGameLoop.m_gameMode as PsGameModeRacing;
		int num = Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks);
		float num2 = HighScores.TicksToTime(num);
		this.SetHeight(1f, RelativeTo.ScreenHeight);
		this.SetWidth(1f, RelativeTo.ScreenWidth);
		this.SetMargins(0.025f, RelativeTo.ScreenShortest);
		this.RemoveDrawHandler();
		this.m_timerArea = new UIHorizontalList(this, "TopMiddle");
		this.m_timerArea.RemoveDrawHandler();
		this.m_timerArea.SetMargins(0.02f, 0.03f, 0f, 0.01f, RelativeTo.ScreenHeight);
		this.m_timerArea.SetAlign(0f, 1f);
		if (PsState.m_activeGameLoop is PsGameLoopRacing && (PsState.m_activeGameLoop as PsGameLoopRacing).m_practiceRun)
		{
			UICanvas uicanvas = new UICanvas(this, false, "practice", null, string.Empty);
			uicanvas.SetWidth(0.25f, RelativeTo.ScreenWidth);
			uicanvas.SetHeight(0.03f, RelativeTo.ScreenHeight);
			uicanvas.SetAlign(0.5f, 1f);
			uicanvas.SetMargins(0f, 0f, 0.13f, -0.13f, RelativeTo.ScreenHeight);
			uicanvas.RemoveDrawHandler();
			UIText uitext = new UIText(uicanvas, false, string.Empty, "PRACTICE", PsFontManager.GetFont(PsFonts.KGSecondChances), 0.03f, RelativeTo.ScreenHeight, null, "313131");
			uitext.SetShadowShift(new Vector2(0.5f, -0.1f), 0.05f);
		}
		this.m_rightArea = new UIHorizontalList(this, "UpperRight");
		this.m_rightArea.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		this.m_rightArea.SetAlign(1f, 1f);
		this.m_rightArea.RemoveDrawHandler();
		this.Update();
		this.Step();
	}

	// Token: 0x0600191B RID: 6427 RVA: 0x0010DA30 File Offset: 0x0010BE30
	public override void CreateLeftArea()
	{
		this.m_vlist = new UIVerticalList(this.m_timerArea, string.Empty);
		this.m_vlist.SetWidth(0.3f, RelativeTo.ScreenHeight);
		this.m_vlist.RemoveDrawHandler();
		this.m_vlist.SetSpacing(0.0025f, RelativeTo.ScreenHeight);
		this.m_timer = new UIText(this.m_vlist, false, string.Empty, HighScores.TicksToTimeString(Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks)), PsFontManager.GetFont(PsFonts.KGSecondChancesMN), 0.075f, RelativeTo.ScreenHeight, "#FFFFFF", "#284E65");
		this.m_timer.SetShadowShift(new Vector2(0f, -1f), 0.05f);
		PsGameLoopRacing psGameLoopRacing = PsState.m_activeGameLoop as PsGameLoopRacing;
		if (PsState.m_activeGameLoop is PsGameLoopRacing)
		{
			GhostData ghostData = new GhostData();
			for (int i = 0; i < (PsState.m_activeGameLoop.m_gameMode as PsGameModeRacing).m_allGhosts.Count; i++)
			{
				if ((PsState.m_activeGameLoop.m_gameMode as PsGameModeRacing).m_allGhosts[i].rival)
				{
					ghostData = (PsState.m_activeGameLoop.m_gameMode as PsGameModeRacing).m_allGhosts[i];
				}
			}
			if (PsState.m_activeGameLoop is PsGameLoopRacing && ghostData != null && (PsState.m_activeGameLoop as PsGameLoopRacing).m_secondarysWon < 2)
			{
				UIHorizontalList uihorizontalList = new UIHorizontalList(this.m_vlist, string.Empty);
				uihorizontalList.SetHeight(0.035f, RelativeTo.ScreenHeight);
				uihorizontalList.RemoveDrawHandler();
				uihorizontalList.SetSpacing(0.01f, RelativeTo.ScreenHeight);
				string text = "menu_chest_badge_active";
				if (psGameLoopRacing.m_heatNumber > 5 && psGameLoopRacing.m_secondarysWon < 2)
				{
					if (psGameLoopRacing.m_heatNumber == 6 && psGameLoopRacing.m_paused)
					{
						text = "menu_chest_badge_active";
					}
					else
					{
						text = "menu_chest_badge_active";
					}
				}
				if (psGameLoopRacing.m_nodeId != psGameLoopRacing.m_path.m_currentNodeId)
				{
					text = "menu_chest_badge_active";
				}
				UIFittedSprite uifittedSprite = new UIFittedSprite(uihorizontalList, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text, null), true, true);
				uifittedSprite.SetSize(0.065f, 0.065f, RelativeTo.ScreenHeight);
				uifittedSprite.SetVerticalAlign(0.8f);
				TransformS.SetRotation(uifittedSprite.m_TC, new Vector3(0f, 0f, 1.5f));
				UIText uitext = new UIText(uihorizontalList, false, string.Empty, HighScores.TimeScoreToTimeString(ghostData.time), PsFontManager.GetFont(PsFonts.KGSecondChancesMN), 0.035f, RelativeTo.ScreenHeight, "#FFE143", "#284E65");
				uitext.SetShadowShift(new Vector2(0f, -1f), 0.05f);
			}
		}
	}

	// Token: 0x0600191C RID: 6428 RVA: 0x0010DCD8 File Offset: 0x0010C0D8
	public override void CreateRestartArea(UIComponent _parent)
	{
		this.m_pauseButton = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.005f, "Button");
		this.m_pauseButton.SetSound("/UI/PauseGame");
		this.m_pauseButton.SetIcon("hud_icon_pause", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_pauseButton.SetOrangeColors(true);
		this.m_restartButton = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.005f, "Button");
		this.m_restartButton.SetIcon("hud_icon_restart", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_restartButton.SetOrangeColors(true);
	}

	// Token: 0x0600191D RID: 6429 RVA: 0x0010DD98 File Offset: 0x0010C198
	public override void CreateCoinArea()
	{
		PsMetagameManager.ShowResources(this.m_camera, false, true, false, false, 0.15f, false, true, false);
	}

	// Token: 0x0600191E RID: 6430 RVA: 0x0010DDBC File Offset: 0x0010C1BC
	public void CreateGoText()
	{
		string text = "hud_countdown_go";
		this.m_go = new UIFittedSprite(this, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(text, null), true, true);
		this.m_go.SetHeight(0.2f, RelativeTo.ScreenHeight);
		this.m_go.SetAlign(0.5f, 0.75f);
		this.m_attentionTween = TweenS.AddTransformTween(this.m_go.m_TC, TweenedProperty.Scale, TweenStyle.QuadInOut, Vector3.one, Vector3.one * 1.085f, 1f, 0f, false);
		TweenS.SetAdditionalTweenProperties(this.m_attentionTween, -1, true, TweenStyle.QuadInOut);
		this.Update();
	}

	// Token: 0x0600191F RID: 6431 RVA: 0x0010DE6C File Offset: 0x0010C26C
	public void CreateGoTween()
	{
		this.CreateRestartArea(this.m_rightArea);
		this.CreateCoinArea();
		this.CreateLeftArea();
		this.Update();
		if (this.m_go != null)
		{
			Vector3 vector = Vector3.one;
			if (this.m_attentionTween != null)
			{
				vector = this.m_attentionTween.currentValue;
				TweenS.RemoveComponent(this.m_attentionTween);
				this.m_attentionTween = null;
			}
			TweenS.AddTransformTween(this.m_go.m_TC, TweenedProperty.Scale, TweenStyle.CubicIn, vector, Vector3.one * 1.5f, 0.5f, 0f, true);
			TweenC tweenC = TweenS.AddTransformTween(this.m_go.m_TC, TweenedProperty.Alpha, TweenStyle.CubicIn, Vector3.one, Vector3.zero, 0.5f, 0f, true);
			TweenS.SetTweenAlphaProperties(tweenC, false, true, false, Shader.Find("WOE/Unlit/ColorUnlitTransparent"));
		}
	}

	// Token: 0x06001920 RID: 6432 RVA: 0x0010DF3C File Offset: 0x0010C33C
	public override void Step()
	{
		if (Main.m_gameTicks % 2 == 1)
		{
			int num = Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks);
			float num2 = HighScores.TicksToTime(num);
			if (this.m_timer != null)
			{
				this.m_timer.SetText(HighScores.TicksToTimeString(num));
			}
		}
		if (this.m_restartButton != null && this.m_restartButton.m_hit)
		{
			if (this.m_restartAction != null)
			{
				PsIngameMenu.CloseAll();
				this.m_restartAction.Invoke();
			}
		}
		else if (this.m_pauseButton != null && this.m_pauseButton.m_TC.p_entity != null && this.m_pauseButton.m_TC.p_entity.m_active && (this.m_pauseButton.m_hit || Main.AndroidBackButtonPressed(null)) && this.m_pauseAction != null)
		{
			this.m_pauseAction.Invoke();
		}
		base.Step();
	}

	// Token: 0x06001921 RID: 6433 RVA: 0x0010E035 File Offset: 0x0010C435
	public override void Destroy()
	{
		base.Destroy();
	}

	// Token: 0x06001922 RID: 6434 RVA: 0x0010E03D File Offset: 0x0010C43D
	public void ApplyLeftySettings()
	{
	}

	// Token: 0x04001BA0 RID: 7072
	protected PsUIGenericButton m_restartButton;

	// Token: 0x04001BA1 RID: 7073
	protected PsUIGenericButton m_pauseButton;

	// Token: 0x04001BA2 RID: 7074
	private new Action m_restartAction;

	// Token: 0x04001BA3 RID: 7075
	private new Action m_pauseAction;

	// Token: 0x04001BA4 RID: 7076
	private UIHorizontalList m_timerArea;

	// Token: 0x04001BA5 RID: 7077
	private int m_starCount;

	// Token: 0x04001BA6 RID: 7078
	private UIFittedSprite m_stars;

	// Token: 0x04001BA7 RID: 7079
	private UIText m_timer;

	// Token: 0x04001BA8 RID: 7080
	private UIText m_popTimer;

	// Token: 0x04001BA9 RID: 7081
	private PsGameModeRacing m_race;

	// Token: 0x04001BAA RID: 7082
	private string m_frameName;

	// Token: 0x04001BAB RID: 7083
	private List<UIStarInflate> m_starList;

	// Token: 0x04001BAC RID: 7084
	private float[] m_medalTimes;

	// Token: 0x04001BAD RID: 7085
	private UIFittedSprite m_go;

	// Token: 0x04001BAE RID: 7086
	protected UIVerticalList m_vlist;

	// Token: 0x04001BAF RID: 7087
	private TweenC m_attentionTween;
}
