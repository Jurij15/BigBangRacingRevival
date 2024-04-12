using System;
using System.Collections;
using System.Collections.Generic;

namespace Server
{
	// Token: 0x0200041A RID: 1050
	public static class Friendly
	{
		// Token: 0x06001D25 RID: 7461 RVA: 0x0014CD54 File Offset: 0x0014B154
		public static HttpC Create(string _gameId, string _friendId, Action<VersusMetaData> _okCallback = null, Action<HttpC> _failEventHandler = null, string _comment = "", Action _errorCallback = null)
		{
			List<string> list = new List<string>();
			ResponseHandler<VersusMetaData> responseHandler = new ResponseHandler<VersusMetaData>(_okCallback, new Func<HttpC, VersusMetaData>(ClientTools.ParseVersusMetaData));
			WOEURL woeurl = new WOEURL("/v1/friendly/create");
			woeurl.AddParameter("gameId", _gameId);
			woeurl.AddParameter("challengedId", _friendId);
			woeurl.AddParameter("comment", _comment);
			Debug.Log(woeurl.ConstructURL(), null);
			string text = ClientTools.GeneratePlayerSetDataJson(new Hashtable(), ref list);
			return Request.Post(woeurl, null, text, new Action<HttpC>(responseHandler.RequestOk), _failEventHandler, _errorCallback, null, null, false);
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x0014CDF4 File Offset: 0x0014B1F4
		public static HttpC Join(string _challengeId, Action<VersusMetaData> _okCallback = null, Action<HttpC> _failEventHandler = null, Action _errorCallback = null)
		{
			List<string> list = new List<string>();
			ResponseHandler<VersusMetaData> responseHandler = new ResponseHandler<VersusMetaData>(_okCallback, new Func<HttpC, VersusMetaData>(ClientTools.ParseVersusMetaData));
			WOEURL woeurl = new WOEURL("/v1/friendly/join");
			woeurl.AddParameter("challengeId", _challengeId);
			Debug.Log(woeurl.ConstructURL(), null);
			string text = ClientTools.GeneratePlayerSetDataJson(new Hashtable(), ref list);
			return Request.Post(woeurl, null, text, new Action<HttpC>(responseHandler.RequestOk), _failEventHandler, _errorCallback, null, null, false);
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x0014CE78 File Offset: 0x0014B278
		public static HttpC Decline(string _challengeId, Action<VersusMetaData> _okCallback = null, Action<HttpC> _failEventHandler = null, Action _errorCallback = null)
		{
			List<string> list = new List<string>();
			ResponseHandler<VersusMetaData> responseHandler = new ResponseHandler<VersusMetaData>(_okCallback, new Func<HttpC, VersusMetaData>(ClientTools.ParseVersusMetaData));
			WOEURL woeurl = new WOEURL("/v1/friendly/decline");
			woeurl.AddParameter("challengeId", _challengeId);
			Debug.Log(woeurl.ConstructURL(), null);
			string text = ClientTools.GeneratePlayerSetDataJson(new Hashtable(), ref list);
			return Request.Post(woeurl, null, text, new Action<HttpC>(responseHandler.RequestOk), _failEventHandler, _errorCallback, null, null, false);
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x0014CEFC File Offset: 0x0014B2FC
		public static HttpC OverwriteScore(string _challengeId, int _time, int _starts, string _gameId, bool _winRound, bool _loseRound, int _upgradeSum, string _playerUnit, DataBlob _dataBlob, Action<HttpC> _okCallback = null, Action<HttpC> _failEventHandler = null, string _comment = "", Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/friendly/score/overwrite");
			woeurl.AddParameter("challengeId", _challengeId);
			woeurl.AddParameter("gameId", _gameId);
			woeurl.AddParameter("time", _time.ToString());
			woeurl.AddParameter("starts", _starts);
			string text = _time.ToString();
			woeurl.AddParameter("hash", ClientTools.GenerateHash(_challengeId + PlayerPrefsX.GetUserId() + text));
			woeurl.AddParameter("winRound", _winRound.ToString().ToLower());
			woeurl.AddParameter("loseRound", _loseRound.ToString().ToLower());
			woeurl.AddParameter("upgradeSum", _upgradeSum.ToString());
			woeurl.AddParameter("playerUnit", _playerUnit);
			woeurl.AddParameter("comment", _comment);
			Debug.Log(woeurl.ConstructURL(), null);
			HttpC httpC = Request.Post(woeurl, null, (_dataBlob.data == null) ? new byte[1] : _dataBlob.data, _okCallback, _failEventHandler, _errorCallback, null, new string[] { "Highscore" });
			httpC.objectData = _dataBlob;
			return httpC;
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x0014D040 File Offset: 0x0014B440
		public static HttpC Quit(string _challengeId, string _gameId, int _starts, bool _winRound, bool _loseRound, Action<HttpC> _okCallback = null, Action<HttpC> _failEventHandler = null, string _comment = "", Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/friendly/score/quit");
			woeurl.AddParameter("challengeId", _challengeId);
			woeurl.AddParameter("gameId", _gameId);
			woeurl.AddParameter("starts", _starts);
			woeurl.AddParameter("winRound", _winRound.ToString().ToLower());
			woeurl.AddParameter("loseRound", _loseRound.ToString().ToLower());
			woeurl.AddParameter("comment", _comment);
			return Request.Post(woeurl, null, new byte[1], _okCallback, _failEventHandler, _errorCallback, null, null);
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x0014D0E0 File Offset: 0x0014B4E0
		public static HttpC Win(string _challengeId, string _gameId, int _starts, Action<HttpC> _okCallback = null, Action<HttpC> _failEventHandler = null, Action _errorCallback = null)
		{
			List<string> list = new List<string>();
			WOEURL woeurl = new WOEURL("/v1/friendly/win");
			woeurl.AddParameter("challengeId", _challengeId);
			woeurl.AddParameter("gameId", _gameId);
			woeurl.AddParameter("starts", _starts);
			string text = ClientTools.GeneratePlayerSetDataJson(new Hashtable(), ref list);
			return Request.Post(woeurl, null, text, _okCallback, _failEventHandler, _errorCallback, null, null, false);
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x0014D148 File Offset: 0x0014B548
		public static HttpC Forfeit(string _challengeId, string _gameId, int _starts, Action<HttpC> _okCallback = null, Action<HttpC> _failEventHandler = null, Action _errorCallback = null)
		{
			List<string> list = new List<string>();
			WOEURL woeurl = new WOEURL("/v1/friendly/forfeit");
			woeurl.AddParameter("challengeId", _challengeId);
			woeurl.AddParameter("gameId", _gameId);
			woeurl.AddParameter("starts", _starts);
			string text = ClientTools.GeneratePlayerSetDataJson(new Hashtable(), ref list);
			return Request.Post(woeurl, null, text, _okCallback, _failEventHandler, _errorCallback, null, null, false);
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x0014D1B0 File Offset: 0x0014B5B0
		public static HttpC SetTries(string _challengeId, int _tries, Action<HttpC> _okCallback = null, Action<HttpC> _failEventHandler = null, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/friendly/tries/set");
			woeurl.AddParameter("challengeId", _challengeId);
			woeurl.AddParameter("tries", _tries);
			return Request.Post(woeurl, null, new byte[1], _okCallback, _failEventHandler, _errorCallback, null, new string[] { "setTries" });
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x0014D208 File Offset: 0x0014B608
		public static HttpC Claim(string _challengeId, Action<HttpC> _okCallback = null, Action<HttpC> _failEventHandler = null, Action _errorCallback = null)
		{
			List<string> list = new List<string>();
			WOEURL woeurl = new WOEURL("/v1/friendly/claim");
			woeurl.AddParameter("challengeId", _challengeId);
			string text = ClientTools.GeneratePlayerSetDataJson(new Hashtable(), ref list);
			return Request.Post(woeurl, null, text, _okCallback, _failEventHandler, _errorCallback, null, null, false);
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x0014D250 File Offset: 0x0014B650
		public static HttpC ClaimAndSave(string _challengeId, string _pathJson, Action<HttpC> _okCallback = null, Action<HttpC> _failEventHandler = null, Action _errorCallback = null)
		{
			List<string> list = new List<string>();
			WOEURL woeurl = new WOEURL("/v1/friendly/claimAndSave");
			woeurl.AddParameter("challengeId", _challengeId);
			string text = ClientTools.GeneratePlayerSetDataJson(new Hashtable(), ref list);
			string text2 = string.Concat(new string[] { "{\"update\":", text, ",\"progression\":", _pathJson, "}" });
			return Request.Post(woeurl, null, text2, _okCallback, _failEventHandler, _errorCallback, null, null, false);
		}
	}
}
