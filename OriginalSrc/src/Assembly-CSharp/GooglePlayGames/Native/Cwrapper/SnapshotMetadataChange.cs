using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000686 RID: 1670
	internal static class SnapshotMetadataChange
	{
		// Token: 0x0600306E RID: 12398
		[DllImport("gpg")]
		internal static extern UIntPtr SnapshotMetadataChange_Description(HandleRef self, [In] [Out] char[] out_arg, UIntPtr out_size);

		// Token: 0x0600306F RID: 12399
		[DllImport("gpg")]
		internal static extern IntPtr SnapshotMetadataChange_Image(HandleRef self);

		// Token: 0x06003070 RID: 12400
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool SnapshotMetadataChange_PlayedTimeIsChanged(HandleRef self);

		// Token: 0x06003071 RID: 12401
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool SnapshotMetadataChange_Valid(HandleRef self);

		// Token: 0x06003072 RID: 12402
		[DllImport("gpg")]
		internal static extern ulong SnapshotMetadataChange_PlayedTime(HandleRef self);

		// Token: 0x06003073 RID: 12403
		[DllImport("gpg")]
		internal static extern void SnapshotMetadataChange_Dispose(HandleRef self);

		// Token: 0x06003074 RID: 12404
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool SnapshotMetadataChange_ImageIsChanged(HandleRef self);

		// Token: 0x06003075 RID: 12405
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool SnapshotMetadataChange_DescriptionIsChanged(HandleRef self);
	}
}
