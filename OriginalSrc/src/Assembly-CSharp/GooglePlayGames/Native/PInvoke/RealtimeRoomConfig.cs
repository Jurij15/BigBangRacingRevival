using System;
using System.Runtime.InteropServices;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x02000716 RID: 1814
	internal class RealtimeRoomConfig : BaseReferenceHolder
	{
		// Token: 0x06003479 RID: 13433 RVA: 0x001CE1C9 File Offset: 0x001CC5C9
		internal RealtimeRoomConfig(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x001CE1D2 File Offset: 0x001CC5D2
		protected override void CallDispose(HandleRef selfPointer)
		{
			RealTimeRoomConfig.RealTimeRoomConfig_Dispose(selfPointer);
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x001CE1DA File Offset: 0x001CC5DA
		internal static RealtimeRoomConfig FromPointer(IntPtr selfPointer)
		{
			if (selfPointer.Equals(IntPtr.Zero))
			{
				return null;
			}
			return new RealtimeRoomConfig(selfPointer);
		}
	}
}
