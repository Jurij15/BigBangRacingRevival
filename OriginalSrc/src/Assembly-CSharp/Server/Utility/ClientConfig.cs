using System;
using System.Collections.Generic;

namespace Server.Utility
{
	// Token: 0x02000453 RID: 1107
	public class ClientConfig
	{
		// Token: 0x06001EAA RID: 7850 RVA: 0x0015935C File Offset: 0x0015775C
		public ClientConfig()
		{
			this.superLikeRefreshMinutes = 12;
			this.carRefreshMinutes = 6;
			this.freshFreeInterval = 15;
			this.fbConnectReward = 20;
			this.dailyGemAmount = 10;
			this.videoAdCount = 2;
			this.videoAdCoolDown = 3600;
			this.freshFreeCount = 3;
			this.freshFreeCoolDown = 1800;
			this.inRaceDiamondSpawnProbability = 25;
			this.boltsAtStart = 0;
			this.coinsAtStart = 1000;
			this.diamondsAtStart = 50;
			this.keysAtStart = 5;
			this.offerCooldownMinutes = 4320;
			this.offerDurationMinutes = 60;
			this.minimumTournamentNitros = 5;
			this.creatorRank1 = 100;
			this.creatorRank2 = 1000;
			this.creatorRank3 = 10000;
			this.creatorRank4 = 100000;
			this.creatorRank5 = 1000000;
			this.creatorRank6 = 10000000;
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x0015943C File Offset: 0x0015783C
		public ClientConfig(Dictionary<string, object> _dictionary)
		{
			this.ParseFromDictionary(_dictionary);
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x0015944C File Offset: 0x0015784C
		public void ParseFromDictionary(Dictionary<string, object> _dictionary)
		{
			ShopRuns.ParseServerConfig(_dictionary);
			if (_dictionary.ContainsKey("carRefreshMinutes"))
			{
				this.carRefreshMinutes = Convert.ToInt32(_dictionary["carRefreshMinutes"]);
			}
			else
			{
				this.carRefreshMinutes = 6;
			}
			if (_dictionary.ContainsKey("superLikeRefreshHours"))
			{
				this.superLikeRefreshMinutes = Convert.ToInt32(_dictionary["superLikeRefreshHours"]);
			}
			else
			{
				this.superLikeRefreshMinutes = 360;
			}
			if (_dictionary.ContainsKey("freshFreeInterval"))
			{
				this.freshFreeInterval = Convert.ToInt32(_dictionary["freshFreeInterval"]);
			}
			else
			{
				this.freshFreeInterval = 15;
			}
			if (_dictionary.ContainsKey("fbConnectRewardAmount"))
			{
				this.fbConnectReward = Convert.ToInt32(_dictionary["fbConnectRewardAmount"]);
			}
			else
			{
				this.fbConnectReward = 20;
			}
			if (_dictionary.ContainsKey("dailyGemAmount"))
			{
				this.dailyGemAmount = Convert.ToInt32(_dictionary["dailyGemAmount"]);
			}
			else
			{
				this.dailyGemAmount = 10;
			}
			if (_dictionary.ContainsKey("videoAdCount"))
			{
				this.videoAdCount = Convert.ToInt32(_dictionary["videoAdCount"]);
			}
			else
			{
				this.videoAdCount = 2;
			}
			if (_dictionary.ContainsKey("videoAdCoolDown"))
			{
				this.videoAdCoolDown = Convert.ToInt32(_dictionary["videoAdCoolDown"]);
			}
			else
			{
				this.videoAdCoolDown = 3600;
			}
			if (_dictionary.ContainsKey("freshFreeCount"))
			{
				this.freshFreeCount = Convert.ToInt32(_dictionary["freshFreeCount"]);
			}
			else
			{
				this.freshFreeCount = 3;
			}
			if (_dictionary.ContainsKey("freshFreeCoolDown"))
			{
				this.freshFreeCoolDown = Convert.ToInt32(_dictionary["freshFreeCoolDown"]);
			}
			else
			{
				this.freshFreeCoolDown = 1800;
			}
			if (_dictionary.ContainsKey("inRaceDiamondSpawnProbability"))
			{
				this.inRaceDiamondSpawnProbability = Convert.ToInt32(_dictionary["inRaceDiamondSpawnProbability"]);
			}
			else
			{
				this.inRaceDiamondSpawnProbability = 25;
			}
			if (_dictionary.ContainsKey("offerCooldownMinutes"))
			{
				this.offerCooldownMinutes = Convert.ToInt32(_dictionary["offerCooldownMinutes"]);
			}
			else
			{
				this.offerCooldownMinutes = 4320;
			}
			if (_dictionary.ContainsKey("offerDurationMinutes"))
			{
				this.offerDurationMinutes = Convert.ToInt32(_dictionary["offerDurationMinutes"]);
			}
			else
			{
				this.offerDurationMinutes = 60;
			}
			if (_dictionary.ContainsKey("minimumTournamentNitros"))
			{
				this.minimumTournamentNitros = Convert.ToInt32(_dictionary["minimumTournamentNitros"]);
			}
			else
			{
				Debug.LogError("NO minimum tournament nitros found from clientconfig, setting to default 5");
				this.minimumTournamentNitros = 5;
			}
			if (_dictionary.ContainsKey("tournamentYoutuberFollowNitros"))
			{
				this.tournamentYoutuberFollowNitros = Convert.ToInt32(_dictionary["tournamentYoutuberFollowNitros"]);
			}
			else
			{
				Debug.LogError("NO tournamentYoutuberFollowNitros, setting to default 5");
				this.tournamentYoutuberFollowNitros = 5;
			}
			this.creatorRank1 = 100;
			this.creatorRank2 = 1000;
			this.creatorRank3 = 10000;
			this.creatorRank4 = 100000;
			this.creatorRank5 = 1000000;
			this.creatorRank6 = 10000000;
			this.boltsAtStart = 0;
			this.coinsAtStart = 1000;
			this.diamondsAtStart = 50;
			this.keysAtStart = 5;
			if (_dictionary.ContainsKey("minimumRentPrice"))
			{
				PsState.m_minimumRentPrice = Convert.ToInt32(_dictionary["minimumRentPrice"]);
			}
			if (_dictionary.ContainsKey("rentDiamondMultiplier"))
			{
				PsState.m_rentPriceMultiplier = Convert.ToSingle(_dictionary["rentDiamondMultiplier"]);
			}
			if (_dictionary.ContainsKey("versusChallengeTryAmount"))
			{
				PsState.m_versusChallengeTryAmount = Convert.ToInt32(_dictionary["versusChallengeTryAmount"]);
			}
			if (_dictionary.ContainsKey("versusTokenMaxCount"))
			{
				PsState.m_versusTokenMaxCount = Convert.ToInt32(_dictionary["versusTokenMaxCount"]);
			}
			if (_dictionary.ContainsKey("versusRankCap"))
			{
				PsState.m_versusRankCap = Convert.ToInt32(_dictionary["versusRankCap"]);
			}
			if (_dictionary.ContainsKey("fixedFreshMinigameId"))
			{
				PsState.m_fixedFreshMinigameId = Convert.ToString(_dictionary["fixedFreshMinigameId"]);
			}
			if (_dictionary.ContainsKey("fixedFreshMotorcycleMinigameId"))
			{
				PsState.m_fixedFreshMcId = Convert.ToString(_dictionary["fixedFreshMotorcycleMinigameId"]);
			}
			if (_dictionary.ContainsKey("gemShopEnabled"))
			{
				PsState.m_gemShopEnabled = Convert.ToBoolean(_dictionary["gemShopEnabled"]);
			}
		}

		// Token: 0x040021DF RID: 8671
		public int carRefreshMinutes;

		// Token: 0x040021E0 RID: 8672
		public int freshFreeInterval;

		// Token: 0x040021E1 RID: 8673
		public int keysAtStart;

		// Token: 0x040021E2 RID: 8674
		public int diamondsAtStart;

		// Token: 0x040021E3 RID: 8675
		public int coinsAtStart;

		// Token: 0x040021E4 RID: 8676
		public int boltsAtStart;

		// Token: 0x040021E5 RID: 8677
		public int fbConnectReward;

		// Token: 0x040021E6 RID: 8678
		public int dailyGemAmount;

		// Token: 0x040021E7 RID: 8679
		public int videoAdCount;

		// Token: 0x040021E8 RID: 8680
		public int videoAdCoolDown;

		// Token: 0x040021E9 RID: 8681
		public int freshFreeCount;

		// Token: 0x040021EA RID: 8682
		public int freshFreeCoolDown;

		// Token: 0x040021EB RID: 8683
		public int inRaceDiamondSpawnProbability;

		// Token: 0x040021EC RID: 8684
		public int superLikeRefreshMinutes;

		// Token: 0x040021ED RID: 8685
		public int offerCooldownMinutes;

		// Token: 0x040021EE RID: 8686
		public int offerDurationMinutes;

		// Token: 0x040021EF RID: 8687
		public int minimumTournamentNitros;

		// Token: 0x040021F0 RID: 8688
		public int tournamentYoutuberFollowNitros;

		// Token: 0x040021F1 RID: 8689
		public int creatorRank1;

		// Token: 0x040021F2 RID: 8690
		public int creatorRank2;

		// Token: 0x040021F3 RID: 8691
		public int creatorRank3;

		// Token: 0x040021F4 RID: 8692
		public int creatorRank4;

		// Token: 0x040021F5 RID: 8693
		public int creatorRank5;

		// Token: 0x040021F6 RID: 8694
		public int creatorRank6;
	}
}
