using System.Data.SQLite;

namespace FantasyFeudAdminConsole.Core.DataAccess
{
    public static class SQLiteConnectionString
    {
        public static string ConnectionString { get; private set; }

        public static void SetConnectionString(string dataSource)
        {
            SQLiteConnectionStringBuilder connectionString = new()
            {
                DataSource = dataSource,
                Version = 3,
                ForeignKeys = true,
                DefaultTimeout = 5000,
                SyncMode = SynchronizationModes.Off,
                JournalMode = SQLiteJournalModeEnum.Wal,
                PageSize = 65536,
                CacheSize = 16777216,
                DateTimeFormat = SQLiteDateFormats.ISO8601,
                FailIfMissing = false
            };
            ConnectionString = connectionString.ToString();
        }
    }
}