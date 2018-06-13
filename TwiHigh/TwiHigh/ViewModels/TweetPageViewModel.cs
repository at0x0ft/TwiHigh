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
    public class TweetPageViewModel : ViewModelBase
    {
        public TweetPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Tweet Page";
            Post = null;
            IsPosting = false;
            PostTweetCommandAsync = new DelegateCommand(async () => await PostTweetExecuteAsync(), CanPostTweetCommand)
                .ObservesProperty(() => this.Post)
                .ObservesProperty(() => this.IsPosting);
        }

        private string _post;
        public string Post
        {
            get { return _post; }
            set { this.SetProperty(ref this._post, value); }
        }
        private bool _isPosting;
        private bool IsPosting
        {
            get { return _isPosting; }
            set { this.SetProperty(ref this._isPosting, value); }
        }
        private bool CanPostTweetCommand()
        {
            return !String.IsNullOrWhiteSpace(Post) && !IsPosting;
        }

        public DelegateCommand PostTweetCommandAsync { get; private set; }
        private async Task PostTweetExecuteAsync()
        {
            IsPosting = true;
            await TwitterAPI.PostTweetAsync(Post);
            Post = null;
            IsPosting = false;
        }
    }
}