using CoreTweet;
using CoreTweet.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;

namespace TwiHigh
{
    public class SearchTweet : Tweet
    {
        public SearchTweet(Status tweet)
        {
            IsFolloweeReTweeted = false;
            IsFolloweeQuoted = false;
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

        public static void ParseTweet(ObservableCollection<SearchTweet> searchView, SearchResult tweetList)
        {
            searchView.Clear();
            foreach(var tweet in tweetList)
            {
                searchView.Add(new SearchTweet(tweet));
            }
        }
    }
}