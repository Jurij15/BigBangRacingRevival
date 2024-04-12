using System;

// Token: 0x0200043F RID: 1087
public static class ServerConfig
{
	// Token: 0x06001E13 RID: 7699 RVA: 0x00155F76 File Offset: 0x00154376
	public static string GetHPW()
	{
		return "bfid3Z53SFib325PJGFasae";
	}

	// Token: 0x040020BB RID: 8379
	public const string HPW = "bfid3Z53SFib325PJGFasae";

	// Token: 0x040020BC RID: 8380
	public const string CONTENT_JSON = "application/json";

	// Token: 0x040020BD RID: 8381
	public const string CONTENT_BYTE = "application/octet-stream";

	// Token: 0x040020BE RID: 8382
	public const string CONTENT_GHOST = "application/ghost";

	// Token: 0x040020BF RID: 8383
	public const string NETWORK_FACEBOOK = "facebook";

	// Token: 0x040020C0 RID: 8384
	public const string NETWORK_GAMECENTER = "gamecenter";

	// Token: 0x040020C1 RID: 8385
	public const string NETWORK_GOOGLE = "google";

	// Token: 0x040020C2 RID: 8386
	public const string SEARCH_GAMES_AND_PLAYERS = "/v1/search/gameAndPlayer";

	// Token: 0x040020C3 RID: 8387
	public const string REPORT_ABUSE = "/v1/abuse/report";

	// Token: 0x040020C4 RID: 8388
	public const string PLAYER_SETTINGS_UPDATE = "/v1/player/settings";

	// Token: 0x040020C5 RID: 8389
	public const string PLAYER_CREATE = "/v4/player/login";

	// Token: 0x040020C6 RID: 8390
	public const string PLAYER_STATS_CHANGE = "/v1/player/stats/change";

	// Token: 0x040020C7 RID: 8391
	public const string GAME_CENTER_SWITCH = "/v2/player/join/gameCenter";

	// Token: 0x040020C8 RID: 8392
	public const string GAME_CENTER_CHECK = "/v2/player/check/gameCenter";

	// Token: 0x040020C9 RID: 8393
	public const string FACEBOOK_JOIN = "/v2/player/join/facebook";

	// Token: 0x040020CA RID: 8394
	public const string NINJA_JOIN = "/v1/player/join/ninja";

	// Token: 0x040020CB RID: 8395
	public const string FOLLOW_PLAYER = "/v1/player/follow/add";

	// Token: 0x040020CC RID: 8396
	public const string UNFOLLOW_PLAYER = "/v1/player/follow/remove";

	// Token: 0x040020CD RID: 8397
	public const string FOLLOWEES = "/v1/player/follow/followees";

	// Token: 0x040020CE RID: 8398
	public const string FOLLOWERS = "/v1/player/follow/followers";

	// Token: 0x040020CF RID: 8399
	public const string FRIENDS = "/v2/player/friends";

	// Token: 0x040020D0 RID: 8400
	public const string SET_NUMBER = "/v1/player/data/setNumber";

	// Token: 0x040020D1 RID: 8401
	public const string SET_STRING = "/v1/player/data/setString";

	// Token: 0x040020D2 RID: 8402
	public const string FIND_PLAYER_DATA = "/v1/player/data/find";

	// Token: 0x040020D3 RID: 8403
	public const string REMOVE_PLAYER_DATA = "/v1/player/data/remove";

	// Token: 0x040020D4 RID: 8404
	public const string PLAYER_SET_MAX_TESTERS = "/v1/player/tester/setMax";

	// Token: 0x040020D5 RID: 8405
	public const string USE_TESTER = "/v1/player/tester/use";

	// Token: 0x040020D6 RID: 8406
	public const string RESET_LEVEL = "/v1/player/resetLevel";

	// Token: 0x040020D7 RID: 8407
	public const string PURCHASE_TESTER = "/v1/player/tester/purchase";

	// Token: 0x040020D8 RID: 8408
	public const string PLAYER_STATS_SET = "/v1/player/stats/set";

	// Token: 0x040020D9 RID: 8409
	public const string PLAYER_DATA_SET_V2 = "/v2/player/data/change";

	// Token: 0x040020DA RID: 8410
	public const string PLAYER_VEHICLE_RENT = "/v1/player/rent";

	// Token: 0x040020DB RID: 8411
	public const string SKIP_LEVEL = "/v1/player/skip";

	// Token: 0x040020DC RID: 8412
	public const string PLAYER_NAME_CHANGE = "/v2/player/changeName";

