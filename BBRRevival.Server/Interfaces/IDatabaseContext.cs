using BBRRevival.Common.Model;
using Microsoft.EntityFrameworkCore;

namespace BBRRevival.Server.Interfaces
{
    public interface IDatabaseContext
    {
        public DbSet<PlayerModel> Players { get; set; }
        public DbSet<ClientConfigModel> ClientConfigs { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
