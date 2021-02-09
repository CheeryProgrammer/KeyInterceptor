using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace KeyInterceptor
{
	public partial class LogForm : Form
	{
		public LogForm()
		{
			InitializeComponent();
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

		private void LogForm_Load(object sender, EventArgs e)
		{
			LoadSettings();
		}

		private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
		{
			lbLog.Items.Clear();
		}

		private void FontToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using(var fontDialog = new FontDialog())
			{
				fontDialog.Font = lbLog.Font;

				if(DialogResult.OK == fontDialog.ShowDialog())
				{
					lbLog.Font = fontDialog.Font;
				}
			}
		}

		private void ChangeFontColorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			lbLog.ForeColor = PickColor(lbLog.ForeColor);
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
			lbLog.BackColor = PickColor(lbLog.BackColor);
		}

		public void Append(string log)
		{
			while(lbLog.ClientSize.Height < (lbLog.Items.Count+1) * lbLog.ItemHeight)
			{
				lbLog.Items.RemoveAt(0);
			}
			lbLog.Items.Insert(lbLog.Items.Count, log);
		}

		private void LogForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveSettings();
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
			File.WriteAllText("log_settings.txt", $"{fontColor}|{backColor}|{font}|{posX}|{posY}|{width}|{height}");
		}

		private void LoadSettings()
		{
			if (File.Exists("log_settings.txt"))
			{
				var parts = File.ReadAllText("log_settings.txt").Split('|');
				lbLog.ForeColor = Color.FromArgb(int.Parse(parts[0]));
				lbLog.BackColor = Color.FromArgb(int.Parse(parts[1]));

				TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
				lbLog.Font = (Font)converter.ConvertFromInvariantString(parts[2]);
				Location = new Point(int.Parse(parts[3]), int.Parse(parts[4]));
				Width = int.Parse(parts[5]);
				Height = int.Parse(parts[6]);
			}
		}
	}
}
