using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyInterceptor
{
	public partial class ClockForm : Form
	{
		private DateTime _startTime;
		private Stopwatch _stopwatch;

		public ClockForm()
		{
			InitializeComponent();
		}

		private void ClockForm_Load(object sender, EventArgs e)
		{
			LoadSettings();
			_startTime = DateTime.Now;
			_stopwatch = Stopwatch.StartNew();
			lblClock.Text = string.Empty;
			Task.Run(StartCounting);
		}

		private void StartCounting()
		{
			while (_stopwatch.IsRunning)
			{
				DateTime time = _startTime.AddMilliseconds(_stopwatch.ElapsedMilliseconds);
				lblClock.BeginInvoke((Action)(()=> { lblClock.Text = time.ToString(@"HH:mm:ss.fff"); }));
				Thread.Sleep(10);
			}
		}

		private void LblClock_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				Point menuLocation = e.Location;
				menuLocation.Offset(Location);
				menuLocation.Offset(lblClock.Location);
				contextMenuClock.Show(menuLocation);
			}
		}

		private void ChangeFontToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (var fontDialog = new FontDialog())
			{
				fontDialog.Font = lblClock.Font;

				if (DialogResult.OK == fontDialog.ShowDialog())
				{
					lblClock.Font = fontDialog.Font;
				}
			}
		}

		private void ChangeFontColorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			lblClock.ForeColor = PickColor(lblClock.ForeColor);
		}

		private void ChangeBackColorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			lblClock.BackColor = PickColor(lblClock.BackColor);
		}

		private Color PickColor(Color sourceColor)
		{
			using (var colorDialog = new ColorDialog())
			{
				colorDialog.Color = sourceColor;

				if (DialogResult.OK == colorDialog.ShowDialog())
				{
					return colorDialog.Color;
				}

				return sourceColor;
			}
		}

		private void ClockForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			_stopwatch.Stop();
			SaveSettings();
		}

		private void SaveSettings()
		{
			string fontColor = lblClock.ForeColor.ToArgb().ToString();
			string backColor = lblClock.BackColor.ToArgb().ToString();
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
			string font = converter.ConvertToInvariantString(lblClock.Font);
			string posX = Location.X.ToString();
			string posY = Location.Y.ToString();
			string width = Width.ToString();
			string height = Height.ToString();
			string topMost = TopMost.ToString();
			File.WriteAllText("clock_settings.txt", $"{fontColor}|{backColor}|{font}|{posX}|{posY}|{width}|{height}|{topMost}");
		}

		private void LoadSettings()
		{
			if (File.Exists("clock_settings.txt"))
			{
				var parts = File.ReadAllText("clock_settings.txt").Split('|');
				lblClock.ForeColor = Color.FromArgb(int.Parse(parts[0]));
				lblClock.BackColor = Color.FromArgb(int.Parse(parts[1]));

				TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
				lblClock.Font = (Font)converter.ConvertFromInvariantString(parts[2]);
				Location = new Point(int.Parse(parts[3]), int.Parse(parts[4]));
				Width = int.Parse(parts[5]);
				Height = int.Parse(parts[6]);
				TopMost = bool.Parse(parts[7]);
			}
		}

		private void SwitchTopMostToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TopMost = !TopMost;

			if(sender is ToolStripMenuItem menuItem)
			{
				menuItem.Checked = TopMost;
			}
		}
	}
}