	// Token: 0x040020DD RID: 8413
	public const string PLAYER_YOUTUBER_CHANGE = "/v1/player/changeYoutuber";

	// Token: 0x040020DE RID: 8414
	public const string REMOVE_PLAYER = "/v1/player/remove";

	// Token: 0x040020DF RID: 8415
	public const string SWITCH_PLAYER = "/v1/player/switch";

	// Token: 0x040020E0 RID: 8416
	public const string PLAYER_SKIP_TIMER = "/v1/player/skipTimeLeft";

	// Token: 0x040020E1 RID: 8417
	public const string RESET_SKIP_TIMER = "/v1/player/resetSkipTimer";

	// Token: 0x040020E2 RID: 8418
	public const string GET_PLAYER_INFO = "/v1/player/info";

	// Token: 0x040020E3 RID: 8419
	public const string GET_PLAYER_STATS = "/v1/player/stats/find";

	// Token: 0x040020E4 RID: 8420
	public const string SOCIAL_CHECK = "/v1/player/check/social";

	// Token: 0x040020E5 RID: 8421
	public const string SOCIAL_SOLVE = "/v1/player/solve/social";

	// Token: 0x040020E6 RID: 8422
	public const string SOCIAL_FOLLOW = "/v1/player/follow/social";

	// Token: 0x040020E7 RID: 8423
	public const string SOCIAL_REMOVE = "/v1/player/remove/social";

	// Token: 0x040020E8 RID: 8424
	public const string GET_PLAYER_SOCIAL_PROFILE = "/v1/player/get/social";

	// Token: 0x040020E9 RID: 8425
	public const string OPEN_CHEST = "/v1/player/openChest";

	// Token: 0x040020EA RID: 8426
	public const string ADD_CUSTOMISATION = "/v1/customisation/add";

	// Token: 0x040020EB RID: 8427
	public const string UPSERT_ACHIEVEMENT = "/v1/achievement/upsert";

	// Token: 0x040020EC RID: 8428
	public const string MINIGAME_GET = "/v1/minigame/meta/find";

	// Token: 0x040020ED RID: 8429
	public const string MINIGAME_OWN_PUBLISHED_FIND = "/v1/minigame/own/published";

	// Token: 0x040020EE RID: 8430
	public const string MINIGAME_OWN_FIND = "/v2/minigame/own";

	// Token: 0x040020EF RID: 8431
	public const string MINIGAME_OWN_SAVED_FIND = "/v1/minigame/own/saved";

	// Token: 0x040020F0 RID: 8432
	public const string MINIGAME_FIND_WITH_ITEMS = "/v1/minigame/meta/findItems";

	// Token: 0x040020F1 RID: 8433
	public const string MINIGAME_DATA_FIND = "/v1/minigame/data/find";

	// Token: 0x040020F2 RID: 8434
	public const string MINIGAME_DATA_SAVE = "/v1/minigame/data/save";

	// Token: 0x040020F3 RID: 8435
	public const string MINIGAME_SAVE = "/v2/minigame/save";

	// Token: 0x040020F4 RID: 8436
	public const string MINIGAME_BACK_TO_SAVED = "/v1/minigame/backtosaved";

	// Token: 0x040020F5 RID: 8437
	public const string MINIGAME_PUBLISH = "/v4/minigame/publish";

	// Token: 0x040020F6 RID: 8438
	public const string MINIGAME_HIDE = "/v1/minigame/hide";

	// Token: 0x040020F7 RID: 8439
	public const string MINIGAME_LIKE = "/v1/minigame/like/save";

	// Token: 0x040020F8 RID: 8440
	public const string MINIGAME_DELETE = "/v1/minigame/delete";

	// Token: 0x040020F9 RID: 8441
	public const string MINIGAME_TREND_FIND = "/v1/minigame/trend/find";

	// Token: 0x040020FA RID: 8442
	public const string MINIGAME_POPULAR_FIND = "/v1/minigame/popular/find";

	// Token: 0x040020FB RID: 8443
	public const string MINIGAME_UNRATED_FIND = "/v1/minigame/unrated/find";

	// Token: 0x040020FC RID: 8444
	public const string MINIGAME_ONE_FRESH = "/v1/minigame/oneFresh";

	// Token: 0x040020FD RID: 8445
	public const string MINIGAME_FOLLOWEE_FIND = "/v1/minigame/followee/find";

	// Token: 0x040020FE RID: 8446
	public const string MINIGAME_LEVEL_UPDATE = "/v1/minigame/level/update";

