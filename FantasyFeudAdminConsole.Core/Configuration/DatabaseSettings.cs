namespace FantasyFeudAdminConsole.Core.Configuration
{
    public class DatabaseSettings
    {
        public string DataSource { get; set; }
        public string InitialCatalog { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public bool PersistSecurityInfo { get; set; }
    }
}