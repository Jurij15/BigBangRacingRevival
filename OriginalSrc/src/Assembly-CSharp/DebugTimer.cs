using System;

// Token: 0x02000564 RID: 1380
public static class DebugTimer
{
	// Token: 0x06002857 RID: 10327 RVA: 0x001AD45E File Offset: 0x001AB85E
	public static void Start()
	{
		DebugTimer.time = Main.m_EPOCHSeconds;
	}

	// Token: 0x06002858 RID: 10328 RVA: 0x001AD46A File Offset: 0x001AB86A
	public static void Stop()
	{
		Debug.Log(Main.m_EPOCHSeconds - DebugTimer.time, null);
	}

	// Token: 0x04002D8B RID: 11659
	private static double time;
}
