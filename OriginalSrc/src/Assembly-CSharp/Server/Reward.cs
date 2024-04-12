using System;
using System.Collections.Generic;

namespace Server
{
	// Token: 0x02000439 RID: 1081
	public static class Reward
	{
		// Token: 0x06001E08 RID: 7688 RVA: 0x001559FC File Offset: 0x00153DFC
		public static HttpC ClaimRewards(List<PsMinigameMetaData> _gamesToClaim, Action<RewardData> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
		{
			WOEURL woeurl = new WOEURL("/v1/reward/claim");
			string text = ClientTools.GenerateIdListJson(_gamesToClaim);
			ResponseHandler<RewardData> responseHandler = new ResponseHandler<RewardData>(_okCallback, new Func<HttpC, RewardData>(ClientTools.ParseRewardData));
			return Request.Post(woeurl, "REWARD_CLAIM", text, new Action<HttpC>(responseHandler.RequestOk), _failCallback, _errorCallback, null, null, false);
		}
	}
}
