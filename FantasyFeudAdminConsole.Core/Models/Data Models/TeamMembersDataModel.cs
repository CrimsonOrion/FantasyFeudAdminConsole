namespace FantasyFeudAdminConsole.Core.Models
{
    public class TeamMembersDataModel
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public string Name { get; set; }
        public int Active { get; set; }
    }
}