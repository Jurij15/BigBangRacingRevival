using System;
using UnityEngine;

// Token: 0x0200035C RID: 860
public class PsUITopPlayRaceDiamond : PsUITopPlayRace
{
	// Token: 0x0600190B RID: 6411 RVA: 0x001103D4 File Offset: 0x0010E7D4
	public PsUITopPlayRaceDiamond(Action _restartAction = null, Action _pauseAction = null)
		: base(_restartAction, _pauseAction)
	{
	}

	// Token: 0x0600190C RID: 6412 RVA: 0x001103DE File Offset: 0x0010E7DE
	public override void CreateCoinArea()
	{
	}

	// Token: 0x0600190D RID: 6413 RVA: 0x001103E0 File Offset: 0x0010E7E0
	public override void CreateRestartArea(UIComponent _parent)
	{
		this.m_restartButton = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.005f, "Button");
		this.m_restartButton.SetIcon("hud_icon_restart", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_restartButton.SetOrangeColors(true);
		this.m_pauseButton = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.005f, "Button");
		this.m_pauseButton.SetIcon("hud_icon_pause", 0.06f, RelativeTo.ScreenShortest, "#FFFFFF", default(cpBB));
		this.m_pauseButton.SetOrangeColors(true);
	}

	// Token: 0x0600190E RID: 6414 RVA: 0x00110490 File Offset: 0x0010E890
	public override string GetStarFrame()
	{
		int num = Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks);
		float num2 = HighScores.TicksToTime(num);
		if (num2 < this.m_medalTimes[2])
		{
			return "menu_mode_diamond_3";
		}
		if (num2 < this.m_medalTimes[1])
		{
			return "menu_mode_diamond_2";
		}
		if (num2 < this.m_medalTimes[0])
		{
			return "menu_mode_diamond_1";
		}
		return "menu_mode_diamond_0";
	}

	// Token: 0x0600190F RID: 6415 RVA: 0x001104F5 File Offset: 0x0010E8F5
	public override void CreateExplodingStars()
	{
	}

	// Token: 0x06001910 RID: 6416 RVA: 0x001104F8 File Offset: 0x0010E8F8
	public override void UpdateTimer()
	{
		int num = Mathf.RoundToInt(PsState.m_activeMinigame.m_gameTicks);
		float num2 = HighScores.TicksToTime(num);
		Frame frame = PsState.m_uiSheet.m_atlas.GetFrame(this.GetStarFrame(), null);
		if (frame != this.m_stars.m_frame)
		{
			this.m_stars.m_frame = frame;
			this.m_stars.Update();
		}
		this.m_timer.SetText(HighScores.TicksToTimeString(num));
		this.UpdatePopTimer(num2);
	}

	// Token: 0x06001911 RID: 6417 RVA: 0x00110574 File Offset: 0x0010E974
	private void UpdatePopTimer(float _time)
	{
		float num = 5f;
		bool flag = false;
		for (int i = this.m_medalTimes.Length - 1; i >= 0; i--)
		{
			if (_time < this.m_medalTimes[i] && _time > this.m_medalTimes[i] - num)
			{
				this.SetPoptimer(this.m_medalTimes[i] - _time, i);
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			this.DestroyPoptimer();
		}
	}

	// Token: 0x06001912 RID: 6418 RVA: 0x001105E6 File Offset: 0x0010E9E6
	private void DestroyPoptimer()
	{
		if (this.m_popTimer != null)
		{
			this.m_popTimer.Destroy();
			this.m_popTimer = null;
		}
	}

	// Token: 0x06001913 RID: 6419 RVA: 0x00110608 File Offset: 0x0010EA08
	private void SetPoptimer(float _number, int _position)
	{
		float num = 0.05f;
		if (this.m_popTimer == null)
		{
			this.m_popTimer = new UIText(this.m_stars, false, "popTimer", Mathf.CeilToInt(_number).ToString(), PsFontManager.GetFont(PsFonts.HurmeSemiBoldMN), num, RelativeTo.ScreenHeight, null, "#000000");
		}
		else
		{
			this.m_popTimer.SetText(Mathf.CeilToInt(_number).ToString());
		}
		this.m_popTimer.SetHorizontalAlign(0.5f + ((float)_position - 1f) * 0.325f);
	}
}
