using Entrance.Controls;
using Entrance.Models;
using GalaSoft.MvvmLight.Command;
using Microsoft.Phone.Tasks;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Entrance.Util
{
    public static class CommandContainer
    {
        internal static void OnPodcastSelected(object episode)
        {
            string target = episode.ToString();
            Uri location = new System.Uri("/Views/EpisodeDetailView.xaml?episode=" + Uri.EscapeDataString(target), UriKind.Relative);
            App.RootFrame.Navigate(location);
        }
    }
}
