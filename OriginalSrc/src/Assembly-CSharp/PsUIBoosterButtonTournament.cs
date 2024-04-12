using System;
using UnityEngine;

// Token: 0x0200021B RID: 539
public class PsUIBoosterButtonTournament : PsUIBoosterPowerUpButton
{
	// Token: 0x06000F97 RID: 3991 RVA: 0x00092AE8 File Offset: 0x00090EE8
	public PsUIBoosterButtonTournament(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x06000F98 RID: 3992 RVA: 0x00092AF4 File Offset: 0x00090EF4
	public void UpdateBoosterCount()
	{
		this.m_boosterCount.SetText(PsMetagameManager.m_playerStats.tournamentBoosters.ToString());
		this.m_boosterCount.Update();
	}

	// Token: 0x06000F99 RID: 3993 RVA: 0x00092B2F File Offset: 0x00090F2F
	protected override string GetIcon()
	{
		return "hud_tournament_boost";
	}

	// Token: 0x06000F9A RID: 3994 RVA: 0x00092B38 File Offset: 0x00090F38
	protected override void CreateBoosterCount(bool _update = false)
	{
		if (PsMetagameManager.IsTimedGiftActive(EventGiftTimedType.unlimitedNitros))
		{
			return;
		}
		if (this.m_boosterCount != null)
		{
			this.m_boosterCount.Destroy();
		}
		this.m_boosterCount = new UIText(this.m_button, false, "BoosterCount", string.Empty + PsMetagameManager.m_playerStats.tournamentBoosters, PsFontManager.GetFont(PsFonts.HurmeSemiBold), 0.275f, RelativeTo.ParentWidth, null, "#000000");
		this.m_boosterCount.SetVerticalAlign(0.25f);
		if (_update)
		{
			this.m_boosterCount.Update();
		}
	}

	// Token: 0x06000F9B RID: 3995 RVA: 0x00092BCC File Offset: 0x00090FCC
	public override void RefillButtonHit()
	{
		CameraS.CreateBlur(null);
		PsUIBasePopup popup = new PsUIBasePopup(typeof(PsUIGetTournamentBooster), null, null, null, true, true, InitialPage.Center, false, false, false);
		popup.SetAction("Purchased", delegate
		{
			this.RemoveRefillButton();
			this.CreateBoosterCount(false);
			this.Update();
			this.GreyScaleOff();
		});
		popup.SetAction("Exit", delegate
		{
			popup.Destroy();
			CameraS.RemoveBlur();
			this.SetResourceView();
		});
		if (PsMetagameManager.m_menuResourceView != null)
		{
			this.m_lastResourceView = PsMetagameManager.m_menuResourceView.SetLastView();
		}
		TweenS.AddTransformTween(popup.m_mainContent.m_parent.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, Vector3.one * 0.75f, Vector3.one, 0.75f, 0f, true);
		PsMetagameManager.ShowResources(popup.m_mainContent.m_camera, true, false, true, false, 0.03f, false, false, false);
	}

	// Token: 0x06000F9C RID: 3996 RVA: 0x00092CBC File Offset: 0x000910BC
	public override void AddBoost()
	{
		base.RemoveRefillButton();
		TweenC tweenC = TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.BounceOut, Vector3.one * 1.05f, 0.05f, 0f, true);
		TweenS.AddTweenEndEventListener(tweenC, delegate(TweenC _t)
		{
			if (this.m_boosterCount == null)
			{
				this.CreateBoosterCount(false);
				this.m_boosterCount.SetText("0");
			}
			if (this.m_boosterCount != null)
			{
				this.m_boosterCount.SetText(((float)Convert.ToInt32(this.m_boosterCount.m_text) + 1f).ToString());
				this.m_boosterCount.Update();
			}
			TweenS.AddTransformTween(this.m_TC, TweenedProperty.Scale, TweenStyle.Linear, Vector3.one, 0.1f, 0f, true);
		});
		this.m_tweenedOff = false;
	}

	// Token: 0x06000F9D RID: 3997 RVA: 0x00092D11 File Offset: 0x00091111
	public override bool IsUnavailable()
	{
		return PsMetagameManager.m_playerStats.tournamentBoosters < 1 || PsState.m_activeGameLoop.m_boosterUsed;
	}

	// Token: 0x06000F9E RID: 3998 RVA: 0x00092D30 File Offset: 0x00091130
	protected override bool ShowRefillButtonNeeded()
	{
		return PsMetagameManager.m_playerStats.tournamentBoosters < 1 && (!PsState.m_activeMinigame.m_gameStarted || PsState.m_activeMinigame.m_gameEnded);
	}
}
