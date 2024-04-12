using System;
using GooglePlayGames.Android;
using GooglePlayGames.BasicApi;
using GooglePlayGames.Native;
using GooglePlayGames.OurUtils;
using UnityEngine;

namespace GooglePlayGames
{
	// Token: 0x0200072D RID: 1837
	internal class PlayGamesClientFactory
	{
		// Token: 0x06003530 RID: 13616 RVA: 0x001CF68A File Offset: 0x001CDA8A
		internal static IPlayGamesClient GetPlatformPlayGamesClient(PlayGamesClientConfiguration config)
		{
			if (Application.isEditor)
			{
				Logger.d("Creating IPlayGamesClient in editor, using DummyClient.");
				return new DummyClient();
			}
			Logger.d("Creating Android IPlayGamesClient Client");
			return new NativeClient(config, new AndroidClient());
		}
	}
}
