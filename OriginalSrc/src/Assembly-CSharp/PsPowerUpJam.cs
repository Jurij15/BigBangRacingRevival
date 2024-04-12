using System;

// Token: 0x02000028 RID: 40
public class PsPowerUpJam : PsPowerUpFreeze
{
	// Token: 0x06000113 RID: 275 RVA: 0x0000CD7F File Offset: 0x0000B17F
	public PsPowerUpJam(float _duration)
		: base(_duration)
	{
	}

	// Token: 0x06000114 RID: 276 RVA: 0x0000CD88 File Offset: 0x0000B188
	public override string GetName()
	{
		return "SlowMo";
	}

	// Token: 0x06000115 RID: 277 RVA: 0x0000CD8F File Offset: 0x0000B18F
	public override string GetFrame()
	{
		return string.Empty;
	}

	// Token: 0x06000116 RID: 278 RVA: 0x0000CD96 File Offset: 0x0000B196
	protected override float TargetTimeScale()
	{
		return 0.5f;
	}
}
