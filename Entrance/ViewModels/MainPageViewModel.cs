using Entrance.Models;
using Entrance.Util;
using System.Collections.ObjectModel;

namespace Entrance.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        static MainPageViewModel instance = null;
        static readonly object padlock = new object();

        public MainPageViewModel()
        {
            PodcastSelectedCommand = new RelayCommand(CommandContainer.OnPodcastSelected);

            Downloaded = new ObservableCollection<Podcast>();
        }

        public static MainPageViewModel Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new MainPageViewModel();
                    }
                    return instance;
                }
            }
        }

        public RelayCommand PodcastSelectedCommand { get; set; }
        
        private PodcastContainer _podcastContainer;
        public PodcastContainer PodcastContainer
        {
            get { return this._podcastContainer; }
            set { this.SetProperty(ref this._podcastContainer, value); }
        }

        private ObservableCollection<Podcast> _downloaded;
        public ObservableCollection<Podcast> Downloaded
        {
            get { return this._downloaded; }
            set { this.SetProperty(ref this._downloaded, value); }
        }
    }
}
