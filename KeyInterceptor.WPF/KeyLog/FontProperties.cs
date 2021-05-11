using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace KeyInterceptor.WPF.KeyLog
{
    class FontProperties: INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private float fontSize;
		public float FontSize
		{
			get { return fontSize; }
			set
			{
				fontSize = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FontSize)));
			}
		}

		private SolidColorBrush fontColor;
		public SolidColorBrush FontColor
		{
			get { return fontColor; }
			set
			{
				fontColor = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FontColor)));
			}
		}

		private FontFamily fontFamily;
		public FontFamily FontFamily
		{
			get { return fontFamily; }
			set
			{
				fontFamily = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FontFamily)));
			}
		}

		private System.Drawing.FontStyle fontStyle;
		public System.Drawing.FontStyle FontStyle
		{
			get { return fontStyle; }
			set
			{
				fontStyle = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FontStyle)));
			}
		}
	}
}
