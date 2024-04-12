using System;
using System.Runtime.InteropServices;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006FA RID: 1786
	internal class NativeScorePageToken : BaseReferenceHolder
	{
		// Token: 0x06003391 RID: 13201 RVA: 0x001CC26A File Offset: 0x001CA66A
		internal NativeScorePageToken(IntPtr selfPtr)
			: base(selfPtr)
		{
		}

		// Token: 0x06003392 RID: 13202 RVA: 0x001CC273 File Offset: 0x001CA673
		protected override void CallDispose(HandleRef selfPointer)
		{
			ScorePage.ScorePage_ScorePageToken_Dispose(selfPointer);
		}
	}
}
