using System;

// Token: 0x02000029 RID: 41
public class PsPowerUpTime : PsPowerUp
{
	// Token: 0x06000117 RID: 279 RVA: 0x0000CD9D File Offset: 0x0000B19D
	public PsPowerUpTime(float _duration)
		: base(_duration)
	{
	}

	// Token: 0x06000118 RID: 280 RVA: 0x0000CDA6 File Offset: 0x0000B1A6
	protected override bool UseOne()
	{
		PsState.m_activeMinigame.TweenTimeScale(this.TargetTimeScale(), TweenStyle.CubicInOut, 0.1f, delegate
		{
			Entity entity = EntityManager.AddEntity();
			TimerS.AddComponent(entity, string.Empty, 0f, this.EffectDuration(), true, delegate(TimerC _c)
			{
				PsState.m_activeMinigame.RemoveTimeScale();
				(PsState.m_activeGameLoop as PsGameLoopAdventureBattle).m_playerTimeScale = true;
			});
		}, 0f);
		(PsState.m_activeGameLoop as PsGameLoopAdventureBattle).m_playerTimeScale = true;
		return true;
	}

	// Token: 0x06000119 RID: 281 RVA: 0x0000CDE0 File Offset: 0x0000B1E0
	public override void GhostUse(GhostObjectBoss _ghost)
	{
		_ghost.m_ghost.SpeedUpPlayback(this.EffectDuration(), this.TargetTimeScale());
	}

	// Token: 0x0600011A RID: 282 RVA: 0x0000CDF9 File Offset: 0x0000B1F9
	private float EffectDuration()
	{
		if (this.TargetTimeScale() <= 0f)
		{
			return base.GetDuration();
		}
		return base.GetDuration() / (this.TargetTimeScale() - 1f);
	}

	// Token: 0x0600011B RID: 283 RVA: 0x0000CE25 File Offset: 0x0000B225
	private float TargetTimeScale()
	{
		return 1.3f;
	}

	// Token: 0x0600011C RID: 284 RVA: 0x0000CE2C File Offset: 0x0000B22C
	public override string GetName()
	{
		return "Time 2x";
	}

	// Token: 0x0600011D RID: 285 RVA: 0x0000CE33 File Offset: 0x0000B233
	public override string GetFrame()
	{
		return string.Empty;
	}
}
