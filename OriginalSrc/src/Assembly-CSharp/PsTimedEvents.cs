using System;
using System.Collections.Generic;
using System.Linq;

// Token: 0x02000401 RID: 1025
public class PsTimedEvents
{
	// Token: 0x06001C74 RID: 7284 RVA: 0x00141136 File Offset: 0x0013F536
	public PsTimedEvents()
	{
		this.tournaments = new PsTournaments();
		this.diamondChallenges = new List<PsChallengeMetaData>();
		this.versusChallenges = new List<VersusMetaData>();
		this.friendlyChallenges = new List<VersusMetaData>();
		this.freshAndFree = new List<PsMinigameMetaData>();
	}

	// Token: 0x06001C75 RID: 7285 RVA: 0x00141178 File Offset: 0x0013F578
	public bool HasTournaments()
	{
		return this.tournaments != null && ((this.tournaments.dailyTournaments != null && this.tournaments.dailyTournaments.Count > 0) || (this.tournaments.activeTournaments != null && this.tournaments.activeTournaments.Count > 0) || (this.tournaments.claimableTournaments != null && this.tournaments.claimableTournaments.Count > 0));
	}

	// Token: 0x06001C76 RID: 7286 RVA: 0x0014120B File Offset: 0x0013F60B
	public bool HasDiamondChallenge()
	{
		return this.diamondChallenges != null && Enumerable.Count<PsChallengeMetaData>(this.diamondChallenges) > 0;
	}

	// Token: 0x06001C77 RID: 7287 RVA: 0x0014122C File Offset: 0x0013F62C
	public bool HasVersusChallenge()
	{
		return this.versusChallenges != null && Enumerable.Count<VersusMetaData>(this.versusChallenges) > 0;
	}

	// Token: 0x06001C78 RID: 7288 RVA: 0x0014124D File Offset: 0x0013F64D
	public bool HasFreshAndFree()
	{
		return this.freshAndFree != null && Enumerable.Count<PsMinigameMetaData>(this.freshAndFree) > 0;
	}

	// Token: 0x04001F4C RID: 8012
	public PsTournaments tournaments;

	// Token: 0x04001F4D RID: 8013
	public List<PsChallengeMetaData> diamondChallenges;

	// Token: 0x04001F4E RID: 8014
	public List<VersusMetaData> versusChallenges;

	// Token: 0x04001F4F RID: 8015
	public List<VersusMetaData> friendlyChallenges;

	// Token: 0x04001F50 RID: 8016
	public List<PsMinigameMetaData> freshAndFree;
}
