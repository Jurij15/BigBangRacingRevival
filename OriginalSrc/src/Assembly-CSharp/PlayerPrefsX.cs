using System;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using MiniJSON;
using Server.Utility;
using UnityEngine;

// Token: 0x02000154 RID: 340
public class PlayerPrefsX
{
	// Token: 0x06000BB6 RID: 2998 RVA: 0x00074197 File Offset: 0x00072597
	public static void DeleteKey(string key)
	{
		ObscuredPrefs.DeleteKey(key);
		ObscuredPrefs.Save();
	}

	// Token: 0x06000BB7 RID: 2999 RVA: 0x000741A4 File Offset: 0x000725A4
	public static void DeleteAll()
	{
		ObscuredPrefs.DeleteAll();
		ObscuredPrefs.Save();
	}

	// Token: 0x06000BB8 RID: 3000 RVA: 0x000741B0 File Offset: 0x000725B0
	public static void SetNameChangesCount(int _count)
	{
		PlayerPrefsX.SaveInt("nameChangesDone", _count);
	}

	// Token: 0x06000BB9 RID: 3001 RVA: 0x000741BD File Offset: 0x000725BD
	public static int GetNameChangesCount()
	{
		return PlayerPrefsX.GetInt("nameChangesDone");
	}

	// Token: 0x06000BBA RID: 3002 RVA: 0x000741C9 File Offset: 0x000725C9
	public static void SetLosingStreak(int _count)
	{
		PlayerPrefsX.SaveInt("losingStreak", _count);
	}

	// Token: 0x06000BBB RID: 3003 RVA: 0x000741D6 File Offset: 0x000725D6
	public static int GetLosingStreak()
	{
		return PlayerPrefsX.GetInt("losingStreak");
	}

	// Token: 0x06000BBC RID: 3004 RVA: 0x000741E2 File Offset: 0x000725E2
	public static void SetLastUpgradeRemindTime(int _epochTime)
	{
		PlayerPrefsX.SaveInt("upgradeRemindEpoch", _epochTime);
	}

	// Token: 0x06000BBD RID: 3005 RVA: 0x000741EF File Offset: 0x000725EF
	public static int GetLastUpgradeRemindTime()
	{
		return PlayerPrefsX.GetInt("upgradeRemindEpoch");
	}

	// Token: 0x06000BBE RID: 3006 RVA: 0x000741FB File Offset: 0x000725FB
	public static void SetRacingUpgradeRemindCount(int _count)
	{
		PlayerPrefsX.SaveInt("upgradeRemindCount", _count);
	}

	// Token: 0x06000BBF RID: 3007 RVA: 0x00074208 File Offset: 0x00072608
	public static int GetRacingUpgradeRemindCount()
	{
		return PlayerPrefsX.GetInt("upgradeRemindCount");
	}

	// Token: 0x06000BC0 RID: 3008 RVA: 0x00074214 File Offset: 0x00072614
	public static void SetTournamentChatTimestamp(int _timestamp)
	{
		PlayerPrefsX.SaveInt("tournamentChatLastTimestamp", _timestamp);
	}

	// Token: 0x06000BC1 RID: 3009 RVA: 0x00074221 File Offset: 0x00072621
	public static int GetTournamentChatTimestamp()
	{
		return PlayerPrefsX.GetInt("tournamentChatLastTimestamp");
	}

	// Token: 0x06000BC2 RID: 3010 RVA: 0x0007422D File Offset: 0x0007262D
	public static bool GetGifDeathRecording()
	{
		return PlayerPrefsX.GetBool("GifDeathRecord", false);
	}

	// Token: 0x06000BC3 RID: 3011 RVA: 0x0007423A File Offset: 0x0007263A
	public static void SetGifDeathRecording(bool _value)
	{
		PlayerPrefsX.SetBool("GifDeathRecord", _value);
	}

	// Token: 0x06000BC4 RID: 3012 RVA: 0x00074248 File Offset: 0x00072648
	public static void AdUsedForLevel(string _planet, int _nodeId)
	{
		Dictionary<string, object> dictionary = PlayerPrefsX.AdUsedOnPlanetLevel();
		if (dictionary == null)
		{
			dictionary = new Dictionary<string, object>();
		}
		dictionary[_planet] = _nodeId;
		ObscuredPrefs.SetString("adUsedOnLevel", Json.Serialize(dictionary));
	}

	// Token: 0x06000BC5 RID: 3013 RVA: 0x00074284 File Offset: 0x00072684
	public static int AdUsedLastLevel(string _planet)
	{
		int num = 0;
		Dictionary<string, object> dictionary = PlayerPrefsX.AdUsedOnPlanetLevel();
		if (dictionary != null && dictionary.ContainsKey(_planet))
		{
			num = Convert.ToInt32(dictionary[_planet]);
		}
		return num;
	}

	// Token: 0x06000BC6 RID: 3014 RVA: 0x000742BC File Offset: 0x000726BC
	private static Dictionary<string, object> AdUsedOnPlanetLevel()
	{
		Dictionary<string, object> dictionary = null;
		if (!string.IsNullOrEmpty(PlayerPrefsX.GetString("adUsedOnLevel")))
		{
			string @string = PlayerPrefsX.GetString("adUsedOnLevel");
			dictionary = Json.Deserialize(@string) as Dictionary<string, object>;
			if (dictionary == null)
			{
				Debug.Log("roulette data is null", null);
			}
		}
		return dictionary;
	}

	// Token: 0x06000BC7 RID: 3015 RVA: 0x00074308 File Offset: 0x00072708
	public static Dictionary<string, object> GetLocalPlanetVersions()
	{
		Dictionary<string, object> dictionary = null;
		if (!string.IsNullOrEmpty(PlayerPrefsX.GetString("planets")))
		{
			string @string = PlayerPrefsX.GetString("planets");
			dictionary = Json.Deserialize(@string) as Dictionary<string, object>;
			if (dictionary == null)
			{
				Debug.LogError("planets is null wtf");
			}
		}
		return dictionary;
	}

	// Token: 0x06000BC8 RID: 3016 RVA: 0x00074354 File Offset: 0x00072754
	public static void SetAllLocalPlanetVersions(Dictionary<string, object> _planets)
	{
		string text = Json.Serialize(_planets);
		ObscuredPrefs.SetString("planets", Json.Serialize(_planets));
	}

	// Token: 0x06000BC9 RID: 3017 RVA: 0x00074378 File Offset: 0x00072778
	public static void SetLocalPlanetVersion(string _planetIdentifier, int _version)
	{
		Dictionary<string, object> dictionary = PlayerPrefsX.GetLocalPlanetVersions();
		if (dictionary == null)
		{
			dictionary = new Dictionary<string, object>();
		}
		dictionary[_planetIdentifier] = _version;
		PlayerPrefsX.SetAllLocalPlanetVersions(dictionary);
	}

	// Token: 0x06000BCA RID: 3018 RVA: 0x000743AA File Offset: 0x000727AA
	public static void SetMinimumTournamentsNitro(int _nitroCount)
	{
		PlayerPrefsX.SaveInt("minTournamentNitros", _nitroCount);
	}

	// Token: 0x06000BCB RID: 3019 RVA: 0x000743B7 File Offset: 0x000727B7
	public static int GetMinimumTournamentsNitro()
	{
		return PlayerPrefsX.GetInt("minTournamentNitros");
	}

