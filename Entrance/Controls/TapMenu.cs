using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Entrance.Controls
{
    public class TapMenu : ItemsControl
    {
        public TapMenu()
        {
            DefaultStyleKey = typeof(TapMenu);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}
