using System;

namespace GooglePlayGames.BasicApi.SavedGame
{
	// Token: 0x020005F6 RID: 1526
	public interface ISavedGameMetadata
	{
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06002C65 RID: 11365
		bool IsOpen { get; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06002C66 RID: 11366
		string Filename { get; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06002C67 RID: 11367
		string Description { get; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06002C68 RID: 11368
		string CoverImageURL { get; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06002C69 RID: 11369
		TimeSpan TotalTimePlayed { get; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06002C6A RID: 11370
		DateTime LastModifiedTimestamp { get; }
	}
}