	// Token: 0x06000BCC RID: 3020 RVA: 0x000743C3 File Offset: 0x000727C3
	public static int GetTournamentYoutuberNitroCount()
	{
		return PlayerPrefsX.GetInt("tournamentYoutuberFollowNitros");
	}

	// Token: 0x06000BCD RID: 3021 RVA: 0x000743CF File Offset: 0x000727CF
	public static void SetLanguage(Language _lang)
	{
		PlayerPrefsX.SaveString("Language", _lang.ToString());
	}

	// Token: 0x06000BCE RID: 3022 RVA: 0x000743E8 File Offset: 0x000727E8
	public static Language GetLanguage()
	{
		string @string = PlayerPrefsX.GetString("Language");
		if (@string != null)
		{
			return (Language)Enum.Parse(typeof(Language), @string);
		}
		SystemLanguage systemLanguage = Application.systemLanguage;
		switch (systemLanguage)
		{
		case 13:
			return Language.Fi;
		case 14:
			return Language.Fr;
		case 15:
			return Language.De;
		default:
			switch (systemLanguage)
			{
			case 28:
				return Language.PtBr;
			default:
				if (systemLanguage == 21)
				{
					return Language.It;
				}
				if (systemLanguage != 34)
				{
					return Language.En;
				}
				return Language.Es;
			case 30:
				return Language.Ru;
			}
			break;
		}
	}

	// Token: 0x06000BCF RID: 3023 RVA: 0x00074471 File Offset: 0x00072871
	public static void SetShopNotification(bool _value)
	{
		PlayerPrefsX.SetBool("ShopNotification", _value);
	}

	// Token: 0x06000BD0 RID: 3024 RVA: 0x0007447E File Offset: 0x0007287E
	public static bool GetShopNotification()
	{
		return ObscuredPrefs.GetBool("ShopNotification", false);
	}

	// Token: 0x06000BD1 RID: 3025 RVA: 0x0007448B File Offset: 0x0007288B
	public static int GetSuperLikeInterval()
	{
		return ObscuredPrefs.GetInt("SuperLikeInterval");
	}

	// Token: 0x06000BD2 RID: 3026 RVA: 0x00074497 File Offset: 0x00072897
	public static void SetCountryCode(string _countryCode)
	{
		ObscuredPrefs.SetString("country", _countryCode);
	}

	// Token: 0x06000BD3 RID: 3027 RVA: 0x000744A4 File Offset: 0x000728A4
	public static string GetCountryCode()
	{
		return PlayerPrefsX.GetString("country");
	}

	// Token: 0x06000BD4 RID: 3028 RVA: 0x000744B0 File Offset: 0x000728B0
	public static void SetEditorUseFlag(bool _state = true)
	{
		ObscuredPrefs.SetBool("playerUsedEditor", _state);
	}

	// Token: 0x06000BD5 RID: 3029 RVA: 0x000744BD File Offset: 0x000728BD
	public static bool GetEditorUseFlag()
	{
		return ObscuredPrefs.GetBool("playerUsedEditor", false);
	}

	// Token: 0x06000BD6 RID: 3030 RVA: 0x000744CA File Offset: 0x000728CA
	public static void SetPublishFlag(bool _state = true)
	{
		ObscuredPrefs.SetBool("playerPublishedLevel", _state);
	}

	// Token: 0x06000BD7 RID: 3031 RVA: 0x000744D7 File Offset: 0x000728D7
	public static bool GetPublishFlag()
	{
		return ObscuredPrefs.GetBool("playerPublishedLevel", false);
	}

	// Token: 0x06000BD8 RID: 3032 RVA: 0x000744E4 File Offset: 0x000728E4
	public static void SetInitialUserPropertiesSent(bool _state)
	{
		ObscuredPrefs.SetBool("initialUserPropertiesSent", _state);
	}

	// Token: 0x06000BD9 RID: 3033 RVA: 0x000744F1 File Offset: 0x000728F1
	public static bool GetInitialUserPropertiesSent()
	{
		return ObscuredPrefs.GetBool("initialUserPropertiesSent", false);
	}

	// Token: 0x06000BDA RID: 3034 RVA: 0x000744FE File Offset: 0x000728FE
	public static void SetInitialUserPropertiesTime(long _time)
	{
		ObscuredPrefs.SetLong("initialUserPropertiesTime", _time);
	}

	// Token: 0x06000BDB RID: 3035 RVA: 0x0007450B File Offset: 0x0007290B
	public static long GetInitialUserPropertiesTime()
	{
		return ObscuredPrefs.GetLong("initialUserPropertiesTime", 0L);
	}

	// Token: 0x06000BDC RID: 3036 RVA: 0x00074519 File Offset: 0x00072919
	public static string GetLaunchUrl()
	{
		return PlayerPrefs.GetString("LaunchUrl");
	}

	// Token: 0x06000BDD RID: 3037 RVA: 0x00074525 File Offset: 0x00072925
	public static void SetLaunchUrl(string _launchUrl)
	{
		PlayerPrefs.SetString("LaunchUrl", _launchUrl);
	}

	// Token: 0x06000BDE RID: 3038 RVA: 0x00074532 File Offset: 0x00072932
	public static void SetFirstLogin(bool _state)
	{
		ObscuredPrefs.SetBool("firstLogin", _state);
	}

	// Token: 0x06000BDF RID: 3039 RVA: 0x0007453F File Offset: 0x0007293F
	public static bool GetFirstLogin()
	{
		return ObscuredPrefs.GetBool("firstLogin", true);
	}

	// Token: 0x06000BE0 RID: 3040 RVA: 0x0007454C File Offset: 0x0007294C
	public static void SetFirstSeen(long _epochSeconds)
	{
		PlayerPrefsX.SaveInt("firstSeen", (int)(_epochSeconds / 1000L));
	}

	// Token: 0x06000BE1 RID: 3041 RVA: 0x00074561 File Offset: 0x00072961
	public static int GetFirstSeenEpochSeconds()
	{
		return PlayerPrefsX.GetInt("firstSeen");
	}

	// Token: 0x06000BE2 RID: 3042 RVA: 0x00074570 File Offset: 0x00072970
	public static DateTime GetFirstSeenDateTime()
	{
		DateTime dateTime;
		dateTime..ctor(1970, 1, 1, 0, 0, 0, 1);
		return dateTime.AddSeconds((double)PlayerPrefsX.GetInt("firstSeen"));
	}

	// Token: 0x06000BE3 RID: 3043 RVA: 0x000745A3 File Offset: 0x000729A3
	public static void SetSession(string _sessionId)
	{
		ObscuredPrefs.SetString("sessionId", _sessionId);
	}

	// Token: 0x06000BE4 RID: 3044 RVA: 0x000745B0 File Offset: 0x000729B0
	public static string GetSession()
	{
		string @string = ObscuredPrefs.GetString("sessionId");
		if (@string != null && @string != string.Empty)
		{
			return @string;
		}
		return "new";
	}

