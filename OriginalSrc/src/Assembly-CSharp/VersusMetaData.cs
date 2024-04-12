using System;
using System.Linq;

// Token: 0x020003FD RID: 1021
public class VersusMetaData
{
	// Token: 0x06001C6E RID: 7278 RVA: 0x00140E0C File Offset: 0x0013F20C
	public VersusParticipant GetOwnInfo()
	{
		if (this.participants != null)
		{
			for (int i = 0; i < Enumerable.Count<VersusParticipant>(this.participants); i++)
			{
				if (this.participants[i].score != null && this.participants[i].score.playerId == PlayerPrefsX.GetUserId())
				{
					return this.participants[i];
				}
			}
		}
		return null;
	}

	// Token: 0x06001C6F RID: 7279 RVA: 0x00140E80 File Offset: 0x0013F280
	public VersusParticipant GetOpponentInfo()
	{
		if (this.participants != null)
		{
			for (int i = 0; i < Enumerable.Count<VersusParticipant>(this.participants); i++)
			{
				if (this.participants[i].score != null && this.participants[i].score.playerId != PlayerPrefsX.GetUserId())
				{
					return this.participants[i];
				}
			}
		}
		return null;
	}

	// Token: 0x04001EFC RID: 7932
	public string timedEventId;

	// Token: 0x04001EFD RID: 7933
	public string gameId;

	// Token: 0x04001EFE RID: 7934
	public int timeLeft;

	// Token: 0x04001EFF RID: 7935
	public string currentPlayer;

	// Token: 0x04001F00 RID: 7936
	public string winner;

	// Token: 0x04001F01 RID: 7937
	public int round;

	// Token: 0x04001F02 RID: 7938
	public double timeLeftSetTime;

	// Token: 0x04001F03 RID: 7939
	public VersusParticipant[] participants;

	// Token: 0x04001F04 RID: 7940
	public PsMinigameMetaData gameMetaData;

	// Token: 0x04001F05 RID: 7941
	public bool matchBall;

	// Token: 0x04001F06 RID: 7942
	public string challengedPlayer;

	// Token: 0x04001F07 RID: 7943
	public string status;
}
