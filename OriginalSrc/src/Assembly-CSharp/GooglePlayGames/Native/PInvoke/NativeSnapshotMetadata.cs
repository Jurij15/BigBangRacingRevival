using System;
using System.Runtime.InteropServices;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.Native.Cwrapper;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006FC RID: 1788
	internal class NativeSnapshotMetadata : BaseReferenceHolder, ISavedGameMetadata
	{
		// Token: 0x06003398 RID: 13208 RVA: 0x001CC2D1 File Offset: 0x001CA6D1
		internal NativeSnapshotMetadata(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06003399 RID: 13209 RVA: 0x001CC2DA File Offset: 0x001CA6DA
		public bool IsOpen
		{
			get
			{
				return SnapshotMetadata.SnapshotMetadata_IsOpen(base.SelfPtr());
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600339A RID: 13210 RVA: 0x001CC2E7 File Offset: 0x001CA6E7
		public string Filename
		{
			get
			{
				return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr out_size) => SnapshotMetadata.SnapshotMetadata_FileName(base.SelfPtr(), out_string, out_size));
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600339B RID: 13211 RVA: 0x001CC2FA File Offset: 0x001CA6FA
		public string Description
		{
			get
			{
				return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr out_size) => SnapshotMetadata.SnapshotMetadata_Description(base.SelfPtr(), out_string, out_size));
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600339C RID: 13212 RVA: 0x001CC30D File Offset: 0x001CA70D
		public string CoverImageURL
		{
			get
			{
				return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr out_size) => SnapshotMetadata.SnapshotMetadata_CoverImageURL(base.SelfPtr(), out_string, out_size));
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600339D RID: 13213 RVA: 0x001CC320 File Offset: 0x001CA720
		public TimeSpan TotalTimePlayed
		{
			get
			{
				long num = SnapshotMetadata.SnapshotMetadata_PlayedTime(base.SelfPtr());
				if (num < 0L)
				{
					return TimeSpan.FromMilliseconds(0.0);
				}
				return TimeSpan.FromMilliseconds((double)num);
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600339E RID: 13214 RVA: 0x001CC357 File Offset: 0x001CA757
		public DateTime LastModifiedTimestamp
		{
			get
			{
				return PInvokeUtilities.FromMillisSinceUnixEpoch(SnapshotMetadata.SnapshotMetadata_LastModifiedTime(base.SelfPtr()));
			}
		}

		// Token: 0x0600339F RID: 13215 RVA: 0x001CC36C File Offset: 0x001CA76C
		public override string ToString()
		{
			if (base.IsDisposed())
			{
				return "[NativeSnapshotMetadata: DELETED]";
			}
			return string.Format("[NativeSnapshotMetadata: IsOpen={0}, Filename={1}, Description={2}, CoverImageUrl={3}, TotalTimePlayed={4}, LastModifiedTimestamp={5}]", new object[] { this.IsOpen, this.Filename, this.Description, this.CoverImageURL, this.TotalTimePlayed, this.LastModifiedTimestamp });
		}

		// Token: 0x060033A0 RID: 13216 RVA: 0x001CC3DF File Offset: 0x001CA7DF
		protected override void CallDispose(HandleRef selfPointer)
		{
			SnapshotMetadata.SnapshotMetadata_Dispose(base.SelfPtr());
		}
	}
}
