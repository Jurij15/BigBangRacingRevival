using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
	// Token: 0x0200067F RID: 1663
	internal static class SnapshotManager
	{
		// Token: 0x06003036 RID: 12342
		[DllImport("gpg")]
		internal static extern void SnapshotManager_FetchAll(HandleRef self, Types.DataSource data_source, SnapshotManager.FetchAllCallback callback, IntPtr callback_arg);

		// Token: 0x06003037 RID: 12343
		[DllImport("gpg")]
		internal static extern void SnapshotManager_ShowSelectUIOperation(HandleRef self, [MarshalAs(3)] bool allow_create, [MarshalAs(3)] bool allow_delete, uint max_snapshots, string title, SnapshotManager.SnapshotSelectUICallback callback, IntPtr callback_arg);

		// Token: 0x06003038 RID: 12344
		[DllImport("gpg")]
		internal static extern void SnapshotManager_Read(HandleRef self, IntPtr snapshot_metadata, SnapshotManager.ReadCallback callback, IntPtr callback_arg);

		// Token: 0x06003039 RID: 12345
		[DllImport("gpg")]
		internal static extern void SnapshotManager_Commit(HandleRef self, IntPtr snapshot_metadata, IntPtr metadata_change, byte[] data, UIntPtr data_size, SnapshotManager.CommitCallback callback, IntPtr callback_arg);

		// Token: 0x0600303A RID: 12346
		[DllImport("gpg")]
		internal static extern void SnapshotManager_Open(HandleRef self, Types.DataSource data_source, string file_name, Types.SnapshotConflictPolicy conflict_policy, SnapshotManager.OpenCallback callback, IntPtr callback_arg);

		// Token: 0x0600303B RID: 12347
		[DllImport("gpg")]
		internal static extern void SnapshotManager_ResolveConflict(HandleRef self, string conflict_id, IntPtr snapshot_metadata, IntPtr metadata_change, SnapshotManager.OpenCallback callback, IntPtr callback_arg);

		// Token: 0x0600303C RID: 12348
		[DllImport("gpg")]
		internal static extern void SnapshotManager_ResolveConflict(HandleRef self, string conflict_id, IntPtr snapshot_metadata, IntPtr metadata_change, byte[] data, UIntPtr data_size, SnapshotManager.OpenCallback callback, IntPtr callback_arg);

		// Token: 0x0600303D RID: 12349
		[DllImport("gpg")]
		internal static extern void SnapshotManager_ResolveConflict(HandleRef self, string conflict_id, IntPtr snapshot_metadata, SnapshotManager.OpenCallback callback, IntPtr callback_arg);

		// Token: 0x0600303E RID: 12350
		[DllImport("gpg")]
		internal static extern void SnapshotManager_Delete(HandleRef self, IntPtr snapshot_metadata);

		// Token: 0x0600303F RID: 12351
		[DllImport("gpg")]
		internal static extern void SnapshotManager_FetchAllResponse_Dispose(HandleRef self);

		// Token: 0x06003040 RID: 12352
		[DllImport("gpg")]
		internal static extern CommonErrorStatus.ResponseStatus SnapshotManager_FetchAllResponse_GetStatus(HandleRef self);

		// Token: 0x06003041 RID: 12353
		[DllImport("gpg")]
		internal static extern UIntPtr SnapshotManager_FetchAllResponse_GetData_Length(HandleRef self);

		// Token: 0x06003042 RID: 12354
		[DllImport("gpg")]
		internal static extern IntPtr SnapshotManager_FetchAllResponse_GetData_GetElement(HandleRef self, UIntPtr index);

		// Token: 0x06003043 RID: 12355
		[DllImport("gpg")]
		internal static extern void SnapshotManager_OpenResponse_Dispose(HandleRef self);

		// Token: 0x06003044 RID: 12356
		[DllImport("gpg")]
		internal static extern CommonErrorStatus.SnapshotOpenStatus SnapshotManager_OpenResponse_GetStatus(HandleRef self);

		// Token: 0x06003045 RID: 12357
		[DllImport("gpg")]
		internal static extern IntPtr SnapshotManager_OpenResponse_GetData(HandleRef self);

		// Token: 0x06003046 RID: 12358
		[DllImport("gpg")]
		internal static extern UIntPtr SnapshotManager_OpenResponse_GetConflictId(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x06003047 RID: 12359
		[DllImport("gpg")]
		internal static extern IntPtr SnapshotManager_OpenResponse_GetConflictOriginal(HandleRef self);

		// Token: 0x06003048 RID: 12360
		[DllImport("gpg")]
		internal static extern IntPtr SnapshotManager_OpenResponse_GetConflictUnmerged(HandleRef self);

		// Token: 0x06003049 RID: 12361
		[DllImport("gpg")]
		internal static extern void SnapshotManager_CommitResponse_Dispose(HandleRef self);

		// Token: 0x0600304A RID: 12362
		[DllImport("gpg")]
		internal static extern CommonErrorStatus.ResponseStatus SnapshotManager_CommitResponse_GetStatus(HandleRef self);

		// Token: 0x0600304B RID: 12363
		[DllImport("gpg")]
		internal static extern IntPtr SnapshotManager_CommitResponse_GetData(HandleRef self);

		// Token: 0x0600304C RID: 12364
		[DllImport("gpg")]
		internal static extern void SnapshotManager_ReadResponse_Dispose(HandleRef self);

		// Token: 0x0600304D RID: 12365
		[DllImport("gpg")]
		internal static extern CommonErrorStatus.ResponseStatus SnapshotManager_ReadResponse_GetStatus(HandleRef self);

		// Token: 0x0600304E RID: 12366
		[DllImport("gpg")]
		internal static extern UIntPtr SnapshotManager_ReadResponse_GetData(HandleRef self, [In] [Out] byte[] out_arg, UIntPtr out_size);

		// Token: 0x0600304F RID: 12367
		[DllImport("gpg")]
		internal static extern void SnapshotManager_SnapshotSelectUIResponse_Dispose(HandleRef self);

		// Token: 0x06003050 RID: 12368
		[DllImport("gpg")]
		internal static extern CommonErrorStatus.UIStatus SnapshotManager_SnapshotSelectUIResponse_GetStatus(HandleRef self);

		// Token: 0x06003051 RID: 12369
		[DllImport("gpg")]
		internal static extern IntPtr SnapshotManager_SnapshotSelectUIResponse_GetData(HandleRef self);

		// Token: 0x02000680 RID: 1664
		// (Invoke) Token: 0x06003053 RID: 12371
		internal delegate void FetchAllCallback(IntPtr arg0, IntPtr arg1);

		// Token: 0x02000681 RID: 1665
		// (Invoke) Token: 0x06003057 RID: 12375
		internal delegate void OpenCallback(IntPtr arg0, IntPtr arg1);

		// Token: 0x02000682 RID: 1666
		// (Invoke) Token: 0x0600305B RID: 12379
		internal delegate void CommitCallback(IntPtr arg0, IntPtr arg1);

		// Token: 0x02000683 RID: 1667
		// (Invoke) Token: 0x0600305F RID: 12383
		internal delegate void ReadCallback(IntPtr arg0, IntPtr arg1);

		// Token: 0x02000684 RID: 1668
		// (Invoke) Token: 0x06003063 RID: 12387
		internal delegate void SnapshotSelectUICallback(IntPtr arg0, IntPtr arg1);
	}
}
