using System;

// Token: 0x020003F9 RID: 1017
public class SeasonConfig
{
	// Token: 0x06001C62 RID: 7266 RVA: 0x00140C7E File Offset: 0x0013F07E
	public int[] GetRewardAmounts()
	{
		return this.coinRewardAmounts;
	}

	// Token: 0x06001C63 RID: 7267 RVA: 0x00140C86 File Offset: 0x0013F086
	public int GetFinalTierReward()
	{
		return this.finalTierCoinReward;
	}

	// Token: 0x06001C64 RID: 7268 RVA: 0x00140C8E File Offset: 0x0013F08E
	public int[] GetTopTenRewards()
	{
		return this.coinTopTenRewards;
	}

	// Token: 0x04001EE0 RID: 7904
	public int[] rewardLimits;

	// Token: 0x04001EE1 RID: 7905
	public int[] coinTopTenRewards;

	// Token: 0x04001EE2 RID: 7906
	public int[] coinRewardAmounts;

	// Token: 0x04001EE3 RID: 7907
	public int finalTierCoinReward;
}
