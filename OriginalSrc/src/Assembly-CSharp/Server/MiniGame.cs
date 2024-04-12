using System;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

namespace Server
{
	// Token: 0x0200042B RID: 1067
	public static class MiniGame
	{
		// Token: 0x06001D91 RID: 7569 RVA: 0x001518A7 File Offset: 0x0014FCA7
		public static HttpC GetMinigamesInSubGenre(PsSubgenre _subGenre, Action<PsMinigameMetaData[]> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			return MiniGame.GetMinigamesInSubGenre(_subGenre.ToString(), _okCallback, _failureCallback, _errorCallback);
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x001518C0 File Offset: 0x0014FCC0
		public static HttpC GetMinigamesInSubGenre(string _subGenre, Action<PsMinigameMetaData[]> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/minigame/subgenre/find");
			ResponseHandler<PsMinigameMetaData[]> responseHandler = new ResponseHandler<PsMinigameMetaData[]>(_okCallback, new Func<HttpC, PsMinigameMetaData[]>(ClientTools.ParseMinigameList));
			woeurl.AddParameter("subGenre", _subGenre);
			return Request.Get(woeurl, "MINIGAME_SUBGENRE_FIND", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x00151924 File Offset: 0x0014FD24
		public static HttpC GetHidden(Action<PsMinigameMetaData[]> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null, int _limit = 10)
		{
			ResponseHandler<PsMinigameMetaData[]> responseHandler = new ResponseHandler<PsMinigameMetaData[]>(_okCallback, new Func<HttpC, PsMinigameMetaData[]>(ClientTools.ParseMinigameList));
			WOEURL woeurl = new WOEURL("/v1/minigame/hidden");
			woeurl.AddParameter("limit", _limit);
			return Request.Get(woeurl, "MINIGAME_HIDDEN_FIND", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x0015198C File Offset: 0x0014FD8C
		public static HttpC SearchMinigames(MinigameSearchParametres _parametres, Action<PsMinigameMetaData[]> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null, int _limit = 10)
		{
			ResponseHandler<PsMinigameMetaData[]> responseHandler = new ResponseHandler<PsMinigameMetaData[]>(_okCallback, new Func<HttpC, PsMinigameMetaData[]>(ClientTools.ParseMinigameList));
			WOEURL woeurl = new WOEURL("/v2/minigame/meta/search");
			if (_parametres.m_items != null && _parametres.m_items.Length > 0)
			{
				woeurl.AddParameter("items", string.Join(",", _parametres.m_items));
			}
			if (_parametres.m_features != null && _parametres.m_features.Length > 0)
			{
				woeurl.AddParameter("features", string.Join(",", _parametres.m_features));
			}
			if (_parametres.m_playerUnitFilter != null && _parametres.m_playerUnitFilter != "Any")
			{
				woeurl.AddParameter("playerUnit", _parametres.m_playerUnitFilter);
			}
			if (_parametres.m_gameMode != PsGameMode.Any)
			{
				woeurl.AddParameter("gameMode", _parametres.m_gameMode.ToString());
			}
			if (_parametres.m_difficulty != PsGameDifficulty.Any)
			{
				woeurl.AddParameter("difficulty", _parametres.m_difficulty.ToString());
			}
			woeurl.AddParameter("limit", _limit);
			HttpC httpC = Request.Get(woeurl, "MINIGAME_SEARCH", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
			httpC.objectData = _parametres;
			return httpC;
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x00151AE4 File Offset: 0x0014FEE4
		public static HttpC GetFolloweeMinigames(Action<PsMinigameMetaData[]> _okCallback, Action<HttpC> _failureCallback, int limit = 50, Action _errorCallback = null)
		{
			ResponseHandler<PsMinigameMetaData[]> responseHandler = new ResponseHandler<PsMinigameMetaData[]>(_okCallback, new Func<HttpC, PsMinigameMetaData[]>(ClientTools.ParseMinigameList));
			WOEURL woeurl = new WOEURL("/v1/minigame/followee/find");
			woeurl.AddParameter("allGameModes", "true");
			woeurl.AddParameter("limit", limit);
			return Request.Get(woeurl, "MINIGAME_FOLLOWEE_LIST_DOWNLOAD", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x00151B5C File Offset: 0x0014FF5C
		public static HttpC Get(string _id, Action<PsMinigameMetaData> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/minigame/meta/find");
			ResponseHandler<PsMinigameMetaData> responseHandler = new ResponseHandler<PsMinigameMetaData>(_okCallback, new Func<HttpC, PsMinigameMetaData>(ClientTools.ParseMinigameMetaData));
			woeurl.AddParameter("id", _id);
			HttpC httpC = Request.Get(woeurl, "MINIGAME_GET", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
			httpC.objectData = _id;
			return httpC;
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x00151BC8 File Offset: 0x0014FFC8
		public static HttpC GetWithItems(string _items, Action<PsMinigameMetaData[]> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			ResponseHandler<PsMinigameMetaData[]> responseHandler = new ResponseHandler<PsMinigameMetaData[]>(_okCallback, new Func<HttpC, PsMinigameMetaData[]>(ClientTools.ParseMinigameList));
			WOEURL woeurl = new WOEURL("/v1/minigame/meta/findItems");
			woeurl.AddParameter("items", _items);
			return Request.Get(woeurl, "MINIGAME_WITH_ITEMS", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x00151C2C File Offset: 0x0015002C
		public static HttpC GetFresh(Action<PsMinigameMetaData> _okCallback, Action<HttpC> _failureCallback, string _playerUnit, string _gamemode = "", Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/minigame/oneFresh");
			if (!string.IsNullOrEmpty(_playerUnit))
			{
				woeurl.AddParameter("playerUnit", _playerUnit);
			}
			if (!string.IsNullOrEmpty(_gamemode))
			{
				woeurl.AddParameter("gameMode", _gamemode);
			}
			ResponseHandler<PsMinigameMetaData> responseHandler = new ResponseHandler<PsMinigameMetaData>(_okCallback, new Func<HttpC, PsMinigameMetaData>(ClientTools.ParseMinigameMetaData));
			return Request.Get(woeurl, "MINIGAME_ONE_FRESH_DOWNLOAD", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x00151CB0 File Offset: 0x001500B0
		public static HttpC GetHistory(string _playerId, Action<PsMinigameMetaData[]> _okCallback, Action<HttpC> _failureCallback, int _limit = 20, Action _errorCallback = null)
		{
			ResponseHandler<PsMinigameMetaData[]> responseHandler = new ResponseHandler<PsMinigameMetaData[]>(_okCallback, new Func<HttpC, PsMinigameMetaData[]>(ClientTools.ParseMinigameList));
			WOEURL woeurl = new WOEURL("/v1/minigame/history");
			woeurl.AddParameter("playerId", _playerId);
			woeurl.AddParameter("limit", _limit);
			return Request.Get(woeurl, "MINIGAME_HISTORY_LIST_DOWNLOAD", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x00151D24 File Offset: 0x00150124
		public static HttpC GetOwn(Action<OwnLevelsInfo> _okCallback, Action<HttpC> _failureCallback, int _limit = 50, Action _errorCallback = null)
		{
			ResponseHandler<OwnLevelsInfo> responseHandler = new ResponseHandler<OwnLevelsInfo>(_okCallback, new Func<HttpC, OwnLevelsInfo>(ClientTools.ParseOwnMinigames));
			WOEURL woeurl = new WOEURL("/v2/minigame/own");
			woeurl.AddParameter("limit", _limit);
			return Request.Get(woeurl, "MINIGAME_OWN_FIND", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x00151D8C File Offset: 0x0015018C
		public static HttpC GetOwnPublished(Action<PsMinigameMetaData[]> _okCallback, Action<HttpC> _failureCallback, int _limit = 50, Action _errorCallback = null)
		{
			ResponseHandler<PsMinigameMetaData[]> responseHandler = new ResponseHandler<PsMinigameMetaData[]>(_okCallback, new Func<HttpC, PsMinigameMetaData[]>(ClientTools.ParseMinigameList));
			WOEURL woeurl = new WOEURL("/v1/minigame/own/published");
			woeurl.AddParameter("limit", _limit);
			return Request.Get(woeurl, "MINIGAME_OWN_PUBLISHED_LIST_DOWNLOAD", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x00151DF4 File Offset: 0x001501F4
		public static HttpC GetOwnSaved(Action<PsMinigameMetaData[]> _okCallback, Action<HttpC> _failureCallback, int _limit = 50, Action _errorCallback = null)
		{
			ResponseHandler<PsMinigameMetaData[]> responseHandler = new ResponseHandler<PsMinigameMetaData[]>(_okCallback, new Func<HttpC, PsMinigameMetaData[]>(ClientTools.ParseMinigameList));
			WOEURL woeurl = new WOEURL("/v1/minigame/own/saved");
			woeurl.AddParameter("limit", _limit);
			return Request.Get(woeurl, "MINIGAME_OWN_SAVED_LIST_DOWNLOAD", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x00151E5C File Offset: 0x0015025C
		public static HttpC GetLevelData(string _gameId, Action<byte[]> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			ResponseHandler<byte[]> responseHandler = new ResponseHandler<byte[]>(_okCallback, new Func<HttpC, byte[]>(ClientTools.ParseBytes));
			WOEURL woeurl = new WOEURL("/v1/minigame/data/find");
			woeurl.AddParameter("id", _gameId);
			return Request.Get(woeurl, "MINIGAME_DOWNLOAD", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x00151EC0 File Offset: 0x001502C0
		public static HttpC OverrideData(string _gameId, byte[] _data, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/minigame/data/save");
			woeurl.AddParameter("id", _gameId);
			return Request.Post(woeurl, "OVERRIDE_MINIGAME_DATA", _data, _okCallback, _failureCallback, _errorCallback, null, null);
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x00151EF8 File Offset: 0x001502F8
		public static HttpC ClaimReward(string _gameId, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/minigame/claim");
			List<string> list = new List<string>();
			woeurl.AddParameter("gameId", _gameId);
			string text = ClientTools.GeneratePlayerSetDataJson(new Hashtable(), ref list);
			return Request.Post(woeurl, "CLAIM_MINIGAME_REWARD", text, _okCallback, _failureCallback, _errorCallback, null, list.ToArray(), false);
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x00151F48 File Offset: 0x00150348
		public static HttpC ClaimAllRewards(Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/minigame/claimAll");
			List<string> list = new List<string>();
			string text = ClientTools.GeneratePlayerSetDataJson(new Hashtable(), ref list);
			return Request.Post(woeurl, "CLAIM_MINIGAME_REWARDS", text, _okCallback, _failureCallback, _errorCallback, null, list.ToArray(), false);
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x00151F8A File Offset: 0x0015038A
		public static HttpC SaveHidden(PsGameLoop _minigameInfo, byte[] _screenshotData, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, string _creationTime, Action _errorCallback = null)
		{
			return MiniGame.Save(_minigameInfo, _screenshotData, null, _okCallback, _failureCallback, MiniGame.HIDE, _creationTime, _errorCallback);
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x00151FA0 File Offset: 0x001503A0
		public static HttpC BackToSaved(PsGameLoop _minigameInfo, Dictionary<string, int> _editorResources, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/minigame/backtosaved");
			List<string> list = new List<string>();
			list.Add("SetData");
			Hashtable hashtable = new Hashtable();
			hashtable.Add("update", ClientTools.GeneratePlayerSetData(new Hashtable(), ref list));
			string text = Json.Serialize(hashtable);
			string[] array = list.ToArray();
			DataBlob dataBlob = ClientTools.CreateDataBlob(ClientTools.ActiveMiniGameJson(), text);
			ResponseHandler<HttpC> responseHandler = new ResponseHandler<HttpC>(delegate(HttpC c)
			{
				Player.SetDataOkCallback(c, _okCallback);
			}, new Func<HttpC, HttpC>(ClientTools.ParsePathSync));
			woeurl.AddParameter("id", _minigameInfo.GetGameId());
			woeurl.AddParameter("timestamp", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
			return Request.Post(woeurl, null, dataBlob.data, new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback, dataBlob.header, array);
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x00152097 File Offset: 0x00150497
		public static HttpC Save(PsGameLoop _minigameInfo, byte[] _screenshotData, Dictionary<string, int> _editorResources, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, bool _publish, string _creationTime, Action _errorCallback = null)
		{
			if (_publish)
			{
				return MiniGame.Save(_minigameInfo, _screenshotData, _editorResources, _okCallback, _failureCallback, MiniGame.PUBLISH, _creationTime, _errorCallback);
			}
			return MiniGame.Save(_minigameInfo, _screenshotData, _editorResources, _okCallback, _failureCallback, MiniGame.SAVE, _creationTime, _errorCallback);
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x001520CC File Offset: 0x001504CC
		private static HttpC Save(PsGameLoop _minigameInfo, byte[] _screenshotData, Dictionary<string, int> _editorResources, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, int _saveType, string _creationTime, Action _errorCallback = null)
		{
			Hashtable hashtable = new Hashtable();
			List<string> list = new List<string>();
			list.Add("SetData");
			if (_editorResources != null)
			{
				hashtable.Add("editorResources", _editorResources);
			}
			string text = Json.Serialize(hashtable);
			string[] array = list.ToArray();
			DataBlob dataBlob = ClientTools.CreateLevelDataBlob(_screenshotData, text);
			string text2;
			if (_saveType == MiniGame.PUBLISH)
			{
				text2 = "/v4/minigame/publish";
			}
			else if (_saveType == MiniGame.SAVE)
			{
				text2 = "/v2/minigame/save";
			}
			else
			{
				text2 = "/v1/minigame/hide";
			}
			WOEURL woeurl = new WOEURL(text2);
			ResponseHandler<HttpC> responseHandler = new ResponseHandler<HttpC>(delegate(HttpC c)
			{
				Player.SetDataOkCallback(c, _okCallback);
			}, new Func<HttpC, HttpC>(ClientTools.ParsePathSync));
			if (!string.IsNullOrEmpty(_minigameInfo.GetGameId()))
			{
				woeurl.AddParameter("id", _minigameInfo.GetGameId());
			}
			woeurl.AddParameter("timestamp", _creationTime);
			return Request.Post(woeurl, null, dataBlob.data, new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback, dataBlob.header, array);
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x001521F0 File Offset: 0x001505F0
		public static HttpC Publish(PsGameLoop _minigameInfo, byte[] _screenshotData, Hashtable _progression, Dictionary<string, int> _editorResources, DataBlob _ghostData, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, bool _hidden, string _creationTime, Action _errorCallback = null)
		{
			Hashtable hashtable = new Hashtable();
			List<string> list = new List<string>();
			list.Add("SetData");
			if (_editorResources != null)
			{
				hashtable.Add("editorResources", _editorResources);
			}
			if (_progression != null)
			{
				hashtable.Add("progression", _progression);
			}
			hashtable.Add("version", 3);
			string text = Json.Serialize(hashtable);
			string[] array = list.ToArray();
			DataBlob dataBlob = ClientTools.CreatePublishLevelDataBlob(_screenshotData, text, _ghostData);
			string text2;
			if (!_hidden)
			{
				text2 = "/v4/minigame/publish";
			}
			else
			{
				text2 = "/v1/minigame/hide";
			}
			WOEURL woeurl = new WOEURL(text2);
			ResponseHandler<HttpC> responseHandler = new ResponseHandler<HttpC>(delegate(HttpC c)
			{
				Player.SetDataOkCallback(c, _okCallback);
			}, new Func<HttpC, HttpC>(ClientTools.ParsePathSync));
			if (!string.IsNullOrEmpty(_minigameInfo.GetGameId()))
			{
				woeurl.AddParameter("id", _minigameInfo.GetGameId());
			}
			woeurl.AddParameter("timestamp", _creationTime);
			return Request.Post(woeurl, null, dataBlob.data, new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback, dataBlob.header, array);
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x00152320 File Offset: 0x00150720
		public static HttpC Delete(string _gameId, Dictionary<string, int> _editorResources, Action<HttpC> _okCallback = null, Action<HttpC> _failureCallback = null, Action _errorCallback = null)
		{
			Debug.Log("Delete minigame " + _gameId, null);
			int second = DateTime.Now.Second;
			WOEURL woeurl = new WOEURL("/v1/minigame/delete");
			woeurl.AddParameter("id", _gameId);
			woeurl.AddParameter("time", second);
			woeurl.AddParameter("hash", ClientTools.GenerateHash(_gameId + second));
			Hashtable hashtable = new Hashtable();
			List<string> list = new List<string>();
			list.Add("SetData");
			if (_editorResources != null)
			{
				hashtable.Add("editorResources", _editorResources);
			}
			string text = Json.Serialize(hashtable);
			string[] array = list.ToArray();
			ResponseHandler<HttpC> responseHandler = new ResponseHandler<HttpC>(delegate(HttpC c)
			{
				Player.SetDataOkCallback(c, _okCallback);
			}, new Func<HttpC, HttpC>(ClientTools.ParsePathSync));
			return Request.Post(woeurl, null, text, new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback, null, array, false);
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x0015242C File Offset: 0x0015082C
		public static HttpC GetPublishedByPlayerId(string _playerId, Action<PsMinigameMetaData[]> _okCallback, Action<HttpC> _failureCallback, int _limit = 50, Action _errorCallback = null)
		{
			ResponseHandler<PsMinigameMetaData[]> responseHandler = new ResponseHandler<PsMinigameMetaData[]>(_okCallback, new Func<HttpC, PsMinigameMetaData[]>(ClientTools.ParseMinigameList));
			WOEURL woeurl = new WOEURL("/v1/minigame/followee/published");
			woeurl.AddParameter("playerId", _playerId);
			woeurl.AddParameter("limit", _limit);
			return Request.Get(woeurl, "MINIGAME_FOLLOWEE_PUBLISHED_LIST_DOWNLOAD", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x0400206C RID: 8300
		private static int PUBLISH = 1;

		// Token: 0x0400206D RID: 8301
		private static int SAVE = 2;

		// Token: 0x0400206E RID: 8302
		private static int HIDE = 3;

		// Token: 0x0200042C RID: 1068
		public class DeleteMinigameData
		{
			// Token: 0x04002080 RID: 8320
			public string minigameId;

			// Token: 0x04002081 RID: 8321
			public Dictionary<string, int> editorResources;
		}
	}
}
