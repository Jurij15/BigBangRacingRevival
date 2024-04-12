using System;

// Token: 0x020002D2 RID: 722
public class LeaderboardAnimation
{
	// Token: 0x06001572 RID: 5490 RVA: 0x000DD8B4 File Offset: 0x000DBCB4
	public LeaderboardAnimation(string _id, int _time, HighscoreDataEntry _newEntry = null)
	{
		this.id = _id;
		this.time = _time;
		this.newEntry = _newEntry;
	}

	// Token: 0x04001827 RID: 6183
	public string id;

	// Token: 0x04001828 RID: 6184
	public int time;

	// Token: 0x04001829 RID: 6185
	public HighscoreDataEntry newEntry;
}
