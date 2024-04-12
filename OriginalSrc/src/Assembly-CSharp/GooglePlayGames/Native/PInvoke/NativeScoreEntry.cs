using System;
using System.Runtime.InteropServices;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006F8 RID: 1784
	internal class NativeScoreEntry : BaseReferenceHolder
	{
		// Token: 0x06003379 RID: 13177 RVA: 0x001CC06A File Offset: 0x001CA46A
		internal NativeScoreEntry(IntPtr selfPtr)
			: base(selfPtr)
		{
		}

		// Token: 0x0600337A RID: 13178 RVA: 0x001CC073 File Offset: 0x001CA473
		protected override void CallDispose(HandleRef selfPointer)
		{
			ScorePage.ScorePage_Entry_Dispose(selfPointer);
		}

		// Token: 0x0600337B RID: 13179 RVA: 0x001CC07B File Offset: 0x001CA47B
		internal ulong GetLastModifiedTime()
		{
			return ScorePage.ScorePage_Entry_LastModifiedTime(base.SelfPtr());
		}

		// Token: 0x0600337C RID: 13180 RVA: 0x001CC088 File Offset: 0x001CA488
		internal string GetPlayerId()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr out_size) => ScorePage.ScorePage_Entry_PlayerId(base.SelfPtr(), out_string, out_size));
		}

		// Token: 0x0600337D RID: 13181 RVA: 0x001CC09B File Offset: 0x001CA49B
		internal NativeScore GetScore()
		{
			return new NativeScore(ScorePage.ScorePage_Entry_Score(base.SelfPtr()));
		}

		// Token: 0x0600337E RID: 13182 RVA: 0x001CC0B0 File Offset: 0x001CA4B0
		internal PlayGamesScore AsScore(string leaderboardId)
		{
			DateTime dateTime;
			dateTime..ctor(1970, 1, 1, 0, 0, 0, 0, 1);
			ulong num = this.GetLastModifiedTime();
			if (num == 18446744073709551615UL)
			{
				num = 0UL;
			}
			DateTime dateTime2 = dateTime.AddMilliseconds(num);
			return new PlayGamesScore(dateTime2, leaderboardId, this.GetScore().GetRank(), this.GetPlayerId(), this.GetScore().GetValue(), this.GetScore().GetMetadata());
		}

		// Token: 0x040032E4 RID: 13028
		private const ulong MinusOne = 18446744073709551615UL;
	}
}
