using BBRRevival.Common.Enums;
using BBRRevival.Common.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Common.Responses.Player
{
    internal class PlayerLoginResponse
    {
        public PlayerLoginResponse() { }

        public void AddPlayerData(PlayerModel model)
        {

        }



        // Token: 0x04001E43 RID: 7747
        public bool cheater;

        // Token: 0x04001E44 RID: 7748
        public bool developer;

        // Token: 0x04001E45 RID: 7749
        public string playerId;

        // Token: 0x04001E46 RID: 7750
        public string name;

        // Token: 0x04001E47 RID: 7751
        public string tag;

        // Token: 0x04001E48 RID: 7752
        public string gameCenterId;

        // Token: 0x04001E49 RID: 7753
        public string facebookId;

        // Token: 0x04001E4A RID: 7754
        public string ninjaCreationTimestamp;

        // Token: 0x04001E4B RID: 7755
        public string countryCode;

        // Token: 0x04001E4C RID: 7756
        public string youtubeName;

        // Token: 0x04001E4D RID: 7757
        public string youtubeId;

        // Token: 0x04001E4E RID: 7758
        public int youtubeSubscriberCount;

        // Token: 0x04001E4F RID: 7759
        public int itemDbVersion;

        // Token: 0x04001E50 RID: 7760
        public int coins;

        // Token: 0x04001E51 RID: 7761
        public int copper;

        // Token: 0x04001E52 RID: 7762
        public int diamonds;

        // Token: 0x04001E53 RID: 7763
        public int shards;

        // Token: 0x04001E54 RID: 7764
        public int stars;

        // Token: 0x04001E55 RID: 7765
        public int mcBoosters;

        // Token: 0x04001E56 RID: 7766
        public int maxMcBoosters;

        // Token: 0x04001E57 RID: 7767
        public int tournamentBoosters;

        // Token: 0x04001E58 RID: 7768
        public int carBoosters;

        // Token: 0x04001E59 RID: 7769
        public int maxCarBoosters;

        // Token: 0x04001E5A RID: 7770
        public int itemLevel;

        // Token: 0x04001E5B RID: 7771
        public int level;

        // Token: 0x04001E5C RID: 7772
        public int cups;

        // Token: 0x04001E5D RID: 7773
        public int mcRank;

        // Token: 0x04001E5E RID: 7774
        public int carRank;

        // Token: 0x04001E5F RID: 7775
        public int mcTrophies;

        // Token: 0x04001E60 RID: 7776
        public int carTrophies;

        // Token: 0x04001E61 RID: 7777
        public int bigBangPoints;

        // Token: 0x04001E62 RID: 7778
        public int xp;

        // Token: 0x04001E63 RID: 7779
        public float mcHandicap;
        public float carHandicap;
        public string cardPurchases;
        public string gachaData;
        public string teamId;
        public string teamName;
        public string teamRoleName;
        public bool hasJoinedTeam;
        public int seasonReward;
        public string teamKickReason;
        public int lastSeasonEndMcTrophies;
        public int lastSeasonEndCarTrophies;
        public int racesThisSeason;
        public string ageGroup;
        public string gender;
        public bool completedSurvey;
        public Hashtable upgrades;
        public Hashtable boosters;
        public Hashtable data;
        public ClientConfig clientConfig;
        public bool acceptNotifications;
        public double mcBoosterRefreshTimeLeft;
        public double carBoosterRefreshTimeLeft;
        public double superLikeRefreshTimeLeft;
        public Dictionary<string, int> editorResources;
        public List<string> claimedTutorials;
        public string hash;
        public int publishedMinigameCount;
        public int followerCount;
        public int totalCoinsEarned;
        public int totalLikes;
        public int totalSuperLikes;
        public int creatorLikes;
        public int creatorRankingDelta;
        public bool coinDoubler;
        public bool dirtBikeBundle;
        public List<string> trailsPurchased; //ISSUE
        public List<string> hatsPurchased; //ISSUE
        public List<string> bundlesPurchased; //ISSUE
        public List<GachaType> pendingSpecialOfferChests; //ISSUE
        public int adventureLevelsCompleted;
        public int racesWon;
        public int newLevelsRated;
        public bool fbClaimed;
        public bool igClaimed;
        public bool forumClaimed;
        //public TeamData teamData; //ISSUE
        public int nameChangesDone;
    }
}
