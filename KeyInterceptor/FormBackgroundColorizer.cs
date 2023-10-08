using System.Drawing;
using System.Windows.Forms;

namespace KeyInterceptor
{
    internal class FormBackgroundColorizer
    {
        private static readonly Color TRANSPARENCY_KEY = Color.Red;

        public Color BackgroundColor => backgroundColor;
        public bool IsTransparent => isTransparent;

        private Form form;
        private Color backgroundColor;
        private bool isTransparent = false;

        public FormBackgroundColorizer(Form form)
        {
            this.form = form;
            backgroundColor = form.BackColor;
        }

        public void SetTransparency(bool isTransparent)
        {
            if (this.isTransparent == isTransparent)
            {
                return;
            }

            this.isTransparent = isTransparent;

            if (isTransparent)
            {
                this.form.AllowTransparency = true;
                this.form.BackColor = this.form.TransparencyKey = TRANSPARENCY_KEY;
            }
            else
            {
                this.form.BackColor = this.backgroundColor;
                this.form.AllowTransparency = false;
            }
        }

        public void SetColor(Color color)
        {
            if (color == Color.Transparent)
            {
                SetTransparency(true);
                return;
            }

            this.backgroundColor = color;
            if (!this.isTransparent)
            {
                this.form.BackColor = this.backgroundColor;
            }
        }
    }
}
