
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
			this.contextMenuLog = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.очиститьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.шрифтToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.цветШрифтаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.цветФонаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.поверхВсехОконToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.lbLog = new System.Windows.Forms.ListBox();
			this.rtbClock = new System.Windows.Forms.RichTextBox();
			this.contextMenuLog.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenuLog
			// 
			this.contextMenuLog.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.очиститьToolStripMenuItem,
            this.шрифтToolStripMenuItem,
            this.цветШрифтаToolStripMenuItem,
            this.цветФонаToolStripMenuItem,
            this.поверхВсехОконToolStripMenuItem});
			this.contextMenuLog.Name = "contextMenuLog";
			this.contextMenuLog.Size = new System.Drawing.Size(173, 114);
			// 
			// очиститьToolStripMenuItem
			// 
			this.очиститьToolStripMenuItem.Name = "очиститьToolStripMenuItem";
			this.очиститьToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.очиститьToolStripMenuItem.Text = "Очистить";
			this.очиститьToolStripMenuItem.Click += new System.EventHandler(this.ClearToolStripMenuItem_Click);
			// 
			// шрифтToolStripMenuItem
			// 
			this.шрифтToolStripMenuItem.Name = "шрифтToolStripMenuItem";
			this.шрифтToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.шрифтToolStripMenuItem.Text = "Шрифт...";
			this.шрифтToolStripMenuItem.Click += new System.EventHandler(this.FontToolStripMenuItem_Click);
			// 
			// цветШрифтаToolStripMenuItem
			// 
			this.цветШрифтаToolStripMenuItem.Name = "цветШрифтаToolStripMenuItem";
			this.цветШрифтаToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.цветШрифтаToolStripMenuItem.Text = "Цвет шрифта...";
			this.цветШрифтаToolStripMenuItem.Click += new System.EventHandler(this.ChangeFontColorToolStripMenuItem_Click);
			// 
			// цветФонаToolStripMenuItem
			// 
			this.цветФонаToolStripMenuItem.Name = "цветФонаToolStripMenuItem";
			this.цветФонаToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.цветФонаToolStripMenuItem.Text = "Цвет фона...";
			this.цветФонаToolStripMenuItem.Click += new System.EventHandler(this.ChangeBackColorToolStripMenuItem_Click);
			// 
			// поверхВсехОконToolStripMenuItem
			// 
			this.поверхВсехОконToolStripMenuItem.Name = "поверхВсехОконToolStripMenuItem";
			this.поверхВсехОконToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.поверхВсехОконToolStripMenuItem.Text = "Поверх всех окон";
			this.поверхВсехОконToolStripMenuItem.Click += new System.EventHandler(this.SwitchTopMostToolStripMenuItem_Click);
			// 
			// lbLog
			// 
			this.lbLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lbLog.FormattingEnabled = true;
			this.lbLog.Location = new System.Drawing.Point(0, 0);
			this.lbLog.Name = "lbLog";
			this.lbLog.Size = new System.Drawing.Size(800, 312);
			this.lbLog.TabIndex = 1;
			this.lbLog.TabStop = false;
			this.lbLog.SelectedIndexChanged += new System.EventHandler(this.lbLog_SelectedIndexChanged);
			this.lbLog.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LbLog_MouseClick);
			// 
			// rtbClock
			// 
			this.rtbClock.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.rtbClock.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.rtbClock.Location = new System.Drawing.Point(0, 397);
			this.rtbClock.Name = "rtbClock";
			this.rtbClock.Size = new System.Drawing.Size(800, 19);
			this.rtbClock.TabIndex = 4;
			this.rtbClock.Text = "";
			// 
			// LogForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 416);
			this.Controls.Add(this.rtbClock);
			this.Controls.Add(this.lbLog);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "LogForm";
			this.Text = "Log";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LogForm_FormClosing);
			this.Load += new System.EventHandler(this.LogForm_Load);
			this.ResizeEnd += new System.EventHandler(this.LogForm_ResizeEnd);
			this.Resize += new System.EventHandler(this.LogForm_Resize);
			this.contextMenuLog.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ContextMenuStrip contextMenuLog;
		private System.Windows.Forms.ToolStripMenuItem очиститьToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem шрифтToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem цветШрифтаToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem цветФонаToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem поверхВсехОконToolStripMenuItem;
		private System.Windows.Forms.ListBox lbLog;
		private System.Windows.Forms.RichTextBox rtbClock;
	}
}