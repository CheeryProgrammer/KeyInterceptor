using System;
using System.Diagnostics;

namespace KeyInterceptor.WPF.Time
{
    class Clock: IDisposable
    {
        public DateTime Now => _startDateTime.AddTicks(_internalClock.ElapsedTicks);

        private readonly DateTime _startDateTime;
        private readonly Stopwatch _internalClock;

        public Clock()
        {
            _startDateTime = DateTime.Now;
            _internalClock = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            _internalClock.Stop();
        }

        public override string ToString()
        {
            return Now.ToString(@"HH:mm:ss.fff");
        }
    }
}
