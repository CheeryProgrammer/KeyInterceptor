using System.Windows.Forms;

namespace KeyInterceptor
{
	public partial class KeyBindingForm : Form
	{
		public Keys? KeyCode { get; private set; }

		public KeyBindingForm()
		{
			InitializeComponent();
		}

		private void BtnCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			KeyCode = null;
		}

		public void RegisterButton(object sender, KeyEventArgs e)
		{
			KeyCode = e.KeyCode;
			DialogResult = DialogResult.OK;
		}
	}
}
