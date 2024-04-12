using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x02000685 RID: 1669
	internal static class SnapshotMetadata
	{
		// Token: 0x06003066 RID: 12390
		[DllImport("gpg")]
		internal static extern void SnapshotMetadata_Dispose(HandleRef self);

		// Token: 0x06003067 RID: 12391
		[DllImport("gpg")]
		internal static extern UIntPtr SnapshotMetadata_CoverImageURL(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06003068 RID: 12392
		[DllImport("gpg")]
		internal static extern UIntPtr SnapshotMetadata_Description(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06003069 RID: 12393
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool SnapshotMetadata_IsOpen(HandleRef self);

		// Token: 0x0600306A RID: 12394
		[DllImport("gpg")]
		internal static extern UIntPtr SnapshotMetadata_FileName(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x0600306B RID: 12395
		[DllImport("gpg")]
		[return: MarshalAs(3)]
		internal static extern bool SnapshotMetadata_Valid(HandleRef self);

		// Token: 0x0600306C RID: 12396
		[DllImport("gpg")]
		internal static extern long SnapshotMetadata_PlayedTime(HandleRef self);

		// Token: 0x0600306D RID: 12397
		[DllImport("gpg")]
		internal static extern long SnapshotMetadata_LastModifiedTime(HandleRef self);
	}
}
