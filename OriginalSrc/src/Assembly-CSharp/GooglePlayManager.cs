using System;
using System.Text;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;

// Token: 0x02000447 RID: 1095
public static class GooglePlayManager
{
	// Token: 0x06001E68 RID: 7784 RVA: 0x00157D5C File Offset: 0x0015615C
	public static void Initialize()
	{
		PlayGamesClientConfiguration playGamesClientConfiguration = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
		PlayGamesPlatform.InitializeInstance(playGamesClientConfiguration);
		PlayGamesPlatform.DebugLogEnabled = true;
	}

	// Token: 0x06001E69 RID: 7785 RVA: 0x00157D85 File Offset: 0x00156185
	public static void AddLoginSucceedCallback(Action _a)
	{
		GooglePlayManager.d_loginSuccess = (Action)Delegate.Combine(GooglePlayManager.d_loginSuccess, _a);
	}

	// Token: 0x06001E6A RID: 7786 RVA: 0x00157D9C File Offset: 0x0015619C
	public static void RemoveLoginSucceedCallback(Action _a)
	{
		GooglePlayManager.d_loginSuccess = (Action)Delegate.Remove(GooglePlayManager.d_loginSuccess, _a);
	}

	// Token: 0x06001E6B RID: 7787 RVA: 0x00157DB4 File Offset: 0x001561B4
	public static void Login(Action<bool> _callback = null)
	{
		if (!PlayGamesPlatform.Instance.IsAuthenticated())
		{
			PlayGamesPlatform.Instance.Authenticate(delegate(bool success)
			{
				PlayerPrefsX.SetGPGSSignedOut(!success);
				if (_callback != null)
				{
					_callback.Invoke(success);
				}
				if (success && GooglePlayManager.d_loginSuccess != null)
				{
					GooglePlayManager.d_loginSuccess.Invoke();
				}
			});
		}
	}

	// Token: 0x06001E6C RID: 7788 RVA: 0x00157DF3 File Offset: 0x001561F3
	public static bool IsAuthenticated()
	{
		return PlayGamesPlatform.Instance.IsAuthenticated();
	}

	// Token: 0x06001E6D RID: 7789 RVA: 0x00157DFF File Offset: 0x001561FF
	public static void Logout()
	{
		if (PlayGamesPlatform.Instance.IsAuthenticated())
		{
			PlayGamesPlatform.Instance.SignOut();
		}
		PlayerPrefsX.SetGPGSSignedOut(true);
	}

	// Token: 0x06001E6E RID: 7790 RVA: 0x00157E20 File Offset: 0x00156220
	public static void IncrementAchievement(string _id, int _amount)
	{
		if (!string.IsNullOrEmpty(_id))
		{
			PlayGamesPlatform.Instance.IncrementAchievement(_id, _amount, delegate(bool success)
			{
			});
		}
	}

	// Token: 0x06001E6F RID: 7791 RVA: 0x00157E58 File Offset: 0x00156258
	public static void ReportProgress(string _id, float _amount)
	{
		string gpgsid = PsAchievementIds.GetGPGSID(_id);
		if (!string.IsNullOrEmpty(gpgsid))
		{
			PlayGamesPlatform.Instance.ReportProgress(gpgsid, Convert.ToDouble(_amount), delegate(bool success)
			{
			});
		}
	}

	// Token: 0x06001E70 RID: 7792 RVA: 0x00157EA8 File Offset: 0x001562A8
	public static string GetUserName()
	{
		return PlayGamesPlatform.Instance.GetUserDisplayName();
	}

	// Token: 0x06001E71 RID: 7793 RVA: 0x00157EC4 File Offset: 0x001562C4
	public static void SaveToCloud(string _fileName, string _json, Action _succeedCallback, Action _failedCallback)
	{
		GooglePlayManager.CloudSaving.SaveToCloud(_fileName, _json, delegate(bool success, string status)
		{
			if (success)
			{
				if (_succeedCallback != null)
				{
					_succeedCallback.Invoke();
				}
			}
			else if (_failedCallback != null)
			{
				_failedCallback.Invoke();
			}
		});
	}

	// Token: 0x06001E72 RID: 7794 RVA: 0x00157EF8 File Offset: 0x001562F8
	public static void LoadFromCloud(string _fileName, Action<string> _succeeedCallback, Action _failedCallback)
	{
		GooglePlayManager.CloudSaving.LoadFromCloud(_fileName, delegate(bool successfull, string json)
		{
			if (successfull)
			{
				if (_succeeedCallback != null)
				{
					_succeeedCallback.Invoke(json);
				}
			}
			else if (_failedCallback != null)
			{
				_failedCallback.Invoke();
			}
		});
	}

	// Token: 0x06001E73 RID: 7795 RVA: 0x00157F2B File Offset: 0x0015632B
	public static void ShowSavedGames()
	{
		GooglePlayManager.CloudSaving.ShowSavedGames();
	}

	// Token: 0x040021AD RID: 8621
	public static Action d_loginSuccess;

