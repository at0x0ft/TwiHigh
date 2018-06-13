using CoreTweet;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwiHigh
{
    public class Tweet
    {
        public bool IsFolloweeReTweeted { get; set; }
        public bool IsFolloweeQuoted { get; set; }
        public string ReTweetMessage { get; set; }

        public string Name { get; set; }
        public string UserID { get; set; }
        public string UserThumbnailURL { get; set; }
        public string Date { get; set; }
        public string Text { get; set; }
        public string RTNum { get; set; }
        public string FavNum { get; set; }
        public string ClientName { get; set; }

    }
}