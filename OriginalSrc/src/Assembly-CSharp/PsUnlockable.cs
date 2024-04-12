using System;

// Token: 0x02000135 RID: 309
public class PsUnlockable
{
	// Token: 0x06000962 RID: 2402 RVA: 0x00064CAC File Offset: 0x000630AC
	public PsUnlockable(PsUnlockableType _itemType, string _name)
	{
		this.m_type = _itemType;
		this.m_name = _name;
	}

	// Token: 0x040008C3 RID: 2243
	public PsUnlockableType m_type;

	// Token: 0x040008C4 RID: 2244
	public PsUnlockable m_container;

	// Token: 0x040008C5 RID: 2245
	public string m_identifier;

	// Token: 0x040008C6 RID: 2246
	public string m_name;

	// Token: 0x040008C7 RID: 2247
	public string m_description;

	// Token: 0x040008C8 RID: 2248
	public string m_iconImage;

	// Token: 0x040008C9 RID: 2249
	public int m_itemLevel;

	// Token: 0x040008CA RID: 2250
	public bool m_unlocked;

	// Token: 0x040008CB RID: 2251
	public string m_tutorialIdentifier;
}
