using System;
using System.Collections;
using System.Collections.Generic;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

// Token: 0x02000508 RID: 1288
public static class SoundS
{
	// Token: 0x06002543 RID: 9539 RVA: 0x0019C148 File Offset: 0x0019A548
	public static void Initialize(GameObject _mainCamera)
	{
		StudioListener component = _mainCamera.GetComponent<StudioListener>();
		PsFMODManager.m_allowInit = true;
		FMODManager.Initialize();
		SoundS.m_components = new DynamicArray<SoundC>(100, 0.5f, 0.25f, 0.5f);
		SoundS.m_mainCamera = _mainCamera;
		SoundS.SetListener(SoundS.m_mainCamera.gameObject, true);
		SoundS.m_combineSounds = new Hashtable();
		SoundS.m_initialized = true;
	}

	// Token: 0x06002544 RID: 9540 RVA: 0x0019C1A8 File Offset: 0x0019A5A8
	public static void SetListener(GameObject _go, bool _forceZToZero = true)
	{
		SoundS.m_listener = SoundS.m_mainCamera.GetComponent<StudioListener>();
		PsFMODManager.m_listener = _go;
		PsFMODManager.m_forceListenerZToZero = _forceZToZero;
		SoundS.m_listenerGO = _go;
		Debug.Log("Listener set to: " + _go.transform.name, null);
	}

	// Token: 0x06002545 RID: 9541 RVA: 0x0019C1E6 File Offset: 0x0019A5E6
	public static void PlaySingleShot(string _event, Vector3 _pos, float _volume = 1f)
	{
		if (!SoundS.m_canPlaySounds || !SoundS.m_initialized)
		{
			return;
		}
		if (!SoundS.m_mixerPaused)
		{
			FMODManager.PlayOneShot("event:" + _event, _pos, _volume);
		}
	}

	// Token: 0x06002546 RID: 9542 RVA: 0x0019C219 File Offset: 0x0019A619
	public static void PlaySingleShotWithParameter(string _event, Vector3 _pos, string _parameter, float _value, float _volume = 1f)
	{
		if (!SoundS.m_canPlaySounds || !SoundS.m_initialized)
		{
			return;
		}
		if (!SoundS.m_mixerPaused)
		{
			FMODManager.PlayOneShotWithParameter("event:" + _event, _pos, _parameter, _value, _volume);
		}
	}

	// Token: 0x06002547 RID: 9543 RVA: 0x0019C250 File Offset: 0x0019A650
	public static void PlaySound(SoundC _c, bool _forcePlay = false)
	{
		if ((!SoundS.m_canPlaySounds && !_forcePlay) || !SoundS.m_initialized)
		{
			return;
		}
		if (_c.isCombineSound)
		{
			_c.isPlaying = true;
			return;
		}
		if (!_c.isPlaying)
		{
			FMODManager.ERRCHECK(_c.eventInstance.start());
			_c.isPlaying = true;
		}
		else
		{
			FMODManager.ERRCHECK(_c.eventInstance.setTimelinePosition(0));
		}
	}

	// Token: 0x06002548 RID: 9544 RVA: 0x0019C2C3 File Offset: 0x0019A6C3
	public static void StopSound(SoundC _c)
	{
		if (_c.isCombineSound)
		{
			_c.isPlaying = false;
			return;
		}
		if (_c.isPlaying)
		{
			FMODManager.ERRCHECK(_c.eventInstance.stop(1));
			_c.isPlaying = false;
		}
	}

	// Token: 0x06002549 RID: 9545 RVA: 0x0019C2FC File Offset: 0x0019A6FC
	public static void PauseSound(SoundC _c)
	{
		if (_c.isCombineSound && _c.isPlaying)
		{
			_c.isPaused = true;
			return;
		}
		if (!_c.isPaused && _c.isPlaying)
		{
			FMODManager.ERRCHECK(_c.eventInstance.setPaused(true));
			_c.isPaused = true;
		}
	}

	// Token: 0x0600254A RID: 9546 RVA: 0x0019C358 File Offset: 0x0019A758
	public static void ResumeSound(SoundC _c)
	{
		if (_c.isCombineSound && _c.isPlaying)
		{
			_c.isPaused = false;
			return;
		}
		if (_c.isPaused && _c.isPlaying)
		{
			FMODManager.ERRCHECK(_c.eventInstance.setPaused(false));
			_c.isPaused = false;
		}
	}

