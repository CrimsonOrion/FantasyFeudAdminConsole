namespace FantasyFeudAdminConsole.Core.Models;

public class AnswersDataModel
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public int Rank { get; set; }
    public string? Answer { get; set; }
    public int Value { get; set; }
    public int Visible { get; set; }
}