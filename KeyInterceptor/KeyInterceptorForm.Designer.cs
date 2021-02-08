
namespace KeyInterceptor
{
	partial class KeyInterceptorForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyInterceptorForm));
			this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.безГраницToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.добавитьКнопкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.показатьЛогToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.buttonContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.назначитьКнопкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.изображениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.изображениеактивToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenu.SuspendLayout();
			this.buttonContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenu
			// 
			this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.безГраницToolStripMenuItem,
            this.добавитьКнопкуToolStripMenuItem,
            this.показатьЛогToolStripMenuItem,
            this.выходToolStripMenuItem});
			this.contextMenu.Name = "contextMenu";
			this.contextMenu.Size = new System.Drawing.Size(181, 114);
			// 
			// безГраницToolStripMenuItem
			// 
			this.безГраницToolStripMenuItem.Name = "безГраницToolStripMenuItem";
			this.безГраницToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.безГраницToolStripMenuItem.Text = "Скрыть границы";
			this.безГраницToolStripMenuItem.Click += new System.EventHandler(this.NoBordersToolStripMenuItem_Click);
			// 
			// добавитьКнопкуToolStripMenuItem
			// 
			this.добавитьКнопкуToolStripMenuItem.Name = "добавитьКнопкуToolStripMenuItem";
			this.добавитьКнопкуToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.добавитьКнопкуToolStripMenuItem.Text = "Добавить кнопку";
			this.добавитьКнопкуToolStripMenuItem.Click += new System.EventHandler(this.AddButtonToolStripMenuItem_Click);
			// 
			// выходToolStripMenuItem
			// 
			this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
			this.выходToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.выходToolStripMenuItem.Text = "Выход";
			this.выходToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
			// 
			// показатьЛогToolStripMenuItem
			// 
			this.показатьЛогToolStripMenuItem.Name = "показатьЛогToolStripMenuItem";
			this.показатьЛогToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.показатьЛогToolStripMenuItem.Text = "Показать лог";
			this.показатьЛогToolStripMenuItem.Click += new System.EventHandler(this.ShowLogToolStripMenuItem_Click);
			// 
			// buttonContextMenu
			// 
			this.buttonContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.назначитьКнопкуToolStripMenuItem,
            this.изображениеToolStripMenuItem,
            this.изображениеактивToolStripMenuItem,
            this.удалитьToolStripMenuItem});
			this.buttonContextMenu.Name = "buttonContextMenu";
			this.buttonContextMenu.Size = new System.Drawing.Size(204, 92);
			// 
			// назначитьКнопкуToolStripMenuItem
			// 
			this.назначитьКнопкуToolStripMenuItem.Name = "назначитьКнопкуToolStripMenuItem";
			this.назначитьКнопкуToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			this.назначитьКнопкуToolStripMenuItem.Text = "Назначить кнопку...";
			this.назначитьКнопкуToolStripMenuItem.Click += new System.EventHandler(this.BindViewToKeyToolStripMenuItem_Click);
			// 
			// изображениеToolStripMenuItem
			// 
			this.изображениеToolStripMenuItem.Name = "изображениеToolStripMenuItem";
			this.изображениеToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			this.изображениеToolStripMenuItem.Text = "Изображение...";
			this.изображениеToolStripMenuItem.Click += new System.EventHandler(this.ChangeViewImageToolStripMenuItem_Click);
			// 
			// изображениеактивToolStripMenuItem
			// 
			this.изображениеактивToolStripMenuItem.Name = "изображениеактивToolStripMenuItem";
			this.изображениеактивToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			this.изображениеактивToolStripMenuItem.Text = "Изображение (актив.)...";
			this.изображениеактивToolStripMenuItem.Click += new System.EventHandler(this.ChangeViewActiveImageToolStripMenuItem_Click);
			// 
			// удалитьToolStripMenuItem
			// 
			this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
			this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			this.удалитьToolStripMenuItem.Text = "Удалить";
			this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.RemoveButtonToolStripMenuItem_Click);
			// 
			// KeyInterceptorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(878, 453);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "KeyInterceptorForm";
			this.Text = "Key interceptor - by Cheery Programmer";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KeyInterceptorForm_FormClosing);
			this.Load += new System.EventHandler(this.KeyInterceptorForm_Load);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.KeyInterceptorForm_MouseDown);
			this.contextMenu.ResumeLayout(false);
			this.buttonContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip contextMenu;
		private System.Windows.Forms.ToolStripMenuItem добавитьКнопкуToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip buttonContextMenu;
		private System.Windows.Forms.ToolStripMenuItem назначитьКнопкуToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem изображениеToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem изображениеактивToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem безГраницToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem показатьЛогToolStripMenuItem;
	}
}

