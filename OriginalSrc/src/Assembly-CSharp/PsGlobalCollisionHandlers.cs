using System;

// Token: 0x02000171 RID: 369
public class PsGlobalCollisionHandlers
{
	// Token: 0x06000C70 RID: 3184 RVA: 0x00075AB0 File Offset: 0x00073EB0
	public static void Initialize()
	{
		ChipmunkProS.AddGlobalCollisionHandler(new CollisionDelegate(PsGlobalCollisionHandlers.UnitToGroundCollisionHandler), (ucpCollisionType)4, (ucpCollisionType)2, true, false, true);
		ChipmunkProS.AddGlobalCollisionHandler(new CollisionDelegate(PsGlobalCollisionHandlers.UnitToUnitCollisionHandler), (ucpCollisionType)4, (ucpCollisionType)4, true, false, true);
		ChipmunkProS.AddGlobalCollisionHandler(new CollisionDelegate(PsGlobalCollisionHandlers.UnitToGroundCollisionHandler), (ucpCollisionType)3, (ucpCollisionType)2, true, false, true);
		ChipmunkProS.AddGlobalCollisionHandler(new CollisionDelegate(PsGlobalCollisionHandlers.UnitToUnitCollisionHandler), (ucpCollisionType)3, (ucpCollisionType)4, true, false, true);
		ChipmunkProS.AddGlobalCollisionHandler(new CollisionDelegate(PsGlobalCollisionHandlers.UnitToUnitCollisionHandler), (ucpCollisionType)3, (ucpCollisionType)3, true, false, true);
	}

	// Token: 0x06000C71 RID: 3185 RVA: 0x00075B80 File Offset: 0x00073F80
	public static void UnitToGroundCollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		ChipmunkBodyC chipmunkBodyC = ChipmunkProS.m_bodies.m_array[_pair.ucpComponentIndexA];
		ChipmunkBodyC chipmunkBodyC2 = ChipmunkProS.m_bodies.m_array[_pair.ucpComponentIndexB];
		UnitC unitC = chipmunkBodyC.customComponent as UnitC;
		GroundC groundC = chipmunkBodyC2.customComponent as GroundC;
		if (unitC == null || unitC.m_unit == null)
		{
			return;
		}
		if (groundC == null || groundC.m_ground == null)
		{
			return;
		}
		if (_phase == ucpCollisionPhase.Begin)
		{
			bool flag = unitC.m_unit.AddGroundContact(chipmunkBodyC, chipmunkBodyC2, groundC.m_ground, _pair.point);
			float num = _pair.totalKE * 0.0001f;
			Damage damage = new Damage();
			damage.m_amount[(int)((UIntPtr)0)] = num * 0.5f;
			bool isDead = unitC.m_unit.m_isDead;
			unitC.m_unit.Damage(damage, 1f, null);
			if (!isDead && unitC.m_unit.m_isDead)
			{
				unitC.m_unit.KillingImpact(chipmunkBodyC, _pair.point, _pair.normal, _pair.impulse);
			}
			if (flag)
			{
				groundC.m_ground.StartedContactWithUnit(unitC, _pair, _phase);
			}
		}
		else if (_phase == ucpCollisionPhase.Separate)
		{
			unitC.m_unit.RemoveGroundContact(chipmunkBodyC, groundC.m_ground);
		}
	}

	// Token: 0x06000C72 RID: 3186 RVA: 0x00075CC8 File Offset: 0x000740C8
	public static void UnitToUnitCollisionHandler(ucpCollisionPair _pair, ucpCollisionPhase _phase)
	{
		ChipmunkBodyC chipmunkBodyC = ChipmunkProS.m_bodies.m_array[_pair.ucpComponentIndexA];
		ChipmunkBodyC chipmunkBodyC2 = ChipmunkProS.m_bodies.m_array[_pair.ucpComponentIndexB];
		UnitC unitC = chipmunkBodyC.customComponent as UnitC;
		UnitC unitC2 = chipmunkBodyC2.customComponent as UnitC;
		if (unitC == null || unitC.m_unit == null)
		{
			return;
		}
		if (unitC2 == null || unitC2.m_unit == null)
		{
			return;
		}
		if (_phase == ucpCollisionPhase.Begin)
		{
			unitC.m_unit.AddUnitContact(chipmunkBodyC, chipmunkBodyC2, unitC2.m_unit, _pair.point);
			unitC2.m_unit.AddUnitContact(chipmunkBodyC2, chipmunkBodyC, unitC.m_unit, _pair.point);
			bool isDead = unitC.m_unit.m_isDead;
			bool isDead2 = unitC2.m_unit.m_isDead;
			if (ChipmunkProWrapper.ucpBodyGetType(chipmunkBodyC.body) == ucpBodyType.DYNAMIC && ChipmunkProWrapper.ucpBodyGetType(chipmunkBodyC.body) == ucpBodyType.DYNAMIC)
			{
				float num = chipmunkBodyC.m_mass + chipmunkBodyC2.m_mass;
				float num2 = _pair.totalKE * 0.0001f;
				float num3 = num2 * (chipmunkBodyC2.m_mass / num * 0.5f);
				float num4 = num2 * (chipmunkBodyC.m_mass / num * 0.5f);
				Damage damage = new Damage();
				damage.m_amount[(int)((UIntPtr)0)] = num3 * 1f;
				Damage damage2 = new Damage();
				damage2.m_amount[(int)((UIntPtr)0)] = num4 * 1f;
				unitC.m_unit.Damage(damage, 1f, unitC2.m_unit);
				unitC2.m_unit.Damage(damage2, 1f, unitC.m_unit);
			}
			else
			{
				float num5 = _pair.totalKE * 0.0001f;
				if (ChipmunkProWrapper.ucpBodyGetType(chipmunkBodyC.body) == ucpBodyType.DYNAMIC)
				{
					Damage damage3 = new Damage();
					damage3.m_amount[(int)((UIntPtr)0)] = num5 * 0.5f;
					unitC.m_unit.Damage(damage3, 1f, unitC2.m_unit);
				}
				else
				{
					Damage damage4 = new Damage();
					damage4.m_amount[(int)((UIntPtr)0)] = num5 * 0.5f;
					unitC2.m_unit.Damage(damage4, 1f, unitC.m_unit);
				}
			}
			if (!isDead && unitC.m_unit.m_isDead)
			{
				unitC.m_unit.KillingImpact(chipmunkBodyC2, _pair.point, _pair.normal, _pair.impulse);
			}
			if (!isDead2 && unitC2.m_unit.m_isDead)
			{
				unitC2.m_unit.KillingImpact(chipmunkBodyC, _pair.point, -_pair.normal, -_pair.impulse);
			}
		}
		else if (_phase == ucpCollisionPhase.Separate)
		{
			unitC.m_unit.RemoveUnitContact(chipmunkBodyC, chipmunkBodyC2, unitC2.m_unit);
			unitC2.m_unit.RemoveUnitContact(chipmunkBodyC2, chipmunkBodyC, unitC.m_unit);
		}
	}
}
