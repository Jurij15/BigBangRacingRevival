using System;
using System.Collections;

// Token: 0x020003FE RID: 1022
public class VersusParticipant
{
	// Token: 0x06001C71 RID: 7281 RVA: 0x00140EFC File Offset: 0x0013F2FC
	public Hashtable GenerateParticipantHashTable()
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("score", HighScores.GenerateDataEntryHashtable(this.score));
		hashtable.Add("rank", this.rank);
		hashtable.Add("stars", this.stars);
		hashtable.Add("tries", this.tries);
		return hashtable;
	}

	// Token: 0x04001F08 RID: 7944
	public int rank;

	// Token: 0x04001F09 RID: 7945
	public HighscoreDataEntry score;

	// Token: 0x04001F0A RID: 7946
	public int stars;

	// Token: 0x04001F0B RID: 7947
	public int tries;
}
