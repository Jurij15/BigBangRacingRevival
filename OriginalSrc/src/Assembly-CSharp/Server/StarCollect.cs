using System;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

namespace Server
{
	// Token: 0x0200044D RID: 1101
	public static class StarCollect
	{
		// Token: 0x06001E8A RID: 7818 RVA: 0x00158674 File Offset: 0x00156A74
		public static HttpC Win(StarCollect.StarCollectData _data, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/starcollect/win");
			Hashtable hashtable = new Hashtable();
			hashtable.Add("gameId", _data.m_gameId);
			hashtable.Add("time", _data.m_time);
			hashtable.Add("playerUnit", _data.m_playerUnit);
			hashtable.Add("starts", _data.m_starts);
			hashtable.Add("stars", _data.m_stars);
			hashtable.Add("version", 3);
			List<string> list = new List<string>();
			Hashtable hashtable2 = ClientTools.GeneratePlayerSetData(new Hashtable(), ref list);
			hashtable.Add("update", hashtable2);
			if (_data.m_pathJson != null)
			{
				hashtable.Add("progression", _data.m_pathJson);
			}
			string text = Json.Serialize(hashtable);
			string[] array = list.ToArray();
			DataBlob dataBlob = default(DataBlob);
			if (_data.m_ghostData.data != null)
			{
				dataBlob = ClientTools.CreateDataBlob(text, _data.m_ghostData);
			}
			else
			{
				dataBlob = ClientTools.CreateDataBlob(text);
			}
			return Request.Post(woeurl, "STARCOLLECT_WIN", dataBlob.data, _okCallback, _failureCallback, _errorCallback, dataBlob.header, array);
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x001587A8 File Offset: 0x00156BA8
		public static HttpC Lose(SendQuitData _data, Hashtable _pathJson, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/starcollect/lose");
			Hashtable hashtable = new Hashtable();
			hashtable.Add("gameId", _data.gameLoop.m_minigameId);
			hashtable.Add("starts", _data.startCount);
			hashtable.Add("playerUnit", _data.playerUnit);
			List<string> list = new List<string>();
			Hashtable hashtable2 = ClientTools.GeneratePlayerSetData(new Hashtable(), ref list);
			hashtable.Add("update", hashtable2);
			if (_pathJson != null)
			{
				hashtable.Add("progression", _pathJson);
			}
			string text = Json.Serialize(hashtable);
			string[] array = list.ToArray();
			return Request.Post(woeurl, "STARCOLLECT_LOSE", text, _okCallback, _failureCallback, _errorCallback, null, array, false);
		}

		// Token: 0x0200044E RID: 1102
		public class StarCollectData
		{
			// Token: 0x06001E8C RID: 7820 RVA: 0x0015885C File Offset: 0x00156C5C
			public StarCollectData(string _gameId, int _starts, string _playerUnit, int _stars, int _time, DataBlob _ghostData, Hashtable _pathJson, int _ghostSize)
			{
				this.m_gameId = _gameId;
				this.m_starts = _starts;
				this.m_playerUnit = _playerUnit;
				this.m_stars = _stars;
				this.m_time = _time;
				this.m_ghostData = _ghostData;
				this.m_pathJson = _pathJson;
				this.m_ghostSize = _ghostSize;
			}

			// Token: 0x040021BD RID: 8637
			public string m_gameId;

			// Token: 0x040021BE RID: 8638
			public int m_starts;

			// Token: 0x040021BF RID: 8639
			public string m_playerUnit;

			// Token: 0x040021C0 RID: 8640
			public int m_stars;

			// Token: 0x040021C1 RID: 8641
			public int m_time;

			// Token: 0x040021C2 RID: 8642
			public DataBlob m_ghostData;

			// Token: 0x040021C3 RID: 8643
			public Hashtable m_pathJson;

			// Token: 0x040021C4 RID: 8644
			public int m_ghostSize;
		}
	}
}
