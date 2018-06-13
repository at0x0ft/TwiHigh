using CoreTweet;
using CoreTweet.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;

namespace TwiHigh
{
    public class UserTLTweet : Tweet
    {
        public UserTLTweet(Status tweet)
        {
            IsFolloweeReTweeted = tweet.RetweetedStatus != null;
            IsFolloweeQuoted = tweet.QuotedStatus != null;

            if(IsFolloweeReTweeted == true)
            {
                ReTweetMessage = tweet.User.Name + "retweeted";
                Name = tweet.RetweetedStatus.User.Name;
                UserID = "@" + tweet.RetweetedStatus.User.ScreenName;
                UserThumbnailURL = tweet.RetweetedStatus.User.ProfileImageUrlHttps;
                Date = tweet.RetweetedStatus.CreatedAt.DateTime.ToString();
                Text = Regex.Replace(tweet.RetweetedStatus.Text, "\n", Environment.NewLine);
                ClientName = "via " + Regex.Replace(tweet.RetweetedStatus.Source, "<[^>]*?>", "");
                RTNum = "RT = " + (int)tweet.RetweetedStatus.RetweetCount;
                FavNum = "Fav = " + (int)tweet.RetweetedStatus.FavoriteCount;
            }
            else
            {
                ReTweetMessage = null;
                Name = tweet.User.Name;
                UserID = "@" + tweet.User.ScreenName;
                UserThumbnailURL = tweet.User.ProfileImageUrlHttps;
                Date = tweet.CreatedAt.DateTime.ToString();
                Text = Regex.Replace(tweet.Text, "\n", Environment.NewLine);
                ClientName = "via " + Regex.Replace(tweet.Source, "<[^>]*?>", "");
                RTNum = "RT = " + (int)tweet.RetweetCount;
                FavNum = "Fav = " + (int)tweet.FavoriteCount;
            }
        }

        public static void ParseTweet(ObservableCollection<UserTLTweet> userTLView, ListedResponse<Status> tweetList)
        {
            userTLView.Clear();
            foreach(var tweet in tweetList)
            {
                userTLView.Add(new UserTLTweet(tweet));
            }
        }
    }
}