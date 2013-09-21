using Entrance.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;

namespace Entrance.Converters
{
    public class PodcastCollectionCountToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ObservableCollection<Podcast> collection = (ObservableCollection<Podcast>)value;
            if (collection == null || collection.Count == 0)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
