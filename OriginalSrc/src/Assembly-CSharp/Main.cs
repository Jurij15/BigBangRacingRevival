using System;
using System.IO;
using UnityEngine;

// Token: 0x02000735 RID: 1845
public class Main : MonoBehaviour
{
	// Token: 0x0600354F RID: 13647 RVA: 0x001CFD05 File Offset: 0x001CE105
	private void Start()
	{
		Main.m_currentGame = new PsGame("PlaySomething");
	}

	// Token: 0x06003550 RID: 13648 RVA: 0x001CFD18 File Offset: 0x001CE118
	private void Update()
	{
		Main.m_backPressedThisFrame = false;
		if (!Main.m_currentGameInitialized)
		{
			if (Main.m_currentGameInitializeTick == 0)
			{
				if (Screen.width > Main.m_maxHorizontalResolution)
				{
					Debug.Log("setting smaller screen resolution", null);
					float num = (float)Screen.width / (float)Screen.height;
					Screen.SetResolution(Main.m_maxHorizontalResolution, Mathf.CeilToInt((float)Main.m_maxHorizontalResolution / num), true);
				}
			}
			else if (Main.m_currentGame != null && Main.m_currentGameInitializeTick > 0)
			{
				this.SetEPOCHTime();
				Main.m_currentGame.Initialize(new StartupScene("StartupScene"));
				Main.m_currentGameInitialized = true;
			}
			Main.m_currentGameInitializeTick++;
		}
		else
		{
			this.SetEPOCHTime();
			Main.m_currentGame.Update();
		}
	}

	// Token: 0x06003551 RID: 13649 RVA: 0x001CFDDC File Offset: 0x001CE1DC
	public static bool AndroidBackButtonPressed(string _guid = null)
	{
		bool flag = PsState.m_openPopups.Count == 0 || (PsState.m_openPopups[PsState.m_openPopups.Count - 1] as PsUIBasePopup).m_guid == _guid;
		if (!Main.m_backPressedThisFrame && Input.GetKeyDown(27) && flag)
		{
			Main.m_backPressedThisFrame = true;
			return true;
		}
		return false;
	}

	// Token: 0x06003552 RID: 13650 RVA: 0x001CFE47 File Offset: 0x001CE247
	private void ResetTemporaryTime()
	{
		Main.m_resettingGameTime = 0f;
	}

	// Token: 0x06003553 RID: 13651 RVA: 0x001CFE54 File Offset: 0x001CE254
	private void SetEPOCHTime()
	{
		Main.m_EPOCHSeconds = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
	}

	// Token: 0x06003554 RID: 13652 RVA: 0x001CFE84 File Offset: 0x001CE284
	public static void SetTimeScale(float _scale)
	{
		Main.m_timeScale = _scale;
		Main.m_timeScaleD = (double)_scale;
		Time.timeScale = _scale;
	}

	// Token: 0x06003555 RID: 13653 RVA: 0x001CFE99 File Offset: 0x001CE299
	public static float GetDeltaTime()
	{
		if (Main.m_timeScale != 0f)
		{
			return Main.m_gameDeltaTime / ToolBox.limitBetween(Main.m_timeScale, 1E-09f, float.MaxValue);
		}
		return Main.m_gameDeltaTime;
	}

	// Token: 0x06003556 RID: 13654 RVA: 0x001CFECA File Offset: 0x001CE2CA
	public static void SetPerfMode(bool _on)
	{
		if (_on)
		{
			Main.m_renderFPS = 30;
			Main.m_ticksPerFrame = 2;
		}
		else
		{
			Main.m_renderFPS = 60;
			Main.m_ticksPerFrame = 1;
		}
		Application.targetFrameRate = Main.m_renderFPS;
	}

	// Token: 0x06003557 RID: 13655 RVA: 0x001CFEFC File Offset: 0x001CE2FC
	public static bool ShouldSetLowPerf()
	{
		bool flag = false;
		if (SystemInfo.processorCount <= 2)
		{
			flag = true;
		}
		else if (SystemInfo.processorCount >= 8)
		{
			flag = false;
		}
		else
		{
			string text = "/sys/devices/system/cpu/cpu0/cpufreq/cpuinfo_max_freq";
			using (TextReader textReader = new StreamReader(text))
			{
				try
				{
					string text2 = textReader.ReadLine();
					int num = int.Parse(text2) / 1000;
					flag = num < 1600;
				}
				catch
				{
					flag = false;
				}
			}
		}
		return flag;
	}

	// Token: 0x06003558 RID: 13656 RVA: 0x001CFFA4 File Offset: 0x001CE3A4
	private void OnApplicationFocus(bool _focusStatus)
	{
		this.SetEPOCHTime();
		if (Main.m_currentGameInitialized)
		{
			Main.m_currentGame.OnApplicationFocus(_focusStatus);
		}
	}

