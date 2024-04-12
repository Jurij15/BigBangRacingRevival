using System;
using UnityEngine;

// Token: 0x02000027 RID: 39
public class PsPowerUpFreeze : PsPowerUp
{
	// Token: 0x0600010C RID: 268 RVA: 0x0000CAAC File Offset: 0x0000AEAC
	public PsPowerUpFreeze(float _duration)
		: base(_duration)
	{
	}

	// Token: 0x0600010D RID: 269 RVA: 0x0000CAB8 File Offset: 0x0000AEB8
	protected override bool UseOne()
	{
		SoundS.PlaySingleShot("/UI/ButtonTransition", Vector2.zero, 1f);
		GhostObjectBoss boss = (PsState.m_activeGameLoop.m_gameMode as PsGameModeAdventureBattle).GetBoss();
		if (boss.ReachedGoal())
		{
			return true;
		}
		boss.AddFreeze(base.GetDuration());
		return true;
	}

	// Token: 0x0600010E RID: 270 RVA: 0x0000CB10 File Offset: 0x0000AF10
	public override void GhostUse(GhostObjectBoss _ghost)
	{
		Vehicle vehicle = PsState.m_activeMinigame.m_playerUnit as Vehicle;
		Vector3 pos = vehicle.m_chassisPrefab.p_gameObject.transform.position;
		Vector3 rot = vehicle.m_chassisPrefab.p_gameObject.transform.localRotation.eulerAngles;
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.EffectFreezePrefab_GameObject);
		TransformC freeze = TransformS.AddComponent(PsState.m_activeMinigame.m_playerUnit.m_entity, "Freeze");
		PrefabC freezeFap = PrefabS.AddComponent(freeze, pos, gameObject);
		TransformS.SetRotation(freeze, rot);
		Debug.LogError("Starting slowmo");
		_ghost.m_ghostPowerUp = true;
		PsState.m_activeMinigame.TweenTimeScale(this.TargetTimeScale(), TweenStyle.ExpoIn, 0f, delegate
		{
			Debug.LogError("Slowmo startween ended");
			Entity e = EntityManager.AddEntity();
			TimerC timerC = TimerS.AddComponent(e, string.Empty, 0f, this.EffectDuration(), false, delegate(TimerC c)
			{
				TimerS.RemoveComponent(c);
				Debug.LogError("Slowmo endtween started");
				PsState.m_activeMinigame.TweenTimeScale(1f, TweenStyle.ExpoIn, 0f, delegate
				{
					_ghost.m_ghostPowerUp = false;
					Debug.LogError("Slowmo ended");
					EntityManager.RemoveEntity(e);
					PrefabS.RemoveComponent(freezeFap, true);
					GameObject gameObject2 = ResourceManager.GetGameObject(RESOURCE.EffectFreezeExplosionPrefab_GameObject);
					PrefabC prefabC = PrefabS.AddComponent(freeze, pos, gameObject2);
					TransformS.SetRotation(freeze, rot);
				}, 0f);
			});
			timerC.useUnscaledDeltaTime = true;
		}, 0f);
	}

	// Token: 0x0600010F RID: 271 RVA: 0x0000CC0F File Offset: 0x0000B00F
	protected virtual float TargetTimeScale()
	{
		return 0f;
	}

	// Token: 0x06000110 RID: 272 RVA: 0x0000CC16 File Offset: 0x0000B016
	private float EffectDuration()
	{
		if (this.TargetTimeScale() <= 0f)
		{
			return base.GetDuration();
		}
		return base.GetDuration() / this.TargetTimeScale();
	}

	// Token: 0x06000111 RID: 273 RVA: 0x0000CC3C File Offset: 0x0000B03C
	public override string GetName()
	{
		return "FREEZE";
	}

	// Token: 0x06000112 RID: 274 RVA: 0x0000CC43 File Offset: 0x0000B043
	public override string GetFrame()
	{
		return string.Empty;
	}
}
