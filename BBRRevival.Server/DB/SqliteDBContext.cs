using BBRRevival.Common.Model;
using BBRRevival.Server.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BBRRevival.Server.DB
{
    public class SqliteDBContext : DbContext, IDatabaseContext
    {
        public SqliteDBContext(DbContextOptions<SqliteDBContext> options)
        : base(options) { }

        public DbSet<PlayerModel> Players { get; set; }
        public DbSet<ClientConfigModel> ClientConfigs { get; set; }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)  =>
            await base.SaveChangesAsync(cancellationToken);
    }
}
