using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BBRRevival.Server
{
    internal class Program
    {
        public static HttpListener listener;

        private static string ip = "http://192.168.1.7:4451/";

        static void Main(string[] args)
        {
            Console.WriteLine("Starting Server...");
            StartServer();
        }

        static void StartServer()
        {
            listener = new System.Net.HttpListener();
            listener.Prefixes.Add(ip);
            listener.Start();
            Console.WriteLine("Server Started!");

            Task listentask = HandleIncomingConnections();
            listentask.GetAwaiter().GetResult();
        }

        //this is how the hash data looks like before being hashed
        //the data here should be hashed? and the client compares it to its own hash
        //Either way, just call this function before sending anything back to the client ok?
        static void AddRequestHeaders(byte[] data, string rawUrl, HttpListenerResponse response, HttpListenerRequest request)
        {
            response.AddHeader("PLAY_STATUS", "OK");
            response.AddHeader("PLAY_HASH", Encoding.Default.GetString(data)+request.RawUrl + "bfid3Z53SFib325PJGFasae"); //server encryption key
        }

        public static async Task HandleIncomingConnections()
        {
            while (true)
            {
                HttpListenerContext ctx = await listener.GetContextAsync();

                HttpListenerRequest request = ctx.Request;
                HttpListenerResponse response = ctx.Response;

                string RequestURL = request.Url.ToString();
                Uri URL = request.Url;
                string RawUrl = request.RawUrl;
                string Query = request.Url.Query;

                Console.WriteLine("URL RECEIVED: "+RawUrl);

                if (RawUrl.Contains("/v1/preload/checkVersion"))
                {
                    //Console.WriteLine("Received CheckVersion Request");
                    byte[] data = null;
                    //TODO: actuall check the version

                    Dictionary<string, object> version = new Dictionary<string, object>();
                    version.Add("version", "upToDate");

                    data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(version));

                    AddRequestHeaders(data, RawUrl, response, request);

                    response.ContentType = "application/octet-stream";

                    await response.OutputStream.WriteAsync(data, 0, data.Length);

                    response.Close();
                }

                if(RawUrl.Contains("/v1/preload/checkFile"))
                {
                    byte[] data = null;
                    //TODO: actuall check the version

                    //Console.WriteLine(Query);

                    string name = Query.Split("&")[1].Remove(0, 5);

                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    dict.Add("PLAY_STATUS", "OK");
                    dict.Add("name", name);
                    dict.Add("type", "idkWhatToPutHere");
                    dict.Add("path", $"{ip}downloadFile?{name}"); //this is the adress to download the music bank
                    dict.Add("version", MusicBankVersion.GetLatestMusicVersion(name)); //maybe?

                    //Console.WriteLine("ADRESS: " + $"{ip}downloadFile ?{name}");

                    data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(dict));

                    AddRequestHeaders(data, RawUrl, response, request);

                    response.ContentType = "application/octet-stream";

                    await response.OutputStream.WriteAsync(data, 0, data.Length);
                    response.Close();
                }

                if (RawUrl.Contains("downloadFile"))
                {
                    byte[] data = null;

                    //QUERY DOES NOT CONTAIN FILE ENDING (.BANK!)

                    //Console.WriteLine("QUERY: "+Query);

                    string name = Query;

                    data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(""));

                    AddRequestHeaders(data, RawUrl, response, request);

                    response.ContentType = "application/octet-stream";

                    await response.OutputStream.WriteAsync(data, 0, data.Length);
                    response.Close();
                }

                if (RawUrl.Contains("v4/player/login?lastPathSync")) // it should be a post request
                {
                    byte[] data = null;

                    string query = Query.Split("?")[1];
                    string param = query.Split("&")[0];

                    int value = Convert.ToInt32(param.Split("=")[1]);

                    if (value == 0)
                    {
                        Console.WriteLine("New User Created?");
                    }

                    Dictionary<string, object> dict = new Dictionary<string, object>();
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

                    //settings.Add("name", "value");

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
                    pdict.Add("planet", "Tutorial");
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

                    PlayerData pdata = new PlayerData(); //just make some temporary player data
                    dict.Add("playerId", "123456789");
                    dict.Add("developer", true);
                    dict.Add("countryCode", "386");
                    dict.Add("clientConfig", settings);
                    dict.Add("teamid", "0");
                    dict.Add("teamName", "BigTeamNameIDK");
                    dict.Add("teamRole", Enums.TeamRole.Creator.ToString());
                    dict.Add("hasJoinedTeam", true);
                    dict.Add("youtubeName", "TempYtName");
                    dict.Add("youtubeId", "ytid");

                    dict.Add("eventMessage", edict);
                    dict.Add("eventList", new List<object> { edict});
                    dict.Add("activeTournament", tdict);
                    
                    //dict.Add("playerId", Guid.NewGuid().ToString());
                    dict.Add("name", "TempUserName");
                    dict.Add("tag", "TempUserTag");
                    dict.Add("itemDbVersion", 0);
                    dict.Add("AcceptNotifications", true);
                    dict.Add("nameChangesDone", 0);

                    dict.Add("planetVersions", new List<object> { pdict });


                    data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(dict));

                    Console.WriteLine(JsonConvert.SerializeObject(dict));

                    AddRequestHeaders(data, RawUrl, response, request);
                    response.ContentType = "application/octet-stream";

                    //Console.WriteLine(response.Headers[1]);

                    await response.OutputStream.WriteAsync(data, 0, data.Length);
                    response.Close();
                }
            }
        }
    }
}
