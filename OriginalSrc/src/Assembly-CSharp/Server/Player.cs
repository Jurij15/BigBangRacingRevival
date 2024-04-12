using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MiniJSON;
using UnityEngine;

namespace Server
{
	// Token: 0x02000431 RID: 1073
	public static class Player
	{
		// Token: 0x06001DCA RID: 7626 RVA: 0x00154598 File Offset: 0x00152998
		public static HttpC GetPlayerProfile(string _playerId, Action<PlayerData> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/player/get/social");
			woeurl.AddParameter("playerId", _playerId);
			ResponseHandler<PlayerData> responseHandler = new ResponseHandler<PlayerData>(_okCallback, new Func<HttpC, PlayerData>(ClientTools.ParsePlayerData));
			HttpC httpC = Request.Get(woeurl, "GET_PLAYER_SOCIAL_PROFILE", new Action<HttpC>(responseHandler.RequestOk), _failCallback, _errorCallback);
			httpC.objectData = _playerId;
			return httpC;
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x00154604 File Offset: 0x00152A04
		public static HttpC GetPlayerInfo(Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/player/info");
			return Request.Get(woeurl, "GET_PLAYER_INFO", _okCallback, _failCallback, _errorCallback);
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x0015462C File Offset: 0x00152A2C
		public static HttpC GetStatistics(Action<PlayerStatistics> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/player/stats/find");
			ResponseHandler<PlayerStatistics> responseHandler = new ResponseHandler<PlayerStatistics>(_okCallback, new Func<HttpC, PlayerStatistics>(ClientTools.ParsePlayerStatistics));
			return Request.Get(woeurl, "GET_PLAYER_STATS", new Action<HttpC>(responseHandler.RequestOk), _failCallback, _errorCallback);
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x00154684 File Offset: 0x00152A84
		public static HttpC SwitchPlayer(Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/player/switch");
			string text = 0.ToString();
			woeurl.AddParameter("lastPathSync", text);
			return Request.Post(woeurl, "SWITCH_PLAYER", ClientTools.GenerateUserJSON(null), _okCallback, _failCallback, _errorCallback, null, new string[] { "login" }, false);
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x001546DC File Offset: 0x00152ADC
		public static HttpC RemovePlayer(Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/player/remove");
			return Request.Post(woeurl, "REMOVE_PLAYER", _okCallback, _failCallback, _errorCallback, null, null);
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x00154704 File Offset: 0x00152B04
		public static HttpC ResetPlayer(Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/player/resetLevel");
			return Request.Post(woeurl, "RESET_LEVEL", _okCallback, _failCallback, _errorCallback, null, null);
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x0015472C File Offset: 0x00152B2C
		public static HttpC RemovePlayerData(string[] _fieldsToRemove, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/player/data/remove");
			woeurl.AddParameter("fields", string.Join(",", _fieldsToRemove));
			return Request.Post(woeurl, "REMOVE_PLAYER_DATA", _okCallback, _failCallback, _errorCallback, null, null);
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x0015476C File Offset: 0x00152B6C
		public static HttpC SkipLevel(string _playerUnit, Hashtable _setData, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/player/skip");
			woeurl.AddParameter("playerUnit", _playerUnit);
			return Player.SetData(woeurl, _setData, _okCallback, _failCallback, _errorCallback);
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x0015479C File Offset: 0x00152B9C
		public static HttpC UpdateSettings(PlayerUpdateSettings _settings, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("locale", _settings.locale);
			dictionary.Add("acceptNotifications", _settings.acceptNotifications);
			string text = Json.Serialize(dictionary);
			WOEURL woeurl = new WOEURL("/v1/player/settings");
			return Request.Post(woeurl, "SETTINGS_UPDATE", text, _okCallback, _failCallback, _errorCallback, null, new string[] { "PlayerSettings" }, false);
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x0015480C File Offset: 0x00152C0C
		public static HttpC SetData(Hashtable _setData, Hashtable _pathJson, Hashtable _customisations, List<Dictionary<string, object>> _achievements, List<Hashtable> _chests, Dictionary<string, int> _editorResources, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null, bool _suspiciousActivity = false)
		{
			WOEURL woeurl = new WOEURL("/v2/player/data/change");
			Hashtable hashtable = new Hashtable();
			List<string> list = new List<string>();
			if (_setData != null)
			{
				Hashtable hashtable2 = ClientTools.GeneratePlayerSetData(_setData, ref list);
				hashtable.Add("update", hashtable2);
			}
			if (!list.Contains("SetData"))
			{
				list.Add("SetData");
			}
			if (_pathJson != null)
			{
				hashtable.Add("progression", _pathJson);
			}
			if (_customisations != null)
			{
				hashtable.Add("customisation", _customisations);
			}
			if (_achievements != null)
			{
				hashtable.Add("achievements", _achievements);
			}
			if (_chests != null)
			{
				hashtable.Add("chest", _chests);
			}
			if (_editorResources != null)
			{
				hashtable.Add("editorResources", _editorResources);
			}
			if (_suspiciousActivity)
			{
				hashtable.Add("suspicious", _suspiciousActivity);
			}
			string text = Json.Serialize(hashtable);
			string[] array = null;
			if (Enumerable.Count<string>(list) > 0)
			{
				array = list.ToArray();
			}
			ResponseHandler<HttpC> responseHandler = new ResponseHandler<HttpC>(delegate(HttpC c)
			{
				Player.SetDataOkCallback(c, _okCallback);
			}, new Func<HttpC, HttpC>(ClientTools.ParsePathSync));
			return Request.Post(woeurl, "SET_DATA_V2", text, new Action<HttpC>(responseHandler.RequestOk), _failCallback, _errorCallback, null, array, false);
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x00154964 File Offset: 0x00152D64
		public static void SetDataOkCallback(HttpC _c, Action<HttpC> _okCallBack)
		{
			if (!string.IsNullOrEmpty(_c.www.text))
			{
				Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
				if (dictionary.ContainsKey("coins"))
				{
					PsMetagameManager.m_playerStats.coins = Convert.ToInt32(dictionary["coins"]);
				}
				if (dictionary.ContainsKey("diamonds"))
				{
					PsMetagameManager.m_playerStats.diamonds = Convert.ToInt32(dictionary["diamonds"]);
				}
				if (dictionary.ContainsKey("mcTrophies"))
				{
					PsMetagameManager.m_playerStats.mcTrophies = Convert.ToInt32(dictionary["mcTrophies"]);
				}
				if (dictionary.ContainsKey("carTrophies"))
				{
					PsMetagameManager.m_playerStats.carTrophies = Convert.ToInt32(dictionary["carTrophies"]);
				}
			}
			_okCallBack.Invoke(_c);
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x00154A48 File Offset: 0x00152E48
		private static HttpC SetData(WOEURL url, Hashtable _setData, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback)
		{
			List<string> list = new List<string>();
			string text = ClientTools.GeneratePlayerSetDataJson(_setData, ref list);
			if (!list.Contains("SetData"))
			{
				list.Add("SetData");
			}
			return Request.Post(url, null, text, _okCallback, _failCallback, _errorCallback, null, list.ToArray(), false);
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x00154A94 File Offset: 0x00152E94
		public static HttpC OpenChest(Player.ChestOpenData _data, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback)
		{
			WOEURL woeurl = new WOEURL("/v1/player/openChest");
			List<string> list = new List<string>();
			list.Add("SetData");
			if (_data.id != -1)
			{
				woeurl.AddParameter("id", _data.id);
			}
			if (!string.IsNullOrEmpty(_data.gachaType))
			{
				woeurl.AddParameter("chestType", _data.gachaType);
			}
			string text = Json.Serialize(_data.setData);
			return Request.Post(woeurl, null, text, _okCallback, _failCallback, _errorCallback, null, list.ToArray(), false);
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x00154B20 File Offset: 0x00152F20
		public static void CheckSocialLogin(string _service, string _socialId, string[] _friendIds, bool autoJoin, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null, bool disableAutoRetry = false)
		{
			WOEURL woeurl = new WOEURL("/v1/player/check/social");
			string text = Player.GenerateSocialJson(_service, _socialId, _friendIds, null, null);
			woeurl.AddParameter("noJoin", (!autoJoin).ToString().ToLower());
			Request.Post(woeurl, null, text, _okCallback, _failCallback, _errorCallback, null, null, true);
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x00154B78 File Offset: 0x00152F78
		public static void ResolveSocialLogin(string _service, string _socialId, string _connectUserId, string _disconnectFromUserId, string[] _friendIds, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/player/solve/social");
			string text = Player.GenerateSocialJson(_service, _socialId, _friendIds, _connectUserId, _disconnectFromUserId);
			Request.Post(woeurl, null, text, _okCallback, _failCallback, _errorCallback, null, null, false);
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x00154BB0 File Offset: 0x00152FB0
		public static void RemoveSocialLogin(string _service, string _socialId, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/player/remove/social");
			string text = Player.GenerateSocialJson(_service, _socialId, null, null, null);
			Request.Post(woeurl, null, text, _okCallback, _failCallback, _errorCallback, null, null, false);
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x00154BE4 File Offset: 0x00152FE4
		public static void SendSocialFriends(string _service, string _socialId, string[] _friendIds, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/player/follow/social");
			string text = Player.GenerateSocialJson(_service, _socialId, _friendIds, null, null);
			Request.Post(woeurl, null, text, _okCallback, _failCallback, _errorCallback, null, null, false);
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x00154C18 File Offset: 0x00153018
		public static void CheckAndJoinGameCenter(Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v2/player/check/gameCenter");
			string text = Player.GenerateGameCenterJSON(null);
			Request.Post(woeurl, null, text, _okCallback, _failCallback, _errorCallback, null, null, false);
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x00154C48 File Offset: 0x00153048
		public static void SwitchGameCenter(string id, string[] _friendIds, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v2/player/join/gameCenter");
			woeurl.AddParameter("oldId", id);
			PlayerPrefsX.SetUserName(string.Empty);
			string text = Player.GenerateGameCenterJSON(_friendIds);
			Request.Post(woeurl, null, text, _okCallback, _failCallback, _errorCallback, null, null, false);
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x00154C90 File Offset: 0x00153090
		public static HttpC Follow(string _followeeId, Hashtable _customisations, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/player/follow/add");
			List<string> list = new List<string>();
			list.Add("Follow");
			Hashtable hashtable = new Hashtable();
			if (_customisations != null)
			{
				list.Add("SetData");
				hashtable.Add("customisation", _customisations);
			}
			string text = Json.Serialize(hashtable);
			woeurl.AddParameter("followeeId", _followeeId);
			return Request.Post(woeurl, null, text, _okCallback, _failCallback, _errorCallback, null, list.ToArray(), false);
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x00154D04 File Offset: 0x00153104
		public static HttpC UnFollow(string _followeeId, Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/player/follow/remove");
			woeurl.AddParameter("followerId", PlayerPrefsX.GetUserId());
			woeurl.AddParameter("followeeId", _followeeId);
			return Request.Post(woeurl, null, _okCallback, _failCallback, _errorCallback, null, new string[] { "Follow" });
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x00154D54 File Offset: 0x00153154
		public static HttpC Followees(Action<PlayerData[]> _okCallback, Action<HttpC> _failCallback, string _playerId, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/player/follow/followees");
			woeurl.AddParameter("followerId", _playerId);
			ResponseHandler<PlayerData[]> responseHandler = new ResponseHandler<PlayerData[]>(_okCallback, new Func<HttpC, PlayerData[]>(ClientTools.ParseFollowPlayers));
			return Request.Get(woeurl, null, new Action<HttpC>(responseHandler.RequestOk), _failCallback, _errorCallback);
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x00154DB4 File Offset: 0x001531B4
		public static HttpC Followers(Action<PlayerData[]> _okCallback, Action<HttpC> _failCallback, string _playerId, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/player/follow/followers");
			woeurl.AddParameter("followeeId", _playerId);
			ResponseHandler<PlayerData[]> responseHandler = new ResponseHandler<PlayerData[]>(_okCallback, new Func<HttpC, PlayerData[]>(ClientTools.ParseFollowPlayers));
			return Request.Get(woeurl, null, new Action<HttpC>(responseHandler.RequestOk), _failCallback, _errorCallback);
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x00154E14 File Offset: 0x00153214
		public static HttpC Friends(Action<Friends> _okCallback, Action<HttpC> _failCallback, string _playerId, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v2/player/friends");
			ResponseHandler<Friends> responseHandler = new ResponseHandler<Friends>(_okCallback, new Func<HttpC, Friends>(ClientTools.ParseFriends));
			return Request.Get(woeurl, null, new Action<HttpC>(responseHandler.RequestOk), _failCallback, _errorCallback);
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x00154E68 File Offset: 0x00153268
		public static void ChangeNamePrompt(Action _proceed)
		{
			TouchAreaS.Enable();
			if (Player.m_changeName == null)
			{
				Player.m_changeName = new PsUIBasePopup(typeof(PsUIPopupChangeName), null, null, null, false, true, InitialPage.Center, false, false, false);
				Player.m_changeName.SetAction("Proceed", _proceed);
				(Player.m_changeName.m_mainContent as PsUIPopupChangeName).CreateUI(true);
				Player.m_changeName.Update();
			}
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x00154ED0 File Offset: 0x001532D0
		public static HttpC Login(Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
		{
			Player.m_changeName = null;
			int num = PsPlanetManager.GetCurrentPlanet().GetMainPathCurrentNodeId() - 1;
			Debug.Log("Login to WoE server", null);
			WOEURL woeurl = new WOEURL("/v4/player/login");
			if (!string.IsNullOrEmpty(PlayerPrefsX.GetUserId()))
			{
				bool flag = false;
				string[] files = Directory.GetFiles(PsPlanetSerializer.GetPathString(), PlayerPrefsX.GetUserId() + "*");
				if (files.Length < PsState.m_vehicleTypes.Length * 2)
				{
					flag = true;
				}
				else
				{
					for (int i = 0; i < files.Length; i++)
					{
						PsPlanetPath psPlanetPath = null;
						try
						{
							psPlanetPath = PsPlanetSerializer.LoadProgression(files[i], PsPlanetPathType.main, true);
						}
						catch
						{
						}
						if (psPlanetPath == null)
						{
							flag = true;
							break;
						}
					}
				}
				string text = ((!string.IsNullOrEmpty(PlayerPrefsX.GetLastSync()) && !flag) ? PlayerPrefsX.GetLastSync() : "0");
				woeurl.AddParameter("lastPathSync", text);
			}
			else
			{
				PsMetrics.CreatingNewUser();
				woeurl.AddParameter("lastPathSync", "0");
			}
			return Request.Post(woeurl, null, ClientTools.GenerateUserJSON(null), _okCallback, _failCallback, _errorCallback, null, new string[] { "login" }, false);
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x0015500C File Offset: 0x0015340C
		public static HttpC ChangeName(string _newName, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v2/player/changeName");
			woeurl.AddParameter("newName", WWW.EscapeURL(_newName));
			return Request.Get(woeurl, "PLAYER_CHANGE_NAME", _okCallback, _failureCallback, null);
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x00155048 File Offset: 0x00153448
		public static HttpC ChangeYoutuber(string _youtuber, string _youtuberId, long _subscriberCount, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/player/changeYoutuber");
			if (_youtuber != null)
			{
				woeurl.AddParameter("youtuber", WWW.EscapeURL(_youtuber));
			}
			if (_youtuberId != null)
			{
				woeurl.AddParameter("youtuberId", WWW.EscapeURL(_youtuberId));
			}
			woeurl.AddParameter("subscriberCount", _subscriberCount);
			return Request.Get(woeurl, "PLAYER_YOUTUBER_CHANGE", _okCallback, _failureCallback, _errorCallback);
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x001550B4 File Offset: 0x001534B4
		public static PlayerData[] ParsePlayers(PlayerData[] _players, bool _fb, bool _gc)
		{
			List<PlayerData> list = new List<PlayerData>();
			for (int i = 0; i < _players.Length; i++)
			{
				if (_fb && _players[i].facebookId != null)
				{
					list.Add(_players[i]);
				}
				else if (_gc && _players[i].gameCenterId != null)
				{
					list.Add(_players[i]);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x00155138 File Offset: 0x00153538
		private static string GenerateSocialJson(string _service, string _socialId, string[] _friendIds, string _connectId = null, string _disconnectId = null)
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("service", _service);
			hashtable.Add("socialId", _socialId);
			hashtable.Add("friendList", _friendIds);
			if (!string.IsNullOrEmpty(_connectId))
			{
				hashtable.Add("connectId", _connectId);
			}
			if (!string.IsNullOrEmpty(_disconnectId))
			{
				hashtable.Add("disconnectId", _disconnectId);
			}
			string text = Json.Serialize(hashtable);
			Debug.Log("SOCIAL JSON GENERATED: " + text, null);
			return text;
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x001551B8 File Offset: 0x001535B8
		private static string GenerateGameCenterJSON(string[] _friendIds)
		{
			Hashtable hashtable = new Hashtable();
			string userName = PlayerPrefsX.GetUserName();
			if (userName != string.Empty && userName != null)
			{
				hashtable.Add("name", PlayerPrefsX.GetUserName());
			}
			if (PlayerPrefsX.GetGameCenterId() != null)
			{
				hashtable.Add("gameCenterId", PlayerPrefsX.GetGameCenterId());
			}
			if (_friendIds != null)
			{
				hashtable.Add("gcFriends", _friendIds);
			}
			string text = Json.Serialize(hashtable);
			Debug.Log("GC JOIN JSON GENERATED: " + text, null);
			return text;
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x0015523C File Offset: 0x0015363C
		private static void LoginComplete()
		{
			Debug.Log("Login complete", null);
		}

		// Token: 0x04002091 RID: 8337
		private static bool m_firstTimeLogin;

		// Token: 0x04002092 RID: 8338
		private static PsUIBasePopup m_changeName;

		// Token: 0x02000432 RID: 1074
		public class ChestOpenData
		{
			// Token: 0x04002099 RID: 8345
			public int id = -1;

			// Token: 0x0400209A RID: 8346
			public string gachaType;

			// Token: 0x0400209B RID: 8347
			public Hashtable setData;
		}

		// Token: 0x02000433 RID: 1075
		public class ProgressionAndSetDataObject
		{
			// Token: 0x0400209C RID: 8348
			public Hashtable setData;

			// Token: 0x0400209D RID: 8349
			public Hashtable pathJson;

			// Token: 0x0400209E RID: 8350
			public Hashtable customisations;

			// Token: 0x0400209F RID: 8351
			public List<Dictionary<string, object>> achievements;

			// Token: 0x040020A0 RID: 8352
			public List<Hashtable> chests;

			// Token: 0x040020A1 RID: 8353
			public bool sendMetrics;

			// Token: 0x040020A2 RID: 8354
			public Dictionary<string, int> editorResources;
		}
	}
}
