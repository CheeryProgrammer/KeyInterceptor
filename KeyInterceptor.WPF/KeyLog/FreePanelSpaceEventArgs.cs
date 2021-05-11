using System;

namespace KeyInterceptor.WPF.KeyLog
{
    public class FreePanelSpaceEventArgs : EventArgs
    {
        public FreePanelSpaceEventArgs(double overflow)
        {
            Overflow = overflow;
        }

        public double Overflow { get; }
    }
} 
