using Entrance.Models;
using Entrance.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Entrance.Util
{
    public class Initializer
    {
        public static void Initialize()
        {
            GetDownloadedPodcasts();
            GetCurrentPodcasts();
        }

        public async static void GetCurrentPodcasts()
        {
            AsotClient client = new AsotClient();
            MainPageViewModel.Instance.PodcastContainer = await client.DownloadPodcasts();
        }

        public async static void GetDownloadedPodcasts()
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFolder folder = await localFolder.CreateFolderAsync("Podcasts", CreationCollisionOption.OpenIfExists);
            foreach (var file in await folder.GetFilesAsync())
            {
                Podcast podcast = new Podcast();
                podcast.Title = Path.GetFileNameWithoutExtension(file.Name).Replace('_',' ');
                podcast.IsDownloaded = true;
                podcast.FileName = file.Name;

                MainPageViewModel.Instance.Downloaded.Add(podcast);
            }
        }
    }
}