	// Token: 0x0600254B RID: 9547 RVA: 0x0019C3B1 File Offset: 0x0019A7B1
	public static void SetVolume(SoundC _c, float _volume)
	{
		FMODManager.ERRCHECK(_c.eventInstance.setVolume(_volume));
	}

	// Token: 0x0600254C RID: 9548 RVA: 0x0019C3C4 File Offset: 0x0019A7C4
	public static void SetSoundParameter(SoundC _c, string _parameterName, float _value)
	{
		if (SoundS.m_initialized)
		{
			if (_c.parameterInstances == null)
			{
				_c.parameterInstances = new Hashtable();
			}
			if (!_c.parameterInstances.Contains(_parameterName))
			{
				_c.parameterInstances[_parameterName] = FMODManager.GetParameterInstance(_c.eventInstance, _parameterName);
			}
			ParameterInstance parameterInstance = _c.parameterInstances[_parameterName] as ParameterInstance;
			FMODManager.SetParameter(parameterInstance, _value);
		}
	}

	// Token: 0x0600254D RID: 9549 RVA: 0x0019C438 File Offset: 0x0019A838
	private static void UpdatePosition(SoundC _c, Vector3 _pos)
	{
		ATTRIBUTES_3D attributes_3D = RuntimeUtils.To3DAttributes(_pos);
		FMODManager.ERRCHECK(_c.eventInstance.set3DAttributes(attributes_3D));
	}

	// Token: 0x0600254E RID: 9550 RVA: 0x0019C460 File Offset: 0x0019A860
	public static void SetPauseForAllSounds(bool _pause)
	{
		int aliveCount = SoundS.m_components.m_aliveCount;
		for (int i = 0; i < aliveCount; i++)
		{
			int num = SoundS.m_components.m_aliveIndices[i];
			SoundC soundC = SoundS.m_components.m_array[num];
			if (_pause)
			{
				SoundS.PauseSound(soundC);
			}
			else
			{
				SoundS.ResumeSound(soundC);
			}
		}
	}

	// Token: 0x0600254F RID: 9551 RVA: 0x0019C4BC File Offset: 0x0019A8BC
	public static void RemoveAllSounds()
	{
		if (SoundS.m_initialized)
		{
			while (SoundS.m_components.m_aliveCount > 0)
			{
				SoundC soundC = SoundS.m_components.m_array[SoundS.m_components.m_aliveIndices[0]];
				SoundS.RemoveComponent(soundC);
			}
			FMODManager.BusStopAllEvents();
		}
	}

	// Token: 0x06002550 RID: 9552 RVA: 0x0019C50B File Offset: 0x0019A90B
	public static void PauseMixer(bool _on)
	{
		if (_on != SoundS.m_mixerPaused)
		{
			FMODManager.BusSetPause(_on);
			SoundS.m_mixerPaused = _on;
		}
	}

	// Token: 0x06002551 RID: 9553 RVA: 0x0019C524 File Offset: 0x0019A924
	public static void MuteMixer(bool _mute)
	{
		FMODManager.BusSetMute(_mute);
	}

	// Token: 0x06002552 RID: 9554 RVA: 0x0019C52C File Offset: 0x0019A92C
	public static void Update()
	{
		if (SoundS.m_initialized)
		{
			int aliveCount = SoundS.m_components.m_aliveCount;
			for (int i = 0; i < aliveCount; i++)
			{
				int num = SoundS.m_components.m_aliveIndices[i];
				SoundC soundC = SoundS.m_components.m_array[num];
				if (!soundC.isCombineSound)
				{
					TransformC p_TC = soundC.p_TC;
					if (soundC.forceAtListenerPosition)
					{
						SoundS.UpdatePosition(soundC, SoundS.m_listenerGO.transform.position);
					}
					else if (p_TC.updatedPosition)
					{
						SoundS.UpdatePosition(soundC, p_TC.transform.position);
					}
				}
			}
			SoundS.UpdateCombineSounds();
			SoundS.m_components.Update();
		}
	}

