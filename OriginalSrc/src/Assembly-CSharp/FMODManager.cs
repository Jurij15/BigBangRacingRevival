using System;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

// Token: 0x0200051A RID: 1306
public static class FMODManager
{
	// Token: 0x0600266B RID: 9835 RVA: 0x001A5DA4 File Offset: 0x001A41A4
	public static void Initialize()
	{
		FMODManager.m_system = RuntimeManager.StudioSystem;
		FMODManager.ERRCHECK(FMODManager.m_system.getBus("bus:/", ref FMODManager.m_bus));
		FMODManager.ERRCHECK(FMODManager.m_system.getLowLevelSystem(ref FMODManager.m_lowLevelSystem));
		uint num;
		int num2;
		FMODManager.ERRCHECK(FMODManager.m_lowLevelSystem.getDSPBufferSize(ref num, ref num2));
		Debug.Log(string.Concat(new object[] { "FMOD Initialized with DSP Buffer: ", num, "/", num2 }), null);
	}

	// Token: 0x0600266C RID: 9836 RVA: 0x001A5E30 File Offset: 0x001A4230
	public static void PlayOneShot(string _event, Vector3 _position, float _volume = 1f)
	{
		EventInstance eventInstance = RuntimeManager.CreateInstance(_event);
		ATTRIBUTES_3D attributes_3D = RuntimeUtils.To3DAttributes(_position);
		FMODManager.ERRCHECK(eventInstance.set3DAttributes(attributes_3D));
		FMODManager.ERRCHECK(eventInstance.setVolume(_volume));
		FMODManager.ERRCHECK(eventInstance.start());
		FMODManager.ERRCHECK(eventInstance.release());
	}

	// Token: 0x0600266D RID: 9837 RVA: 0x001A5E7C File Offset: 0x001A427C
	public static void PlayOneShotWithParameter(string _event, Vector3 _position, string _parameter, float _value, float _volume = 1f)
	{
		EventInstance eventInstance = RuntimeManager.CreateInstance(_event);
		ParameterInstance parameterInstance = FMODManager.GetParameterInstance(eventInstance, _parameter);
		FMODManager.SetParameter(parameterInstance, _value);
		ATTRIBUTES_3D attributes_3D = RuntimeUtils.To3DAttributes(_position);
		FMODManager.ERRCHECK(eventInstance.set3DAttributes(attributes_3D));
		FMODManager.ERRCHECK(eventInstance.setVolume(_volume));
		FMODManager.ERRCHECK(eventInstance.start());
		FMODManager.ERRCHECK(eventInstance.release());
	}

	// Token: 0x0600266E RID: 9838 RVA: 0x001A5ED8 File Offset: 0x001A42D8
	public static EventInstance NewEvent(string _event, Vector3 _position, bool _startImmediately = true, float _volume = 1f)
	{
		EventInstance eventInstance = RuntimeManager.CreateInstance(_event);
		ATTRIBUTES_3D attributes_3D = RuntimeUtils.To3DAttributes(_position);
		FMODManager.ERRCHECK(eventInstance.set3DAttributes(attributes_3D));
		FMODManager.ERRCHECK(eventInstance.setVolume(_volume));
		if (_startImmediately)
		{
			FMODManager.ERRCHECK(eventInstance.start());
		}
		return eventInstance;
	}

	// Token: 0x0600266F RID: 9839 RVA: 0x001A5F1D File Offset: 0x001A431D
	public static void ReleaseEvent(EventInstance _eventInstance)
	{
		if (_eventInstance != null)
		{
			FMODManager.ERRCHECK(_eventInstance.stop(1));
			FMODManager.ERRCHECK(_eventInstance.release());
		}
	}

	// Token: 0x06002670 RID: 9840 RVA: 0x001A5F42 File Offset: 0x001A4342
	public static void SetParameter(ParameterInstance _parameterInstance, float _value)
	{
		FMODManager.ERRCHECK(_parameterInstance.setValue(_value));
	}

	// Token: 0x06002671 RID: 9841 RVA: 0x001A5F50 File Offset: 0x001A4350
	public static ParameterInstance GetParameterInstance(EventInstance _eventInstance, string _parameterName)
	{
		ParameterInstance parameterInstance = null;
		FMODManager.ERRCHECK(_eventInstance.getParameter(_parameterName, ref parameterInstance));
		return parameterInstance;
	}

	// Token: 0x06002672 RID: 9842 RVA: 0x001A5F6E File Offset: 0x001A436E
	public static void BusSetPause(bool _pause)
	{
		FMODManager.ERRCHECK(FMODManager.m_bus.setPaused(_pause));
	}

	// Token: 0x06002673 RID: 9843 RVA: 0x001A5F80 File Offset: 0x001A4380
	public static void BusSetMute(bool _mute)
	{
		FMODManager.ERRCHECK(FMODManager.m_bus.setMute(_mute));
	}

	// Token: 0x06002674 RID: 9844 RVA: 0x001A5F92 File Offset: 0x001A4392
	public static void BusStopAllEvents()
	{
		FMODManager.ERRCHECK(FMODManager.m_bus.stopAllEvents(0));
	}

	// Token: 0x06002675 RID: 9845 RVA: 0x001A5FA4 File Offset: 0x001A43A4
	public static void ReleaseAllInstances()
	{
	}

	// Token: 0x06002676 RID: 9846 RVA: 0x001A5FA6 File Offset: 0x001A43A6
	public static void ERRCHECK(RESULT result)
	{
		if (result != null)
		{
			Debug.LogError("FMOD Error (" + result.ToString() + "): " + Error.String(result));
		}
	}

	// Token: 0x04002BDD RID: 11229
	public static global::FMOD.System m_lowLevelSystem;

	// Token: 0x04002BDE RID: 11230
	public static global::FMOD.Studio.System m_system;

	// Token: 0x04002BDF RID: 11231
	public static Bus m_bus;
}
