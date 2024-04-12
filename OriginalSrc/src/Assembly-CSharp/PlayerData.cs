using System;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using Server.Utility;

// Token: 0x020003EB RID: 1003
public struct PlayerData
{
	// Token: 0x04001E43 RID: 7747
	public bool cheater;

	// Token: 0x04001E44 RID: 7748
	public bool developer;

	// Token: 0x04001E45 RID: 7749
	public string playerId;

	// Token: 0x04001E46 RID: 7750
	public string name;

	// Token: 0x04001E47 RID: 7751
	public string tag;

	// Token: 0x04001E48 RID: 7752
	public string gameCenterId;

	// Token: 0x04001E49 RID: 7753
	public string facebookId;

	// Token: 0x04001E4A RID: 7754
	public string ninjaCreationTimestamp;

	// Token: 0x04001E4B RID: 7755
	public string countryCode;

	// Token: 0x04001E4C RID: 7756
	public string youtubeName;

	// Token: 0x04001E4D RID: 7757
	public string youtubeId;

	// Token: 0x04001E4E RID: 7758
	public int youtubeSubscriberCount;

	// Token: 0x04001E4F RID: 7759
	public int itemDbVersion;

	// Token: 0x04001E50 RID: 7760
	public int coins;

	// Token: 0x04001E51 RID: 7761
	public int copper;

	// Token: 0x04001E52 RID: 7762
	public int diamonds;

	// Token: 0x04001E53 RID: 7763
	public int shards;

	// Token: 0x04001E54 RID: 7764
	public int stars;

	// Token: 0x04001E55 RID: 7765
	public int mcBoosters;

	// Token: 0x04001E56 RID: 7766
	public int maxMcBoosters;

	// Token: 0x04001E57 RID: 7767
	public int tournamentBoosters;

	// Token: 0x04001E58 RID: 7768
	public int carBoosters;

	// Token: 0x04001E59 RID: 7769
	public int maxCarBoosters;

	// Token: 0x04001E5A RID: 7770
	public int itemLevel;

	// Token: 0x04001E5B RID: 7771
	public int level;

	// Token: 0x04001E5C RID: 7772
	public int cups;

	// Token: 0x04001E5D RID: 7773
	public int mcRank;

	// Token: 0x04001E5E RID: 7774
	public int carRank;

	// Token: 0x04001E5F RID: 7775
	public int mcTrophies;

	// Token: 0x04001E60 RID: 7776
	public int carTrophies;

	// Token: 0x04001E61 RID: 7777
	public int bigBangPoints;

	// Token: 0x04001E62 RID: 7778
	public int xp;

	// Token: 0x04001E63 RID: 7779
	public float mcHandicap;

	// Token: 0x04001E64 RID: 7780
	public float carHandicap;

	// Token: 0x04001E65 RID: 7781
	public string cardPurchases;

	// Token: 0x04001E66 RID: 7782
	public string gachaData;

	// Token: 0x04001E67 RID: 7783
	public string teamId;

	// Token: 0x04001E68 RID: 7784
	public string teamName;

	// Token: 0x04001E69 RID: 7785
	public TeamRole teamRole;

	// Token: 0x04001E6A RID: 7786
	public string teamRoleName;

	// Token: 0x04001E6B RID: 7787
	public bool hasJoinedTeam;

	// Token: 0x04001E6C RID: 7788
	public int seasonReward;

	// Token: 0x04001E6D RID: 7789
	public string teamKickReason;

	// Token: 0x04001E6E RID: 7790
	public int lastSeasonEndMcTrophies;

	// Token: 0x04001E6F RID: 7791
	public int lastSeasonEndCarTrophies;

	// Token: 0x04001E70 RID: 7792
	public int racesThisSeason;

	// Token: 0x04001E71 RID: 7793
	public string ageGroup;

	// Token: 0x04001E72 RID: 7794
	public string gender;

	// Token: 0x04001E73 RID: 7795
	public bool completedSurvey;

	// Token: 0x04001E74 RID: 7796
	public Hashtable upgrades;

	// Token: 0x04001E75 RID: 7797
	public Hashtable boosters;

	// Token: 0x04001E76 RID: 7798
	public Hashtable data;

	// Token: 0x04001E77 RID: 7799
	public ClientConfig clientConfig;

	// Token: 0x04001E78 RID: 7800
	public bool acceptNotifications;

	// Token: 0x04001E79 RID: 7801
	public Hashtable setData;

	// Token: 0x04001E7A RID: 7802
	public double mcBoosterRefreshTimeLeft;

	// Token: 0x04001E7B RID: 7803
	public double carBoosterRefreshTimeLeft;

	// Token: 0x04001E7C RID: 7804
	public double superLikeRefreshTimeLeft;

	// Token: 0x04001E7D RID: 7805
	public Dictionary<string, ObscuredInt> editorResources;

	// Token: 0x04001E7E RID: 7806
	public List<string> claimedTutorials;

	// Token: 0x04001E7F RID: 7807
	public string hash;

	// Token: 0x04001E80 RID: 7808
	public int publishedMinigameCount;

	// Token: 0x04001E81 RID: 7809
	public int followerCount;

	// Token: 0x04001E82 RID: 7810
	public int totalCoinsEarned;

	// Token: 0x04001E83 RID: 7811
	public int totalLikes;

	// Token: 0x04001E84 RID: 7812
	public int totalSuperLikes;

	// Token: 0x04001E85 RID: 7813
	public int creatorLikes;

	// Token: 0x04001E86 RID: 7814
	public int creatorRankingDelta;

	// Token: 0x04001E87 RID: 7815
	public bool coinDoubler;

	// Token: 0x04001E88 RID: 7816
	public bool dirtBikeBundle;

	// Token: 0x04001E89 RID: 7817
	public List<ObscuredString> trailsPurchased;

	// Token: 0x04001E8A RID: 7818
	public List<ObscuredString> hatsPurchased;

	// Token: 0x04001E8B RID: 7819
	public List<ObscuredString> bundlesPurchased;

	// Token: 0x04001E8C RID: 7820
	public List<GachaType> pendingSpecialOfferChests;

	// Token: 0x04001E8D RID: 7821
	public int adventureLevelsCompleted;

	// Token: 0x04001E8E RID: 7822
	public int racesWon;

	// Token: 0x04001E8F RID: 7823
	public int newLevelsRated;

	// Token: 0x04001E90 RID: 7824
	public bool fbClaimed;

	// Token: 0x04001E91 RID: 7825
	public bool igClaimed;

	// Token: 0x04001E92 RID: 7826
	public bool forumClaimed;

	// Token: 0x04001E93 RID: 7827
	public TeamData teamData;

	// Token: 0x04001E94 RID: 7828
	public int nameChangesDone;
}
