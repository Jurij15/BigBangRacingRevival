using System;

// Token: 0x020003FB RID: 1019
public class PercentileField
{
	// Token: 0x06001C67 RID: 7271 RVA: 0x00140CA8 File Offset: 0x0013F0A8
	public override string ToString()
	{
		return string.Concat(new object[]
		{
			"{Percentile: ",
			this.percentile.ToString(),
			", Reward: ",
			this.reward,
			"}"
		});
	}

	// Token: 0x04001EF5 RID: 7925
	public int index;

	// Token: 0x04001EF6 RID: 7926
	public int percentile;

	// Token: 0x04001EF7 RID: 7927
	public int reward;
}
