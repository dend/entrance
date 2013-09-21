using System;
using System.Linq;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Entrance.ViewModels;
using Entrance.Util;
using Entrance.Models;
using Microsoft.Phone.BackgroundAudio;
using System.Diagnostics;
using Microsoft.Xna.Framework.GamerServices;
using System.Collections.Generic;
using System.Windows.Controls;
using Entrance.Controls;
using Microsoft.Phone.Tasks;
using System.Windows;
using Nokia.Music.Tasks;

namespace Entrance.Views
{
    public partial class EpisodeDetailView : PhoneApplicationPage
    {
        public EpisodeDetailView()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (MainPageViewModel.Instance.Downloaded.Count == 0)
            {
                Initializer.GetDownloadedPodcasts();
            }

            if (NavigationContext.QueryString.ContainsKey("episode"))
            {
                string rawName = Uri.UnescapeDataString(NavigationContext.QueryString["episode"]);
                Podcast target = (from c in MainPageViewModel.Instance.Downloaded
                                  where
                                      c.Title == rawName
                                  select c).FirstOrDefault();
                if (target != null)
                {
                    EpisodeDetailViewModel.Instance.Podcast = target;
                }
                else
                {
                    EpisodeDetailViewModel.Instance.Podcast = (from c in MainPageViewModel.Instance.PodcastContainer.Podcasts
                                                               where
                                                                   c.Title == rawName
                                                               select c).FirstOrDefault();
                }

                AsotClient client = new AsotClient();
                EpisodeDetailViewModel.Instance.Podcast.Tracks =
                    new System.Collections.ObjectModel.ObservableCollection<Track>(
                        await client.GetTracksForPodcast(EpisodeDetailViewModel.Instance.Podcast));
            }


            if (EpisodeDetailViewModel.Instance.Podcast.IsDownloaded)
            {
                pathPlay.Visibility = System.Windows.Visibility.Visible;
                this.ApplicationBar.IsVisible = true;
            }
            if (BackgroundAudioPlayer.Instance.Track != null &&
                        BackgroundAudioPlayer.Instance.Track.Title == EpisodeDetailViewModel.Instance.Podcast.Title)
            {
                if (BackgroundAudioPlayer.Instance.PlayerState == PlayState.Playing)
                {
                    EpisodeDetailViewModel.Instance.IsPlaying = true;
                    pathPlay.Visibility = System.Windows.Visibility.Collapsed;
                    pathPause.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    EpisodeDetailViewModel.Instance.IsPlaying = false;
                    pathPlay.Visibility = System.Windows.Visibility.Visible;
                    pathPause.Visibility = System.Windows.Visibility.Collapsed;
                }
            }

            ShellHelper.SetCoreTile();

            base.OnNavigatedTo(e);
        }

