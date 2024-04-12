using System;
using System.Collections.Generic;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.Native
{
	// Token: 0x0200072B RID: 1835
	internal class UnsupportedSavedGamesClient : ISavedGameClient
	{
		// Token: 0x06003525 RID: 13605 RVA: 0x001CF591 File Offset: 0x001CD991
		public UnsupportedSavedGamesClient(string message)
		{
			this.mMessage = Misc.CheckNotNull<string>(message);
		}

		// Token: 0x06003526 RID: 13606 RVA: 0x001CF5A5 File Offset: 0x001CD9A5
		public void OpenWithAutomaticConflictResolution(string filename, DataSource source, ConflictResolutionStrategy resolutionStrategy, Action<SavedGameRequestStatus, ISavedGameMetadata> callback)
		{
			throw new NotImplementedException(this.mMessage);
		}

		// Token: 0x06003527 RID: 13607 RVA: 0x001CF5B2 File Offset: 0x001CD9B2
		public void OpenWithManualConflictResolution(string filename, DataSource source, bool prefetchDataOnConflict, ConflictCallback conflictCallback, Action<SavedGameRequestStatus, ISavedGameMetadata> completedCallback)
		{
			throw new NotImplementedException(this.mMessage);
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x001CF5BF File Offset: 0x001CD9BF
		public void ReadBinaryData(ISavedGameMetadata metadata, Action<SavedGameRequestStatus, byte[]> completedCallback)
		{
			throw new NotImplementedException(this.mMessage);
		}

		// Token: 0x06003529 RID: 13609 RVA: 0x001CF5CC File Offset: 0x001CD9CC
		public void ShowSelectSavedGameUI(string uiTitle, uint maxDisplayedSavedGames, bool showCreateSaveUI, bool showDeleteSaveUI, Action<SelectUIStatus, ISavedGameMetadata> callback)
		{
			throw new NotImplementedException(this.mMessage);
		}

		// Token: 0x0600352A RID: 13610 RVA: 0x001CF5D9 File Offset: 0x001CD9D9
		public void CommitUpdate(ISavedGameMetadata metadata, SavedGameMetadataUpdate updateForMetadata, byte[] updatedBinaryData, Action<SavedGameRequestStatus, ISavedGameMetadata> callback)
		{
			throw new NotImplementedException(this.mMessage);
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x001CF5E6 File Offset: 0x001CD9E6
		public void FetchAllSavedGames(DataSource source, Action<SavedGameRequestStatus, List<ISavedGameMetadata>> callback)
		{
			throw new NotImplementedException(this.mMessage);
		}

		// Token: 0x0600352C RID: 13612 RVA: 0x001CF5F3 File Offset: 0x001CD9F3
		public void Delete(ISavedGameMetadata metadata)
		{
			throw new NotImplementedException(this.mMessage);
		}

		// Token: 0x04003339 RID: 13113
		private readonly string mMessage;
	}
}
