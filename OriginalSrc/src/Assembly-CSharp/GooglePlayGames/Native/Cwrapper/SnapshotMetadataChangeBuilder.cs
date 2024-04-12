using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000687 RID: 1671
	internal static class SnapshotMetadataChangeBuilder
	{
		// Token: 0x06003076 RID: 12406
		[DllImport("gpg")]
		internal static extern void SnapshotMetadataChange_Builder_SetDescription(HandleRef self, string description);

		// Token: 0x06003077 RID: 12407
		[DllImport("gpg")]
		internal static extern IntPtr SnapshotMetadataChange_Builder_Construct();

		// Token: 0x06003078 RID: 12408
		[DllImport("gpg")]
		internal static extern void SnapshotMetadataChange_Builder_SetPlayedTime(HandleRef self, ulong played_time);

		// Token: 0x06003079 RID: 12409
		[DllImport("gpg")]
		internal static extern void SnapshotMetadataChange_Builder_SetCoverImageFromPngData(HandleRef self, byte[] png_data, UIntPtr png_data_size);

		// Token: 0x0600307A RID: 12410
		[DllImport("gpg")]
		internal static extern IntPtr SnapshotMetadataChange_Builder_Create(HandleRef self);

		// Token: 0x0600307B RID: 12411
		[DllImport("gpg")]
		internal static extern void SnapshotMetadataChange_Builder_Dispose(HandleRef self);
	}
}
