using BBRRevival.Common.Enums;
using BBRRevival.Common.Model;
using BBRRevival.Common.Responses.ResponsesModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Common.Responses.Player
{
    public class PlayerLoginResponse
    {
        public class ClientConfig
        {
            public int carRefreshMinutes { get; set; }
            public int freshFreeInterval { get; set; }
            public int keysAtStart { get; set; }
            public int diamondsAtStart { get; set; }
            public int coinsAtStart { get; set; }
            public int boltsAtStart { get; set; }
            public int fbConnectReward { get; set; }
            public int dailyGemAmount { get; set; }
            public int videoAdCount { get; set; }
            public int videoAdCoolDown { get; set; }
            public int freshFreeCount { get; set; }
            public int freshFreeCoolDown { get; set; }
            public int inRaceDiamondSpawnProbability { get; set; }
            public int superLikeRefreshMinutes { get; set; }
            public int offerCooldownMinutes { get; set; }
            public int offerDurationMinutes { get; set; }
            public int minimumTournamentNitros { get; set; }
            public int tournamentYoutuberFollowNitros { get; set; }
            public int creatorRank1 { get; set; }
            public int creatorRank2 { get; set; }
            public int creatorRank3 { get; set; }
            public int creatorRank4 { get; set; }
            public int creatorRank5 { get; set; }
            public int creatorRank6 { get; set; }

            public ClientConfig CreateFromModel(ClientConfigModel model)
            {
                carRefreshMinutes = model.CarRefreshMinutes;
                freshFreeInterval = model.FreshFreeInterval;
                keysAtStart = model.KeysAtStart;
                diamondsAtStart = model.DiamondsAtStart;
                coinsAtStart = model.CoinsAtStart;
                boltsAtStart = model.BoltsAtStart;
                fbConnectReward = model.FbConnectReward;
                dailyGemAmount = model.DailyGemAmount;
                videoAdCount = model.VideoAdCount;
                videoAdCoolDown = model.VideoAdCoolDown;
                freshFreeCount = model.FreshFreeCount;
                freshFreeCoolDown = model.FreshFreeCoolDown;
                inRaceDiamondSpawnProbability= model.InRaceDiamondSpawnProbability;
                superLikeRefreshMinutes = model.SuperLikeRefreshMinutes;
                offerCooldownMinutes = model.OfferCooldownMinutes;
                offerDurationMinutes = model.OfferDurationMinutes;
                minimumTournamentNitros = model.MinimumTournamentNitros;
                tournamentYoutuberFollowNitros = 5; //default for now
                creatorRank1 = model.CreatorRank1;
                creatorRank2 = model.CreatorRank2;
                creatorRank3 = model.CreatorRank3;
                creatorRank4 = model.CreatorRank4;
                creatorRank5 = model.CreatorRank5;
                creatorRank6 = model.CreatorRank6;

                return this;
            }
        }

        #region Construction
        public PlayerLoginResponse() { }

        public void AddPlayerData(PlayerModel model)
        {
            this.playerId = model.Id.ToString();
            this.name = model.Name;
            this.tag = model.Tag;
            if (model.GameCenterId is not null)
            {
                this.gameCenterId = model.GameCenterId;
            }
            else
            {
                this.gameCenterId = "noid";
            }
            if (model.FacebookId is not null)
            {
                this.facebookId = model.FacebookId;
            }
            else
            {
                this.facebookId = "noid";
            }
            if (model.NinjaCreationTimestamp is not null)
            {
                this.ninjaCreationTimestamp = model.NinjaCreationTimestamp;
            }
            this.countryCode = model.CountryCode;
            this.youtubeName = model.YoutubeName;
            this.youtubeId = model.YoutubeId;
            this.youtubeSubscriberCount = model.YoutubeSubscriberCount;
            this.itemDbVersion = model.ItemDbVersion;
            this.coins = model.Coins;
            this.copper = model.Copper;
            this.diamonds = model.Diamonds;
            this.shards = model.Shards;
            this.stars = model.Stars;
            this.mcBoosters = model.McBoosters;
            this.maxMcBoosters = model.MaxMcBoosters;
            this.tournamentBoosters = model.TournamentBoosters;
            this.carBoosters = model.CarBoosters;
            this.maxCarBoosters = model.MaxCarBoosters;
            this.itemLevel = model.ItemLevel;
            this.level = model.Level;
            this.cups = model.Cups;
            this.mcRank = model.McRank;
            this.carRank = model.VarRank;
            this.mcTrophies = model.McTrophies;
            this.carTrophies = model.CarTrophies;
            this.bigBangPoints = model.BigBangPoints;
            this.xp = model.Xp;
            this.mcHandicap = model.McHandicap;
            this.carHandicap = model.CarHandicap;
            this.cardPurchases = model.CardPurchases;
            this.gachaData = model.GachaData;
            this.teamId = model.TeamId;
            this.teamName = model.TeamName;
            this.teamRoleName = model.TeamRoleName;
            this.hasJoinedTeam = model.HasJoinedTeam;
            this.seasonReward = model.SeasonReward;
            this.teamKickReason = model.TeamKickReason;
            this.lastSeasonEndMcTrophies = model.LastSeasonEndMcTrophies;
            this.lastSeasonEndCarTrophies = model.LastSeasonEndCarTrophies;
            this.racesThisSeason = model.RacesThisSeason;
            this.ageGroup = model.AgeGroup;
            this.gender = model.Gender;
            this.completedSurvey = model.CompletedSurvey;
            //this.upgrades = model.Upgrades; // Conversion to Dictionary<string,object> needed
            this.boosters = model.Boosters;
            this.acceptNotifications = model.AcceptNotifications;
            this.mcBoosterRefreshTimeLeft = model.McBoosterRefreshTimeLeft;
            this.carBoosterRefreshTimeLeft = model.CarBoosterRefreshTimeLeft;
            this.superLikeRefreshTimeLeft = model.SuperLikeRefreshTimeLeft;
            this.editorResources = model.EditorResources;
            this.claimedTutorials = model.ClaimedTutorials;
            this.hash = model.Hash;
            this.publishedMinigameCount = model.PublishedMinigameCount;
            this.followerCount = model.FollowerCount;
            this.totalCoinsEarned = model.TotalCoinsEarned;
            this.totalLikes = model.TotalLikes;
            this.totalSuperLikes = model.TotalSuperLikes;
            this.creatorLikes = model.CreatorLikes;
            this.creatorRankingDelta = model.CreatorRankingDelta;
            this.coinDoubler = model.CoinDoubler;
            this.dirtBikeBundle = model.DirtBikeBundle;
            this.trailsPurchased = model.TrailsPurchased;
            this.hatsPurchased = model.HatsPurchased;
            this.bundlesPurchased = model.BundlesPurchased;
            this.pendingSpecialOfferChests = model.PendingSpecialOfferChests;
            this.adventureLevelsCompleted = model.AdventureLevelsCompleted;
            this.racesWon = model.RacesWon;
            this.newLevelsRated = model.NewLevelsRated;
            this.fbClaimed = model.FbClaimed;
            this.igClaimed = model.IgClaimed;
            this.forumClaimed = model.ForumClaimed;
            //this.teamData = model.TeamData; // Conversion needed
            this.nameChangesDone = model.NameChangesDone;

            if (model.ClientConfig is not null)
            {
                AddClientConfig(model.ClientConfig);
            }
        }

        public void AddProgression()
        {

        }

        public void AddPlanetVersion(PlanetVersion version)
        {
            planetVersions.Add(version);
        }

        public void AddClientConfig(ClientConfigModel model)
        {
            this.clientConfig = new ClientConfig().CreateFromModel(model);
        }

        public void AddClientVersion(long version)
        {
            this.clientVersion = version;
        }

        public void AddVersionInfo(string info)
        {
            this.versionInfo = info;
        }
        #endregion

        public List<object> paths { get; set; } = new();

        public List<PlanetVersion> planetVersions { get; set; } = new();

        public long clientVersion {  get; set; }
        public string versionInfo { get; set; }

        #region Player Data
        //player data
        public bool cheater { get; set; }
        public bool developer { get; set; }
        public string playerId { get; set; }
        public string name { get; set; }
        public string tag { get; set; }
        public string gameCenterId { get; set; }
        public string facebookId { get; set; }
        public string ninjaCreationTimestamp { get; set; }
        public string countryCode { get; set; }
        public string youtubeName { get; set; }
        public string youtubeId { get; set; }
        public int youtubeSubscriberCount { get; set; }
        public int itemDbVersion { get; set; }
        public int coins { get; set; }
        public int copper { get; set; }
        public int diamonds { get; set; }
        public int shards { get; set; }
        public int stars { get; set; }
        public int mcBoosters { get; set; }
        public int maxMcBoosters { get; set; }
        public int tournamentBoosters { get; set; }
        public int carBoosters { get; set; }
        public int maxCarBoosters { get; set; }
        public int itemLevel { get; set; }
        public int level { get; set; }
        public int cups { get; set; }
        public int mcRank { get; set; }
        public int carRank { get; set; }
        public int mcTrophies { get; set; }
        public int carTrophies { get; set; }
        public int bigBangPoints { get; set; }
        public int xp { get; set; }
        public float mcHandicap { get; set; }
        public float carHandicap { get; set; }
        public string cardPurchases { get; set; }
        public string gachaData { get; set; }
        public string teamId { get; set; }
        public string teamName { get; set; }
        public string teamRoleName { get; set; }
        public bool hasJoinedTeam { get; set; }
        public int seasonReward { get; set; }
        public string teamKickReason { get; set; }
        public int lastSeasonEndMcTrophies { get; set; }
        public int lastSeasonEndCarTrophies { get; set; }
        public int racesThisSeason { get; set; }
        public string ageGroup { get; set; }
        public string gender { get; set; }
        public bool completedSurvey { get; set; }
        //public Hashtable upgrades { get; set; } //ISSUE, CONVERSION TO DICTINOARY<string,object> NEEDED TODO
        public Hashtable boosters { get; set; }
        public Hashtable data { get; set; }
        public ClientConfig clientConfig { get; set; }
        public bool acceptNotifications { get; set; }
        public double mcBoosterRefreshTimeLeft { get; set; }
        public double carBoosterRefreshTimeLeft { get; set; }
        public double superLikeRefreshTimeLeft { get; set; }
        public Dictionary<string, int> editorResources { get; set; } = new();
        public List<string> claimedTutorials { get; set; } = new();
        public string hash { get; set; }
        public int publishedMinigameCount { get; set; }
        public int followerCount { get; set; }
        public int totalCoinsEarned { get; set; }
        public int totalLikes { get; set; }
        public int totalSuperLikes { get; set; }
        public int creatorLikes { get; set; }
        public int creatorRankingDelta { get; set; }
        public bool coinDoubler { get; set; }
        public bool dirtBikeBundle { get; set; }
        public List<string> trailsPurchased { get; set; } = new(); //ISSUE
        public List<string> hatsPurchased { get; set; } = new(); //ISSUE
        public List<string> bundlesPurchased { get; set; } = new(); //ISSUE
        public List<GachaType> pendingSpecialOfferChests { get; set; } = new(); //ISSUE
        public int adventureLevelsCompleted { get; set; }
        public int racesWon { get; set; }
        public int newLevelsRated { get; set; }
        public bool fbClaimed { get; set; }
        public bool igClaimed { get; set; }
        public bool forumClaimed { get; set; }
        //public TeamData teamData; //ISSUE
        public int nameChangesDone { get; set; }

        #endregion
    }
}
