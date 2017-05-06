using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace 分类器
{
    public partial class Form_主界面 : Form
    {
        private String file_save_path;//文件保存路径
        private const String file_filter = "分类器代码文件(*.clfe)|*.clfe";//代码文件扩展名
        private object[,] data_obj;//要分析的数据
        //DecisionTree dt;
        public Form_主界面()
        {
            InitializeComponent();
            initialUI();
        }
        //界面控件初始化
        private void initialUI()
        {
            toolStripMenuItem_文件_保存.Enabled = false;//初始不可用
            toolStripMenuItem_文件_另存为.Enabled = false;
            toolStripMenuItem_运行_启动分类程序.Enabled = false;
            //添加点击响应函数
            toolStripMenuItem_文件_新建分析代码.Click += new EventHandler(newFile);
            toolStripMenuItem_文件_打开分析代码.Click += new EventHandler(openFile);
            toolStripMenuItem_文件_保存.Click += new EventHandler(saveFile);
            toolStripMenuItem_文件_另存为.Click += new EventHandler(saveFileAs);
            toolStripMenuItem_文件_退出.Click += new EventHandler(doExit);
            toolStripMenuItem_文件_导入数据_Excel.Click += new EventHandler(openExcelFile);
            toolStripMenuItem_运行_启动分类程序.Click += new EventHandler(doRunProgramme);
            toolStripMenuItem_编辑_清空输出信息栏.Click += new EventHandler(clearWindow);
            //测试部分
            //richTextBox_ComandWindow.AppendText(CodeAnalysis.getExcelColumn(15) + "\n");
        }
        //新建文件
        private void newFile(object obj,EventArgs ea)
        {
            StreamWriter myStream;
            saveFileDialog1.Filter = file_filter;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox_CodeEditArea.Text = "";//清空代码编辑区
                myStream = new StreamWriter(saveFileDialog1.FileName, true, System.Text.Encoding.Default);
                myStream.Write(richTextBox_CodeEditArea.Text); //写入代码文件
                file_save_path = saveFileDialog1.FileName;//临时保存文件的保存路径
                richTextBox_ComandWindow.AppendText("new code file in " + file_save_path + "\n");//控制台提示
                toolStripMenuItem_文件_保存.Enabled = true;//可用
                toolStripMenuItem_文件_另存为.Enabled = true;//可用
                toolStripMenuItem_运行_启动分类程序.Enabled = true;
                myStream.Close();//关闭流
            }
        }
        //打开文件
        private void openFile(object obj,EventArgs ea)
        {
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "打开代码文件";
            openFileDialog1.Filter = file_filter;
            openFileDialog1.InitialDirectory = @"C:\Users\Administrator\Desktop";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //打开文件对话框选择的文件
                StreamReader sr = new StreamReader(openFileDialog1.FileName, Encoding.Default);
                String line;
                richTextBox_CodeEditArea.Text = "";//清空代码区
                while ((line = sr.ReadLine()) != null)
                {
                    richTextBox_CodeEditArea.AppendText(line + "\n");
                }
                file_save_path = openFileDialog1.FileName;//临时保存文件的保存路径
                richTextBox_ComandWindow.AppendText("open code file in " + file_save_path + "\n");//控制台提示打开
                toolStripMenuItem_文件_保存.Enabled = true;//可用
                toolStripMenuItem_文件_另存为.Enabled = true;//可用
                toolStripMenuItem_运行_启动分类程序.Enabled = true;
                sr.Close();//关闭流
            }
        }
        //保存文件
        private void saveFile(object obj,EventArgs ea)
        {
            StreamWriter myStream = new StreamWriter(file_save_path);
            myStream.Write(richTextBox_CodeEditArea.Text); //写入代码文件
            richTextBox_ComandWindow.AppendText("save code file in " + file_save_path + "\n");//控制台提示
            myStream.Close();//关闭流
        }
        //另存文件为
        private void saveFileAs(object obj,EventArgs ea)
        {
            StreamWriter myStream;
            saveFileDialog1.Filter = file_filter;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                myStream = new StreamWriter(saveFileDialog1.FileName);
                myStream.Write(richTextBox_CodeEditArea.Text); //写入代码文件
                file_save_path = saveFileDialog1.FileName;//临时保存文件的保存路径
                richTextBox_ComandWindow.AppendText("save code file in " + file_save_path + "\n");//控制台提示
                myStream.Close();//关闭流
            }
        }
        //从Excel导入数据
        private void openExcelFile(object obj, EventArgs ea)
        {
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "打开excel文件";
            openFileDialog1.Filter = "excel03文件(*.xls)|*.xls|excel07文件(*.xlsx)|*.xlsx";
            openFileDialog1.InitialDirectory = @"C:\Users\Administrator\Desktop";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //打开文件对话框选择的文件
                openExcel(openFileDialog1.FileName);
            }
        }
        public void openExcel(String filePath)
        {
            object missing = System.Reflection.Missing.Value;//缺省值
            Excel.Application excel = new Excel.Application();//创建excel应用
            if (excel == null)
            {
                MessageBox.Show("调用excel功能失败！");
                return;
            }
            excel.UserControl = true;
            Excel.Workbook wb = excel.Application.Workbooks.Open(filePath, missing, missing, missing, missing, missing, missing, missing,
                missing, missing, missing, missing, missing, missing, missing);//打开excel文件
            Excel.Worksheet ws = (Excel.Worksheet)wb.Worksheets.get_Item(1);//打开工作簿，默认打开第一个
            int rown = ws.UsedRange.Cells.Rows.Count;//取得行数
            int columnn = ws.UsedRange.Cells.Columns.Count;//取得列数
            Excel.Range range = ws.Cells.get_Range("A1", CodeAnalysis.getExcelColumn(columnn) + rown);//取得数据区域范围
            data_obj = (object[,])range.Value2;//取得数据值
            excel.Quit(); excel = null;
            Process[] procs = Process.GetProcessesByName("excel");
            foreach (Process pro in procs)
            {
                pro.Kill();//消去excel进程
            }
            GC.Collect();
            richTextBox_ComandWindow.AppendText("import data from " + filePath + "\n");
            /*
            dt = new DecisionTree(data_obj);
            dt.setInfoThreshold(0);
            dt.setTargetAttribute("buys_computer");
            double zero = 0;
            richTextBox_ComandWindow.AppendText("gain:"+dt.getGainA(dt.data_obj,0,4)+"\n"+(zero)+"\n");
            */
        }
        //显示数据
        private void showData()
        {
            //用控制窗口显示导入的数据
            richTextBox_ComandWindow.AppendText("the content of the data imported is:\n");
            for (int i = 1; i <= data_obj.GetLength(0); i++)
            {
                for (int j = 1; j <= data_obj.GetLength(1); j++)
                {
                    richTextBox_ComandWindow.AppendText(data_obj[i, j].ToString() + "\t");
                }
                richTextBox_ComandWindow.AppendText("\n");
            }
        }
        //清除输出窗口
        private void clearWindow(object obj,EventArgs ea)
        {
            richTextBox_ComandWindow.Text = "";
        }
        //退出方法
        private void doExit(object obj,EventArgs ea)
        {
            Application.Exit();
        }
        //测试运行
        private void doRunProgramme(object obj,EventArgs ea)
        {
            CodeAnalysis ca = new CodeAnalysis();
            saveFile(null, null);//先保存文件
            //读取文件
            StreamReader sr = new StreamReader(file_save_path, Encoding.Default);
            richTextBox_ComandWindow.AppendText("run code file in " + file_save_path + "\n");//控制台提示运行
            String line;
            bool FLAG_IS_PROGRAMME_START = false;//标记程序是否已运行
            int ACTION_MODEL = 0;//操作标号
            int rowcount = 0;//记录行数
            while ((line = sr.ReadLine()) != null)
            {
                ++rowcount;
                richTextBox_ComandWindow.AppendText("Command:" + CodeAnalysis.getCommandString(line)+" Value:");
                String[] value = CodeAnalysis.getValueString(line);
                for(int i = 0; i < value.Length; i++)
                {
                    richTextBox_ComandWindow.AppendText(value[i] + " ");
                }
                richTextBox_ComandWindow.AppendText("\n");
                //程序运行
                try {
                    if (CodeAnalysis.getCommandString(line) == "start")
                    {
                        FLAG_IS_PROGRAMME_START = true;
                        continue;
                    }
                    if (CodeAnalysis.getCommandString(line) == "end")
                    {
                        FLAG_IS_PROGRAMME_START = false;
                        SoundPlayer sp = new SoundPlayer();
                        sp.SoundLocation = @"./audio/exception.wav";
                        sp.Play();
                        break;
                    }
                    if (FLAG_IS_PROGRAMME_START)
                    {
                        if (CodeAnalysis.getCommandString(line) == "data")
                        {
                            showData();
                            continue;
                        }
                        if (CodeAnalysis.getCommandString(line) == "clear")
                        {
                            clearWindow(null, null);
                            continue;
                        }
                        if (CodeAnalysis.getCommandString(line) == "importDataFrom")
                        {
                            openExcel(value[0]);
                            continue;
                        }
                        if (CodeAnalysis.getCommandString(line) == "display")
                        {
                            switch (ACTION_MODEL)
                            {
                                case NaiveBayesian.METHOD_NAIVEBAYESIAN:
                                    richTextBox_ComandWindow.AppendText(ca.nb.display() + "\n");//显示贝叶斯分析结果
                                    break;
                                case KNearestNeibor.METHOD_KNEARESTNEIGHBOR:
                                    richTextBox_ComandWindow.AppendText(ca.knn.display() + "\n");//显示K近邻结果
                                    break;
                            }
                            continue;
                        }
                        if (CodeAnalysis.getCommandString(line) == "figure")
                        {
                            switch (ACTION_MODEL)
                            {
                                case KNearestNeibor.METHOD_KNEARESTNEIGHBOR:
                                    ca.knn.figure();
                                    break;
                                case DecisionTree.METHOD_DECISIONTREE:
                                    ca.dt.figure();
                                    break;
                            }
                            Form_figure ff = new Form_figure();
                            ff.Show();
                            continue;
                        }
                        if (CodeAnalysis.getCommandString(line) == "test")
                        {
                            switch (ACTION_MODEL)
                            {
                                case KNearestNeibor.METHOD_KNEARESTNEIGHBOR:
                                    richTextBox_ComandWindow.AppendText(ca.knn.testResult(CodeAnalysis.getValueString(line)[0]));
                                    break;
                            }
                            continue;
                        }
                        if(CodeAnalysis.getCommandString(line)== "getBestKValue")
                        {
                            switch (ACTION_MODEL)
                            {
                                case KNearestNeibor.METHOD_KNEARESTNEIGHBOR:
                                    richTextBox_ComandWindow.AppendText("the best K value is " + ca.knn.getBestKValue() + "\n");
                                    break;
                            }
                            continue;
                        }
                        ACTION_MODEL = ca.doActionAccordingToCommand(line, data_obj, ACTION_MODEL);//执行操作
                        if (ACTION_MODEL == CodeAnalysis.COMMAND_UNDEFINE_COMMAND)
                        {//查出未声明命令
                            richTextBox_ComandWindow.AppendText("undefine command in row " + rowcount + "\n");
                            SoundPlayer sp = new SoundPlayer();
                            sp.SoundLocation = @"./audio/exception.wav";
                            sp.Play();
                            break;
                        }
                    }
                }catch(Exception e)
                {
                    richTextBox_ComandWindow.AppendText("error in row " + rowcount +"\n" + e.ToString() + "\n");
                    SoundPlayer sp = new SoundPlayer();
                    sp.SoundLocation = @"./audio/exception.wav";
                    sp.Play();
                    break;
                }
            }
            sr.Close();//关闭流
        }
    }
}
