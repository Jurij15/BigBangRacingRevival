using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000173 RID: 371
public static class PsState
{
	// Token: 0x06000C74 RID: 3188 RVA: 0x00076035 File Offset: 0x00074435
	public static int GetVehicleIndex()
	{
		if (PsState.currentVehicleIndex == -1)
		{
			PsState.currentVehicleIndex = PlayerPrefsX.GetLastVehicle();
		}
		return PsState.currentVehicleIndex;
	}

	// Token: 0x06000C75 RID: 3189 RVA: 0x00076051 File Offset: 0x00074451
	public static void SetVehicleIndex(int _index)
	{
		PsState.currentVehicleIndex = _index;
		PlayerPrefsX.SetLastVehicle(PsState.currentVehicleIndex);
	}

	// Token: 0x06000C76 RID: 3190 RVA: 0x00076064 File Offset: 0x00074464
	public static Type GetCurrentVehicleType(bool _checkMinigameFirst = false)
	{
		if (_checkMinigameFirst)
		{
			if (PsState.m_activeMinigame != null && PsState.m_activeMinigame.m_playerUnit != null)
			{
				return PsState.m_activeMinigame.m_playerUnit.GetType();
			}
			if (PsState.m_activeGameLoop != null && !string.IsNullOrEmpty(PsState.m_activeGameLoop.m_minigameMetaData.playerUnit))
			{
				if (PsState.m_activeGameLoop.m_minigameMetaData.playerUnit == typeof(Motorcycle).ToString())
				{
					return typeof(Motorcycle);
				}
				if (PsState.m_activeGameLoop.m_minigameMetaData.playerUnit == typeof(OffroadCar).ToString())
				{
					return typeof(OffroadCar);
				}
			}
		}
		return PsState.m_vehicleTypes[PsState.GetVehicleIndex()];
	}

	// Token: 0x17000057 RID: 87
	// (get) Token: 0x06000C77 RID: 3191 RVA: 0x00076134 File Offset: 0x00074534
	public static int m_bigShardValue
	{
		get
		{
			if (PsMetagameManager.m_doubleValueGoodOrBadEvent != null && PsMetagameManager.m_doubleValueGoodOrBadEvent.timeToStart <= 0 && PsMetagameManager.m_doubleValueGoodOrBadEvent.timeLeft > 0)
			{
				return 50;
			}
			return 25;
		}
	}

	// Token: 0x17000058 RID: 88
	// (get) Token: 0x06000C78 RID: 3192 RVA: 0x00076165 File Offset: 0x00074565
	public static bool m_canAutoPause
	{
		get
		{
			return PsState.m_activeMinigame != null && PsState.m_activeMinigame.m_gameStarted && Main.m_currentGame.m_currentScene is GameScene;
		}
	}

	// Token: 0x06000C79 RID: 3193 RVA: 0x00076195 File Offset: 0x00074595
	public static bool UsingEditorResources()
	{
		return Main.m_currentGame.m_currentScene.m_name == "EditorScene" && !PsState.m_adminMode;
	}

	// Token: 0x06000C7A RID: 3194 RVA: 0x000761C0 File Offset: 0x000745C0
	public static void SetLanguage(Language _lang)
	{
		PsStrings.m_currentLanguage = (int)_lang;
	}

	// Token: 0x06000C7B RID: 3195 RVA: 0x000761C8 File Offset: 0x000745C8
	public static void SetDeathReasonByDamagetype(DamageType _damageType)
	{
		switch (_damageType)
		{
		case DamageType.Impact:
			PsState.m_lastDeathReason = DeathReason.NECKSNAP;
			return;
		case DamageType.Weapon:
			PsState.m_lastDeathReason = DeathReason.EXPLODED;
			return;
		case DamageType.Electric:
			PsState.m_lastDeathReason = DeathReason.ELECTRIFIED;
			return;
		case DamageType.BlackHole:
			PsState.m_lastDeathReason = DeathReason.BLACK_HOLE;
			return;
		}
	}

	// Token: 0x04000B86 RID: 2950
	public const bool DEV_BUILD = false;

	// Token: 0x04000B87 RID: 2951
	public const bool LOG_ALL = false;

