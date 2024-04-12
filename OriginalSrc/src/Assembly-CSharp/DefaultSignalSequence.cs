using System;

// Token: 0x0200020C RID: 524
public static class DefaultSignalSequence
{
	// Token: 0x06000F40 RID: 3904 RVA: 0x00090EBC File Offset: 0x0008F2BC
	public static void StartDefaultSignalSequence(Action _endSequenceCallback)
	{
		GameSceneEffectManager.CreateTVChannels("TVChannelFlicksMat_Material", 4);
		GameSceneEffectManager.m_endSequenceCallback = _endSequenceCallback;
		GameSceneEffectManager.SetChannel(0);
		GameSceneEffectManager.SetEffects(true);
		TimerS.AddComponent(GameSceneEffectManager.m_utilityEntity, string.Empty, 2f, 0f, false, new TimerComponentDelegate(DefaultSignalSequence.OtherChannelGameNoise));
	}

	// Token: 0x06000F41 RID: 3905 RVA: 0x00090F20 File Offset: 0x0008F320
	private static void OtherChannelGameNoise(TimerC _c)
	{
		TimerS.RemoveComponent(_c);
		GameSceneEffectManager.SetChannel(-1);
		GameSceneEffectManager.SetEffects(false);
		TimerS.AddComponent(GameSceneEffectManager.m_utilityEntity, string.Empty, 1f, 0f, false, new TimerComponentDelegate(DefaultSignalSequence.OtherTheEnd));
	}

	// Token: 0x06000F42 RID: 3906 RVA: 0x00090F77 File Offset: 0x0008F377
	private static void OtherTheEnd(TimerC _c)
	{
		TimerS.RemoveComponent(_c);
		GameSceneEffectManager.m_endSequenceCallback.Invoke();
	}

	// Token: 0x06000F43 RID: 3907 RVA: 0x00090F8C File Offset: 0x0008F38C
	public static void StartDefaultSignalEndSequence(Action _endSequenceCallback)
	{
		GameSceneEffectManager.m_endSequenceCallback = _endSequenceCallback;
		GameSceneEffectManager.SetEffects(true);
		TimerS.AddComponent(GameSceneEffectManager.m_utilityEntity, string.Empty, 2f, 0f, false, new TimerComponentDelegate(DefaultSignalSequence.OtherChannelGameNoiseEND));
	}

	// Token: 0x06000F44 RID: 3908 RVA: 0x00090FE0 File Offset: 0x0008F3E0
	private static void OtherChannelGameNoiseEND(TimerC _c)
	{
		TimerS.RemoveComponent(_c);
		GameSceneEffectManager.SetChannel(0);
		TimerS.AddComponent(GameSceneEffectManager.m_utilityEntity, string.Empty, 1f, 0f, false, new TimerComponentDelegate(DefaultSignalSequence.OtherTheEnd2));
	}

	// Token: 0x06000F45 RID: 3909 RVA: 0x00091031 File Offset: 0x0008F431
	private static void OtherTheEnd2(TimerC _c)
	{
		TimerS.RemoveComponent(_c);
		GameSceneEffectManager.m_endSequenceCallback.Invoke();
	}
}
