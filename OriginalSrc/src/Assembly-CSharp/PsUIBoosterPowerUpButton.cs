using System;
using UnityEngine;

// Token: 0x02000217 RID: 535
public class PsUIBoosterPowerUpButton : PsUIBoosterButton
{
	// Token: 0x06000F73 RID: 3955 RVA: 0x000920B2 File Offset: 0x000904B2
	public PsUIBoosterPowerUpButton(UIComponent _parent)
		: base(_parent, false)
	{
	}

	// Token: 0x06000F74 RID: 3956 RVA: 0x000920BC File Offset: 0x000904BC
	protected override string GetIcon()
	{
		if (this.HasPowerUp() && !this.m_used)
		{
			return "hud_boost_freeze";
		}
		return "hud_boost_boost";
	}

	// Token: 0x06000F75 RID: 3957 RVA: 0x000920E0 File Offset: 0x000904E0
	protected override void SetTween()
	{
		if (this.HasPowerUp() && !this.m_used)
		{
			TweenC tweenC = TweenS.AddTransformTween(this.m_icon.m_TC, TweenedProperty.Scale, TweenStyle.ElasticOut, new Vector3(0.9f, 0.9f, 1f), new Vector3(1.1f, 1.1f, 1f), 0.5f, 0f, false);
			TweenS.SetAdditionalTweenProperties(tweenC, -1, true, TweenStyle.Linear);
		}
	}

	// Token: 0x06000F76 RID: 3958 RVA: 0x00092154 File Offset: 0x00090554
	public void SetFreezer(bool _usedUp)
	{
		this.m_used = _usedUp;
		if (!_usedUp)
		{
			base.DestroyBoosterCount();
			this.GreyScaleOff();
			base.SetBoosterIcon(0, true);
			if (this.m_tweenedOff)
			{
				this.AddBoost();
			}
		}
		else
		{
			base.SetBoosterIcon(PsMetagameManager.m_playerStats.boosters, true);
			if (PsMetagameManager.m_playerStats.boosters < 1 || PsState.m_activeGameLoop.m_boosterUsed)
			{
				this.GreyScaleOn();
			}
		}
		this.m_used = false;
	}

	// Token: 0x06000F77 RID: 3959 RVA: 0x000921D8 File Offset: 0x000905D8
	private bool HasPowerUp()
	{
		PsPowerUp powerUp = (PsState.m_activeMinigame.m_playerUnit as Vehicle).m_powerUp;
		return powerUp != null;
	}

	// Token: 0x06000F78 RID: 3960 RVA: 0x00092201 File Offset: 0x00090601
	public override void GreyScaleOn()
	{
		if (!this.HasPowerUp() || this.m_used)
		{
			base.GreyScaleOn();
		}
	}

	// Token: 0x06000F79 RID: 3961 RVA: 0x0009221F File Offset: 0x0009061F
	protected override void CreateBoosterCount(bool _update = false)
	{
		if (!this.HasPowerUp() || this.m_used)
		{
			base.CreateBoosterCount(_update);
		}
	}

	// Token: 0x06000F7A RID: 3962 RVA: 0x0009223E File Offset: 0x0009063E
	public void CumulateBooster()
	{
		if (!this.HasPowerUp())
		{
			base.SetBoosterIcon(PsMetagameManager.m_playerStats.boosters, true);
			if (!PsState.m_activeGameLoop.m_boosterUsed)
			{
				this.GreyScaleOff();
			}
			else
			{
				this.GreyScaleOn();
			}
		}
	}

	// Token: 0x04001238 RID: 4664
	private bool m_used;
}
