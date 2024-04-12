using System;
using UnityEngine;

// Token: 0x0200005D RID: 93
public class PowerButton : GadgetPowerSource
{
	// Token: 0x0600021D RID: 541 RVA: 0x0001ADE3 File Offset: 0x000191E3
	public PowerButton(GraphElement _graphElement)
		: base(_graphElement)
	{
	}

	// Token: 0x0600021E RID: 542 RVA: 0x0001AE04 File Offset: 0x00019204
	protected override void CreateObject(LevelPowerLink _powerSource)
	{
		_powerSource.m_name = "PowerButton";
		_powerSource.m_outputLimit = 50;
		_powerSource.m_outputOffset = new Vector3(0f, -4f, 47.5f);
		this.m_reverseButton = this.m_triggerOnStart;
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.PowerPlatePrefab_GameObject);
		this.m_group = base.GetGroup();
		float num = 10f;
		TransformC transformC = TransformS.AddComponent(this.m_entity, "BumperBottom");
		TransformS.SetTransform(transformC, _powerSource.m_position, _powerSource.m_rotation);
		GameObject gameObject2 = gameObject.transform.Find("PowerPlatePlatformBase").gameObject;
		PrefabC prefabC = PrefabS.AddComponent(transformC, new Vector3(0f, -18f, 10f) + base.GetZBufferBias(), gameObject2);
		this.m_baseVisuals = prefabC.p_gameObject.GetComponent<PowerPlateBaseVisuals>();
		ucpPolyShape ucpPolyShape = ChipmunkProS.GeneratePolyShapeFromGameObject(gameObject.transform.Find("Collision2").gameObject, new Vector2(0f, 1.5f), 1f, 0f, 0.9f, (ucpCollisionType)4, 257U, false, false);
		ucpPolyShape.group = this.m_group;
		this.m_bumperBottomBody = ChipmunkProS.AddStaticBody(transformC, ucpPolyShape, null);
		this.m_bumperBottomBody.customComponent = this.m_unitC;
		transformC = TransformS.AddComponent(this.m_entity, "BumperTop");
		TransformS.SetTransform(transformC, _powerSource.m_position, _powerSource.m_rotation);
		gameObject2 = gameObject.transform.Find("PowerPlatePlatform").gameObject;
		this.bouncerPrefab = PrefabS.AddComponent(transformC, new Vector3(0f, 0f, 10f) + base.GetZBufferBias(), gameObject2);
		this.m_visuals = this.bouncerPrefab.p_gameObject.GetComponent<PowerPlateVisuals>();
		this.m_visuals.TurnOn(false, this.m_activeColor);
		ucpPolyShape ucpPolyShape2 = ChipmunkProS.GeneratePolyShapeFromGameObject(gameObject.transform.Find("Collision1").gameObject, new Vector2(0f, 1.5f), 10f, 0.1f, 0.9f, (ucpCollisionType)4, 1118208U, false, false);
		ucpPolyShape2.group = this.m_group;
		this.m_bumperTopBody = ChipmunkProS.AddDynamicBody(transformC, ucpPolyShape2, null);
		this.m_bumperTopBody.customComponent = this.m_unitC;
		this.m_restLength = 10f;
		TransformC transformC2 = TransformS.AddComponent(this.m_entity, "Groove1", _powerSource.m_position + new Vector3(0f, 0f, 0f));
		TransformC transformC3 = TransformS.AddComponent(this.m_entity, "Groove2", _powerSource.m_position + new Vector3(0f, num, 0f));
		TransformC transformC4 = TransformS.AddComponent(this.m_entity, "GrooveAnchor", _powerSource.m_position + new Vector3(0f, 0f, 0f));
		ChipmunkProS.AddGrooveJoint(this.m_bumperTopBody, this.m_bumperBottomBody, transformC2, transformC3, transformC4);
		transformC2 = TransformS.AddComponent(this.m_entity, "PowerButtonAnchor1", _powerSource.m_position + new Vector3(0f, -5f));
		transformC3 = TransformS.AddComponent(this.m_entity, "PowerButtonAnchor2", _powerSource.m_position + new Vector3(0f, num + 5f));
		this.m_spring1 = ChipmunkProS.AddDampedSpring(this.m_bumperTopBody, this.m_bumperBottomBody, transformC2, transformC3, -num * 2f, 200f, 100f);
		transformC2 = TransformS.AddComponent(this.m_entity, "RotatoryLimit", _powerSource.m_position + new Vector3(0f, -this.m_restLength));
		ChipmunkProS.AddRotaryLimitJoint(this.m_bumperTopBody, this.m_bumperBottomBody, transformC2, 0f, 0f);
		this.CreateEditorTouchArea(gameObject2, null);
		base.m_graphElement.m_isRotateable = true;
	}

	// Token: 0x0600021F RID: 543 RVA: 0x0001B1E8 File Offset: 0x000195E8
	public override void Update()
	{
		base.Update();
		if (!this.m_minigame.m_editing && this.m_minigame.m_gameStarted)
		{
			Vector2 vector = ChipmunkProWrapper.ucpBodyGetPos(this.m_bumperBottomBody.body);
			Vector2 vector2 = ChipmunkProWrapper.ucpBodyGetPos(this.m_bumperTopBody.body);
			float num = Vector2.Distance(vector, vector2);
			if ((num > this.triggerDistance && !this.m_reverseButton) || (this.m_reverseButton && num <= this.triggerDistance))
			{
				if (!this.m_powerOn && this.m_cooldownOver < Main.m_resettingGameTime)
				{
					this.Trigger();
					SoundS.PlaySingleShot("/Ingame/Units/PowerConnectorOn", vector, 1f);
				}
			}
			else if (this.m_powerOn && this.m_cooldownOver < Main.m_resettingGameTime)
			{
				this.Trigger();
				SoundS.PlaySingleShot("/Ingame/Units/PowerConnectorOff", vector, 1f);
			}
		}
	}

	// Token: 0x06000220 RID: 544 RVA: 0x0001B2E4 File Offset: 0x000196E4
	protected override void Trigger()
	{
		this.m_cooldownOver = Main.m_resettingGameTime + 0.5f;
		base.Trigger();
	}

	// Token: 0x06000221 RID: 545 RVA: 0x0001B2FD File Offset: 0x000196FD
	protected override void TurnOn(bool _anim)
	{
		base.TurnOn(_anim);
		this.m_baseVisuals.TurnOn(_anim, this.m_activeColor);
	}

	// Token: 0x06000222 RID: 546 RVA: 0x0001B318 File Offset: 0x00019718
	protected override void TurnOff(bool _anim)
	{
		base.TurnOff(_anim);
		this.m_baseVisuals.TurnOff(_anim, this.m_disabledColor);
	}

	// Token: 0x06000223 RID: 547 RVA: 0x0001B333 File Offset: 0x00019733
	public override void Kill(DamageType _damageType, float _totalDamage)
	{
		base.Kill(_damageType, _totalDamage);
		this.Destroy();
	}

	// Token: 0x04000216 RID: 534
	private const float MAX_DIST = 60f;

	// Token: 0x04000217 RID: 535
	private const float ACCEL_START = 0f;

	// Token: 0x04000218 RID: 536
	private const float ACCEL_INCREMENT = 0.75f;

	// Token: 0x04000219 RID: 537
	public uint m_group;

	// Token: 0x0400021A RID: 538
	public bool m_wasHit;

	// Token: 0x0400021B RID: 539
	public bool m_onCooldown;

	// Token: 0x0400021C RID: 540
	public float m_accel;

	// Token: 0x0400021D RID: 541
	private ChipmunkBodyC m_bumperBottomBody;

	// Token: 0x0400021E RID: 542
	private ChipmunkBodyC m_bumperTopBody;

	// Token: 0x0400021F RID: 543
	private ChipmunkConstraintC m_spring1;

	// Token: 0x04000220 RID: 544
	private ChipmunkConstraintC m_spring2;

	// Token: 0x04000221 RID: 545
	private float m_restLength;

	// Token: 0x04000222 RID: 546
	private float m_coolDownTimer = 1f;

	// Token: 0x04000223 RID: 547
	private PrefabC bouncerPrefab;

	// Token: 0x04000224 RID: 548
	private bool m_reverseButton;

	// Token: 0x04000225 RID: 549
	private PowerPlateVisuals m_visuals;

	// Token: 0x04000226 RID: 550
	private PowerPlateBaseVisuals m_baseVisuals;

	// Token: 0x04000227 RID: 551
	private float triggerDistance = 4.75f;

	// Token: 0x04000228 RID: 552
	private float m_cooldownOver;
}
