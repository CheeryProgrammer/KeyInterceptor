using KeyInterceptor.Properties;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyInterceptor
{
	public partial class ButtonView : PictureBox
	{
		private static readonly KeyNamesMap _keyMap = new KeyNamesMap();

		private Point _cursorLocation;
		private ToolTip _toolTip = new ToolTip();
		private Keys? _keyCode;

		private DateTime _pressedTimestamp;

		private Bitmap _image = null;
		private Bitmap _activeImage = null;

		private volatile bool _pressed = false;

		public string ImagePath { get; private set; }
		public string ActiveImagePath { get; private set; }

		public event EventHandler<KeyReleasedEventArgs> KeyReleased;

		public Keys? KeyCode
		{
			get
			{
				return _keyCode;
			}
			set
			{
				_toolTip.SetToolTip(this, _keyMap.Map(value?.ToString()) ?? "Не назначена");
				_keyCode = value;
			}
		}

		public ButtonView(): this(null, 10, 10, 40 ,40, null, null)
		{
			Image = Resources.ButtonDefault;
			KeyCode = null;
		}

		public ButtonView(Keys? keyCode, int x, int y, int width, int height, string imagePath, string activeImagePath) : base()
		{
			KeyCode = keyCode;
			Location = new Point(x, y);
			Width = width;
			Height = height;
			SetImage(imagePath);
			SetActiveImage(activeImagePath);
			SizeMode = PictureBoxSizeMode.StretchImage;
			MouseDown += ButtonView_MouseDown;
			MouseMove += ButtonView_MouseMove;
			MouseUp += Redraw;
			UnPress(DateTime.MinValue);
		}

		private void Redraw(object sender, MouseEventArgs e)
		{
			Invalidate();
			Update();
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
			Graphics g = e.Graphics;

			if (this.Parent != null)
			{
				var index = Parent.Controls.GetChildIndex(this);
				for (var i = Parent.Controls.Count - 1; i > index; i--)
				{
					var c = Parent.Controls[i];
					if (c.Bounds.IntersectsWith(Bounds) && c.Visible)
					{
						using (var bmp = new Bitmap(c.Width, c.Height, g))
						{
							c.DrawToBitmap(bmp, c.ClientRectangle);
							g.TranslateTransform(c.Left - Left, c.Top - Top);
							g.DrawImageUnscaled(bmp, Point.Empty);
							g.TranslateTransform(Left - c.Left, Top - c.Top);
						}
					}
				}
			}
		}

		private void ButtonView_MouseDown(object sender, MouseEventArgs e)
		{
			this.BringToFront();

			if(e.Button == MouseButtons.Left)
			{
				_cursorLocation = e.Location;
			}
		}

		private void ButtonView_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.Left = e.X + this.Left - _cursorLocation.X;
				this.Top = e.Y + this.Top - _cursorLocation.Y;
				Redraw(null, null);
			}
		}

		public void Press(DateTime pressTimestamp)
		{
			if (!_pressed)
			{
				_pressed = true;
				_pressedTimestamp = pressTimestamp;
				BeginInvoke((Action)(() => { this.Image = _activeImage; }));
			}
		}

		public void UnPress(DateTime unpressTimestamp)
		{
			_pressed = false;
			Task.Run(() =>
			{
				KeyReleased?.Invoke(this, new KeyReleasedEventArgs(KeyCode.Value, _pressedTimestamp, (unpressTimestamp - _pressedTimestamp).TotalMilliseconds));
				this.Image = _image;
			});
		}

		internal void SetImage(string imagePath)
		{
			ImagePath = imagePath;
			_image = imagePath == null || !File.Exists(imagePath)
				? Resources.ButtonDefault
				: new Bitmap(imagePath);
			UnPress(DateTime.MinValue);
		}

		internal void SetActiveImage(string activeImagePath)
		{
			ActiveImagePath = activeImagePath;
			_activeImage = activeImagePath == null || !File.Exists(activeImagePath)
				? Resources.ActiveButtonDefault
				: new Bitmap(activeImagePath);
		}

		protected override void Dispose(bool disposing)
		{
			MouseDown -= ButtonView_MouseDown;
			MouseMove -= ButtonView_MouseMove;
			MouseUp -= Redraw;

			base.Dispose(disposing);
		}
	}
}
