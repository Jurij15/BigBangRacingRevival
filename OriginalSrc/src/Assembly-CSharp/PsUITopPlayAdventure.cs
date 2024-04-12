using System;
using UnityEngine;

// Token: 0x02000356 RID: 854
public class PsUITopPlayAdventure : PsUITopPlay, IPlayMenu
{
	// Token: 0x060018EF RID: 6383 RVA: 0x0010E120 File Offset: 0x0010C520
	public PsUITopPlayAdventure(Action _restartAction = null, Action _pauseAction = null)
		: base(_restartAction, _pauseAction)
	{
		this.m_starCount = PsState.m_activeMinigame.m_collectedStars;
		this.SetHeight(1f, RelativeTo.ScreenHeight);
		this.SetWidth(1f, RelativeTo.ScreenWidth);
		this.RemoveDrawHandler();
		this.m_leftArea = new UIHorizontalList(this, "UpperLeft");
		this.m_leftArea.SetMargins(0.01f, RelativeTo.ScreenShortest);
		this.m_leftArea.SetSpacing(0.005f, RelativeTo.ScreenShortest);
		this.m_leftArea.SetAlign(0f, 1f);
		this.m_leftArea.SetHeight(0.15f, RelativeTo.ScreenHeight);
		this.m_leftArea.RemoveDrawHandler();
		this.CreateScoreArea(this.m_leftArea);
		this.m_rightArea = new UIHorizontalList(this, "UpperLeft");
		this.m_rightArea.SetMargins(0.025f, RelativeTo.ScreenShortest);
		this.m_rightArea.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		this.m_rightArea.SetAlign(1f, 1f);
		this.m_rightArea.RemoveDrawHandler();
		this.Update();
	}

	// Token: 0x060018F0 RID: 6384 RVA: 0x0010E22C File Offset: 0x0010C62C
	public override void CreateLeftArea()
	{
	}

	// Token: 0x060018F1 RID: 6385 RVA: 0x0010E230 File Offset: 0x0010C630
	public virtual void CreateScoreArea(UIComponent _parent)
	{
		for (int i = 0; i < 3; i++)
		{
			string collactableFrame = this.GetCollactableFrame(i);
			UIFittedSprite uifittedSprite = new UIFittedSprite(_parent, false, string.Empty, PsState.m_uiSheet, PsState.m_uiSheet.m_atlas.GetFrame(collactableFrame, null), true, true);
			uifittedSprite.SetOverrideShader(Shader.Find("WOE/Unlit/ColorUnlitTransparent"));
			if (i > this.m_starCount - 1)
			{
				uifittedSprite.SetColor(DebugDraw.HexToColor("#535E6A"));
			}
		}
	}

	// Token: 0x060018F2 RID: 6386 RVA: 0x0010E2AA File Offset: 0x0010C6AA
	protected virtual string GetCollactableFrame(int _index)
	{
		return "menu_debrief_adventure_map_piece" + (_index + 1);
	}

	// Token: 0x060018F3 RID: 6387 RVA: 0x0010E2C0 File Offset: 0x0010C6C0
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

	// Token: 0x060018F4 RID: 6388 RVA: 0x0010E380 File Offset: 0x0010C780
	public void TweenMapPiece(int _index)
	{
		TweenC tweenC = TweenS.AddTransformTween(this.m_leftArea.m_childs[_index].m_TC, TweenedProperty.Scale, TweenStyle.CubicInOut, Vector3.one * 1.15f, 0.15f, 0f, true);
		TweenS.SetAdditionalTweenProperties(tweenC, 0, true, TweenStyle.CubicInOut);
		TimerS.AddComponent(this.m_leftArea.m_childs[_index].m_TC.p_entity, string.Empty, 0.15f, 0f, false, delegate(TimerC c)
		{
			TimerS.RemoveComponent(c);
			(this.m_leftArea.m_childs[_index] as UIFittedSprite).SetColor(Color.white);
		});
	}

	// Token: 0x060018F5 RID: 6389 RVA: 0x0010E42C File Offset: 0x0010C82C
	public override void CreateCoinArea()
	{
		bool flag = PsState.m_activeGameLoop.m_gameMode.ShowDiamondsInHUD();
		Camera camera = this.m_camera;
		bool flag2 = flag;
		PsMetagameManager.ShowResources(camera, false, true, flag2, false, 0.15f, false, PsState.m_activeGameLoop.m_gameMode.m_coinStreakStyle == CoinStreakStyle.BASIC || PsState.m_activeGameLoop.m_gameMode.m_coinStreakStyle == CoinStreakStyle.COPPER_AND_GOLD, false);
	}

	// Token: 0x060018F6 RID: 6390 RVA: 0x0010E48C File Offset: 0x0010C88C
	public override void Step()
	{
		if (this.m_leftArea != null && this.m_starCount != PsState.m_activeMinigame.m_collectedStars)
		{
			for (int i = this.m_starCount; i < PsState.m_activeMinigame.m_collectedStars; i++)
			{
				this.TweenMapPiece(i);
			}
			this.m_starCount = PsState.m_activeMinigame.m_collectedStars;
			this.m_leftArea.ArrangeContents();
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

	// Token: 0x060018F7 RID: 6391 RVA: 0x0010E5A7 File Offset: 0x0010C9A7
	public virtual void ApplyLeftySettings()
	{
	}

	// Token: 0x04001B82 RID: 7042
	protected PsUIGenericButton m_restartButton;

	// Token: 0x04001B83 RID: 7043
	protected PsUIGenericButton m_pauseButton;

	// Token: 0x04001B84 RID: 7044
	protected int m_starCount;

	// Token: 0x04001B85 RID: 7045
	private string m_frameName;
}
