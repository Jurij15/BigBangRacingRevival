using BBRRevival.Common.Model;
using BBRRevival.Server.Interfaces;

namespace BBRRevival.Server.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IDatabaseContext context;
        private readonly ILogger<PlayerService> logger;

        public PlayerService(IDatabaseContext dbContext, ILogger<PlayerService> logger)
        {
            context = dbContext;
            this.logger = logger;
        }

        public async Task<PlayerModel> CreatePlayer()
        {
            PlayerModel model = new();

            model.Id = new Guid();
            model.Name = "CoolPlayerName";
            model.NameChangesDone = 0;
            model.AcceptNotifications = false;
            model.Tag = "CoolPlayerTag";


            model.CountryCode = "0";

            model.HasJoinedTeam = false;
            model.IsDeveloper = false;

            model.Upgrades = new();
            model.Boosters = new();

            model.EditorResources = new();

            model.ClaimedTutorials  = new();

            model.TrailsPurchased = new();
            model.HatsPurchased= new();
            model.BundlesPurchased = new();
            model.PendingSpecialOfferChests = new();
            
            //TODO: CLIENT CONFIG!

            return model;
        }
    }
}
