namespace FantasyFeudAdminConsole.Core.Models
{
    public class QuestionsDataModel
    {
        public int GameId { get; set; }
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public int Responses { get; set; }
        public int Strikes { get; set; }
    }
}