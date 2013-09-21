using Entrance.Util;
using System.Collections.ObjectModel;

namespace Entrance.Models
{
    public class PodcastContainer : BindableBase
    {
        public PodcastContainer()
        {
            Podcasts = new ObservableCollection<Podcast>();
        }

        private string _description;
        public string Description
        {
            get { return this._description; }
            set { SetProperty(ref this._description, value); }
        }

        private string _logoUrl;
        public string LogoUrl
        {
            get { return this._logoUrl; }
            set { SetProperty(ref this._logoUrl, value); }
        }

        private ObservableCollection<Podcast> _podcasts;
        public ObservableCollection<Podcast> Podcasts
        {
            get { return this._podcasts; }
            set { SetProperty(ref this._podcasts, value); }
        }
    }
}
