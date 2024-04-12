using System;
using System.Collections.Generic;
using UnityEngine;

namespace Server
{
	// Token: 0x02000462 RID: 1122
	public static class Youtube
	{
		// Token: 0x06001EE3 RID: 7907 RVA: 0x0015AD78 File Offset: 0x00159178
		public static HttpC GetChannels(string _name, string _id, Action<List<YoutubeChannelInfo>> _okCallback, Action<HttpC> _failureCallback = null, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/youtube/getChannels");
			if (!string.IsNullOrEmpty(_name))
			{
				woeurl.AddParameter("name", WWW.EscapeURL(_name));
			}
			else
			{
				woeurl.AddParameter("id", WWW.EscapeURL(_id));
			}
			ResponseHandler<List<YoutubeChannelInfo>> responseHandler = new ResponseHandler<List<YoutubeChannelInfo>>(_okCallback, new Func<HttpC, List<YoutubeChannelInfo>>(Youtube.ParseChannelsFromJson));
			return Request.Get(woeurl, null, new Action<HttpC>(responseHandler.RequestOk), _failureCallback, _errorCallback);
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x0015ADFC File Offset: 0x001591FC
		public static List<YoutubeChannelInfo> ParseChannelsFromJson(HttpC _c)
		{
			List<YoutubeChannelInfo> list = new List<YoutubeChannelInfo>();
			Dictionary<string, object> dictionary = ClientTools.ParseServerResponse(_c.www.text);
			if (dictionary.ContainsKey("data"))
			{
				List<object> list2 = dictionary["data"] as List<object>;
				foreach (object obj in list2)
				{
					YoutubeChannelInfo youtubeChannelInfo = default(YoutubeChannelInfo);
					Dictionary<string, object> dictionary2 = obj as Dictionary<string, object>;
					if (dictionary2.ContainsKey("id"))
					{
						youtubeChannelInfo.id = (string)dictionary2["id"];
					}
					else
					{
						youtubeChannelInfo.id = string.Empty;
					}
					if (dictionary2.ContainsKey("title"))
					{
						youtubeChannelInfo.title = (string)dictionary2["title"];
					}
					else
					{
						youtubeChannelInfo.title = string.Empty;
					}
					if (dictionary2.ContainsKey("description"))
					{
						youtubeChannelInfo.description = (string)dictionary2["description"];
					}
					else
					{
						youtubeChannelInfo.description = string.Empty;
					}
					if (dictionary2.ContainsKey("thumbnail"))
					{
						youtubeChannelInfo.thumbnail = (string)dictionary2["thumbnail"];
					}
					else
					{
						youtubeChannelInfo.thumbnail = null;
					}
					if (dictionary2.ContainsKey("statistics"))
					{
						Dictionary<string, object> dictionary3 = dictionary2["statistics"] as Dictionary<string, object>;
						if (dictionary3.ContainsKey("subscriberCount"))
						{
							youtubeChannelInfo.subscribers = Convert.ToInt32(dictionary3["subscriberCount"]);
						}
					}
					list.Add(youtubeChannelInfo);
				}
			}
			return list;
		}
	}
}
