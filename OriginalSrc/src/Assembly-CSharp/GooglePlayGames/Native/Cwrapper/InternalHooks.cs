using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000647 RID: 1607
	internal static class InternalHooks
	{
		// Token: 0x06002ECB RID: 11979
		[DllImport("gpg")]
		internal static extern void InternalHooks_ConfigureForUnityPlugin(HandleRef builder, string unity_version);

		// Token: 0x06002ECC RID: 11980
		[DllImport("gpg")]
		internal static extern IntPtr InternalHooks_GetApiClient(HandleRef services);
	}
}
