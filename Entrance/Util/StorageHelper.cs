using Entrance.Models;
using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace Entrance.Util
{
    public static class StorageHelper
    {
        public async static Task<bool> DeletePodcast(Podcast podcast)
        {
            StorageFolder podcastFolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Podcasts");
            StorageFile targetEpisode = await podcastFolder.GetFileAsync(podcast.FileName);

            if (targetEpisode != null)
            {
                await targetEpisode.DeleteAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
