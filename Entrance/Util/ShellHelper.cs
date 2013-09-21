using Entrance.ViewModels;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entrance.Util
{
    public static class ShellHelper
    {
        public static void SetCoreTile()
        {
            ShellTile appTile = ShellTile.ActiveTiles.First();

            if (appTile != null &&
                EpisodeDetailViewModel.Instance.Podcast != null)
            {
                ShellTileData data = new FlipTileData
                {
                    BackTitle = "ASOT",
                    BackContent = EpisodeDetailViewModel.Instance.Podcast.Title,
                    WideBackContent = EpisodeDetailViewModel.Instance.Podcast.Title,
                    WideBackBackgroundImage = new System.Uri("Images/Core/plain_wide_bg.png", System.UriKind.Relative),
                    BackBackgroundImage = new System.Uri("Images/Core/plain_wide_bg.png", System.UriKind.Relative)
                };

                appTile.Update(data);
            }
        }
    }
}
