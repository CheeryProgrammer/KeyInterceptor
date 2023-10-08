using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyInterceptor
{
    public partial class LogForm : Form
    {
        private IClock _clock;
        private Label invisibleLabelForFocus;
        private Brush defaultForegroundBrush;

        private ToolStripMenuItem _transparencyMenu;
        private bool _isTransparentBackground;
        private Color _basicBackColor;
        private Color BackgroundColor
        {
            get
            {
                return _isTransparentBackground ? this._basicBackColor : this.BackColor;
            }
        }

        public LogForm(IClock clock)
        {
            InitializeComponent();
            _clock = clock;
        }

        private void LogForm_Load(object sender, EventArgs e)
        {
            _transparencyMenu = this.contextMenuLog.Items[3] as ToolStripMenuItem;
            LoadSettings();

            Task.Factory.StartNew(UpdateClock, TaskCreationOptions.LongRunning);


            invisibleLabelForFocus = new Label();
            invisibleLabelForFocus.SendToBack();
            this.Controls.Add(invisibleLabelForFocus);

            logPanel1.MouseClick += LbLog_MouseClick;

            //UpdateLogItemHeight();
            //AlignLogClientSize();
            //AlignLogItems();
            //this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //this.TransparencyKey = this.BackColor = Color.Blue;
        }

        private void UpdateClock()
        {
            while (_clock.IsRunning && !IsDisposed)
            {
                logPanel1.BeginInvoke((Action)UpdateClockText);
                Thread.Sleep(10);
            }
        }

        private void UpdateClockText()
        {
            logPanel1.UpdateTimer(_clock.ToString());
        }

        private void LbLog_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point menuLocation = e.Location;
                menuLocation.Offset(Location);
                menuLocation.Offset(logPanel1.Location);
                contextMenuLog.Show(menuLocation);
            }
        }

        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logPanel1.Clear();
        }

        private void FontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fontDialog = new FontDialog())
            {
                fontDialog.Font = logPanel1.Font;
                fontDialog.AllowVectorFonts = true;
                fontDialog.AllowVerticalFonts = true;

                if (DialogResult.OK == fontDialog.ShowDialog())
                {
                    //UpdateLogItemHeight();
                    //AlignLogClientSize();
                    //AlignLogItems();
                    //AlignLogClientSize();
                    logPanel1.SetFont(fontDialog.Font);
                }
            }
        }

        private void ChangeFontColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logPanel1.ForeColor = PickColor(logPanel1.ForeColor);
            defaultForegroundBrush = new SolidBrush(logPanel1.ForeColor);
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

        private void ChangeBackColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackColor = logPanel1.BackColor = PickColor(logPanel1.BackColor);
        }

        public void Append(ref LogEntry logItem)
        {
           // AlignLogItems(1);
            logPanel1.Write(logItem.Text);
        }

        private void LogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void SaveSettings()
        {
            string fontColor = logPanel1.ForeColor.ToArgb().ToString();
            string backColor = BackgroundColor.ToArgb().ToString();
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
            string font = converter.ConvertToInvariantString(logPanel1.Font);
            string posX = Location.X.ToString();
            string posY = Location.Y.ToString();
            string width = Width.ToString();
            string height = Height.ToString();
            string topMost = TopMost.ToString();
            bool setTransparency = _isTransparentBackground;
            File.WriteAllText("log_settings.txt", $"{fontColor}|{backColor}|{font}|{posX}|{posY}|{width}|{height}|{topMost}|{setTransparency}");
        }

        private void LoadSettings()
        {
            if (File.Exists("log_settings.txt"))
            {
                var parts = File.ReadAllText("log_settings.txt").Split('|');
                logPanel1.ForeColor = Color.FromArgb(int.Parse(parts[0]));
                defaultForegroundBrush = new SolidBrush(logPanel1.ForeColor);
                _basicBackColor = Color.FromArgb(int.Parse(parts[1]));

                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
                logPanel1.SetFont((Font)converter.ConvertFromInvariantString(parts[2]));
                Location = new Point(int.Parse(parts[3]), int.Parse(parts[4]));
                Width = int.Parse(parts[5]);
                Height = int.Parse(parts[6]);
                TopMost = bool.Parse(parts[7]);
                SetTransparency(bool.Parse(parts[8]));
            }
            else
            {
                defaultForegroundBrush = Brushes.Black;
            }
        }

        //private void LogForm_Resize(object sender, EventArgs e)
        //{
        //    AlignLogItems();
        //}

        //private void AlignLogItems(int remainCount = 0)
        //{
        //    //while ((ClientSize.Height < (lbLog.Items.Count + 1 + remainCount) * lbLog.ItemHeight) && lbLog.Items.Count > 0)
        //    //{
        //    //    lbLog.Items.RemoveAt(0);
        //    //}
        //}

        //private void LogForm_ResizeEnd(object sender, EventArgs e)
        //{
        //    AlignLogClientSize();
        //    //lbLog.Invalidate();
        //    //lbLog.Update();
        //}

        //private void AlignLogClientSize()
        //{
        //    //return;
        //    //lbLog.ClientSize = ClientSize;
        //    //var toCropY = ClientSize.Height % lbLog.ItemHeight;
        //    //ClientSize = new Size(ClientSize.Width, ClientSize.Height - toCropY);
        //}

        //private void UpdateLogItemHeight()
        //{
        //    //lbLog.ItemHeight = (int)Graphics.FromHwnd(lbLog.Handle).MeasureString("text", lbLog.Font, Screen.GetBounds(this).Width, StringFormat.GenericDefault).Height + 2;
        //    //rtbClock.Height = lbLog.ItemHeight;
        //}

        private void SwitchTopMostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TopMost = !TopMost;
            if (sender is ToolStripMenuItem menuItem)
            {
                menuItem.Checked = TopMost;
            }
        }

        //private void lbLog_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //lbLog.ClearSelected();
        //}

        private void NoBordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem.Checked)
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                menuItem.Checked = false;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.None;
                menuItem.Checked = true;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawString("Hello", Font, Brushes.Black, 100, 10);
            //base.OnPaint(e);
        }

        private void SetTransparentBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetTransparency(!_isTransparentBackground);
        }

        private void SetTransparency(bool isTransparentBackground)
        {
            if (_isTransparentBackground == isTransparentBackground)
            {
                return;
            }
            if (_isTransparentBackground)
            {
                _isTransparentBackground = false;
                TransparencyKey = Color.Empty;
                //BackColor = rtbClock.BackColor = lbLog.BackColor = _basicBackColor;
            }
            else
            {
                _isTransparentBackground = true;
                this.TransparencyKey = this.BackColor = Color.Red;
                //lbLog.BackColor = Color.Transparent;
                //rtbClock.BackColor = Color.Transparent;
                
            }
            _transparencyMenu.Checked = _isTransparentBackground;
        }
    }
}
