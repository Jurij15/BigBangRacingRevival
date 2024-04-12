using System;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

// Token: 0x020000EA RID: 234
public class PsUpgradeItem
{
	// Token: 0x06000514 RID: 1300 RVA: 0x00042E78 File Offset: 0x00041278
	public PsUpgradeItem(string _identifier, int _tier, StringID _title, StringID _description, StringID _typeName, string _typeColor, int _powerNumber, string _iconName, ObscuredInt[] _priceList, float[] _efficiencyList, int[] _resourceRequrements, PsUpgradeManager.UpgradeType _upgradeType, bool _powerUpItem, PsRarity _rarity)
	{
		this.m_identifier = _identifier;
		this.m_tier = _tier;
		this.m_title = _title;
		this.m_description = _description;
		this.m_typeName = _typeName;
		this.m_typeColor = _typeColor;
		this.m_powerNumber = _powerNumber;
		this.m_iconName = _iconName;
		this.m_priceList = _priceList;
		this.m_efficiencyList = _efficiencyList;
		this.m_resourceRequrements = _resourceRequrements;
		this.m_upgradeType = _upgradeType;
		this.m_powerUpItem = _powerUpItem;
		this.m_maxLevel = this.m_priceList.Length;
		this.m_rarity = _rarity;
		if (this.m_priceList.Length != this.m_efficiencyList.Length || this.m_priceList.Length != this.m_resourceRequrements.Length)
		{
			Debug.LogError("PsUpgadeItem initialize error. Item: " + _identifier);
		}
		for (int i = 0; i < this.m_maxLevel; i++)
		{
			this.m_maxEfficiency += this.m_efficiencyList[i];
		}
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x06000515 RID: 1301 RVA: 0x00042F6C File Offset: 0x0004136C
	public ObscuredInt m_nextLevelPrice
	{
		get
		{
			if (this.m_currentLevel >= this.m_maxLevel)
			{
				return 0;
			}
			EventGiftTimedUpgradeInstallDiscount activeEventGift = PsMetagameManager.GetActiveEventGift<EventGiftTimedUpgradeInstallDiscount>();
			if (activeEventGift != null)
			{
				return (int)((float)this.m_priceList[this.m_currentLevel] * activeEventGift.GetPriceMultiplier());
			}
			return this.m_priceList[this.m_currentLevel];
		}
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x06000516 RID: 1302 RVA: 0x00042FEC File Offset: 0x000413EC
	public float m_nextLevelEfficiency
	{
		get
		{
			if (this.m_currentLevel < this.m_maxLevel)
			{
				return this.m_efficiencyList[this.m_currentLevel];
			}
			return 0f;
		}
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x06000517 RID: 1303 RVA: 0x0004301C File Offset: 0x0004141C
	public int m_nextLevelResourceRequrement
	{
		get
		{
			if (this.m_currentLevel < this.m_maxLevel)
			{
				return this.m_resourceRequrements[this.m_currentLevel];
			}
			return 0;
		}
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x06000518 RID: 1304 RVA: 0x00043048 File Offset: 0x00041448
	public int m_resourcesNeededToMaxLevel
	{
		get
		{
			int num = 0;
			for (int i = this.m_currentLevel; i < this.m_resourceRequrements.Length; i++)
			{
				num += this.m_resourceRequrements[i];
			}
			return num;
		}
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x00043088 File Offset: 0x00041488
	public void SetCurrentLevel(int _currentLevel)
	{
		this.m_currentLevel = Mathf.Clamp(_currentLevel, 0, this.m_maxLevel);
		this.m_currentEfficiency = 0f;
		for (int i = 0; i < this.m_currentLevel; i++)
		{
			this.m_currentEfficiency += this.m_efficiencyList[i];
		}
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x000430EC File Offset: 0x000414EC
	public bool LevelUp()
	{
		if (this.m_currentLevel < this.m_maxLevel)
		{
			this.m_currentEfficiency += this.m_efficiencyList[this.m_currentLevel];
			this.m_currentLevel = ObscuredInt.op_Increment(this.m_currentLevel);
			return true;
		}
		return false;
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x00043144 File Offset: 0x00041544
	public int GetPowerUpItemPerformance()
	{
		int num = 0;
		PsRarity rarity = this.m_rarity;
		if (rarity != PsRarity.Common)
		{
			if (rarity != PsRarity.Rare)
			{
				if (rarity == PsRarity.Epic)
				{
					num = 7;
				}
			}
			else
			{
				num = 5;
			}
		}
		else
		{
			num = 3;
		}
		return num;
	}

	// Token: 0x04000672 RID: 1650
	public string m_identifier;

	// Token: 0x04000673 RID: 1651
	public int m_tier;

	// Token: 0x04000674 RID: 1652
	public StringID m_title;

	// Token: 0x04000675 RID: 1653
	public StringID m_description;

	// Token: 0x04000676 RID: 1654
	public string m_iconName;

	// Token: 0x04000677 RID: 1655
	private ObscuredInt[] m_priceList;

	// Token: 0x04000678 RID: 1656
	private float[] m_efficiencyList;

	// Token: 0x04000679 RID: 1657
	private int[] m_resourceRequrements;

	// Token: 0x0400067A RID: 1658
	public float m_maxEfficiency;

	// Token: 0x0400067B RID: 1659
	public float m_currentEfficiency;

	// Token: 0x0400067C RID: 1660
	public int m_maxLevel;

	// Token: 0x0400067D RID: 1661
	public ObscuredInt m_currentLevel;

	// Token: 0x0400067E RID: 1662
	public PsUpgradeManager.UpgradeType m_upgradeType;

	// Token: 0x0400067F RID: 1663
	public bool m_powerUpItem;

	// Token: 0x04000680 RID: 1664
	public PsRarity m_rarity;

	// Token: 0x04000681 RID: 1665
	public StringID m_typeName;

	// Token: 0x04000682 RID: 1666
	public string m_typeColor;

	// Token: 0x04000683 RID: 1667
	public int m_powerNumber;
}
