using System;

// Token: 0x020003F0 RID: 1008
public class SeasonEndData
{
	// Token: 0x06001C5D RID: 7261 RVA: 0x00140C2D File Offset: 0x0013F02D
	public bool hasSeasonEnded()
	{
		return this.currentSeason != null && this.latestSeason != null && this.currentSeason.number > this.latestSeason.number;
	}

	// Token: 0x06001C5E RID: 7262 RVA: 0x00140C60 File Offset: 0x0013F060
	public string GetCurrentSeasonRewardIcon()
	{
		return "menu_resources_coin_icon";
	}

	// Token: 0x06001C5F RID: 7263 RVA: 0x00140C67 File Offset: 0x0013F067
	public string GetLatestSeasonRewardIcon()
	{
		return "menu_resources_coin_icon";
	}

	// Token: 0x04001EAD RID: 7853
	public Season currentSeason;

	// Token: 0x04001EAE RID: 7854
	public Season latestSeason;

	// Token: 0x04001EAF RID: 7855
	public int bigBangPoints;

	// Token: 0x04001EB0 RID: 7856
	public int mcTrophies;

	// Token: 0x04001EB1 RID: 7857
	public int carTrophies;

	// Token: 0x04001EB2 RID: 7858
	public int seasonTeamReward;

	// Token: 0x04001EB3 RID: 7859
	public bool teamEligibleForRewards;
}
