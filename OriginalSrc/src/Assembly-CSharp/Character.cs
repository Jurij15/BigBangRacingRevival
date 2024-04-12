using System;
using System.Collections.Generic;

// Token: 0x020000D0 RID: 208
public class Character : Unit
{
	// Token: 0x0600044B RID: 1099 RVA: 0x0000F3AF File Offset: 0x0000D7AF
	public Character(GraphElement _graphElement)
		: base(_graphElement, UnitType.Character)
	{
	}

	// Token: 0x0600044C RID: 1100 RVA: 0x0000F3BC File Offset: 0x0000D7BC
	public override void CalculateCurrentShield()
	{
		for (int i = 0; i < this.m_currentShield.Length; i++)
		{
			this.m_currentShield[i] = this.m_baseShield[i];
			if (this.m_shield != null)
			{
				this.m_currentShield[i] += this.m_shield.m_shieldModifier[i];
			}
			if (this.m_armor != null)
			{
				this.m_currentShield[i] += this.m_armor.m_shieldModifier[i];
			}
			if (this.m_weapon != null)
			{
				this.m_currentShield[i] += this.m_weapon.m_shieldModifier[i];
			}
			for (int j = 0; j < this.m_shieldModifiers.Count; j++)
			{
				this.m_currentShield[i] *= this.m_shieldModifiers[j].m_modifier.m_multipler;
			}
		}
	}

	// Token: 0x0600044D RID: 1101 RVA: 0x0000F4AC File Offset: 0x0000D8AC
	public override void CalculateCurrentArmor()
	{
		for (int i = 0; i < this.m_currentArmor.Length; i++)
		{
			this.m_currentArmor[i] = this.m_baseArmor[i];
			if (this.m_shield != null)
			{
				this.m_currentShield[i] += this.m_shield.m_armorModifier[i];
			}
			if (this.m_armor != null)
			{
				this.m_currentShield[i] += this.m_armor.m_armorModifier[i];
			}
			if (this.m_weapon != null)
			{
				this.m_currentShield[i] += this.m_weapon.m_armorModifier[i];
			}
			for (int j = 0; j < this.m_armorModifiers.Count; j++)
			{
				this.m_currentArmor[i] *= this.m_armorModifiers[j].m_modifier.m_multipler;
			}
		}
	}

	// Token: 0x0400058F RID: 1423
	public List<Container> m_itemContainers;

	// Token: 0x04000590 RID: 1424
	public Shield m_shield;

	// Token: 0x04000591 RID: 1425
	public Armor m_armor;

	// Token: 0x04000592 RID: 1426
	public Weapon m_weapon;
}
