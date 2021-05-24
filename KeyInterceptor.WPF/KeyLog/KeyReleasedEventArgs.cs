using System;
using System.Windows.Input;

namespace KeyInterceptor.WPF.KeyLog
{
    internal class KeyReleasedEventArgs : EventArgs
    {
        public DateTime PressedTimeStamp { get; }
        public Key KeyCode { get; }
        public double DurationMs { get; }

        public KeyReleasedEventArgs(Key keyCode, double durationMs, DateTime startTime)
        {
            KeyCode = keyCode;
            DurationMs = durationMs;
            PressedTimeStamp = startTime;
        }
    }
}