	// Token: 0x040020FF RID: 8447
	public const string MINIGAME_REWARD_CLAIM = "/v1/minigame/claim";

	// Token: 0x04002100 RID: 8448
	public const string MINIGAME_FOLLOWEE_PUBLISHED_FIND = "/v1/minigame/followee/published";

	// Token: 0x04002101 RID: 8449
	public const string MINIGAME_HISTORY_FIND = "/v1/minigame/history";

	// Token: 0x04002102 RID: 8450
	public const string MINIGAME_SUBGENRE_FIND = "/v1/minigame/subgenre/find";

	// Token: 0x04002103 RID: 8451
	public const string MINIGAME_SEARCH = "/v2/minigame/meta/search";

	// Token: 0x04002104 RID: 8452
	public const string MINIGAME_HIDDEN_FIND = "/v1/minigame/hidden";

	// Token: 0x04002105 RID: 8453
	public const string MINIGAME_REWARDS_CLAIM = "/v1/minigame/claimAll";

	// Token: 0x04002106 RID: 8454
	public const string MINIGAME_START = "/v1/minigame/start";

	// Token: 0x04002107 RID: 8455
	public const string CHALLENGE_GET_DAILY = "/v1/challenge/daily/find";

	// Token: 0x04002108 RID: 8456
	public const string VERSUS_CREATE = "/v4/versus/create";

	// Token: 0x04002109 RID: 8457
	public const string VERSUS_SCORE_OVERWRITE = "/v2/versus/score/overwrite";

	// Token: 0x0400210A RID: 8458
	public const string VERSUS_QUIT = "/v1/versus/score/quit";

	// Token: 0x0400210B RID: 8459
	public const string VERSUS_WIN = "/v1/versus/win";

	// Token: 0x0400210C RID: 8460
	public const string VERSUS_FORFEIT = "/v1/versus/forfeit";

	// Token: 0x0400210D RID: 8461
	public const string VERSUS_SET_TRIES = "/v1/versus/tries/set";

	// Token: 0x0400210E RID: 8462
	public const string VERSUS_CLAIM = "/v1/versus/claim";

	// Token: 0x0400210F RID: 8463
	public const string VERSUS_CLAIM_SAVE = "/v1/versus/claimAndSave";

	// Token: 0x04002110 RID: 8464
	public const string FRIENDLY_CREATE = "/v1/friendly/create";

	// Token: 0x04002111 RID: 8465
	public const string FRIENDLY_JOIN = "/v1/friendly/join";

	// Token: 0x04002112 RID: 8466
	public const string FRIENDLY_DECLINE = "/v1/friendly/decline";

	// Token: 0x04002113 RID: 8467
	public const string FRIENDLY_SCORE_OVERWRITE = "/v1/friendly/score/overwrite";

	// Token: 0x04002114 RID: 8468
	public const string FRIENDLY_QUIT = "/v1/friendly/score/quit";

	// Token: 0x04002115 RID: 8469
	public const string FRIENDLY_WIN = "/v1/friendly/win";

	// Token: 0x04002116 RID: 8470
	public const string FRIENDLY_FORFEIT = "/v1/friendly/forfeit";

	// Token: 0x04002117 RID: 8471
	public const string FRIENDLY_SET_TRIES = "/v1/friendly/tries/set";

	// Token: 0x04002118 RID: 8472
	public const string FRIENDLY_CLAIM = "/v1/friendly/claim";

	// Token: 0x04002119 RID: 8473
	public const string FRIENDLY_CLAIM_SAVE = "/v1/friendly/claimAndSave";

	// Token: 0x0400211A RID: 8474
	public const string SCREENSHOT_SAVE = "/v1/minigame/screenshot/save";

	// Token: 0x0400211B RID: 8475
	public const string SCREENSHOT_FIND = "/v1/minigame/screenshot/find";

	// Token: 0x0400211C RID: 8476
	public const string RATING_SAVE = "/v1/rating/save";

	// Token: 0x0400211D RID: 8477
	public const string GHOST_ADVENTURE_BATTLE = "/v1/ghost/bossbattle/get";

	// Token: 0x0400211E RID: 8478
	public const string BATTLE_MINIGAME_SEARCH = "/v1/bossbattle/new";

	// Token: 0x0400211F RID: 8479
	public const string GHOST_CREATOR = "/v1/ghost/creator/get";

	// Token: 0x04002120 RID: 8480
	public const string GHOST_VERSUS = "/v1/ghost/versus/get";

