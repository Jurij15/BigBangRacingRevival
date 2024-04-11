using Serilog;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Services.Database
{
    public class DatabaseInteractor
    {
        private static string DefaultDBConnectionString = "Data Source=Assets\\PlayerData.db;Version=3;";
        private SQLiteConnection _connection;
        public bool isConnected = false;

        public DatabaseInteractor()
        {
            InitConnection();
        }

        public SQLiteConnection GetConnection()
        {
            return _connection;
        }

        public async void InitConnection()
        {
            _connection = new SQLiteConnection(DefaultDBConnectionString);
            try
            {
                await _connection.OpenAsync();
                isConnected = true;
                Log.Information("Connected to PlayerData db!");
            }
            catch (Exception ex)
            {
                Log.Error($"Connection to PlayerData DB failed with error: {ex.Message}");
            }
        }

        public async void Disconnect()
        {
            await _connection.CloseAsync();
            _connection = null;
            isConnected = false;

            Log.Warning("Disconnected from PlayerData db!");
        }

        public async Task ExecuteSqlCommand(string Command)
        {
            try
            {
                SQLiteCommand cmd = new SQLiteCommand(Command, _connection);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Log.Error($"Executing Command {Command} failed with error {ex.Message}");
            }
        }

        public async Task<DbDataReader> ExecuteSqlCommandReader(string Command)
        {
            DbDataReader reader = null;
            try
            {
                SQLiteCommand cmd = new SQLiteCommand(Command, _connection);
                reader = await cmd.ExecuteReaderAsync();
            }
            catch (Exception ex)
            {
                Log.Error($"Executing Command {Command} failed with error {ex.Message}");
                throw;
            }
            return reader;
        }
    }
}
