using System;
using MiniJSON;

namespace Server
{
	// Token: 0x02000436 RID: 1078
	public static class Rating
	{
		// Token: 0x06001DF7 RID: 7671 RVA: 0x001555F0 File Offset: 0x001539F0
		public static HttpC Save(PsMetagameManager.SaveRatingData _data, Action<HttpC> _okCallback, Action<HttpC> _failureCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/rating/save");
			woeurl.AddParameter("gameId", _data.m_minigameId);
			woeurl.AddParameter("rating", _data.m_rating);
			woeurl.AddParameter("victory", _data.m_playerReachedGoal.ToString().ToLower());
			woeurl.AddParameter("playerLevel", _data.m_playerLevel);
			woeurl.AddParameter("playerUnit", _data.m_playerUnitName);
			woeurl.AddParameter("skipped", _data.m_playerSkipped.ToString().ToLower());
			woeurl.AddParameter("fresh", _data.m_isFresh.ToString().ToLower());
			if (_data.m_ghostCount != -1)
			{
				woeurl.AddParameter("ghostCount", _data.m_ghostCount);
			}
			if (_data.m_ghostsWon != -1)
			{
				woeurl.AddParameter("ghostsWon", _data.m_ghostsWon);
			}
			if (_data.setData != null)
			{
				if (_data.setData.ContainsKey("hash"))
				{
					_data.setData["hash"] = ClientTools.GenerateHash(string.Empty);
				}
				else
				{
					_data.setData.Add("hash", ClientTools.GenerateHash(string.Empty));
				}
				string text = Json.Serialize(_data.setData);
				return Request.Post(woeurl, null, text, _okCallback, _failureCallback, _errorCallback, null, new string[] { "Rating" }, false);
			}
			return Request.Post(woeurl, null, _okCallback, _failureCallback, _errorCallback, null, new string[] { "Rating" });
		}
	}
}
