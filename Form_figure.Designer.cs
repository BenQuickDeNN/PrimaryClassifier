namespace 分类器
{
    partial class Form_figure
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_文件 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_文件_保存图片 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_文件_关闭 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.pictureBox_显示 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_显示)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_文件});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem_文件
            // 
            this.toolStripMenuItem_文件.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_文件_保存图片,
            this.toolStripMenuItem_文件_关闭});
            this.toolStripMenuItem_文件.Name = "toolStripMenuItem_文件";
            this.toolStripMenuItem_文件.Size = new System.Drawing.Size(44, 21);
            this.toolStripMenuItem_文件.Text = "文件";
            // 
            // toolStripMenuItem_文件_保存图片
            // 
            this.toolStripMenuItem_文件_保存图片.Name = "toolStripMenuItem_文件_保存图片";
            this.toolStripMenuItem_文件_保存图片.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem_文件_保存图片.Text = "保存图片...";
            // 
            // toolStripMenuItem_文件_关闭
            // 
            this.toolStripMenuItem_文件_关闭.Name = "toolStripMenuItem_文件_关闭";
            this.toolStripMenuItem_文件_关闭.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem_文件_关闭.Text = "关闭";
            // 
            // pictureBox_显示
            // 
            this.pictureBox_显示.BackColor = System.Drawing.Color.Black;
            this.pictureBox_显示.Location = new System.Drawing.Point(0, 29);
            this.pictureBox_显示.Name = "pictureBox_显示";
            this.pictureBox_显示.Size = new System.Drawing.Size(784, 531);
            this.pictureBox_显示.TabIndex = 1;
            this.pictureBox_显示.TabStop = false;
            // 
            // Form_figure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.pictureBox_显示);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form_figure";
            this.Text = "figure";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_显示)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_文件;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_文件_保存图片;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_文件_关闭;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.PictureBox pictureBox_显示;
    }
}