	// Token: 0x06000BE5 RID: 3045 RVA: 0x000745E8 File Offset: 0x000729E8
	public static void SetClientConfig(ClientConfig _conf)
	{
		if (_conf == null)
		{
			_conf = new ClientConfig();
		}
		ObscuredPrefs.SetInt("SuperLikeInterval", _conf.superLikeRefreshMinutes);
		ObscuredPrefs.SetInt("carRentRefresh", _conf.carRefreshMinutes);
		ObscuredPrefs.SetInt("freshFreeInterval", _conf.freshFreeInterval);
		ObscuredPrefs.SetInt("startBolts", _conf.boltsAtStart);
		ObscuredPrefs.SetInt("startCoins", _conf.coinsAtStart);
		ObscuredPrefs.SetInt("startDiamonds", _conf.diamondsAtStart);
		ObscuredPrefs.SetInt("startKeys", _conf.keysAtStart);
		ObscuredPrefs.SetInt("fbConnectRewardAmount", _conf.fbConnectReward);
		ObscuredPrefs.SetInt("dailyGemMultiplier", _conf.dailyGemAmount);
		ObscuredPrefs.SetInt("freshFreeStartCoolDown", _conf.freshFreeCoolDown);
		ObscuredPrefs.SetInt("freshFreeStartCount", _conf.freshFreeCount);
		ObscuredPrefs.SetInt("videoAdStartTimeOut", _conf.videoAdCoolDown);
		ObscuredPrefs.SetInt("videoAdStartCount", _conf.videoAdCount);
		PlayerPrefsX.SetMinimumTournamentsNitro(_conf.minimumTournamentNitros);
		ObscuredPrefs.SetInt("tournamentYoutuberFollowNitros", _conf.tournamentYoutuberFollowNitros);
		ObscuredPrefs.SetInt("creatorRank1", _conf.creatorRank1);
		ObscuredPrefs.SetInt("creatorRank2", _conf.creatorRank2);
		ObscuredPrefs.SetInt("creatorRank3", _conf.creatorRank3);
		ObscuredPrefs.SetInt("creatorRank4", _conf.creatorRank4);
		ObscuredPrefs.SetInt("creatorRank5", _conf.creatorRank5);
		ObscuredPrefs.SetInt("creatorRank6", _conf.creatorRank6);
		ObscuredPrefs.Save();
	}

	// Token: 0x06000BE6 RID: 3046 RVA: 0x00074754 File Offset: 0x00072B54
	public static ClientConfig GetClientConfig()
	{
		return new ClientConfig
		{
			carRefreshMinutes = ObscuredPrefs.GetInt("carRentRefresh"),
			freshFreeInterval = ObscuredPrefs.GetInt("freshFreeInterval"),
			boltsAtStart = ObscuredPrefs.GetInt("startBolts"),
			coinsAtStart = ObscuredPrefs.GetInt("startCoins"),
			diamondsAtStart = ObscuredPrefs.GetInt("startDiamonds"),
			keysAtStart = ObscuredPrefs.GetInt("startKeys"),
			fbConnectReward = ObscuredPrefs.GetInt("fbConnectRewardAmount"),
			creatorRank1 = ObscuredPrefs.GetInt("creatorRank1"),
			creatorRank2 = ObscuredPrefs.GetInt("creatorRank2"),
			creatorRank3 = ObscuredPrefs.GetInt("creatorRank3"),
			creatorRank4 = ObscuredPrefs.GetInt("creatorRank4"),
			creatorRank5 = ObscuredPrefs.GetInt("creatorRank5"),
			creatorRank6 = ObscuredPrefs.GetInt("creatorRank6")
		};
	}

	// Token: 0x06000BE7 RID: 3047 RVA: 0x00074838 File Offset: 0x00072C38
	public static int GetCreatorRank(int _likes)
	{
		if (_likes < ObscuredPrefs.GetInt("creatorRank1"))
		{
			return 0;
		}
		if (_likes < ObscuredPrefs.GetInt("creatorRank2"))
		{
			return 1;
		}
		if (_likes < ObscuredPrefs.GetInt("creatorRank3"))
		{
			return 2;
		}
		if (_likes < ObscuredPrefs.GetInt("creatorRank4"))
		{
			return 3;
		}
		if (_likes < ObscuredPrefs.GetInt("creatorRank5"))
		{
			return 4;
		}
		if (_likes >= ObscuredPrefs.GetInt("creatorRank6"))
		{
			return 6;
		}
		return 5;
	}

	// Token: 0x06000BE8 RID: 3048 RVA: 0x000748B4 File Offset: 0x00072CB4
	public static void SetPlayerData(PlayerData _data)
	{
		ObscuredPrefs.SetString("UserID", _data.playerId);
		ObscuredPrefs.SetString("UserName", _data.name);
		ObscuredPrefs.SetString("UserTag", _data.tag);
		ObscuredPrefs.SetInt("itemDb", _data.itemDbVersion);
		ObscuredPrefs.SetInt("AcceptNotifications", (!_data.acceptNotifications) ? 0 : 1);
		PlayerPrefsX.SetNameChangesCount(_data.nameChangesDone);
		ObscuredPrefs.Save();
	}

	// Token: 0x06000BE9 RID: 3049 RVA: 0x00074933 File Offset: 0x00072D33
	public static void SetLastLoginTime(string _value)
	{
		PlayerPrefsX.SaveString("LastLogin", _value);
	}

	// Token: 0x06000BEA RID: 3050 RVA: 0x00074940 File Offset: 0x00072D40
	public static void SetGameCenterName(string _value)
	{
		PlayerPrefsX.SaveString("GameCenterName", _value);
	}

	// Token: 0x06000BEB RID: 3051 RVA: 0x0007494D File Offset: 0x00072D4D
	public static void SetFacebookName(string _value)
	{
		PlayerPrefsX.SaveString("FacebookName", _value);
	}

	// Token: 0x06000BEC RID: 3052 RVA: 0x0007495A File Offset: 0x00072D5A
	public static string GetFacebookName()
	{
		return PlayerPrefsX.GetString("FacebookName");
	}

	// Token: 0x06000BED RID: 3053 RVA: 0x00074966 File Offset: 0x00072D66
	public static void SetAcceptNotifications(bool _value)
	{
		PlayerPrefsX.SetBool("AcceptNotifications", _value);
	}

	// Token: 0x06000BEE RID: 3054 RVA: 0x00074973 File Offset: 0x00072D73
	public static bool GetAcceptNotifications()
	{
		return PlayerPrefsX.GetBool("AcceptNotifications", true);
	}

	// Token: 0x06000BEF RID: 3055 RVA: 0x00074980 File Offset: 0x00072D80
	public static void SetStreamerHud(bool _value)
	{
		PlayerPrefsX.SetBool("StreamerHud", _value);
	}

	// Token: 0x06000BF0 RID: 3056 RVA: 0x0007498D File Offset: 0x00072D8D
	public static bool GetStreamerHud()
	{
		return PlayerPrefsX.GetBool("StreamerHud");
	}

	// Token: 0x06000BF1 RID: 3057 RVA: 0x00074999 File Offset: 0x00072D99
	public static void SetFacebookId(string _facebookId)
	{
		PlayerPrefsX.SaveString("FacebookId", _facebookId);
	}

	// Token: 0x06000BF2 RID: 3058 RVA: 0x000749A6 File Offset: 0x00072DA6
	public static void SetGameCenterId(string _gameCenterId)
	{
		PlayerPrefsX.SaveString("GameCenterId", _gameCenterId);
	}

