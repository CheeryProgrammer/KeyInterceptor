
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
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.скрытьГраницыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.прозрачныйФонToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поверхВсехОконToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.цветФонаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.шрифтToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.цветШрифтаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logPanel1 = new KeyInterceptor.LogPanel();
            this.contextMenuLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuLog
            // 
            this.contextMenuLog.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.очиститьToolStripMenuItem,
            this.toolStripMenuItem1,
            this.скрытьГраницыToolStripMenuItem,
            this.прозрачныйФонToolStripMenuItem,
            this.поверхВсехОконToolStripMenuItem,
            this.toolStripMenuItem2,
            this.цветФонаToolStripMenuItem,
            this.шрифтToolStripMenuItem,
            this.цветШрифтаToolStripMenuItem});
            this.contextMenuLog.Name = "contextMenuLog";
            this.contextMenuLog.Size = new System.Drawing.Size(172, 170);
            // 
            // очиститьToolStripMenuItem
            // 
            this.очиститьToolStripMenuItem.Name = "очиститьToolStripMenuItem";
            this.очиститьToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.очиститьToolStripMenuItem.Text = "Очистить";
            this.очиститьToolStripMenuItem.Click += new System.EventHandler(this.ClearToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(168, 6);
            // 
            // скрытьГраницыToolStripMenuItem
            // 
            this.скрытьГраницыToolStripMenuItem.Name = "скрытьГраницыToolStripMenuItem";
            this.скрытьГраницыToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.скрытьГраницыToolStripMenuItem.Text = "Скрыть границы";
            this.скрытьГраницыToolStripMenuItem.Click += new System.EventHandler(this.NoBordersToolStripMenuItem_Click);
            // 
            // прозрачныйФонToolStripMenuItem
            // 
            this.прозрачныйФонToolStripMenuItem.Name = "прозрачныйФонToolStripMenuItem";
            this.прозрачныйФонToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.прозрачныйФонToolStripMenuItem.Text = "Прозрачный фон";
            this.прозрачныйФонToolStripMenuItem.Click += new System.EventHandler(this.SetTransparentBackgroundToolStripMenuItem_Click);
            // 
            // поверхВсехОконToolStripMenuItem
            // 
            this.поверхВсехОконToolStripMenuItem.Name = "поверхВсехОконToolStripMenuItem";
            this.поверхВсехОконToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.поверхВсехОконToolStripMenuItem.Text = "Поверх всех окон";
            this.поверхВсехОконToolStripMenuItem.Click += new System.EventHandler(this.SwitchTopMostToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(168, 6);
            // 
            // цветФонаToolStripMenuItem
            // 
            this.цветФонаToolStripMenuItem.Name = "цветФонаToolStripMenuItem";
            this.цветФонаToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.цветФонаToolStripMenuItem.Text = "Цвет фона...";
            this.цветФонаToolStripMenuItem.Click += new System.EventHandler(this.ChangeBackColorToolStripMenuItem_Click);
            // 
            // шрифтToolStripMenuItem
            // 
            this.шрифтToolStripMenuItem.Name = "шрифтToolStripMenuItem";
            this.шрифтToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.шрифтToolStripMenuItem.Text = "Шрифт...";
            this.шрифтToolStripMenuItem.Click += new System.EventHandler(this.FontToolStripMenuItem_Click);
            // 
            // цветШрифтаToolStripMenuItem
            // 
            this.цветШрифтаToolStripMenuItem.Name = "цветШрифтаToolStripMenuItem";
            this.цветШрифтаToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.цветШрифтаToolStripMenuItem.Text = "Цвет шрифта...";
            this.цветШрифтаToolStripMenuItem.Click += new System.EventHandler(this.ChangeFontColorToolStripMenuItem_Click);
            // 
            // logPanel1
            // 
            this.logPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logPanel1.Location = new System.Drawing.Point(0, 0);
            this.logPanel1.Name = "logPanel1";
            this.logPanel1.Size = new System.Drawing.Size(800, 416);
            this.logPanel1.TabIndex = 1;
            // 
            // LogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 416);
            this.Controls.Add(this.logPanel1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LogForm";
            this.Text = "Log";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LogForm_FormClosing);
            this.Load += new System.EventHandler(this.LogForm_Load);
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
        private System.Windows.Forms.ToolStripMenuItem скрытьГраницыToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem прозрачныйФонToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private LogPanel logPanel1;
    }
}