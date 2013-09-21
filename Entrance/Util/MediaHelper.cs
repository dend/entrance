using Entrance.Models;
using Microsoft.Devices;
using Microsoft.Phone.BackgroundAudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Resources;

namespace Entrance.Util
{
    public class MediaHelper
    {
        private void SetMediaItem(Podcast podcast)
        {
            MediaHistoryItem item = new MediaHistoryItem();

            StreamResourceInfo localImage = Application.GetResourceStream(new Uri("Images/Core/entr_media.jpg", UriKind.Relative));
            item.ImageStream = localImage.Stream;
            item.Source = "";
            item.Title = "ASOT";
            item.PlayerContext.Add("EnTrance", podcast.Title);
            MediaHistory.Instance.NowPlaying = item;

            localImage = Application.GetResourceStream(new Uri("Images/Core/entr_media_micro.jpg", UriKind.Relative));
            item.ImageStream = localImage.Stream;
            MediaHistory.Instance.WriteRecentPlay(item);
        }

        public void PlayPodcast(Podcast podcast)
        {
            BackgroundAudioPlayer.Instance.Track = new AudioTrack(new Uri(string.Format("Podcasts/{0}", podcast.FileName), UriKind.Relative), podcast.Title,
                 "EnTrance", "EnTrance", new Uri("Images/Core/entr_media.jpg", UriKind.Relative),null,EnabledPlayerControls.All);

            BackgroundAudioPlayer.Instance.Play();
            SetMediaItem(podcast);
        }
    }
}
