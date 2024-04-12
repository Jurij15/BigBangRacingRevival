using System;
using System.Runtime.InteropServices;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x020006FD RID: 1789
	internal class NativeSnapshotMetadataChange : BaseReferenceHolder
	{
		// Token: 0x060033A4 RID: 13220 RVA: 0x001CC419 File Offset: 0x001CA819
		internal NativeSnapshotMetadataChange(IntPtr selfPointer)
			: base(selfPointer)
		{
		}

		// Token: 0x060033A5 RID: 13221 RVA: 0x001CC422 File Offset: 0x001CA822
		protected override void CallDispose(HandleRef selfPointer)
		{
			SnapshotMetadataChange.SnapshotMetadataChange_Dispose(selfPointer);
		}

		// Token: 0x060033A6 RID: 13222 RVA: 0x001CC42A File Offset: 0x001CA82A
		internal static NativeSnapshotMetadataChange FromPointer(IntPtr pointer)
		{
			if (pointer.Equals(IntPtr.Zero))
			{
				return null;
			}
			return new NativeSnapshotMetadataChange(pointer);
		}

		// Token: 0x020006FE RID: 1790
		internal class Builder : BaseReferenceHolder
		{
			// Token: 0x060033A7 RID: 13223 RVA: 0x001CC450 File Offset: 0x001CA850
			internal Builder()
				: base(SnapshotMetadataChangeBuilder.SnapshotMetadataChange_Builder_Construct())
			{
			}

			// Token: 0x060033A8 RID: 13224 RVA: 0x001CC45D File Offset: 0x001CA85D
			protected override void CallDispose(HandleRef selfPointer)
			{
				SnapshotMetadataChangeBuilder.SnapshotMetadataChange_Builder_Dispose(selfPointer);
			}

			// Token: 0x060033A9 RID: 13225 RVA: 0x001CC465 File Offset: 0x001CA865
			internal NativeSnapshotMetadataChange.Builder SetDescription(string description)
			{
				SnapshotMetadataChangeBuilder.SnapshotMetadataChange_Builder_SetDescription(base.SelfPtr(), description);
				return this;
			}

			// Token: 0x060033AA RID: 13226 RVA: 0x001CC474 File Offset: 0x001CA874
			internal NativeSnapshotMetadataChange.Builder SetPlayedTime(ulong playedTime)
			{
				SnapshotMetadataChangeBuilder.SnapshotMetadataChange_Builder_SetPlayedTime(base.SelfPtr(), playedTime);
				return this;
			}

			// Token: 0x060033AB RID: 13227 RVA: 0x001CC483 File Offset: 0x001CA883
			internal NativeSnapshotMetadataChange.Builder SetCoverImageFromPngData(byte[] pngData)
			{
				Misc.CheckNotNull<byte[]>(pngData);
				SnapshotMetadataChangeBuilder.SnapshotMetadataChange_Builder_SetCoverImageFromPngData(base.SelfPtr(), pngData, new UIntPtr((ulong)pngData.LongLength));
				return this;
			}

			// Token: 0x060033AC RID: 13228 RVA: 0x001CC4A4 File Offset: 0x001CA8A4
			internal NativeSnapshotMetadataChange.Builder From(SavedGameMetadataUpdate update)
			{
				NativeSnapshotMetadataChange.Builder builder = this;
				if (update.IsDescriptionUpdated)
				{
					builder = builder.SetDescription(update.UpdatedDescription);
				}
				if (update.IsCoverImageUpdated)
				{
					builder = builder.SetCoverImageFromPngData(update.UpdatedPngCoverImage);
				}
				if (update.IsPlayedTimeUpdated)
				{
					builder = builder.SetPlayedTime((ulong)update.UpdatedPlayedTime.Value.TotalMilliseconds);
				}
				return builder;
			}

			// Token: 0x060033AD RID: 13229 RVA: 0x001CC513 File Offset: 0x001CA913
			internal NativeSnapshotMetadataChange Build()
			{
				return NativeSnapshotMetadataChange.FromPointer(SnapshotMetadataChangeBuilder.SnapshotMetadataChange_Builder_Create(base.SelfPtr()));
			}
		}
	}
}
