using System;
using System.Diagnostics;

namespace KeyInterceptor
{
	public interface IClock : IDisposable
	{
		DateTime Now { get; }
		bool IsRunning { get; }
	}

	internal class Clock : IClock
	{
		private DateTime _startTime;
		private Stopwatch _stopwatch;

		public DateTime Now => _startTime.AddMilliseconds(_stopwatch.ElapsedMilliseconds);
		public bool IsRunning => _stopwatch.IsRunning;

		public Clock()
		{
			_startTime = DateTime.Now;
			_stopwatch = Stopwatch.StartNew();
		}

		public override string ToString()
		{
			DateTime time = _startTime.AddMilliseconds(_stopwatch.ElapsedMilliseconds);
			return time.ToString(@"HH:mm:ss.fff");
		}

		public void Dispose()
		{
			_stopwatch.Stop();
		}
	}
}
