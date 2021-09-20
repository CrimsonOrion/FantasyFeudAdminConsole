using System.Collections.Generic;

namespace FantasyFeudAdminConsole.Core.Models
{
    public class QuestionModel
    {
        public bool IsValid { get; set; }
        public string Team1Name { get; set; }
        public string Team2Name { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        public IEnumerable<TeamMembersModel> Team1Members { get; set; }
        public IEnumerable<TeamMembersModel> Team2Members { get; set; }
        public string Question { get; set; }
        public int Responses { get; set; }
        public IEnumerable<AnswerModel> Answers { get; set; }
        public int Strikes { get; set; }
    }
}