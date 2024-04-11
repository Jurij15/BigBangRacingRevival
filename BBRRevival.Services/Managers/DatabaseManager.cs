using BBRRevival.Services.API;
using BBRRevival.Services.Database;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Services.Managers
{
    public class DatabaseManager : DatabaseInteractor
    {
        public DatabaseManager() : base()
        {
            Log.Verbose("Initialized DatabaseManager!");
        }

        public void CreateTableClientConfig(ClientConfig config)
        {
            string command = "INSERT INTO ClientConfig (carRefreshMinutes, freshFreeInterval, keysAtStart, diamondsAtStart, coinsAtStart, boltsAtStart, fbConnectReward, dailyGemAmount, videoAdCount, videoAdCoolDown, freshFreeCount, freshFreeCoolDown, inRaceDiamondSpawnProbability, superLikeRefreshMinutes, offerCooldownMinutes, offerDurationMinutes, minimumTournamentNitros, tournamentYoutuberFollowNitros, creatorRank1¸ creatorRank2, creatorRank3, creatorRank4, creatorRank5, creatorRank6) VALUES ";
        }
    }
}
