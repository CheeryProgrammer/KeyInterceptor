using System;
using System.Windows;
using System.Windows.Controls;

namespace KeyInterceptor.WPF.KeyLog
{
    public class ShrinkableStackPanel : StackPanel
    {
        public event EventHandler<FreePanelSpaceEventArgs> Arranged;

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            var newSize = base.ArrangeOverride(arrangeSize);
            Arranged?.Invoke(this, new FreePanelSpaceEventArgs(arrangeSize.Height));
            return newSize;
        }
    }
}
