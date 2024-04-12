using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200000E RID: 14
public static class PsS
{
	// Token: 0x06000057 RID: 87 RVA: 0x00003604 File Offset: 0x00001A04
	public static void Initialize()
	{
		PsS.m_units = new DynamicArray<UnitC>(100, 0.5f, 0.25f, 0.5f);
		PsS.m_customObjects = new DynamicArray<CustomObjectC>(100, 0.5f, 0.25f, 0.5f);
		PsS.m_grounds = new DynamicArray<GroundC>(100, 0.5f, 0.25f, 0.5f);
		Buffs.Initialize();
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00003668 File Offset: 0x00001A68
	public static UnitC AddUnit(Entity _entity, Unit _unitClass)
	{
		UnitC unitC = PsS.m_units.AddItem();
		unitC.m_unit = _unitClass;
		EntityManager.AddComponentToEntity(_entity, unitC);
		return unitC;
	}

	// Token: 0x06000059 RID: 89 RVA: 0x0000368F File Offset: 0x00001A8F
	public static void RemoveUnit(UnitC _c)
	{
		_c.m_unit.DestroyAllContacts();
		EntityManager.RemoveComponentFromEntity(_c);
		PsS.m_units.RemoveItem(_c);
	}

	// Token: 0x0600005A RID: 90 RVA: 0x000036B0 File Offset: 0x00001AB0
	public static CustomObjectC AddCustomObject(Entity _entity, object _customObject)
	{
		CustomObjectC customObjectC = PsS.m_customObjects.AddItem();
		customObjectC.m_customObject = _customObject;
		EntityManager.AddComponentToEntity(_entity, customObjectC);
		return customObjectC;
	}

	// Token: 0x0600005B RID: 91 RVA: 0x000036D7 File Offset: 0x00001AD7
	public static void RemoveCustomObject(CustomObjectC _c)
	{
		EntityManager.RemoveComponentFromEntity(_c);
		PsS.m_customObjects.RemoveItem(_c);
	}

	// Token: 0x0600005C RID: 92 RVA: 0x000036EC File Offset: 0x00001AEC
	public static GroundC AddGround(Entity _entity, Ground _groundClass)
	{
		GroundC groundC = PsS.m_grounds.AddItem();
		groundC.m_ground = _groundClass;
		EntityManager.AddComponentToEntity(_entity, groundC);
		return groundC;
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00003713 File Offset: 0x00001B13
	public static void RemoveGround(GroundC _c)
	{
		EntityManager.RemoveComponentFromEntity(_c);
		PsS.m_grounds.RemoveItem(_c);
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00003728 File Offset: 0x00001B28
	public static void Update()
	{
		int num = PsS.m_units.m_aliveCount;
		for (int i = 0; i < num; i++)
		{
			UnitC unitC = PsS.m_units.m_array[PsS.m_units.m_aliveIndices[i]];
			if (unitC.m_active)
			{
				unitC.m_unit.Update();
			}
			else
			{
				unitC.m_unit.InactiveUpdate();
			}
		}
		PsS.m_units.Update();
		num = PsS.m_grounds.m_aliveCount;
		for (int j = 0; j < num; j++)
		{
			GroundC groundC = PsS.m_grounds.m_array[PsS.m_grounds.m_aliveIndices[j]];
			if (groundC.m_active)
			{
				groundC.m_ground.Update();
			}
		}
		PsS.m_grounds.Update();
		PsS.m_customObjects.Update();
	}

	// Token: 0x0600005F RID: 95 RVA: 0x000037FC File Offset: 0x00001BFC
	public static void ApplyBlastWave(Vector2 _blastCenter, Vector2 _blastForceMinMax, float _blastRadius, float _destroyGroundRadius = 0f, float _damage = 0f)
	{
		int num = PsS.m_units.m_aliveCount;
		for (int i = 0; i < num; i++)
		{
			UnitC unitC = PsS.m_units.m_array[PsS.m_units.m_aliveIndices[i]];
			if (unitC.m_active && unitC.m_unit.m_reactToBlastWaves)
			{
				Unit unit = unitC.m_unit;
				Vector2 vector = Vector2.zero;
				int num2 = 0;
				for (int j = 0; j < unit.m_assembledEntities.Count; j++)
				{
					Entity entity = unit.m_assembledEntities[j];
					List<IComponent> componentsByEntity = EntityManager.GetComponentsByEntity(ComponentType.ChipmunkBody, entity);
					for (int k = 0; k < componentsByEntity.Count; k++)
					{
						ChipmunkBodyC chipmunkBodyC = componentsByEntity[k] as ChipmunkBodyC;
						Vector2 vector2 = ChipmunkProWrapper.ucpBodyGetPos(chipmunkBodyC.body);
						vector += vector2;
						num2++;
					}
				}
				if (_damage > 0f && num2 > 0)
				{
					vector /= (float)num2;
					float magnitude = (_blastCenter - vector).magnitude;
					float num3 = 1f - Mathf.Min(magnitude / _blastRadius, 1f);
					unit.Damage(new Damage(DamageType.Weapon, _damage * num3), 1f, null);
				}
			}
		}
		num = PsS.m_units.m_aliveCount;
		for (int l = 0; l < num; l++)
		{
			UnitC unitC2 = PsS.m_units.m_array[PsS.m_units.m_aliveIndices[l]];
			if (unitC2.m_active && unitC2.m_unit.m_reactToBlastWaves)
			{
				Unit unit2 = unitC2.m_unit;
				for (int m = 0; m < unit2.m_assembledEntities.Count; m++)
				{
					Entity entity2 = unit2.m_assembledEntities[m];
					List<IComponent> componentsByEntity2 = EntityManager.GetComponentsByEntity(ComponentType.ChipmunkBody, entity2);
					for (int n = 0; n < componentsByEntity2.Count; n++)
					{
						ChipmunkBodyC chipmunkBodyC2 = componentsByEntity2[n] as ChipmunkBodyC;
						Vector2 vector3 = ChipmunkProWrapper.ucpBodyGetPos(chipmunkBodyC2.body);
						Vector2 vector4 = -(_blastCenter - vector3).normalized;
						float magnitude2 = (_blastCenter - vector3).magnitude;
						float num4 = 1f - Mathf.Min(magnitude2 / _blastRadius, 1f);
						float num5 = _blastForceMinMax.x + (_blastForceMinMax.y - _blastForceMinMax.x) * num4;
						ChipmunkProWrapper.ucpBodyApplyImpulseAtWorldPoint(chipmunkBodyC2.body, vector4 * num5, vector3);
					}
				}
			}
		}
		List<Entity> entitiesByTag = EntityManager.GetEntitiesByTag("GTAG_DEBRIS");
		foreach (Entity entity3 in entitiesByTag)
		{
			List<IComponent> componentsByEntity3 = EntityManager.GetComponentsByEntity(ComponentType.ChipmunkBody, entity3);
			for (int num6 = 0; num6 < componentsByEntity3.Count; num6++)
			{
				ChipmunkBodyC chipmunkBodyC3 = componentsByEntity3[num6] as ChipmunkBodyC;
				Vector2 vector5 = ChipmunkProWrapper.ucpBodyGetPos(chipmunkBodyC3.body);
				Vector2 vector6 = -(_blastCenter - vector5).normalized;
				float magnitude3 = (_blastCenter - vector5).magnitude;
				float num7 = 1f - Mathf.Min(magnitude3 / _blastRadius, 1f);
				float num8 = _blastForceMinMax.x + (_blastForceMinMax.y - _blastForceMinMax.x) * num7;
				ChipmunkProWrapper.ucpBodyApplyImpulseAtWorldPoint(chipmunkBodyC3.body, vector6 * num8, vector5);
			}
		}
		if (_destroyGroundRadius > 0f)
		{
			AutoGeometryBrush autoGeometryBrush = new AutoGeometryBrush(Mathf.Max(1.5f, _destroyGroundRadius / 16f), false, 1f, 0.66f);
			foreach (AutoGeometryLayer autoGeometryLayer in AutoGeometryManager.m_layers)
			{
				if (autoGeometryLayer.m_groundC.m_ground.m_destructible)
				{
					autoGeometryLayer.PaintWithBrush(autoGeometryBrush, _blastCenter, AGDrawMode.SUB, false, ref autoGeometryLayer.m_bytes, true, false);
				}
			}
		}
		float magnitude4 = (_blastCenter - CameraS.m_mainCamera.transform.position).magnitude;
		float num9 = 1f - ToolBox.getPositionBetween(magnitude4, 100f, _blastRadius * 1.5f);
		CameraS.ShakeMainCamera(num9 * 20f, 0.3f, 2);
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00003C94 File Offset: 0x00002094
	public static void ApplyBlastWaveToGround(Vector2 _blastCenter, float _destroyGroundRadius)
	{
		if (_destroyGroundRadius > 0f)
		{
			AutoGeometryBrush autoGeometryBrush = new AutoGeometryBrush(Mathf.Max(1.5f, _destroyGroundRadius / 16f), false, 1f, 0.66f);
			foreach (AutoGeometryLayer autoGeometryLayer in AutoGeometryManager.m_layers)
			{
				if (autoGeometryLayer.m_groundC.m_ground.m_destructible)
				{
					autoGeometryLayer.PaintWithBrush(autoGeometryBrush, _blastCenter, AGDrawMode.SUB, false, ref autoGeometryLayer.m_bytes, true, false);
				}
			}
		}
	}

	// Token: 0x0400002C RID: 44
	public static DynamicArray<UnitC> m_units;

	// Token: 0x0400002D RID: 45
	public static DynamicArray<CustomObjectC> m_customObjects;

	// Token: 0x0400002E RID: 46
	public static DynamicArray<GroundC> m_grounds;
}