        private async void btnAction_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (EpisodeDetailViewModel.Instance.Podcast.IsDownloaded)
            {
                // Episode is already downloaded. Play it.
                MediaHelper helper = new MediaHelper();
                if (EpisodeDetailViewModel.Instance.IsPlaying)
                {
                    if (BackgroundAudioPlayer.Instance.Track != null &&
                        BackgroundAudioPlayer.Instance.Track.Title != EpisodeDetailViewModel.Instance.Podcast.Title)
                    {
                        helper.PlayPodcast(EpisodeDetailViewModel.Instance.Podcast);

                        pathPlay.Visibility = System.Windows.Visibility.Collapsed;
                        pathPause.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    {
                        pathPlay.Visibility = System.Windows.Visibility.Visible;
                        pathPause.Visibility = System.Windows.Visibility.Collapsed;

                        BackgroundAudioPlayer.Instance.Pause();

                        EpisodeDetailViewModel.Instance.IsPlaying = false;
                    }
                }
                else
                {
                    if (BackgroundAudioPlayer.Instance.Track == null ||
                        BackgroundAudioPlayer.Instance.Track.Title != EpisodeDetailViewModel.Instance.Podcast.Title)
                    {
                        helper.PlayPodcast(EpisodeDetailViewModel.Instance.Podcast);
                    }
                    else
                    {
                        BackgroundAudioPlayer.Instance.Play();
                    }

                    EpisodeDetailViewModel.Instance.IsPlaying = true;

                    pathPlay.Visibility = System.Windows.Visibility.Collapsed;
                    pathPause.Visibility = System.Windows.Visibility.Visible;
                }
            }
            else
            {
                AsotClient client = new AsotClient();
                bool suc = await client.DownloadPodcastEpisode(EpisodeDetailViewModel.Instance.Podcast);
                EpisodeDetailViewModel.Instance.IsDownloading = false;

                if (suc)
                {
                    this.ApplicationBar.IsVisible = true;
                    pathPlay.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("There was a problem downloading this episode. Please try again later.",
                        "Download Episode", MessageBoxButton.OK);
                }

                Debug.WriteLine(suc);
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            Guide.BeginShowMessageBox("Delete Episode", "Are you sure you want to delete this podcast episode?",
                new List<string> { "yes", "no" }, 0, MessageBoxIcon.Warning, new AsyncCallback(async (result) =>
                    {
                        int? userChoice = Guide.EndShowMessageBox(result);

                        if (userChoice == 0)
                        {
                            if (BackgroundAudioPlayer.Instance.Track != null &&
                                BackgroundAudioPlayer.Instance.Track.Artist == "EnTrance")
                            {
                                BackgroundAudioPlayer.Instance.Track = null;
                            }

                            await StorageHelper.DeletePodcast(EpisodeDetailViewModel.Instance.Podcast);

                            Podcast toDelete = (from c in MainPageViewModel.Instance.Downloaded
                                                where
                                                    c.Title == EpisodeDetailViewModel.Instance.Podcast.Title
                                                select c).FirstOrDefault();

                            Podcast coreRef = (from c in MainPageViewModel.Instance.PodcastContainer.Podcasts
                                               where
                                                   c.Title == EpisodeDetailViewModel.Instance.Podcast.Title
                                               select c).FirstOrDefault();

                            Dispatcher.BeginInvoke(() =>
                                {
                                    coreRef.IsDownloaded = false;
                                    EpisodeDetailViewModel.Instance.Podcast = null;

                                    if (toDelete != null)
                                    {
                                        MainPageViewModel.Instance.Downloaded.Remove(toDelete);
                                    }

                                    GoBack();
                                });
                        }
                    }), null);
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;

            if (EpisodeDetailViewModel.Instance.IsDownloading)
            {
                Guide.BeginShowMessageBox("Go Back", "The podcast isn't yet downloaded. Going back to the main page will cancel the download. Are you sure?",
                    new List<string> { "yes", "no" }, 0, MessageBoxIcon.Warning, new AsyncCallback(async (result) =>
                        {
                            int? userChoice = Guide.EndShowMessageBox(result);

                            if (userChoice == 0)
                            {
                                // We are definitely going back.
                                EpisodeDetailViewModel.Instance.IsDownloading = false;
                                EpisodeDetailViewModel.Instance.DownloadProgress = 0; // reset the progress, since next download is from 0.

                                GoBack();
                            }
                        }), null);
            }
            else
            {
                GoBack();
            }
        }

        private void GoBack()
        {
            Dispatcher.BeginInvoke(() =>
                {
                    if (NavigationService.CanGoBack)
                    {
                        NavigationService.GoBack();
                    }
                    else
                    {
                        NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                        NavigationService.RemoveBackEntry();
                    }
                });
        }

        private void TrackContainerGrid_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            TapMenu menu = ((Grid)sender).FindName("tapMenu") as TapMenu;
            if (menu.Visibility == System.Windows.Visibility.Collapsed)
                menu.Visibility = System.Windows.Visibility.Visible;
            else
                menu.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void XBM_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Track track = (Track)((FrameworkElement)sender).Tag;

            
            MarketplaceSearchTask task = new MarketplaceSearchTask();
            task.ContentType = MarketplaceContentType.Music;
            task.SearchTerms = string.Format("{0} - {1}", track.Artist, track.Name);
            task.Show();
        }

        private void Nokia_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Track track = (Track)((FrameworkElement)sender).Tag;

            Nokia.Music.Tasks.MusicSearchTask task = new MusicSearchTask();
            task.SearchTerms = string.Format("{0} - {1}", track.Artist, track.Name);
            task.Show();
        }

        private void YouTube_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Track track = (Track)((FrameworkElement)sender).Tag;

            if (!string.IsNullOrWhiteSpace(track.YouTubeLink))
            {
                WebBrowserTask task = new WebBrowserTask();
                task.Uri = new Uri(track.YouTubeLink);
                task.Show();
            }
        }

        private void Share_Click(object sender, EventArgs e)
        {
            ShareStatusTask task = new ShareStatusTask();
            task.Status = "Listening to " + EpisodeDetailViewModel.Instance.Podcast.Title + " @asot with #EnTranced.";
            task.Show();
        }
    }
}