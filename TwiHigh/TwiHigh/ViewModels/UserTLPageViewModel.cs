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
    public class UserTLPageViewModel : ViewModelBase
    {
        public UserTLPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "UserTimeLine Page";
            LoadUserTLCommandAsync = new DelegateCommand(async () => await LoadUserTLExecuteAsync());
            UserTimeLine = new ObservableCollection<UserTLTweet>();
        }

        private bool _isNowRefreshing;
        public bool IsNowRefreshing
        {
            get { return _isNowRefreshing; }
            set { this.SetProperty(ref this._isNowRefreshing, value); }
        }

        public DelegateCommand LoadUserTLCommandAsync { get; private set; }
        private async Task LoadUserTLExecuteAsync()
        {
            this.IsNowRefreshing = true;
            UserTLTweet.ParseTweet(UserTimeLine, await TwitterAPI.LoadUserTLAsync(100));
            this.IsNowRefreshing = false;
        }

        private ObservableCollection<UserTLTweet> _userTimeLine;
        public ObservableCollection<UserTLTweet> UserTimeLine
        {
            get { return _userTimeLine; }
            set
            {
                if(_userTimeLine != value)
                {
                    _userTimeLine = value;
                    RaisePropertyChanged("UserTimeLine");
                }
            }
        }
    }
}