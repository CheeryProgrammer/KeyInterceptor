using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace KeyInterceptor
{
    public class LogItem : Label
    {
        public int PaddingPcnt { get; } = 50;

        private int _padding = 0;

        public LogItem(Font font, int width)
        {
            this.AutoSize = false;
            this.BackColor = Color.Transparent;
            this.Width = width;
            Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.SetFont(font);
        }

        public void SetFont(Font font)
        {
            Font = font;
            var fontHeight = (int)(Font.Height);
            _padding = (int)((PaddingPcnt / 100.0f) * fontHeight);
            Height = fontHeight + 2 * _padding;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (var sf = new StringFormat())
            using (var br = new SolidBrush(ForeColor))
            using (GraphicsPath path = new GraphicsPath())
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                path.AddString(Text, Font.FontFamily, (int)Font.Style, Font.Height, new Point(0, (int)(_padding*0.75)), sf);
                e.Graphics.FillPath(br, path);
            }
        }
    }

    public partial class LogPanel : UserControl
    {
        static Queue<LogItem> logItems = new Queue<LogItem>();

        LogItem blTimer;

        public LogPanel()
        {
            blTimer = new LogItem(Font, Width) { Text = "time" };

            InitializeComponent();
        }

        private void LogPanel_Load(object sender, EventArgs e)
        {
            blTimer.Width = Width;
            blTimer.Location = new Point(0, Height - blTimer.Height);
            blTimer.Anchor |= AnchorStyles.Bottom;
            blTimer.MouseClick += (s, ev) => OnMouseClick(ev);
            Controls.Add(blTimer);
            RefreshItems();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RefreshItems();
        }

        private void RefreshItems()
        {
            AdjustItems();
            AdjustLocations();
        }

        private void AdjustLocations()
        {
            var yPos = 0;
            foreach (var item in logItems)
            {
                item.Location = new Point(0, yPos);
                yPos += blTimer.Height;
            }
            blTimer.Location = new Point(0, Height - blTimer.Height);
        }

        private void AdjustItems()
        {
            var height = blTimer.Height;
            var count = Height / height - 1;
            while (logItems.Count > count && logItems.Count > 0)
            {
                var item = logItems.Dequeue();
                Controls.Remove(item);
            }
        }

        public void Write(string text)
        {
            var logItem = new LogItem(Font, Width)
            {
                Text = text,
            };
            logItem.MouseClick += (s,e) => OnMouseClick(e);

            AddItem(logItem);
            RefreshItems();
        }

        private void AddItem(LogItem label)
        {
            logItems.Enqueue(label);
            Controls.Add(label);
            RefreshItems();
        }

        internal void UpdateTimer(string v)
        {
            blTimer.Text = v;
        }

        internal void Clear()
        {
            while (logItems.Count > 0)
            {
                var item = logItems.Dequeue();
                Controls.Remove(item);
            }
        }

        internal void SetFont(Font font)
        {
            Font = font;
            foreach (var li in logItems)
            {
                li.SetFont(font);
            }
            blTimer.SetFont(font);
            RefreshItems();
        }
    }
}
