using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000080 RID: 128
public class SpeedRamp : Gadget
{
	// Token: 0x06000282 RID: 642 RVA: 0x00020868 File Offset: 0x0001EC68
	public SpeedRamp(GraphElement _graphElement)
		: base(_graphElement)
	{
		this.m_speedAddedList = new List<UnitC>();
		this.m_updater = false;
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.SpeedRampPrefab_GameObject);
		TransformC transformC = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetTransform(transformC, _graphElement.m_position, _graphElement.m_rotation);
		GameObject gameObject2 = gameObject.transform.Find("BoostRampBody").gameObject;
		PrefabC prefabC = PrefabS.AddComponent(transformC, new Vector3(0f, 0f, 10f) + base.GetZBufferBias(), gameObject2);
		PrefabC prefabC2 = PrefabS.AddComponent(transformC, new Vector3(0f, 0f, 10f) + base.GetZBufferBias(), gameObject.transform.Find("BoostRampBelt").gameObject);
		if (this.IsSolo())
		{
			Material material = new Material(gameObject.transform.Find("BoostRampBelt").GetComponent<Renderer>().sharedMaterial);
			prefabC2.p_gameObject.GetComponent<Renderer>().material = material;
			this.m_beltMaterial = material;
			this.m_isSolo = true;
		}
		else
		{
			this.m_beltMaterial = gameObject.transform.Find("BoostRampBelt").GetComponent<Renderer>().sharedMaterial;
			this.UvUpdater();
		}
		float num = 0.1f;
		if (!this.m_powerOn)
		{
			num = 1f;
		}
		ucpPolyShape ucpPolyShape = ChipmunkProS.GeneratePolyShapeFromGameObject(gameObject.transform.Find("CollisionBelt").gameObject, Vector2.zero, 1f, 0.25f, num, (ucpCollisionType)4, 257U, false, base.m_graphElement.m_flipped);
		ucpPolyShape[] array = ChipmunkProS.GeneratePolyShapesFromChildren(gameObject.transform.Find("CollisionBody").gameObject, Vector2.zero, 1f, 0.25f, 0.9f, (ucpCollisionType)4, 257U, false, base.m_graphElement.m_flipped);
		ucpPolyShape[] array2 = new ucpPolyShape[array.Length + 1];
		array.CopyTo(array2, 0);
		array2[array2.Length - 1] = ucpPolyShape;
		this.m_cmb = ChipmunkProS.AddStaticBody(transformC, array2, this.m_unitC);
		this.m_beltShape = ucpPolyShape.shapePtr;
		if (!this.m_minigame.m_editing)
		{
			ChipmunkProS.AddCollisionHandler(this.m_cmb, new CollisionDelegate(this.RampCollisionHandler), (ucpCollisionType)4, (ucpCollisionType)3, true, true, false);
			ChipmunkProS.AddCollisionHandler(this.m_cmb, new CollisionDelegate(this.RampCollisionHandler), (ucpCollisionType)4, (ucpCollisionType)4, true, true, false);
		}
		this.m_rampBaseAngle = 27.5f;
		this.CreateEditorTouchArea(prefabC.p_gameObject, null);
		base.m_graphElement.m_isRotateable = true;
		base.m_graphElement.m_isFlippable = true;
		if (base.m_graphElement.m_flipped)
		{
			TransformS.SetScale(this.m_cmb.TC, new Vector3(-1f, 1f, 1f));
		}
	}

	// Token: 0x06000283 RID: 643 RVA: 0x00020B38 File Offset: 0x0001EF38
	public bool IsSolo()
	{
		return base.m_graphElement != null && base.m_graphElement.m_inputs != null && base.m_graphElement.m_inputs.Count > 0;
	}

	// Token: 0x06000284 RID: 644 RVA: 0x00020B6C File Offset: 0x0001EF6C
	private void UvUpdater()
	{
		int num = 0;
		for (int i = 0; i < PsS.m_units.m_aliveCount; i++)
		{
			if (num > 0)
			{
				break;
			}
			UnitC unitC = PsS.m_units.m_array[PsS.m_units.m_aliveIndices[i]];
			SpeedRamp speedRamp = unitC.m_unit as SpeedRamp;
			if (speedRamp != null && speedRamp != this && !speedRamp.m_isDead && !speedRamp.IsSolo())
			{
				num++;
			}
			else if (speedRamp != null && speedRamp == this)
			{
				break;
			}
		}
		if (num == 0)
		{
			this.m_updater = true;
		}
	}

	// Token: 0x06000285 RID: 645 RVA: 0x00020C14 File Offset: 0x0001F014
	public override void Destroy()
	{
		this.m_isDead = true;
		if (this.m_updater)
		{
			for (int i = 0; i < PsS.m_units.m_aliveCount; i++)
			{
				UnitC unitC = PsS.m_units.m_array[PsS.m_units.m_aliveIndices[i]];
				SpeedRamp speedRamp = unitC.m_unit as SpeedRamp;
				if (speedRamp != null && speedRamp != this && !speedRamp.IsSolo())
				{
					speedRamp.m_updater = true;
					break;
				}
			}
		}
		else if (this.m_isSolo)
		{
			Object.Destroy(this.m_beltMaterial);
		}
		base.Destroy();
	}

	// Token: 0x06000286 RID: 646 RVA: 0x00020CB8 File Offset: 0x0001F0B8
	public void UpdateMaterial()
	{
		this.m_beltMaterial.mainTextureOffset -= Vector2.up * 0.02f;
		if (this.m_beltMaterial.mainTextureOffset.y < 0f)
		{
			this.m_beltMaterial.mainTextureOffset = new Vector2(this.m_beltMaterial.mainTextureOffset.x, 1f);
		}
	}

	// Token: 0x06000287 RID: 647 RVA: 0x00020D30 File Offset: 0x0001F130
	public override void PowerStateChange(bool _state)
	{
		base.PowerStateChange(_state);
		if (_state)
		{
			ChipmunkProWrapper.ucpShapeSetFriction(this.m_beltShape, 0.1f);
			for (int i = 0; i < this.m_contacts.Count; i++)
			{
				ChipmunkProWrapper.ucpBodyActivate(this.m_contacts[i].m_cmb.body);
			}
		}
		else
		{
			ChipmunkProWrapper.ucpShapeSetFriction(this.m_beltShape, 1f);
		}
	}

	// Token: 0x06000288 RID: 648 RVA: 0x00020DA8 File Offset: 0x0001F1A8
	private void RampCollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		if (!this.m_powerOn)
		{
			return;
		}
		ChipmunkBodyC chipmunkBodyC = ChipmunkProS.m_bodies.m_array[_pair.ucpComponentIndexB];
		UnitC unitC = chipmunkBodyC.customComponent as UnitC;
		if (unitC == null || unitC.m_unit == null)
		{
			return;
		}
		if ((_phase == ucpCollisionPhase.Begin || _phase == ucpCollisionPhase.Persist) && _pair.shapeA == this.m_beltShape && !this.m_speedAddedList.Contains(unitC))
		{
			if (_phase == ucpCollisionPhase.Begin && this.m_soundTriggerTimer <= 0 && unitC.m_unit.m_entity == this.m_minigame.m_playerTC.p_entity)
			{
				this.m_soundTriggerTimer = 60;
				SoundS.PlaySingleShot("/Ingame/Units/BoostRamp", new Vector3(_pair.point.x, _pair.point.y, 0f), 1f);
			}
			float num = this.m_rampBaseAngle + 90f;
			float num2 = (this.m_cmb.TC.transform.rotation.eulerAngles.z + ((!base.m_graphElement.m_flipped) ? num : (-num))) * 0.017453292f;
			Vector2 vector;
			vector..ctor(Mathf.Sin(num2), -Mathf.Cos(num2));
			Vector2 normalized = vector.normalized;
			unitC.m_unit.AddSpeed(normalized * 20f, 1000f);
			this.m_speedAddedList.Add(unitC);
		}
	}

	// Token: 0x06000289 RID: 649 RVA: 0x00020F2C File Offset: 0x0001F32C
	public override void Update()
	{
		base.Update();
		if (!this.m_minigame.m_editing && (this.m_updater || this.m_isSolo) && this.m_powerOn)
		{
			this.UpdateMaterial();
		}
		if (this.m_soundTriggerTimer > 0)
		{
			this.m_soundTriggerTimer--;
		}
		this.m_speedAddedList.Clear();
	}

	// Token: 0x0600028A RID: 650 RVA: 0x00020F9B File Offset: 0x0001F39B
	public override void Kill(DamageType _damageType, float _totalDamage)
	{
		base.Kill(_damageType, _totalDamage);
		this.Destroy();
	}

	// Token: 0x040002BC RID: 700
	private ChipmunkBodyC m_cmb;

	// Token: 0x040002BD RID: 701
	private int m_soundTriggerTimer;

	// Token: 0x040002BE RID: 702
	private IntPtr m_beltShape;

	// Token: 0x040002BF RID: 703
	private Material m_beltMaterial;

	// Token: 0x040002C0 RID: 704
	private bool m_updater;

	// Token: 0x040002C1 RID: 705
	private int m_addSpeedCounter;

	// Token: 0x040002C2 RID: 706
	private List<UnitC> m_speedAddedList;

	// Token: 0x040002C3 RID: 707
	private float m_rampBaseAngle;

	// Token: 0x040002C4 RID: 708
	private const float BELT_FRICTION_ON = 0.1f;

	// Token: 0x040002C5 RID: 709
	private const float BELT_FRICTION_OFF = 1f;

	// Token: 0x040002C6 RID: 710
	private bool m_isSolo;
}
