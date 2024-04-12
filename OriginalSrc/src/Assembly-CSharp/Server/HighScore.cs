using System;
using UnityEngine;

namespace Server
{
	// Token: 0x02000420 RID: 1056
	public static class HighScore
	{
		// Token: 0x06001D5D RID: 7517 RVA: 0x0014F630 File Offset: 0x0014DA30
		public static HttpC GetNext(PsMinigameMetaData _meta, int _time, Action<HighscoreDataEntry> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/highscore/next");
			woeurl.AddParameter("gameId", _meta.id);
			woeurl.AddParameter("gameMode", _meta.gameMode);
			woeurl.AddParameter("time", _time);
			ResponseHandler<HighscoreDataEntry> responseHandler = new ResponseHandler<HighscoreDataEntry>(_okCallback, new Func<HttpC, HighscoreDataEntry>(HighScores.ParseDataEntryJSON));
			return Request.Get(woeurl, "GAME_FEED_LIST_DOWNLOAD", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x0014F6C0 File Offset: 0x0014DAC0
		public static HttpC Get(PsMinigameMetaData _meta, Action<ScoreTable> _okCallback, Action<HttpC> _failureCallback, int limit = 100, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v2/highscore/find");
			woeurl.AddParameter("gameId", _meta.id);
			woeurl.AddParameter("gameMode", _meta.gameMode);
			woeurl.AddParameter("limit", limit);
			ResponseHandler<ScoreTable> responseHandler = new ResponseHandler<ScoreTable>(_okCallback, new Func<HttpC, ScoreTable>(HighScores.ParseScoreTableJSON));
			return Request.Get(woeurl, "FIND_HIGHSCORES", new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x0014F750 File Offset: 0x0014DB50
		public static HttpC SendHighscoreAndGhostSafe(HighScore.HighScoreAndGhostData _data, Action<ScoreTable> _okCallback = null, Action<HttpC> _failEventHandler = null, Action _errorCallback = null)
		{
			PsMetricsData.m_lastGhostSize = _data.ghostSize;
			return HighScore.Send(_data.context, _data.elapsedTime, _data.lastSentTimeScore, _data.lastSentScore, _data.starts, _data.metadata, _data.ghostData, _data.upgradeSum, _data.playerUnit, _okCallback, _failEventHandler, _errorCallback, _data.mainpath, _data.boosterUsed, _data.deathCount);
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x0014F7B8 File Offset: 0x0014DBB8
		private static HttpC Send(PsMinigameContext _context, long _timeSpentInGame, int _time, int _stars, int _starts, PsMinigameMetaData _meta, DataBlob _ghostData, int _upgradeSum, string _playerUnit, Action<ScoreTable> _okCallback = null, Action<HttpC> _failEventHandler = null, Action _errorCallback = null, bool _mainPath = false, bool _boost = false, int _deathCount = 0)
		{
			ResponseHandler<ScoreTable> responseHandler = new ResponseHandler<ScoreTable>(_okCallback, new Func<HttpC, ScoreTable>(HighScores.ParseScoreTableJSON));
			WOEURL woeurl = new WOEURL("/v5/highscore/send");
			woeurl.AddParameter("gameId", _meta.id);
			woeurl.AddParameter("starts", _starts);
			string text = string.Empty;
			if (_time != 0)
			{
				woeurl.AddParameter("time", _time.ToString());
				text = _time.ToString();
			}
			woeurl.AddParameter("stars", _stars.ToString());
			woeurl.AddParameter("hash", ClientTools.GenerateHash(_meta.id + PlayerPrefsX.GetUserId() + text + _stars.ToString()));
			woeurl.AddParameter("name", WWW.EscapeURL(PlayerPrefsX.GetUserName()));
			woeurl.AddParameter("returnScores", "false");
			woeurl.AddParameter("gameMode", _meta.gameMode);
			woeurl.AddParameter("mainPath", _mainPath.ToString().ToLower());
			woeurl.AddParameter("playerUnit", _playerUnit);
			woeurl.AddParameter("boost", _boost.ToString().ToLower());
			woeurl.AddParameter("upgradeSum", _upgradeSum.ToString());
			woeurl.AddParameter("deathCount", _deathCount);
			Debug.LogWarning(woeurl.ConstructURL());
			HttpC httpC = Request.Post(woeurl, null, (_ghostData.data == null) ? new byte[1] : _ghostData.data, new Action<HttpC>(responseHandler.RequestOk), _failEventHandler, _errorCallback, null, new string[] { "Highscore" });
			httpC.objectData = _ghostData;
			return httpC;
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x0014F9A0 File Offset: 0x0014DDA0
		public static HttpC SendQuit(PsMinigameContext _context, long _timeSpentInGame, int _starts, PsMinigameMetaData _meta, Action<HttpC> _okCallback = null, Action<HttpC> _failEventHandler = null, Action _errorCallback = null, bool _mainPath = false, int _deathCount = 0)
		{
			WOEURL woeurl = new WOEURL("/v2/highscore/quit");
			woeurl.AddParameter("gameId", _meta.id);
			woeurl.AddParameter("starts", _starts);
			woeurl.AddParameter("hash", ClientTools.GenerateHash(_meta.id + PlayerPrefsX.GetUserId() + _starts));
			woeurl.AddParameter("gameMode", _meta.gameMode);
			woeurl.AddParameter("mainPath", _mainPath.ToString().ToLower());
			woeurl.AddParameter("playerUnit", _meta.playerUnit);
			woeurl.AddParameter("deathCount", _deathCount);
			Debug.LogWarning(woeurl.ConstructURL());
			return Request.Post(woeurl, null, _okCallback, _failEventHandler, _errorCallback, null, new string[] { "Quit" });
		}

		// Token: 0x02000421 RID: 1057
		public class HighScoreAndGhostData
		{
			// Token: 0x06001D62 RID: 7522 RVA: 0x0014FA80 File Offset: 0x0014DE80
			public HighScoreAndGhostData(PsMinigameContext _context, long _elapsedTime, DataBlob _ghostData, int _ghostSize, PsMinigameMetaData _metadata, int _lastSentTimeScore, int _lastSentScore, bool _boosterUsed, string _playerUnit, bool _mainpath = false, int _upgradeSum = 0, int _deathCount = 0, int _starts = 1)
			{
				this.context = _context;
				this.elapsedTime = _elapsedTime;
				this.ghostData = _ghostData;
				this.ghostSize = _ghostSize;
				this.metadata = _metadata;
				this.lastSentTimeScore = _lastSentTimeScore;
				this.lastSentScore = _lastSentScore;
				this.boosterUsed = _boosterUsed;
				this.mainpath = _mainpath;
				this.upgradeSum = _upgradeSum;
				this.deathCount = _deathCount;
				this.starts = _starts;
				this.playerUnit = _playerUnit;
			}

			// Token: 0x04002037 RID: 8247
			public PsMinigameContext context;

			// Token: 0x04002038 RID: 8248
			public long elapsedTime;

			// Token: 0x04002039 RID: 8249
			public DataBlob ghostData;

			// Token: 0x0400203A RID: 8250
			public int ghostSize;

			// Token: 0x0400203B RID: 8251
			public PsMinigameMetaData metadata;

			// Token: 0x0400203C RID: 8252
			public int lastSentTimeScore;

			// Token: 0x0400203D RID: 8253
			public int lastSentScore;

			// Token: 0x0400203E RID: 8254
			public bool boosterUsed;

			// Token: 0x0400203F RID: 8255
			public bool mainpath;

			// Token: 0x04002040 RID: 8256
			public int upgradeSum;

			// Token: 0x04002041 RID: 8257
			public int deathCount;

			// Token: 0x04002042 RID: 8258
			public int starts;

			// Token: 0x04002043 RID: 8259
			public string playerUnit;
		}
	}
}
