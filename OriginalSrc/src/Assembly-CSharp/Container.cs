using System;

// Token: 0x020000C0 RID: 192
public class Container : Item
{
	// Token: 0x060003D4 RID: 980 RVA: 0x00037072 File Offset: 0x00035472
	public Container(GraphElement _graphElement)
		: base(_graphElement, ItemType.Container)
	{
		this.m_capacity = 5;
		this.m_itemCount = 0;
		this.m_items = new Item[this.m_capacity];
	}

	// Token: 0x040004FB RID: 1275
	public int m_capacity;

	// Token: 0x040004FC RID: 1276
	public int m_itemCount;

	// Token: 0x040004FD RID: 1277
	public Item[] m_items;
}
