using System.Data.SqlClient;

namespace FantasyFeudAdminConsole.Core.DataAccess
{
    public static class MsSqlConnectionString
    {
        public static string ConnectionString(string dataSource, string initialCatalog, string userId, string password, bool integratedSecurity = false)
        {
            SqlConnectionStringBuilder connectionString = new()
            {
                DataSource = dataSource,
                InitialCatalog = initialCatalog,
                UserID = userId,
                Password = password,
                IntegratedSecurity = integratedSecurity
            };
            return connectionString.ToString();
        }
    }
}