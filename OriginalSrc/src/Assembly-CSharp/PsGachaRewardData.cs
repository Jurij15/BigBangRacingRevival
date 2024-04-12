using System;

// Token: 0x020000A8 RID: 168
public class PsGachaRewardData
{
	// Token: 0x06000386 RID: 902 RVA: 0x00035A1C File Offset: 0x00033E1C
	public PsGachaRewardData(int _totalUpgradeItemCount, int _minRareUpgradeItemCount, int _minEpicUpgradeItemCount, int _maxDifferendUpgradeItems, int _minCoinReward, int _maxCoinReward, int _totalEditorItemCount, int _minRareEditorItemCount, int _minEpicEditorItemCount, int _maxDifferendEditorItems, int _nitroCount, float _hatProbabilityValue, int _bossHandicap, PsRarity _guaranteedHatRarity, int _maxGemReward = -1)
		: this(_totalUpgradeItemCount, _minRareUpgradeItemCount, _minEpicUpgradeItemCount, _maxDifferendUpgradeItems, _minCoinReward, _maxCoinReward, _totalEditorItemCount, _minRareEditorItemCount, _minEpicEditorItemCount, _maxDifferendEditorItems, _nitroCount, _hatProbabilityValue, _bossHandicap, _maxGemReward)
	{
		this.m_guaranteedHatRarity = _guaranteedHatRarity;
	}

	// Token: 0x06000387 RID: 903 RVA: 0x00035A50 File Offset: 0x00033E50
	public PsGachaRewardData(int _totalUpgradeItemCount, int _minRareUpgradeItemCount, int _minEpicUpgradeItemCount, int _maxDifferendUpgradeItems, int _minCoinReward, int _maxCoinReward, int _totalEditorItemCount, int _minRareEditorItemCount, int _minEpicEditorItemCount, int _maxDifferendEditorItems, int _nitroCount, float _hatProbabilityValue, int _bossHandicap, int _maxGemReward = -1)
	{
		this.m_totalUpgradeItemCount = _totalUpgradeItemCount;
		this.m_minRareUpgradeItemCount = _minRareUpgradeItemCount;
		this.m_minEpicUpgradeItemCount = _minEpicUpgradeItemCount;
		this.m_maxDifferendUpgradeItems = _maxDifferendUpgradeItems;
		this.m_minCoinReward = _minCoinReward;
		this.m_maxCoinReward = _maxCoinReward;
		this.m_maxGemReward = _maxGemReward;
		this.m_totalEditorItemCount = _totalEditorItemCount;
		this.m_minRareEditorItemCount = _minRareEditorItemCount;
		this.m_minEpicEditorItemCount = _minEpicEditorItemCount;
		this.m_maxDifferendEditorItems = _maxDifferendEditorItems;
		this.m_nitroCount = _nitroCount;
		this.m_hatProbabilityValue = _hatProbabilityValue;
		this.m_bossHandicap = _bossHandicap;
	}

	// Token: 0x04000479 RID: 1145
	public int m_totalUpgradeItemCount;

	// Token: 0x0400047A RID: 1146
	public int m_minRareUpgradeItemCount;

	// Token: 0x0400047B RID: 1147
	public int m_minEpicUpgradeItemCount;

	// Token: 0x0400047C RID: 1148
	public int m_maxDifferendUpgradeItems;

	// Token: 0x0400047D RID: 1149
	public int m_minCoinReward;

	// Token: 0x0400047E RID: 1150
	public int m_maxCoinReward;

	// Token: 0x0400047F RID: 1151
	public int m_maxGemReward;

	// Token: 0x04000480 RID: 1152
	public int m_totalEditorItemCount;

	// Token: 0x04000481 RID: 1153
	public int m_minRareEditorItemCount;

	// Token: 0x04000482 RID: 1154
	public int m_minEpicEditorItemCount;

	// Token: 0x04000483 RID: 1155
	public int m_maxDifferendEditorItems;

	// Token: 0x04000484 RID: 1156
	public int m_nitroCount;

	// Token: 0x04000485 RID: 1157
	public float m_hatProbabilityValue;

	// Token: 0x04000486 RID: 1158
	public PsRarity m_guaranteedHatRarity;

	// Token: 0x04000487 RID: 1159
	public int m_bossHandicap;
}
