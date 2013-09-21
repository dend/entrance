using Entrance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Xml;

using System.Xml.Linq;
using System.ServiceModel.Syndication;
using Entrance.ViewModels;
using Windows.Storage;
using Microsoft.WindowsAzure.MobileServices;

namespace Entrance.Util
{
    public class AsotClient
    {
        public async Task<PodcastContainer> DownloadPodcasts()
        {
            PodcastContainer returnable = new PodcastContainer();

            await WebRequest.CreateHttp(Constants.PODCAST_URL).GetResponseAsync().ContinueWith(t =>
            {
                XDocument document = XDocument.Load(t.Result.GetResponseStream());
                string lookupElement = string.Format(Constants.ITUNES_FEED_PREFIX,
                    "image");
                returnable.Description = document.Root.Element("channel").Element("description").Value;
                returnable.LogoUrl = document.Root.Element("channel").Element(lookupElement).Attribute("href").Value;

                foreach (var element in document.Root.Element("channel").Elements("item"))
                {
                    Podcast podcast = new Podcast();
                    podcast.AudioUrl = element.Element("enclosure").Attribute("url").Value;
                    try
                    {
                        podcast.Duration = TimeSpan.Parse(element.Element(string.Format(Constants.ITUNES_FEED_PREFIX, "duration")).Value);
                    }
                    catch
                    {
                        podcast.Duration = new TimeSpan();
                    }
                    podcast.FileLength = Convert.ToInt64(element.Element("enclosure").Attribute("length").Value);
                    podcast.LogoUrl = element.Element(string.Format(Constants.ITUNES_FEED_PREFIX, "image")).Attribute("href").Value;
                    podcast.Title = element.Element("title").Value;

                    returnable.Podcasts.Add(podcast);
                }
            });

            return returnable;
        }

        internal async Task<bool> DownloadPodcastEpisode(Podcast podcast)
        {
            try
            {
                EpisodeDetailViewModel.Instance.IsDownloading = true;

                HttpClient client = new HttpClient();
                CancellationToken token = new CancellationToken();

                // Request the data 
                HttpResponseMessage responseMessage = await client.GetAsync(podcast.AudioUrl,
                   HttpCompletionOption.ResponseHeadersRead, token);

                if (responseMessage.IsSuccessStatusCode)
                {

                    // Get the size of the content
                    long? contentLength = responseMessage.Content.Headers.ContentLength;

                    string fileName = podcast.Title + Path.GetExtension(podcast.AudioUrl);
                    fileName = fileName.Replace(' ', '_');

                    // Create a stream for the destnation file
                    StorageFolder destinationFolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Podcasts");
                    StorageFile destinationFile = await destinationFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

                    using (Stream fileStream = await destinationFile.OpenStreamForWriteAsync())
                    {
                        // Read the content into the file stream
                        int totalNumberOfBytesRead = 0;
                        using (var responseStream = await responseMessage.Content.ReadAsStreamAsync())
                        {
                            int numberOfReadBytes;
                            do
                            {
                                // Read a data block into the buffer
                                const int bufferSize = 1048576; // 1MB
                                byte[] responseBuffer = new byte[bufferSize];

                                numberOfReadBytes = await responseStream.ReadAsync(responseBuffer, 0, responseBuffer.Length);
                                totalNumberOfBytesRead += numberOfReadBytes;

                                // Write the data block into the file stream
                                fileStream.Write(responseBuffer, 0, numberOfReadBytes);

                                // Calculate the progress
                                if (contentLength.HasValue)
                                {
                                    // Calculate the progress
                                    EpisodeDetailViewModel.Instance.DownloadProgress = (totalNumberOfBytesRead / (double)contentLength);
                                }
                                else
                                {
                                    // Just display the read bytes   

                                }
                            } while (numberOfReadBytes != 0 && EpisodeDetailViewModel.Instance.IsDownloading);

                            System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
                                {
                                    EpisodeDetailViewModel.Instance.DownloadProgress = 0;
                                    EpisodeDetailViewModel.Instance.Podcast.IsDownloaded = true;
                                    EpisodeDetailViewModel.Instance.Podcast.FileName = fileName;
                                });

                            return true;
                        }

                    }
                }
                else
                {
                    System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
                               {
                                   EpisodeDetailViewModel.Instance.DownloadProgress = 0;
                                   EpisodeDetailViewModel.Instance.Podcast.IsDownloaded = false;
                               });

                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Track>> GetTracksForPodcast(Podcast podcast)
        {
            List<Track> returnable = null;

            IMobileServiceTable<Track> trackTable = App.MobileService.GetTable<Track>();
            // This query filters out completed TodoItems. 
            returnable = await trackTable
               .Where(track => track.Association.ToLower() == podcast.Title.ToLower())
               .ToListAsync();

            return returnable;
        }
    }
}
