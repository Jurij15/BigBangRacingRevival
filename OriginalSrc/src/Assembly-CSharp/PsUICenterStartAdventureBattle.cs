using System;
using UnityEngine;

// Token: 0x020002CC RID: 716
public class PsUICenterStartAdventureBattle : PsUICenterStartAdventure
{
	// Token: 0x06001528 RID: 5416 RVA: 0x000DB781 File Offset: 0x000D9B81
	public PsUICenterStartAdventureBattle(UIComponent _parent)
		: base(_parent)
	{
	}

	// Token: 0x06001529 RID: 5417 RVA: 0x000DB78C File Offset: 0x000D9B8C
	public override void CreateCenterContent()
	{
		PsUIPowerFuels psUIPowerFuels = new PsUIPowerFuels(this, new Action(this.UpdatePowerFuelCc));
		psUIPowerFuels.SetVerticalAlign(0f);
		this.m_skullRiderCharacter = new PsUISkullRiderCharacter(this.m_TC.p_entity);
		this.m_skullRiderCharacter.Enter();
	}

	// Token: 0x0600152A RID: 5418 RVA: 0x000DB7D8 File Offset: 0x000D9BD8
	public override void CreateLowerLeftContent()
	{
		float num = ((!GameLevelPreview.m_camIsTurned) ? 0.5f : 0f);
		TimerS.AddComponent(this.m_buttonTimerEntity, string.Empty, num, 0f, false, delegate(TimerC c)
		{
			TimerS.RemoveComponent(c);
			if (PlayerPrefsX.GetOffroadRacing() && !(PsState.m_activeGameLoop is PsGameLoopFresh))
			{
				this.m_boostButton = new PsUIBoosterButton(this.m_lowerLeft, false);
				this.m_boostButton.SetHorizontalAlign(0f);
				this.m_boostButton.ShowRefillButton();
			}
			float num2 = 0f;
			if (PsState.m_activeGameLoop is PsGameLoopAdventureBattle)
			{
				PsGameLoopAdventureBattle psGameLoopAdventureBattle = PsState.m_activeGameLoop as PsGameLoopAdventureBattle;
				if (psGameLoopAdventureBattle.m_purchasedPowerFuels != null)
				{
					for (int i = 0; i < psGameLoopAdventureBattle.m_purchasedPowerFuels.Count; i++)
					{
						num2 += (float)psGameLoopAdventureBattle.m_purchasedPowerFuels[i];
					}
				}
			}
			Type currentVehicleType = PsState.GetCurrentVehicleType(true);
			this.vehicleStatsMini = new PsUIVehicleStatsMini(this.m_lowerLeft, currentVehicleType, (int)PsUpgradeManager.GetCurrentPerformance(currentVehicleType), (int)num2);
			this.CreateGarageButton(this.m_lowerLeft);
			this.m_lowerLeft.Update();
			if (this.m_boostButton != null && PsMetagameManager.m_playerStats.boosters < 1)
			{
				this.m_boostButton.GreyScaleOn();
			}
			if (!GameLevelPreview.m_camIsTurned)
			{
				TweenC tweenC = TweenS.AddTransformTween(this.m_garage.m_TC, TweenedProperty.Scale, TweenStyle.BackOut, Vector3.zero, Vector3.one, 0.25f, 0f, false, true);
				tweenC.useUnscaledDeltaTime = true;
			}
		});
	}

	// Token: 0x0600152B RID: 5419 RVA: 0x000DB823 File Offset: 0x000D9C23
	protected override void UpdateUpgradeNotification()
	{
		this.UpdateCc();
		base.UpdateUpgradeNotification();
	}

	// Token: 0x0600152C RID: 5420 RVA: 0x000DB831 File Offset: 0x000D9C31
	public new virtual void CreateLowerCenterContent()
	{
	}

	// Token: 0x0600152D RID: 5421 RVA: 0x000DB834 File Offset: 0x000D9C34
	public override void CreateLowerRightContent()
	{
		float num = ((!GameLevelPreview.m_camIsTurned) ? 0.5f : 0f);
		TimerS.AddComponent(this.m_buttonTimerEntity, string.Empty, num, 0f, false, delegate(TimerC c)
		{
			TimerS.RemoveComponent(c);
			this.CreateGoButton(this.m_lowerRight);
			this.m_lowerRight.Update();
		});
	}

	// Token: 0x0600152E RID: 5422 RVA: 0x000DB880 File Offset: 0x000D9C80
	public override void CreateGoButton(UIComponent _parent)
	{
		if (PsState.m_activeGameLoop.m_scoreBest < 3 && !this.m_hasCoinRouletteButton)
		{
			this.m_go = new PsUIAttentionButton(_parent, Vector3.one * 1.15f, 0.25f, 0.25f, 0.01f);
			this.m_go.SetDepthOffset(-20f);
		}
		else
		{
			this.m_go = new PsUIGenericButton(_parent, 0.25f, 0.25f, 0.01f, "Button");
		}
		this.m_go.SetMargins(0.02f, 0.02f, 0.01f, 0.01f, RelativeTo.ScreenHeight);
		this.m_go.SetOrangeColors(true);
		this.m_go.SetHeight(0.12f, RelativeTo.ScreenHeight);
		this.m_go.SetHorizontalAlign(1f);
		if (PsState.m_activeMinigame.m_gameEnded || PsState.m_activeGameLoop.m_scoreBest > 0)
		{
			this.m_go.SetFittedText(PsStrings.Get(StringID.PLAY_AGAIN), 0.04f, 0.3f, RelativeTo.ScreenHeight, true);
		}
		else
		{
			this.m_go.SetFittedText(PsStrings.Get(StringID.PLAY), 0.04f, 0.2f, RelativeTo.ScreenHeight, true);
		}
	}

	// Token: 0x0600152F RID: 5423 RVA: 0x000DB9B4 File Offset: 0x000D9DB4
	public override void Destroy()
	{
		if (this.m_skullRiderCharacter != null)
		{
			this.m_skullRiderCharacter.Destroy();
			this.m_skullRiderCharacter = null;
		}
		base.Destroy();
	}

	// Token: 0x06001530 RID: 5424 RVA: 0x000DB9DC File Offset: 0x000D9DDC
	public void UpdatePowerFuelCc()
	{
		float num = 0f;
		if (PsState.m_activeGameLoop is PsGameLoopAdventureBattle)
		{
			PsGameLoopAdventureBattle psGameLoopAdventureBattle = PsState.m_activeGameLoop as PsGameLoopAdventureBattle;
			if (psGameLoopAdventureBattle.m_purchasedPowerFuels != null)
			{
				for (int i = 0; i < psGameLoopAdventureBattle.m_purchasedPowerFuels.Count; i++)
				{
					num += (float)psGameLoopAdventureBattle.m_purchasedPowerFuels[i];
				}
			}
		}
		this.vehicleStatsMini.SetPowerFuelCc((int)num);
	}

	// Token: 0x06001531 RID: 5425 RVA: 0x000DBA4D File Offset: 0x000D9E4D
	public void UpdateCc()
	{
		this.vehicleStatsMini.SetCc((int)PsUpgradeManager.GetCurrentPerformance(PsState.GetCurrentVehicleType(true)));
	}

	// Token: 0x040017D6 RID: 6102
	private PsUISkullRiderCharacter m_skullRiderCharacter;

	// Token: 0x040017D7 RID: 6103
	private PsUIVehicleStatsMini vehicleStatsMini;
}