	// Token: 0x04000B88 RID: 2952
	public const bool VIDEOCAPTURE = false;

	// Token: 0x04000B89 RID: 2953
	public static bool m_hideAllUICameras = false;

	// Token: 0x04000B8A RID: 2954
	public const string SERVER_BASE = "https://woeprod.traplightgames.com";

	// Token: 0x04000B8B RID: 2955
	public const string BUILD_SERVER = "PRODUCTION";

	// Token: 0x04000B8C RID: 2956
	public const string STORE_ID = "com.traplight.bigbangracing";

	// Token: 0x04000B8D RID: 2957
	public const string STORE_URL = "market://details?id=com.traplight.bigbangracing";

	// Token: 0x04000B8E RID: 2958
	public const bool PRODUCTION_BUILD = true;

	// Token: 0x04000B8F RID: 2959
	public const string FlurryIOSApiKey = "WQWM93PDWY44322VD3K8";

	// Token: 0x04000B90 RID: 2960
	public const string FlurryAndroidApiKey = "2XG43WSQHR5C77VCXMJQ";

	// Token: 0x04000B91 RID: 2961
	public const string UNITY_ADS_GAMEID = "96752";

	// Token: 0x04000B92 RID: 2962
	public const string AdjustToken = "m4ck5yjbw7pc";

	// Token: 0x04000B93 RID: 2963
	public const bool LOCAL_INITIAL_DATA = false;

	// Token: 0x04000B94 RID: 2964
	public static PerformanceClass m_performanceClass = ToolBox.GetDevicePerformanceClass();

	// Token: 0x04000B95 RID: 2965
	public static bool m_muteSoundFX = PlayerPrefsX.GetMuteSoundFX();

	// Token: 0x04000B96 RID: 2966
	public static bool m_muteMusic = PlayerPrefsX.GetMuteMusic();

	// Token: 0x04000B97 RID: 2967
	public static bool m_perfMode = PlayerPrefsX.GetPerfMode();

	// Token: 0x04000B98 RID: 2968
	public static bool m_notifications = PlayerPrefsX.GetAcceptNotifications();

	// Token: 0x04000B99 RID: 2969
	public static bool m_streamerHUD = PlayerPrefsX.GetStreamerHud();

	// Token: 0x04000B9A RID: 2970
	public static bool m_chestWarnings = PlayerPrefsX.GetChestWarnings();

	// Token: 0x04000B9B RID: 2971
	public static string m_sessionId = string.Empty;

	// Token: 0x04000B9C RID: 2972
	public static bool m_adminMode = false;

	// Token: 0x04000B9D RID: 2973
	public static float m_globalOverrideCC = -1f;

	// Token: 0x04000B9E RID: 2974
	public static bool m_adminModeSkipping = false;

	// Token: 0x04000B9F RID: 2975
	public static bool m_enableAutodecos = false;

	// Token: 0x04000BA0 RID: 2976
	public static bool m_adWatched = false;

	// Token: 0x04000BA1 RID: 2977
	public static bool m_inIapFlow = false;

	// Token: 0x04000BA2 RID: 2978
	public static bool m_inLoginFlow = false;

	// Token: 0x04000BA3 RID: 2979
	public static bool m_inStartupSequence = true;

	// Token: 0x04000BA4 RID: 2980
	public static bool m_restarting = false;

	// Token: 0x04000BA5 RID: 2981
	public static bool m_gemShopEnabled = false;

	// Token: 0x04000BA6 RID: 2982
	public static int m_previousLoginVersion;

	// Token: 0x04000BA7 RID: 2983
	public static Type[] m_vehicleTypes = new Type[]
	{
		typeof(OffroadCar),
		typeof(Motorcycle)
	};

	// Token: 0x04000BA8 RID: 2984
	private static int currentVehicleIndex = -1;

	// Token: 0x04000BA9 RID: 2985
	public const int m_starterPackPopupLevelNumber = 10;

	// Token: 0x04000BAA RID: 2986
	public const int m_timedSpecialOffersStartLevelNumber = 12;

	// Token: 0x04000BAB RID: 2987
	public static int m_versusChallengeTryAmount = 10;

