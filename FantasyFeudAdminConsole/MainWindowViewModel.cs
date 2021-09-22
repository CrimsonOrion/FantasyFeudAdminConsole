
using FantasyFeudAdminConsole.Core.Models;
using FantasyFeudAdminConsole.Core.Processors;

using MahApps.Metro.Controls.Dialogs;

using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

using System;
using System.Collections.Generic;
using System.Linq;

namespace FantasyFeudAdminConsole
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly IRegionManager _regionManager;
        private readonly IDataProcessor _dataProcessor;
        private readonly IWebProcessor _webProcessor;

        public static string Title => "Fantasy Feud Admin Console";

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

        private List<TeamMembersDataModel> _team1Members = new();
        public List<TeamMembersDataModel> Team1Members
        {
            get => _team1Members;
            set => SetProperty(ref _team1Members, value);
        }

        private List<TeamMembersDataModel> _team2Members = new();
        public List<TeamMembersDataModel> Team2Members
        {
            get => _team2Members;
            set => SetProperty(ref _team2Members, value);
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

        private List<AnswersDataModel> _answersData = new();
        public List<AnswersDataModel> AnswersData
        {
            get => _answersData;
            set => SetProperty(ref _answersData, value);
        }

        #endregion

        #region UI Data

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

        private string _answer0;
        public string Answer0
        {
            get { return _answer0; }
            set { SetProperty(ref _answer0, value); }
        }

        private int _answer0Value;
        public int Answer0Value
        {
            get { return _answer0Value; }
            set { SetProperty(ref _answer0Value, value); }
        }

        private int _answer0Visible;
        public int Answer0Visible
        {
            get { return _answer0Visible; }
            set { SetProperty(ref _answer0Visible, value); }
        }

        #endregion

        #endregion

        #region Commands

        public DelegateCommand AddStrikeCommand => new(AddStrike);
        public DelegateCommand NextQuestionCommand => new(NextQuestion);
        public DelegateCommand GetDataCommand => new(GetData);
        public DelegateCommand ProcessChangesCommand => new(ProcessChanges);
        public DelegateCommand<string> ShowAnswerCommand => new(ShowAnswer);

        #endregion

        public MainWindowViewModel(IEventAggregator eventAggregator, IDialogCoordinator dialogCoordinator, IRegionManager regionManager, IDataProcessor dataProcessor, IWebProcessor webProcessor)
        {
            _eventAggregator = eventAggregator;
            _dialogCoordinator = dialogCoordinator;
            _regionManager = regionManager;
            _dataProcessor = dataProcessor;
            _webProcessor = webProcessor;
        }

        private async void AddStrike()
        {

        }

        private void NextQuestion()
        {
            QuestionData = QuestionDataList.FirstOrDefault(_ => _.Id == ++QuestionData.Id);
            QuestionId = QuestionData.Id;
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
        }

        async void ProcessChanges()
        {

        }

        private async void ShowAnswer(string index)
        {
            var answerIndex = Convert.ToInt16(index);

            var model = AnswersData.FirstOrDefault(_ => _.Id == answerIndex);
            model.Visible = model.Visible == 1 ? 0 : 1;
            await _dataProcessor.ShowAnswerAsync(model);
            switch (index)
            {
                case "0":
                    Answer0Visible = model.Visible;
                    break;
                default:
                    break;
            }
        }
    }
}