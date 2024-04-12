using System;
using System.Collections.Generic;

// Token: 0x020000E8 RID: 232
public class PsUpgradeData
{
	// Token: 0x0600050D RID: 1293 RVA: 0x00042A8F File Offset: 0x00040E8F
	public PsUpgradeData(float _baseEfficiency, int _tierCount, float[] _tierLimits, params PsUpgradeItem[] _upgradeItems)
	{
		this.m_tierCount = _tierCount;
		this.m_basePerformance = _baseEfficiency;
		this.m_tierLimits = _tierLimits;
		this.m_upgradeItems = _upgradeItems;
		this.m_typeDatas = new Dictionary<PsUpgradeManager.UpgradeType, PsUpgradeData.TypeData>();
		this.UpdateValues();
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x00042AC8 File Offset: 0x00040EC8
	public void UpdateValues()
	{
		this.m_typeDatas.Clear();
		this.m_maxPerformance = 0f;
		this.m_maxPerformance += this.m_basePerformance;
		this.m_currentPerformance = 0f;
		for (int i = 0; i < this.m_upgradeItems.Length; i++)
		{
			PsUpgradeData.TypeData typeData;
			if (this.m_typeDatas.TryGetValue(this.m_upgradeItems[i].m_upgradeType, ref typeData))
			{
				typeData.m_maxEfficiency += this.m_upgradeItems[i].m_maxEfficiency;
				typeData.m_currentEfficiency += this.m_upgradeItems[i].m_currentEfficiency;
				if (!this.m_upgradeItems[i].m_powerUpItem)
				{
					this.m_maxPerformance += this.m_upgradeItems[i].m_maxEfficiency;
					this.m_currentPerformance += this.m_upgradeItems[i].m_currentEfficiency;
				}
				else
				{
					this.m_maxPerformance += (float)(this.m_upgradeItems[i].GetPowerUpItemPerformance() * this.m_upgradeItems[i].m_maxLevel);
					this.m_currentPerformance += (float)(this.m_upgradeItems[i].GetPowerUpItemPerformance() * this.m_upgradeItems[i].m_currentLevel);
				}
				this.m_typeDatas[this.m_upgradeItems[i].m_upgradeType] = typeData;
			}
			else
			{
				typeData = default(PsUpgradeData.TypeData);
				typeData.m_upgradeType = this.m_upgradeItems[i].m_upgradeType;
				typeData.m_maxEfficiency = this.m_upgradeItems[i].m_maxEfficiency;
				typeData.m_currentEfficiency = this.m_upgradeItems[i].m_currentEfficiency;
				if (!this.m_upgradeItems[i].m_powerUpItem)
				{
					this.m_maxPerformance += this.m_upgradeItems[i].m_maxEfficiency;
					this.m_currentPerformance += this.m_upgradeItems[i].m_currentEfficiency;
				}
				else
				{
					this.m_maxPerformance += (float)(this.m_upgradeItems[i].GetPowerUpItemPerformance() * this.m_upgradeItems[i].m_maxLevel);
					this.m_currentPerformance += (float)(this.m_upgradeItems[i].GetPowerUpItemPerformance() * this.m_upgradeItems[i].m_currentLevel);
				}
				this.m_typeDatas.Add(this.m_upgradeItems[i].m_upgradeType, typeData);
			}
		}
	}

	// Token: 0x0600050F RID: 1295 RVA: 0x00042D38 File Offset: 0x00041138
	public PsUpgradeItem[] GetUpgradeItemsByTier(int _tier)
	{
		List<PsUpgradeItem> list = new List<PsUpgradeItem>();
		for (int i = 0; i < this.m_upgradeItems.Length; i++)
		{
			if (this.m_upgradeItems[i].m_tier == _tier)
			{
				list.Add(this.m_upgradeItems[i]);
			}
		}
		return list.ToArray();
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x00042D8C File Offset: 0x0004118C
	public PsUpgradeItem GetUpgradeItemByIdentifier(string _identifier)
	{
		for (int i = 0; i < this.m_upgradeItems.Length; i++)
		{
			if (this.m_upgradeItems[i].m_identifier == _identifier)
			{
				return this.m_upgradeItems[i];
			}
		}
		return null;
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x00042DD4 File Offset: 0x000411D4
	public PsUpgradeManager.UpgradeType[] GetUpgradeTypes()
	{
		PsUpgradeManager.UpgradeType[] array = new PsUpgradeManager.UpgradeType[this.m_typeDatas.Keys.Count];
		this.m_typeDatas.Keys.CopyTo(array, 0);
		return array;
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x00042E0C File Offset: 0x0004120C
	public int GetPowerUpItemsCurrentPerformance()
	{
		int num = 0;
		for (int i = 0; i < this.m_upgradeItems.Length; i++)
		{
			if (this.m_upgradeItems[i].m_powerUpItem)
			{
				num += this.m_upgradeItems[i].GetPowerUpItemPerformance() * this.m_upgradeItems[i].m_currentLevel;
			}
		}
		return num;
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x00042E6A File Offset: 0x0004126A
	public float GetTierLimit(int _tier)
	{
		return this.m_tierLimits[_tier - 1];
	}

	// Token: 0x04000668 RID: 1640
	public int m_tierCount;

	// Token: 0x04000669 RID: 1641
	public float[] m_tierLimits;

	// Token: 0x0400066A RID: 1642
	public PsUpgradeItem[] m_upgradeItems;

	// Token: 0x0400066B RID: 1643
	public float m_basePerformance;

	// Token: 0x0400066C RID: 1644
	public float m_currentPerformance;

	// Token: 0x0400066D RID: 1645
	public float m_maxPerformance;

	// Token: 0x0400066E RID: 1646
	public Dictionary<PsUpgradeManager.UpgradeType, PsUpgradeData.TypeData> m_typeDatas;

	// Token: 0x020000E9 RID: 233
	public struct TypeData
	{
		// Token: 0x0400066F RID: 1647
		public PsUpgradeManager.UpgradeType m_upgradeType;

		// Token: 0x04000670 RID: 1648
		public float m_maxEfficiency;

		// Token: 0x04000671 RID: 1649
		public float m_currentEfficiency;
	}
}
