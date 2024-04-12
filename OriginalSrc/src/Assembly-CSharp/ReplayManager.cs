using System;
using System.Collections.Generic;

// Token: 0x0200054F RID: 1359
public static class ReplayManager
{
	// Token: 0x060027B7 RID: 10167 RVA: 0x001AA68C File Offset: 0x001A8A8C
	public static void Initialize()
	{
		ReplayManager.m_playbackReplays = new List<Replay>();
		ReplayManager.m_recordedReplays = new List<Replay>();
	}

	// Token: 0x060027B8 RID: 10168 RVA: 0x001AA6A2 File Offset: 0x001A8AA2
	public static void CreateRecordingReplay(string _levelId)
	{
		if (ReplayManager.m_recordingReplay != null)
		{
			ReplayManager.m_recordedReplays.Add(ReplayManager.m_recordingReplay);
		}
		ReplayManager.m_recordingReplay = new Replay(_levelId);
	}

	// Token: 0x060027B9 RID: 10169 RVA: 0x001AA6C8 File Offset: 0x001A8AC8
	public static void CreatePlaybackReplay(Replay _replay)
	{
		ReplayManager.m_playbackReplays.Add(_replay);
	}

	// Token: 0x060027BA RID: 10170 RVA: 0x001AA6D5 File Offset: 0x001A8AD5
	public static void ClearPlaybackReplays()
	{
		ReplayManager.m_playbackReplays.Clear();
	}

	// Token: 0x060027BB RID: 10171 RVA: 0x001AA6E1 File Offset: 0x001A8AE1
	public static void Update()
	{
	}

	// Token: 0x04002D4C RID: 11596
	public static List<Replay> m_playbackReplays;

	// Token: 0x04002D4D RID: 11597
	public static Replay m_recordingReplay;

	// Token: 0x04002D4E RID: 11598
	public static List<Replay> m_recordedReplays;
}
