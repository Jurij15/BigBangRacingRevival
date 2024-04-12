using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000658 RID: 1624
	internal static class NearbyConnectionsBuilder
	{
		// Token: 0x06002F3B RID: 12091
		[DllImport("gpg")]
		internal static extern void NearbyConnections_Builder_SetOnInitializationFinished(HandleRef self, NearbyConnectionsBuilder.OnInitializationFinishedCallback callback, IntPtr callback_arg);

		// Token: 0x06002F3C RID: 12092
		[DllImport("gpg")]
		internal static extern IntPtr NearbyConnections_Builder_Construct();

		// Token: 0x06002F3D RID: 12093
		[DllImport("gpg")]
		internal static extern void NearbyConnections_Builder_SetClientId(HandleRef self, long client_id);

		// Token: 0x06002F3E RID: 12094
		[DllImport("gpg")]
		internal static extern void NearbyConnections_Builder_SetOnLog(HandleRef self, NearbyConnectionsBuilder.OnLogCallback callback, IntPtr callback_arg, Types.LogLevel min_level);

		// Token: 0x06002F3F RID: 12095
		[DllImport("gpg")]
		internal static extern void NearbyConnections_Builder_SetDefaultOnLog(HandleRef self, Types.LogLevel min_level);

		// Token: 0x06002F40 RID: 12096
		[DllImport("gpg")]
		internal static extern IntPtr NearbyConnections_Builder_Create(HandleRef self, IntPtr platform);

		// Token: 0x06002F41 RID: 12097
		[DllImport("gpg")]
		internal static extern void NearbyConnections_Builder_Dispose(HandleRef self);

		// Token: 0x02000659 RID: 1625
		// (Invoke) Token: 0x06002F43 RID: 12099
		internal delegate void OnInitializationFinishedCallback(NearbyConnectionsStatus.InitializationStatus arg0, IntPtr arg1);

		// Token: 0x0200065A RID: 1626
		// (Invoke) Token: 0x06002F47 RID: 12103
		internal delegate void OnLogCallback(Types.LogLevel arg0, string arg1, IntPtr arg2);
	}
}
