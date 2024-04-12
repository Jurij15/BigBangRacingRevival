using System;
using System.Runtime.InteropServices;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006F1 RID: 1777
	internal class NativeLeaderboard : BaseReferenceHolder
	{
		// Token: 0x0600333D RID: 13117 RVA: 0x001CBB76 File Offset: 0x001C9F76
		internal NativeLeaderboard(IntPtr selfPtr)
			: base(selfPtr)
		{
		}

		// Token: 0x0600333E RID: 13118 RVA: 0x001CBB7F File Offset: 0x001C9F7F
		protected override void CallDispose(HandleRef selfPointer)
		{
			GooglePlayGames.Native.Cwrapper.Leaderboard.Leaderboard_Dispose(selfPointer);
		}

		// Token: 0x0600333F RID: 13119 RVA: 0x001CBB87 File Offset: 0x001C9F87
		internal string Title()
		{
			return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr out_size) => GooglePlayGames.Native.Cwrapper.Leaderboard.Leaderboard_Name(base.SelfPtr(), out_string, out_size));
		}

		// Token: 0x06003340 RID: 13120 RVA: 0x001CBB9A File Offset: 0x001C9F9A
		internal static NativeLeaderboard FromPointer(IntPtr pointer)
		{
			if (pointer.Equals(IntPtr.Zero))
			{
				return null;
			}
			return new NativeLeaderboard(pointer);
		}
	}
}
