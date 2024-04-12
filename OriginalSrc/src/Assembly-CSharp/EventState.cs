using System;
using System.Collections.Generic;

// Token: 0x02000407 RID: 1031
public class EventState
{
	// Token: 0x04001FAA RID: 8106
	public int id;

	// Token: 0x04001FAB RID: 8107
	public string title;

	// Token: 0x04001FAC RID: 8108
	public string description;

	// Token: 0x04001FAD RID: 8109
	public long timeLeft;

	// Token: 0x04001FAE RID: 8110
	public long localEndTime;

	// Token: 0x04001FAF RID: 8111
	public string mainVideoUrl;

	// Token: 0x04001FB0 RID: 8112
	public string mainSearchUrl;

	// Token: 0x04001FB1 RID: 8113
	public string mainParticipateUrl;

	// Token: 0x04001FB2 RID: 8114
	public LevelEntry adventureWinner;

	// Token: 0x04001FB3 RID: 8115
	public LevelEntry raceWinner;

	// Token: 0x04001FB4 RID: 8116
	public List<LevelEntry> entries;
}
