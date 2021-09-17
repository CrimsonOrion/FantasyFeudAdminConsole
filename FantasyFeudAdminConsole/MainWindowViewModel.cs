using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FantasyFeudAdminConsole.Core.Processors;

using MahApps.Metro.Controls.Dialogs;

using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace FantasyFeudAdminConsole
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly IRegionManager _regionManager;
        private readonly IDataProcessor _dataProcessor;

        public static string Title => "Fantasy Feud Admin Console";

        private string _answer0 = "Answer0";
        public string Answer0
        {
            get { return _answer0; }
            set { SetProperty(ref _answer0, value); }
        }

        private string _answer1 = "Answer1";
        public string Answer1
        {
            get { return _answer1; }
            set { SetProperty(ref _answer1, value); }
        }

        private string _answer2 = "Answer2";
        public string Answer2
        {
            get { return _answer2; }
            set { SetProperty(ref _answer2, value); }
        }

        private string _answer3 = "Answer3";
        public string Answer3
        {
            get { return _answer3; }
            set { SetProperty(ref _answer3, value); }
        }

        private string _answer4 = "Answer4";
        public string Answer4
        {
            get { return _answer4; }
            set { SetProperty(ref _answer4, value); }
        }

        private string _answer5 = "Answer5";
        public string Answer5
        {
            get { return _answer5; }
            set { SetProperty(ref _answer5, value); }
        }

        private string _answer6 = "Answer6";
        public string Answer6
        {
            get { return _answer6; }
            set { SetProperty(ref _answer6, value); }
        }

        private string _answer7 = "Answer7";
        public string Answer7
        {
            get { return _answer7; }
            set { SetProperty(ref _answer7, value); }
        }

        public MainWindowViewModel(IEventAggregator eventAggregator, IDialogCoordinator dialogCoordinator, IRegionManager regionManager, IDataProcessor dataProcessor)
        {
            _eventAggregator = eventAggregator;
            _dialogCoordinator = dialogCoordinator;
            _regionManager = regionManager;
            _dataProcessor = dataProcessor;
        }
    }
}