using System;
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

        //client config things
        public Guid ClientConfigID { get; set; }

        [ForeignKey(nameof(ClientConfigID))]
        public ClientConfigModel ClientConfig { get; set; }
    }
}