	// Token: 0x06000BF3 RID: 3059 RVA: 0x000749B3 File Offset: 0x00072DB3
	public static void SetUserName(string _userName)
	{
		PlayerPrefsX.SaveString("UserName", _userName);
	}

	// Token: 0x06000BF4 RID: 3060 RVA: 0x000749C0 File Offset: 0x00072DC0
	public static void SetYoutubeName(string _ytName)
	{
		if (_ytName == null)
		{
			_ytName = string.Empty;
		}
		PlayerPrefsX.SaveString("youtubeName", _ytName);
	}

	// Token: 0x06000BF5 RID: 3061 RVA: 0x000749DA File Offset: 0x00072DDA
	public static void SetYoutubeId(string _ytId)
	{
		if (_ytId == null)
		{
			_ytId = string.Empty;
		}
		PlayerPrefsX.SaveString("youtubeId", _ytId);
	}

	// Token: 0x06000BF6 RID: 3062 RVA: 0x000749F4 File Offset: 0x00072DF4
	public static void SetUserTag(string _userTag)
	{
		PlayerPrefsX.SaveString("UserTag", _userTag);
	}

	// Token: 0x06000BF7 RID: 3063 RVA: 0x00074A01 File Offset: 0x00072E01
	public static void SetUserId(string _userId)
	{
		PlayerPrefsX.SaveString("UserID", _userId);
	}

	// Token: 0x06000BF8 RID: 3064 RVA: 0x00074A0E File Offset: 0x00072E0E
	public static void SetMuteSoundFX(bool _mute)
	{
		PlayerPrefsX.SetBool("soundMute", _mute);
	}

	// Token: 0x06000BF9 RID: 3065 RVA: 0x00074A1B File Offset: 0x00072E1B
	public static void SetMuteMusic(bool _mute)
	{
		PlayerPrefsX.SetBool("soundMuteMusic", _mute);
	}

	// Token: 0x06000BFA RID: 3066 RVA: 0x00074A28 File Offset: 0x00072E28
	public static void SetLefty(bool _lefty)
	{
		PlayerPrefsX.SetBool("Lefty", _lefty);
	}

	// Token: 0x06000BFB RID: 3067 RVA: 0x00074A35 File Offset: 0x00072E35
	public static void SetPerfMode(bool _on)
	{
		PlayerPrefsX.SetBool("PerfMode", _on);
	}

	// Token: 0x06000BFC RID: 3068 RVA: 0x00074A42 File Offset: 0x00072E42
	public static void SetEveryplayEnabled(bool _enabled)
	{
		PlayerPrefsX.SetBool("EveryplayEnabled", _enabled);
	}

	// Token: 0x06000BFD RID: 3069 RVA: 0x00074A4F File Offset: 0x00072E4F
	public static void SetItemDbVersion(int _version)
	{
		PlayerPrefsX.SaveInt("itemDb", _version);
	}

	// Token: 0x06000BFE RID: 3070 RVA: 0x00074A5C File Offset: 0x00072E5C
	public static void SetPathVersion(int _version)
	{
		PlayerPrefsX.SaveInt("pathVersion", _version);
	}

	// Token: 0x06000BFF RID: 3071 RVA: 0x00074A69 File Offset: 0x00072E69
	public static int GetDailyGemMultiplier()
	{
		return PlayerPrefsX.GetInt("dailyGemMultiplier");
	}

	// Token: 0x06000C00 RID: 3072 RVA: 0x00074A75 File Offset: 0x00072E75
	public static int GetVideoAdCount()
	{
		if (ObscuredPrefs.HasKey("videoAdCount"))
		{
			return ObscuredPrefs.GetInt("videoAdCount");
		}
		return -1;
	}

	// Token: 0x06000C01 RID: 3073 RVA: 0x00074A92 File Offset: 0x00072E92
	public static int GetVideoAdStartCount()
	{
		return PlayerPrefsX.GetInt("videoAdStartCount");
	}

	// Token: 0x06000C02 RID: 3074 RVA: 0x00074A9E File Offset: 0x00072E9E
	public static void SetVideoAdCount(int _count)
	{
		PlayerPrefsX.SaveInt("videoAdCount", _count);
	}

	// Token: 0x06000C03 RID: 3075 RVA: 0x00074AAB File Offset: 0x00072EAB
	public static string GetVideoAdTimeOut()
	{
		return PlayerPrefsX.GetString("videoAdTimeOut");
	}

	// Token: 0x06000C04 RID: 3076 RVA: 0x00074AB7 File Offset: 0x00072EB7
	public static int GetVideoAdStartTimeOut()
	{
		return PlayerPrefsX.GetInt("videoAdStartTimeOut");
	}

	// Token: 0x06000C05 RID: 3077 RVA: 0x00074AC3 File Offset: 0x00072EC3
	public static void SetVideoAdTimeOut(string _date)
	{
		PlayerPrefsX.SaveString("videoAdTimeOut", _date);
	}

	// Token: 0x06000C06 RID: 3078 RVA: 0x00074AD0 File Offset: 0x00072ED0
	public static int GetFreshCount()
	{
		if (ObscuredPrefs.HasKey("freshFreeCount"))
		{
			return ObscuredPrefs.GetInt("freshFreeCount");
		}
		return -1;
	}

	// Token: 0x06000C07 RID: 3079 RVA: 0x00074AED File Offset: 0x00072EED
	public static int GetFreshStartCount()
	{
		return PlayerPrefsX.GetInt("freshFreeStartCount");
	}

	// Token: 0x06000C08 RID: 3080 RVA: 0x00074AF9 File Offset: 0x00072EF9
	public static void SetFreshCount(int _count)
	{
		PlayerPrefsX.SaveInt("freshFreeCount", _count);
	}

	// Token: 0x06000C09 RID: 3081 RVA: 0x00074B06 File Offset: 0x00072F06
	public static string GetFreshTimeOut()
	{
		return PlayerPrefsX.GetString("freshFreeCoolDown");
	}

	// Token: 0x06000C0A RID: 3082 RVA: 0x00074B12 File Offset: 0x00072F12
	public static int GetFreshStartTimeOut()
	{
		return PlayerPrefsX.GetInt("freshFreeStartCoolDown");
	}

	// Token: 0x06000C0B RID: 3083 RVA: 0x00074B1E File Offset: 0x00072F1E
	public static void SetFreshTimeOut(string _date)
	{
		PlayerPrefsX.SaveString("freshFreeCoolDown", _date);
	}

	// Token: 0x06000C0C RID: 3084 RVA: 0x00074B2B File Offset: 0x00072F2B
	public static int GetPathVersion()
	{
		return PlayerPrefsX.GetInt("pathVersion");
	}

	// Token: 0x06000C0D RID: 3085 RVA: 0x00074B37 File Offset: 0x00072F37
	public static string GetLastLoginTime()
	{
		return PlayerPrefsX.GetString("LastLogin");
	}

	// Token: 0x06000C0E RID: 3086 RVA: 0x00074B43 File Offset: 0x00072F43
	public static bool GetMuteSoundFX()
	{
		return PlayerPrefsX.GetBool("soundMute", false);
	}

	// Token: 0x06000C0F RID: 3087 RVA: 0x00074B50 File Offset: 0x00072F50
	public static bool GetMuteMusic()
	{
		return PlayerPrefsX.GetBool("soundMuteMusic", false);
	}

