using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x020004B9 RID: 1209
public static class Debug
{
	// Token: 0x0600227E RID: 8830 RVA: 0x0018F83E File Offset: 0x0018DC3E
	public static void Initialize(bool _log, bool _error, bool _warning, bool _info)
	{
		Debug.m_filterLog = _log;
		Debug.m_filterLogError = _error;
		Debug.m_filterLogWarning = _warning;
		Debug.m_filterLogInfo = _info;
	}

	// Token: 0x0600227F RID: 8831 RVA: 0x0018F858 File Offset: 0x0018DC58
	public static void Log(object message, object obu = null)
	{
		if (Debug.m_filterLog)
		{
			Debug.Log(message);
		}
	}

	// Token: 0x06002280 RID: 8832 RVA: 0x0018F86A File Offset: 0x0018DC6A
	public static void LogError(object message)
	{
		if (Debug.m_filterLogError)
		{
			Debug.LogError(message);
		}
	}

	// Token: 0x06002281 RID: 8833 RVA: 0x0018F87C File Offset: 0x0018DC7C
	public static void LogError(object message, object obu)
	{
		if (Debug.m_filterLogError)
		{
			Debug.LogError(message);
		}
	}

	// Token: 0x06002282 RID: 8834 RVA: 0x0018F88E File Offset: 0x0018DC8E
	public static void LogWarning(object message)
	{
		if (Debug.m_filterLogWarning)
		{
			Debug.LogWarning(message);
		}
	}

	// Token: 0x06002283 RID: 8835 RVA: 0x0018F8A0 File Offset: 0x0018DCA0
	public static void LogWarning(object message, object obu)
	{
		if (Debug.m_filterLogWarning)
		{
			Debug.LogWarning(message);
		}
	}

	// Token: 0x06002284 RID: 8836 RVA: 0x0018F8B2 File Offset: 0x0018DCB2
	public static void LogInfo(object message)
	{
		if (Debug.m_filterLogInfo)
		{
			Debug.Log(message);
		}
	}

	// Token: 0x06002285 RID: 8837 RVA: 0x0018F8C4 File Offset: 0x0018DCC4
	public static void Assert(bool ok, string message = "")
	{
		if (ok)
		{
			return;
		}
		if (Debug.m_filterLogError)
		{
			Debug.LogError("Error with LibTessDotNET: " + message);
		}
	}

	// Token: 0x06002286 RID: 8838 RVA: 0x0018F8E8 File Offset: 0x0018DCE8
	public static void LogStackTrace()
	{
		if (Debug.m_filterLogInfo)
		{
			StackTrace stackTrace = new StackTrace();
			Debug.Log(stackTrace, null);
		}
	}

	// Token: 0x06002287 RID: 8839 RVA: 0x0018F90C File Offset: 0x0018DD0C
	public static string GetSystemStats()
	{
		string text = "FULL FRAMEWORK STATS: \n";
		string text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"Entities: ",
			EntityManager.m_entities.m_aliveCount,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"Transforms: ",
			TransformS.m_components.m_aliveCount,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"Prefabs: ",
			PrefabS.m_components.m_aliveCount,
			"\n"
		});
		int num = 0;
		int aliveCount = SpriteS.m_sheets.m_aliveCount;
		for (int i = 0; i < aliveCount; i++)
		{
			num += SpriteS.m_sheets.m_array[SpriteS.m_sheets.m_aliveIndices[i]].m_components.m_aliveCount;
		}
		text2 = text;
		text = string.Concat(new object[] { text2, "Sprites: ", num, "\n" });
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"Sounds: ",
			SoundS.m_components.m_aliveCount,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"Text meshes: ",
			TextMeshS.m_components.m_aliveCount,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"Tweens: ",
			TweenS.m_components.m_aliveCount,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"Projectors: ",
			ProjectorS.m_components.m_aliveCount,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"Touch areas: ",
			TouchAreaS.m_areas.m_aliveCount,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"Timers: ",
			TimerS.m_components.m_aliveCount,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"Http: ",
			HttpS.m_components.m_aliveCount,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"Chipmunk bodies: ",
			ChipmunkProS.m_bodies.m_aliveCount,
			"(",
			ChipmunkProWrapper.ucpGetTotalBodyCount(),
			")\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"Chipmunk constraints: ",
			ChipmunkProS.m_constraints.m_aliveCount,
			"(",
			ChipmunkProWrapper.ucpGetTotalConstraintCount(),
			")\n"
		});
		text += "\nUNITY STATS:\n";
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"GameObjects: ",
			Object.FindObjectsOfType(typeof(GameObject)).Length,
			"\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"Materials: ",
			Object.FindObjectsOfType(typeof(Material)).Length,
			"\n"
		});
		text2 = text;
		return string.Concat(new object[]
		{
			text2,
			"Meshes: ",
			Object.FindObjectsOfType(typeof(Mesh)).Length,
			"\n"
		});
	}

	// Token: 0x0400289E RID: 10398
	public static bool m_filterLog = true;

	// Token: 0x0400289F RID: 10399
	public static bool m_filterLogError = true;

	// Token: 0x040028A0 RID: 10400
	public static bool m_filterLogWarning = true;

	// Token: 0x040028A1 RID: 10401
	public static bool m_filterLogInfo = true;
}
