using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KeyInterceptor.WPF.KeyLog
{
    public class ShrinkableStackPanel : StackPanel
    {
        public event EventHandler<FreePanelSpaceEventArgs> Overflew;

        protected override Size MeasureOverride(Size constraint)
        {
            return base.MeasureOverride(constraint);
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            Overflew?.Invoke(this, new FreePanelSpaceEventArgs(arrangeSize.Height));
            return base.ArrangeOverride(arrangeSize);
        }
    }
}
