using System;

namespace GooglePlayGames.BasicApi.SavedGame
{
	// Token: 0x020005F5 RID: 1525
	public interface IConflictResolver
	{
		// Token: 0x06002C63 RID: 11363
		void ChooseMetadata(ISavedGameMetadata chosenMetadata);

		// Token: 0x06002C64 RID: 11364
		void ResolveConflict(ISavedGameMetadata chosenMetadata, SavedGameMetadataUpdate metadataUpdate, byte[] updatedData);
	}
}
