using BBRRevival.Common.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Common.Model
{
    public class PlayerModel
    {
        [Key]
        public Guid Id { get; set; } = new();

        public string Name { get; set; }
        public string Tag { get; set; }

        public bool IsCheater { get; set; } = false; //default to false
        public bool IsDeveloper { get; set; } = false; //default to false

        public string GameCenterId { get; set; } //id for the ios gamecenter, to restore data
        public string FacebookId { get; set; } //id for facebook, probably for sharing things on it

        public string NinjaCreationTimestamp { get; set; } //i have no idea what this is

        public string CountryCode { get; set; } //country code for the flag

        public string YoutubeName { get; set; } //name of the youtube channel
        public string YoutubeId { get; set; }
        public int YoutubeSubscriberCount { get; set; }

        public int ItemDbVersion { get; set; } = 0; //version of the database this entry uses, just default to 0 in this case

        public int Coins { get; set; }

        public int Copper { get; set; }

        public int Diamonds { get; set; }

        public int Shards { get; set; }

        public int Stars { get; set; }

        public int McBoosters { get; set; }

        public int MaxMcBoosters { get; set; }

        public int TournamentBoosters { get; set; }

        public int CarBoosters { get; set; }

        public int MaxCarBoosters { get; set; }

        public int ItemLevel { get; set; }

        public int Level { get; set; }

        public int Cups { get; set; }

        public int McRank { get; set; }

        public int VarRank { get; set; }

        public int McTrophies { get; set; }

        public int CarTrophies { get; set; }

        public int BigBangPoints { get; set; }

        public int Xp { get; set; }

        public float McHandicap { get; set; }

        public float CarHandicap { get; set; }

        public string CardPurchases { get; set; }

        public string GachaData { get; set; }

        public string TeamId { get; set; }

        public string TeamName { get; set; }

        public TeamRole teamRole { get; set; }

        public string TeamRoleName { get; set; }

        public bool HasJoinedTeam { get; set; }

        public int SeasonReward { get; set; }

        public string TeamKickReason { get; set; }

        public int LastSeasonEndMcTrophies { get; set; }

        public int LastSeasonEndCarTrophies { get; set; }

        public int RacesThisSeason { get; set; }

        public string AgeGroup { get; set; }

        public string Gender { get; set; }

        public bool CompletedSurvey { get; set; }

        public Hashtable Upgrades { get; set; }

        public Hashtable Boosters { get; set; }

        public bool AcceptNotifications { get; set; }

        public double McBoosterRefreshTimeLeft { get; set; }

        public double CarBoosterRefreshTimeLeft { get; set; }

        public double SuperLikeRefreshTimeLeft { get; set; }

        public Dictionary<string, int> EditorResources { get; set; }

        public List<string> ClaimedTutorials { get; set; }

        public string Hash { get; set; }

        public int PublishedMinigameCount { get; set; }

        public int FollowerCount { get; set; }

        public int TotalCoinsEarned { get; set; }

        public int TotalLikes { get; set; }

        public int TotalSuperLikes { get; set; }

        public int CreatorLikes { get; set; }

        public int CreatorRankingDelta { get; set; }

        public bool CoinDoubler { get; set; }

        public bool DirtBikeBundle { get; set; }

        public List<string> TrailsPurchased { get; set; }

        public List<string> HatsPurchased { get; set; }

        public List<string> BundlesPurchased { get; set; }

        public List<GachaType> PendingSpecialOfferChests { get; set; }

        public int AdventureLevelsCompleted { get; set; }

        public int RacesWon { get; set; }

        public int NewLevelsRated { get; set; }

        public bool FbClaimed { get; set; }

        public bool IgClaimed { get; set; }

        public bool ForumClaimed { get; set; }

        // TODO: When teams are finished, add this struct here like clientconfig
        //public TeamData teamData; //ISSUE

        public int NameChangesDone { get; set; }

        //client config things
        public Guid ClientConfigID { get; set; }

        [ForeignKey(nameof(ClientConfigID))]
        public ClientConfigModel ClientConfig { get; set; }
    }
}
