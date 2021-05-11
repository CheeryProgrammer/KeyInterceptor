using KeyInterceptor.WPF.KeyLog;
using System;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

namespace KeyInterceptor.WPF
{
    class ChangeFontCommand : ICommand
	{
        private FontProperties _fontProperties;

		public event EventHandler CanExecuteChanged;

        public ChangeFontCommand(FontProperties fontProperties)
        {
            _fontProperties = fontProperties;
        }

        public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			FontDialog fontDialog = new FontDialog();
			fontDialog.ShowColor = true;
			fontDialog.ShowDialog();
			_fontProperties.FontFamily = new FontFamily(fontDialog.Font.FontFamily.Name);
			_fontProperties.FontStyle = fontDialog.Font.Style;
			_fontProperties.FontSize = fontDialog.Font.Size;
			_fontProperties.FontColor = new SolidColorBrush(
				Color.FromArgb(fontDialog.Color.A, fontDialog.Color.R, fontDialog.Color.G, fontDialog.Color.B)
				);
		}
	}
}
