using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000079 RID: 121
public class PsGravitySwitch : Unit
{
	// Token: 0x06000269 RID: 617 RVA: 0x0001F1D4 File Offset: 0x0001D5D4
	public PsGravitySwitch(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		this.m_alienContactCount = 0;
		EntityManager.AddTagForEntity(this.m_entity, "GravitySwitch");
		this.m_tc = TransformS.AddComponent(this.m_entity);
		TransformS.SetGlobalTransform(this.m_tc, base.m_graphElement.m_position, base.m_graphElement.m_rotation);
		this.SetGraphics();
		if (!this.m_minigame.m_editing)
		{
			ucpCircleShape ucpCircleShape = new ucpCircleShape(50f, Vector2.zero, 257U, 1f, 0.5f, 0.5f, (ucpCollisionType)10, true);
			this.m_cmb = ChipmunkProS.AddStaticBody(this.m_tc, ucpCircleShape, null);
			ChipmunkProS.AddCollisionHandler(this.m_cmb, new CollisionDelegate(this.TriggerCollisionHandler), (ucpCollisionType)10, (ucpCollisionType)3, true, false, true);
			if (PsGravitySwitch.m_reversedGravityEffect == null)
			{
				PsGravitySwitch.m_reversedGravityEffect = Object.Instantiate<GameObject>(ResourceManager.GetGameObject(RESOURCE.EffectGravitationStreaksPrefab_GameObject));
				PsGravitySwitch.m_reversedGravityEffect.SetActive(false);
			}
		}
		this.CreateEditorTouchArea(50f, 50f, null, default(Vector2));
	}

	// Token: 0x0600026A RID: 618 RVA: 0x0001F2EC File Offset: 0x0001D6EC
	public virtual void TriggerCollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		ChipmunkBodyC chipmunkBodyC = ChipmunkProS.m_bodies.m_array[_pair.ucpComponentIndexB];
		UnitC unitC = chipmunkBodyC.customComponent as UnitC;
		if (unitC == null || unitC.m_unit == null)
		{
			return;
		}
		if (_phase == ucpCollisionPhase.Begin)
		{
			if (this.m_alienContactCount == 0 && PsGravitySwitch.m_cooldownOver < Main.m_resettingGameTime)
			{
				PsGravitySwitch.m_cooldownOver = Main.m_resettingGameTime + 0.5f;
				this.SwitchGravity();
			}
			this.m_alienContactCount++;
		}
		else if (_phase == ucpCollisionPhase.Separate)
		{
			this.m_alienContactCount--;
		}
	}

	// Token: 0x0600026B RID: 619 RVA: 0x0001F388 File Offset: 0x0001D788
	private void SwitchGravity()
	{
		this.m_minigame.m_gravityMultipler *= -1;
		Vector2 vector = this.m_minigame.m_globalGravity * (float)this.m_minigame.m_gravityMultipler;
		SoundS.PlaySingleShot("/Ingame/Units/GravityButton", this.m_tc.transform.position, 1f);
		List<IComponent> componentsByType = EntityManager.GetComponentsByType((ComponentType)30);
		for (int i = 0; i < componentsByType.Count; i++)
		{
			UnitC unitC = componentsByType[i] as UnitC;
			unitC.m_unit.SetGravity(vector);
			if (unitC.m_unit is PsGravitySwitch)
			{
				bool flag = (unitC.m_unit as PsGravitySwitch).m_effect.Toggle();
				PsGravitySwitch.m_reversedGravityEffect.SetActive(flag);
			}
		}
		List<Entity> entitiesByTag = EntityManager.GetEntitiesByTag("GTAG_DEBRIS");
		if (entitiesByTag != null)
		{
			for (int j = 0; j < entitiesByTag.Count; j++)
			{
				List<IComponent> componentsByEntity = EntityManager.GetComponentsByEntity(ComponentType.ChipmunkBody, entitiesByTag[j]);
				for (int k = 0; k < componentsByEntity.Count; k++)
				{
					ChipmunkBodyC chipmunkBodyC = componentsByEntity[k] as ChipmunkBodyC;
					if (chipmunkBodyC.m_isDynamic)
					{
						ChipmunkProWrapper.ucpBodySetGravity(chipmunkBodyC.body, vector);
						ChipmunkProWrapper.ucpBodyActivate(chipmunkBodyC.body);
					}
				}
			}
		}
	}

	// Token: 0x0600026C RID: 620 RVA: 0x0001F4E0 File Offset: 0x0001D8E0
	public void SetGraphics()
	{
		this.m_mainPrefab = ResourceManager.GetGameObject(RESOURCE.GravitationSwitchPrefab_GameObject);
		PrefabC prefabC = PrefabS.AddComponent(this.m_tc, new Vector3(0f, 0f, 130f), this.m_mainPrefab);
		prefabC.p_gameObject.transform.Rotate(new Vector3(0f, 180f, 0f));
		this.m_effect = prefabC.p_gameObject.GetComponent<EffectGravitationSwitch>();
	}

	// Token: 0x0600026D RID: 621 RVA: 0x0001F558 File Offset: 0x0001D958
	public override void Destroy()
	{
		base.Destroy();
		if (PsGravitySwitch.m_reversedGravityEffect != null)
		{
			Object.Destroy(PsGravitySwitch.m_reversedGravityEffect);
			PsGravitySwitch.m_reversedGravityEffect = null;
		}
	}

	// Token: 0x0600026E RID: 622 RVA: 0x0001F580 File Offset: 0x0001D980
	public override void Update()
	{
		if (this.m_minigame.m_gravityMultipler > 0 && this.m_effect.coilLightning.isPlaying)
		{
			this.m_effect.coilLightning.Stop();
		}
		base.Update();
	}

	// Token: 0x04000293 RID: 659
	protected GameObject m_mainPrefab;

	// Token: 0x04000294 RID: 660
	protected TransformC m_tc;

	// Token: 0x04000295 RID: 661
	protected ChipmunkBodyC m_cmb;

	// Token: 0x04000296 RID: 662
	public EffectGravitationSwitch m_effect;

	// Token: 0x04000297 RID: 663
	private int m_alienContactCount;

	// Token: 0x04000298 RID: 664
	private static float m_cooldownOver;

	// Token: 0x04000299 RID: 665
	public static GameObject m_reversedGravityEffect;
}
