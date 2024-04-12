using System;
using UnityEngine;

// Token: 0x02000063 RID: 99
public class DecorationalMagicCauldron : Unit
{
	// Token: 0x06000233 RID: 563 RVA: 0x0001BBB8 File Offset: 0x00019FB8
	public DecorationalMagicCauldron(GraphElement _graphElement)
		: base(_graphElement, UnitType.Basic)
	{
		float num = 150f;
		this.m_node = base.m_graphElement as LevelDecorationNode;
		this.m_tc = TransformS.AddComponent(this.m_entity, "Cauldron");
		TransformS.SetTransform(this.m_tc, this.m_node.m_position + new Vector3(0f, 0f, num) + base.GetZBufferBias(), this.m_node.m_rotation);
		this.m_mainPrefab = ResourceManager.GetGameObject(RESOURCE.DecorationMagicCauldronPrefab_GameObject);
		this.m_cauldron = PrefabS.AddComponent(this.m_tc, Vector3.zero, this.m_mainPrefab, string.Empty, true, true);
		this.m_node.m_isFlippable = false;
		this.m_effect = this.m_cauldron.p_gameObject.GetComponentInChildren<EffectMagicCauldron>();
		if (base.m_graphElement.m_storedRotation == Vector3.zero)
		{
			float num2 = Random.Range(-15f, 15f);
			base.m_graphElement.m_storedRotation = new Vector3(1f, num2, 0f);
		}
		this.m_cauldron.p_gameObject.transform.localRotation *= Quaternion.Euler(new Vector3(0f, base.m_graphElement.m_storedRotation[1], 0f));
		this.CreateEditorTouchArea(50f, 50f, this.m_tc, default(Vector2));
		if (!this.m_minigame.m_editing)
		{
			this.m_cauldronBoilingLoopSound = SoundS.AddCombineSoundComponent(this.m_tc, "CauldronBoilingLoop", "/InGame/Units/CauldronBoilingLoop", 0.8f);
			ucpCircleShape ucpCircleShape = new ucpCircleShape(250f, Vector2.zero, 16777216U, 1f, 1f, 1f, (ucpCollisionType)10, true);
			ChipmunkBodyC chipmunkBodyC = ChipmunkProS.AddStaticBody(this.m_tc, ucpCircleShape, null);
			ChipmunkProS.AddCollisionHandler(chipmunkBodyC, new CollisionDelegate(this.CollisionHandler), (ucpCollisionType)10, (ucpCollisionType)3, true, false, false);
		}
		this.m_triggered = false;
	}

	// Token: 0x06000234 RID: 564 RVA: 0x0001BDC8 File Offset: 0x0001A1C8
	public override void Update()
	{
		if (this.m_triggered && this.m_ticks > 0)
		{
			this.m_ticks--;
			if (this.m_ticks <= 0)
			{
				this.m_triggered = false;
			}
		}
		if (!this.m_minigame.m_editing && this.m_minigame.m_gameStarted && this.m_cauldronBoilingLoopSound != null && !this.m_cauldronBoilingLoopSound.isPlaying)
		{
			SoundS.PlaySound(this.m_cauldronBoilingLoopSound, false);
		}
		base.Update();
	}

	// Token: 0x06000235 RID: 565 RVA: 0x0001BE5C File Offset: 0x0001A25C
	private void CollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		if (!this.m_triggered)
		{
			ChipmunkBodyC chipmunkBodyC = ChipmunkProS.m_bodies.m_array[_pair.ucpComponentIndexB];
			UnitC unitC = chipmunkBodyC.customComponent as UnitC;
			if (unitC == null || unitC.m_unit == null)
			{
				return;
			}
			this.m_effect.SplatCauldron();
			this.m_ticks = 120;
			this.m_triggered = true;
			SoundS.PlaySingleShot("/Ingame/Units/CauldronBlast", new Vector3(this.m_tc.transform.position.x, this.m_tc.transform.position.y, 0f), 1f);
		}
	}

	// Token: 0x0400023C RID: 572
	public LevelDecorationNode m_node;

	// Token: 0x0400023D RID: 573
	public GameObject m_mainPrefab;

	// Token: 0x0400023E RID: 574
	public PrefabC m_cauldron;

	// Token: 0x0400023F RID: 575
	private EffectMagicCauldron m_effect;

	// Token: 0x04000240 RID: 576
	private SoundC m_cauldronBoilingLoopSound;

	// Token: 0x04000241 RID: 577
	public TransformC m_tc;

	// Token: 0x04000242 RID: 578
	private bool m_triggered;

	// Token: 0x04000243 RID: 579
	private int m_ticks = 120;
}
