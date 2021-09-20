namespace FantasyFeudAdminConsole.Core.Models
{
    public class QuestionsDataModel
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string Question { get; set; }
        public int Responses { get; set; }
        public int Strikes { get; set; }
    }
}