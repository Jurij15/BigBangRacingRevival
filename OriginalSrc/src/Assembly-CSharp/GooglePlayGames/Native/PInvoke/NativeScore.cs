using System;
using System.Runtime.InteropServices;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006F7 RID: 1783
	internal class NativeScore : BaseReferenceHolder
	{
		// Token: 0x06003370 RID: 13168 RVA: 0x001CBF95 File Offset: 0x001CA395
		internal NativeScore(IntPtr selfPtr)
			: base(selfPtr)
		{
		}

		// Token: 0x06003371 RID: 13169 RVA: 0x001CBF9E File Offset: 0x001CA39E
		protected override void CallDispose(HandleRef selfPointer)
		{
			Score.Score_Dispose(base.SelfPtr());
		}

		// Token: 0x06003372 RID: 13170 RVA: 0x001CBFAB File Offset: 0x001CA3AB
		internal ulong GetDate()
		{
			return ulong.MaxValue;
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x001CBFAF File Offset: 0x001CA3AF
		internal string GetMetadata()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr out_size) => Score.Score_Metadata(base.SelfPtr(), out_string, out_size));
		}

		// Token: 0x06003374 RID: 13172 RVA: 0x001CBFC2 File Offset: 0x001CA3C2
		internal ulong GetRank()
		{
			return Score.Score_Rank(base.SelfPtr());
		}

		// Token: 0x06003375 RID: 13173 RVA: 0x001CBFCF File Offset: 0x001CA3CF
		internal ulong GetValue()
		{
			return Score.Score_Value(base.SelfPtr());
		}

		// Token: 0x06003376 RID: 13174 RVA: 0x001CBFDC File Offset: 0x001CA3DC
		internal PlayGamesScore AsScore(string leaderboardId, string selfPlayerId)
		{
			DateTime dateTime;
			dateTime..ctor(1970, 1, 1, 0, 0, 0, 0, 1);
			ulong num = this.GetDate();
			if (num == 18446744073709551615UL)
			{
				num = 0UL;
			}
			DateTime dateTime2 = dateTime.AddMilliseconds(num);
			return new PlayGamesScore(dateTime2, leaderboardId, this.GetRank(), selfPlayerId, this.GetValue(), this.GetMetadata());
		}

		// Token: 0x06003377 RID: 13175 RVA: 0x001CC035 File Offset: 0x001CA435
		internal static NativeScore FromPointer(IntPtr pointer)
		{
			if (pointer.Equals(IntPtr.Zero))
			{
				return null;
			}
			return new NativeScore(pointer);
		}

		// Token: 0x040032E3 RID: 13027
		private const ulong MinusOne = 18446744073709551615UL;
	}
}