	// Token: 0x04002121 RID: 8481
	public const string GHOST_FRIENDLY = "/v1/ghost/friendly/get";

	// Token: 0x04002122 RID: 8482
	public const string TROPHY_GHOST_GET_BY_IDS = "/v1/trophy/ghostsByIds";

	// Token: 0x04002123 RID: 8483
	public const string TROPHY_GHOST_GET_BY_TIME = "/v1/trophy/ghostsByTime";

	// Token: 0x04002124 RID: 8484
	public const string TROPHY_GHOST_GET_BY_TROPHIES = "/v1/trophy/ghostsByTrophies";

	// Token: 0x04002125 RID: 8485
	public const string TROPHY_WIN = "/v1/trophy/win";

	// Token: 0x04002126 RID: 8486
	public const string TROPHY_LOSE = "/v1/trophy/lose";

	// Token: 0x04002127 RID: 8487
	public const string TROPHY_LEADERBOARD = "/v1/trophy/leaderboard";

	// Token: 0x04002128 RID: 8488
	public const string TROPHY_GAME_LEADERBOARD = "/v1/trophy/leaderboardByGame";

	// Token: 0x04002129 RID: 8489
	public const string TROPHY_SCORE_SEND = "/v1/trophy/score/send";

	// Token: 0x0400212A RID: 8490
	public const string STARCOLLECT_WIN = "/v1/starcollect/win";

	// Token: 0x0400212B RID: 8491
	public const string STARCOLLECT_LOSE = "/v1/starcollect/lose";

	// Token: 0x0400212C RID: 8492
	public const string CREATOR_LEADERBOARD = "/v1/creator/top";

	// Token: 0x0400212D RID: 8493
	public const string HIGHSCORE_SEND = "/v5/highscore/send";

	// Token: 0x0400212E RID: 8494
	public const string HIGHSCORE_FIND = "/v2/highscore/find";

	// Token: 0x0400212F RID: 8495
	public const string HIGHSCORE_QUIT = "/v2/highscore/quit";

	// Token: 0x04002130 RID: 8496
	public const string HIGHSCORE_NEXT = "/v1/highscore/next";

	// Token: 0x04002131 RID: 8497
	public const string COMMENT_SAVE = "/v2/minigame/comment/save";

	// Token: 0x04002132 RID: 8498
	public const string COMMENT_FIND = "/v2/minigame/comment/find";

	// Token: 0x04002133 RID: 8499
	public const string PUSH_TOKEN_SEND = "/v1/push/token/save";

	// Token: 0x04002134 RID: 8500
	public const string NOTIFICATIONS_FIND = "/v1/notification/find";

	// Token: 0x04002135 RID: 8501
	public const string NOTIFICATIONS_COUNT = "/v1/notification/count";

	// Token: 0x04002136 RID: 8502
	public const string PLANET_META_FIND = "/v1/planet/current/find";

	// Token: 0x04002137 RID: 8503
	public const string REWARD_CLAIM = "/v1/reward/claim";

	// Token: 0x04002138 RID: 8504
	public const string TIMED_EVENT_GET = "/v1/metagame/getTimed";

	// Token: 0x04002139 RID: 8505
	public const string PATH_DB_FIND = "/v1/path/db/find";

	// Token: 0x0400213A RID: 8506
	public const string ADNETWORK_CONFIG_GET = "/v1/ads/config";

	// Token: 0x0400213B RID: 8507
	public const string ANALYTICS_TRACK_PURCHASE = "/v1/analytics/trackPurchase";

	// Token: 0x0400213C RID: 8508
	public const string ANALYTICS_TRACK_EVENT = "/v1/analytics/trackEvent";

	// Token: 0x0400213D RID: 8509
	public const string IAP_CONFIG_GET = "/v1/iap/config";

	// Token: 0x0400213E RID: 8510
	public const string IAP_COMPLETE_PURCHASE = "/v1/iap/purchase";

	// Token: 0x0400213F RID: 8511
	public const string IAP_NONCE_GET = "/v1/iap/nonce";

	// Token: 0x04002140 RID: 8512
	public const string PRELOAD_FILE_CHECK = "/v1/preload/checkFile";

	// Token: 0x04002141 RID: 8513
	public const string PRELOAD_VERSION_CHECK = "/v1/preload/checkVersion";

	// Token: 0x04002142 RID: 8514
	public const string TEAM_GET = "/v1/team/get";

