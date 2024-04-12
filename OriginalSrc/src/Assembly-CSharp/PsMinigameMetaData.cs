using System;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;

// Token: 0x02000400 RID: 1024
public class PsMinigameMetaData
{
	// Token: 0x06001C73 RID: 7283 RVA: 0x00140FA0 File Offset: 0x0013F3A0
	public void DebugPrint()
	{
		Debug.Log("MINIGAME METADATA: " + this.name, null);
		Debug.Log("ID: " + this.id, null);
		Debug.Log("CREATOR NAME: " + this.creatorName, null);
		Debug.Log("PUBLISHED: " + this.published, null);
		Debug.Log("TIME SPENT EDITING:" + this.timeSpentEditing, null);
		Debug.Log("TIMES PLAYED: " + this.timesPlayed, null);
		Debug.Log("TIMES LIKED: " + this.timesLiked, null);
		Debug.Log("CLIENT VERSION: " + this.clientVersion, null);
		Debug.Log("GAME MODE: " + this.gameMode, null);
		Debug.Log("COMPLEXITY: " + this.complexity, null);
		Debug.Log("LEVEL REQUIREMENT: " + this.levelRequirement, null);
		Debug.Log("PLAYER UNIT: " + this.playerUnit, null);
		if (this.itemsUsed != null)
		{
			for (int i = 0; i < this.itemsUsed.Length; i++)
			{
				Debug.Log(string.Concat(new object[]
				{
					"ITEMS USED ",
					i,
					": ",
					this.itemsUsed[i]
				}), null);
			}
		}
	}

	// Token: 0x04001F14 RID: 7956
	public string id;

	// Token: 0x04001F15 RID: 7957
	public string name = "Unnamed Track";

	// Token: 0x04001F16 RID: 7958
	public string creatorName;

	// Token: 0x04001F17 RID: 7959
	public string creatorId;

	// Token: 0x04001F18 RID: 7960
	public string creatorFacebookId;

	// Token: 0x04001F19 RID: 7961
	public string creatorGameCenterId;

	// Token: 0x04001F1A RID: 7962
	public string creatorCountryCode;

	// Token: 0x04001F1B RID: 7963
	public string videoUrl;

	// Token: 0x04001F1C RID: 7964
	public bool published;

	// Token: 0x04001F1D RID: 7965
	public int timesPlayed;

	// Token: 0x04001F1E RID: 7966
	public int timesLiked;

	// Token: 0x04001F1F RID: 7967
	public int timesRated;

	// Token: 0x04001F20 RID: 7968
	public int timesSuperLiked;

	// Token: 0x04001F21 RID: 7969
	public int upThumbs;

	// Token: 0x04001F22 RID: 7970
	public int downThumbs;

	// Token: 0x04001F23 RID: 7971
	public int timesAbused;

	// Token: 0x04001F24 RID: 7972
	public int timeSpentEditing;

	// Token: 0x04001F25 RID: 7973
	public string description = "No Description";

	// Token: 0x04001F26 RID: 7974
	public int clientVersion;

	// Token: 0x04001F27 RID: 7975
	public PsGameDifficulty difficulty;

	// Token: 0x04001F28 RID: 7976
	public PsGameMode gameMode = PsGameMode.Race;

	// Token: 0x04001F29 RID: 7977
	public PsRating rating = PsRating.Unrated;

	// Token: 0x04001F2A RID: 7978
	public bool played;

	// Token: 0x04001F2B RID: 7979
	public int score;

	// Token: 0x04001F2C RID: 7980
	public int rewardCoins;

	// Token: 0x04001F2D RID: 7981
	public int totalCoinsEarned;

	// Token: 0x04001F2E RID: 7982
	public int complexity;

	// Token: 0x04001F2F RID: 7983
	public int levelRequirement;

	// Token: 0x04001F30 RID: 7984
	public string playerUnit;

	// Token: 0x04001F31 RID: 7985
	public string[] itemsUsed;

	// Token: 0x04001F32 RID: 7986
	public Dictionary<string, ObscuredInt> itemsCount;

	// Token: 0x04001F33 RID: 7987
	public string researchIdentifier;

	// Token: 0x04001F34 RID: 7988
	public int bestTime;

	// Token: 0x04001F35 RID: 7989
	public int medianTime;

	// Token: 0x04001F36 RID: 7990
	public int totalWinners;

	// Token: 0x04001F37 RID: 7991
	public int timeScore;

	// Token: 0x04001F38 RID: 7992
	public int oneStarWinners;

	// Token: 0x04001F39 RID: 7993
	public int twoStarWinners;

	// Token: 0x04001F3A RID: 7994
	public int threeStarWinners;

	// Token: 0x04001F3B RID: 7995
	public int totalPlayers;

	// Token: 0x04001F3C RID: 7996
	public Hashtable creatorUpgrades;

	// Token: 0x04001F3D RID: 7997
	public float quality;

	// Token: 0x04001F3E RID: 7998
	public float overrideCC = -1f;

	// Token: 0x04001F3F RID: 7999
	public bool template;

	// Token: 0x04001F40 RID: 8000
	public bool hidden;

	// Token: 0x04001F41 RID: 8001
	public const string TIME_SPENT_IN_EDIT_MODE = "timeSpentInEditMode";

	// Token: 0x04001F42 RID: 8002
	public const string EDIT_SESSION_COUNT = "editSessionCount";

	// Token: 0x04001F43 RID: 8003
	public const string ITEMS_MODIFICATION_COUNT = "itemsModificationCount";

	// Token: 0x04001F44 RID: 8004
	public const string GROUNDS_MODIFICATION_COUNT = "groundsModificationCount";

	// Token: 0x04001F45 RID: 8005
	public const string LAST_PLAY_SESSION_START_COUNT = "lastPlaySessionStartCount";

	// Token: 0x04001F46 RID: 8006
	public int timeSpentInEditMode;

	// Token: 0x04001F47 RID: 8007
	public int editSessionCount;

	// Token: 0x04001F48 RID: 8008
	public int itemsModificationCount;

	// Token: 0x04001F49 RID: 8009
	public int groundsModificationCount;

	// Token: 0x04001F4A RID: 8010
	public int lastPlaySessionStartCount;

	// Token: 0x04001F4B RID: 8011
	public PsMinigameServerState m_state;
}
