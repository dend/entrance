using Entrance.Models;
using Entrance.Util;
using GalaSoft.MvvmLight.Command;
using System.Windows;

namespace Entrance.ViewModels
{
    public class EpisodeDetailViewModel : BindableBase
    {
        static EpisodeDetailViewModel instance = null;
        static readonly object padlock = new object();

        public EpisodeDetailViewModel()
        {
        }

        public static EpisodeDetailViewModel Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new EpisodeDetailViewModel();
                    }
                    return instance;
                }
            }
        }

        private Podcast _podcast;
        public Podcast Podcast
        {
            get { return this._podcast; }
            set { this.SetProperty(ref this._podcast, value); }
        }

        private bool _isDownloading;
        public bool IsDownloading
        {
            get { return this._isDownloading; }
            set { this.SetProperty(ref this._isDownloading, value); }
        }

        private double _downloadProgress;
        public double DownloadProgress
        {
            get { return this._downloadProgress; }
            set { this.SetProperty(ref this._downloadProgress, value); }
        }

        private bool _isPlaying;
        public bool IsPlaying
        {
            get { return this._isPlaying; }
            set { this.SetProperty(ref this._isPlaying, value); }
        }
    }
}
