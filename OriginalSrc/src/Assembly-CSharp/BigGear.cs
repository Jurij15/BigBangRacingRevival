using System;
using UnityEngine;

// Token: 0x0200004B RID: 75
public class BigGear : Gadget
{
	// Token: 0x060001DE RID: 478 RVA: 0x00015C78 File Offset: 0x00014078
	public BigGear(GraphElement _graphElement)
		: base(_graphElement)
	{
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.BigGearPrefab_GameObject);
		this.m_TC = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetTransform(this.m_TC, _graphElement.m_position, _graphElement.m_rotation);
		PrefabC prefabC = PrefabS.AddComponent(this.m_TC, new Vector3(0f, 0f, 0f) + base.GetZBufferBias(), gameObject);
		if (!this.m_minigame.m_editing)
		{
			this.m_gearSound = SoundS.AddCombineSoundComponent(this.m_TC, "BigGearLoopSound", "/InGame/Units/BigGearTurning", 3f);
		}
		else
		{
			this.m_gearSound = null;
		}
		ucpPolyShape[] array = ChipmunkProS.GeneratePolyShapesFromChildren(gameObject.transform.Find("Collision1").gameObject, Vector2.zero, 1f, 0.25f, 0.8f, (ucpCollisionType)4, 257U, false, base.m_graphElement.m_flipped);
		this.m_cmb = ChipmunkProS.AddKinematicBody(this.m_TC, array, this.m_unitC);
		this.CreateEditorTouchArea(prefabC.p_gameObject, null);
		base.m_graphElement.m_isRotateable = true;
		base.m_graphElement.m_isFlippable = true;
		if (base.m_graphElement.m_flipped)
		{
			TransformS.SetScale(this.m_cmb.TC, new Vector3(-1f, 1f, 1f));
		}
	}

	// Token: 0x060001DF RID: 479 RVA: 0x00015DDF File Offset: 0x000141DF
	public override void PowerStateChange(bool _state)
	{
		base.PowerStateChange(_state);
		if (this.m_minigame.m_gameStarted)
		{
			this.SetGear(_state);
		}
		else
		{
			this.m_initialPowerOn = true;
		}
		if (!_state)
		{
			SoundS.StopSound(this.m_gearSound);
		}
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x00015E1C File Offset: 0x0001421C
	private void SetGear(bool _on)
	{
		this.m_angle = 0f;
		if (_on)
		{
			this.m_angle = ToolBox.getCappedAngle(this.m_angle - 0.3f * Main.m_timeScale * (float)((!base.m_graphElement.m_flipped) ? 1 : (-1)));
		}
		ChipmunkProWrapper.ucpBodySetAngle(this.m_cmb.body, (this.m_TC.transform.localRotation.eulerAngles.z + this.m_angle) * 0.017453292f);
	}

	// Token: 0x060001E1 RID: 481 RVA: 0x00015EB0 File Offset: 0x000142B0
	public override void Update()
	{
		base.Update();
		if (!this.m_minigame.m_editing && this.m_minigame.m_gameStarted)
		{
			if (this.m_initialPowerOn)
			{
				this.m_initialPowerOn = false;
				this.SetGear(true);
			}
			if (this.m_powerOn)
			{
				SoundS.PlaySound(this.m_gearSound, false);
			}
		}
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x00015F13 File Offset: 0x00014313
	public override void Kill(DamageType _damageType, float _totalDamage)
	{
		base.Kill(_damageType, _totalDamage);
		this.Destroy();
	}

	// Token: 0x040001B1 RID: 433
	private ChipmunkBodyC m_cmb;

	// Token: 0x040001B2 RID: 434
	private float m_angle;

	// Token: 0x040001B3 RID: 435
	private SoundC m_gearSound;

	// Token: 0x040001B4 RID: 436
	private TransformC m_TC;
}
