using System;

// Token: 0x0200014D RID: 333
public static class PsMetricsData
{
	// Token: 0x06000B9E RID: 2974 RVA: 0x0007342D File Offset: 0x0007182D
	public static void Reset()
	{
		PsMetricsData.m_testersUsed = 0;
		PsMetricsData.m_beamersUsed = 0;
		PsMetricsData.m_refillsUsed = 0;
	}

	// Token: 0x04000A41 RID: 2625
	public static int m_beamersUsed;

	// Token: 0x04000A42 RID: 2626
	public static int m_testersUsed;

	// Token: 0x04000A43 RID: 2627
	public static int m_refillsUsed;

	// Token: 0x04000A44 RID: 2628
	public static int m_timeInEditor;

	// Token: 0x04000A45 RID: 2629
	public static int m_lastGhostSize;

	// Token: 0x04000A46 RID: 2630
	public static bool m_isSendingGhost;

	// Token: 0x04000A47 RID: 2631
	public static bool m_isNewGhost;
}