	// Token: 0x06002553 RID: 9555 RVA: 0x0019C5E0 File Offset: 0x0019A9E0
	public static void UpdateCombineSounds()
	{
		if (SoundS.m_combineSounds.Count == 0)
		{
			return;
		}
		IEnumerator enumerator = SoundS.m_combineSounds.Values.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				CombineSound combineSound = (CombineSound)obj;
				int count = combineSound.components.Count;
				int num = 0;
				int num2 = 0;
				float num3 = -1f;
				foreach (SoundC soundC in combineSound.components)
				{
					if (soundC.isPlaying)
					{
						num++;
					}
					if (soundC.isPaused)
					{
						num2++;
					}
					if (soundC.isPlaying && !soundC.isPaused)
					{
						float magnitude = (SoundS.m_listenerGO.transform.position - soundC.p_TC.transform.position).magnitude;
						if (magnitude < num3 || num3 < 0f)
						{
							num3 = magnitude;
							combineSound.nearestSoundC = soundC;
						}
					}
				}
				if (!combineSound.isPlaying && num > 0)
				{
					FMODManager.ERRCHECK(combineSound.eventInstance.start());
					combineSound.isPlaying = true;
				}
				if (combineSound.isPlaying && num == 0)
				{
					FMODManager.ERRCHECK(combineSound.eventInstance.stop(1));
					combineSound.isPlaying = false;
				}
				if (combineSound.isPlaying)
				{
					if (!combineSound.isPaused && num2 == count)
					{
						FMODManager.ERRCHECK(combineSound.eventInstance.setPaused(true));
						combineSound.isPaused = true;
					}
					if (combineSound.isPaused && num2 == 0)
					{
						FMODManager.ERRCHECK(combineSound.eventInstance.setPaused(false));
						combineSound.isPaused = false;
					}
					if (!combineSound.isPaused)
					{
						float num4 = 1f - ToolBox.getPositionBetween(num3, combineSound.minDist, combineSound.maxDist);
						if (!combineSound.isOutOfRange)
						{
							if (num4 > 0f)
							{
								FMODManager.ERRCHECK(combineSound.eventInstance.setVolume(combineSound.volume * num4));
								Vector3 position = SoundS.m_listenerGO.transform.position;
								if (PsFMODManager.m_forceListenerZToZero)
								{
									position.z = 0f;
								}
								ATTRIBUTES_3D attributes_3D = RuntimeUtils.To3DAttributes(position);
								FMODManager.ERRCHECK(combineSound.eventInstance.set3DAttributes(attributes_3D));
							}
							else
							{
								FMODManager.ERRCHECK(combineSound.eventInstance.setPaused(true));
								combineSound.isOutOfRange = true;
							}
						}
						else if (num4 > 0f)
						{
							FMODManager.ERRCHECK(combineSound.eventInstance.setPaused(false));
							FMODManager.ERRCHECK(combineSound.eventInstance.setVolume(combineSound.volume * num4));
							combineSound.isOutOfRange = false;
						}
					}
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = enumerator as IDisposable) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x06002554 RID: 9556 RVA: 0x0019C900 File Offset: 0x0019AD00
	public static CombineSound GetCombineSoundWithKey(string _key)
	{
		if (SoundS.m_combineSounds.Contains(_key))
		{
			return SoundS.m_combineSounds[_key] as CombineSound;
		}
		return null;
	}

	// Token: 0x06002555 RID: 9557 RVA: 0x0019C924 File Offset: 0x0019AD24
	public static void RemoveCombineSoundWithTag(string _tag)
	{
		if (SoundS.m_combineSounds.Contains(_tag))
		{
			CombineSound combineSound = SoundS.m_combineSounds[_tag] as CombineSound;
			Debug.Log("Combine sound removed", null);
			combineSound.components.Clear();
			combineSound.components = null;
			if (combineSound.eventInstance != null)
			{
				FMODManager.ERRCHECK(combineSound.eventInstance.stop(1));
				FMODManager.ReleaseEvent(combineSound.eventInstance);
				combineSound.eventInstance = null;
			}
			SoundS.m_combineSounds.Remove(_tag);
		}
	}

	// Token: 0x06002556 RID: 9558 RVA: 0x0019C9B0 File Offset: 0x0019ADB0
	public static SoundC AddComponent(TransformC _parentTC, string _event, float _volume = 1f, bool _forceAtListenersPosition = false)
	{
		if (SoundS.m_initialized)
		{
			SoundC soundC = SoundS.m_components.AddItem();
			soundC.name = _event;
			soundC.eventInstance = FMODManager.NewEvent("event:" + _event, _parentTC.transform.position, false, _volume);
			soundC.p_TC = _parentTC;
			soundC.forceAtListenerPosition = _forceAtListenersPosition;
			soundC.isCombineSound = false;
			soundC.isPaused = false;
			soundC.isPlaying = false;
			EntityManager.AddComponentToEntity(_parentTC.p_entity, soundC);
			return soundC;
		}
		return null;
	}

	// Token: 0x06002557 RID: 9559 RVA: 0x0019CA30 File Offset: 0x0019AE30
	public static SoundC AddCombineSoundComponent(TransformC _parentTC, string _key, string _event, float _volume = 1f)
	{
		if (SoundS.m_initialized)
		{
			CombineSound combineSound = SoundS.GetCombineSoundWithKey(_key);
			if (combineSound == null)
			{
				Debug.Log("Combine sound created: " + _event, null);
				combineSound = new CombineSound();
				combineSound.components = new List<SoundC>();
				combineSound.eventInstance = FMODManager.NewEvent("event:" + _event, SoundS.m_listenerGO.transform.position, false, _volume);
				combineSound.volume = _volume;
				combineSound.isOutOfRange = false;
				EventDescription eventDescription = null;
				combineSound.eventInstance.getDescription(ref eventDescription);
				eventDescription.getMinimumDistance(ref combineSound.minDist);
				eventDescription.getMaximumDistance(ref combineSound.maxDist);
				SoundS.m_combineSounds.Add(_key, combineSound);
			}
			SoundC soundC = SoundS.m_components.AddItem();
			soundC.eventInstance = combineSound.eventInstance;
			soundC.p_TC = _parentTC;
			soundC.forceAtListenerPosition = false;
			soundC.isCombineSound = true;
			soundC.combineSoundKey = _key;
			soundC.isPaused = false;
			soundC.isPlaying = false;
			EntityManager.AddComponentToEntity(_parentTC.p_entity, soundC);
			combineSound.components.Add(soundC);
			return soundC;
		}
		return null;
	}

	// Token: 0x06002558 RID: 9560 RVA: 0x0019CB40 File Offset: 0x0019AF40
	public static void RemoveComponent(SoundC _c)
	{
		if (SoundS.m_initialized)
		{
			if (_c.p_entity == null)
			{
				Debug.LogWarning("Trying to remove component that has already been removed");
				return;
			}
			if (_c.isCombineSound)
			{
				CombineSound combineSound = SoundS.m_combineSounds[_c.combineSoundKey] as CombineSound;
				if (combineSound.nearestSoundC == _c)
				{
					combineSound.nearestSoundC = null;
				}
				combineSound.components.Remove(_c);
				if (combineSound.components.Count == 0)
				{
					SoundS.RemoveCombineSoundWithTag(_c.combineSoundKey);
				}
			}
			if (!_c.isCombineSound && _c.eventInstance != null)
			{
				SoundS.StopSound(_c);
				FMODManager.ReleaseEvent(_c.eventInstance);
				_c.eventInstance = null;
			}
			EntityManager.RemoveComponentFromEntity(_c);
			SoundS.m_components.RemoveItem(_c);
		}
	}

	// Token: 0x04002AFB RID: 11003
	public static bool m_canPlaySounds = true;

	// Token: 0x04002AFC RID: 11004
	private static bool m_mixerPaused;

	// Token: 0x04002AFD RID: 11005
	public static GameObject m_mainCamera;

	// Token: 0x04002AFE RID: 11006
	public static DynamicArray<SoundC> m_components;

	// Token: 0x04002AFF RID: 11007
	public static GameObject m_listenerGO;

	// Token: 0x04002B00 RID: 11008
	public static StudioListener m_listener;

	// Token: 0x04002B01 RID: 11009
	public static bool m_initialized;

	// Token: 0x04002B02 RID: 11010
	private static Hashtable m_combineSounds;
}
