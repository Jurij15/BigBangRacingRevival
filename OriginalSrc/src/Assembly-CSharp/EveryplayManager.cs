using System;

// Token: 0x02000519 RID: 1305
public static class EveryplayManager
{
	// Token: 0x0600265A RID: 9818 RVA: 0x001A5BE4 File Offset: 0x001A3FE4
	public static void Initialize()
	{
	}

	// Token: 0x0600265B RID: 9819 RVA: 0x001A5BE6 File Offset: 0x001A3FE6
	public static void PlayLastRecording()
	{
		SoundS.SetPauseForAllSounds(true);
	}

	// Token: 0x0600265C RID: 9820 RVA: 0x001A5BEE File Offset: 0x001A3FEE
	private static void WasClosed()
	{
		SoundS.SetPauseForAllSounds(false);
	}

	// Token: 0x0600265D RID: 9821 RVA: 0x001A5BF6 File Offset: 0x001A3FF6
	private static void OnReadyForRecording(bool _enabled)
	{
		if (EveryplayManager.m_isSupported || _enabled)
		{
		}
	}

	// Token: 0x0600265E RID: 9822 RVA: 0x001A5C08 File Offset: 0x001A4008
	private static void OnRecordingStopped()
	{
		Debug.Log("Everyplay recording stopped.", null);
		EveryplayManager.m_isRecording = false;
	}

	// Token: 0x0600265F RID: 9823 RVA: 0x001A5C1B File Offset: 0x001A401B
	private static void OnRecordingStarted()
	{
		Debug.Log("Everyplay recording started...", null);
		EveryplayManager.m_isRecording = true;
	}

	// Token: 0x06002660 RID: 9824 RVA: 0x001A5C2E File Offset: 0x001A402E
	public static void SetEnabled(bool _enabled)
	{
		EveryplayManager.m_isEnabled = _enabled;
	}

	// Token: 0x06002661 RID: 9825 RVA: 0x001A5C36 File Offset: 0x001A4036
	public static bool IsEnabled()
	{
		return EveryplayManager.m_isSupported && EveryplayManager.m_isEnabled;
	}

	// Token: 0x06002662 RID: 9826 RVA: 0x001A5C4A File Offset: 0x001A404A
	public static bool IsSupported()
	{
		return EveryplayManager.m_isSupported;
	}

	// Token: 0x06002663 RID: 9827 RVA: 0x001A5C51 File Offset: 0x001A4051
	public static bool IsRecording()
	{
		return !EveryplayManager.IsEnabled() && false;
	}

	// Token: 0x06002664 RID: 9828 RVA: 0x001A5C60 File Offset: 0x001A4060
	public static bool HasRecordedVideo()
	{
		return EveryplayManager.IsEnabled() && EveryplayManager.m_hasRecordedVideo;
	}

	// Token: 0x06002665 RID: 9829 RVA: 0x001A5C73 File Offset: 0x001A4073
	public static void StartRecording()
	{
		if (!EveryplayManager.IsEnabled())
		{
			return;
		}
		EveryplayManager.m_startRecording = true;
		EveryplayManager.m_hasRecordedVideo = false;
		Debug.Log("Everyplay: Trying to start recording...", null);
	}

	// Token: 0x06002666 RID: 9830 RVA: 0x001A5C98 File Offset: 0x001A4098
	public static void StopRecording(float _delay = 0f)
	{
		if (!EveryplayManager.IsEnabled())
		{
			return;
		}
		EveryplayManager.m_startRecording = false;
		if (EveryplayManager.m_isRecording)
		{
			if (_delay <= 0f)
			{
				EveryplayManager.m_stopDelaySecs = 0f;
				Debug.Log("Everyplay: Trying to stop recording.", null);
			}
			else
			{
				EveryplayManager.m_stopDelaySecs = _delay;
				Debug.Log("Everyplay: Stop recording after " + _delay + " secs.", null);
			}
			EveryplayManager.m_hasRecordedVideo = true;
		}
	}

	// Token: 0x06002667 RID: 9831 RVA: 0x001A5D0C File Offset: 0x001A410C
	public static void SetPaused(bool _paused)
	{
		if (!EveryplayManager.IsEnabled())
		{
			return;
		}
	}

	// Token: 0x06002668 RID: 9832 RVA: 0x001A5D19 File Offset: 0x001A4119
	public static void ClearRecording()
	{
		if (!EveryplayManager.IsEnabled())
		{
			return;
		}
		EveryplayManager.m_hasRecordedVideo = false;
	}

	// Token: 0x06002669 RID: 9833 RVA: 0x001A5D2C File Offset: 0x001A412C
	public static void Update()
	{
		if (EveryplayManager.IsEnabled())
		{
			if (EveryplayManager.m_isRecording)
			{
				if (EveryplayManager.m_stopDelaySecs > 0f)
				{
					EveryplayManager.m_stopDelaySecs -= Main.m_gameDeltaTime;
					if (EveryplayManager.m_stopDelaySecs <= 0f)
					{
						EveryplayManager.StopRecording(0f);
					}
				}
			}
			else if (EveryplayManager.m_startRecording)
			{
				EveryplayManager.m_startRecording = false;
			}
		}
	}

	// Token: 0x04002BD7 RID: 11223
	private static bool m_isEnabled = true;

	// Token: 0x04002BD8 RID: 11224
	private static bool m_isSupported;

	// Token: 0x04002BD9 RID: 11225
	private static bool m_isRecording;

	// Token: 0x04002BDA RID: 11226
	private static bool m_hasRecordedVideo;

	// Token: 0x04002BDB RID: 11227
	private static float m_stopDelaySecs;

	// Token: 0x04002BDC RID: 11228
	private static bool m_startRecording;
}