	// Token: 0x06000C10 RID: 3088 RVA: 0x00074B5D File Offset: 0x00072F5D
	public static bool GetLefty()
	{
		return PlayerPrefsX.GetBool("Lefty", false);
	}

	// Token: 0x06000C11 RID: 3089 RVA: 0x00074B6A File Offset: 0x00072F6A
	public static bool GetPerfMode()
	{
		return PlayerPrefsX.GetBool("PerfMode", false);
	}

	// Token: 0x06000C12 RID: 3090 RVA: 0x00074B77 File Offset: 0x00072F77
	public static bool GetEveryplayEnabled()
	{
		return PlayerPrefsX.GetBool("EveryplayEnabled", true);
	}

	// Token: 0x06000C13 RID: 3091 RVA: 0x00074B84 File Offset: 0x00072F84
	public static string GetFacebookId()
	{
		return PlayerPrefsX.GetString("FacebookId");
	}

	// Token: 0x06000C14 RID: 3092 RVA: 0x00074B90 File Offset: 0x00072F90
	public static string GetUserName()
	{
		return PlayerPrefsX.GetString("UserName");
	}

	// Token: 0x06000C15 RID: 3093 RVA: 0x00074BAC File Offset: 0x00072FAC
	public static string GetYoutubeName()
	{
		return PlayerPrefsX.GetString("youtubeName");
	}

	// Token: 0x06000C16 RID: 3094 RVA: 0x00074BC8 File Offset: 0x00072FC8
	public static string GetYoutubeId()
	{
		return PlayerPrefsX.GetString("youtubeId");
	}

	// Token: 0x06000C17 RID: 3095 RVA: 0x00074BE4 File Offset: 0x00072FE4
	public static string GetUserTag()
	{
		return PlayerPrefsX.GetString("UserTag");
	}

	// Token: 0x06000C18 RID: 3096 RVA: 0x00074C00 File Offset: 0x00073000
	public static string GetUserId()
	{
		return PlayerPrefsX.GetString("UserID");
	}

	// Token: 0x06000C19 RID: 3097 RVA: 0x00074C1C File Offset: 0x0007301C
	public static string GetTeamName()
	{
		return PlayerPrefsX.GetString("teamName");
	}

	// Token: 0x06000C1A RID: 3098 RVA: 0x00074C38 File Offset: 0x00073038
	public static string GetTeamId()
	{
		return PlayerPrefsX.GetString("teamId");
	}

	// Token: 0x06000C1B RID: 3099 RVA: 0x00074C54 File Offset: 0x00073054
	public static TeamRole GetTeamRole()
	{
		string @string = PlayerPrefsX.GetString("teamRole");
		TeamRole teamRole = TeamRole.NotInTeam;
		if (!string.IsNullOrEmpty(@string))
		{
			teamRole = (TeamRole)Enum.Parse(typeof(TeamRole), @string);
		}
		return teamRole;
	}

	// Token: 0x06000C1C RID: 3100 RVA: 0x00074C90 File Offset: 0x00073090
	public static bool GetTeamJoined()
	{
		return PlayerPrefsX.GetBool("joinedTeam", false);
	}

	// Token: 0x06000C1D RID: 3101 RVA: 0x00074C9D File Offset: 0x0007309D
	public static void SetTeamJoined(bool _joined)
	{
		PlayerPrefsX.SetBool("joinedTeam", _joined);
	}

	// Token: 0x06000C1E RID: 3102 RVA: 0x00074CAC File Offset: 0x000730AC
	public static void SetTeamName(string _name)
	{
		string text = _name;
		if (text == null)
		{
			text = string.Empty;
		}
		PlayerPrefsX.SaveString("teamName", text);
	}

	// Token: 0x06000C1F RID: 3103 RVA: 0x00074CD4 File Offset: 0x000730D4
	public static void SetTeamId(string _id)
	{
		string text = _id;
		if (text == null)
		{
			text = string.Empty;
		}
		PlayerPrefsX.SaveString("teamId", text);
	}

	// Token: 0x06000C20 RID: 3104 RVA: 0x00074CFA File Offset: 0x000730FA
	public static void SetTeamRole(TeamRole _role)
	{
		PlayerPrefsX.SaveString("teamRole", _role.ToString());
	}

	// Token: 0x06000C21 RID: 3105 RVA: 0x00074D13 File Offset: 0x00073113
	public static int GetItemDbVersion()
	{
		return ObscuredPrefs.GetInt("itemDb");
	}

	// Token: 0x06000C22 RID: 3106 RVA: 0x00074D1F File Offset: 0x0007311F
	public static string GetGameCenterId()
	{
		return PlayerPrefsX.GetString("GameCenterId");
	}

	// Token: 0x06000C23 RID: 3107 RVA: 0x00074D2C File Offset: 0x0007312C
	public static string GetDeviceToken()
	{
		string text = PlayerPrefsX.GetString("deviceToken");
		if (!string.IsNullOrEmpty(text))
		{
			text = text.Replace("\"", string.Empty);
		}
		return text;
	}

	// Token: 0x06000C24 RID: 3108 RVA: 0x00074D61 File Offset: 0x00073161
	public static void SetDeviceToken(string token)
	{
		PlayerPrefsX.SaveString("deviceToken", token);
	}

	// Token: 0x06000C25 RID: 3109 RVA: 0x00074D6E File Offset: 0x0007316E
	public static string GetMetricsGUID()
	{
		return PlayerPrefsX.GetString("MetricsGUID");
	}

	// Token: 0x06000C26 RID: 3110 RVA: 0x00074D7A File Offset: 0x0007317A
	public static void SetMetricsGUID(string guid)
	{
		PlayerPrefsX.SaveString("MetricsGUID", guid);
	}

	// Token: 0x06000C27 RID: 3111 RVA: 0x00074D87 File Offset: 0x00073187
	public static void SetLastSync(string _lastLogin)
	{
		PlayerPrefsX.SaveString("LastSync", _lastLogin);
	}

	// Token: 0x06000C28 RID: 3112 RVA: 0x00074D94 File Offset: 0x00073194
	public static string GetLastSync()
	{
		return PlayerPrefsX.GetString("LastSync");
	}

	// Token: 0x06000C29 RID: 3113 RVA: 0x00074DA0 File Offset: 0x000731A0
	public static void SetLastVehicle(int _index)
	{
		PlayerPrefsX.SaveInt("lastVehicle", _index);
	}

	// Token: 0x06000C2A RID: 3114 RVA: 0x00074DAD File Offset: 0x000731AD
	public static int GetLastVehicle()
	{
		return PlayerPrefsX.GetInt("lastVehicle");
	}

	// Token: 0x06000C2B RID: 3115 RVA: 0x00074DB9 File Offset: 0x000731B9
	public static void SetOffroadRacing(bool _unlocked)
	{
		PlayerPrefsX.SetBool("offroadCarRacing", _unlocked);
	}

	// Token: 0x06000C2C RID: 3116 RVA: 0x00074DC6 File Offset: 0x000731C6
	public static bool GetOffroadRacing()
	{
		return PlayerPrefsX.GetBool("offroadCarRacing", false);
	}

