using KeyInterceptor.Properties;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace KeyInterceptor
{
	public partial class ButtonView : PictureBox
	{
		private Point _cursorLocation;
		private ToolTip _toolTip = new ToolTip();
		private Keys? _keyCode;

		private Bitmap _image = null;
		private Bitmap _activeImage = null;

		public string ImagePath { get; private set; }
		public string ActiveImagePath { get; private set; }

		public Keys? KeyCode
		{
			get
			{
				return _keyCode;
			}
			set
			{
				_toolTip.SetToolTip(this, value?.ToString() ?? "Не назначена");
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
			UnPress();
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
			}
		}

		public void Press()
		{
			this.Image = _activeImage;
		}

		public void UnPress()
		{
			this.Image = _image;
		}

		internal void SetImage(string imagePath)
		{
			ImagePath = imagePath;
			_image = imagePath == null || !File.Exists(imagePath)
				? Resources.ButtonDefault
				: new Bitmap(imagePath);
			UnPress();
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

			base.Dispose(disposing);
		}
	}
}
