using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.Native.PInvoke
{
	// Token: 0x02000718 RID: 1816
	internal class SnapshotManager
	{
		// Token: 0x06003486 RID: 13446 RVA: 0x001CE2AA File Offset: 0x001CC6AA
		internal SnapshotManager(GameServices services)
		{
			this.mServices = Misc.CheckNotNull<GameServices>(services);
		}

		// Token: 0x06003487 RID: 13447 RVA: 0x001CE2C0 File Offset: 0x001CC6C0
		internal void FetchAll(Types.DataSource source, Action<SnapshotManager.FetchAllResponse> callback)
		{
			SnapshotManager.SnapshotManager_FetchAll(this.mServices.AsHandle(), source, new SnapshotManager.FetchAllCallback(SnapshotManager.InternalFetchAllCallback), Callbacks.ToIntPtr<SnapshotManager.FetchAllResponse>(callback, new Func<IntPtr, SnapshotManager.FetchAllResponse>(SnapshotManager.FetchAllResponse.FromPointer)));
		}

		// Token: 0x06003488 RID: 13448 RVA: 0x001CE31E File Offset: 0x001CC71E
		[MonoPInvokeCallback(typeof(SnapshotManager.FetchAllCallback))]
		internal static void InternalFetchAllCallback(IntPtr response, IntPtr data)
		{
			Callbacks.PerformInternalCallback("SnapshotManager#FetchAllCallback", Callbacks.Type.Temporary, response, data);
		}

		// Token: 0x06003489 RID: 13449 RVA: 0x001CE330 File Offset: 0x001CC730
		internal void SnapshotSelectUI(bool allowCreate, bool allowDelete, uint maxSnapshots, string uiTitle, Action<SnapshotManager.SnapshotSelectUIResponse> callback)
		{
			SnapshotManager.SnapshotManager_ShowSelectUIOperation(this.mServices.AsHandle(), allowCreate, allowDelete, maxSnapshots, uiTitle, new SnapshotManager.SnapshotSelectUICallback(SnapshotManager.InternalSnapshotSelectUICallback), Callbacks.ToIntPtr<SnapshotManager.SnapshotSelectUIResponse>(callback, new Func<IntPtr, SnapshotManager.SnapshotSelectUIResponse>(SnapshotManager.SnapshotSelectUIResponse.FromPointer)));
		}

		// Token: 0x0600348A RID: 13450 RVA: 0x001CE393 File Offset: 0x001CC793
		[MonoPInvokeCallback(typeof(SnapshotManager.SnapshotSelectUICallback))]
		internal static void InternalSnapshotSelectUICallback(IntPtr response, IntPtr data)
		{
			Callbacks.PerformInternalCallback("SnapshotManager#SnapshotSelectUICallback", Callbacks.Type.Temporary, response, data);
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x001CE3A4 File Offset: 0x001CC7A4
		internal void Open(string fileName, Types.DataSource source, Types.SnapshotConflictPolicy conflictPolicy, Action<SnapshotManager.OpenResponse> callback)
		{
			Misc.CheckNotNull<string>(fileName);
			Misc.CheckNotNull<Action<SnapshotManager.OpenResponse>>(callback);
			SnapshotManager.SnapshotManager_Open(this.mServices.AsHandle(), source, fileName, conflictPolicy, new SnapshotManager.OpenCallback(SnapshotManager.InternalOpenCallback), Callbacks.ToIntPtr<SnapshotManager.OpenResponse>(callback, new Func<IntPtr, SnapshotManager.OpenResponse>(SnapshotManager.OpenResponse.FromPointer)));
		}

		// Token: 0x0600348C RID: 13452 RVA: 0x001CE414 File Offset: 0x001CC814
		[MonoPInvokeCallback(typeof(SnapshotManager.OpenCallback))]
		internal static void InternalOpenCallback(IntPtr response, IntPtr data)
		{
			Callbacks.PerformInternalCallback("SnapshotManager#OpenCallback", Callbacks.Type.Temporary, response, data);
		}

		// Token: 0x0600348D RID: 13453 RVA: 0x001CE424 File Offset: 0x001CC824
		internal void Commit(NativeSnapshotMetadata metadata, NativeSnapshotMetadataChange metadataChange, byte[] updatedData, Action<SnapshotManager.CommitResponse> callback)
		{
			Misc.CheckNotNull<NativeSnapshotMetadata>(metadata);
			Misc.CheckNotNull<NativeSnapshotMetadataChange>(metadataChange);
			SnapshotManager.SnapshotManager_Commit(this.mServices.AsHandle(), metadata.AsPointer(), metadataChange.AsPointer(), updatedData, new UIntPtr((ulong)((long)updatedData.Length)), new SnapshotManager.CommitCallback(SnapshotManager.InternalCommitCallback), Callbacks.ToIntPtr<SnapshotManager.CommitResponse>(callback, new Func<IntPtr, SnapshotManager.CommitResponse>(SnapshotManager.CommitResponse.FromPointer)));
		}

		// Token: 0x0600348E RID: 13454 RVA: 0x001CE4A8 File Offset: 0x001CC8A8
		internal void Resolve(NativeSnapshotMetadata metadata, NativeSnapshotMetadataChange metadataChange, string conflictId, Action<SnapshotManager.OpenResponse> callback)
		{
			Misc.CheckNotNull<NativeSnapshotMetadata>(metadata);
			Misc.CheckNotNull<NativeSnapshotMetadataChange>(metadataChange);
			Misc.CheckNotNull<string>(conflictId);
			SnapshotManager.SnapshotManager_ResolveConflict(this.mServices.AsHandle(), conflictId, metadata.AsPointer(), metadataChange.AsPointer(), new SnapshotManager.OpenCallback(SnapshotManager.InternalOpenCallback), Callbacks.ToIntPtr<SnapshotManager.OpenResponse>(callback, new Func<IntPtr, SnapshotManager.OpenResponse>(SnapshotManager.OpenResponse.FromPointer)));
		}

		// Token: 0x0600348F RID: 13455 RVA: 0x001CE528 File Offset: 0x001CC928
		internal void Resolve(NativeSnapshotMetadata metadata, NativeSnapshotMetadataChange metadataChange, string conflictId, byte[] updatedData, Action<SnapshotManager.OpenResponse> callback)
		{
			Misc.CheckNotNull<NativeSnapshotMetadata>(metadata);
			Misc.CheckNotNull<NativeSnapshotMetadataChange>(metadataChange);
			Misc.CheckNotNull<string>(conflictId);
			Misc.CheckNotNull<byte[]>(updatedData);
			SnapshotManager.SnapshotManager_ResolveConflict(this.mServices.AsHandle(), conflictId, metadata.AsPointer(), metadataChange.AsPointer(), updatedData, new UIntPtr((ulong)((long)updatedData.Length)), new SnapshotManager.OpenCallback(SnapshotManager.InternalOpenCallback), Callbacks.ToIntPtr<SnapshotManager.OpenResponse>(callback, new Func<IntPtr, SnapshotManager.OpenResponse>(SnapshotManager.OpenResponse.FromPointer)));
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x001CE5BC File Offset: 0x001CC9BC
		[MonoPInvokeCallback(typeof(SnapshotManager.CommitCallback))]
		internal static void InternalCommitCallback(IntPtr response, IntPtr data)
		{
			Callbacks.PerformInternalCallback("SnapshotManager#CommitCallback", Callbacks.Type.Temporary, response, data);
		}

		// Token: 0x06003491 RID: 13457 RVA: 0x001CE5CB File Offset: 0x001CC9CB
		internal void Delete(NativeSnapshotMetadata metadata)
		{
			Misc.CheckNotNull<NativeSnapshotMetadata>(metadata);
			SnapshotManager.SnapshotManager_Delete(this.mServices.AsHandle(), metadata.AsPointer());
		}

		// Token: 0x06003492 RID: 13458 RVA: 0x001CE5EC File Offset: 0x001CC9EC
		internal void Read(NativeSnapshotMetadata metadata, Action<SnapshotManager.ReadResponse> callback)
		{
			Misc.CheckNotNull<NativeSnapshotMetadata>(metadata);
			Misc.CheckNotNull<Action<SnapshotManager.ReadResponse>>(callback);
			SnapshotManager.SnapshotManager_Read(this.mServices.AsHandle(), metadata.AsPointer(), new SnapshotManager.ReadCallback(SnapshotManager.InternalReadCallback), Callbacks.ToIntPtr<SnapshotManager.ReadResponse>(callback, new Func<IntPtr, SnapshotManager.ReadResponse>(SnapshotManager.ReadResponse.FromPointer)));
		}

		// Token: 0x06003493 RID: 13459 RVA: 0x001CE65D File Offset: 0x001CCA5D
		[MonoPInvokeCallback(typeof(SnapshotManager.ReadCallback))]
		internal static void InternalReadCallback(IntPtr response, IntPtr data)
		{
			Callbacks.PerformInternalCallback("SnapshotManager#ReadCallback", Callbacks.Type.Temporary, response, data);
		}

		// Token: 0x0400330E RID: 13070
		private readonly GameServices mServices;

		// Token: 0x02000719 RID: 1817
		internal class OpenResponse : BaseReferenceHolder
		{
			// Token: 0x06003494 RID: 13460 RVA: 0x001CE66C File Offset: 0x001CCA6C
			internal OpenResponse(IntPtr selfPointer)
				: base(selfPointer)
			{
			}

			// Token: 0x06003495 RID: 13461 RVA: 0x001CE675 File Offset: 0x001CCA75
			internal bool RequestSucceeded()
			{
				return this.ResponseStatus() > (CommonErrorStatus.SnapshotOpenStatus)0;
			}

			// Token: 0x06003496 RID: 13462 RVA: 0x001CE680 File Offset: 0x001CCA80
			internal CommonErrorStatus.SnapshotOpenStatus ResponseStatus()
			{
				return SnapshotManager.SnapshotManager_OpenResponse_GetStatus(base.SelfPtr());
			}

			// Token: 0x06003497 RID: 13463 RVA: 0x001CE68D File Offset: 0x001CCA8D
			internal string ConflictId()
			{
				if (this.ResponseStatus() != CommonErrorStatus.SnapshotOpenStatus.VALID_WITH_CONFLICT)
				{
					throw new InvalidOperationException("OpenResponse did not have a conflict");
				}
				return PInvokeUtilities.OutParamsToString((byte[] out_string, UIntPtr out_size) => SnapshotManager.SnapshotManager_OpenResponse_GetConflictId(base.SelfPtr(), out_string, out_size));
			}

			// Token: 0x06003498 RID: 13464 RVA: 0x001CE6B7 File Offset: 0x001CCAB7
			internal NativeSnapshotMetadata Data()
			{
				if (this.ResponseStatus() != CommonErrorStatus.SnapshotOpenStatus.VALID)
				{
					throw new InvalidOperationException("OpenResponse had a conflict");
				}
				return new NativeSnapshotMetadata(SnapshotManager.SnapshotManager_OpenResponse_GetData(base.SelfPtr()));
			}

			// Token: 0x06003499 RID: 13465 RVA: 0x001CE6E0 File Offset: 0x001CCAE0
			internal NativeSnapshotMetadata ConflictOriginal()
			{
				if (this.ResponseStatus() != CommonErrorStatus.SnapshotOpenStatus.VALID_WITH_CONFLICT)
				{
					throw new InvalidOperationException("OpenResponse did not have a conflict");
				}
				return new NativeSnapshotMetadata(SnapshotManager.SnapshotManager_OpenResponse_GetConflictOriginal(base.SelfPtr()));
			}

			// Token: 0x0600349A RID: 13466 RVA: 0x001CE709 File Offset: 0x001CCB09
			internal NativeSnapshotMetadata ConflictUnmerged()
			{
				if (this.ResponseStatus() != CommonErrorStatus.SnapshotOpenStatus.VALID_WITH_CONFLICT)
				{
					throw new InvalidOperationException("OpenResponse did not have a conflict");
				}
				return new NativeSnapshotMetadata(SnapshotManager.SnapshotManager_OpenResponse_GetConflictUnmerged(base.SelfPtr()));
			}

			// Token: 0x0600349B RID: 13467 RVA: 0x001CE732 File Offset: 0x001CCB32
			protected override void CallDispose(HandleRef selfPointer)
			{
				SnapshotManager.SnapshotManager_OpenResponse_Dispose(selfPointer);
			}

			// Token: 0x0600349C RID: 13468 RVA: 0x001CE73A File Offset: 0x001CCB3A
			internal static SnapshotManager.OpenResponse FromPointer(IntPtr pointer)
			{
				if (pointer.Equals(IntPtr.Zero))
				{
					return null;
				}
				return new SnapshotManager.OpenResponse(pointer);
			}
		}

		// Token: 0x0200071A RID: 1818
		internal class FetchAllResponse : BaseReferenceHolder
		{
			// Token: 0x0600349E RID: 13470 RVA: 0x001CE76F File Offset: 0x001CCB6F
			internal FetchAllResponse(IntPtr selfPointer)
				: base(selfPointer)
			{
			}

			// Token: 0x0600349F RID: 13471 RVA: 0x001CE778 File Offset: 0x001CCB78
			internal CommonErrorStatus.ResponseStatus ResponseStatus()
			{
				return SnapshotManager.SnapshotManager_FetchAllResponse_GetStatus(base.SelfPtr());
			}

			// Token: 0x060034A0 RID: 13472 RVA: 0x001CE785 File Offset: 0x001CCB85
			internal bool RequestSucceeded()
			{
				return this.ResponseStatus() > (CommonErrorStatus.ResponseStatus)0;
			}

			// Token: 0x060034A1 RID: 13473 RVA: 0x001CE790 File Offset: 0x001CCB90
			internal IEnumerable<NativeSnapshotMetadata> Data()
			{
				return PInvokeUtilities.ToEnumerable<NativeSnapshotMetadata>(SnapshotManager.SnapshotManager_FetchAllResponse_GetData_Length(base.SelfPtr()), (UIntPtr index) => new NativeSnapshotMetadata(SnapshotManager.SnapshotManager_FetchAllResponse_GetData_GetElement(base.SelfPtr(), index)));
			}

			// Token: 0x060034A2 RID: 13474 RVA: 0x001CE7AE File Offset: 0x001CCBAE
			protected override void CallDispose(HandleRef selfPointer)
			{
				SnapshotManager.SnapshotManager_FetchAllResponse_Dispose(selfPointer);
			}

			// Token: 0x060034A3 RID: 13475 RVA: 0x001CE7B6 File Offset: 0x001CCBB6
			internal static SnapshotManager.FetchAllResponse FromPointer(IntPtr pointer)
			{
				if (pointer.Equals(IntPtr.Zero))
				{
					return null;
				}
				return new SnapshotManager.FetchAllResponse(pointer);
			}
		}

		// Token: 0x0200071B RID: 1819
		internal class CommitResponse : BaseReferenceHolder
		{
			// Token: 0x060034A5 RID: 13477 RVA: 0x001CE7EF File Offset: 0x001CCBEF
			internal CommitResponse(IntPtr selfPointer)
				: base(selfPointer)
			{
			}

			// Token: 0x060034A6 RID: 13478 RVA: 0x001CE7F8 File Offset: 0x001CCBF8
			internal CommonErrorStatus.ResponseStatus ResponseStatus()
			{
				return SnapshotManager.SnapshotManager_CommitResponse_GetStatus(base.SelfPtr());
			}

			// Token: 0x060034A7 RID: 13479 RVA: 0x001CE805 File Offset: 0x001CCC05
			internal bool RequestSucceeded()
			{
				return this.ResponseStatus() > (CommonErrorStatus.ResponseStatus)0;
			}

			// Token: 0x060034A8 RID: 13480 RVA: 0x001CE810 File Offset: 0x001CCC10
			internal NativeSnapshotMetadata Data()
			{
				if (!this.RequestSucceeded())
				{
					throw new InvalidOperationException("Request did not succeed");
				}
				return new NativeSnapshotMetadata(SnapshotManager.SnapshotManager_CommitResponse_GetData(base.SelfPtr()));
			}

			// Token: 0x060034A9 RID: 13481 RVA: 0x001CE838 File Offset: 0x001CCC38
			protected override void CallDispose(HandleRef selfPointer)
			{
				SnapshotManager.SnapshotManager_CommitResponse_Dispose(selfPointer);
			}

			// Token: 0x060034AA RID: 13482 RVA: 0x001CE840 File Offset: 0x001CCC40
			internal static SnapshotManager.CommitResponse FromPointer(IntPtr pointer)
			{
				if (pointer.Equals(IntPtr.Zero))
				{
					return null;
				}
				return new SnapshotManager.CommitResponse(pointer);
			}
		}

		// Token: 0x0200071C RID: 1820
		internal class ReadResponse : BaseReferenceHolder
		{
			// Token: 0x060034AB RID: 13483 RVA: 0x001CE866 File Offset: 0x001CCC66
			internal ReadResponse(IntPtr selfPointer)
				: base(selfPointer)
			{
			}

			// Token: 0x060034AC RID: 13484 RVA: 0x001CE86F File Offset: 0x001CCC6F
			internal CommonErrorStatus.ResponseStatus ResponseStatus()
			{
				return SnapshotManager.SnapshotManager_CommitResponse_GetStatus(base.SelfPtr());
			}

			// Token: 0x060034AD RID: 13485 RVA: 0x001CE87C File Offset: 0x001CCC7C
			internal bool RequestSucceeded()
			{
				return this.ResponseStatus() > (CommonErrorStatus.ResponseStatus)0;
			}

			// Token: 0x060034AE RID: 13486 RVA: 0x001CE887 File Offset: 0x001CCC87
			internal byte[] Data()
			{
				if (!this.RequestSucceeded())
				{
					throw new InvalidOperationException("Request did not succeed");
				}
				return PInvokeUtilities.OutParamsToArray<byte>((byte[] out_bytes, UIntPtr out_size) => SnapshotManager.SnapshotManager_ReadResponse_GetData(base.SelfPtr(), out_bytes, out_size));
			}

			// Token: 0x060034AF RID: 13487 RVA: 0x001CE8B0 File Offset: 0x001CCCB0
			protected override void CallDispose(HandleRef selfPointer)
			{
				SnapshotManager.SnapshotManager_ReadResponse_Dispose(selfPointer);
			}

			// Token: 0x060034B0 RID: 13488 RVA: 0x001CE8B8 File Offset: 0x001CCCB8
			internal static SnapshotManager.ReadResponse FromPointer(IntPtr pointer)
			{
				if (pointer.Equals(IntPtr.Zero))
				{
					return null;
				}
				return new SnapshotManager.ReadResponse(pointer);
			}
		}

		// Token: 0x0200071D RID: 1821
		internal class SnapshotSelectUIResponse : BaseReferenceHolder
		{
			// Token: 0x060034B2 RID: 13490 RVA: 0x001CE8ED File Offset: 0x001CCCED
			internal SnapshotSelectUIResponse(IntPtr selfPointer)
				: base(selfPointer)
			{
			}

			// Token: 0x060034B3 RID: 13491 RVA: 0x001CE8F6 File Offset: 0x001CCCF6
			internal CommonErrorStatus.UIStatus RequestStatus()
			{
				return SnapshotManager.SnapshotManager_SnapshotSelectUIResponse_GetStatus(base.SelfPtr());
			}

			// Token: 0x060034B4 RID: 13492 RVA: 0x001CE903 File Offset: 0x001CCD03
			internal bool RequestSucceeded()
			{
				return this.RequestStatus() > (CommonErrorStatus.UIStatus)0;
			}

			// Token: 0x060034B5 RID: 13493 RVA: 0x001CE90E File Offset: 0x001CCD0E
			internal NativeSnapshotMetadata Data()
			{
				if (!this.RequestSucceeded())
				{
					throw new InvalidOperationException("Request did not succeed");
				}
				return new NativeSnapshotMetadata(SnapshotManager.SnapshotManager_SnapshotSelectUIResponse_GetData(base.SelfPtr()));
			}

			// Token: 0x060034B6 RID: 13494 RVA: 0x001CE936 File Offset: 0x001CCD36
			protected override void CallDispose(HandleRef selfPointer)
			{
				SnapshotManager.SnapshotManager_SnapshotSelectUIResponse_Dispose(selfPointer);
			}

			// Token: 0x060034B7 RID: 13495 RVA: 0x001CE93E File Offset: 0x001CCD3E
			internal static SnapshotManager.SnapshotSelectUIResponse FromPointer(IntPtr pointer)
			{
				if (pointer.Equals(IntPtr.Zero))
				{
					return null;
				}
				return new SnapshotManager.SnapshotSelectUIResponse(pointer);
			}
		}
	}
}
