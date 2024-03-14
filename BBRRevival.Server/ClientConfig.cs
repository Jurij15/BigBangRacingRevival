using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Server
{
    public class ClientConfig
    {
        public ClientConfig()
        {
            this.superLikeRefreshMinutes = 12;
            this.carRefreshMinutes = 6;
            this.freshFreeInterval = 15;
            this.fbConnectReward = 20;
            this.dailyGemAmount = 10;
            this.videoAdCount = 2;
            this.videoAdCoolDown = 3600;
            this.freshFreeCount = 3;
            this.freshFreeCoolDown = 1800;
            this.inRaceDiamondSpawnProbability = 25;
            this.boltsAtStart = 0;
            this.coinsAtStart = 1000;
            this.diamondsAtStart = 50;
            this.keysAtStart = 5;
            this.offerCooldownMinutes = 4320;
            this.offerDurationMinutes = 60;
            this.minimumTournamentNitros = 5;
            this.creatorRank1 = 100;
            this.creatorRank2 = 1000;
            this.creatorRank3 = 10000;
            this.creatorRank4 = 100000;
            this.creatorRank5 = 1000000;
            this.creatorRank6 = 10000000;
        }



        // Token: 0x040021DF RID: 8671
        public int carRefreshMinutes;

        // Token: 0x040021E0 RID: 8672
        public int freshFreeInterval;

        // Token: 0x040021E1 RID: 8673
        public int keysAtStart;

        // Token: 0x040021E2 RID: 8674
        public int diamondsAtStart;

        // Token: 0x040021E3 RID: 8675
        public int coinsAtStart;

        // Token: 0x040021E4 RID: 8676
        public int boltsAtStart;

        // Token: 0x040021E5 RID: 8677
        public int fbConnectReward;

        // Token: 0x040021E6 RID: 8678
        public int dailyGemAmount;

        // Token: 0x040021E7 RID: 8679
        public int videoAdCount;

        // Token: 0x040021E8 RID: 8680
        public int videoAdCoolDown;

        // Token: 0x040021E9 RID: 8681
        public int freshFreeCount;

        // Token: 0x040021EA RID: 8682
        public int freshFreeCoolDown;

        // Token: 0x040021EB RID: 8683
        public int inRaceDiamondSpawnProbability;

        // Token: 0x040021EC RID: 8684
        public int superLikeRefreshMinutes;

        // Token: 0x040021ED RID: 8685
        public int offerCooldownMinutes;

        // Token: 0x040021EE RID: 8686
        public int offerDurationMinutes;

        // Token: 0x040021EF RID: 8687
        public int minimumTournamentNitros;

        // Token: 0x040021F0 RID: 8688
        public int tournamentYoutuberFollowNitros;

        // Token: 0x040021F1 RID: 8689
        public int creatorRank1;

        // Token: 0x040021F2 RID: 8690
        public int creatorRank2;

        // Token: 0x040021F3 RID: 8691
        public int creatorRank3;

        // Token: 0x040021F4 RID: 8692
        public int creatorRank4;

        // Token: 0x040021F5 RID: 8693
        public int creatorRank5;

        // Token: 0x040021F6 RID: 8694
        public int creatorRank6;
    }
}
