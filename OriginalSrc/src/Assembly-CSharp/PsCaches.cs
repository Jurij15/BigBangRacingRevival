using System;
using UnityEngine;

// Token: 0x02000155 RID: 341
public static class PsCaches
{
	// Token: 0x06000C57 RID: 3159 RVA: 0x00075140 File Offset: 0x00073540
	public static void Initialize()
	{
		PsCaches.m_screenshots = new ItemCache<byte[]>("SCREENSHOT_CACHE", 50, -1f);
		PsCaches.m_screenshotsDiskCache = new ByteArrayDiskCache("SCREENSHOT_DISKCACHE", 300);
		PsCaches.m_urlPictures = new ItemCache<byte[]>("URLPICTURE_CACHE", 50, -1f);
		PsCaches.m_urlPicturesDiskCache = new ByteArrayDiskCache("URLPICTURE_DISKCACHE", 300);
		PsCaches.m_profileImages = new ItemCache<Texture2D>("PROFILE_IMAGES", 100, -1f);
		PsCaches.m_latestList = new ListCache<PsMinigameMetaData>("MINIGAMES_LATEST", 100f);
		PsCaches.m_trendingList = new ListCache<PsMinigameMetaData>("MINIGAMES_TRENDING", 100f);
		PsCaches.m_popularList = new ListCache<PsMinigameMetaData>("MINIGAMES_POPULAR", 100f);
		PsCaches.m_unratedList = new ListCache<PsMinigameMetaData>("MINIGAMES_NEW", 100f);
		PsCaches.m_savedList = new ListCache<PsMinigameMetaData>("MINIGAMES_SAVED", 100f);
		PsCaches.m_publishedList = new ListCache<PsMinigameMetaData>("MINIGAMES_PUBLISHED", 5f);
		PsCaches.m_signalList = new ListCache<PsReseachMetaData>("ITEMS_SIGNALS", 100f);
		PsCaches.m_searchedLevelList = new ListCache<PsMinigameMetaData>("SEARCHED_LEVELS", -1f);
		PsCaches.m_friendsLevelList = new ListCache<PsMinigameMetaData[]>("FRIEND_LEVELS", -1f);
		PsCaches.m_searchedPlayersList = new ListCache<PlayerData>("SEARCHED_PLAYERS", -1f);
		PsCaches.m_searchedPlayersLevelList = new ListCache<PsMinigameMetaData[]>("SEARCHED_PLAYER_LEVELS", -1f);
	}

	// Token: 0x04000AB3 RID: 2739
	public static ItemCache<byte[]> m_screenshots;

	// Token: 0x04000AB4 RID: 2740
	public static ByteArrayDiskCache m_screenshotsDiskCache;

	// Token: 0x04000AB5 RID: 2741
	public static ItemCache<Texture2D> m_profileImages;

	// Token: 0x04000AB6 RID: 2742
	public static ItemCache<byte[]> m_urlPictures;

	// Token: 0x04000AB7 RID: 2743
	public static ByteArrayDiskCache m_urlPicturesDiskCache;

	// Token: 0x04000AB8 RID: 2744
	public static ListCache<PsMinigameMetaData> m_latestList;

	// Token: 0x04000AB9 RID: 2745
	public static ListCache<PsMinigameMetaData> m_trendingList;

	// Token: 0x04000ABA RID: 2746
	public static ListCache<PsMinigameMetaData> m_popularList;

	// Token: 0x04000ABB RID: 2747
	public static ListCache<PsMinigameMetaData> m_unratedList;

	// Token: 0x04000ABC RID: 2748
	public static ListCache<PsMinigameMetaData> m_savedList;

	// Token: 0x04000ABD RID: 2749
	public static ListCache<PsMinigameMetaData> m_publishedList;

	// Token: 0x04000ABE RID: 2750
	public static ListCache<PsReseachMetaData> m_signalList;

	// Token: 0x04000ABF RID: 2751
	public static ListCache<PsMinigameMetaData> m_searchedLevelList;

	// Token: 0x04000AC0 RID: 2752
	public static ListCache<PsMinigameMetaData[]> m_friendsLevelList;

	// Token: 0x04000AC1 RID: 2753
	public static ListCache<PlayerData> m_searchedPlayersList;

	// Token: 0x04000AC2 RID: 2754
	public static ListCache<PsMinigameMetaData[]> m_searchedPlayersLevelList;
}
