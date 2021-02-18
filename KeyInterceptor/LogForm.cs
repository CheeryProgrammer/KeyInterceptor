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
	public partial class LogForm : Form
	{
		private Clock _clock;
		private Label invisibleLabelForFocus;

		public LogForm()
		{
			InitializeComponent();
		}

		private void LogForm_Load(object sender, EventArgs e)
		{
			LoadSettings();
			AlignLog();
			_clock = new Clock();
			Task.Factory.StartNew(UpdateClock, TaskCreationOptions.LongRunning);


			invisibleLabelForFocus = new Label();
			invisibleLabelForFocus.SendToBack();
			this.Controls.Add(invisibleLabelForFocus);

			lbLog.GotFocus += (s, a) => invisibleLabelForFocus.Focus();
			rtbClock.GotFocus += (s, a) => invisibleLabelForFocus.Focus();
			rtbClock.Height = lbLog.ItemHeight;
			lbLog.Anchor |= AnchorStyles.Bottom;
		}

		private void UpdateClock()
		{
			while (_clock.IsRunning)
			{
				rtbClock.BeginInvoke((Action)UpdateClockText);
				Thread.Sleep(10);
			}
		}

		private void UpdateClockText()
		{
			rtbClock.Text = _clock.ToString();
		}

		private void LbLog_MouseClick(object sender, MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Right)
			{
				Point menuLocation = e.Location;
				menuLocation.Offset(Location);
				menuLocation.Offset(lbLog.Location);
				contextMenuLog.Show(menuLocation);
			}
		}

		private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
		{
			while(lbLog.Items.Count > 1)
				lbLog.Items.RemoveAt(0);
		}

		private void FontToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using(var fontDialog = new FontDialog())
			{
				fontDialog.Font = lbLog.Font;

				if(DialogResult.OK == fontDialog.ShowDialog())
				{
					lbLog.Font = rtbClock.Font = fontDialog.Font;
					lbLog.Anchor ^= AnchorStyles.Bottom;
					rtbClock.Height = lbLog.ItemHeight;
					ClientSize = new Size(ClientSize.Width, lbLog.Height + lbLog.ItemHeight);
					lbLog.Anchor |= AnchorStyles.Bottom;
				}
			}
		}

		private void ChangeFontColorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			lbLog.ForeColor = rtbClock.ForeColor = PickColor(lbLog.ForeColor);
		}

		private Color PickColor(Color sourceColor)
		{
			using(var colorDialog = new ColorDialog())
			{
				colorDialog.Color = sourceColor;

				if (DialogResult.OK == colorDialog.ShowDialog())
				{
					return colorDialog.Color;
				}

				return sourceColor;
			}
		}

		private void ChangeBackColorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			BackColor = lbLog.BackColor = rtbClock.BackColor = PickColor(lbLog.BackColor);
		}

		public void Append(string log)
		{
			AlignLog(1);
			lbLog.Items.Insert(lbLog.Items.Count, log);
		}

		private void LogForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveSettings();
			_clock?.Dispose();
		}

		private void SaveSettings()
		{
			string fontColor = lbLog.ForeColor.ToArgb().ToString();
			string backColor = lbLog.BackColor.ToArgb().ToString();
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
			string font = converter.ConvertToInvariantString(lbLog.Font);
			string posX = Location.X.ToString();
			string posY = Location.Y.ToString();
			string width = Width.ToString();
			string height = Height.ToString();
			string topMost = TopMost.ToString();
			File.WriteAllText("log_settings.txt", $"{fontColor}|{backColor}|{font}|{posX}|{posY}|{width}|{height}|{topMost}");
		}

		private void LoadSettings()
		{
			if (File.Exists("log_settings.txt"))
			{
				var parts = File.ReadAllText("log_settings.txt").Split('|');
				rtbClock.ForeColor = lbLog.ForeColor = Color.FromArgb(int.Parse(parts[0]));
				BackColor = rtbClock.BackColor = lbLog.BackColor = Color.FromArgb(int.Parse(parts[1]));

				TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
				rtbClock.Font = lbLog.Font = (Font)converter.ConvertFromInvariantString(parts[2]);
				Location = new Point(int.Parse(parts[3]), int.Parse(parts[4]));
				Width = int.Parse(parts[5]);
				Height = int.Parse(parts[6]);
				TopMost = bool.Parse(parts[7]);
			}
		}

		private void LogForm_Resize(object sender, EventArgs e)
		{
			AlignLog();
		}

		private void AlignLog(int remainCount = 0)
		{
			while ((ClientSize.Height < (lbLog.Items.Count + 1 + remainCount) * lbLog.ItemHeight) && lbLog.Items.Count > 0)
			{
				lbLog.Items.RemoveAt(0);
			}
		}

		private void LogForm_ResizeEnd(object sender, EventArgs e)
		{
			lbLog.ClientSize = ClientSize;
			var toCropY = ClientSize.Height % lbLog.ItemHeight;
			ClientSize = new Size(ClientSize.Width, ClientSize.Height - toCropY);
		}

		private void SwitchTopMostToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TopMost = !TopMost;
			if(sender is ToolStripMenuItem menuItem)
			{
				menuItem.Checked = TopMost;
			}
		}

		private class Clock: IDisposable
		{
			private DateTime _startTime;
			private Stopwatch _stopwatch;

			public bool IsRunning => _stopwatch.IsRunning;

			public Clock()
			{
				_startTime = DateTime.Now;
				_stopwatch = Stopwatch.StartNew();
			}

			public override string ToString()
			{
				DateTime time = _startTime.AddMilliseconds(_stopwatch.ElapsedMilliseconds);
				return time.ToString(@"HH:mm:ss.ff");
			}

			public void Dispose()
			{
				_stopwatch.Stop();
			}
		}

		private void lbLog_SelectedIndexChanged(object sender, EventArgs e)
		{
			lbLog.ClearSelected();
		}
	}
}
