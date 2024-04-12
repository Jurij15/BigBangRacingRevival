using System;

// Token: 0x020000C4 RID: 196
public class Weapon : Item
{
	// Token: 0x060003D7 RID: 983 RVA: 0x000370AF File Offset: 0x000354AF
	public Weapon(GraphElement _graphElement)
		: base(_graphElement, ItemType.Weapon)
	{
	}

	// Token: 0x04000501 RID: 1281
	public WeaponType m_weaponType;
}
