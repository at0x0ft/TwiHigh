using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TwiHigh.ViewModels
{
    public class TweetPageViewModel : ViewModelBase
    {
        public TweetPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Tweet Page";
            Post = "";
            FixedTag = "";
            IsPosting = false;
            PostTweetCommandAsync = new DelegateCommand(async () => await PostTweetExecuteAsync(), CanPostTweetCommand)
                .ObservesProperty(() => this.Post)
                .ObservesProperty(() => this.FixedTag)
                .ObservesProperty(() => this.IsPosting);
        }

        private const int maxPostLen = 140;

        private string _post;
        public string Post
        {
            get { return _post; }
            set { this.SetProperty(ref this._post, value); }
        }

        private string _fixedTag;
        public string FixedTag
        {
            get { return _fixedTag; }
            set { this.SetProperty(ref this._fixedTag, value); }
        }

        private int TotalCount
        {
            get
            {
                int count = 0;
                if(!String.IsNullOrWhiteSpace(Post))
                {
                    count += Post.Length;
                }

                if(!String.IsNullOrWhiteSpace(FixedTag))
                {
                    if(count > 0)
                    {
                        count += FixedTag.Length + 1;
                    }
                    else
                    {
                        count += FixedTag.Length;
                    }
                }

                return count;
            }
        }

        private bool _isPosting;
        private bool IsPosting
        {
            get { return _isPosting; }
            set { this.SetProperty(ref this._isPosting, value); }
        }
        private bool CanPostTweetCommand()
        {

            return !IsPosting && TotalCount > 0 && TotalCount <= maxPostLen;
        }

        public DelegateCommand PostTweetCommandAsync { get; private set; }
        private async Task PostTweetExecuteAsync()
        {
            IsPosting = true;
            String finalPost = null;

            if(!String.IsNullOrWhiteSpace(FixedTag))
            {
                finalPost = Regex.Replace(Post, Environment.NewLine, "\n") + "\n" + Regex.Replace(FixedTag, Environment.NewLine, "\n");
            }
            else
            {
                finalPost = Regex.Replace(Post, Environment.NewLine, "\n");
            }
            await TwitterAPI.PostTweetAsync(finalPost);

            Post = null;
            IsPosting = false;
        }
    }
}