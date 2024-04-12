using System;
using System.Collections;
using System.Collections.Generic;

namespace Server
{
	// Token: 0x02000460 RID: 1120
	public static class Versus
	{
		// Token: 0x06001EDB RID: 7899 RVA: 0x0015A934 File Offset: 0x00158D34
		public static HttpC Create(Action<VersusMetaData> _okCallback = null, Action<HttpC> _failEventHandler = null, string _gameId = null, Action _errorCallback = null)
		{
			List<string> list = new List<string>();
			ResponseHandler<VersusMetaData> responseHandler = new ResponseHandler<VersusMetaData>(_okCallback, new Func<HttpC, VersusMetaData>(ClientTools.ParseVersusMetaData));
			WOEURL woeurl = new WOEURL("/v4/versus/create");
			if (!string.IsNullOrEmpty(_gameId))
			{
				woeurl.AddParameter("gameId", _gameId);
			}
			Debug.Log(woeurl.ConstructURL(), null);
			string text = ClientTools.GeneratePlayerSetDataJson(new Hashtable(), ref list);
			return Request.Post(woeurl, null, text, new Action<HttpC>(responseHandler.RequestOk), _failEventHandler, _errorCallback, null, null, false);
		}

		// Token: 0x06001EDC RID: 7900 RVA: 0x0015A9C4 File Offset: 0x00158DC4
		public static HttpC OverwriteScore(string _challengeId, int _time, int _starts, string _gameId, bool _winRound, bool _loseRound, int _upgradeSum, string _playerUnit, DataBlob _dataBlob, Action<HttpC> _okCallback = null, Action<HttpC> _failEventHandler = null, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v2/versus/score/overwrite");
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
			Debug.Log(woeurl.ConstructURL(), null);
			HttpC httpC = Request.Post(woeurl, null, (_dataBlob.data == null) ? new byte[1] : _dataBlob.data, _okCallback, _failEventHandler, _errorCallback, null, new string[] { "Highscore" });
			httpC.objectData = _dataBlob;
			return httpC;
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x0015AAFC File Offset: 0x00158EFC
		public static HttpC Quit(string _challengeId, string _gameId, int _starts, bool _winRound, bool _loseRound, Action<HttpC> _okCallback = null, Action<HttpC> _failEventHandler = null, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/versus/score/quit");
			woeurl.AddParameter("challengeId", _challengeId);
			woeurl.AddParameter("gameId", _gameId);
			woeurl.AddParameter("starts", _starts);
			woeurl.AddParameter("winRound", _winRound.ToString().ToLower());
			woeurl.AddParameter("loseRound", _loseRound.ToString().ToLower());
			return Request.Post(woeurl, null, new byte[1], _okCallback, _failEventHandler, _errorCallback, null, null);
		}

		// Token: 0x06001EDE RID: 7902 RVA: 0x0015AB90 File Offset: 0x00158F90
		public static HttpC Win(string _challengeId, string _gameId, int _starts, Action<HttpC> _okCallback = null, Action<HttpC> _failEventHandler = null, Action _errorCallback = null)
		{
			List<string> list = new List<string>();
			WOEURL woeurl = new WOEURL("/v1/versus/win");
			woeurl.AddParameter("challengeId", _challengeId);
			woeurl.AddParameter("gameId", _gameId);
			woeurl.AddParameter("starts", _starts);
			string text = ClientTools.GeneratePlayerSetDataJson(new Hashtable(), ref list);
			return Request.Post(woeurl, null, text, _okCallback, _failEventHandler, _errorCallback, null, null, false);
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x0015ABF8 File Offset: 0x00158FF8
		public static HttpC Forfeit(string _challengeId, string _gameId, int _starts, Action<HttpC> _okCallback = null, Action<HttpC> _failEventHandler = null, Action _errorCallback = null)
		{
			List<string> list = new List<string>();
			WOEURL woeurl = new WOEURL("/v1/versus/forfeit");
			woeurl.AddParameter("challengeId", _challengeId);
			woeurl.AddParameter("gameId", _gameId);
			woeurl.AddParameter("starts", _starts);
			string text = ClientTools.GeneratePlayerSetDataJson(new Hashtable(), ref list);
			return Request.Post(woeurl, null, text, _okCallback, _failEventHandler, _errorCallback, null, null, false);
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x0015AC60 File Offset: 0x00159060
		public static HttpC SetTries(string _challengeId, int _tries, Action<HttpC> _okCallback = null, Action<HttpC> _failEventHandler = null, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/versus/tries/set");
			woeurl.AddParameter("challengeId", _challengeId);
			woeurl.AddParameter("tries", _tries);
			return Request.Post(woeurl, null, new byte[1], _okCallback, _failEventHandler, _errorCallback, null, new string[] { "setTries" });
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x0015ACB8 File Offset: 0x001590B8
		public static HttpC Claim(string _challengeId, Action<HttpC> _okCallback = null, Action<HttpC> _failEventHandler = null, Action _errorCallback = null)
		{
			List<string> list = new List<string>();
			WOEURL woeurl = new WOEURL("/v1/versus/claim");
			woeurl.AddParameter("challengeId", _challengeId);
			string text = ClientTools.GeneratePlayerSetDataJson(new Hashtable(), ref list);
			return Request.Post(woeurl, null, text, _okCallback, _failEventHandler, _errorCallback, null, null, false);
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x0015AD00 File Offset: 0x00159100
		public static HttpC ClaimAndSave(string _challengeId, string _pathJson, Action<HttpC> _okCallback = null, Action<HttpC> _failEventHandler = null, Action _errorCallback = null)
		{
			List<string> list = new List<string>();
			WOEURL woeurl = new WOEURL("/v1/versus/claimAndSave");
			woeurl.AddParameter("challengeId", _challengeId);
			string text = ClientTools.GeneratePlayerSetDataJson(new Hashtable(), ref list);
			string text2 = string.Concat(new string[] { "{\"update\":", text, ",\"progression\":", _pathJson, "}" });
			return Request.Post(woeurl, null, text2, _okCallback, _failEventHandler, _errorCallback, null, null, false);
		}
	}
}