	// Token: 0x06000C2D RID: 3117 RVA: 0x00074DD3 File Offset: 0x000731D3
	[Obsolete("You should not use this method!.", false)]
	public static void SetMotorcyclePlay(bool _unlocked)
	{
		PlayerPrefsX.SetBool("motorcyclePlay", _unlocked);
	}

	// Token: 0x06000C2E RID: 3118 RVA: 0x00074DE0 File Offset: 0x000731E0
	[Obsolete("Use PsMetagameManager.IsVehicleUnlocked() method, instead of this.", false)]
	public static bool GetMotorcyclePlay()
	{
		return PlayerPrefsX.GetBool("motorcyclePlay", false);
	}

	// Token: 0x06000C2F RID: 3119 RVA: 0x00074DED File Offset: 0x000731ED
	public static void SetMotorcycleChecked(bool _checked)
	{
		PlayerPrefsX.SetBool("motorcyleChecked", _checked);
	}

	// Token: 0x06000C30 RID: 3120 RVA: 0x00074DFA File Offset: 0x000731FA
	public static bool GetMotorcycleChecked()
	{
		return PlayerPrefsX.GetBool("motorcyleChecked", false);
	}

	// Token: 0x06000C31 RID: 3121 RVA: 0x00074E07 File Offset: 0x00073207
	public static void SetFreshAndFree(bool _unlocked)
	{
		PlayerPrefsX.SetBool("FreshUnlocked", _unlocked);
	}

	// Token: 0x06000C32 RID: 3122 RVA: 0x00074E14 File Offset: 0x00073214
	public static bool GetFreshAndFree()
	{
		return PlayerPrefsX.GetBool("FreshUnlocked", false);
	}

	// Token: 0x06000C33 RID: 3123 RVA: 0x00074E21 File Offset: 0x00073221
	public static bool GetNameChanged()
	{
		return PlayerPrefsX.GetNameChangesCount() > 0 || PlayerPrefsX.GetBool("nameChanged");
	}

	// Token: 0x06000C34 RID: 3124 RVA: 0x00074E3B File Offset: 0x0007323B
	public static void SetExistingNotify(bool _show)
	{
		PlayerPrefsX.SetBool("notifyExisting", _show);
	}

	// Token: 0x06000C35 RID: 3125 RVA: 0x00074E48 File Offset: 0x00073248
	public static bool GetExistingNotify()
	{
		return PlayerPrefsX.GetBool("notifyExisting", false);
	}

	// Token: 0x06000C36 RID: 3126 RVA: 0x00074E55 File Offset: 0x00073255
	public static void SetLowEndPrompt(bool _show)
	{
		PlayerPrefsX.SetBool("lowEndPrompted", _show);
	}

	// Token: 0x06000C37 RID: 3127 RVA: 0x00074E62 File Offset: 0x00073262
	public static bool GetLowEndPrompt()
	{
		return PlayerPrefsX.GetBool("lowEndPrompted", false);
	}

	// Token: 0x06000C38 RID: 3128 RVA: 0x00074E6F File Offset: 0x0007326F
	public static void SetTeamUnlocked(bool _value)
	{
		PlayerPrefsX.SetBool("teamUnlocked", _value);
	}

	// Token: 0x06000C39 RID: 3129 RVA: 0x00074E7C File Offset: 0x0007327C
	public static bool GetTeamUnlocked()
	{
		return PlayerPrefsX.GetBool("teamUnlocked", false);
	}

	// Token: 0x06000C3A RID: 3130 RVA: 0x00074E89 File Offset: 0x00073289
	public static void SetShowEjection(bool _value)
	{
		PlayerPrefsX.SetBool("showEjection", _value);
	}

	// Token: 0x06000C3B RID: 3131 RVA: 0x00074E96 File Offset: 0x00073296
	public static bool GetShowEjection()
	{
		return PlayerPrefsX.GetBool("showEjection", false);
	}

	// Token: 0x06000C3C RID: 3132 RVA: 0x00074EA3 File Offset: 0x000732A3
	public static void SetChestWarnings(bool _enabled)
	{
		PlayerPrefsX.SetBool("chestWarnings", _enabled);
	}

	// Token: 0x06000C3D RID: 3133 RVA: 0x00074EB0 File Offset: 0x000732B0
	public static bool GetChestWarnings()
	{
		return PlayerPrefsX.GetBool("chestWarnings", true);
	}

	// Token: 0x06000C3E RID: 3134 RVA: 0x00074EBD File Offset: 0x000732BD
	public static void SetSeasonEnded(bool _ended)
	{
		PlayerPrefsX.SetBool("seasonEnded", _ended);
	}

	// Token: 0x06000C3F RID: 3135 RVA: 0x00074ECA File Offset: 0x000732CA
	public static bool GetSeasonEnded()
	{
		return PlayerPrefsX.GetBool("seasonEnded", false);
	}

	// Token: 0x06000C40 RID: 3136 RVA: 0x00074ED7 File Offset: 0x000732D7
	public static void SetNewComments(int _amount)
	{
		PlayerPrefsX.SaveInt("newComments", _amount);
	}

	// Token: 0x06000C41 RID: 3137 RVA: 0x00074EE4 File Offset: 0x000732E4
	public static int GetNewComments()
	{
		return PlayerPrefsX.GetInt("newComments");
	}

	// Token: 0x06000C42 RID: 3138 RVA: 0x00074EF0 File Offset: 0x000732F0
	public static void SetOwnLevelClaimCount(int _count)
	{
		PlayerPrefsX.SaveInt("unclaimedLevels", _count);
	}

	// Token: 0x06000C43 RID: 3139 RVA: 0x00074EFD File Offset: 0x000732FD
	public static int GetOwnLevelClaimCount()
	{
		return PlayerPrefsX.GetInt("unclaimedLevels");
	}

	// Token: 0x06000C44 RID: 3140 RVA: 0x00074F09 File Offset: 0x00073309
	public static bool GetGPGSSignedOut()
	{
		return PlayerPrefsX.GetBool("gpgsSignedOut", false);
	}

	// Token: 0x06000C45 RID: 3141 RVA: 0x00074F16 File Offset: 0x00073316
	public static void SetGPGSSignedOut(bool _value)
	{
		PlayerPrefsX.SetBool("gpgsSignedOut", _value);
	}

	// Token: 0x06000C46 RID: 3142 RVA: 0x00074F23 File Offset: 0x00073323
	public static bool GetLocalSavePrompted()
	{
		return PlayerPrefsX.GetBool("localSavePrompt", false);
	}

	// Token: 0x06000C47 RID: 3143 RVA: 0x00074F30 File Offset: 0x00073330
	public static void SetLocalSavePrompted(bool _value)
	{
		PlayerPrefsX.SetBool("localSavePrompt", _value);
	}

	// Token: 0x06000C48 RID: 3144 RVA: 0x00074F40 File Offset: 0x00073340
	public static string[] GetNewItems()
	{
		string @string = PlayerPrefsX.GetString("newItems");
		string[] array = new string[0];
		if (!string.IsNullOrEmpty(@string))
		{
			array = @string.Split(new char[] { ',' });
		}
		return array;
	}

	// Token: 0x06000C49 RID: 3145 RVA: 0x00074F80 File Offset: 0x00073380
	public static void SetNewItems(string[] _items)
	{
		string text = string.Empty;
		for (int i = 0; i < _items.Length; i++)
		{
			text += _items[i];
			if (i < _items.Length - 1)
			{
				text += ",";
			}
		}
		PlayerPrefsX.SaveString("newItems", text);
	}

