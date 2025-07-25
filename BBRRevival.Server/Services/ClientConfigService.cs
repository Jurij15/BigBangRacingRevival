using BBRRevival.Common.Model;
using BBRRevival.Server.Interfaces;

namespace BBRRevival.Server.Services
{
    public class ClientConfigService : IClientConfigService
    {
        private readonly IDatabaseContext context;
        private readonly ILogger<ClientConfigService> logger;

        public ClientConfigService(IDatabaseContext dbContext, ILogger<ClientConfigService> logger)
        {
            context = dbContext;
            this.logger = logger;
        }

        public async Task<ClientConfigModel> CreateClientConfig()
        {
            ClientConfigModel model = new();

            //TODO: Write to db

            return model;
        }

        public async Task<ClientConfigModel> GetClientConfigForPlayer()
        {
            ClientConfigModel result = null;

            return result;
        }
    }
}