	// Token: 0x04000BAC RID: 2988
	public static int m_versusTokenMaxCount = 10;

	// Token: 0x04000BAD RID: 2989
	public static int m_versusRankCap = 25;

	// Token: 0x04000BAE RID: 2990
	public static string m_fixedFreshMinigameId = "567011c8340000ce0286aca1";

	// Token: 0x04000BAF RID: 2991
	public static string m_fixedFreshMcId = "56fe40aa3200001806a679c9";

	// Token: 0x04000BB0 RID: 2992
	public static float m_rentPriceMultiplier = 0.5f;

	// Token: 0x04000BB1 RID: 2993
	public static int m_minimumRentPrice = 5;

	// Token: 0x04000BB2 RID: 2994
	public static string[] m_newEditorItems;

	// Token: 0x04000BB3 RID: 2995
	public static int m_newEditorItemCount = 0;

	// Token: 0x04000BB4 RID: 2996
	public static int m_superLikeVisualMultiplier = 10;

	// Token: 0x04000BB5 RID: 2997
	public static bool m_unlockEditorItems = true;

	// Token: 0x04000BB6 RID: 2998
	public static int m_coinValueBoosterSecs = 180;

	// Token: 0x04000BB7 RID: 2999
	public const int m_teamJoinReward = 50;

	// Token: 0x04000BB8 RID: 3000
	private const int bigShardValue = 25;

	// Token: 0x04000BB9 RID: 3001
	public static bool m_levelLeaderboardsEnabled = false;

	// Token: 0x04000BBA RID: 3002
	public static bool m_languageSelectEnabled = true;

	// Token: 0x04000BBB RID: 3003
	public const string GTAG_AUTODESTROY = "GTAG_AUTODESTROY";

	// Token: 0x04000BBC RID: 3004
	public const string GTAG_INGAME_COIN = "GTAG_COIN";

	// Token: 0x04000BBD RID: 3005
	public const string GTAG_INGAME_DEBRIS = "GTAG_DEBRIS";

	// Token: 0x04000BBE RID: 3006
	public const string GTAG_UNIT = "GTAG_UNIT";

	// Token: 0x04000BBF RID: 3007
	public const int SCREENSHOT_SIZE = 512;

	// Token: 0x04000BC0 RID: 3008
	public const float ZBUFFER_FIGHT_BIAS = 0.5f;

	// Token: 0x04000BC1 RID: 3009
	public const int MAX_TRACK_NAME_CHARACTERS = 32;

	// Token: 0x04000BC2 RID: 3010
	public const int GHOST_RECORD_FRAMESKIP = 5;

	// Token: 0x04000BC3 RID: 3011
	public const int GHOST_MAX_KEYFRAMES = 7200;

	// Token: 0x04000BC4 RID: 3012
	public const int UI_SPRITE_REFERENCE_SCALE = 1536;

	// Token: 0x04000BC5 RID: 3013
	public const float EDITOR_MAX_ZOOM_LEVEL = 1500f;

	// Token: 0x04000BC6 RID: 3014
	public const float EDITOR_MIN_ZOOM_LEVEL = 200f;

	// Token: 0x04000BC7 RID: 3015
	public static SpriteSheet m_uiSheet;

	// Token: 0x04000BC8 RID: 3016
	public static Vector2[] m_domeSizes = new Vector2[]
	{
		new Vector2(3f, 1f),
		new Vector2(6f, 2f),
		new Vector2(9f, 2f)
	};

	// Token: 0x04000BC9 RID: 3017
	public static Vector2 m_editorAreaSize = PsState.m_domeSizes[2];

	// Token: 0x04000BCA RID: 3018
	public static bool m_editorIsLefty = PlayerPrefsX.GetLefty();

	// Token: 0x04000BCB RID: 3019
	public static bool m_everyplayEnabled = PlayerPrefsX.GetEveryplayEnabled();

	// Token: 0x04000BCC RID: 3020
	public static float m_drawButtonWindowPosition;

	// Token: 0x04000BCD RID: 3021
	public static float m_drawMenuAlign;

	// Token: 0x04000BCE RID: 3022
	public static float m_objectMenuButtonAlign;

