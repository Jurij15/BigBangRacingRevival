using System;
using UnityEngine;

// Token: 0x0200035D RID: 861
public class PsUITopPlayRaceSimple : UICanvas, IPlayMenu
{
	// Token: 0x06001914 RID: 6420 RVA: 0x001106A4 File Offset: 0x0010EAA4
	public PsUITopPlayRaceSimple(Action _restartAction = null, Action _pauseAction = null)
		: base(null, false, "TopContent", null, string.Empty)
	{
		this.m_restartAction = _restartAction;
		this.m_pauseAction = _pauseAction;
		int num = Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks);
		float num2 = HighScores.TicksToTime(num);
		this.RemoveDrawHandler();
		this.m_middleArea = new UIHorizontalList(this, "TopMiddle");
		this.m_middleArea.SetDrawHandler(new UIDrawDelegate(PsUIDrawHandlers.RaceTimerBackground));
		this.m_middleArea.SetMargins(0.02f, 0.03f, 0.015f, 0.01f, RelativeTo.ScreenHeight);
		this.m_middleArea.SetAlign(0.5f, 1f);
		UIVerticalList uiverticalList = new UIVerticalList(this.m_middleArea, string.Empty);
		uiverticalList.SetWidth(0.3f, RelativeTo.ScreenHeight);
		uiverticalList.RemoveDrawHandler();
		uiverticalList.SetSpacing(0.0025f, RelativeTo.ScreenHeight);
		this.m_timer = new UIText(uiverticalList, false, string.Empty, HighScores.TicksToTimeString(Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks)), PsFontManager.GetFont(PsFonts.HurmeSemiBoldMN), 0.05f, RelativeTo.ScreenHeight, null, null);
		this.CreateCoinArea();
		UIHorizontalList uihorizontalList = new UIHorizontalList(this, "UpperLeft");
		uihorizontalList.SetMargins(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetSpacing(0.025f, RelativeTo.ScreenShortest);
		uihorizontalList.SetAlign(1f, 1f);
		uihorizontalList.RemoveDrawHandler();
		this.CreateRestartArea(uihorizontalList);
		this.Update();
		this.Step();
	}

	// Token: 0x06001915 RID: 6421 RVA: 0x00110814 File Offset: 0x0010EC14
	public virtual void CreateRestartArea(UIComponent _parent)
	{
		this.m_restartButton = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.005f, "Button");
		this.m_restartButton.SetIcon("hud_icon_restart", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_restartButton.SetOrangeColors(true);
		this.m_pauseButton = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.005f, "Button");
		this.m_pauseButton.SetIcon("hud_icon_pause", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_pauseButton.SetOrangeColors(true);
	}

	// Token: 0x06001916 RID: 6422 RVA: 0x001108C4 File Offset: 0x0010ECC4
	public virtual void CreateCoinArea()
	{
		PsMetagameManager.ShowResources(this.m_camera, false, true, false, false, 0.15f, false, false, false);
	}

	// Token: 0x06001917 RID: 6423 RVA: 0x001108E8 File Offset: 0x0010ECE8
	public override void Step()
	{
		int num = Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks);
		float num2 = HighScores.TicksToTime(num);
		this.m_timer.SetText(HighScores.TicksToTimeString(num));
		if (this.m_restartButton.m_hit)
		{
			if (this.m_restartAction != null)
			{
				this.m_restartAction.Invoke();
			}
		}
		else if (this.m_pauseButton != null && this.m_pauseButton.m_TC.p_entity != null && this.m_pauseButton.m_TC.p_entity.m_active && (this.m_pauseButton.m_hit || Main.AndroidBackButtonPressed(null)) && this.m_pauseAction != null)
		{
			this.m_pauseAction.Invoke();
		}
		base.Step();
	}

	// Token: 0x06001918 RID: 6424 RVA: 0x001109B9 File Offset: 0x0010EDB9
	public override void Destroy()
	{
		base.Destroy();
	}

	// Token: 0x06001919 RID: 6425 RVA: 0x001109C1 File Offset: 0x0010EDC1
	public void ApplyLeftySettings()
	{
	}

	// Token: 0x04001B98 RID: 7064
	protected PsUIGenericButton m_restartButton;

	// Token: 0x04001B99 RID: 7065
	protected PsUIGenericButton m_pauseButton;

	// Token: 0x04001B9A RID: 7066
	private Action m_restartAction;

	// Token: 0x04001B9B RID: 7067
	private Action m_pauseAction;

	// Token: 0x04001B9C RID: 7068
	private UIVerticalList m_leftArea;

	// Token: 0x04001B9D RID: 7069
	private UIHorizontalList m_middleArea;

	// Token: 0x04001B9E RID: 7070
	protected UIText m_timer;
}
