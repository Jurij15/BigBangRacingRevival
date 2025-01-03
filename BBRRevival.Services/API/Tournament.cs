using BBRRevival.Services.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Services.API
{
    public class Tournament : DictionaryModel
    {
        public string tournamentId {  get; set; }
        public string minigameId {  get; set; }
        public float ccCap {  get; set; } //what?
        public int prizeCoins {  get; set; }
        public bool acceptingNewScores {  get; set; }
        public string ownerId {  get; set; }
        public string ownerName {  get; set; }
        public bool claimed {  get; set; }
    }
}
