using BBRRevival.Services.API.Models.Enums;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BBRRevival.Services.API.Models
{
    public class MinigameMetadataModel
    {
        public void DebugPrint()
        {
            Log.Verbose("MINIGAME METADATA: " + this.name, null);
            Log.Verbose("ID: " + this.id, null);
            Log.Verbose("CREATOR NAME: " + this.creatorName, null);
            Log.Verbose("PUBLISHED: " + this.published, null);
            Log.Verbose("TIME SPENT EDITING:" + this.timeSpentEditing, null);
            Log.Verbose("TIMES PLAYED: " + this.timesPlayed, null);
            Log.Verbose("TIMES LIKED: " + this.timesLiked, null);
            Log.Verbose("CLIENT VERSION: " + this.clientVersion, null);
            Log.Verbose("GAME MODE: " + this.gameMode, null);
            Log.Verbose("COMPLEXITY: " + this.complexity, null);
            Log.Verbose("LEVEL REQUIREMENT: " + this.levelRequirement, null);
            Log.Verbose("PLAYER UNIT: " + this.playerUnit, null);
            if (this.itemsUsed != null)
            {
                for (int i = 0; i < this.itemsUsed.Length; i++)
                {
                    Log.Verbose(string.Concat(new object[]
                    {
                    "ITEMS USED ",
                    i,
                    ": ",
                    this.itemsUsed[i]
                    }), null);
                }
            }
        }

        // Token: 0x040021A7 RID: 8615
        public string id;

        // Token: 0x040021A8 RID: 8616
        public string name = "Unnamed Track";

        // Token: 0x040021A9 RID: 8617
        public string creatorName;

        // Token: 0x040021AA RID: 8618
        public string creatorId;

        // Token: 0x040021AB RID: 8619
        public string creatorFacebookId;

        // Token: 0x040021AC RID: 8620
        public string creatorGameCenterId;

        // Token: 0x040021AD RID: 8621
        public string creatorCountryCode;

        // Token: 0x040021AE RID: 8622
        public string videoUrl;

        // Token: 0x040021AF RID: 8623
        public bool published;

        // Token: 0x040021B0 RID: 8624
        public int timesPlayed;

        // Token: 0x040021B1 RID: 8625
        public int timesLiked;

        // Token: 0x040021B2 RID: 8626
        public int timesRated;

        // Token: 0x040021B3 RID: 8627
        public int timesSuperLiked;

        // Token: 0x040021B4 RID: 8628
        public int upThumbs;

        // Token: 0x040021B5 RID: 8629
        public int downThumbs;

        // Token: 0x040021B6 RID: 8630
        public int timesAbused;

        // Token: 0x040021B7 RID: 8631
        public int timeSpentEditing; //IN SECONDS

        // Token: 0x040021B8 RID: 8632
        public string description = "No Description";

        // Token: 0x040021B9 RID: 8633
        public int clientVersion;

        // Token: 0x040021BA RID: 8634
        public string difficulty;

        // Token: 0x040021BB RID: 8635
        public string gameMode = PsGameMode.Race.ToString();

        // Token: 0x040021BC RID: 8636
        public string rating = PsRating.Unrated.ToString();

        // Token: 0x040021BD RID: 8637
        public bool played;

        // Token: 0x040021BE RID: 8638
        public int score;

        // Token: 0x040021BF RID: 8639
        public int rewardCoins;

        // Token: 0x040021C0 RID: 8640
        public int totalCoinsEarned;

        // Token: 0x040021C1 RID: 8641
        public int complexity;

        // Token: 0x040021C2 RID: 8642
        public int levelRequirement;

        // Token: 0x040021C3 RID: 8643
        public string playerUnit;

        // Token: 0x040021C4 RID: 8644
        public string[] itemsUsed;

        // Token: 0x040021C5 RID: 8645
        public Dictionary<string, int> itemsCount;

        // Token: 0x040021C6 RID: 8646
        public string researchIdentifier;

        // Token: 0x040021C7 RID: 8647
        public int bestTime;

        // Token: 0x040021C8 RID: 8648
        public int medianTime;

        // Token: 0x040021C9 RID: 8649
        public int totalWinners;

        // Token: 0x040021CA RID: 8650
        public int timeScore;

        // Token: 0x040021CB RID: 8651
        public int oneStarWinners;

        // Token: 0x040021CC RID: 8652
        public int twoStarWinners;

        // Token: 0x040021CD RID: 8653
        public int threeStarWinners;

        // Token: 0x040021CE RID: 8654
        public int totalPlayers;

        // Token: 0x040021CF RID: 8655
        public Hashtable creatorUpgrades;

        // Token: 0x040021D0 RID: 8656
        public float gameQuality;
        
        public float quality;

        // Token: 0x040021D1 RID: 8657
        public float overrideCC = -1f;

        // Token: 0x040021D2 RID: 8658
        public bool template;

        // Token: 0x040021D3 RID: 8659
        public bool hidden;

        // Token: 0x040021D4 RID: 8660
        public const string TIME_SPENT_IN_EDIT_MODE = "timeSpentInEditMode";

        // Token: 0x040021D5 RID: 8661
        public const string EDIT_SESSION_COUNT = "editSessionCount";

        // Token: 0x040021D6 RID: 8662
        public const string ITEMS_MODIFICATION_COUNT = "itemsModificationCount";

        // Token: 0x040021D7 RID: 8663
        public const string GROUNDS_MODIFICATION_COUNT = "groundsModificationCount";

        // Token: 0x040021D8 RID: 8664
        public const string LAST_PLAY_SESSION_START_COUNT = "lastPlaySessionStartCount";

        // Token: 0x040021D9 RID: 8665
        public int timeSpentInEditMode;

        // Token: 0x040021DA RID: 8666
        public int editSessionCount;

        // Token: 0x040021DB RID: 8667
        public int itemsModificationCount;

        // Token: 0x040021DC RID: 8668
        public int groundsModificationCount;

        // Token: 0x040021DD RID: 8669
        public int lastPlaySessionStartCount;

        // Token: 0x040021DE RID: 8670
        public PsMinigameServerState m_state;
    }
}
