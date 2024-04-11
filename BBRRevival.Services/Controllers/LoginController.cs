﻿using BBRRevival.Services.Helpers;
using BBRRevival.Services.Routing;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Services.Controllers
{
    public class LoginController : Controller
    {
        [Route("POST", "/v4/player/login")]
        public async void CheckVersion()
        {
            Log.Information("Received Login Request");
            byte[] data = null;

            string Query = _request.Url.Query;
            string param = Query.Split("&")[0];
            int value = Convert.ToInt32(param.Split("=")[1]);

            string sessionId = sessionsManager.AddNewSessionId();

            Dictionary<string, object> dict = new Dictionary<string, object>();

            if (value == 0)
            {
                Log.Verbose("New User Creation");

                dict.Add("PLAY_STATUS", "OK");
                dict.Add("sessionId", "IdkWhatSessionThisIsFor");
                dict.Add("clientVersion", 1);
                dict.Add("versionInfo", "1");

                Dictionary<string, object> settings = new Dictionary<string, object>();

                settings.Add("superLikeRefreshMinutes", 12);
                settings.Add("carRefreshMinutes", 6);
                settings.Add("freshFreeInterval", 15);
                settings.Add("fbConnectReward", 20);
                settings.Add("dailyGemAmount", 10);
                settings.Add("videoAdCount", 2);
                settings.Add("videoAdCoolDown", 3600);
                settings.Add("freshFreeCount", 3);
                settings.Add("freshFreeCoolDown", 1800);
                settings.Add("inRaceDiamondSpawnProbability", 25);
                settings.Add("boltsAtStart", 0);
                settings.Add("coinsAtStart", 1000);
                settings.Add("diamondsAtStart", 50);
                settings.Add("keysAtStart", 5);
                settings.Add("offerCooldownMinutes", 4320);
                settings.Add("offerDurationMinutes", 60);
                settings.Add("minimumTournamentNitros", 5);
                settings.Add("creatorRank1", 100);
                settings.Add("creatorRank2", 1000);
                settings.Add("creatorRank3", 10000);
                settings.Add("creatorRank4", 100000);
                settings.Add("creatorRank5", 1000000);
                settings.Add("creatorRank6", 10000000);
                settings.Add("triesForAd", 0);
                settings.Add("triesForGems", 0);
                settings.Add("triesGemPrice", 0);

                Dictionary<string, object> tdict = new Dictionary<string, object>();
                tdict.Add("tournamentId", "testTournamentID");
                tdict.Add("minigameId", "testMinigameID");
                tdict.Add("ccCap", 12.4f);
                tdict.Add("prizeCoins", 999);
                tdict.Add("acceptingNewScores", true);
                tdict.Add("ownerId", "testOwnerId");
                tdict.Add("ownerName", "testOwnerName");
                tdict.Add("claimed", false);

                List<Dictionary<string, object>> turis = new List<Dictionary<string, object>>
                    {
                        new Dictionary<string, object> { { "uri", "http://example.com/1" } },
                        new Dictionary<string, object> { { "uri", "http://example.com/2" } },
                        new Dictionary<string, object> { { "uri", "http://example.com/3" } }
                    };

                Dictionary<string, object> pdict = new Dictionary<string, object>();
                pdict.Add("planet", "Adventure");
                pdict.Add("version", 2);

                //add tournament things, looks like it is neccesary to do this idk why
                Dictionary<string, object> edict = new Dictionary<string, object>();
                edict.Add("eventName", "TestEvemt");
                edict.Add("eventType", "Tournament");
                edict.Add("messageId", "testMessageID");
                edict.Add("id", "123456");
                edict.Add("header", "eventHeader");
                edict.Add("message", "eventMessage");
                edict.Add("label", "eventLabel");
                edict.Add("popup", true);
                edict.Add("newsFeed", true);
                edict.Add("startTime", Convert.ToInt64(DateTime.Now.Ticks));
                edict.Add("endTime", Convert.ToInt64(new DateTime(2025, 12, 4).Ticks));
                edict.Add("eventData", tdict);
                edict.Add("uris", turis);

                dict.Add("playerId", "123456789");
                dict.Add("developer", true);
                dict.Add("countryCode", "386");
                dict.Add("clientConfig", settings);
                dict.Add("teamid", "0");
                dict.Add("teamName", "BigTeamNameIDK");
                //dict.Add("teamRole", Enums.TeamRole.Creator.ToString());
                dict.Add("hasJoinedTeam", true);
                dict.Add("youtubeName", "TempYtName");
                dict.Add("youtubeId", "ytid");

                dict.Add("eventMessage", edict);
                dict.Add("eventList", new List<object> { edict });
                dict.Add("activeTournament", tdict);

                //dict.Add("playerId", Guid.NewGuid().ToString());
                dict.Add("name", "TempUserName");
                dict.Add("tag", "TempUserTag");
                dict.Add("itemDbVersion", 0);
                dict.Add("AcceptNotifications", true);
                dict.Add("nameChangesDone", 0);

                dict.Add("planetVersions", new List<object> { pdict });

                data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dict));
            }
            else
            {
                Log.Verbose($"Value is {value}");
            }

            Log.Verbose("All Headers:");
            foreach (string item in _request.Headers)
            {
                Log.Verbose(item);
            }

            //send the request
            ResponseHelper.PrepareRequest(data, RawUrl, _response, _request);
            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }
    }
}
