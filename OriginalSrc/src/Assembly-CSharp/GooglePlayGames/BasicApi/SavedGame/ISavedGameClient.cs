using System;
using System.Collections.Generic;

namespace GooglePlayGames.BasicApi.SavedGame
{
	// Token: 0x020005F4 RID: 1524
	public interface ISavedGameClient
	{
		// Token: 0x06002C5C RID: 11356
		void OpenWithAutomaticConflictResolution(string filename, DataSource source, ConflictResolutionStrategy resolutionStrategy, Action<SavedGameRequestStatus, ISavedGameMetadata> callback);

		// Token: 0x06002C5D RID: 11357
		void OpenWithManualConflictResolution(string filename, DataSource source, bool prefetchDataOnConflict, ConflictCallback conflictCallback, Action<SavedGameRequestStatus, ISavedGameMetadata> completedCallback);

		// Token: 0x06002C5E RID: 11358
		void ReadBinaryData(ISavedGameMetadata metadata, Action<SavedGameRequestStatus, byte[]> completedCallback);

		// Token: 0x06002C5F RID: 11359
		void ShowSelectSavedGameUI(string uiTitle, uint maxDisplayedSavedGames, bool showCreateSaveUI, bool showDeleteSaveUI, Action<SelectUIStatus, ISavedGameMetadata> callback);

		// Token: 0x06002C60 RID: 11360
		void CommitUpdate(ISavedGameMetadata metadata, SavedGameMetadataUpdate updateForMetadata, byte[] updatedBinaryData, Action<SavedGameRequestStatus, ISavedGameMetadata> callback);

		// Token: 0x06002C61 RID: 11361
		void FetchAllSavedGames(DataSource source, Action<SavedGameRequestStatus, List<ISavedGameMetadata>> callback);

		// Token: 0x06002C62 RID: 11362
		void Delete(ISavedGameMetadata metadata);
	}
}
