using System;
using System.Windows.Forms;

namespace KeyInterceptor
{
	public class KeyReleasedEventArgs
	{
		public Keys KeyCode { get; }
		public DateTime PressedTimestamp { get; }
		public double Duration { get; }

		public KeyReleasedEventArgs(Keys keyCode, DateTime pressedTimestamp, double milliseconds)
		{
			KeyCode = keyCode;
			PressedTimestamp = pressedTimestamp;
			Duration = milliseconds;
		}
	}
}