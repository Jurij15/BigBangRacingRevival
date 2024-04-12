using System;
using UnityEngine;

// Token: 0x02000059 RID: 89
public class FanPlatform : Gadget
{
	// Token: 0x0600020C RID: 524 RVA: 0x000195E8 File Offset: 0x000179E8
	public FanPlatform(GraphElement _graphElement)
		: base(_graphElement)
	{
		base.SetInputOffset(new Vector3(0f, 0f, 12f));
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.FanPlatformPrefab_GameObject);
		TransformC transformC = TransformS.AddComponent(this.m_entity, _graphElement.m_name);
		TransformS.SetTransform(transformC, _graphElement.m_position, _graphElement.m_rotation);
		PrefabC prefabC = PrefabS.AddComponent(transformC, new Vector3(0f, 0f, 10f) + base.GetZBufferBias(), gameObject.transform.Find("fan_platform").gameObject);
		this.m_fanRotorPrefab = PrefabS.AddComponent(transformC, new Vector3(0f, 0f, 10f) + base.GetZBufferBias(), gameObject.transform.Find("fan_blades").gameObject);
		this.m_particleFx = prefabC.p_gameObject.transform.Find("effect_wind");
		this.m_particleFx.gameObject.SetActive(false);
		ucpPolyShape ucpPolyShape = ChipmunkProS.GeneratePolyShapeFromGameObject(gameObject.transform.Find("collision1").gameObject, Vector2.zero, 1f, 0.25f, 0.9f, (ucpCollisionType)4, 257U, false, false);
		this.m_cmb = ChipmunkProS.AddStaticBody(transformC, ucpPolyShape, this.m_unitC);
		this.m_fanAreaSize = new Vector2(140f, 250f);
		ucpPolyShape ucpPolyShape2 = new ucpPolyShape(this.m_fanAreaSize.x, this.m_fanAreaSize.y, new Vector2(0f, this.m_fanAreaSize.y * 0.5f), 16777216U, 1f, 0.1f, 0.1f, (ucpCollisionType)4, true);
		ChipmunkBodyC chipmunkBodyC = ChipmunkProS.AddStaticBody(transformC, ucpPolyShape2, this.m_unitC);
		chipmunkBodyC.m_identifier = 1;
		this.m_bb = CameraS.CenterBB(_graphElement.m_position + transformC.transform.up * 125f, new cpBB(0f, 0f, 500f, 500f));
		if (!this.m_minigame.m_editing)
		{
			this.m_fanLoopSound = SoundS.AddCombineSoundComponent(transformC, "FanLoopSound", "/InGame/Units/FanNoiseLoop", 1f);
			ChipmunkProS.AddCollisionHandler(chipmunkBodyC, new CollisionDelegate(this.FanCollisionHandler), (ucpCollisionType)4, (ucpCollisionType)3, true, true, false);
			ChipmunkProS.AddCollisionHandler(chipmunkBodyC, new CollisionDelegate(this.FanCollisionHandler), (ucpCollisionType)4, (ucpCollisionType)4, true, true, false);
		}
		else
		{
			this.m_fanLoopSound = null;
		}
		this.CreateEditorTouchArea(prefabC.p_gameObject, null);
		base.m_graphElement.m_isRotateable = true;
	}

	// Token: 0x0600020D RID: 525 RVA: 0x0001987C File Offset: 0x00017C7C
	private void FanCollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
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
		Vector2 vector = ChipmunkProWrapper.ucpBodyGetPos(chipmunkBodyC.body);
		Vector2 vector2 = _pair.point - vector;
		if (vector2.magnitude > 50f)
		{
			float y = this.m_cmb.TC.transform.InverseTransformPoint(_pair.point).y;
			float num = (1f - ToolBox.getPositionBetween(y, 0f, this.m_fanAreaSize.y)) * Main.m_timeScale;
			Vector2 vector3 = this.m_cmb.TC.transform.up.normalized * 40f * num / (float)chipmunkBodyC.shapes.Count;
			ChipmunkProWrapper.ucpBodyApplyImpulseAtWorldPoint(chipmunkBodyC.body, vector3 * chipmunkBodyC.m_mass * 0.5f, vector + vector2);
			ChipmunkProWrapper.ucpBodyApplyImpulseAtWorldPoint(chipmunkBodyC.body, vector3 * chipmunkBodyC.m_mass * 0.5f, vector);
		}
		else
		{
			float y2 = this.m_cmb.TC.transform.InverseTransformPoint(chipmunkBodyC.TC.transform.position).y;
			float num2 = (1f - ToolBox.getPositionBetween(y2, 0f, this.m_fanAreaSize.y)) * Main.m_timeScale;
			Vector2 vector4 = this.m_cmb.TC.transform.up.normalized * 40f * num2 / (float)chipmunkBodyC.shapes.Count;
			ChipmunkProWrapper.ucpBodyApplyImpulseAtWorldPoint(chipmunkBodyC.body, vector4 * chipmunkBodyC.m_mass, vector);
		}
	}

	// Token: 0x0600020E RID: 526 RVA: 0x00019A90 File Offset: 0x00017E90
	public override void Update()
	{
		base.Update();
		if (!this.m_minigame.m_editing && this.m_powerOn)
		{
			if (this.m_fanRotorPrefab.p_gameObject.GetComponent<Renderer>().isVisible)
			{
				this.m_fanRotorPrefab.p_gameObject.transform.Rotate(new Vector3(0f, 15f, 0f), 1);
			}
			if (ChipmunkProWrapper.ucpBBIntersects(CameraS.m_mainCameraFrame, this.m_bb))
			{
				this.m_particleFx.gameObject.SetActive(true);
			}
			else
			{
				this.m_particleFx.gameObject.SetActive(false);
			}
			if (this.m_minigame.m_gameStarted)
			{
				SoundS.PlaySound(this.m_fanLoopSound, false);
			}
		}
		else if (!ChipmunkProWrapper.ucpBBIntersects(CameraS.m_mainCameraFrame, this.m_bb))
		{
			this.m_particleFx.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600020F RID: 527 RVA: 0x00019B88 File Offset: 0x00017F88
	public override void PowerStateChange(bool _state)
	{
		base.PowerStateChange(_state);
		if (_state)
		{
			if (ChipmunkProWrapper.ucpBBIntersects(CameraS.m_mainCameraFrame, this.m_bb))
			{
				this.m_particleFx.gameObject.SetActive(true);
			}
			for (int i = 0; i < this.m_contacts.Count; i++)
			{
				ChipmunkProWrapper.ucpBodyActivate(this.m_contacts[i].m_cmb.body);
			}
		}
		else
		{
			SoundS.StopSound(this.m_fanLoopSound);
			this.m_particleFx.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000210 RID: 528 RVA: 0x00019C20 File Offset: 0x00018020
	public override void Kill(DamageType _damageType, float _totalDamage)
	{
		base.Kill(_damageType, _totalDamage);
		this.Destroy();
	}

	// Token: 0x040001F9 RID: 505
	private ChipmunkBodyC m_cmb;

	// Token: 0x040001FA RID: 506
	private PrefabC m_fanRotorPrefab;

	// Token: 0x040001FB RID: 507
	private Vector2 m_fanAreaSize;

	// Token: 0x040001FC RID: 508
	private SoundC m_fanLoopSound;

	// Token: 0x040001FD RID: 509
	private Transform m_particleFx;

	// Token: 0x040001FE RID: 510
	private cpBB m_bb;
}
