using Entrance.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace Entrance.Models
{
    public class Podcast : BindableBase
    {
        public Podcast()
        {
            Tracks = new ObservableCollection<Track>();
        }

        public string Title { get; set; }
        public int Episode { get; set; }
        public DateTime ReleaseDate { get; set; }
        public TimeSpan Duration { get; set; }
        public long FileLength { get; set; }
        public string AudioUrl { get; set; }
        public string PostUrl { get; set; }
        public string LogoUrl { get; set; }

        private ObservableCollection<Track> _tracks;
        public ObservableCollection<Track> Tracks
        {
            get
            { return this._tracks; }
            set
            { this.SetProperty(ref this._tracks, value); }
        }

        private bool _isDownloaded;
        public bool IsDownloaded
        {
            get
            { return this._isDownloaded; }
            set
            { this.SetProperty(ref this._isDownloaded, value); }
        }

        private string _fileName;
        public string FileName
        {
            get
            { return this._fileName; }
            set
            { this.SetProperty(ref this._fileName, value); }
        }
    }
}
