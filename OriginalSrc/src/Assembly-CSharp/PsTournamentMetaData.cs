using System;
using System.Linq;

// Token: 0x02000403 RID: 1027
public class PsTournamentMetaData
{
	// Token: 0x06001C7B RID: 7291 RVA: 0x00141288 File Offset: 0x0013F688
	public int GetReward()
	{
		int position = this.GetPosition();
		if (position >= 1 && position <= 3)
		{
			return this.topThreeRewards[position - 1];
		}
		return this.GetPercentile(0).reward;
	}

	// Token: 0x06001C7C RID: 7292 RVA: 0x001412C4 File Offset: 0x0013F6C4
	public int GetPosition()
	{
		for (int i = 0; i < this.topScores.Length; i++)
		{
			if (this.topScores[i].time == this.time)
			{
				return i + 1;
			}
		}
		return 0;
	}

	// Token: 0x06001C7D RID: 7293 RVA: 0x00141308 File Offset: 0x0013F708
	public PercentileField GetPercentile(int _returnShift = 0)
	{
		if (this.percentiles.Length == 0)
		{
			return new PercentileField
			{
				reward = 0,
				percentile = 0,
				index = 1
			};
		}
		if (this.participantCount > 9)
		{
			for (int i = 0; i < this.percentiles.Length; i++)
			{
				if (this.time != 0 && (this.time <= this.percentiles[i].percentile || i == 9))
				{
					return this.percentiles[i + _returnShift];
				}
			}
		}
		else
		{
			for (int j = 0; j < this.topScores.Length; j++)
			{
				if (this.time != 0 && this.time <= this.topScores[j].time)
				{
					return this.percentiles[j + _returnShift];
				}
			}
		}
		return Enumerable.Last<PercentileField>(this.percentiles);
	}

	// Token: 0x06001C7E RID: 7294 RVA: 0x001413F4 File Offset: 0x0013F7F4
	public int GetNextBestPosition()
	{
		int position = this.GetPosition();
		if (position == 1)
		{
			return -1;
		}
		if (position > 1 && position <= 4)
		{
			return position - 1;
		}
		if (this.percentiles[8].percentile == 0 && position != 0)
		{
			return position - 1;
		}
		return -1;
	}

	// Token: 0x06001C7F RID: 7295 RVA: 0x00141440 File Offset: 0x0013F840
	public int GetNextBestRewardTime()
	{
		int position = this.GetPosition();
		if (position == 1)
		{
			return -1;
		}
		if (position > 1 && position <= 4)
		{
			return this.topScores[position - 2].time;
		}
		if (this.percentiles[8].percentile == 0 && position != 0)
		{
			return this.topScores[position - 2].time;
		}
		Debug.Log("REWARD TIME: " + this.GetPercentile(-1).percentile, null);
		return this.GetPercentile(-1).percentile;
	}

	// Token: 0x06001C80 RID: 7296 RVA: 0x001414D0 File Offset: 0x0013F8D0
	public int GetNextBestReward()
	{
		int position = this.GetPosition();
		if (position == 1)
		{
			return -1;
		}
		if (position > 1 && position <= 4)
		{
			return this.topThreeRewards[position - 2];
		}
		Debug.Log("REWARD: " + this.GetPercentile(-1).reward, null);
		return this.GetPercentile(-1).reward;
	}

	// Token: 0x04001F55 RID: 8021
	public bool waitingForRewards;

	// Token: 0x04001F56 RID: 8022
	public string tournamentId;

	// Token: 0x04001F57 RID: 8023
	public int timeLeft;

	// Token: 0x04001F58 RID: 8024
	public int duration;

	// Token: 0x04001F59 RID: 8025
	public int playerRewardCoins;

	// Token: 0x04001F5A RID: 8026
	public int playerRewardDiamonds;

	// Token: 0x04001F5B RID: 8027
	public int participationCoinsCost;

	// Token: 0x04001F5C RID: 8028
	public int participationDiamondsCost;

	// Token: 0x04001F5D RID: 8029
	public bool participated;

	// Token: 0x04001F5E RID: 8030
	public bool daily;

	// Token: 0x04001F5F RID: 8031
	public string name;

	// Token: 0x04001F60 RID: 8032
	public int time;

	// Token: 0x04001F61 RID: 8033
	public double timeLeftSetTime;

	// Token: 0x04001F62 RID: 8034
	public bool claimed;

	// Token: 0x04001F63 RID: 8035
	public int participantCount;

	// Token: 0x04001F64 RID: 8036
	public string gameId;

	// Token: 0x04001F65 RID: 8037
	public string gameName;

	// Token: 0x04001F66 RID: 8038
	public PsGameDifficulty difficulty;

	// Token: 0x04001F67 RID: 8039
	public string creatorName;

	// Token: 0x04001F68 RID: 8040
	public string creatorId;

	// Token: 0x04001F69 RID: 8041
	public string playerUnit;

	// Token: 0x04001F6A RID: 8042
	public PsRating rating = PsRating.Unrated;

	// Token: 0x04001F6B RID: 8043
	public string creatorFacebookId;

	// Token: 0x04001F6C RID: 8044
	public string creatorGameCenterId;

	// Token: 0x04001F6D RID: 8045
	public string gameDescription;

	// Token: 0x04001F6E RID: 8046
	public PercentileField[] percentiles;

	// Token: 0x04001F6F RID: 8047
	public int[] topThreeRewards;

	// Token: 0x04001F70 RID: 8048
	public HighscoreDataEntry[] topScores;

	// Token: 0x04001F71 RID: 8049
	public int totalPot;
}