	// Token: 0x06000C4A RID: 3146 RVA: 0x00074FD3 File Offset: 0x000733D3
	private static void SaveString(string name, string value)
	{
		ObscuredPrefs.SetString(name, value);
		ObscuredPrefs.Save();
	}

	// Token: 0x06000C4B RID: 3147 RVA: 0x00074FE1 File Offset: 0x000733E1
	private static void SaveInt(string name, int value)
	{
		ObscuredPrefs.SetInt(name, value);
		ObscuredPrefs.Save();
	}

	// Token: 0x06000C4C RID: 3148 RVA: 0x00074FEF File Offset: 0x000733EF
	private static int GetInt(string name)
	{
		if (ObscuredPrefs.HasKey(name))
		{
			return ObscuredPrefs.GetInt(name);
		}
		return 0;
	}

	// Token: 0x06000C4D RID: 3149 RVA: 0x00075004 File Offset: 0x00073404
	public static string GetString(string key)
	{
		string @string = ObscuredPrefs.GetString(key);
		if (@string.Equals(string.Empty))
		{
			return null;
		}
		return @string;
	}

	// Token: 0x06000C4E RID: 3150 RVA: 0x0007502B File Offset: 0x0007342B
	private static void SetBool(string _name, bool _booleanValue)
	{
		ObscuredPrefs.SetInt(_name, (!_booleanValue) ? 0 : 1);
		ObscuredPrefs.Save();
	}

	// Token: 0x06000C4F RID: 3151 RVA: 0x00075045 File Offset: 0x00073445
	private static bool GetBool(string _name)
	{
		return ObscuredPrefs.GetInt(_name) == 1;
	}

	// Token: 0x06000C50 RID: 3152 RVA: 0x0007505A File Offset: 0x0007345A
	private static bool GetBool(string name, bool defaultValue)
	{
		if (ObscuredPrefs.HasKey(name))
		{
			return PlayerPrefsX.GetBool(name);
		}
		return defaultValue;
	}

	// Token: 0x06000C51 RID: 3153 RVA: 0x0007506F File Offset: 0x0007346F
	public static void SetSoundbankVersion(int _version)
	{
		PlayerPrefsX.SaveString("SoundBank", _version.ToString());
	}

	// Token: 0x06000C52 RID: 3154 RVA: 0x00075088 File Offset: 0x00073488
	public static int GetSoundbankVersion()
	{
		string @string = PlayerPrefsX.GetString("SoundBank");
		int num = 0;
		if (!string.IsNullOrEmpty(@string))
		{
			num = Convert.ToInt32(@string);
		}
		return num;
	}

	// Token: 0x06000C53 RID: 3155 RVA: 0x000750B5 File Offset: 0x000734B5
	public static void SetFileVersion(string _name, int _version)
	{
		PlayerPrefsX.SaveString(_name, _version.ToString());
	}

	// Token: 0x06000C54 RID: 3156 RVA: 0x000750CC File Offset: 0x000734CC
	public static int GetFileVersion(string _name)
	{
		string @string = PlayerPrefsX.GetString(_name);
		int num = 0;
		if (!string.IsNullOrEmpty(@string))
		{
			num = Convert.ToInt32(@string);
		}
		return num;
	}

	// Token: 0x06000C55 RID: 3157 RVA: 0x000750F8 File Offset: 0x000734F8
	public static int GetRatingStatus()
	{
		string @string = PlayerPrefsX.GetString("RatingStatus");
		int num = 0;
		if (!string.IsNullOrEmpty(@string))
		{
			num = Convert.ToInt32(@string);
		}
		return num;
	}

	// Token: 0x06000C56 RID: 3158 RVA: 0x00075125 File Offset: 0x00073525
	public static void SetRatingStatus(int _status)
	{
		PlayerPrefsX.SaveString("RatingStatus", _status.ToString());
	}

	// Token: 0x04000A58 RID: 2648
	public const string METRICS_GUID = "MetricsGUID";

	// Token: 0x04000A59 RID: 2649
	public const string GAME_CENTER_ID = "GameCenterId";

	// Token: 0x04000A5A RID: 2650
	public const string GAME_CENTER_NAME = "GameCenterName";

	// Token: 0x04000A5B RID: 2651
	public const string FACEBOOK_ID = "FacebookId";

	// Token: 0x04000A5C RID: 2652
	public const string FACEBOOK_NAME = "FacebookName";

	// Token: 0x04000A5D RID: 2653
	public const string USER_NAME = "UserName";

	// Token: 0x04000A5E RID: 2654
	public const string USER_TAG = "UserTag";

	// Token: 0x04000A5F RID: 2655
	public const string USER_ID = "UserID";

	// Token: 0x04000A60 RID: 2656
	public const string ACCEPT_NOTIFICATIONS = "AcceptNotifications";

	// Token: 0x04000A61 RID: 2657
	public const string STREAMER_HUD = "StreamerHud";

	// Token: 0x04000A62 RID: 2658
	public const string DEVICE_TOKEN = "deviceToken";

	// Token: 0x04000A63 RID: 2659
	public const string SOUND_MUTE_SOUNDFX = "soundMute";

	// Token: 0x04000A64 RID: 2660
	public const string SOUND_MUTE_MUSIC = "soundMuteMusic";

	// Token: 0x04000A65 RID: 2661
	public const string ITEM_DB_VERSION = "itemDb";

	// Token: 0x04000A66 RID: 2662
	public const string LEFTY_FLIP = "Lefty";

	// Token: 0x04000A67 RID: 2663
	public const string PERF_MODE = "PerfMode";

	// Token: 0x04000A68 RID: 2664
	public const string EVERYPLAY_ENABLED = "EveryplayEnabled";

	// Token: 0x04000A69 RID: 2665
	public const string LAST_LOGIN = "LastLogin";

	// Token: 0x04000A6A RID: 2666
	public const string PATH_VERSION = "pathVersion";

	// Token: 0x04000A6B RID: 2667
	public const string CAR_RENT_REFRESH = "carRentRefresh";

	// Token: 0x04000A6C RID: 2668
	public const string FRESHFREE_INTERVAL = "freshFreeInterval";

	// Token: 0x04000A6D RID: 2669
	public const string SESSION_ID = "sessionId";

	// Token: 0x04000A6E RID: 2670
	public const string START_BOLTS = "startBolts";

	// Token: 0x04000A6F RID: 2671
	public const string START_COINS = "startCoins";

	// Token: 0x04000A70 RID: 2672
	public const string START_DIAMONDS = "startDiamonds";

	// Token: 0x04000A71 RID: 2673
	public const string START_KEYS = "startKeys";

	// Token: 0x04000A72 RID: 2674
	public const string FB_CONNECT_REWARD = "fbConnectRewardAmount";

	// Token: 0x04000A73 RID: 2675
	public const string VIDEO_AD_TIME_OUT = "videoAdTimeOut";

	// Token: 0x04000A74 RID: 2676
	public const string VIDEO_AD_START_TIME_OUT = "videoAdStartTimeOut";

	// Token: 0x04000A75 RID: 2677
	public const string VIDEO_AD_COUNT = "videoAdCount";

