using System;

// Token: 0x02000026 RID: 38
public abstract class PsPowerUp
{
	// Token: 0x06000104 RID: 260 RVA: 0x0000CA24 File Offset: 0x0000AE24
	public PsPowerUp(float _duration)
	{
		this.m_duration = _duration;
	}

	// Token: 0x06000105 RID: 261
	protected abstract bool UseOne();

	// Token: 0x06000106 RID: 262
	public abstract void GhostUse(GhostObjectBoss _ghost);

	// Token: 0x06000107 RID: 263
	public abstract string GetName();

	// Token: 0x06000108 RID: 264
	public abstract string GetFrame();

	// Token: 0x06000109 RID: 265 RVA: 0x0000CA33 File Offset: 0x0000AE33
	public float GetDuration()
	{
		return this.m_duration;
	}

	// Token: 0x0600010A RID: 266 RVA: 0x0000CA3C File Offset: 0x0000AE3C
	public bool Use()
	{
		bool flag = this.UseOne();
		this.SetUI(flag);
		return flag;
	}

	// Token: 0x0600010B RID: 267 RVA: 0x0000CA58 File Offset: 0x0000AE58
	public void SetUI(bool _usedUp)
	{
		if (PsIngameMenu.m_controller != null && PsIngameMenu.m_controller is MotorcycleController && (PsIngameMenu.m_controller as MotorcycleController).m_boostButton != null)
		{
			((PsIngameMenu.m_controller as MotorcycleController).m_boostButton as PsUIBoosterPowerUpButton).SetFreezer(_usedUp);
		}
	}

	// Token: 0x040000C4 RID: 196
	protected PsGameModeAdventureBattle m_gameMode;

	// Token: 0x040000C5 RID: 197
	private float m_duration;
}