	// Token: 0x04000BCF RID: 3023
	public static CameraTargetC m_editorCamTarget;

	// Token: 0x04000BD0 RID: 3024
	public static Vector3 m_editorCameraPos;

	// Token: 0x04000BD1 RID: 3025
	public static float m_editorCameraZoom;

	// Token: 0x04000BD2 RID: 3026
	public static cpBB m_editorCameraExtraBorder;

	// Token: 0x04000BD3 RID: 3027
	public static EditorTool m_currentTool;

	// Token: 0x04000BD4 RID: 3028
	public static string m_editorLastSelectedItemCategory;

	// Token: 0x04000BD5 RID: 3029
	public static string m_freeWizardLastSelectedItemCategory;

	// Token: 0x04000BD6 RID: 3030
	public static bool m_cameraManTakeShot;

	// Token: 0x04000BD7 RID: 3031
	public static string m_wizardPlayer;

	// Token: 0x04000BD8 RID: 3032
	public static string m_wizardGameMode;

	// Token: 0x04000BD9 RID: 3033
	public static int m_wizardDomeIndex;

	// Token: 0x04000BDA RID: 3034
	public static int m_currentBrush = 0;

	// Token: 0x04000BDB RID: 3035
	public static int m_drawLayer;

	// Token: 0x04000BDC RID: 3036
	public static int m_drawMenuTargetPage;

	// Token: 0x04000BDD RID: 3037
	public static bool m_addDown;

	// Token: 0x04000BDE RID: 3038
	public static bool m_subDown;

	// Token: 0x04000BDF RID: 3039
	public static bool m_specialDown;

	// Token: 0x04000BE0 RID: 3040
	public static PsGameLoop m_activeGameLoop;

	// Token: 0x04000BE1 RID: 3041
	public static Minigame m_activeMinigame;

	// Token: 0x04000BE2 RID: 3042
	public static bool m_physicsPaused;

	// Token: 0x04000BE3 RID: 3043
	public static List<UIComponent> m_openPopups = new List<UIComponent>();

	// Token: 0x04000BE4 RID: 3044
	public static List<UI3DRenderTextureCanvas> m_activeRenderTextures = new List<UI3DRenderTextureCanvas>();

	// Token: 0x04000BE5 RID: 3045
	public static List<PsDialogueFlow> m_dialogues = new List<PsDialogueFlow>();

	// Token: 0x04000BE6 RID: 3046
	public static bool m_UIComponentsHidden = false;

	// Token: 0x04000BE7 RID: 3047
	public static PsUIBasePopup m_openSocialPopup = null;

	// Token: 0x04000BE8 RID: 3048
	public static bool m_showMcDialogue = false;

	// Token: 0x04000BE9 RID: 3049
	public static bool m_showStarterPackPopup = false;

	// Token: 0x04000BEA RID: 3050
	public static Vector2 m_defaultGravity = new Vector2(0f, -550f);

	// Token: 0x04000BEB RID: 3051
	public const uint CP_LAYER_DEBRIS = 1U;

	// Token: 0x04000BEC RID: 3052
	public const uint CP_LAYER_GROUND = 17U;

	// Token: 0x04000BED RID: 3053
	public const uint CP_LAYER_STATIC = 257U;

	// Token: 0x04000BEE RID: 3054
	public const uint CP_LAYER_ALL_BUT_NO_STATICS = 1118208U;

	// Token: 0x04000BEF RID: 3055
	public const uint CP_LAYER_DEFAULT = 17895696U;

	// Token: 0x04000BF0 RID: 3056
	public const uint CP_LAYER_TRIGGER = 16777216U;

	// Token: 0x04000BF1 RID: 3057
	public const uint CP_LAYER_AUTODECOS = 268435456U;

	// Token: 0x04000BF2 RID: 3058
	public static int m_debugCopperCoinsCollected;

	// Token: 0x04000BF3 RID: 3059
	public static int m_debugCopperValue;

	// Token: 0x04000BF4 RID: 3060
	public static float m_debugCoinsTime;

	// Token: 0x04000BF5 RID: 3061
	public static DeathReason m_lastDeathReason;
}
