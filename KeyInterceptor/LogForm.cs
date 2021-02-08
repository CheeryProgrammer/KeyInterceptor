using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace KeyInterceptor
{
	public partial class LogForm : Form
	{
		public LogForm()
		{
			InitializeComponent();
		}

		private void TbLog_MouseClick(object sender, MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Right)
			{
				contextMenuLog.Show(e.Location);
			}
		}

		private void LogForm_Load(object sender, EventArgs e)
		{
			LoadSettings();
		}

		private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
		{
			tbLog.Clear();
		}

		private void FontToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using(var fontDialog = new FontDialog())
			{
				fontDialog.Font = tbLog.Font;

				if(DialogResult.OK == fontDialog.ShowDialog())
				{
					tbLog.Font = fontDialog.Font;
				}
			}
		}

		private void ChangeFontColorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			tbLog.ForeColor = PickColor(tbLog.ForeColor);
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
			tbLog.BackColor = PickColor(tbLog.BackColor);
		}

		public void Append(string log)
		{
			tbLog.AppendText(log + Environment.NewLine);
		}

		private void LogForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveSettings();
		}

		private void SaveSettings()
		{
			string fontColor = tbLog.ForeColor.ToArgb().ToString();
			string backColor = tbLog.BackColor.ToArgb().ToString();
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
			string font = converter.ConvertToInvariantString(tbLog.Font);
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
				tbLog.ForeColor = Color.FromArgb(int.Parse(parts[0]));
				tbLog.BackColor = Color.FromArgb(int.Parse(parts[1]));

				TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
				tbLog.Font = (Font)converter.ConvertFromInvariantString(parts[2]);
				Location = new Point(int.Parse(parts[3]), int.Parse(parts[4]));
				Width = int.Parse(parts[5]);
				Height = int.Parse(parts[6]);
			}
		}
	}
}
