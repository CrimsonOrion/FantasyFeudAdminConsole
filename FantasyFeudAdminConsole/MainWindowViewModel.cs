
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

        private int _questionId;
        public int QuestionId
        {
            get => _questionId;
            set => SetProperty(ref _questionId, value);
        }

        public DelegateCommand GetDataCommand => new(GetData);
        public DelegateCommand NextQuestionCommand => new(NextQuestion);
        public DelegateCommand<string> ShowAnswerCommand => new(ShowAnswer);

        public MainWindowViewModel(IEventAggregator eventAggregator, IDialogCoordinator dialogCoordinator, IRegionManager regionManager, IDataProcessor dataProcessor, IWebProcessor webProcessor)
        {
            _eventAggregator = eventAggregator;
            _dialogCoordinator = dialogCoordinator;
            _regionManager = regionManager;
            _dataProcessor = dataProcessor;
            _webProcessor = webProcessor;
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

        private void NextQuestion()
        {
            QuestionData = QuestionDataList.FirstOrDefault(_ => _.Id == ++QuestionData.Id);
            QuestionId = QuestionData.Id;
        }

        private async void ShowAnswer(string index)
        {
            var answerIndex = Convert.ToInt16(index);
            _ = await _dialogCoordinator.ShowMessageAsync(this, "yay", $"You selected Answer {answerIndex}");
        }
    }
}