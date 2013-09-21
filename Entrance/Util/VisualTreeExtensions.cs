using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Entrance.Util
{
    public static class VisualTreeExtensions
    {
        public static T GetVisualAncestor<T>(this FrameworkElement d) where T : class
        {
            DependencyObject item = VisualTreeHelper.GetParent(d);

            while (item != null)
            {
                T itemAsT = item as T;
                if (itemAsT != null) return itemAsT;
                item = VisualTreeHelper.GetParent(item);
            }

            return null;
        }
    }
}
