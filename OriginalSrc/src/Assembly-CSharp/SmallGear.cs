using System;
using UnityEngine;

// Token: 0x0200007F RID: 127
public class SmallGear : Gadget
{
	// Token: 0x0600027D RID: 637 RVA: 0x000205B8 File Offset: 0x0001E9B8
	public SmallGear(GraphElement _graphElement)
		: base(_graphElement)
	{
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.SmallGearPrefab_GameObject);
		this.m_TC = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetTransform(this.m_TC, _graphElement.m_position, _graphElement.m_rotation);
		PrefabC prefabC = PrefabS.AddComponent(this.m_TC, new Vector3(0f, 0f, 0f) + base.GetZBufferBias(), gameObject);
		if (!this.m_minigame.m_editing)
		{
			this.m_gearSound = SoundS.AddCombineSoundComponent(this.m_TC, "BigGearLoopSound", "/InGame/Units/BigGearTurning", 2f);
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

	// Token: 0x0600027E RID: 638 RVA: 0x0002071F File Offset: 0x0001EB1F
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

	// Token: 0x0600027F RID: 639 RVA: 0x0002075C File Offset: 0x0001EB5C
	private void SetGear(bool _on)
	{
		this.m_angle = 0f;
		if (_on)
		{
			this.m_angle = ToolBox.getCappedAngle(this.m_angle - 0.3f * Main.m_timeScale * (float)((!base.m_graphElement.m_flipped) ? 1 : (-1)) * 1.6f);
		}
		ChipmunkProWrapper.ucpBodySetAngle(this.m_cmb.body, (this.m_TC.transform.localRotation.eulerAngles.z + this.m_angle) * 0.017453292f);
	}

	// Token: 0x06000280 RID: 640 RVA: 0x000207F4 File Offset: 0x0001EBF4
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

	// Token: 0x06000281 RID: 641 RVA: 0x00020857 File Offset: 0x0001EC57
	public override void Kill(DamageType _damageType, float _totalDamage)
	{
		base.Kill(_damageType, _totalDamage);
		this.Destroy();
	}

	// Token: 0x040002B8 RID: 696
	private ChipmunkBodyC m_cmb;

	// Token: 0x040002B9 RID: 697
	private float m_angle;

	// Token: 0x040002BA RID: 698
	private SoundC m_gearSound;

	// Token: 0x040002BB RID: 699
	private TransformC m_TC;
}
