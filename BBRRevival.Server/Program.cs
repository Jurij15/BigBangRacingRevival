
using BBRRevival.Server.DB;
using BBRRevival.Server.Interfaces;
using BBRRevival.Server.Middleware;
using BBRRevival.Server.Services;
using Microsoft.EntityFrameworkCore;

namespace BBRRevival.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<SqliteDBContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));

            // Register interface for abstraction (optional)
            builder.Services.AddScoped<IDatabaseContext>(provider =>
                provider.GetRequiredService<SqliteDBContext>());

            builder.Services.AddScoped<IMusicService, MusicService>();
            builder.Services.AddScoped<IPlayerService, PlayerService>();

            builder.Services.AddControllers();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.WebHost.UseUrls("http://0.0.0.0:5000");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //custom middleware for PLAY_*
            app.UseMiddleware<PlayMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
