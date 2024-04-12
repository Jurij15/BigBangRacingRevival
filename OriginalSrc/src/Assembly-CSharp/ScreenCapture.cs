using System;

// Token: 0x0200034C RID: 844
public class ScreenCapture
{
	// Token: 0x1700007C RID: 124
	// (get) Token: 0x060018BB RID: 6331 RVA: 0x0010D734 File Offset: 0x0010BB34
	public static bool isAvailable
	{
		get
		{
			return EveryplayManager.IsEnabled();
		}
	}

	// Token: 0x1700007D RID: 125
	// (get) Token: 0x060018BC RID: 6332 RVA: 0x0010D73B File Offset: 0x0010BB3B
	public static bool isRecording
	{
		get
		{
			return EveryplayManager.IsRecording();
		}
	}

	// Token: 0x060018BD RID: 6333 RVA: 0x0010D742 File Offset: 0x0010BB42
	public static void Start()
	{
		ScreenCapture.m_preview = false;
		ScreenCapture.m_discard = false;
		if (!EveryplayManager.IsRecording())
		{
			EveryplayManager.StartRecording();
		}
	}

	// Token: 0x060018BE RID: 6334 RVA: 0x0010D75F File Offset: 0x0010BB5F
	public static void Stop()
	{
		ScreenCapture.m_preview = false;
		ScreenCapture.m_discard = false;
		if (ScreenCapture.isRecording)
		{
			EveryplayManager.StopRecording(0f);
		}
	}

	// Token: 0x060018BF RID: 6335 RVA: 0x0010D781 File Offset: 0x0010BB81
	public static void Discard()
	{
		if (EveryplayManager.HasRecordedVideo())
		{
			EveryplayManager.ClearRecording();
			ScreenCapture.m_discard = false;
		}
		else
		{
			ScreenCapture.m_discard = true;
		}
		ScreenCapture.m_preview = false;
	}

	// Token: 0x060018C0 RID: 6336 RVA: 0x0010D7A9 File Offset: 0x0010BBA9
	public static void Preview()
	{
		if (EveryplayManager.HasRecordedVideo())
		{
			EveryplayManager.PlayLastRecording();
			ScreenCapture.m_preview = false;
		}
		else
		{
			ScreenCapture.m_preview = true;
		}
	}

	// Token: 0x060018C1 RID: 6337 RVA: 0x0010D7CB File Offset: 0x0010BBCB
	public static void Update()
	{
		if (EveryplayManager.IsRecording())
		{
			if (ScreenCapture.m_preview)
			{
				ScreenCapture.Preview();
			}
			else if (ScreenCapture.m_discard)
			{
				ScreenCapture.Discard();
			}
		}
	}

	// Token: 0x04001B63 RID: 7011
	private static bool m_preview;

	// Token: 0x04001B64 RID: 7012
	private static bool m_discard;
}
