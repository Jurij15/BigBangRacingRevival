using BBRRevival.Common.Model;

namespace BBRRevival.Server.Interfaces
{
    public interface IClientConfigService
    {
        public Task<ClientConfigModel> GetClientConfigForPlayer();
        public Task<ClientConfigModel> CreateClientConfig();
    }
}
