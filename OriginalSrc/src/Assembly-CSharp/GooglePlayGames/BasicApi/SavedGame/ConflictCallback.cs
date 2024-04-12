using System;

namespace GooglePlayGames.BasicApi.SavedGame
{
	// Token: 0x020005F3 RID: 1523
	// (Invoke) Token: 0x06002C59 RID: 11353
	public delegate void ConflictCallback(IConflictResolver resolver, ISavedGameMetadata original, byte[] originalData, ISavedGameMetadata unmerged, byte[] unmergedData);
}