	// Token: 0x06003559 RID: 13657 RVA: 0x001CFFC1 File Offset: 0x001CE3C1
	private void OnApplicationPause(bool _pauseStatus)
	{
		this.SetEPOCHTime();
		if (Main.m_currentGameInitialized)
		{
			Main.m_currentGame.OnApplicationPause(_pauseStatus);
		}
	}

	// Token: 0x0600355A RID: 13658 RVA: 0x001CFFDE File Offset: 0x001CE3DE
	private void LateUpdate()
	{
		if (Main.m_currentGameInitialized)
		{
			Main.m_currentGame.LateUpdate();
		}
	}

	// Token: 0x0600355B RID: 13659 RVA: 0x001CFFF4 File Offset: 0x001CE3F4
	public static string GetPersistentDataPath()
	{
		string text = Application.persistentDataPath + "/" + Main.m_currentGame.m_projectCode;
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		return text;
	}

	// Token: 0x0600355C RID: 13660 RVA: 0x001D0030 File Offset: 0x001CE430
	public static string GetApplicationDataPath()
	{
		string text = Application.dataPath;
		if (Application.platform == 1)
		{
			text = text + "/../../SaveData/" + Main.m_currentGame.m_projectCode;
		}
		else if (Application.platform == 2)
		{
			text = text + "/../SaveData/" + Main.m_currentGame.m_projectCode;
		}
		else if (Application.platform == 7)
		{
			text = string.Concat(new string[]
			{
				Application.dataPath,
				"/",
				Main.m_currentGame.m_projectCode,
				"/Resources/",
				Main.m_currentGame.m_projectCode
			});
		}
		else if (Application.platform == null)
		{
			text = string.Concat(new string[]
			{
				Application.dataPath,
				"/",
				Main.m_currentGame.m_projectCode,
				"/Resources/",
				Main.m_currentGame.m_projectCode
			});
		}
		else
		{
			text = Main.GetPersistentDataPath();
		}
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		return text;
	}

	// Token: 0x0600355D RID: 13661 RVA: 0x001D0143 File Offset: 0x001CE543
	public static bool FileExists(string _path)
	{
		return File.Exists(_path);
	}

	// Token: 0x0600355E RID: 13662 RVA: 0x001D014C File Offset: 0x001CE54C
	public static int CLIENT_VERSION()
	{
		if (Main.m_currentVersion == 0)
		{
			string version = TrackedBundleVersion.Current.version;
			string text = string.Empty;
			string[] array = version.Split(new char[] { '.' });
			for (int i = 0; i < array.Length; i++)
			{
				text += array[i];
			}
			Main.m_currentVersion = Convert.ToInt32(text);
		}
		return Main.m_currentVersion;
	}

	// Token: 0x0600355F RID: 13663 RVA: 0x001D01B4 File Offset: 0x001CE5B4
	public static void DeleteLocalDataFiles()
	{
		string persistentDataPath = Main.GetPersistentDataPath();
		Debug.LogError("Deleting all contents of folder: " + persistentDataPath);
		DirectoryInfo directoryInfo = new DirectoryInfo(persistentDataPath);
		foreach (FileInfo fileInfo in directoryInfo.GetFiles())
		{
			fileInfo.Delete();
		}
		foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
		{
			directoryInfo2.Delete(true);
		}
	}

	// Token: 0x0400339D RID: 13213
	public static double m_EPOCHSeconds = 0.0;

	// Token: 0x0400339E RID: 13214
	public static double m_gameTimeSinceAppStarted = 0.0;

	// Token: 0x0400339F RID: 13215
	public static float m_resettingGameTime = 0f;

	// Token: 0x040033A0 RID: 13216
	public static float m_gameDeltaTime = 0f;

	// Token: 0x040033A1 RID: 13217
	public static int m_gameTicks = 0;

	// Token: 0x040033A2 RID: 13218
	public static int m_logicFPS = 60;

	// Token: 0x040033A3 RID: 13219
	public static float m_dt = 1f / (float)Main.m_logicFPS;

	// Token: 0x040033A4 RID: 13220
	public static double m_dtD = 1.0 / (double)((float)Main.m_logicFPS);

	// Token: 0x040033A5 RID: 13221
	public static int m_renderFPS = 60;

	// Token: 0x040033A6 RID: 13222
	public static int m_ticksPerFrame = 1;

	// Token: 0x040033A7 RID: 13223
	public static IGame m_currentGame;

	// Token: 0x040033A8 RID: 13224
	public static bool m_currentGameInitialized = false;

	// Token: 0x040033A9 RID: 13225
	public static int m_currentGameInitializeTick = 0;

	// Token: 0x040033AA RID: 13226
	public static bool m_paused = false;

	// Token: 0x040033AB RID: 13227
	public static float m_timeScale = 1f;

	// Token: 0x040033AC RID: 13228
	public static double m_timeScaleD = 1.0;

	// Token: 0x040033AD RID: 13229
	public static int m_currentVersion = 0;

	// Token: 0x040033AE RID: 13230
	private static int m_maxHorizontalResolution = 1600;

	// Token: 0x040033AF RID: 13231
	private static bool m_backPressedThisFrame = false;
}
