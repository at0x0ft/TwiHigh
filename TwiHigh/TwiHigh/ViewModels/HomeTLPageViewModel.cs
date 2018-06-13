using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwiHigh.ViewModels
{
    public class HomeTLPageViewModel : ViewModelBase
    {
        public HomeTLPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "HomeTimeLine Page";
            LoadHomeTLCommandAsync = new DelegateCommand(async () => await LoadHomeTLExecuteAsync());
            HomeTimeLine = new ObservableCollection<HomeTLTweet>();
        }

        private bool _isNowRefreshing;
        public bool IsNowRefreshing
        {
            get { return _isNowRefreshing; }
            set { this.SetProperty(ref this._isNowRefreshing, value); }
        }

        public DelegateCommand LoadHomeTLCommandAsync { get; private set; }
        private async Task LoadHomeTLExecuteAsync()
        {
            this.IsNowRefreshing = true;
            HomeTLTweet.ParseTweet(HomeTimeLine, await TwitterAPI.LoadHomeTLAsync(100));
            this.IsNowRefreshing = false;
        }

        private ObservableCollection<HomeTLTweet> _homeTimeLine;
        public ObservableCollection<HomeTLTweet> HomeTimeLine
        {
            get { return _homeTimeLine; }
            set
            {
                if(_homeTimeLine != value)
                {
                    _homeTimeLine = value;
                    RaisePropertyChanged("HomeTimeLine");
                }
            }
        }
    }
}