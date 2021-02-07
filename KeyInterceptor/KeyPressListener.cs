using Gma.System.MouseKeyHook;
using System;
using System.Windows.Forms;

namespace KeyInterceptor
{
	class KeyPressListener : IDisposable
	{
		public event KeyEventHandler KeyDown;
		public event KeyEventHandler KeyUp;

		private IKeyboardMouseEvents m_GlobalHook;

		public KeyPressListener()
		{
			Subscribe();
		}

		public void Subscribe()
		{
			// Note: for the application hook, use the Hook.AppEvents() instead
			m_GlobalHook = Hook.GlobalEvents();

			m_GlobalHook.KeyDown += M_GlobalHook_KeyDown;
			m_GlobalHook.KeyUp += M_GlobalHook_KeyUp;
		}

		private void M_GlobalHook_KeyDown(object sender, KeyEventArgs e)
		{
			KeyDown?.Invoke(sender, e);
		}

		private void M_GlobalHook_KeyUp(object sender, KeyEventArgs e)
		{
			KeyUp?.Invoke(sender, e);
		}

		public void Unsubscribe()
		{
			m_GlobalHook.KeyDown -= M_GlobalHook_KeyDown;
			m_GlobalHook.KeyUp -= M_GlobalHook_KeyUp;

			//It is recommened to dispose it
			m_GlobalHook.Dispose();
		}

		public void Dispose()
		{
			Unsubscribe();
		}
	}
}
