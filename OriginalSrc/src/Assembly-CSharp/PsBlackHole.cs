using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000078 RID: 120
public class PsBlackHole : Unit
{
	// Token: 0x06000261 RID: 609 RVA: 0x0001EC38 File Offset: 0x0001D038
	public PsBlackHole(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		this.m_isWhiteHole = this is PsWhiteHole;
		this.m_contactCount = new Dictionary<UnitC, int>();
		EntityManager.AddTagForEntity(this.m_entity, "GravitySwitch");
		this.m_tc = TransformS.AddComponent(this.m_entity);
		TransformS.SetGlobalTransform(this.m_tc, base.m_graphElement.m_position, base.m_graphElement.m_rotation);
		_graphElement.m_isRotateable = false;
		float[] array = new float[] { 1f, 0.75f };
		int num = 0;
		if (_graphElement is LevelIntNode)
		{
			LevelIntNode levelIntNode = _graphElement as LevelIntNode;
			num = levelIntNode.m_int;
			if (_graphElement.m_flipped)
			{
				levelIntNode.m_flipped = false;
				levelIntNode.m_int = ((levelIntNode.m_int + 1 >= array.Length) ? 0 : (levelIntNode.m_int + 1));
				num = levelIntNode.m_int;
			}
		}
		this.m_pullRadiusMultiplier = array[Mathf.Clamp(num, 0, array.Length - 1)];
		this.SetGraphics();
		if (!this.m_minigame.m_editing)
		{
			this.AddSound();
			this.CreateCameraTarget();
			ucpCircleShape ucpCircleShape = new ucpCircleShape(500f * this.m_pullRadiusMultiplier, Vector2.zero, 257U, 1f, 0.5f, 0.5f, (ucpCollisionType)10, true);
			this.m_cmb = ChipmunkProS.AddStaticBody(this.m_tc, ucpCircleShape, null);
			ChipmunkProS.AddCollisionHandler(this.m_cmb, new CollisionDelegate(this.TriggerCollisionHandler), (ucpCollisionType)10, (ucpCollisionType)4, true, false, true);
			ChipmunkProS.AddCollisionHandler(this.m_cmb, new CollisionDelegate(this.TriggerCollisionHandler), (ucpCollisionType)10, (ucpCollisionType)3, true, false, true);
			if (!this.m_isWhiteHole)
			{
				ucpCircleShape ucpCircleShape2 = new ucpCircleShape(35f, Vector2.zero, 257U, 1f, 0.5f, 0.5f, (ucpCollisionType)10, true);
				this.m_cmb2 = ChipmunkProS.AddStaticBody(this.m_tc, ucpCircleShape2, null);
				ChipmunkProS.AddCollisionHandler(this.m_cmb2, new CollisionDelegate(this.KillZoneCollisionHandler), (ucpCollisionType)10, (ucpCollisionType)4, true, false, true);
				ChipmunkProS.AddCollisionHandler(this.m_cmb2, new CollisionDelegate(this.KillZoneCollisionHandler), (ucpCollisionType)10, (ucpCollisionType)3, true, false, true);
			}
		}
		this.CreateEditorTouchArea(50f, 50f, null, default(Vector2));
	}

