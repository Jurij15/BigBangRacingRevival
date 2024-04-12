using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.Native.PInvoke;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.Native
{
	// Token: 0x020006CD RID: 1741
	internal class NativeSavedGameClient : ISavedGameClient
	{
		// Token: 0x060031FB RID: 12795 RVA: 0x001C7520 File Offset: 0x001C5920
		internal NativeSavedGameClient(GooglePlayGames.Native.PInvoke.SnapshotManager manager)
		{
			this.mSnapshotManager = Misc.CheckNotNull<GooglePlayGames.Native.PInvoke.SnapshotManager>(manager);
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x001C7534 File Offset: 0x001C5934
		public void OpenWithAutomaticConflictResolution(string filename, DataSource source, ConflictResolutionStrategy resolutionStrategy, bool prefetchDataOnConflict, ConflictCallback conflictCallback, Action<SavedGameRequestStatus, ISavedGameMetadata> completedCallback)
		{
			Misc.CheckNotNull<string>(filename);
			Misc.CheckNotNull<Action<SavedGameRequestStatus, ISavedGameMetadata>>(completedCallback);
			completedCallback = NativeSavedGameClient.ToOnGameThread<SavedGameRequestStatus, ISavedGameMetadata>(completedCallback);
			if (conflictCallback == null)
			{
				conflictCallback = delegate(IConflictResolver resolver, ISavedGameMetadata original, byte[] originalData, ISavedGameMetadata unmerged, byte[] unmergedData)
				{
					switch (resolutionStrategy)
					{
					case ConflictResolutionStrategy.UseLongestPlaytime:
						if (original.TotalTimePlayed >= unmerged.TotalTimePlayed)
						{
							resolver.ChooseMetadata(original);
						}
						else
						{
							resolver.ChooseMetadata(unmerged);
						}
						return;
					case ConflictResolutionStrategy.UseOriginal:
						resolver.ChooseMetadata(original);
						return;
					case ConflictResolutionStrategy.UseUnmerged:
						resolver.ChooseMetadata(unmerged);
						return;
					default:
						Logger.e("Unhandled strategy " + resolutionStrategy);
						completedCallback.Invoke(SavedGameRequestStatus.InternalError, null);
						return;
					}
				};
			}
			conflictCallback = this.ToOnGameThread(conflictCallback);
			if (!NativeSavedGameClient.IsValidFilename(filename))
			{
				Logger.e("Received invalid filename: " + filename);
				completedCallback.Invoke(SavedGameRequestStatus.BadInputError, null);
				return;
			}
			this.InternalOpen(filename, source, resolutionStrategy, prefetchDataOnConflict, conflictCallback, completedCallback);
		}

		// Token: 0x060031FD RID: 12797 RVA: 0x001C75DB File Offset: 0x001C59DB
		public void OpenWithAutomaticConflictResolution(string filename, DataSource source, ConflictResolutionStrategy resolutionStrategy, Action<SavedGameRequestStatus, ISavedGameMetadata> completedCallback)
		{
			this.OpenWithAutomaticConflictResolution(filename, source, resolutionStrategy, false, null, completedCallback);
		}

		// Token: 0x060031FE RID: 12798 RVA: 0x001C75EC File Offset: 0x001C59EC
		private ConflictCallback ToOnGameThread(ConflictCallback conflictCallback)
		{
			return delegate(IConflictResolver resolver, ISavedGameMetadata original, byte[] originalData, ISavedGameMetadata unmerged, byte[] unmergedData)
			{
				Logger.d("Invoking conflict callback");
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					conflictCallback(resolver, original, originalData, unmerged, unmergedData);
				});
			};
		}

		// Token: 0x060031FF RID: 12799 RVA: 0x001C7614 File Offset: 0x001C5A14
		public void OpenWithManualConflictResolution(string filename, DataSource source, bool prefetchDataOnConflict, ConflictCallback conflictCallback, Action<SavedGameRequestStatus, ISavedGameMetadata> completedCallback)
		{
			Misc.CheckNotNull<string>(filename);
			Misc.CheckNotNull<ConflictCallback>(conflictCallback);
			Misc.CheckNotNull<Action<SavedGameRequestStatus, ISavedGameMetadata>>(completedCallback);
			conflictCallback = this.ToOnGameThread(conflictCallback);
			completedCallback = NativeSavedGameClient.ToOnGameThread<SavedGameRequestStatus, ISavedGameMetadata>(completedCallback);
			if (!NativeSavedGameClient.IsValidFilename(filename))
			{
				Logger.e("Received invalid filename: " + filename);
				completedCallback.Invoke(SavedGameRequestStatus.BadInputError, null);
				return;
			}
			this.InternalOpen(filename, source, ConflictResolutionStrategy.UseManual, prefetchDataOnConflict, conflictCallback, completedCallback);
		}

		// Token: 0x06003200 RID: 12800 RVA: 0x001C7680 File Offset: 0x001C5A80
		private void InternalOpen(string filename, DataSource source, ConflictResolutionStrategy resolutionStrategy, bool prefetchDataOnConflict, ConflictCallback conflictCallback, Action<SavedGameRequestStatus, ISavedGameMetadata> completedCallback)
		{
			NativeSavedGameClient.<InternalOpen>c__AnonStorey3 <InternalOpen>c__AnonStorey = new NativeSavedGameClient.<InternalOpen>c__AnonStorey3();
			<InternalOpen>c__AnonStorey.completedCallback = completedCallback;
			<InternalOpen>c__AnonStorey.filename = filename;
			<InternalOpen>c__AnonStorey.source = source;
			<InternalOpen>c__AnonStorey.resolutionStrategy = resolutionStrategy;
			<InternalOpen>c__AnonStorey.prefetchDataOnConflict = prefetchDataOnConflict;
			<InternalOpen>c__AnonStorey.conflictCallback = conflictCallback;
			<InternalOpen>c__AnonStorey.$this = this;
			Types.SnapshotConflictPolicy snapshotConflictPolicy;
			switch (<InternalOpen>c__AnonStorey.resolutionStrategy)
			{
			case ConflictResolutionStrategy.UseLongestPlaytime:
				snapshotConflictPolicy = Types.SnapshotConflictPolicy.LONGEST_PLAYTIME;
				goto IL_85;
			case ConflictResolutionStrategy.UseManual:
				snapshotConflictPolicy = Types.SnapshotConflictPolicy.MANUAL;
				goto IL_85;
			case ConflictResolutionStrategy.UseLastKnownGood:
				snapshotConflictPolicy = Types.SnapshotConflictPolicy.LAST_KNOWN_GOOD;
				goto IL_85;
			case ConflictResolutionStrategy.UseMostRecentlySaved:
				snapshotConflictPolicy = Types.SnapshotConflictPolicy.MOST_RECENTLY_MODIFIED;
				goto IL_85;
			}
			snapshotConflictPolicy = Types.SnapshotConflictPolicy.MOST_RECENTLY_MODIFIED;
			IL_85:
			this.mSnapshotManager.Open(<InternalOpen>c__AnonStorey.filename, NativeSavedGameClient.AsDataSource(<InternalOpen>c__AnonStorey.source), snapshotConflictPolicy, delegate(GooglePlayGames.Native.PInvoke.SnapshotManager.OpenResponse response)
			{
				if (!response.RequestSucceeded())
				{
					<InternalOpen>c__AnonStorey.completedCallback.Invoke(NativeSavedGameClient.AsRequestStatus(response.ResponseStatus()), null);
				}
				else if (response.ResponseStatus() == CommonErrorStatus.SnapshotOpenStatus.VALID)
				{
					<InternalOpen>c__AnonStorey.completedCallback.Invoke(SavedGameRequestStatus.Success, response.Data());
				}
				else if (response.ResponseStatus() == CommonErrorStatus.SnapshotOpenStatus.VALID_WITH_CONFLICT)
				{
					NativeSnapshotMetadata original = response.ConflictOriginal();
					NativeSnapshotMetadata unmerged = response.ConflictUnmerged();
					NativeSavedGameClient.NativeConflictResolver resolver = new NativeSavedGameClient.NativeConflictResolver(<InternalOpen>c__AnonStorey.$this.mSnapshotManager, response.ConflictId(), original, unmerged, <InternalOpen>c__AnonStorey.completedCallback, delegate
					{
						<InternalOpen>c__AnonStorey.InternalOpen(<InternalOpen>c__AnonStorey.filename, <InternalOpen>c__AnonStorey.source, <InternalOpen>c__AnonStorey.resolutionStrategy, <InternalOpen>c__AnonStorey.prefetchDataOnConflict, <InternalOpen>c__AnonStorey.conflictCallback, <InternalOpen>c__AnonStorey.completedCallback);
					});
					if (!<InternalOpen>c__AnonStorey.prefetchDataOnConflict)
					{
						<InternalOpen>c__AnonStorey.conflictCallback(resolver, original, null, unmerged, null);
						return;
					}
					NativeSavedGameClient.Prefetcher prefetcher = new NativeSavedGameClient.Prefetcher(delegate(byte[] originalData, byte[] unmergedData)
					{
						<InternalOpen>c__AnonStorey.conflictCallback(resolver, original, originalData, unmerged, unmergedData);
					}, <InternalOpen>c__AnonStorey.completedCallback);
					<InternalOpen>c__AnonStorey.$this.mSnapshotManager.Read(original, new Action<GooglePlayGames.Native.PInvoke.SnapshotManager.ReadResponse>(prefetcher.OnOriginalDataRead));
					<InternalOpen>c__AnonStorey.$this.mSnapshotManager.Read(unmerged, new Action<GooglePlayGames.Native.PInvoke.SnapshotManager.ReadResponse>(prefetcher.OnUnmergedDataRead));
				}
				else
				{
					Logger.e("Unhandled response status");
					<InternalOpen>c__AnonStorey.completedCallback.Invoke(SavedGameRequestStatus.InternalError, null);
				}
			});
		}

		// Token: 0x06003201 RID: 12801 RVA: 0x001C773C File Offset: 0x001C5B3C
		public void ReadBinaryData(ISavedGameMetadata metadata, Action<SavedGameRequestStatus, byte[]> completedCallback)
		{
			Misc.CheckNotNull<ISavedGameMetadata>(metadata);
			Misc.CheckNotNull<Action<SavedGameRequestStatus, byte[]>>(completedCallback);
			completedCallback = NativeSavedGameClient.ToOnGameThread<SavedGameRequestStatus, byte[]>(completedCallback);
			NativeSnapshotMetadata nativeSnapshotMetadata = metadata as NativeSnapshotMetadata;
			if (nativeSnapshotMetadata == null)
			{
				Logger.e("Encountered metadata that was not generated by this ISavedGameClient");
				completedCallback.Invoke(SavedGameRequestStatus.BadInputError, null);
				return;
			}
			if (!nativeSnapshotMetadata.IsOpen)
			{
				Logger.e("This method requires an open ISavedGameMetadata.");
				completedCallback.Invoke(SavedGameRequestStatus.BadInputError, null);
				return;
			}
			this.mSnapshotManager.Read(nativeSnapshotMetadata, delegate(GooglePlayGames.Native.PInvoke.SnapshotManager.ReadResponse response)
			{
				if (!response.RequestSucceeded())
				{
					completedCallback.Invoke(NativeSavedGameClient.AsRequestStatus(response.ResponseStatus()), null);
				}
				else
				{
					completedCallback.Invoke(SavedGameRequestStatus.Success, response.Data());
				}
			});
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x001C77DC File Offset: 0x001C5BDC
		public void ShowSelectSavedGameUI(string uiTitle, uint maxDisplayedSavedGames, bool showCreateSaveUI, bool showDeleteSaveUI, Action<SelectUIStatus, ISavedGameMetadata> callback)
		{
			Misc.CheckNotNull<string>(uiTitle);
			Misc.CheckNotNull<Action<SelectUIStatus, ISavedGameMetadata>>(callback);
			callback = NativeSavedGameClient.ToOnGameThread<SelectUIStatus, ISavedGameMetadata>(callback);
			if (maxDisplayedSavedGames <= 0U)
			{
				Logger.e("maxDisplayedSavedGames must be greater than 0");
				callback.Invoke(SelectUIStatus.BadInputError, null);
				return;
			}
			this.mSnapshotManager.SnapshotSelectUI(showCreateSaveUI, showDeleteSaveUI, maxDisplayedSavedGames, uiTitle, delegate(GooglePlayGames.Native.PInvoke.SnapshotManager.SnapshotSelectUIResponse response)
			{
				callback.Invoke(NativeSavedGameClient.AsUIStatus(response.RequestStatus()), (!response.RequestSucceeded()) ? null : response.Data());
			});
		}

		// Token: 0x06003203 RID: 12803 RVA: 0x001C7858 File Offset: 0x001C5C58
		public void CommitUpdate(ISavedGameMetadata metadata, SavedGameMetadataUpdate updateForMetadata, byte[] updatedBinaryData, Action<SavedGameRequestStatus, ISavedGameMetadata> callback)
		{
			Misc.CheckNotNull<ISavedGameMetadata>(metadata);
			Misc.CheckNotNull<byte[]>(updatedBinaryData);
			Misc.CheckNotNull<Action<SavedGameRequestStatus, ISavedGameMetadata>>(callback);
			callback = NativeSavedGameClient.ToOnGameThread<SavedGameRequestStatus, ISavedGameMetadata>(callback);
			NativeSnapshotMetadata nativeSnapshotMetadata = metadata as NativeSnapshotMetadata;
			if (nativeSnapshotMetadata == null)
			{
				Logger.e("Encountered metadata that was not generated by this ISavedGameClient");
				callback.Invoke(SavedGameRequestStatus.BadInputError, null);
				return;
			}
			if (!nativeSnapshotMetadata.IsOpen)
			{
				Logger.e("This method requires an open ISavedGameMetadata.");
				callback.Invoke(SavedGameRequestStatus.BadInputError, null);
				return;
			}
			this.mSnapshotManager.Commit(nativeSnapshotMetadata, NativeSavedGameClient.AsMetadataChange(updateForMetadata), updatedBinaryData, delegate(GooglePlayGames.Native.PInvoke.SnapshotManager.CommitResponse response)
			{
				if (!response.RequestSucceeded())
				{
					callback.Invoke(NativeSavedGameClient.AsRequestStatus(response.ResponseStatus()), null);
				}
				else
				{
					callback.Invoke(SavedGameRequestStatus.Success, response.Data());
				}
			});
		}

		// Token: 0x06003204 RID: 12804 RVA: 0x001C7908 File Offset: 0x001C5D08
		public void FetchAllSavedGames(DataSource source, Action<SavedGameRequestStatus, List<ISavedGameMetadata>> callback)
		{
			Misc.CheckNotNull<Action<SavedGameRequestStatus, List<ISavedGameMetadata>>>(callback);
			callback = NativeSavedGameClient.ToOnGameThread<SavedGameRequestStatus, List<ISavedGameMetadata>>(callback);
			this.mSnapshotManager.FetchAll(NativeSavedGameClient.AsDataSource(source), delegate(GooglePlayGames.Native.PInvoke.SnapshotManager.FetchAllResponse response)
			{
				if (!response.RequestSucceeded())
				{
					callback.Invoke(NativeSavedGameClient.AsRequestStatus(response.ResponseStatus()), new List<ISavedGameMetadata>());
				}
				else
				{
					callback.Invoke(SavedGameRequestStatus.Success, Enumerable.ToList<ISavedGameMetadata>(Enumerable.Cast<ISavedGameMetadata>(response.Data())));
				}
			});
		}

		// Token: 0x06003205 RID: 12805 RVA: 0x001C795C File Offset: 0x001C5D5C
		public void Delete(ISavedGameMetadata metadata)
		{
			Misc.CheckNotNull<ISavedGameMetadata>(metadata);
			this.mSnapshotManager.Delete((NativeSnapshotMetadata)metadata);
		}

		// Token: 0x06003206 RID: 12806 RVA: 0x001C7976 File Offset: 0x001C5D76
		internal static bool IsValidFilename(string filename)
		{
			return filename != null && NativeSavedGameClient.ValidFilenameRegex.IsMatch(filename);
		}

		// Token: 0x06003207 RID: 12807 RVA: 0x001C798B File Offset: 0x001C5D8B
		private static Types.SnapshotConflictPolicy AsConflictPolicy(ConflictResolutionStrategy strategy)
		{
			switch (strategy)
			{
			case ConflictResolutionStrategy.UseLongestPlaytime:
				return Types.SnapshotConflictPolicy.LONGEST_PLAYTIME;
			case ConflictResolutionStrategy.UseOriginal:
				return Types.SnapshotConflictPolicy.LAST_KNOWN_GOOD;
			case ConflictResolutionStrategy.UseUnmerged:
				return Types.SnapshotConflictPolicy.MOST_RECENTLY_MODIFIED;
			default:
				throw new InvalidOperationException("Found unhandled strategy: " + strategy);
			}
		}

		// Token: 0x06003208 RID: 12808 RVA: 0x001C79BF File Offset: 0x001C5DBF
		private static SavedGameRequestStatus AsRequestStatus(CommonErrorStatus.SnapshotOpenStatus status)
		{
			switch (status + 5)
			{
			case (CommonErrorStatus.SnapshotOpenStatus)0:
				return SavedGameRequestStatus.TimeoutError;
			default:
				if (status != CommonErrorStatus.SnapshotOpenStatus.VALID)
				{
					Logger.e("Encountered unknown status: " + status);
					return SavedGameRequestStatus.InternalError;
				}
				return SavedGameRequestStatus.Success;
			case (CommonErrorStatus.SnapshotOpenStatus)2:
				return SavedGameRequestStatus.AuthenticationError;
			}
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x001C79FF File Offset: 0x001C5DFF
		private static Types.DataSource AsDataSource(DataSource source)
		{
			if (source == DataSource.ReadCacheOrNetwork)
			{
				return Types.DataSource.CACHE_OR_NETWORK;
			}
			if (source != DataSource.ReadNetworkOnly)
			{
				throw new InvalidOperationException("Found unhandled DataSource: " + source);
			}
			return Types.DataSource.NETWORK_ONLY;
		}

		// Token: 0x0600320A RID: 12810 RVA: 0x001C7A2C File Offset: 0x001C5E2C
		private static SavedGameRequestStatus AsRequestStatus(CommonErrorStatus.ResponseStatus status)
		{
			switch (status + 5)
			{
			case (CommonErrorStatus.ResponseStatus)0:
				return SavedGameRequestStatus.TimeoutError;
			case CommonErrorStatus.ResponseStatus.VALID_BUT_STALE:
				Logger.e("User was not authorized (they were probably not logged in).");
				return SavedGameRequestStatus.AuthenticationError;
			case (CommonErrorStatus.ResponseStatus)3:
				return SavedGameRequestStatus.InternalError;
			case (CommonErrorStatus.ResponseStatus)4:
				Logger.e("User attempted to use the game without a valid license.");
				return SavedGameRequestStatus.AuthenticationError;
			case (CommonErrorStatus.ResponseStatus)6:
			case (CommonErrorStatus.ResponseStatus)7:
				return SavedGameRequestStatus.Success;
			}
			Logger.e("Unknown status: " + status);
			return SavedGameRequestStatus.InternalError;
		}

		// Token: 0x0600320B RID: 12811 RVA: 0x001C7AA0 File Offset: 0x001C5EA0
		private static SelectUIStatus AsUIStatus(CommonErrorStatus.UIStatus uiStatus)
		{
			switch (uiStatus + 6)
			{
			case (CommonErrorStatus.UIStatus)0:
				return SelectUIStatus.UserClosedUI;
			case CommonErrorStatus.UIStatus.VALID:
				return SelectUIStatus.TimeoutError;
			case (CommonErrorStatus.UIStatus)3:
				return SelectUIStatus.AuthenticationError;
			case (CommonErrorStatus.UIStatus)4:
				return SelectUIStatus.InternalError;
			case (CommonErrorStatus.UIStatus)7:
				return SelectUIStatus.SavedGameSelected;
			}
			Logger.e("Encountered unknown UI Status: " + uiStatus);
			return SelectUIStatus.InternalError;
		}

		// Token: 0x0600320C RID: 12812 RVA: 0x001C7AFC File Offset: 0x001C5EFC
		private static NativeSnapshotMetadataChange AsMetadataChange(SavedGameMetadataUpdate update)
		{
			NativeSnapshotMetadataChange.Builder builder = new NativeSnapshotMetadataChange.Builder();
			if (update.IsCoverImageUpdated)
			{
				builder.SetCoverImageFromPngData(update.UpdatedPngCoverImage);
			}
			if (update.IsDescriptionUpdated)
			{
				builder.SetDescription(update.UpdatedDescription);
			}
			if (update.IsPlayedTimeUpdated)
			{
				builder.SetPlayedTime((ulong)update.UpdatedPlayedTime.Value.TotalMilliseconds);
			}
			return builder.Build();
		}

		// Token: 0x0600320D RID: 12813 RVA: 0x001C7B74 File Offset: 0x001C5F74
		private static Action<T1, T2> ToOnGameThread<T1, T2>(Action<T1, T2> toConvert)
		{
			return delegate(T1 val1, T2 val2)
			{
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					toConvert.Invoke(val1, val2);
				});
			};
		}

		// Token: 0x040032A6 RID: 12966
		private static readonly Regex ValidFilenameRegex = new Regex("\\A[a-zA-Z0-9-._~]{1,100}\\Z");

		// Token: 0x040032A7 RID: 12967
		private readonly GooglePlayGames.Native.PInvoke.SnapshotManager mSnapshotManager;

		// Token: 0x020006CE RID: 1742
		private class NativeConflictResolver : IConflictResolver
		{
			// Token: 0x0600320F RID: 12815 RVA: 0x001C7BAC File Offset: 0x001C5FAC
			internal NativeConflictResolver(GooglePlayGames.Native.PInvoke.SnapshotManager manager, string conflictId, NativeSnapshotMetadata original, NativeSnapshotMetadata unmerged, Action<SavedGameRequestStatus, ISavedGameMetadata> completeCallback, Action retryOpen)
			{
				this.mManager = Misc.CheckNotNull<GooglePlayGames.Native.PInvoke.SnapshotManager>(manager);
				this.mConflictId = Misc.CheckNotNull<string>(conflictId);
				this.mOriginal = Misc.CheckNotNull<NativeSnapshotMetadata>(original);
				this.mUnmerged = Misc.CheckNotNull<NativeSnapshotMetadata>(unmerged);
				this.mCompleteCallback = Misc.CheckNotNull<Action<SavedGameRequestStatus, ISavedGameMetadata>>(completeCallback);
				this.mRetryFileOpen = Misc.CheckNotNull<Action>(retryOpen);
			}

			// Token: 0x06003210 RID: 12816 RVA: 0x001C7C0C File Offset: 0x001C600C
			public void ResolveConflict(ISavedGameMetadata chosenMetadata, SavedGameMetadataUpdate metadataUpdate, byte[] updatedData)
			{
				NativeSnapshotMetadata nativeSnapshotMetadata = chosenMetadata as NativeSnapshotMetadata;
				if (nativeSnapshotMetadata != this.mOriginal && nativeSnapshotMetadata != this.mUnmerged)
				{
					Logger.e("Caller attempted to choose a version of the metadata that was not part of the conflict");
					this.mCompleteCallback.Invoke(SavedGameRequestStatus.BadInputError, null);
					return;
				}
				NativeSnapshotMetadataChange nativeSnapshotMetadataChange = new NativeSnapshotMetadataChange.Builder().From(metadataUpdate).Build();
				this.mManager.Resolve(nativeSnapshotMetadata, nativeSnapshotMetadataChange, this.mConflictId, updatedData, delegate(GooglePlayGames.Native.PInvoke.SnapshotManager.OpenResponse response)
				{
					if (!response.RequestSucceeded())
					{
						this.mCompleteCallback.Invoke(NativeSavedGameClient.AsRequestStatus(response.ResponseStatus()), null);
						return;
					}
					this.mRetryFileOpen.Invoke();
				});
			}

			// Token: 0x06003211 RID: 12817 RVA: 0x001C7C84 File Offset: 0x001C6084
			public void ChooseMetadata(ISavedGameMetadata chosenMetadata)
			{
				NativeSnapshotMetadata nativeSnapshotMetadata = chosenMetadata as NativeSnapshotMetadata;
				if (nativeSnapshotMetadata != this.mOriginal && nativeSnapshotMetadata != this.mUnmerged)
				{
					Logger.e("Caller attempted to choose a version of the metadata that was not part of the conflict");
					this.mCompleteCallback.Invoke(SavedGameRequestStatus.BadInputError, null);
					return;
				}
				this.mManager.Resolve(nativeSnapshotMetadata, new NativeSnapshotMetadataChange.Builder().Build(), this.mConflictId, delegate(GooglePlayGames.Native.PInvoke.SnapshotManager.OpenResponse response)
				{
					if (!response.RequestSucceeded())
					{
						this.mCompleteCallback.Invoke(NativeSavedGameClient.AsRequestStatus(response.ResponseStatus()), null);
						return;
					}
					this.mRetryFileOpen.Invoke();
				});
			}

			// Token: 0x040032A8 RID: 12968
			private readonly GooglePlayGames.Native.PInvoke.SnapshotManager mManager;

			// Token: 0x040032A9 RID: 12969
			private readonly string mConflictId;

			// Token: 0x040032AA RID: 12970
			private readonly NativeSnapshotMetadata mOriginal;

			// Token: 0x040032AB RID: 12971
			private readonly NativeSnapshotMetadata mUnmerged;

			// Token: 0x040032AC RID: 12972
			private readonly Action<SavedGameRequestStatus, ISavedGameMetadata> mCompleteCallback;

			// Token: 0x040032AD RID: 12973
			private readonly Action mRetryFileOpen;
		}

		// Token: 0x020006CF RID: 1743
		private class Prefetcher
		{
			// Token: 0x06003214 RID: 12820 RVA: 0x001C7D51 File Offset: 0x001C6151
			internal Prefetcher(Action<byte[], byte[]> dataFetchedCallback, Action<SavedGameRequestStatus, ISavedGameMetadata> completedCallback)
			{
				this.mDataFetchedCallback = Misc.CheckNotNull<Action<byte[], byte[]>>(dataFetchedCallback);
				this.completedCallback = Misc.CheckNotNull<Action<SavedGameRequestStatus, ISavedGameMetadata>>(completedCallback);
			}

			// Token: 0x06003215 RID: 12821 RVA: 0x001C7D7C File Offset: 0x001C617C
			internal void OnOriginalDataRead(GooglePlayGames.Native.PInvoke.SnapshotManager.ReadResponse readResponse)
			{
				object obj = this.mLock;
				lock (obj)
				{
					if (!readResponse.RequestSucceeded())
					{
						Logger.e("Encountered error while prefetching original data.");
						this.completedCallback.Invoke(NativeSavedGameClient.AsRequestStatus(readResponse.ResponseStatus()), null);
						this.completedCallback = delegate
						{
						};
					}
					else
					{
						Logger.d("Successfully fetched original data");
						this.mOriginalDataFetched = true;
						this.mOriginalData = readResponse.Data();
						this.MaybeProceed();
					}
				}
			}

			// Token: 0x06003216 RID: 12822 RVA: 0x001C7E2C File Offset: 0x001C622C
			internal void OnUnmergedDataRead(GooglePlayGames.Native.PInvoke.SnapshotManager.ReadResponse readResponse)
			{
				object obj = this.mLock;
				lock (obj)
				{
					if (!readResponse.RequestSucceeded())
					{
						Logger.e("Encountered error while prefetching unmerged data.");
						this.completedCallback.Invoke(NativeSavedGameClient.AsRequestStatus(readResponse.ResponseStatus()), null);
						this.completedCallback = delegate
						{
						};
					}
					else
					{
						Logger.d("Successfully fetched unmerged data");
						this.mUnmergedDataFetched = true;
						this.mUnmergedData = readResponse.Data();
						this.MaybeProceed();
					}
				}
			}

			// Token: 0x06003217 RID: 12823 RVA: 0x001C7EDC File Offset: 0x001C62DC
			private void MaybeProceed()
			{
				if (this.mOriginalDataFetched && this.mUnmergedDataFetched)
				{
					Logger.d("Fetched data for original and unmerged, proceeding");
					this.mDataFetchedCallback.Invoke(this.mOriginalData, this.mUnmergedData);
				}
				else
				{
					Logger.d(string.Concat(new object[] { "Not all data fetched - original:", this.mOriginalDataFetched, " unmerged:", this.mUnmergedDataFetched }));
				}
			}

			// Token: 0x040032AE RID: 12974
			private readonly object mLock = new object();

			// Token: 0x040032AF RID: 12975
			private bool mOriginalDataFetched;

			// Token: 0x040032B0 RID: 12976
			private byte[] mOriginalData;

			// Token: 0x040032B1 RID: 12977
			private bool mUnmergedDataFetched;

			// Token: 0x040032B2 RID: 12978
			private byte[] mUnmergedData;

			// Token: 0x040032B3 RID: 12979
			private Action<SavedGameRequestStatus, ISavedGameMetadata> completedCallback;

			// Token: 0x040032B4 RID: 12980
			private readonly Action<byte[], byte[]> mDataFetchedCallback;
		}
	}
}
