using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000648 RID: 1608
	internal static class IosPlatformConfiguration
	{
		// Token: 0x06002ECD RID: 11981
		[DllImport("gpg")]
		internal static extern IntPtr IosPlatformConfiguration_Construct();

		// Token: 0x06002ECE RID: 11982
		[DllImport("gpg")]
		internal static extern void IosPlatformConfiguration_Dispose(HandleRef self);

		// Token: 0x06002ECF RID: 11983
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool IosPlatformConfiguration_Valid(HandleRef self);

		// Token: 0x06002ED0 RID: 11984
		[DllImport("gpg")]
		internal static extern void IosPlatformConfiguration_SetClientID(HandleRef self, string client_id);
	}
}
