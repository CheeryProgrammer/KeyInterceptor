
namespace KeyInterceptor
{
	partial class LogForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogForm));
			this.tbLog = new System.Windows.Forms.TextBox();
			this.contextMenuLog = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.очиститьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.шрифтToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.цветШрифтаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.цветФонаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuLog.SuspendLayout();
			this.SuspendLayout();
			// 
			// tbLog
			// 
			this.tbLog.ContextMenuStrip = this.contextMenuLog;
			this.tbLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbLog.Location = new System.Drawing.Point(0, 0);
			this.tbLog.Multiline = true;
			this.tbLog.Name = "tbLog";
			this.tbLog.ReadOnly = true;
			this.tbLog.Size = new System.Drawing.Size(800, 450);
			this.tbLog.TabIndex = 0;
			this.tbLog.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TbLog_MouseClick);
			// 
			// contextMenuLog
			// 
			this.contextMenuLog.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.очиститьToolStripMenuItem,
            this.шрифтToolStripMenuItem,
            this.цветШрифтаToolStripMenuItem,
            this.цветФонаToolStripMenuItem});
			this.contextMenuLog.Name = "contextMenuLog";
			this.contextMenuLog.Size = new System.Drawing.Size(158, 92);
			// 
			// очиститьToolStripMenuItem
			// 
			this.очиститьToolStripMenuItem.Name = "очиститьToolStripMenuItem";
			this.очиститьToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			this.очиститьToolStripMenuItem.Text = "Очистить";
			this.очиститьToolStripMenuItem.Click += new System.EventHandler(this.ClearToolStripMenuItem_Click);
			// 
			// шрифтToolStripMenuItem
			// 
			this.шрифтToolStripMenuItem.Name = "шрифтToolStripMenuItem";
			this.шрифтToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			this.шрифтToolStripMenuItem.Text = "Шрифт...";
			this.шрифтToolStripMenuItem.Click += new System.EventHandler(this.FontToolStripMenuItem_Click);
			// 
			// цветШрифтаToolStripMenuItem
			// 
			this.цветШрифтаToolStripMenuItem.Name = "цветШрифтаToolStripMenuItem";
			this.цветШрифтаToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			this.цветШрифтаToolStripMenuItem.Text = "Цвет шрифта...";
			this.цветШрифтаToolStripMenuItem.Click += new System.EventHandler(this.ChangeFontColorToolStripMenuItem_Click);
			// 
			// цветФонаToolStripMenuItem
			// 
			this.цветФонаToolStripMenuItem.Name = "цветФонаToolStripMenuItem";
			this.цветФонаToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			this.цветФонаToolStripMenuItem.Text = "Цвет фона...";
			this.цветФонаToolStripMenuItem.Click += new System.EventHandler(this.ChangeBackColorToolStripMenuItem_Click);
			// 
			// LogForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.tbLog);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "LogForm";
			this.Text = "Log";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LogForm_FormClosing);
			this.Load += new System.EventHandler(this.LogForm_Load);
			this.contextMenuLog.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tbLog;
		private System.Windows.Forms.ContextMenuStrip contextMenuLog;
		private System.Windows.Forms.ToolStripMenuItem очиститьToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem шрифтToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem цветШрифтаToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem цветФонаToolStripMenuItem;
	}
}