using System;
using System.Runtime.InteropServices;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006FB RID: 1787
	internal class NativeScoreSummary : BaseReferenceHolder
	{
		// Token: 0x06003393 RID: 13203 RVA: 0x001CC27B File Offset: 0x001CA67B
		internal NativeScoreSummary(IntPtr selfPtr)
			: base(selfPtr)
		{
		}

		// Token: 0x06003394 RID: 13204 RVA: 0x001CC284 File Offset: 0x001CA684
		protected override void CallDispose(HandleRef selfPointer)
		{
			ScoreSummary.ScoreSummary_Dispose(selfPointer);
		}

		// Token: 0x06003395 RID: 13205 RVA: 0x001CC28C File Offset: 0x001CA68C
		internal ulong ApproximateResults()
		{
			return ScoreSummary.ScoreSummary_ApproximateNumberOfScores(base.SelfPtr());
		}

		// Token: 0x06003396 RID: 13206 RVA: 0x001CC299 File Offset: 0x001CA699
		internal NativeScore LocalUserScore()
		{
			return NativeScore.FromPointer(ScoreSummary.ScoreSummary_CurrentPlayerScore(base.SelfPtr()));
		}

		// Token: 0x06003397 RID: 13207 RVA: 0x001CC2AB File Offset: 0x001CA6AB
		internal static NativeScoreSummary FromPointer(IntPtr pointer)
		{
			if (pointer.Equals(IntPtr.Zero))
			{
				return null;
			}
			return new NativeScoreSummary(pointer);
		}
	}
}
