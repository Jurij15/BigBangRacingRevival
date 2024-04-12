using System;
using UnityEngine;

// Token: 0x0200005F RID: 95
public class PowerSwitch : GadgetPowerSource
{
	// Token: 0x06000228 RID: 552 RVA: 0x0001B545 File Offset: 0x00019945
	public PowerSwitch(GraphElement _graphElement)
		: base(_graphElement)
	{
	}

	// Token: 0x06000229 RID: 553 RVA: 0x0001B550 File Offset: 0x00019950
	protected override void CreateObject(LevelPowerLink _powerSource)
	{
		_powerSource.m_name = "PowerSwitch";
		_powerSource.m_outputOffset = new Vector3(0f, 45f, 10f);
		_powerSource.m_outputLimit = 50;
		this.m_tc = TransformS.AddComponent(this.m_entity, _powerSource.m_name);
		TransformS.SetTransform(this.m_tc, _powerSource.m_position + new Vector3(0f, 0f, 50f), _powerSource.m_rotation);
		GameObject gameObject = ResourceManager.GetGameObject(RESOURCE.EnergySwitchBoxPrefab_GameObject);
		this.m_mainBody = PrefabS.AddComponent(this.m_tc, new Vector3(0f, 0f, 0f) + base.GetZBufferBias(), gameObject);
		this.m_visuals = this.m_mainBody.p_gameObject.GetComponent<SwitchAnimation>();
		this.m_visuals.SetHandleColor(this.m_activeColor);
		this.m_shape = new ucpCircleShape(45f, Vector2.zero, 17895696U, 0f, 0f, 0f, (ucpCollisionType)10, true);
		this.m_body = ChipmunkProS.AddStaticBody(this.m_tc, this.m_shape, null);
		if (!this.m_minigame.m_editing)
		{
			ChipmunkProS.AddCollisionHandler(this.m_body, new CollisionDelegate(this.TriggerCollisionHandler), (ucpCollisionType)10, (ucpCollisionType)3, true, false, true);
		}
		this.m_powerSource.m_isRotateable = false;
		if (this.m_minigame.m_editing)
		{
			this.CreateGraphElementTouchArea(45f, 45f, null, default(Vector2));
		}
	}

	// Token: 0x0600022A RID: 554 RVA: 0x0001B6DC File Offset: 0x00019ADC
	protected override void TurnOn(bool _anim)
	{
		base.TurnOn(_anim);
		this.m_visuals.TurnOn(_anim, this.m_activeColor);
		if (_anim)
		{
			Vector2 vector = ChipmunkProWrapper.ucpBodyGetPos(this.m_body.body);
			SoundS.PlaySingleShot("/Ingame/Units/PowerConnectorOn", vector, 1f);
		}
	}

	// Token: 0x0600022B RID: 555 RVA: 0x0001B730 File Offset: 0x00019B30
	protected override void TurnOff(bool _anim)
	{
		base.TurnOff(_anim);
		this.m_visuals.TurnOff(_anim, this.m_disabledColor);
		if (_anim)
		{
			Vector2 vector = ChipmunkProWrapper.ucpBodyGetPos(this.m_body.body);
			SoundS.PlaySingleShot("/Ingame/Units/PowerConnectorOff", vector, 1f);
		}
	}

	// Token: 0x0600022C RID: 556 RVA: 0x0001B784 File Offset: 0x00019B84
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
			if (this.m_alienContactCount == 0 && this.m_cooldownOver < Main.m_resettingGameTime)
			{
				this.Trigger();
			}
			this.m_alienContactCount++;
		}
		else if (_phase == ucpCollisionPhase.Separate)
		{
			this.m_alienContactCount--;
		}
	}

	// Token: 0x0600022D RID: 557 RVA: 0x0001B811 File Offset: 0x00019C11
	protected override void Trigger()
	{
		this.m_cooldownOver = Main.m_resettingGameTime + 0.5f;
		base.Trigger();
	}

	// Token: 0x0400022F RID: 559
	private const int RADIUS = 45;

	// Token: 0x04000230 RID: 560
	private ChipmunkBodyC m_body;

	// Token: 0x04000231 RID: 561
	private ucpShape m_shape;

	// Token: 0x04000232 RID: 562
	private TransformC m_tc;

	// Token: 0x04000233 RID: 563
	private new bool m_powerOn;

	// Token: 0x04000234 RID: 564
	private PrefabC m_mainBody;

	// Token: 0x04000235 RID: 565
	private SwitchAnimation m_visuals;

	// Token: 0x04000236 RID: 566
	public int m_collidingCount;

	// Token: 0x04000237 RID: 567
	private float m_cooldownOver;

	// Token: 0x04000238 RID: 568
	private int m_alienContactCount;
}
