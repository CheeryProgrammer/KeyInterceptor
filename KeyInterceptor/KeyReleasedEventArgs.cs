using System;
using System.Windows.Forms;

namespace KeyInterceptor
{
	public class KeyReleasedEventArgs
	{
		public Keys KeyCode { get; }
		public DateTime PressedTimestamp { get; }
		public long Duration { get; }

		public KeyReleasedEventArgs(Keys keyCode, DateTime pressedTimestamp, long milliseconds)
		{
			KeyCode = keyCode;
			PressedTimestamp = pressedTimestamp;
			Duration = milliseconds;
		}
	}
}