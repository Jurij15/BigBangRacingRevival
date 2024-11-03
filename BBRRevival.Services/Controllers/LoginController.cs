using BBRRevival.Services.API;
using BBRRevival.Services.Helpers;
using BBRRevival.Services.Routing;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Services.Controllers
{
    public class LoginController : Controller
    {
        [Route("POST", "/v4/player/login")]
        public async void PlayerLogin()
        {
            Log.Information("Received Login Request");
            byte[] data = null;

            string Query = _request.Url.Query;
            string param = Query.Split("&")[0];
            string value = param.Split("=")[1];

            string sessionId = sessionsManager.AddNewSessionId();

            Dictionary<string, object> dict = new Dictionary<string, object>();

            if (true) //check value == "0", setting it to true for now
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
                tdict.Add("tournamentId", "d1a79cbc63964a18a6ba05f11d5df82b");
                tdict.Add("minigameId", "d1a79cbc63964a18a6ba05f11d5df82b");
                tdict.Add("ccCap", 1200f);
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

                Dictionary<string, object> adpdict = new Dictionary<string, object>();
                adpdict.Add("planet", "AdventureMotorcycle");
                adpdict.Add("version", 2);

                Dictionary<string, object> tpdict = new Dictionary<string, object>(); //add in another one that is not adventure, maybe it will hit the callback
                tpdict.Add("planet", "RacingOffroadCar");
                tpdict.Add("version", 2);

                Dictionary<string, object> ofcarpdict = new Dictionary<string, object>(); //add in another one that is not adventure, maybe it will hit the callback
                ofcarpdict.Add("planet", "AdventureOffroadCar");
                ofcarpdict.Add("version", 2);

                Dictionary<string, object> rmcarpdict = new Dictionary<string, object>(); //add in another one that is not adventure, maybe it will hit the callback
                rmcarpdict.Add("planet", "RacingMotorcycle");
                rmcarpdict.Add("version", 2);

                Dictionary<string, object> metadatadict = new Dictionary<string, object>(); //add in another one that is not adventure, maybe it will hit the callback
                metadatadict.Add("planet", "Metadata");
                metadatadict.Add("version", 2);

                //planet paths, neccesary, kt crashes without them for some reason
                List<object> nodes = new List<object>();
                Dictionary<string, object> nodesdict = new Dictionary<string, object>();

                nodesdict.Add("id", "1234");
                nodesdict.Add("levelNumber", "12345");
                nodesdict.Add("score", "123456");

                nodes.Add(nodesdict);

                List<object> paths = new List<object>();
                Dictionary<string, object> path = new Dictionary<string, object>();
                path.Add("name", "MainPath");
                path.Add("currentNode", "5");
                //path.Add("type", "1");
                path.Add("nodes", nodes);
                path.Add("startNode", "1");
                path.Add("planet", "AdventureOffroadCar");

                paths.Add(path);

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

                //ill move these keys to another dictionary
                Dictionary<string, object> dataload = new Dictionary<string, object>();
                dataload.Add("sessionExpiration", false);
                dataload.Add("adsConfig", new List<object>());
                dataload.Add("seasonConfig", new Dictionary<string, object>());
                dataload.Add("gemPriceConfig", new Dictionary<string, object>());
                dataload.Add("editorConfig", new Dictionary<string, object>());
                dataload.Add("achievements", new List<object>());
                dataload.Add("numOfPurchases", 10);
                dataload.Add("bossBattleConfig", new Dictionary<string, object>());
                dataload.Add("numOfSessions", 11);
                dataload.Add("publishedMinigameCount", 11);
                dataload.Add("tournamentRewardShares", new List<object>());
                dataload.Add("lastPathSync", "what");

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
                dict.Add("name", "Jurij15");
                dict.Add("tag", "JurijG");
                dict.Add("itemDbVersion", 0);
                dict.Add("AcceptNotifications", true);
                dict.Add("nameChangesDone", 0);
                dict.Add("level", 100);

                dict.Add("mcRank", 4000);
                dict.Add("carRank", 4000);
                dict.Add("mcTrophies", 21000);
                dict.Add("carTrophies", 21000);

                dict.Add("coins", 1000000);
                dict.Add("diamonds", 1000000);
                dict.Add("copper", 1000000);
                dict.Add("shards", 1000000);

                dict.Add("xp", 100000);
                dict.Add("totalLikes", 100000);
                dict.Add("totalCoinsEarned", 100000);
                dict.Add("completedAdventures", 999);
                dict.Add("reward", 111);
                dict.Add("dirtBikeBundle", true);
                //Adding Hats and Trails.

                dict.Add("MotorcycleVisual", new Dictionary<string, object>());

                var motorcycleVisual = (Dictionary<string, object>)dict["MotorcycleVisual"];
                motorcycleVisual.Add("PaperBag", false);
                motorcycleVisual.Add("OrangeHat", false);
                motorcycleVisual.Add("WinterHat", false);
                motorcycleVisual.Add("PinkHat", false);
                motorcycleVisual.Add("ReindeerHat", false);
                motorcycleVisual.Add("HawkMask", false);
                motorcycleVisual.Add("MrBaconHair", false);
                motorcycleVisual.Add("HelmetGolden", false);
                motorcycleVisual.Add("LorpHeadband", false);
                motorcycleVisual.Add("GoldenShades", false);
                motorcycleVisual.Add("KnightHelmet", false);
                motorcycleVisual.Add("Mask", false);
                motorcycleVisual.Add("PumpkinHat", false);
                motorcycleVisual.Add("PilotHat", false);
                motorcycleVisual.Add("HorseHead", false);
                motorcycleVisual.Add("WitchHat", false);
                motorcycleVisual.Add("GirlyHair", false);
                motorcycleVisual.Add("BuilderHat", false);
                motorcycleVisual.Add("IceCreamHat", false);
                motorcycleVisual.Add("UnicornMask", false);
                motorcycleVisual.Add("GoldenCarHelmet", true);
                motorcycleVisual.Add("LovelyHat", false);
                motorcycleVisual.Add("AnniversaryCandleHat", false);
                motorcycleVisual.Add("AnniversaryPartyHat", false);
                motorcycleVisual.Add("AnglerFishHat", false);
                motorcycleVisual.Add("BarbarianHelmet", false);
                motorcycleVisual.Add("BaseballHat", false);
                motorcycleVisual.Add("BobbleHat", false);
                motorcycleVisual.Add("BootHat", false);
                motorcycleVisual.Add("CatHat", false);
                motorcycleVisual.Add("CowboyHat", false);
                motorcycleVisual.Add("SteelMask", false);
                motorcycleVisual.Add("TimeTravellerHat", false);
                motorcycleVisual.Add("Helmet", false);
                motorcycleVisual.Add("MotocrossHelmet", false);
                motorcycleVisual.Add("VR", false);
                motorcycleVisual.Add("Fish", false);
                motorcycleVisual.Add("MilkJugHat", false);
                motorcycleVisual.Add("DealWithItGlasses", false);
                motorcycleVisual.Add("ReversalCrown", false);
                motorcycleVisual.Add("ToadHat", false);
                motorcycleVisual.Add("PowerHelmet", false);
                motorcycleVisual.Add("MushroomHat", false);
                motorcycleVisual.Add("FishHat", false);
                motorcycleVisual.Add("WerewolfMask", false);
                motorcycleVisual.Add("trail_anniversary", false);
                motorcycleVisual.Add("trail_bubble", false);
                motorcycleVisual.Add("trail_cash", false);
                motorcycleVisual.Add("trail_death", true);
                motorcycleVisual.Add("trail_feather", false);
                motorcycleVisual.Add("trail_fire", false);
                motorcycleVisual.Add("trail_kittypaw", false);
                motorcycleVisual.Add("trail_rainbow", false);
                motorcycleVisual.Add("trail_singular", false);
                motorcycleVisual.Add("trail_snow", false);
                motorcycleVisual.Add("trail_scifi", true);
                motorcycleVisual.Add("trail_bat", false);
                motorcycleVisual.Add("ChickenHat", false);
                motorcycleVisual.Add("WinterCap", false);

                // The Trail Values seem to be quite easy to add.
                dict.Add("OffroadCarVisual", new Dictionary<string, object>());

                var offroadCarVisual = (Dictionary<string, object>)dict["OffroadCarVisual"];
                offroadCarVisual.Add("PaperBag", false);
                offroadCarVisual.Add("OrangeHat", false);
                offroadCarVisual.Add("WinterHat", false);
                offroadCarVisual.Add("PinkHat", false);
                offroadCarVisual.Add("ReindeerHat", false);
                offroadCarVisual.Add("HawkMask", false);
                offroadCarVisual.Add("MrBaconHair", false);
                offroadCarVisual.Add("RobotHat", false);
                offroadCarVisual.Add("LorpHeadband", false);
                offroadCarVisual.Add("GoldenShades", false);
                offroadCarVisual.Add("KnightHelmet", false);
                offroadCarVisual.Add("Mask", false);
                offroadCarVisual.Add("PumpkinHat", false);
                offroadCarVisual.Add("PilotHat", false);
                offroadCarVisual.Add("HorseHead", false);
                offroadCarVisual.Add("WitchHat", false);
                offroadCarVisual.Add("GirlyHair", false);
                offroadCarVisual.Add("BuilderHat", false);
                offroadCarVisual.Add("IceCreamHat", false);
                offroadCarVisual.Add("UnicornMask", false);
                offroadCarVisual.Add("GoldenCarHelmet", true);
                offroadCarVisual.Add("LovelyHat", false);
                offroadCarVisual.Add("AnniversaryCandleHat", false);
                offroadCarVisual.Add("AnniversaryPartyHat", false);
                offroadCarVisual.Add("AnglerFishHat", false);
                offroadCarVisual.Add("BarbarianHelmet", false);
                offroadCarVisual.Add("BaseballHat", false);
                offroadCarVisual.Add("BobbleHat", false);
                offroadCarVisual.Add("BootHat", false);
                offroadCarVisual.Add("CatHat", false);
                offroadCarVisual.Add("CowboyHat", false);
                offroadCarVisual.Add("SteelMask", false);
                offroadCarVisual.Add("TimeTravellerHat", false);
                offroadCarVisual.Add("Helmet", false);
                offroadCarVisual.Add("MotocrossHelmet", false);
                offroadCarVisual.Add("VR", false);
                offroadCarVisual.Add("Fish", false);
                offroadCarVisual.Add("MilkJugHat", false);
                offroadCarVisual.Add("DealWithItGlasses", false);
                offroadCarVisual.Add("ReversalCrown", false);
                offroadCarVisual.Add("ToadHat", false);
                offroadCarVisual.Add("PowerHelmet", false);
                offroadCarVisual.Add("MushroomHat", false);
                offroadCarVisual.Add("FishHat", false);
                offroadCarVisual.Add("WerewolfMask", false);
                offroadCarVisual.Add("trail_anniversary", false);
                offroadCarVisual.Add("trail_bubble", false);
                offroadCarVisual.Add("trail_cash", false);
                offroadCarVisual.Add("trail_death", true);
                offroadCarVisual.Add("trail_feather", false);
                offroadCarVisual.Add("trail_fire", false);
                offroadCarVisual.Add("trail_kittypaw", false);
                offroadCarVisual.Add("trail_rainbow", false);
                offroadCarVisual.Add("trail_singular", false);
                offroadCarVisual.Add("trail_snow", false);
                offroadCarVisual.Add("trail_scifi", true);
                offroadCarVisual.Add("trail_bat", false);
                offroadCarVisual.Add("ChickenHat", false);
                offroadCarVisual.Add("WinterCap", false);

                dict.Add("OffroadCarUpgrades", new Dictionary<string, object>());
                //offroader upgrades
                var UpgradeDict = (Dictionary<string, object>)dict["OffroadCarUpgrades"];
                UpgradeDict.Add("CarGrip1", "2000");
                UpgradeDict.Add("CarGrip2", "2000");
                UpgradeDict.Add("CarGrip3", "2000");
                UpgradeDict.Add("CarGrip4", "2000");
                UpgradeDict.Add("CarGrip5", "2000");
                UpgradeDict.Add("CarGrip6", "2000");
                UpgradeDict.Add("CarSpeed1", "2000");
                UpgradeDict.Add("CarSpeed2", "2000");
                UpgradeDict.Add("CarSpeed3", "2000");
                UpgradeDict.Add("CarSpeed4", "2000");
                UpgradeDict.Add("CarSpeed5", "2000");
                UpgradeDict.Add("CarSpeed6", "2000");
                UpgradeDict.Add("CarHandling1", "2000");
                UpgradeDict.Add("CarHandling2", "2000");
                UpgradeDict.Add("CarHandling3", "2000");
                UpgradeDict.Add("CarHandling4", "2000");
                UpgradeDict.Add("CarHandling5", "2000");
                UpgradeDict.Add("CarHandling6", "2000");
                UpgradeDict.Add("CarSpecial1", "2000");
                UpgradeDict.Add("CarSpecial2", "2000");
                UpgradeDict.Add("CarSpecial3", "2000");
                UpgradeDict.Add("CarSpecial4", "2000");
                UpgradeDict.Add("CarSpecial5", "2000");
                UpgradeDict.Add("CarSpecial6", "2000");

                dict.Add("MotorcycleUpgrades", new Dictionary<string, object>());
                //offroader upgrades
                var MotorcycleUpgrade = (Dictionary<string, object>)dict["MotorcycleUpgrades"];
                MotorcycleUpgrade.Add("Grip1", "2000");
                MotorcycleUpgrade.Add("Grip2", "2000");
                MotorcycleUpgrade.Add("Grip3", "2000");
                MotorcycleUpgrade.Add("Grip4", "2000");
                MotorcycleUpgrade.Add("Grip5", "2000");
                MotorcycleUpgrade.Add("Grip6", "2000");
                MotorcycleUpgrade.Add("Speed1", "2000");
                MotorcycleUpgrade.Add("Speed2", "2000");
                MotorcycleUpgrade.Add("Speed3", "2000");
                MotorcycleUpgrade.Add("Speed4", "2000");
                MotorcycleUpgrade.Add("Speed5", "2000");
                MotorcycleUpgrade.Add("Speed6", "2000");
                MotorcycleUpgrade.Add("Handling1", "2000");
                MotorcycleUpgrade.Add("Handling2", "2000");
                MotorcycleUpgrade.Add("Handling3", "2000");
                MotorcycleUpgrade.Add("Handling4", "2000");
                MotorcycleUpgrade.Add("Handling5", "2000");
                MotorcycleUpgrade.Add("Handling6", "2000");
                MotorcycleUpgrade.Add("Special1", "2000");
                MotorcycleUpgrade.Add("Special2", "2000");
                MotorcycleUpgrade.Add("Special3", "2000");
                MotorcycleUpgrade.Add("Special4", "2000");
                MotorcycleUpgrade.Add("Special5", "2000");
                MotorcycleUpgrade.Add("Special6", "2000");

                dict.Add("planetVersions", new List<object> { adpdict, tpdict, ofcarpdict, rmcarpdict, metadatadict });

                dict.Add("paths", paths);

                //initialise shop dict
                dict.Add("iapConfig", new Dictionary<string, object>());

                var ShopData = (Dictionary<string, object>)dict["iapConfig"];
                ShopData.Add("debugLevel", 1);
                ShopData.Add("items", new List<object>());

                List<object> list = ShopData["items"] as List<object>;

                //i dont know the values for any of these keys. Setting them to random values.
                Dictionary<string, object> shopItems = new Dictionary<string, object>();
                shopItems.Add("identifier", "24662");
                shopItems.Add("androidIdentifier", "24662");
                shopItems.Add("resource", "bundle");
                shopItems.Add("amount", 999);
                shopItems.Add("visible", true);
                shopItems.Add("order", 1);
                shopItems.Add("sticker", "what");
                shopItems.Add("bundle", new Dictionary<string, object>());//bundle dict

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

        [Route("POST", "/v2/player/data/change")]
        public async void SetPlayerData()
        {
            byte[] data = null;

            Dictionary<string, object> path = new Dictionary<string, object>();
            path.Add("lastPathSync", "what");

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(path));

            Console.WriteLine("request headers");
            foreach (var item in _request.Headers)
            {
                Console.WriteLine(item);
            }


            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("GET", "/v2/player/changeName")]
        public async void ChangeName()
        {
            byte[] data = null;

            Dictionary<string, object> path = new Dictionary<string, object>();

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(path));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("GET", "/v1/player/friends")]
        public async void GetFriends()
        {
            byte[] data = null;

            Dictionary<string, object> friends = new Dictionary<string, object>();
            friends.Add("followees", new List<object>());
            friends.Add("friends", new List<object>());
            friends.Add("followers", new List<object>());

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(friends));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request, true);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("POST", "/v1/player/openChest")]
        public async void OpenChest()
        {
            byte[] data = null;

            Dictionary<string, object> OpenChestData = new Dictionary<string, object>();
            OpenChestData.Add("SetData", new List<object>());

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(OpenChestData));

            Console.WriteLine("request headers");
            foreach (var item in _request.Headers)
            {
                Console.WriteLine(item);
            }

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("GET", "/v1/tournament/score/get")]
        public async void GetLeaderboard()
        {
            byte[] data = null;

            Dictionary<string, object> TournamentLeaderboard = new Dictionary<string, object>();
            TournamentLeaderboard.Add("globalParticipants", 0);
            TournamentLeaderboard.Add("acceptingNewScores", true);
            TournamentLeaderboard.Add("globalNitroPot", 21);
            TournamentLeaderboard.Add("roomNitroPot", 21);
            TournamentLeaderboard.Add("ownerTime", 11);
            TournamentLeaderboard.Add("room", 1);
            TournamentLeaderboard.Add("roomCount", 1);

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(TournamentLeaderboard));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request, true);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        //Gotta try move this function somewhere else... LoginController Is getting too big.

        [Route("GET", "/v1/global/chat/find")]
        public async void GetComments()
        {
            byte[] data = null;

            Dictionary<string, object> commentData = new Dictionary<string, object>();
            commentData.Add("data", new List<object>());

            List<object> list = commentData["data"] as List<object>;

            Dictionary<string, object> commentData2 = new Dictionary<string, object>();
            commentData2.Add("playerId", "123456798");
            commentData2.Add("comment", "placeholdertext");
            commentData2.Add("message", "inserttext");
            commentData2.Add("name", "testguy1");
            commentData2.Add("tag", "tg1");
            commentData2.Add("facebookId", "f612dg82dq87");
            commentData2.Add("gameCenterId", "g37gr74g73428");
            commentData2.Add("admin", true);
            commentData2.Add("publishTime", new Dictionary<string, object>());

            Dictionary<string, object> commentData3 = (Dictionary<string, object>)commentData2["publishTime"];
            commentData3.Add("$date", 1518220800000L);

            commentData2.Add("type", "whatdoesthismean");
            commentData2.Add("customData", new Dictionary<string, object>());

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(commentData));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request, true);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("POST", "/v1/global/chat/save")]
        public async void SaveComment()
        {
            byte[] data = null;

            Dictionary<string, object> savecommentData = new Dictionary<string, object>();

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(savecommentData));



            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request, true);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        //unfinished, have to add leaderboardentrylist

        [Route("GET", "/v1/trophy/leaderboard")]
        public async void GetTrophyLeaderboard()
        {
            byte[] data = null;

            Dictionary<string, object> ParseLeaderBoard = new Dictionary<string, object>();
            ParseLeaderBoard.Add("global", new List<object>());

            List<object> leaderboard1 = ParseLeaderBoard["global"] as List<object>;

            Dictionary<string, object> person1 = new Dictionary<string, object>(); //this dict makes me add the parseplayerdata keys here. I will add it later because its too long
            person1.Add("id", "20103204");
            person1.Add("name", "Surge");
            person1.Add("trophies", 2000000000);
            person1.Add("time", 100);

            leaderboard1.Add(person1);

            ParseLeaderBoard.Add("local", new List<object>());
            ParseLeaderBoard.Add("friend", new List<object>());

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(ParseLeaderBoard));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("GET", "/v1/event/feed")]

        public async void GetFeed()
        {
            byte[] data = null;

            Dictionary<string, object> FeedData = new Dictionary<string, object>();
            FeedData.Add("data", new List<object>());

            List<object> FeedList = FeedData["data"] as List<object>;

            Dictionary<string, object> FeedDict = new Dictionary<string, object>();
            FeedDict.Add("eventName", "Hello!");
            FeedDict.Add("eventType", "Special");
            FeedDict.Add("id", "369852140");
            FeedDict.Add("header", "Attention Racers!");
            FeedDict.Add("message", "It is with heavy hearts that we must announce the following: Big Bang Racing will be having the final BIg Bang on the 31st of May, 2022.This is due to critical issues that will prevent the game from working for much longer. The game is avaliable in the App and Play Storeup untill the 30th of April, 2022.");
            FeedDict.Add("label", "what");
            FeedDict.Add("popup", true);
            FeedDict.Add("floatingNode", false);
            FeedDict.Add("newsFeed", true);

            FeedList.Add(FeedDict);

            Dictionary<string, object> FeedDict2 = new Dictionary<string, object>();
            FeedDict2.Add("eventName", "Hello2!");
            FeedDict2.Add("eventType", "Special");
            FeedDict2.Add("id", "369852132");
            FeedDict2.Add("header", "sihdij");
            FeedDict2.Add("message", "testmessage");
            FeedDict2.Add("label", "what");
            FeedDict2.Add("popup", false);
            FeedDict2.Add("floatingNode", false);
            FeedDict2.Add("newsFeed", true);

            FeedList.Add(FeedDict2);

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(FeedData));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("GET", "/v1/creator/top")]
        public async void GetTopCreators()
        {
            byte[] data = null;

            Dictionary<string, object> ParseCreatorLeaderboard = new Dictionary<string, object>();
            ParseCreatorLeaderboard.Add("leaderboard", new List<object>());

            List<object> CreatorLdb1 = ParseCreatorLeaderboard["leaderboard"] as List<object>;

            Dictionary<string, object> person1 = new Dictionary<string, object>(); //this dict makes me add the parseplayerdata keys here. I will add it later because its too long
            person1.Add("id", "24687531");
            person1.Add("name", "Dodo Nickey");
            person1.Add("tag", "dodonickey");
            person1.Add("acceptNotifications", true);
            person1.Add("facebookId", "uhihv67g9onob");
            person1.Add("gameCenterId", "g76f7g8p9j8t33");
            person1.Add("ninjaCreationTimestamp", "what");
            person1.Add("countryCode", "284");
            person1.Add("itemDbVersion", 0);
            person1.Add("publishedMinigameCount", 2);
            person1.Add("followerCount", 230000);
            person1.Add("totalCoinsEarned", 100000000);
            person1.Add("totalLikes", 100000000);
            person1.Add("totalSuperLikes", 100000000);
            person1.Add("mcTrophies", 100000000);
            person1.Add("carTrophies", 100000000);
            person1.Add("bigBangPoints", 100000000);
            person1.Add("completedAdventures", 100000000);
            person1.Add("racesWon", 100000000);
            person1.Add("teamId", "ff1du7");
            person1.Add("teamName", "SYS64738");
            person1.Add("teamRole", "Creator");
            person1.Add("hasJoinedTeam", true);
            person1.Add("reward", 100);
            person1.Add("lastSeasonEndCarTrophies", 100000000);
            person1.Add("lastSeasonEndMcTrophies", 100000000);
            person1.Add("racesThisSeason", 100000000);
            person1.Add("completedSurvey", false);
            person1.Add("gender", "Male");
            person1.Add("ageGroup", "24");
            person1.Add("developer", true);

            CreatorLdb1.Add(person1);

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(ParseCreatorLeaderboard));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("GET", "/v1/minigame/followee/published")]
        public async void GetFolloweeLevels()
        {
            byte[] data = null;

            Dictionary<string, object> FolloweeLevels = new Dictionary<string, object>();
            FolloweeLevels.Add("data", new List<object>());

            List<object> Followee = FolloweeLevels["data"] as List<object>;

            Dictionary<string, object> Followeelevel = new Dictionary<string, object>();
            Followeelevel.Add("name", "Okay");
            Followeelevel.Add("id", "f6f765c6fc064338b4d28560eac2ccbf-workingPub");//change this with your levels metadata
            Followeelevel.Add("creatorId", "1238429");
            Followeelevel.Add("gameMode", "StarCollect");

            Followee.Add(Followeelevel);

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(FolloweeLevels));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }
    }
}