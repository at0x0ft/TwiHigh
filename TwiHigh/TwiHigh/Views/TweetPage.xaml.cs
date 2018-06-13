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
using Xamarin.Forms;

namespace TwiHigh.Views
{
    public partial class TweetPage : ContentPage
    {
        public TweetPage()
        {
            InitializeComponent();
        }

        private int TotalCount
        {
            get
            {
                int count = 0;
                if(!String.IsNullOrWhiteSpace(this.Post.Text))
                {
                    count += this.Post.Text.Length;
                }

                if(!String.IsNullOrWhiteSpace(this.FixedTag.Text))
                {
                    if(count > 0)
                    {
                        count += this.FixedTag.Text.Length + 1;
                    }
                    else
                    {
                        count += this.FixedTag.Text.Length;
                    }
                }

                return count;
            }
        }

        private void Post_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.TotalWordCount.Text = TotalCount.ToString();
        }

        private void FixedTag_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.TotalWordCount.Text = TotalCount.ToString();
        }
    }
}
