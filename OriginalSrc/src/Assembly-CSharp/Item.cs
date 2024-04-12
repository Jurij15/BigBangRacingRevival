using System;

// Token: 0x020000BE RID: 190
public class Item : BasicAssembledClass
{
	// Token: 0x060003CE RID: 974 RVA: 0x00037050 File Offset: 0x00035450
	public Item(GraphElement _graphElement, ItemType _itemType)
		: base(_graphElement)
	{
		this.m_itemType = _itemType;
	}

	// Token: 0x060003CF RID: 975 RVA: 0x00037060 File Offset: 0x00035460
	public virtual void Update()
	{
	}

	// Token: 0x060003D0 RID: 976 RVA: 0x00037062 File Offset: 0x00035462
	public virtual void Use()
	{
	}

	// Token: 0x060003D1 RID: 977 RVA: 0x00037064 File Offset: 0x00035464
	public virtual void PickUp()
	{
	}

	// Token: 0x060003D2 RID: 978 RVA: 0x00037066 File Offset: 0x00035466
	public virtual void Discard()
	{
	}

	// Token: 0x040004F4 RID: 1268
	public ItemType m_itemType;

	// Token: 0x040004F5 RID: 1269
	public float m_durability;

	// Token: 0x040004F6 RID: 1270
	public float m_maxDurability;

	// Token: 0x040004F7 RID: 1271
	public float m_maxHealthModifer;

	// Token: 0x040004F8 RID: 1272
	public float m_maxEnergyModifier;

	// Token: 0x040004F9 RID: 1273
	public float[] m_shieldModifier;

	// Token: 0x040004FA RID: 1274
	public float[] m_armorModifier;
}
