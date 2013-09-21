using Entrance.Models;
using Entrance.Util;
using Entrance.ViewModels;
using Microsoft.Phone.Controls;
using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Entrance
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void InsertTracks()
        {
            Track track = new Track();
            track.Artist = "Dash Berlin feat. Christina Novelli";
            track.Name = "Jar Of Hearts (Club Mix) [Aropa]";
            track.YouTubeLink = "http://www.youtube.com/watch?v=ben-Sy50nFQ";
            track.Association = "A State of Trance Official Podcast Episode 283";
            await App.MobileService.GetTable<Track>().InsertAsync(track);

            track = new Track();
            track.Artist = "Arisen Flame";
            track.Name = "Gladius (Original Mix) [Armada]";
            track.YouTubeLink = "http://www.youtube.com/watch?v=c9zIkSrQX90";
            track.Association = "A State of Trance Official Podcast Episode 283"; 
            await App.MobileService.GetTable<Track>().InsertAsync(track);

            track = new Track();
            track.Artist = "Omnia";
            track.Name = "Immersion (Original Mix) [Armind]";
            track.YouTubeLink = "http://www.youtube.com/watch?v=A56TNNHW2sE";
            track.Association = "A State of Trance Official Podcast Episode 283";
            await App.MobileService.GetTable<Track>().InsertAsync(track);

            track = new Track();
            track.Artist = "Rex Mundi";
            track.Name = "Aureolo (Original Mix) [Armind]";
            track.YouTubeLink = "http://www.youtube.com/watch?v=9cKGvFoMKHA";
            track.Association = "A State of Trance Official Podcast Episode 283";
            await App.MobileService.GetTable<Track>().InsertAsync(track);

            track = new Track();
            track.Artist = "Andrew Rayel & Alexandre Bergheau";
            track.Name = "We Are Not Afraid of 138 (Original Mix) [WAO138]";
            track.YouTubeLink = "http://www.youtube.com/watch?v=9c3DZ7C9ujo";
            track.Association = "A State of Trance Official Podcast Episode 283";
            await App.MobileService.GetTable<Track>().InsertAsync(track);

            track = new Track();
            track.Artist = "Armin van Buuren";
            track.Name = "Who’s Afraid of 138!? (Photographer Remix)";
            track.YouTubeLink = "http://www.youtube.com/watch?v=2XlTEocKIWs";
            track.Association = "A State of Trance Official Podcast Episode 283";
            await App.MobileService.GetTable<Track>().InsertAsync(track);

        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            ((Storyboard)this.Resources["TranslationStoryboard"]).Begin();

            if (NavigationContext.QueryString.ContainsKey("EnTrance"))
            {
                Uri location = new System.Uri("/Views/EpisodeDetailView.xaml?episode=" + NavigationContext.QueryString["EnTrance"], UriKind.Relative);
                NavigationService.Navigate(location);
                NavigationService.RemoveBackEntry();
            }

            base.OnNavigatedTo(e);
        }

        private void About_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/AboutView.xaml", UriKind.Relative));
        }
    }
}