	// Token: 0x02000448 RID: 1096
	private static class CloudSaving
	{
		// Token: 0x06001E76 RID: 7798 RVA: 0x00157F36 File Offset: 0x00156336
		private static void FailedCallback()
		{
			if (GooglePlayManager.CloudSaving.m_callback != null)
			{
				GooglePlayManager.CloudSaving.m_callback.Invoke(false, string.Empty);
			}
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x00157F52 File Offset: 0x00156352
		private static void SuccessCallback(string _success = "")
		{
			if (GooglePlayManager.CloudSaving.m_callback != null)
			{
				GooglePlayManager.CloudSaving.m_callback.Invoke(true, _success);
			}
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x00157F6C File Offset: 0x0015636C
		public static void ShowSavedGames()
		{
			if (PlayGamesPlatform.Instance.IsAuthenticated())
			{
				Debug.LogError("Trying to show google play saves: ");
				PlayGamesPlatform.Instance.SavedGame.ShowSelectSavedGameUI("Saved bbr", 5U, true, true, delegate(SelectUIStatus s, ISavedGameMetadata a)
				{
					Debug.LogError(s.ToString() + ", Description: " + a.Description);
				});
			}
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x00157FC8 File Offset: 0x001563C8
		public static void LoadFromCloud(string _filename, Action<bool, string> _callback)
		{
			GooglePlayManager.CloudSaving.m_callback = _callback;
			Debug.LogError("CloudSaving: Trying to load from cloud: Filename: " + _filename);
			if (PlayGamesPlatform.Instance.IsAuthenticated())
			{
				PlayGamesPlatform.Instance.SavedGame.OpenWithAutomaticConflictResolution(_filename, DataSource.ReadNetworkOnly, ConflictResolutionStrategy.UseUnmerged, delegate(SavedGameRequestStatus status, ISavedGameMetadata game)
				{
					GooglePlayManager.CloudSaving.SaveGameOpened(status, game, delegate
					{
						GooglePlayManager.CloudSaving.LoadBinary(game);
					});
				});
			}
			else
			{
				Debug.LogError("CloudSaving: Not authenticated!");
				GooglePlayManager.CloudSaving.FailedCallback();
			}
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x0015803D File Offset: 0x0015643D
		private static void LoadBinary(ISavedGameMetadata _metadata)
		{
			PlayGamesPlatform.Instance.SavedGame.ReadBinaryData(_metadata, new Action<SavedGameRequestStatus, byte[]>(GooglePlayManager.CloudSaving.BinaryLoadedFromCloud));
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x0015806C File Offset: 0x0015646C
		private static void BinaryLoadedFromCloud(SavedGameRequestStatus _status, byte[] _data)
		{
			if (_status == SavedGameRequestStatus.Success)
			{
				Debug.LogError("CloudSaving: SaveGameLoaded, success=" + _status);
				if (_data == null)
				{
					Debug.LogError("CloudSaving: Loaded data was null");
					GooglePlayManager.CloudSaving.FailedCallback();
					return;
				}
				string @string = Encoding.UTF8.GetString(_data);
				Debug.LogError("CloudSaving: Decoding cloud data from bytes: " + @string);
				GooglePlayManager.CloudSaving.SuccessCallback(@string);
			}
			else
			{
				Debug.LogError("CloudSaving: Error reading game binary from cloud: " + _status);
				GooglePlayManager.CloudSaving.FailedCallback();
			}
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x001580EC File Offset: 0x001564EC
		public static void SaveToCloud(string _fileName, string _json, Action<bool, string> _callback)
		{
			GooglePlayManager.CloudSaving.m_callback = _callback;
			Debug.LogError("CloudSaving: Trying to save progress to the cloud... filename: " + _fileName);
			if (PlayGamesPlatform.Instance.IsAuthenticated())
			{
				Debug.LogError("CloudSaving: (savetocloud) was authenticated: ");
				PlayGamesPlatform.Instance.SavedGame.OpenWithAutomaticConflictResolution(_fileName, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseUnmerged, delegate(SavedGameRequestStatus status, ISavedGameMetadata game)
				{
					GooglePlayManager.CloudSaving.SaveGameOpened(status, game, delegate
					{
						GooglePlayManager.CloudSaving.CommitUpdate(game, _json);
					});
				});
			}
			else
			{
				Debug.LogError("CloudSaving: Not authenticated!");
				GooglePlayManager.CloudSaving.FailedCallback();
			}
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x00158168 File Offset: 0x00156568
		private static void CommitUpdate(ISavedGameMetadata _game, string _json)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(_json);
			SavedGameMetadataUpdate savedGameMetadataUpdate = default(SavedGameMetadataUpdate.Builder).WithUpdatedDescription("Big Bang Racing google play backup, saved: " + DateTime.Now.ToString()).Build();
			PlayGamesPlatform.Instance.SavedGame.CommitUpdate(_game, savedGameMetadataUpdate, bytes, delegate(SavedGameRequestStatus s, ISavedGameMetadata a)
			{
				GooglePlayManager.CloudSaving.CommitUpdateSuccessfull(s, a, _json);
			});
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x001581E8 File Offset: 0x001565E8
		private static void CommitUpdateSuccessfull(SavedGameRequestStatus _status, ISavedGameMetadata _game, string _json)
		{
			if (_status == SavedGameRequestStatus.Success)
			{
				Debug.LogError("CloudSaving: Game " + _game.Description + ", JSON written: " + _json);
				GooglePlayManager.CloudSaving.SuccessCallback(string.Empty);
			}
			else
			{
				Debug.LogError("CloudSaving: Error commiting game update: " + _status);
				GooglePlayManager.CloudSaving.FailedCallback();
			}
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x00158240 File Offset: 0x00156640
		private static void SaveGameOpened(SavedGameRequestStatus _status, ISavedGameMetadata _game, Action _successCallback)
		{
			if (_status == SavedGameRequestStatus.Success)
			{
				Debug.LogError("CloudSaving: Savegame opened: " + _status);
				_successCallback.Invoke();
			}
			else
			{
				Debug.LogError("CloudSaving: Error opening savegame: " + _status);
				GooglePlayManager.CloudSaving.FailedCallback();
			}
		}

		// Token: 0x040021B0 RID: 8624
		private static Action<bool, string> m_callback;
	}
}
