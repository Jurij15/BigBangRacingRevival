using System;
using System.Collections.Generic;

// Token: 0x020000D6 RID: 214
public class PsCustomisationData
{
	// Token: 0x06000495 RID: 1173 RVA: 0x000390DF File Offset: 0x000374DF
	public PsCustomisationData(PsCustomisationItem[] _customisationItems)
	{
		this.m_customisationItems = _customisationItems;
	}

	// Token: 0x06000496 RID: 1174 RVA: 0x000390F0 File Offset: 0x000374F0
	public PsCustomisationItem GetItemByIdentifier(string _identifier)
	{
		for (int i = 0; i < this.m_customisationItems.Length; i++)
		{
			if (this.m_customisationItems[i].m_identifier == _identifier)
			{
				return this.m_customisationItems[i];
			}
		}
		return null;
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x00039138 File Offset: 0x00037538
	public List<PsCustomisationItem> GetItemsByCategory(PsCustomisationManager.CustomisationCategory _category)
	{
		List<PsCustomisationItem> list = new List<PsCustomisationItem>();
		for (int i = 0; i < this.m_customisationItems.Length; i++)
		{
			if (this.m_customisationItems[i].m_category == _category)
			{
				list.Add(this.m_customisationItems[i]);
			}
		}
		return list;
	}

	// Token: 0x06000498 RID: 1176 RVA: 0x00039188 File Offset: 0x00037588
	public List<PsCustomisationItem> GetInstalledItems()
	{
		List<PsCustomisationItem> list = new List<PsCustomisationItem>();
		for (int i = 0; i < this.m_customisationItems.Length; i++)
		{
			if (this.m_customisationItems[i].m_installed)
			{
				list.Add(this.m_customisationItems[i]);
			}
		}
		return list;
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x000391D8 File Offset: 0x000375D8
	public List<string> GetInstalledItemIdentifiers()
	{
		List<string> list = new List<string>();
		for (int i = 0; i < this.m_customisationItems.Length; i++)
		{
			if (this.m_customisationItems[i].m_installed)
			{
				list.Add(this.m_customisationItems[i].m_identifier);
			}
		}
		return list;
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x0003922C File Offset: 0x0003762C
	public List<string> GetUnlockedItemIdentifiers()
	{
		List<string> list = new List<string>();
		for (int i = 0; i < this.m_customisationItems.Length; i++)
		{
			if (this.m_customisationItems[i].m_unlocked)
			{
				list.Add(this.m_customisationItems[i].m_identifier);
			}
		}
		return list;
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x00039280 File Offset: 0x00037680
	public Dictionary<string, bool> GetUnlockAndInstallationData()
	{
		Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
		for (int i = 0; i < this.m_customisationItems.Length; i++)
		{
			if (this.m_customisationItems[i].m_unlocked)
			{
				if (this.m_customisationItems[i].m_installed)
				{
					dictionary.Add(this.m_customisationItems[i].m_identifier, true);
				}
				else
				{
					dictionary.Add(this.m_customisationItems[i].m_identifier, false);
				}
			}
		}
		return dictionary;
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x00039300 File Offset: 0x00037700
	public PsCustomisationItem GetInstalledItemByCategory(PsCustomisationManager.CustomisationCategory _category)
	{
		for (int i = 0; i < this.m_customisationItems.Length; i++)
		{
			if (this.m_customisationItems[i].m_installed && this.m_customisationItems[i].m_category == _category)
			{
				return this.m_customisationItems[i];
			}
		}
		return null;
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x00039358 File Offset: 0x00037758
	public List<PsCustomisationItem> GetUnlockedItemsByCategory(PsCustomisationManager.CustomisationCategory _category)
	{
		List<PsCustomisationItem> list = new List<PsCustomisationItem>();
		for (int i = 0; i < this.m_customisationItems.Length; i++)
		{
			if (this.m_customisationItems[i].m_unlocked && this.m_customisationItems[i].m_category == _category)
			{
				list.Add(this.m_customisationItems[i]);
			}
		}
		return list;
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x000393B8 File Offset: 0x000377B8
	public List<PsCustomisationItem> GetLockedItemsByCategory(PsCustomisationManager.CustomisationCategory _category)
	{
		List<PsCustomisationItem> list = new List<PsCustomisationItem>();
		for (int i = 0; i < this.m_customisationItems.Length; i++)
		{
			if (!this.m_customisationItems[i].m_unlocked && this.m_customisationItems[i].m_category == _category)
			{
				list.Add(this.m_customisationItems[i]);
			}
		}
		return list;
	}

	// Token: 0x0600049F RID: 1183 RVA: 0x00039418 File Offset: 0x00037818
	public List<PsCustomisationItem> GetCustomizationItemsByAchievement(string _achievementId)
	{
		List<PsCustomisationItem> list = new List<PsCustomisationItem>();
		for (int i = 0; i < this.m_customisationItems.Length; i++)
		{
			for (int j = 0; j < this.m_customisationItems[i].m_requiredAchievements.Count; j++)
			{
				if (this.m_customisationItems[i].m_requiredAchievements[j] == _achievementId)
				{
					list.Add(this.m_customisationItems[i]);
					break;
				}
			}
		}
		return list;
	}

	// Token: 0x040005F5 RID: 1525
	public PsCustomisationItem[] m_customisationItems;
}
