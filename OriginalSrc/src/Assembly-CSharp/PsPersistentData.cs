using System;
using System.Collections.Generic;
using Server;

// Token: 0x02000008 RID: 8
public static class PsPersistentData
{
	// Token: 0x06000029 RID: 41 RVA: 0x0000270C File Offset: 0x00000B0C
	public static void Init()
	{
		if (PsPersistentData.m_backups == null)
		{
			PsPersistentData.m_backups = new List<PsBackup>();
			PsPersistentData.m_backups.Add(new PsBackupGooglePlay(Main.m_currentGame.m_projectCode));
			PsPersistentData.m_backups.Add(new PsBackupLocalAndroid("backup.txt"));
		}
	}

	// Token: 0x0600002A RID: 42 RVA: 0x0000275C File Offset: 0x00000B5C
	public static void Save()
	{
		foreach (PsBackup psBackup in PsPersistentData.m_backups)
		{
			psBackup.SaveBackup();
		}
	}

	// Token: 0x0600002B RID: 43 RVA: 0x000027B8 File Offset: 0x00000BB8
	public static bool HasBackupAvailable()
	{
		foreach (PsBackup psBackup in PsPersistentData.m_backups)
		{
			if (psBackup.GetStatus() != PsBackup.Status.Loading && psBackup.GetPlayer().userId != PlayerPrefsX.GetUserId())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00002840 File Offset: 0x00000C40
	public static void CheckBackups(Action _userWasNotChanged = null)
	{
		PsPersistentData.m_userWasNotChanged = _userWasNotChanged;
		Dictionary<string, PsBackup> dictionary = new Dictionary<string, PsBackup>();
		Debug.Log("Backups: check backups", null);
		foreach (PsBackup psBackup in PsPersistentData.m_backups)
		{
			if (psBackup.GetStatus() == PsBackup.Status.Loading)
			{
				Debug.Log("Backups: still loading: " + psBackup.ToString(), null);
			}
			else if (string.IsNullOrEmpty(psBackup.GetPlayer().userId))
			{
				Debug.Log("Backups: NOT loading, and User id was null or empty: " + psBackup.ToString() + ", writing current id: " + PlayerPrefsX.GetUserId(), null);
				psBackup.SaveBackup();
			}
			else
			{
				bool flag = PlayerPrefsX.GetUserId() == psBackup.GetPlayer().userId;
				Debug.Log("Backups: Not loading. same id: " + flag, null);
				if (!flag)
				{
					Debug.Log("Backups: Resolve conflict", null);
					dictionary[psBackup.GetPlayer().userId] = psBackup;
				}
			}
		}
		if (dictionary.Count > 0)
		{
			PsPersistentData.ResolveConflict(dictionary);
		}
		else if (PsPersistentData.m_userWasNotChanged != null)
		{
			PsPersistentData.m_userWasNotChanged.Invoke();
		}
	}

	// Token: 0x0600002D RID: 45 RVA: 0x0000299C File Offset: 0x00000D9C
	private static void ResolveConflict(Dictionary<string, PsBackup> _dict)
	{
		PsUIPopupSyncAccount.m_currentData = new SyncAccountData(PlayerPrefsX.GetUserId(), PsMetagameManager.m_playerStats.totalTrophies, PsMetagameManager.m_playerStats.racesCompleted + PsMetagameManager.m_playerStats.adventureLevels, false, string.Empty, "CurrentAccount");
		PsUIPopupSyncAccount.m_cloudData = new List<SyncAccountData>();
		foreach (KeyValuePair<string, PsBackup> keyValuePair in _dict)
		{
			PsUIPopupSyncAccount.m_cloudData.Add(new SyncAccountData(keyValuePair.Key, keyValuePair.Value.m_playerData.mcTrophies + keyValuePair.Value.m_playerData.carTrophies, keyValuePair.Value.m_playerData.racesWon + keyValuePair.Value.m_playerData.adventureLevelsCompleted, true, keyValuePair.Value.m_playerData.name, keyValuePair.Value.GetServiceName()));
		}
		if (PsUIPopupSyncAccount.m_cloudData.Count > 0)
		{
			PsPersistentData.CreateSyncPopup();
		}
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00002AC0 File Offset: 0x00000EC0
	private static void CreateSyncPopup()
	{
		PsMetrics.ProgressionChoiceOffered();
		PsPersistentData.m_syncPopup = new PsUIBasePopup(typeof(PsUIPopupSyncBackup), null, null, null, true, true, InitialPage.Center, false, false, false);
		PsPersistentData.m_syncPopup.SetAction("Cloud", delegate
		{
			PsUIBasePopup psUIBasePopup = new PsUIBasePopup(typeof(PsUIPopupConfirmSyncFacebook), null, null, null, true, true, InitialPage.Center, false, false, false);
			psUIBasePopup.SetAction("Confirm", delegate
			{
				PsPersistentData.RestartWithExistingUser(PsUIPopupConfirmSyncAccount.m_selectedData);
			});
			psUIBasePopup.SetAction("Cancel", delegate
			{
				PsPersistentData.CreateSyncPopup();
			});
		});
		PsPersistentData.m_syncPopup.SetAction("Current", delegate
		{
			PsUIPopupConfirmSyncAccount.m_selectedData = PsUIPopupSyncAccount.m_currentData;
			PsUIBasePopup psUIBasePopup2 = new PsUIBasePopup(typeof(PsUIPopupConfirmSyncFacebook), null, null, null, true, true, InitialPage.Center, false, false, false);
			psUIBasePopup2.SetAction("Confirm", delegate
			{
				PsPersistentData.UseCurrent();
			});
			psUIBasePopup2.SetAction("Cancel", delegate
			{
				PsPersistentData.CreateSyncPopup();
			});
		});
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00002B48 File Offset: 0x00000F48
	private static void RestartWithExistingUser(SyncAccountData _data)
	{
		PsMetrics.ConnectedToOldProgression(_data.m_serviceName);
		Debug.Log("Restarting with existing user account", null);
		string userId = PlayerPrefsX.GetUserId();
		PsState.m_restarting = true;
		if (PsMetagameManager.m_serverQueueItems != null)
		{
			for (int i = 0; i < PsMetagameManager.m_serverQueueItems.Count; i++)
			{
				PsMetagameManager.m_serverQueueItems[i] = null;
			}
			PsMetagameManager.m_serverQueueItems.Clear();
		}
		PsBackup psBackup = null;
		foreach (PsBackup psBackup2 in PsPersistentData.m_backups)
		{
			if (psBackup2.GetStatus() != PsBackup.Status.Loading && psBackup2.m_playerData.playerId == _data.m_id)
			{
				psBackup = psBackup2;
				break;
			}
		}
		if (psBackup != null)
		{
			PsPersistentData.SaveBackup(psBackup.m_playerData);
		}
		ResetFlow.Start(false);
		PlayerPrefsX.SetUserId(_data.m_id);
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00002C4C File Offset: 0x0000104C
	private static void SaveBackup(PlayerData _data)
	{
		foreach (PsBackup psBackup in PsPersistentData.m_backups)
		{
			if (psBackup.GetStatus() != PsBackup.Status.Loading)
			{
				psBackup.SaveBackup(_data);
			}
		}
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00002CB4 File Offset: 0x000010B4
	private static void UseCurrent()
	{
		PsMetrics.ConnectedToNewProgression();
		Debug.Log("Backup: Override Backups with Current ID", null);
		foreach (PsBackup psBackup in PsPersistentData.m_backups)
		{
			if (psBackup.GetStatus() != PsBackup.Status.Loading)
			{
				psBackup.SaveBackup();
			}
		}
		if (PsPersistentData.m_userWasNotChanged != null)
		{
			PsPersistentData.m_userWasNotChanged.Invoke();
		}
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00002D40 File Offset: 0x00001140
	public static void ClearBackups()
	{
		foreach (PsBackup psBackup in PsPersistentData.m_backups)
		{
			psBackup.Clear();
		}
	}

	// Token: 0x04000011 RID: 17
	private static bool m_init;

	// Token: 0x04000012 RID: 18
	private static List<PsBackup> m_backups;

	// Token: 0x04000013 RID: 19
	private static Action m_userWasNotChanged;

	// Token: 0x04000014 RID: 20
	private static PsUIBasePopup m_syncPopup;
}
