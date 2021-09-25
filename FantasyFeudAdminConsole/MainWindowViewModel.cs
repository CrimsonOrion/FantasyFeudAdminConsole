
using FantasyFeudAdminConsole.Core.Models;
using FantasyFeudAdminConsole.Core.Processors;

using MahApps.Metro.Controls.Dialogs;

using Prism.Commands;
using Prism.Mvvm;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace FantasyFeudAdminConsole
{
    public class MainWindowViewModel : BindableBase
    {
        #region Private fields

        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly IDataProcessor _dataProcessor;
        private readonly IWebProcessor _webProcessor;

        #endregion

        #region Properties

        #region Database Data

        private GamesDataModel _gameData = new();
        public GamesDataModel GameData
        {
            get => _gameData;
            set => SetProperty(ref _gameData, value);
        }

        private List<QuestionsDataModel> _questionDataList = new();
        public List<QuestionsDataModel> QuestionDataList
        {
            get => _questionDataList;
            set => SetProperty(ref _questionDataList, value);
        }

        private QuestionsDataModel _questionData = new();
        public QuestionsDataModel QuestionData
        {
            get => _questionData;
            set => SetProperty(ref _questionData, value);
        }

        private TeamsDataModel _team1Data = new();
        public TeamsDataModel Team1Data
        {
            get => _team1Data;
            set => SetProperty(ref _team1Data, value);
        }

        private TeamsDataModel _team2Data = new();
        public TeamsDataModel Team2Data
        {
            get => _team2Data;
            set => SetProperty(ref _team2Data, value);
        }

        private ObservableCollection<TeamMembersDataModel> _team1Members = new();
        public ObservableCollection<TeamMembersDataModel> Team1Members
        {
            get => _team1Members;
            set => SetProperty(ref _team1Members, value);
        }

        private ObservableCollection<TeamMembersDataModel> _team2Members = new();
        public ObservableCollection<TeamMembersDataModel> Team2Members
        {
            get => _team2Members;
            set => SetProperty(ref _team2Members, value);
        }

        private List<AnswersDataModel> _answersData = new();
        public List<AnswersDataModel> AnswersData
        {
            get => _answersData;
            set => SetProperty(ref _answersData, value);
        }

        #endregion

        #region UI Data

        public static string Title => "Fantasy Feud Admin Console";

        private int _gameId;
        public int GameId
        {
            get => _gameId;
            set => SetProperty(ref _gameId, value);
        }

        private int _questionId;
        public int QuestionId
        {
            get => _questionId;
            set => SetProperty(ref _questionId, value);
        }

        private int _strikes;
        public int Strikes
        {
            get => _strikes;
            set => SetProperty(ref _strikes, value);
        }

        private string _question;
        public string Question
        {
            get => _question;
            set => SetProperty(ref _question, value);
        }

        private string _team1Name;
        public string Team1Name
        {
            get => _team1Name;
            set => SetProperty(ref _team1Name, value);
        }

        private int _team1Score;
        public int Team1Score
        {
            get => _team1Score;
            set => SetProperty(ref _team1Score, value);
        }

        private string _team2Name;
        public string Team2Name
        {
            get => _team2Name;
            set => SetProperty(ref _team2Name, value);
        }

        private int _team2Score;
        public int Team2Score
        {
            get => _team2Score;
            set => SetProperty(ref _team2Score, value);
        }

        private string _answer0;
        public string Answer0
        {
            get => _answer0;
            set => SetProperty(ref _answer0, value);
        }

        private int _answer0Value;
        public int Answer0Value
        {
            get => _answer0Value;
            set => SetProperty(ref _answer0Value, value);
        }

        private int _answer0Visible;
        public int Answer0Visible
        {
            get => _answer0Visible;
            set => SetProperty(ref _answer0Visible, value);
        }

        private bool _answer0IsEnabled;
        public bool Answer0IsEnabled
        {
            get => _answer0IsEnabled;
            set => SetProperty(ref _answer0IsEnabled, value);
        }

        private string _answer1;
        public string Answer1
        {
            get => _answer1;
            set => SetProperty(ref _answer1, value);
        }

        private int _answer1Value;
        public int Answer1Value
        {
            get => _answer1Value;
            set => SetProperty(ref _answer1Value, value);
        }

        private int _answer1Visible;
        public int Answer1Visible
        {
            get => _answer1Visible;
            set => SetProperty(ref _answer1Visible, value);
        }

        private bool _answer1IsEnabled;
        public bool Answer1IsEnabled
        {
            get => _answer1IsEnabled;
            set => SetProperty(ref _answer1IsEnabled, value);
        }

        private string _answer2;
        public string Answer2
        {
            get => _answer2;
            set => SetProperty(ref _answer2, value);
        }

        private int _answer2Value;
        public int Answer2Value
        {
            get => _answer2Value;
            set => SetProperty(ref _answer2Value, value);
        }

        private int _answer2Visible;
        public int Answer2Visible
        {
            get => _answer2Visible;
            set => SetProperty(ref _answer2Visible, value);
        }

        private bool _answer2IsEnabled;
        public bool Answer2IsEnabled
        {
            get => _answer2IsEnabled;
            set => SetProperty(ref _answer2IsEnabled, value);
        }

        private string _answer3;
        public string Answer3
        {
            get => _answer3;
            set => SetProperty(ref _answer3, value);
        }

        private int _answer3Value;
        public int Answer3Value
        {
            get => _answer3Value;
            set => SetProperty(ref _answer3Value, value);
        }

        private int _answer3Visible;
        public int Answer3Visible
        {
            get => _answer3Visible;
            set => SetProperty(ref _answer3Visible, value);
        }

        private bool _answer3IsEnabled;
        public bool Answer3IsEnabled
        {
            get => _answer3IsEnabled;
            set => SetProperty(ref _answer3IsEnabled, value);
        }

        private string _answer4;
        public string Answer4
        {
            get => _answer4;
            set => SetProperty(ref _answer4, value);
        }

        private int _answer4Value;
        public int Answer4Value
        {
            get => _answer4Value;
            set => SetProperty(ref _answer4Value, value);
        }

        private int _answer4Visible;
        public int Answer4Visible
        {
            get => _answer4Visible;
            set => SetProperty(ref _answer4Visible, value);
        }

        private bool _answer4IsEnabled;
        public bool Answer4IsEnabled
        {
            get => _answer4IsEnabled;
            set => SetProperty(ref _answer4IsEnabled, value);
        }

        private string _answer5;
        public string Answer5
        {
            get => _answer5;
            set => SetProperty(ref _answer5, value);
        }

        private int _answer5Value;
        public int Answer5Value
        {
            get => _answer5Value;
            set => SetProperty(ref _answer5Value, value);
        }

        private int _answer5Visible;
        public int Answer5Visible
        {
            get => _answer5Visible;
            set => SetProperty(ref _answer5Visible, value);
        }

        private bool _answer5IsEnabled;
        public bool Answer5IsEnabled
        {
            get => _answer5IsEnabled;
            set => SetProperty(ref _answer5IsEnabled, value);
        }

        private string _answer6;
        public string Answer6
        {
            get => _answer6;
            set => SetProperty(ref _answer6, value);
        }

        private int _answer6Value;
        public int Answer6Value
        {
            get => _answer6Value;
            set => SetProperty(ref _answer6Value, value);
        }

        private int _answer6Visible;
        public int Answer6Visible
        {
            get => _answer6Visible;
            set => SetProperty(ref _answer6Visible, value);
        }

        private bool _answer6IsEnabled;
        public bool Answer6IsEnabled
        {
            get => _answer6IsEnabled;
            set => SetProperty(ref _answer6IsEnabled, value);
        }

        private string _answer7;
        public string Answer7
        {
            get => _answer7;
            set => SetProperty(ref _answer7, value);
        }

        private int _answer7Value;
        public int Answer7Value
        {
            get => _answer7Value;
            set => SetProperty(ref _answer7Value, value);
        }

        private int _answer7Visible;
        public int Answer7Visible
        {
            get => _answer7Visible;
            set => SetProperty(ref _answer7Visible, value);
        }

        private bool _answer7IsEnabled;
        public bool Answer7IsEnabled
        {
            get => _answer7IsEnabled;
            set => SetProperty(ref _answer7IsEnabled, value);
        }

        private bool _nextQuestionIsEnabled;
        public bool NextQuestionIsEnabled
        {
            get => _nextQuestionIsEnabled;
            set => SetProperty(ref _nextQuestionIsEnabled, value);
        }

        private bool _strikeIsEnabled;
        public bool StrikeIsEnabled
        {
            get => _strikeIsEnabled;
            set => SetProperty(ref _strikeIsEnabled, value);
        }

        private TeamMembersDataModel _selectedTeam1Member = new();
        public TeamMembersDataModel SelectedTeam1Member
        {
            get => _selectedTeam1Member;
            set => SetProperty(ref _selectedTeam1Member, value);
        }

        private TeamMembersDataModel _selectedTeam2Member = new();
        public TeamMembersDataModel SelectedTeam2Member
        {
            get => _selectedTeam2Member;
            set => SetProperty(ref _selectedTeam2Member, value);
        }

        #endregion

        #endregion

        #region Commands

        public DelegateCommand AddStrikeCommand => new(AddStrike);
        public DelegateCommand NextQuestionCommand => new(NextQuestion);
        public DelegateCommand GetDataCommand => new(GetData);
        public DelegateCommand ProcessChangesCommand => new(ProcessChanges);
        public DelegateCommand<int?> ShowAnswerCommand => new(ShowAnswer);
        public DelegateCommand<int?> AddTeamMemberCommand => new(AddTeamMember);
        public DelegateCommand<int?> RemoveTeamMemberCommand => new(RemoveTeamMember);
        public DelegateCommand PostEventCommand => new(PostEvent);
        public DelegateCommand<int?> ChangeActiveMemberCommand => new(ChangeActiveMember);
        public DelegateCommand<int?> ReorderTeamMemberCommand => new(ReorderTeamMember);
        public DelegateCommand<int?> AwardTeamCommand => new(AwardTeam);

        #endregion

        #region ctor

        public MainWindowViewModel(IDialogCoordinator dialogCoordinator, IDataProcessor dataProcessor, IWebProcessor webProcessor)
        {
            _dialogCoordinator = dialogCoordinator;
            _dataProcessor = dataProcessor;
            _webProcessor = webProcessor;
        }

        #endregion

        #region Methods

        private async void AddStrike()
        {
            Strikes = Strikes switch
            {
                < 4 => ++Strikes,
                _ => 0,
            };
            QuestionData.Strikes = Strikes;

            _ = await _dataProcessor.AddStrikeAsync(QuestionData);
        }

        private async void NextQuestion()
        {
            QuestionsDataModel question = QuestionDataList.FirstOrDefault(_ => _.Id == QuestionData.Id + 1);
            if (question is not null)
            {
                QuestionData = question;
                AnswersData = new(await _dataProcessor.GetAnswersDataAsync(question.Id));
                SetConsole();
            }

            if (QuestionDataList.FirstOrDefault(_ => _.Id == QuestionData.Id + 1) is null)
            {
                NextQuestionIsEnabled = false;
            }
        }

        private async void GetData()
        {
            GameData = await _dataProcessor.GetGameDataAsync(GameData.Id);
            QuestionDataList = new(await _dataProcessor.GetQuestionsDataAsync(GameData.Id));
            QuestionData = QuestionDataList.FirstOrDefault(_ => _.Id == QuestionData.Id);
            Team1Data = await _dataProcessor.GetTeamDataAsync(GameData.Team1Id);
            Team2Data = await _dataProcessor.GetTeamDataAsync(GameData.Team2Id);
            Team1Members = new(await _dataProcessor.GetTeamMembersDataAsync(Team1Data.Id));
            Team2Members = new(await _dataProcessor.GetTeamMembersDataAsync(Team2Data.Id));
            AnswersData = new(await _dataProcessor.GetAnswersDataAsync(QuestionData.Id));
            NextQuestionIsEnabled = QuestionData is not null;
            StrikeIsEnabled = QuestionData is not null;
            SetConsole();
        }

        private async void ProcessChanges()
        {
            // In the event there's a manual change
            Team1Data.TeamName = Team1Name;
            GameData.Team1Score = Team1Score;
            Team2Data.TeamName = Team2Name;
            GameData.Team2Score = Team2Score;

            var success = await _dataProcessor.UpdateAllDataAsync(Team1Data, Team2Data, GameData, Team1Members, Team2Members, QuestionData, AnswersData);

            _ = success
                ? await _dialogCoordinator.ShowMessageAsync(this, "Success!", "Data sync'd successfully.")
                : await _dialogCoordinator.ShowMessageAsync(this, "Failure!", "Data NOT sync'd successfully.");
        }

        private async void AddTeamMember(int? teamNumber)
        {
            var teamName = teamNumber == 1 ? Team1Name : Team2Name;
            var name = await _dialogCoordinator.ShowInputAsync(this, "Insert Name", $"Insert new player for {teamName}");

            TeamMembersDataModel newMember = new()
            {
                TeamId = teamNumber == 1 ? Team1Data.Id : Team2Data.Id,
                Name = name,
                Active = 0
            };

            _ = await _dataProcessor.AddTeamMemberAsync(newMember);

            if (teamNumber == 1)
            {
                Team1Members.Add(newMember);
            }
            else
            {
                Team2Members.Add(newMember);
            }
        }

        private async void RemoveTeamMember(int? teamNumber)
        {
            TeamMembersDataModel member = new();

            _ = teamNumber == 1 ? Team1Members.Remove(SelectedTeam1Member) : Team2Members.Remove(SelectedTeam2Member);

            _ = await _dataProcessor.RemoveTeamMemberAsync(member.Id);

        }

        private async void PostEvent()
        {
            List<TeamMembersDataModel> teamMembersData = new();
            teamMembersData.AddRange(Team1Members);
            teamMembersData.AddRange(Team2Members);
            QuestionModel model = _webProcessor.CreateEventMessage(GameData, new List<TeamsDataModel>() { Team1Data, Team2Data }, teamMembersData, QuestionData, AnswersData);
            Debug.WriteLine(await _webProcessor.PostEventAsync(model));
        }

        private async void ChangeActiveMember(int? teamNumber)
        {
            if (Answer0 == null)
            {
                return;
            }

            if (teamNumber == 1 && SelectedTeam1Member is not null)
            {
                TeamMembersDataModel inactive = Team1Members.FirstOrDefault(_ => _.Active == 1) ?? new() { Id = 0 };
                inactive.Active = 0;
                TeamMembersDataModel active = Team1Members.FirstOrDefault(_ => _.Id == SelectedTeam1Member.Id) ?? new() { Id = 0 };
                active.Active = 1;
                _ = await _dataProcessor.ChangeActiveMemberAsync(inactive.Id, active.Id);
                Debug.WriteLine($"{SelectedTeam1Member.Name} is now Active!");
            }
            else if (SelectedTeam2Member is not null)
            {
                TeamMembersDataModel inactive = Team2Members.FirstOrDefault(_ => _.Active == 1) ?? new() { Id = 0 };
                inactive.Active = 0;
                TeamMembersDataModel active = Team2Members.FirstOrDefault(_ => _.Id == SelectedTeam2Member.Id) ?? new() { Id = 0 };
                active.Active = 1;
                _ = await _dataProcessor.ChangeActiveMemberAsync(inactive.Id, active.Id);
                Debug.WriteLine($"{SelectedTeam2Member.Name} is now Active!");
            }

            PostEvent();
        }

        private void ReorderTeamMember(int? teamNumber)
        {
            if (teamNumber == 1)
            {
                var selectedIndex = Team1Members.IndexOf(SelectedTeam1Member);
                Team1Members.Move(selectedIndex, selectedIndex == 0 ? Team1Members.Count - 1 : selectedIndex - 1);
            }
            else
            {
                var selectedIndex = Team2Members.IndexOf(SelectedTeam2Member);
                Team2Members.Move(selectedIndex, selectedIndex == 0 ? Team2Members.Count - 1 : selectedIndex - 1);
            }

            PostEvent();
        }

        private async void AwardTeam(int? teamNumber)
        {
            var points = AnswersData.Where(_ => _.Visible == 1).Sum(_ => _.Value);
            if (teamNumber == 1)
            {
                GameData.Team1Score += points;
                Team1Score = GameData.Team1Score;
            }
            else
            {
                GameData.Team2Score += points;
                Team2Score = GameData.Team2Score;
            }

            _ = await _dataProcessor.AwardPointsAsync(GameData.Id, teamNumber ?? 0, teamNumber == 1 ? GameData.Team1Score : GameData.Team2Score);
        }

        private void SetConsole()
        {
            GameId = GameData.Id;
            QuestionId = QuestionData.Id;
            Strikes = QuestionData.Strikes;
            Question = QuestionData.Question;
            Team1Name = Team1Data.TeamName;
            Team1Score = GameData.Team1Score;
            SelectedTeam1Member = Team1Members.FirstOrDefault(_ => _.Active == 1);
            Team2Name = Team2Data.TeamName;
            Team2Score = GameData.Team2Score;
            SelectedTeam2Member = Team2Members.FirstOrDefault(_ => _.Active == 1);

            for (var i = 0; i < 8; i++)
            {
                if (i == 0)
                {
                    if (i < AnswersData.Count)
                    {
                        Answer0 = AnswersData[i].Answer;
                        Answer0Value = AnswersData[i].Value;
                        Answer0Visible = AnswersData[i].Visible;
                        Answer0IsEnabled = true;
                    }
                    else
                    {
                        Answer0 = "";
                        Answer0Value = 0;
                        Answer0Visible = 0;
                        Answer0IsEnabled = false;
                    }
                }
                else if (i == 1)
                {
                    if (i < AnswersData.Count)
                    {
                        Answer1 = AnswersData[i].Answer;
                        Answer1Value = AnswersData[i].Value;
                        Answer1Visible = AnswersData[i].Visible;
                        Answer1IsEnabled = true;
                    }
                    else
                    {
                        Answer1 = "";
                        Answer1Value = 0;
                        Answer1Visible = 0;
                        Answer1IsEnabled = false;
                    }
                }
                else if (i == 2)
                {
                    if (i < AnswersData.Count)
                    {
                        Answer2 = AnswersData[i].Answer;
                        Answer2Value = AnswersData[i].Value;
                        Answer2Visible = AnswersData[i].Visible;
                        Answer2IsEnabled = true;
                    }
                    else
                    {
                        Answer2 = "";
                        Answer2Value = 0;
                        Answer2Visible = 0;
                        Answer2IsEnabled = false;
                    }
                }
                else if (i == 3)
                {
                    if (i < AnswersData.Count)
                    {
                        Answer3 = AnswersData[i].Answer;
                        Answer3Value = AnswersData[i].Value;
                        Answer3Visible = AnswersData[i].Visible;
                        Answer3IsEnabled = true;
                    }
                    else
                    {
                        Answer3 = "";
                        Answer3Value = 0;
                        Answer3Visible = 0;
                        Answer3IsEnabled = false;
                    }
                }
                else if (i == 4)
                {
                    if (i < AnswersData.Count)
                    {
                        Answer4 = AnswersData[i].Answer;
                        Answer4Value = AnswersData[i].Value;
                        Answer4Visible = AnswersData[i].Visible;
                        Answer4IsEnabled = true;
                    }
                    else
                    {
                        Answer4 = "";
                        Answer4Value = 0;
                        Answer4Visible = 0;
                        Answer4IsEnabled = false;
                    }
                }
                else if (i == 5)
                {
                    if (i < AnswersData.Count)
                    {
                        Answer5 = AnswersData[i].Answer;
                        Answer5Value = AnswersData[i].Value;
                        Answer5Visible = AnswersData[i].Visible;
                        Answer5IsEnabled = true;
                    }
                    else
                    {
                        Answer5 = "";
                        Answer5Value = 0;
                        Answer5Visible = 0;
                        Answer5IsEnabled = false;
                    }
                }
                else if (i == 6)
                {
                    if (i < AnswersData.Count)
                    {
                        Answer6 = AnswersData[i].Answer;
                        Answer6Value = AnswersData[i].Value;
                        Answer6Visible = AnswersData[i].Visible;
                        Answer6IsEnabled = true;
                    }
                    else
                    {
                        Answer6 = "";
                        Answer6Value = 0;
                        Answer6Visible = 0;
                        Answer6IsEnabled = false;
                    }
                }
                else if (i == 7)
                {
                    if (i < AnswersData.Count)
                    {
                        Answer7 = AnswersData[i].Answer;
                        Answer7Value = AnswersData[i].Value;
                        Answer7Visible = AnswersData[i].Visible;
                        Answer7IsEnabled = true;
                    }
                    else
                    {
                        Answer7 = "";
                        Answer7Value = 0;
                        Answer7Visible = 0;
                        Answer7IsEnabled = false;
                    }
                }
            }
        }

        private async void ShowAnswer(int? index)
        {
            AnswersDataModel model = AnswersData.FirstOrDefault(_ => _.Id == index);
            model.Visible = model.Visible == 1 ? 0 : 1;
            switch (index)
            {
                case 0:
                    Answer0Visible = model.Visible;
                    break;
                case 1:
                    Answer1Visible = model.Visible;
                    break;
                case 2:
                    Answer2Visible = model.Visible;
                    break;
                case 3:
                    Answer3Visible = model.Visible;
                    break;
                case 4:
                    Answer4Visible = model.Visible;
                    break;
                case 5:
                    Answer5Visible = model.Visible;
                    break;
                case 6:
                    Answer6Visible = model.Visible;
                    break;
                case 7:
                    Answer7Visible = model.Visible;
                    break;
                default:
                    break;
            }

            _ = await _dataProcessor.ShowAnswerAsync(model);
        }

        #endregion
    }
}