
namespace KeyInterceptor
{
	partial class ClockForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClockForm));
			this.lblClock = new System.Windows.Forms.Label();
			this.contextMenuClock = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.шрифтToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.цветШрифтаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.цветФонаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuClock.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblClock
			// 
			this.lblClock.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblClock.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblClock.Location = new System.Drawing.Point(0, 0);
			this.lblClock.Name = "lblClock";
			this.lblClock.Size = new System.Drawing.Size(800, 450);
			this.lblClock.TabIndex = 1;
			this.lblClock.Text = "label1";
			this.lblClock.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LblClock_MouseClick);
			// 
			// contextMenuClock
			// 
			this.contextMenuClock.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.шрифтToolStripMenuItem,
            this.цветШрифтаToolStripMenuItem,
            this.цветФонаToolStripMenuItem});
			this.contextMenuClock.Name = "contextMenuClock";
			this.contextMenuClock.Size = new System.Drawing.Size(158, 70);
			// 
			// шрифтToolStripMenuItem
			// 
			this.шрифтToolStripMenuItem.Name = "шрифтToolStripMenuItem";
			this.шрифтToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			this.шрифтToolStripMenuItem.Text = "Шрифт...";
			this.шрифтToolStripMenuItem.Click += new System.EventHandler(this.ChangeFontToolStripMenuItem_Click);
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
			// ClockForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.lblClock);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ClockForm";
			this.Text = "Clock";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClockForm_FormClosing);
			this.Load += new System.EventHandler(this.ClockForm_Load);
			this.contextMenuClock.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblClock;
		private System.Windows.Forms.ContextMenuStrip contextMenuClock;
		private System.Windows.Forms.ToolStripMenuItem шрифтToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem цветШрифтаToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem цветФонаToolStripMenuItem;
	}
}