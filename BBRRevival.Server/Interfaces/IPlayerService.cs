using BBRRevival.Common.Model;

namespace BBRRevival.Server.Interfaces
{
    public interface IPlayerService
    {
        public Task<PlayerModel> CreatePlayer();
    }
}
