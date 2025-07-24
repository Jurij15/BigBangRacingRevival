using BBRRevival.Common.Enums;
using BBRRevival.Common.Model;
using BBRRevival.Server.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections;
using System.Text.Json;
using System.Xml;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //dictionaries cannot be saved directly to sqlite, this converts it to json and vice versa
            var dictionaryConverter = new ValueConverter<Dictionary<string, int>, string>(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<Dictionary<string, int>>(v, (JsonSerializerOptions)null));

            //hashtables cannot be saved directly to sqlite, this converts it to json and vice versa
            var hashtableConverter = new ValueConverter<Hashtable, string>(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<Hashtable>(v, (JsonSerializerOptions)null));

            //hashtables cannot be saved directly to sqlite, this converts it to json and vice versa
            var listConverter = new ValueConverter<List<string>, string>(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null));

            //hashtables cannot be saved directly to sqlite, this converts it to json and vice versa
            var gachaListConverter = new ValueConverter<List<GachaType>, string>(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<GachaType>>(v, (JsonSerializerOptions)null));

            modelBuilder.Entity<PlayerModel>()
                .Property(e => e.Upgrades)
                .HasConversion(hashtableConverter);

            modelBuilder.Entity<PlayerModel>()
                .Property(e => e.Boosters)
                .HasConversion(hashtableConverter);

            modelBuilder.Entity<PlayerModel>()
                .Property(e => e.EditorResources)
                .HasConversion(dictionaryConverter);

            modelBuilder.Entity<PlayerModel>()
                .Property(e => e.TrailsPurchased)
                .HasConversion(listConverter);

            modelBuilder.Entity<PlayerModel>()
                .Property(e => e.HatsPurchased)
                .HasConversion(listConverter);

            modelBuilder.Entity<PlayerModel>()
                .Property(e => e.BundlesPurchased)
                .HasConversion(listConverter);

            modelBuilder.Entity<PlayerModel>()
                .Property(e => e.PendingSpecialOfferChests)
                .HasConversion(gachaListConverter);

            base.OnModelCreating(modelBuilder);
        }
    }
}
