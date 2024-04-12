using System;
using System.Collections.Generic;

// Token: 0x020000D7 RID: 215
public class PsCustomisationItem
{
	// Token: 0x060004A0 RID: 1184 RVA: 0x0003949C File Offset: 0x0003789C
	public PsCustomisationItem(string _identifier, string _iapIdentifier, PsCustomisationManager.CustomisationCategory _category, StringID _title, string _description, string _iconName, PsRarity _rarity, string[] _requiredAchievements = null, bool _unlockedByDefault = false)
	{
		this.m_identifier = _identifier;
		this.m_iapIdentifier = _iapIdentifier;
		this.m_category = _category;
		this.m_title = _title;
		this.m_description = _description;
		this.m_iconName = _iconName;
		this.m_installed = false;
		this.m_unlocked = _unlockedByDefault;
		this.m_rarity = _rarity;
		if (_requiredAchievements == null)
		{
			this.m_requiredAchievements = new List<string>();
		}
		else
		{
			this.m_requiredAchievements = new List<string>(_requiredAchievements);
		}
	}

	// Token: 0x040005F6 RID: 1526
	public string m_identifier;

	// Token: 0x040005F7 RID: 1527
	public string m_iapIdentifier;

	// Token: 0x040005F8 RID: 1528
	public PsCustomisationManager.CustomisationCategory m_category;

	// Token: 0x040005F9 RID: 1529
	public StringID m_title;

	// Token: 0x040005FA RID: 1530
	public string m_description;

	// Token: 0x040005FB RID: 1531
	public string m_iconName;

	// Token: 0x040005FC RID: 1532
	public bool m_installed;

	// Token: 0x040005FD RID: 1533
	public bool m_unlocked;

	// Token: 0x040005FE RID: 1534
	public PsRarity m_rarity;

	// Token: 0x040005FF RID: 1535
	public List<string> m_requiredAchievements;
}