	// Token: 0x06000262 RID: 610 RVA: 0x0001EE7C File Offset: 0x0001D27C
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
			if (!this.m_contactCount.ContainsKey(unitC))
			{
				this.m_contactCount.Add(unitC, 1);
				unitC.m_unit.m_affectingBlackHoles.Add(this);
			}
			else
			{
				this.m_contactCount[unitC] = this.m_contactCount[unitC] + 1;
			}
		}
		else if (_phase == ucpCollisionPhase.Separate)
		{
			int num = this.m_contactCount[unitC] - 1;
			if (num == 0)
			{
				this.m_contactCount.Remove(unitC);
				unitC.m_unit.m_affectingBlackHoles.Remove(this);
			}
			else
			{
				this.m_contactCount[unitC] = num;
			}
		}
	}

	// Token: 0x06000263 RID: 611 RVA: 0x0001EF60 File Offset: 0x0001D360
	public virtual void KillZoneCollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		ChipmunkBodyC chipmunkBodyC = ChipmunkProS.m_bodies.m_array[_pair.ucpComponentIndexB];
		UnitC unitC = chipmunkBodyC.customComponent as UnitC;
		ucpBodyType ucpBodyType = ChipmunkProWrapper.ucpBodyGetType(chipmunkBodyC.body);
		if (unitC == null || unitC.m_unit == null || ucpBodyType != ucpBodyType.DYNAMIC || unitC.m_unit is Goal)
		{
			return;
		}
		if (_phase == ucpCollisionPhase.Begin && !unitC.m_unit.m_teleported)
		{
			SoundS.PlaySingleShot("/InGame/Units/BlackHoleSuck", this.m_tc.transform.position, 1f);
			unitC.m_unit.SetAsTeleporting(20, this.m_tc.transform, this.m_tc.transform, true, false, true, false);
			unitC.m_unit.Kill(DamageType.BlackHole, float.MaxValue);
		}
	}

	// Token: 0x06000264 RID: 612 RVA: 0x0001F030 File Offset: 0x0001D430
	public virtual void SetGraphics()
	{
		this.m_mainPrefab = ResourceManager.GetGameObject(RESOURCE.BlackholePrefab_GameObject);
		PrefabC prefabC = PrefabS.AddComponent(this.m_tc, new Vector3(0f, 18.5f, 100f), this.m_mainPrefab);
		prefabC.p_gameObject.transform.localScale = new Vector3(1.75f, 1.75f, 1.75f);
		Transform transform = prefabC.p_gameObject.transform.Find("BlackholeOuterEdge");
		this.m_effect = prefabC.p_gameObject.GetComponent<EffectBlackhole>();
		if (transform != null)
		{
			transform.localScale *= this.m_pullRadiusMultiplier;
		}
	}

	// Token: 0x06000265 RID: 613 RVA: 0x0001F0E1 File Offset: 0x0001D4E1
	protected virtual void AddSound()
	{
		this.m_blackHoleLoopSound = SoundS.AddCombineSoundComponent(this.m_tc, "BlackHoleLoopSound", "/InGame/Units/BlackHoleLoop", 2.4f);
	}

	// Token: 0x06000266 RID: 614 RVA: 0x0001F103 File Offset: 0x0001D503
	public void CreateCameraTarget()
	{
		this.m_ctc = CameraS.AddTargetComponent(this.m_tc, 250f, 250f, Vector2.zero);
		this.m_ctc.activeRadius = 750f;
	}

	// Token: 0x06000267 RID: 615 RVA: 0x0001F138 File Offset: 0x0001D538
	public override void Update()
	{
		base.Update();
		if (!this.m_effect.enabled)
		{
			this.m_effect.enabled = true;
		}
		if (!this.m_minigame.m_editing && this.m_minigame.m_gameStarted && this.m_blackHoleLoopSound != null && !this.m_blackHoleLoopSound.isPlaying)
		{
			SoundS.PlaySound(this.m_blackHoleLoopSound, false);
		}
	}

	// Token: 0x06000268 RID: 616 RVA: 0x0001F1AE File Offset: 0x0001D5AE
	public override void InactiveUpdate()
	{
		if (this.m_effect.enabled)
		{
			this.m_effect.enabled = false;
		}
		base.InactiveUpdate();
	}

	// Token: 0x04000287 RID: 647
	protected GameObject m_mainPrefab;

	// Token: 0x04000288 RID: 648
	public TransformC m_tc;

	// Token: 0x04000289 RID: 649
	protected ChipmunkBodyC m_cmb;

	// Token: 0x0400028A RID: 650
	protected ChipmunkBodyC m_cmb2;

	// Token: 0x0400028B RID: 651
	private Dictionary<UnitC, int> m_contactCount;

	// Token: 0x0400028C RID: 652
	public const float PULL_RADIUS = 500f;

	// Token: 0x0400028D RID: 653
	public const float KILL_RADIUS = 35f;

	// Token: 0x0400028E RID: 654
	public float m_pullRadiusMultiplier = 1f;

	// Token: 0x0400028F RID: 655
	protected MonoBehaviour m_effect;

	// Token: 0x04000290 RID: 656
	private CameraTargetC m_ctc;

	// Token: 0x04000291 RID: 657
	protected SoundC m_blackHoleLoopSound;

	// Token: 0x04000292 RID: 658
	public bool m_isWhiteHole;
}
