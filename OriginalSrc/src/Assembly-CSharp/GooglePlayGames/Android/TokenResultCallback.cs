using System;
using Com.Google.Android.Gms.Common.Api;

namespace GooglePlayGames.Android
{
	// Token: 0x0200060E RID: 1550
	internal class TokenResultCallback : ResultCallbackProxy<TokenResult>
	{
		// Token: 0x06002D72 RID: 11634 RVA: 0x001C1832 File Offset: 0x001BFC32
		public TokenResultCallback(Action<int, string, string, string> callback)
		{
			this.callback = callback;
		}

		// Token: 0x06002D73 RID: 11635 RVA: 0x001C1841 File Offset: 0x001BFC41
		public override void OnResult(TokenResult arg_Result_1)
		{
			if (this.callback != null)
			{
				this.callback.Invoke(arg_Result_1.getStatusCode(), arg_Result_1.getAuthCode(), arg_Result_1.getEmail(), arg_Result_1.getIdToken());
			}
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x001C1871 File Offset: 0x001BFC71
		public override string toString()
		{
			return this.ToString();
		}

		// Token: 0x04003173 RID: 12659
		private Action<int, string, string, string> callback;
	}
}
