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
    public class SearchPageViewModel : ViewModelBase
    {
        public SearchPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Search Page";
            SearchWord = null;
            SearchTweetCommandAsync = new DelegateCommand(async () => await SearchTweetExecuteAsync(), CanSearchTweetCommand)
                .ObservesProperty(() => this.SearchWord);
            Result = new ObservableCollection<SearchTweet>();
        }

        private string _searchWord;
        public string SearchWord
        {
            get { return _searchWord; }
            set { this.SetProperty(ref this._searchWord, value); }
        }
        private bool CanSearchTweetCommand()
        {
            return !String.IsNullOrWhiteSpace(SearchWord);
        }

        public DelegateCommand SearchTweetCommandAsync { get; private set; }
        private async Task SearchTweetExecuteAsync()
        {
            SearchTweet.ParseTweet(Result, await TwitterAPI.SearchKeywordAsync(100, SearchWord));
        }

        private ObservableCollection<SearchTweet> _result;
        public ObservableCollection<SearchTweet> Result
        {
            get { return _result; }
            set
            {
                if(_result != value)
                {
                    _result = value;
                    RaisePropertyChanged("Result");
                }
            }
        }
    }
}