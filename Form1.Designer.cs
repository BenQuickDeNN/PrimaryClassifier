namespace 分类器
{
    partial class Form_主界面
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_主界面));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_文件 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_文件_新建分析代码 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_文件_打开分析代码 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_文件_保存 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_文件_另存为 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_文件_导入数据 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_文件_导入数据_Excel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_文件_退出 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_编辑 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_编辑_清空输出信息栏 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_编辑_清空代码编辑栏 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_运行 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_运行_启动分类程序 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_帮助 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_帮助_关于软件 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_帮助_使用说明 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_帮助_建议与反馈 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_帮助_购买激活码 = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextBox_ComandWindow = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox_CodeEditArea = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_文件,
            this.toolStripMenuItem_编辑,
            this.toolStripMenuItem_运行,
            this.toolStripMenuItem_帮助});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem_文件
            // 
            this.toolStripMenuItem_文件.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_文件_新建分析代码,
            this.toolStripMenuItem_文件_打开分析代码,
            this.toolStripMenuItem_文件_保存,
            this.toolStripMenuItem_文件_另存为,
            this.toolStripMenuItem_文件_导入数据,
            this.toolStripMenuItem_文件_退出});
            this.toolStripMenuItem_文件.Name = "toolStripMenuItem_文件";
            this.toolStripMenuItem_文件.Size = new System.Drawing.Size(44, 21);
            this.toolStripMenuItem_文件.Text = "文件";
            // 
            // toolStripMenuItem_文件_新建分析代码
            // 
            this.toolStripMenuItem_文件_新建分析代码.Name = "toolStripMenuItem_文件_新建分析代码";
            this.toolStripMenuItem_文件_新建分析代码.Size = new System.Drawing.Size(157, 22);
            this.toolStripMenuItem_文件_新建分析代码.Text = "新建分析代码...";
            // 
            // toolStripMenuItem_文件_打开分析代码
            // 
            this.toolStripMenuItem_文件_打开分析代码.Name = "toolStripMenuItem_文件_打开分析代码";
            this.toolStripMenuItem_文件_打开分析代码.Size = new System.Drawing.Size(157, 22);
            this.toolStripMenuItem_文件_打开分析代码.Text = "打开分析代码...";
            // 
            // toolStripMenuItem_文件_保存
            // 
            this.toolStripMenuItem_文件_保存.Name = "toolStripMenuItem_文件_保存";
            this.toolStripMenuItem_文件_保存.Size = new System.Drawing.Size(157, 22);
            this.toolStripMenuItem_文件_保存.Text = "保存";
            // 
            // toolStripMenuItem_文件_另存为
            // 
            this.toolStripMenuItem_文件_另存为.Name = "toolStripMenuItem_文件_另存为";
            this.toolStripMenuItem_文件_另存为.Size = new System.Drawing.Size(157, 22);
            this.toolStripMenuItem_文件_另存为.Text = "另存为...";
            // 
            // toolStripMenuItem_文件_导入数据
            // 
            this.toolStripMenuItem_文件_导入数据.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_文件_导入数据_Excel});
            this.toolStripMenuItem_文件_导入数据.Name = "toolStripMenuItem_文件_导入数据";
            this.toolStripMenuItem_文件_导入数据.Size = new System.Drawing.Size(157, 22);
            this.toolStripMenuItem_文件_导入数据.Text = "导入数据";
            // 
            // toolStripMenuItem_文件_导入数据_Excel
            // 
            this.toolStripMenuItem_文件_导入数据_Excel.Name = "toolStripMenuItem_文件_导入数据_Excel";
            this.toolStripMenuItem_文件_导入数据_Excel.Size = new System.Drawing.Size(129, 22);
            this.toolStripMenuItem_文件_导入数据_Excel.Text = "来自Excel";
            // 
            // toolStripMenuItem_文件_退出
            // 
            this.toolStripMenuItem_文件_退出.Name = "toolStripMenuItem_文件_退出";
            this.toolStripMenuItem_文件_退出.Size = new System.Drawing.Size(157, 22);
            this.toolStripMenuItem_文件_退出.Text = "退出";
            // 
            // toolStripMenuItem_编辑
            // 
            this.toolStripMenuItem_编辑.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_编辑_清空输出信息栏,
            this.toolStripMenuItem_编辑_清空代码编辑栏});
            this.toolStripMenuItem_编辑.Name = "toolStripMenuItem_编辑";
            this.toolStripMenuItem_编辑.Size = new System.Drawing.Size(44, 21);
            this.toolStripMenuItem_编辑.Text = "编辑";
            // 
            // toolStripMenuItem_编辑_清空输出信息栏
            // 
            this.toolStripMenuItem_编辑_清空输出信息栏.Name = "toolStripMenuItem_编辑_清空输出信息栏";
            this.toolStripMenuItem_编辑_清空输出信息栏.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItem_编辑_清空输出信息栏.Text = "清空输出信息栏";
            // 
            // toolStripMenuItem_编辑_清空代码编辑栏
            // 
            this.toolStripMenuItem_编辑_清空代码编辑栏.Name = "toolStripMenuItem_编辑_清空代码编辑栏";
            this.toolStripMenuItem_编辑_清空代码编辑栏.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItem_编辑_清空代码编辑栏.Text = "清空代码编辑栏";
            // 
            // toolStripMenuItem_运行
            // 
            this.toolStripMenuItem_运行.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_运行_启动分类程序});
            this.toolStripMenuItem_运行.Name = "toolStripMenuItem_运行";
            this.toolStripMenuItem_运行.Size = new System.Drawing.Size(44, 21);
            this.toolStripMenuItem_运行.Text = "运行";
            // 
            // toolStripMenuItem_运行_启动分类程序
            // 
            this.toolStripMenuItem_运行_启动分类程序.Name = "toolStripMenuItem_运行_启动分类程序";
            this.toolStripMenuItem_运行_启动分类程序.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem_运行_启动分类程序.Text = "启动分类程序";
            // 
            // toolStripMenuItem_帮助
            // 
            this.toolStripMenuItem_帮助.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_帮助_关于软件,
            this.toolStripMenuItem_帮助_使用说明,
            this.toolStripMenuItem_帮助_建议与反馈,
            this.toolStripMenuItem_帮助_购买激活码});
            this.toolStripMenuItem_帮助.Name = "toolStripMenuItem_帮助";
            this.toolStripMenuItem_帮助.Size = new System.Drawing.Size(44, 21);
            this.toolStripMenuItem_帮助.Text = "帮助";
            // 
            // toolStripMenuItem_帮助_关于软件
            // 
            this.toolStripMenuItem_帮助_关于软件.Name = "toolStripMenuItem_帮助_关于软件";
            this.toolStripMenuItem_帮助_关于软件.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItem_帮助_关于软件.Text = "关于软件";
            // 
            // toolStripMenuItem_帮助_使用说明
            // 
            this.toolStripMenuItem_帮助_使用说明.Name = "toolStripMenuItem_帮助_使用说明";
            this.toolStripMenuItem_帮助_使用说明.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItem_帮助_使用说明.Text = "使用说明";
            // 
            // toolStripMenuItem_帮助_建议与反馈
            // 
            this.toolStripMenuItem_帮助_建议与反馈.Name = "toolStripMenuItem_帮助_建议与反馈";
            this.toolStripMenuItem_帮助_建议与反馈.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItem_帮助_建议与反馈.Text = "建议与反馈";
            // 
            // toolStripMenuItem_帮助_购买激活码
            // 
            this.toolStripMenuItem_帮助_购买激活码.Name = "toolStripMenuItem_帮助_购买激活码";
            this.toolStripMenuItem_帮助_购买激活码.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItem_帮助_购买激活码.Text = "购买激活码";
            // 
            // richTextBox_ComandWindow
            // 
            this.richTextBox_ComandWindow.Location = new System.Drawing.Point(13, 47);
            this.richTextBox_ComandWindow.Name = "richTextBox_ComandWindow";
            this.richTextBox_ComandWindow.Size = new System.Drawing.Size(343, 502);
            this.richTextBox_ComandWindow.TabIndex = 1;
            this.richTextBox_ComandWindow.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "输出信息:";
            // 
            // richTextBox_CodeEditArea
            // 
            this.richTextBox_CodeEditArea.Location = new System.Drawing.Point(383, 47);
            this.richTextBox_CodeEditArea.Name = "richTextBox_CodeEditArea";
            this.richTextBox_CodeEditArea.Size = new System.Drawing.Size(389, 502);
            this.richTextBox_CodeEditArea.TabIndex = 3;
            this.richTextBox_CodeEditArea.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(383, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "代码编辑栏:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form_主界面
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.richTextBox_CodeEditArea);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox_ComandWindow);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form_主界面";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "分类器";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_文件;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_文件_新建分析代码;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_文件_打开分析代码;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_文件_导入数据;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_文件_导入数据_Excel;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_文件_退出;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_帮助;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_帮助_关于软件;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_帮助_使用说明;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_帮助_建议与反馈;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_帮助_购买激活码;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_文件_保存;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_文件_另存为;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox_CodeEditArea;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_运行;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_运行_启动分类程序;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_编辑;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_编辑_清空输出信息栏;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_编辑_清空代码编辑栏;
        public System.Windows.Forms.RichTextBox richTextBox_ComandWindow;
    }
}

