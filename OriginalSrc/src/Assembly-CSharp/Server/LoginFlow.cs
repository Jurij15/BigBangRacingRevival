using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Server
{
	// Token: 0x02000428 RID: 1064
	public static class LoginFlow
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06001D7C RID: 7548 RVA: 0x00150432 File Offset: 0x0014E832
		public static bool isCompleted
		{
			get
			{
				return LoginFlow.m_isCompeleted;
			}
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x0015043C File Offset: 0x0014E83C
		public static HttpC Start(Action<HttpC> _okCallback, Action<HttpC> _failCallback, Action _errorCallback = null)
		{
			if (PsState.m_openSocialPopup != null)
			{
				PsState.m_openSocialPopup.Destroy();
				PsState.m_openSocialPopup = null;
			}
			Debug.Log("LoginFlow Start", null);
			LoginFlow.m_isCompeleted = false;
			GooglePlayManager.Initialize();
			if (!PlayerPrefsX.GetGPGSSignedOut())
			{
				GooglePlayManager.Login(null);
			}
			if (string.IsNullOrEmpty(PlayerPrefsX.GetUserId()) || string.IsNullOrEmpty(PlayerPrefsX.GetUserName()))
			{
				string text = PsStrings.Get(StringID.PLAYER_TEMP_NAME);
				PlayerPrefsX.SetUserName(text);
				PsMetagameManager.m_firstTimeLogin = true;
			}
			if (LoginFlow.m_firstTimeLoginBegin)
			{
				PsMetrics.FirstLoginStarted();
				LoginFlow.m_firstTimeLoginBegin = false;
			}
			return Player.Login(delegate(HttpC c)
			{
				LoginFlow.LoginOk(c, _okCallback, _failCallback);
			}, _failCallback, null);
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x00150504 File Offset: 0x0014E904
		private static void AfterLogin()
		{
			PsPersistentData.Init();
			if (FacebookManager.m_friendLoadPending)
			{
				FacebookManager.LoadFriends(false);
			}
			PsMetagameManager.GetFriends(delegate(Friends c)
			{
				Debug.Log("Friends loaded", null);
			}, true);
			if (PlayerPrefsX.GetTeamId() != null)
			{
				PsMetagameManager.GetOwnTeam(delegate(TeamData c)
				{
					Debug.Log("Own team loaded", null);
				}, true);
			}
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x00150578 File Offset: 0x0014E978
		private static void CheckGamemodeUnlocks()
		{
			foreach (KeyValuePair<string, PlanetProgressionInfo> keyValuePair in PlanetTools.m_planetProgressionInfos)
			{
				List<string> list = new List<string>();
				if (!PlayerPrefsX.GetOffroadRacing())
				{
					list.Add("unlock_offroadcar_racing");
				}
				if (!PsMetagameManager.IsVehicleUnlocked(typeof(Motorcycle)))
				{
					list.Add("unlock_motorcycle_planet");
				}
				if (!PlayerPrefsX.GetFreshAndFree())
				{
					list.Add("unlock_freshandfree");
				}
				PsPlanetPath mainPath = keyValuePair.Value.m_mainPath;
				List<PsGameLoop> nodeInfos = mainPath.m_nodeInfos;
				for (int i = 0; i < nodeInfos.Count; i++)
				{
					if (nodeInfos[i].m_nodeId > mainPath.m_currentNodeId)
					{
						break;
					}
					if (nodeInfos[i].m_dialogues != null)
					{
						for (int j = 0; j < list.Count; j++)
						{
							if (nodeInfos[i].m_dialogues.ContainsValue(list[j]))
							{
								if (list[j] == "unlock_offroadcar_racing")
								{
									PlayerPrefsX.SetOffroadRacing(true);
								}
								if (list[j] == "unlock_freshandfree")
								{
									PlayerPrefsX.SetFreshAndFree(true);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x00150704 File Offset: 0x0014EB04
		private static void HandleOldUserProgression()
		{
			bool flag = true;
			foreach (KeyValuePair<string, PlanetProgressionInfo> keyValuePair in PlanetTools.m_planetProgressionInfos)
			{
				PsPlanetPath mainPath = keyValuePair.Value.m_mainPath;
				if (mainPath.m_planet != "RacingMotorcycle")
				{
					if (mainPath.m_currentNodeId > 1)
					{
						flag = false;
						break;
					}
				}
			}
			if (flag)
			{
				foreach (KeyValuePair<string, PlanetProgressionInfo> keyValuePair2 in PlanetTools.m_planetProgressionInfos)
				{
					PsPlanetPath mainPath2 = keyValuePair2.Value.m_mainPath;
					if (mainPath2.m_planet == "RacingMotorcycle")
					{
						if (mainPath2.m_currentNodeId > 1)
						{
							if (!PlayerPrefsX.GetOffroadRacing())
							{
								PsMainMenuState.m_createExistingPopup = true;
								PlayerPrefsX.SetExistingNotify(true);
								PsState.SetVehicleIndex(1);
							}
							PlayerPrefsX.SetOffroadRacing(true);
							PlayerPrefsX.SetMotorcyclePlay(true);
							PlayerPrefsX.SetFreshAndFree(true);
							PlayerPrefsX.SetMotorcycleChecked(true);
						}
						else
						{
							PsState.SetVehicleIndex(0);
						}
						break;
					}
				}
			}
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x00150860 File Offset: 0x0014EC60
		private static void LoadData(Dictionary<string, object> serverResponse, Action _callback)
		{
			PsMetagameManager.m_keyReloadTimeSeconds = PsMetagameManager.GetSecondsFromTimeString(PsMetagameManager.GetKeyReloadTime());
			List<PsPlanetPath> list = ClientTools.ParseProgressionPathData(serverResponse["paths"] as List<object>, false);
			PlanetTools.UpdateLocalPlanetPaths(list);
			PlanetTools.OpenAllUnlocks();
			LoginFlow.HandleOldUserProgression();
			if (!PlayerPrefsX.GetOffroadRacing() || !PsMetagameManager.IsVehicleUnlocked(typeof(Motorcycle)))
			{
				LoginFlow.CheckGamemodeUnlocks();
			}
			bool flag = false;
			if (serverResponse.ContainsKey("sessionExpiration"))
			{
				flag = (bool)serverResponse["sessionExpiration"];
			}
			if (flag)
			{
				LoginFlow.ClearServerQueueItems();
				ServerManager.ClearRetryCallBacks();
			}
			else
			{
				ServerManager.ExecuteRetryCallBacks();
			}
			if (serverResponse.ContainsKey("lastLogin"))
			{
				Dictionary<string, object> dictionary = (Dictionary<string, object>)serverResponse["lastLogin"];
				PlayerPrefsX.SetLastLoginTime(Convert.ToUInt64(dictionary["$date"]).ToString());
			}
			if (LoginFlow.m_firstTimeLoginFinished)
			{
				PsMetrics.FirstLoginFinished();
				LoginFlow.m_firstTimeLoginFinished = false;
			}
			if (serverResponse.ContainsKey("adsConfig"))
			{
				List<object> list2 = serverResponse["adsConfig"] as List<object>;
				PsAdMediation.Initialize(list2);
			}
			if (serverResponse.ContainsKey("seasonConfig"))
			{
				SeasonConfig seasonConfig = ClientTools.ParseSeasonConfig(serverResponse["seasonConfig"] as Dictionary<string, object>);
				PsMetagameManager.m_seasonConfig = seasonConfig;
			}
			if (serverResponse.ContainsKey("iapConfig"))
			{
				Dictionary<string, object> dictionary2 = serverResponse["iapConfig"] as Dictionary<string, object>;
				PsIAPManager.Initialize(dictionary2);
			}
			if (serverResponse.ContainsKey("gemPriceConfig"))
			{
				Dictionary<string, object> dictionary3 = serverResponse["gemPriceConfig"] as Dictionary<string, object>;
				PsMetagameManager.ParseGemPrices(dictionary3);
			}
			if (serverResponse.ContainsKey("editorConfig"))
			{
				Dictionary<string, object> dictionary4 = serverResponse["editorConfig"] as Dictionary<string, object>;
				PsMetagameManager.ParseEditorItemLimits(dictionary4);
			}
			if (serverResponse.ContainsKey("achievements"))
			{
				List<object> list3 = serverResponse["achievements"] as List<object>;
				PsAchievementManager.UpdateFromList(list3);
			}
			List<string> list4 = new List<string>();
			if (serverResponse.ContainsKey("numOfPurchases"))
			{
				int num = Convert.ToInt32(serverResponse["numOfPurchases"]);
				if (num > 0)
				{
					list4.Add("paid");
				}
				if (num > 4)
				{
					list4.Add("VIP");
				}
			}
			if (serverResponse.ContainsKey("bossBattleConfig"))
			{
				Dictionary<string, object> dictionary5 = serverResponse["bossBattleConfig"] as Dictionary<string, object>;
				BossBattles.ParseConfig(dictionary5);
			}
			if (serverResponse.ContainsKey("numOfSessions"))
			{
				int num2 = Convert.ToInt32(serverResponse["numOfSessions"]);
				if (num2 < 10)
				{
					list4.Add("new user");
				}
			}
			if (serverResponse.ContainsKey("publishedMinigameCount"))
			{
				int num3 = Convert.ToInt32(serverResponse["publishedMinigameCount"]);
				if (num3 > 9)
				{
					list4.Add("creator");
				}
			}
			if (list4.Count > 0)
			{
				LoginFlow.m_helpshiftTags = list4.ToArray();
			}
			if (serverResponse.ContainsKey("ejectionDay"))
			{
				PlayerPrefsX.SetShowEjection(true);
			}
			if (serverResponse.ContainsKey("tournamentRewardShares"))
			{
				Tournaments.ParseTournamentRewardShares(serverResponse["tournamentRewardShares"] as List<object>);
			}
			else
			{
				Debug.LogError("Tournament shares: NOPE!");
			}
			PsCustomisationManager.SetData(serverResponse);
			PsUpgradeManager.SetData(serverResponse);
			PsGachaManager.SetData(serverResponse);
			PsTimedSpecialOffer psTimedSpecialOffer = ClientTools.ParseTimedSpecialOffer(serverResponse);
			PsMetagameManager.InitializeTimedSpecialOffers(new PsTimedSpecialOffer[] { psTimedSpecialOffer });
			if (serverResponse.ContainsKey("lastPathSync"))
			{
				PlayerPrefsX.SetLastSync((string)serverResponse["lastPathSync"]);
			}
			LoginFlow.AfterLogin();
			FrbMetrics.SetUserId();
			LoginFlow.m_isCompeleted = true;
			_callback.Invoke();
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x00150C08 File Offset: 0x0014F008
		private static void LoginOk(HttpC _c, Action<HttpC> _okCallback, Action<HttpC> _failCallback)
		{
			Dictionary<string, object> serverResponse = ClientTools.ParseServerResponse(_c.www.text);
			if (ClientTools.ServerResponseOk(serverResponse))
			{
				if (serverResponse.ContainsKey("sessionId"))
				{
					PlayerPrefsX.SetSession((string)serverResponse["sessionId"]);
					PsState.m_sessionId = PlayerPrefsX.GetSession();
				}
				Debug.Log("PLAYER LOGGED IN", null);
				long num = (long)serverResponse["clientVersion"];
				int num2 = (int)num;
				string text = (string)serverResponse["versionInfo"];
				string text2 = Main.CLIENT_VERSION().ToString();
				if (serverResponse.ContainsKey("previousLoginClientVersion"))
				{
					text2 = (string)serverResponse["previousLoginClientVersion"];
				}
				string[] array = text2.Split(new char[] { ' ' });
				PsState.m_previousLoginVersion = Convert.ToInt32(array[0]);
				PlayerData playerData = ClientTools.ParsePlayerData(serverResponse);
				PsMetagameManager.SetPlayer(playerData);
				PsMetagameManager.m_followRewardData = ClientTools.ParseFollowRewadData(serverResponse);
				if (serverResponse.ContainsKey("ratingStatus"))
				{
					int num3 = Convert.ToInt32(serverResponse["ratingStatus"]);
					int ratingStatus = PlayerPrefsX.GetRatingStatus();
					if (num3 != 0)
					{
						PlayerPrefsX.SetRatingStatus(num3);
					}
					else if (ratingStatus != 0)
					{
						PsMetagameManager.m_ratingStatusChanged = true;
					}
				}
				if (serverResponse.ContainsKey("acceptNotifications"))
				{
					PlayerPrefsX.SetAcceptNotifications(Convert.ToBoolean(serverResponse["acceptNotifications"]));
				}
				if (serverResponse.ContainsKey("newCommentCount"))
				{
					PlayerPrefsX.SetNewComments(Convert.ToInt32(serverResponse["newCommentCount"]));
				}
				if (serverResponse.ContainsKey("unclaimedLevels"))
				{
					PlayerPrefsX.SetOwnLevelClaimCount(Convert.ToInt32(serverResponse["unclaimedLevels"]));
				}
				SeasonEndData seasonEndData = ClientTools.ParseSeasonEndData(serverResponse);
				if (seasonEndData != null)
				{
					PsMetagameManager.SetSeasonEndData(seasonEndData);
				}
				if (Main.CLIENT_VERSION() < num2)
				{
					Debug.Log(string.Concat(new object[]
					{
						"ClientVersion: ",
						Main.CLIENT_VERSION(),
						", ServerVersion: ",
						num2
					}), null);
					Debug.Log("Client has old version!", null);
					ServerManager.ThrowOldVersionMessage(text);
				}
				else if (!Application.genuine)
				{
					Debug.Log("version not genuine", null);
					string text3 = "Please wait, tracking location...";
					ServerManager.CreateServerErrorPopup("Hi there", text3, ":)", delegate
					{
					});
				}
				else
				{
					if (LoginFlow.m_debugReset)
					{
						if (!serverResponse.ContainsKey("reset"))
						{
							serverResponse.Add("reset", "yep");
						}
						LoginFlow.m_debugReset = false;
						Debug.LogWarning("WARNING! YOU HAVE DEBUG RESET FLAG ON");
					}
					if (serverResponse.ContainsKey("resetLocal"))
					{
						Debug.LogWarning("Resetting local progression...");
						LoginFlow.ClearServerQueueItems();
						ServerManager.ClearRetryCallBacks();
						ServerManager.m_playerAuthenticated = false;
						PsPlanetManager.GetCurrentPlanet().ResetLocalProgression();
						PlayerPrefsX.SetLastSync("0");
						PsState.m_inLoginFlow = false;
						LoginFlow.m_isCompeleted = true;
						Main.m_currentGame.m_sceneManager.ChangeScene(new StartupScene("StartupScene"), null);
					}
					else if (serverResponse.ContainsKey("reset"))
					{
						Debug.LogWarning("Reset progression...");
						LoginFlow.ClearServerQueueItems();
						ServerManager.ClearRetryCallBacks();
						ServerManager.m_playerAuthenticated = false;
						ResetFlow.Start(true);
					}
					else
					{
						if (serverResponse.ContainsKey("firstSeen"))
						{
							Dictionary<string, object> dictionary = (Dictionary<string, object>)serverResponse["firstSeen"];
							PlayerPrefsX.SetFirstSeen((long)dictionary["$date"]);
						}
						if (serverResponse.ContainsKey("epochDays"))
						{
							PsMetagameManager.m_epochDays = Convert.ToInt32(serverResponse["epochDays"]);
						}
						if (serverResponse.ContainsKey("daySecondsLeft"))
						{
							PsMetagameManager.m_daySecondsLeft = Convert.ToInt32(serverResponse["daySecondsLeft"]);
						}
						if (!serverResponse.ContainsKey("gameCenterId"))
						{
							PlayerPrefsX.DeleteKey("GameCenterId");
							PlayerPrefsX.DeleteKey("GameCenterName");
						}
						else
						{
							PlayerPrefsX.SetGameCenterId((string)serverResponse["gameCenterId"]);
						}
						if (!serverResponse.ContainsKey("facebookId"))
						{
							PlayerPrefsX.DeleteKey("FacebookId");
							PlayerPrefsX.DeleteKey("FacebookName");
						}
						else
						{
							PlayerPrefsX.SetFacebookId((string)serverResponse["facebookId"]);
						}
						PlayerPrefsX.SetPlayerData(playerData);
						PlayerPrefsX.SetUserName(playerData.name);
						if (GameCenterManager.IsLoggedIn())
						{
							GameCenterManager.LoadFriends();
						}
						if (serverResponse.ContainsKey("lastClaimedGiftId"))
						{
							if (PsMetagameManager.m_giftEvents == null)
							{
								PsMetagameManager.m_giftEvents = new EventGifts();
							}
							int num4 = Convert.ToInt32(serverResponse["lastClaimedGiftId"]);
							PsMetagameManager.m_giftEvents.lastClaimedGift = num4;
						}
						if (serverResponse.ContainsKey("patchNotes") && serverResponse["patchNotes"] != null)
						{
							EventMessage eventMessage = ClientTools.ParseEventMessageFromDict(serverResponse["patchNotes"] as Dictionary<string, object>);
							if (!string.IsNullOrEmpty(eventMessage.message) && eventMessage.messageId != -1)
							{
								PsMetagameManager.m_patchNotes = eventMessage;
							}
						}
						if (serverResponse.ContainsKey("eventList") && serverResponse["eventList"] != null)
						{
							List<EventMessage> list = new List<EventMessage>();
							foreach (object obj in (serverResponse["eventList"] as List<object>))
							{
								if (obj != null)
								{
									list.Add(ClientTools.ParseEventMessageFromDict(obj as Dictionary<string, object>));
								}
							}
							list = Enumerable.ToList<EventMessage>(Enumerable.OrderBy<EventMessage, int>(list, (EventMessage c) => c.messageId));
							PsMetagameManager.m_eventList = list;
							if (PsMetagameManager.m_giftEvents == null)
							{
								PsMetagameManager.m_giftEvents = new EventGifts();
							}
							PsMetagameManager.m_giftEvents.m_gifts = list.FindAll((EventMessage c) => c.eventType == "Gift" && c.giftContent != null);
							PsMetagameManager.m_activeTournament = null;
							PsMetagameManager.m_tournaments = list.FindAll((EventMessage c) => c.tournament != null);
							PsMetagameManager.m_doubleValueGoodOrBadEvent = list.Find((EventMessage c) => c.liveOps != null && !string.IsNullOrEmpty(c.liveOps.liveOpsEvent));
							if (PsMetagameManager.m_tournaments.Count > 0)
							{
								PsMetagameManager.m_activeTournament = PsMetagameManager.m_tournaments[0];
							}
						}
						if (serverResponse.ContainsKey("eventMessage") && serverResponse["eventMessage"] != null)
						{
							EventMessage eventMessage2 = ClientTools.ParseEventMessageFromDict(serverResponse["eventMessage"] as Dictionary<string, object>);
							if (!string.IsNullOrEmpty(eventMessage2.message) && eventMessage2.messageId != -1)
							{
								PsMetagameManager.m_eventMessage = eventMessage2;
							}
						}
						if (serverResponse.ContainsKey("activeTournament"))
						{
							Dictionary<string, object> dictionary2 = serverResponse["activeTournament"] as Dictionary<string, object>;
							string tournamentId = string.Empty;
							int num5 = 0;
							int num6 = 0;
							bool flag = false;
							bool flag2 = false;
							bool flag3 = false;
							if (dictionary2 != null)
							{
								if (dictionary2.ContainsKey("tournamentRoom"))
								{
									num5 = Convert.ToInt32(dictionary2["tournamentRoom"]);
								}
								if (dictionary2.ContainsKey("tournamentTime"))
								{
									num6 = Convert.ToInt32(dictionary2["tournamentTime"]);
								}
								if (dictionary2.ContainsKey("superFuelBought"))
								{
									flag = Convert.ToBoolean(dictionary2["superFuelBought"]);
								}
								if (dictionary2.ContainsKey("tournamentId"))
								{
									tournamentId = (string)dictionary2["tournamentId"];
								}
								if (dictionary2.ContainsKey("claimed"))
								{
									flag2 = Convert.ToBoolean(dictionary2["claimed"]);
								}
								if (dictionary2.ContainsKey("youtubeNitrosClaimed"))
								{
									flag3 = Convert.ToBoolean(dictionary2["youtubeNitrosClaimed"]);
								}
							}
							if (PsMetagameManager.m_tournaments.Count > 0)
							{
								PsMetagameManager.m_activeTournament = PsMetagameManager.m_tournaments.Find((EventMessage c) => c.tournament.tournamentId == tournamentId);
							}
							if (PsMetagameManager.m_activeTournament != null)
							{
								PsMetagameManager.m_activeTournament.tournament.joined = true;
								PsMetagameManager.m_activeTournament.tournament.room = num5;
								PsMetagameManager.m_activeTournament.tournament.time = num6;
								PsMetagameManager.m_activeTournament.tournament.hasSuperFuel = flag;
								PsMetagameManager.m_activeTournament.tournament.claimed = flag2;
								PsMetagameManager.m_activeTournament.tournament.youtubeNitrosClaimed = flag3;
							}
						}
						if ((PsMetagameManager.m_activeTournament == null || PsMetagameManager.m_activeTournament.tournament.claimed) && PsMetagameManager.m_tournaments.Count > 0)
						{
							EventMessage eventMessage3 = PsMetagameManager.m_tournaments.Find((EventMessage c) => (double)c.localEndTime > Main.m_EPOCHSeconds);
							if (eventMessage3 != null)
							{
								PsMetagameManager.m_activeTournament = eventMessage3;
							}
						}
						PsMetagameManager.SetLastLoginInfo();
						LoginFlow.m_userDataLoaded = true;
						if (serverResponse.ContainsKey("planetVersions"))
						{
							PlanetTools.SetServerVersions(serverResponse["planetVersions"] as List<object>);
						}
						else
						{
							Debug.LogError("No planetversions returned from server, this is bad.");
						}
						string text4 = ((PsPlanetManager.GetCurrentPlanet() == null) ? null : PsPlanetManager.GetCurrentPlanet().GetIdentifier());
						LoginFlow.LoadPlanetInitialData(delegate
						{
							LoginFlow.LoadData(serverResponse, delegate
							{
								_okCallback.Invoke(_c);
							});
						}, text4);
						PsUrlLaunch.Launch();
						PsIAPManager.CheckItemAvailability();
					}
				}
			}
			else
			{
				_failCallback.Invoke(_c);
			}
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x001516AC File Offset: 0x0014FAAC
		private static void LoadPlanetInitialData(Action _callback, string _identifier = null)
		{
			if (_identifier == null)
			{
				_identifier = PsMainMenuState.GetCurrentPlanetIdentifier();
			}
			PsMetagameData.Clear();
			PlanetTools.LoadLocalPlanets();
			PlanetTools.LoadAll(_callback);
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x001516CC File Offset: 0x0014FACC
		private static void ClearServerQueueItems()
		{
			if (PsMetagameManager.m_serverQueueItems != null)
			{
				for (int i = 0; i < PsMetagameManager.m_serverQueueItems.Count; i++)
				{
					PsMetagameManager.m_serverQueueItems[i] = null;
				}
				PsMetagameManager.m_serverQueueItems.Clear();
			}
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x00151714 File Offset: 0x0014FB14
		public static void Restart()
		{
			HttpS.ClearAllRequests(null);
			LoginFlow.ClearServerQueueItems();
			Main.m_currentGame.m_sceneManager.ChangeScene(new StartupScene("StartupScene"), new FadeLoadingScene(Color.black, true, 0.25f));
		}

		// Token: 0x04002059 RID: 8281
		private static bool m_debugReset;

		// Token: 0x0400205A RID: 8282
		private static bool m_firstTimeLoginBegin = true;

		// Token: 0x0400205B RID: 8283
		private static bool m_firstTimeLoginFinished = true;

		// Token: 0x0400205C RID: 8284
		public static bool m_userDataLoaded;

		// Token: 0x0400205D RID: 8285
		private static string[] m_helpshiftTags;

		// Token: 0x0400205E RID: 8286
		private static bool m_isCompeleted;
	}
}
