using System;
using System.Drawing;
using System.Windows.Forms;

namespace KeyInterceptor
{
	public class KeyReleasedEventArgs
	{
		public Keys KeyCode { get; }
		public DateTime PressedTimestamp { get; }
		public double Duration { get; }
		public Brush Brush { get; }

		public KeyReleasedEventArgs(Keys keyCode, DateTime pressedTimestamp, double milliseconds, Brush brush)
		{
			KeyCode = keyCode;
			PressedTimestamp = pressedTimestamp;
			Duration = milliseconds;
			Brush = brush;
		}
	}
}