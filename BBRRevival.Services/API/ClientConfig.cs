using BBRRevival.Services.API.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Services.API
{
    public class ClientConfig : DictionaryModel
    {
        public int SuperLikeRefreshMinutes { get; set; } = 12;
        public int CarRefreshMinutes { get; set; } = 6;
        public int FreshFreeInterval { get; set; } = 15;
        public int FbConnectReward { get; set; } = 20;
        public int DailyGemAmount { get; set; } = 10;
        public int VideoAdCount { get; set; } = 2;
        public int VideoAdCoolDown { get; set; } = 3600;
        public int FreshFreeCount { get; set; } = 3;
        public int FreshFreeCoolDown { get; set; } = 1800;
        public int InRaceDiamondSpawnProbability { get; set; } = 25;
        public int BoltsAtStart { get; set; } = 0;
        public int CoinsAtStart { get; set; } = 1000;
        public int DiamondsAtStart { get; set; } = 50;
        public int KeysAtStart { get; set; } = 5;
        public int OfferCooldownMinutes { get; set; } = 4320;
        public int OfferDurationMinutes { get; set; } = 60;
        public int MinimumTournamentNitros { get; set; } = 5;
        public int CreatorRank1 { get; set; } = 100;
        public int CreatorRank2 { get; set; } = 1000;
        public int CreatorRank3 { get; set; } = 10000;
        public int CreatorRank4 { get; set; } = 100000;
        public int CreatorRank5 { get; set; } = 1000000;
        public int CreatorRank6 { get; set; } = 10000000;
        public int TriesForAd { get; set; } = 0;
        public int TriesForGems { get; set; } = 0;
        public int TriesGemPrice { get; set; } = 0;
    }
}