	// Token: 0x04002143 RID: 8515
	public const string TEAM_UPDATE = "/v1/team/update";

	// Token: 0x04002144 RID: 8516
	public const string TEAM_JOIN = "/v1/team/join";

	// Token: 0x04002145 RID: 8517
	public const string TEAM_LEAVE = "/v1/team/leave";

	// Token: 0x04002146 RID: 8518
	public const string TEAM_LEADERBOARD = "/v1/team/leaderboard";

	// Token: 0x04002147 RID: 8519
	public const string TEAM_SEARCH = "/v1/team/search";

	// Token: 0x04002148 RID: 8520
	public const string TEAM_SUGGEST = "/v1/team/suggest";

	// Token: 0x04002149 RID: 8521
	public const string TEAM_KICK = "/v1/team/kick";

	// Token: 0x0400214A RID: 8522
	public const string TEAM_CLAIM_KICK = "/v1/team/kick/claim";

	// Token: 0x0400214B RID: 8523
	public const string SEASON_CLAIM = "/v1/season/claim";

	// Token: 0x0400214C RID: 8524
	public const string PREVIOUS_SEASONS_GET = "/v1/season/previous";

	// Token: 0x0400214D RID: 8525
	public const string GIF_SAVE_LEVEL = "/v2/gif/save";

	// Token: 0x0400214E RID: 8526
	public const string GIF_SAVE = "/v1/gif/save";

	// Token: 0x0400214F RID: 8527
	public const string YOUTUBE_CHANNEL_LIST_GET = "/v1/youtube/getChannels";

	// Token: 0x04002150 RID: 8528
	public const string TEST_UPDATE_PLAYER = "/v1/test/player/update";

	// Token: 0x04002151 RID: 8529
	public const string EVENT_CLAIM = "/v2/event/claim";

	// Token: 0x04002152 RID: 8530
	public const string EVENT_FEED = "/v1/event/feed";

	// Token: 0x04002153 RID: 8531
	public const string TOURNAMENT_HOST_ROOMCHANGE = "/v1/tournament/ownerRoomChange";

	// Token: 0x04002154 RID: 8532
	public const string TOURNAMENT_JOIN = "/v1/tournament/join";

	// Token: 0x04002155 RID: 8533
	public const string TOURNAMENT_CLAIM = "/v1/tournament/claim";

	// Token: 0x04002156 RID: 8534
	public const string TOURNAMENT_CLAIM_YOUTUBENITROS = "/v1/tournament/claimYoutuberNitros";

	// Token: 0x04002157 RID: 8535
	public const string TOURNAMENT_GET_BOOSTER = "/v1/tournament/tournamentNitro";

	// Token: 0x04002158 RID: 8536
	public const string TOURNAMENT_SET_SUPER_FUEL = "/v1/tournament/setSuperFuel";

	// Token: 0x04002159 RID: 8537
	public const string TOURNAMENT_SEND_SCORE = "/v1/tournament/score/send";

	// Token: 0x0400215A RID: 8538
	public const string TOURNAMENT_GET_SCORE = "/v1/tournament/score/get";

	// Token: 0x0400215B RID: 8539
	public const string TOURNAMENT_GET_GHOSTS = "/v1/tournament/ghosts";

	// Token: 0x0400215C RID: 8540
	public const string TOURNAMENT_GET_GHOST = "/v1/tournament/ghost";

	// Token: 0x0400215D RID: 8541
	public const string TOURNAMENT_GET_LATEST_GHOSTS = "/v1/tournament/ghosts/latest";

	// Token: 0x0400215E RID: 8542
	public const string TOURNAMENT_SAVE_COMMENT = "/v1/tournament/comment/save";

	// Token: 0x0400215F RID: 8543
	public const string TOURNAMENT_GET_COMMENTS = "/v1/tournament/comment/get";

	// Token: 0x04002160 RID: 8544
	public const string GLOBAL_CHAT_GET_COMMENTS = "/v1/global/chat/find";

	// Token: 0x04002161 RID: 8545
	public const string GLOBAL_CHAT_SAVE_COMMENT = "/v1/global/chat/save";

	// Token: 0x04002162 RID: 8546
	public const string SPECIAL_OFFER_GET = "/v1/player/specialOffer";

	// Token: 0x04002163 RID: 8547
	public const string SPECIAL_OFFER_START = "/v1/player/specialOffer/start";

	// Token: 0x04002164 RID: 8548
	public const string SPECIAL_OFFER_CHEST_CLAIM = "/v1/player/specialOffer/claim";
}
