using KeyInterceptor.WPF.KeyLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KeyInterceptor.WPF
{
	/// <summary>
	/// Interaction logic for LogForm.xaml
	/// </summary>
	public partial class LogForm : Window
	{
		public LogForm()
		{
			InitializeComponent();
			DataContext = new Model(() => DesiredSize.Height < tStack.DesiredSize.Height + Clock.DesiredSize.Height);
		}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
			var items = tStack.Items;
        }

        private void ShrinkableStackPanel_Overflew(object sender, FreePanelSpaceEventArgs e)
        {
            if (ClockIsOverlapped())
                (DataContext as Model).ShrinkLog();
        }

        private bool ClockIsOverlapped()
        {
            return Clock.DesiredSize.Height < Clock.ActualHeight;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
			(DataContext as Model).Add();
		}
    }
}