	// Token: 0x04000A76 RID: 2678
	public const string VIDEO_AD_START_COUNT = "videoAdStartCount";

	// Token: 0x04000A77 RID: 2679
	public const string FRESH_LEVEL_COOLDOWN = "freshFreeCoolDown";

	// Token: 0x04000A78 RID: 2680
	public const string FRESH_LEVEL_START_COOLDWON = "freshFreeStartCoolDown";

	// Token: 0x04000A79 RID: 2681
	public const string FRESH_LEVEL_COUNT = "freshFreeCount";

	// Token: 0x04000A7A RID: 2682
	public const string FRESH_LEVEL_START_COUNT = "freshFreeStartCount";

	// Token: 0x04000A7B RID: 2683
	public const string DAILY_GEM_MULTIPLIER = "dailyGemMultiplier";

	// Token: 0x04000A7C RID: 2684
	public const string FIRST_LOGIN = "firstLogin";

	// Token: 0x04000A7D RID: 2685
	public const string CREATOR_RANK_1 = "creatorRank1";

	// Token: 0x04000A7E RID: 2686
	public const string CREATOR_RANK_2 = "creatorRank2";

	// Token: 0x04000A7F RID: 2687
	public const string CREATOR_RANK_3 = "creatorRank3";

	// Token: 0x04000A80 RID: 2688
	public const string CREATOR_RANK_4 = "creatorRank4";

	// Token: 0x04000A81 RID: 2689
	public const string CREATOR_RANK_5 = "creatorRank5";

	// Token: 0x04000A82 RID: 2690
	public const string CREATOR_RANK_6 = "creatorRank6";

	// Token: 0x04000A83 RID: 2691
	public const string FIRST_SEEN = "firstSeen";

	// Token: 0x04000A84 RID: 2692
	public const string MAJOR_PATH = "majorPath";

	// Token: 0x04000A85 RID: 2693
	public const string LAST_SYNC = "LastSync";

	// Token: 0x04000A86 RID: 2694
	public const string USED_EDITOR = "playerUsedEditor";

	// Token: 0x04000A87 RID: 2695
	public const string PUBLISHED_LEVEL = "playerPublishedLevel";

	// Token: 0x04000A88 RID: 2696
	public const string COUNTRY_CODE = "country";

	// Token: 0x04000A89 RID: 2697
	public const string LAST_VEHICLE = "lastVehicle";

	// Token: 0x04000A8A RID: 2698
	public const string CHEST_WARNINGS = "chestWarnings";

	// Token: 0x04000A8B RID: 2699
	public const string YOUTUBE_NAME = "youtubeName";

	// Token: 0x04000A8C RID: 2700
	public const string YOUTUBE_ID = "youtubeId";

	// Token: 0x04000A8D RID: 2701
	public const string SHOP_NOTIFICATION = "ShopNotification";

	// Token: 0x04000A8E RID: 2702
	public const string LOCAL_PLANET_VERSIONS = "planets";

	// Token: 0x04000A8F RID: 2703
	public const string LAUNCH_URL = "LaunchUrl";

	// Token: 0x04000A90 RID: 2704
	public const string SUPERLIKE_INTERVAL = "SuperLikeInterval";

	// Token: 0x04000A91 RID: 2705
	public const string SOUNDBANK_VERSION = "SoundBank";

	// Token: 0x04000A92 RID: 2706
	public const string RATING_STATUS = "RatingStatus";

	// Token: 0x04000A93 RID: 2707
	public const string AD_USED_ON_PLANETLEVEL = "adUsedOnLevel";

	// Token: 0x04000A94 RID: 2708
	public const string TEAM_ID = "teamId";

	// Token: 0x04000A95 RID: 2709
	public const string TEAM_NAME = "teamName";

	// Token: 0x04000A96 RID: 2710
	public const string TEAM_ROLE = "teamRole";

	// Token: 0x04000A97 RID: 2711
	public const string JOINED_TEAM = "joinedTeam";

	// Token: 0x04000A98 RID: 2712
	public const string NEW_COMMENTS = "newComments";

	// Token: 0x04000A99 RID: 2713
	public const string SEASON_END = "seasonEnded";

	// Token: 0x04000A9A RID: 2714
	public const string LANGUAGE = "Language";

	// Token: 0x04000A9B RID: 2715
	public const string TEAM_UNLOCKED = "teamUnlocked";

	// Token: 0x04000A9C RID: 2716
	public const string PUBLISHED_UNCLAIMED_COUNT = "unclaimedLevels";

	// Token: 0x04000A9D RID: 2717
	public const string GPGS_SIGNED_OUT = "gpgsSignedOut";

	// Token: 0x04000A9E RID: 2718
	public const string LOCAL_SAVE_PROMPTED = "localSavePrompt";

	// Token: 0x04000A9F RID: 2719
	public const string NEW_ITEMS = "newItems";

	// Token: 0x04000AA0 RID: 2720
	public const string OFFROADCAR_RACING = "offroadCarRacing";

	// Token: 0x04000AA1 RID: 2721
	public const string MOTORCYCLE_PLAY = "motorcyclePlay";

	// Token: 0x04000AA2 RID: 2722
	public const string MOTORCYCLE_CHECKED = "motorcyleChecked";

	// Token: 0x04000AA3 RID: 2723
	public const string NAME_CHANGED = "nameChanged";

	// Token: 0x04000AA4 RID: 2724
	public const string FRESH_UNLOCKED = "FreshUnlocked";

	// Token: 0x04000AA5 RID: 2725
	public const string EXISTING_NOTIFY = "notifyExisting";

	// Token: 0x04000AA6 RID: 2726
	public const string LOW_END_PROMPT = "lowEndPrompted";

	// Token: 0x04000AA7 RID: 2727
	public const string INITIAL_USER_PROPERTIES_SENT = "initialUserPropertiesSent";

	// Token: 0x04000AA8 RID: 2728
	public const string INITIAL_USER_PROPERTIES_TIME = "initialUserPropertiesTime";

	// Token: 0x04000AA9 RID: 2729
	public const string UPDATE_ONE_TIME_METRICS = "updateOneTimeMetrics";

	// Token: 0x04000AAA RID: 2730
	public const string SHOW_EJECTION_MESSAGE = "showEjection";

	// Token: 0x04000AAB RID: 2731
	public const string MINIMUM_TOURNAMENT_NITROS = "minTournamentNitros";

	// Token: 0x04000AAC RID: 2732
	public const string YOUTUBER_TOURNAMENT_NITROS = "tournamentYoutuberFollowNitros";

	// Token: 0x04000AAD RID: 2733
	public const string TOURNAMENT_CHAT_TIMESTAMP = "tournamentChatLastTimestamp";

	// Token: 0x04000AAE RID: 2734
	public const string UPGRADE_REMIND_COUNT = "upgradeRemindCount";

	// Token: 0x04000AAF RID: 2735
	public const string UPGRADE_REMIND_LASTTIME = "upgradeRemindEpoch";

	// Token: 0x04000AB0 RID: 2736
	public const string RACE_LOSING_STREAK = "losingStreak";

	// Token: 0x04000AB1 RID: 2737
	public const string GIF_DEATH_RECORD = "GifDeathRecord";

	// Token: 0x04000AB2 RID: 2738
	public const string NAME_CHANGES_DONE = "nameChangesDone";
}
