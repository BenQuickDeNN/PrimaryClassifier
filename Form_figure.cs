using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 分类器
{
    public partial class Form_figure : Form
    {
        //KNN用到的变量
        private String[] TypeName;
        private int[] X;
        private int[] Y;
        private int K;
        private int R;
        //决策树用到的变量
        private DecisionTree.Node[] node;
        private DecisionTree.Brunch[] brunch;
        private const int font_size = 10;//字体大小
        private const int seperate_size = 20;//分割尺寸
        private int string_size = 0;//最宽的字符串尺寸
        private int maxSerial;//最大序列
        private int maxScale;//最大层
        //图片类
        Bitmap bp;
        Graphics gp;
        //指令常量
        public const String METHOD_KNEARSTNEIGHBOR = "KNearestNeighbor";
        public const String METHOD_DECISIONTREE = "DecisionTree";
        public Form_figure()
        {
            InitializeComponent();
            toolStripMenuItem_文件_保存图片.Click += new EventHandler(saveImage);
            toolStripMenuItem_文件_关闭.Click += new EventHandler(doExit);
            try {
                StreamReader sr = new StreamReader(@"./cache/figure.fg", Encoding.Default);
                String line;
                String temp1;
                String[] temp2;
                line = sr.ReadLine();
                temp1 = CodeAnalysis.getCommandString(line);
                temp2 = CodeAnalysis.getValueString(line);
                if (temp1 == "Method")
                {
                    switch (temp2[0])
                    {
                        case METHOD_KNEARSTNEIGHBOR:
                            KNearestNeighborFigure();
                            break;
                        case METHOD_DECISIONTREE:
                            DecisionTreeFigure();
                            break;
                    }
                }
                sr.Close();
            }catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        //KNN方法
        private void KNearestNeighborFigure()
        {
            initialFigureData();
            drawFigure();
        }
        private void initialFigureData()
        {
            try
            {
                //打开文件对话框选择的文件
                StreamReader sr = new StreamReader(@"./cache/figure.fg", Encoding.Default);
                String line;
                String temp1;
                String[] temp2;
                while ((line = sr.ReadLine()) != null)
                {
                    temp1 = CodeAnalysis.getCommandString(line);
                    temp2 = CodeAnalysis.getValueString(line);
                    switch (temp1)
                    {
                        case "TypeName":
                            TypeName = temp2;
                            break;
                        case "XAxis":
                            X = new int[temp2.Length];
                            for (int i = 0; i < X.Length; i++)
                            {
                                X[i] = int.Parse(temp2[i]);
                            }
                            break;
                        case "YAxis":
                            Y = new int[temp2.Length];
                            for (int i = 0; i < Y.Length; i++)
                            {
                                Y[i] = int.Parse(temp2[i]);
                            }
                            break;
                        case "KValue":
                            K = int.Parse(temp2[0]);
                            break;
                        case "RValue":
                            R = int.Parse(temp2[0]);
                            break;
                    }
                }
                sr.Close();//关闭流
                //加载画布
                bp = new Bitmap(784, 530);
                gp = Graphics.FromImage(bp);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        //绘制图片
        private void drawFigure()
        {
            try
            {
                Font font= new Font("宋体", 10, FontStyle.Regular);
                Brush brush = new SolidBrush(Color.Blue);
                //画字符串
                for(int i = 1; i < TypeName.Length; i++)
                {
                    gp.DrawString(TypeName[i], font, brush, new PointF(X[i], Y[i]));//绘制字符串
                    gp.FillEllipse(brush, new Rectangle(X[i], Y[i], 4, 4));
                }
                brush = new SolidBrush(Color.Red);
                gp.DrawString(TypeName[0], font, brush, new PointF(X[0], Y[0]));
                gp.FillEllipse(brush, new Rectangle(X[0], Y[0], 4, 4));
                //画最近邻圆
                gp.DrawEllipse(new Pen(Color.Green), new Rectangle(X[0] - R, Y[0] - R, 2 * R, 2 * R));
                pictureBox_显示.Image = bp;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        //决策树方法
        private void DecisionTreeFigure()
        {
            initialDecisionTreeData();
            createCanvas();
            drawTree();
        }
        //导入决策树数据
        private void initialDecisionTreeData()
        {
            node = new DecisionTree.Node[0];
            brunch = new DecisionTree.Brunch[0];
            try
            {
                StreamReader sr = new StreamReader(@"./cache/figure.fg", Encoding.Default);
                String line;
                String temp1;
                String[] temp2;
                while ((line = sr.ReadLine()) != null)
                {
                    temp1 = CodeAnalysis.getCommandString(line);
                    temp2 = CodeAnalysis.getValueString(line);
                    switch (temp1)
                    {
                        case "Node":
                            DecisionTree.Node[] temp_node = node;
                            node = new DecisionTree.Node[temp_node.Length + 1];
                            for(int i = 0; i < temp_node.Length; i++)
                            {
                                node[i] = temp_node[i];
                            }
                            node[temp_node.Length] = new DecisionTree.Node(int.Parse(temp2[0]), int.Parse(temp2[1]), temp2[2]);
                            break;
                        case "Brunch":
                            DecisionTree.Brunch[] temp_brunch = brunch;
                            brunch = new DecisionTree.Brunch[temp_brunch.Length + 1];
                            for(int i = 0; i < temp_brunch.Length; i++)
                            {
                                brunch[i] = temp_brunch[i];
                            }
                            brunch[temp_brunch.Length] = new DecisionTree.Brunch(int.Parse(temp2[0]), int.Parse(temp2[1]), temp2[2]);
                            break;
                    }
                }
                sr.Close();
            }catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        //计算画布的尺寸
        private void createCanvas()
        {
            //先计算宽度
            int max_width = 0;
            //获取最大序列
            int max_scale = 0;
            for(int i = 0; i < node.Length; i++)
            {
                if (max_scale < node[i].Serial) max_scale = node[i].Scale;
            }
            //逐个序列查看最宽的文字长度
            int max_string_length = 0;
            for(int i = 0; i < node.Length; i++)
            {
                if (max_string_length < node[i].Name.Length) max_string_length = node[i].Name.Length;
            }
            //再逐个树枝查看
            for(int i = 0; i < brunch.Length; i++)
            {
                if (max_string_length < brunch[i].Name.Length) max_string_length = brunch[i].Name.Length;
            }
            ++max_scale;
            ++max_scale;
            ++max_scale;
            string_size = max_string_length * font_size;//字符串尺寸确定
            max_width = (2 * max_scale - 1) * string_size + (2 * max_scale - 1) * seperate_size;//得到宽度
            //计算高度
            int max_height = 0;
            //获取最大层次
            int max_serial = 0;
            for(int i = 0; i < node.Length; i++)
            {
                if (max_serial < node[i].Serial) max_serial = node[i].Serial;
            }
            ++max_serial;
            max_height = (2 * max_serial + 1) * seperate_size;
            //创建画布
            bp = new Bitmap(max_width, max_height);
            gp = Graphics.FromImage(bp);
            maxScale = max_scale;
            maxSerial = max_serial;
        }
        //根据节点的层次和序列画图
        private void drawTree()
        {
            Brush brush = new SolidBrush(Color.Green);
            Pen pen = new Pen(Color.Green);
            Font font = new Font("宋体", font_size, FontStyle.Regular);
            //逐层逐序列画节点
            for(int i = 0; i < node.Length; i++)
            {
                int temp_x = node[i].Scale * (2 * (string_size + seperate_size)) + seperate_size;
                int temp_y = node[i].Serial * (2 * seperate_size) + seperate_size;
                int temp_width = string_size;
                int temp_height = seperate_size;
                Rectangle temp_rg = new Rectangle(temp_x, temp_y, temp_width, temp_height);
                //画节点矩形和文字，以及线
                gp.DrawRectangle(pen, temp_rg);
                gp.DrawString(node[i].Name, font, brush, new Point(temp_x, temp_y));
            }
            //画树枝
            for(int i = 0; i < brunch.Length; i++)
            {
                int temp_x = brunch[i].Scale * (string_size + seperate_size) * 2 + string_size + 2 * seperate_size;
                int temp_y = brunch[i].Serial * (2 * seperate_size) + seperate_size;
                int temp_width = string_size + seperate_size;
                int temp_height = seperate_size;
                //画字符串和横线
                gp.DrawString(brunch[i].Name, font, brush, new Point(temp_x, temp_y - (font_size/2)));
                gp.DrawLine(pen, new Point(temp_x, temp_y + (seperate_size / 2)),
                    new Point(temp_x + temp_width, temp_y + (seperate_size / 2)));
            }
            //画连线
            for(int temp_scale = 0; temp_scale < maxScale; temp_scale++)
            {
                for(int i = 0; i < node.Length; i++)
                {
                    //找到当前层的节点
                    if (node[i].Scale == temp_scale)
                    {
                        int temp_serial2 = maxSerial;
                        //寻找当前层下一个序列的节点
                        for(int j = 0; j < node.Length; j++)
                        {
                            if (node[j].Serial > node[i].Serial && node[j].Scale == temp_scale)
                            {
                                temp_serial2 = node[j].Serial;
                                break;
                            }
                        }
                        //寻找当前层，序列内的树枝
                        for(int j = 0; j < brunch.Length; j++)
                        {
                            if (brunch[j].Scale == temp_scale && brunch[j].Serial < temp_serial2 && brunch[j].Serial >= node[i].Serial)
                            {
                                //画斜线
                                int temp_x1 = node[i].Scale * (2 * (string_size + seperate_size)) + seperate_size +
                                    string_size;
                                int temp_y1 = node[i].Serial * (2 * seperate_size) + seperate_size + (font_size / 2) +
                                     (font_size / 2);
                                int temp_x2 = brunch[j].Scale * (string_size + seperate_size) * 2 + 
                                    string_size + 2 * seperate_size;
                                int temp_y2 = brunch[j].Serial * (2 * seperate_size) + seperate_size + (seperate_size / 2);
                                gp.DrawLine(pen, new Point(temp_x1, temp_y1), new Point(temp_x2, temp_y2));
                            }
                        }
                    }
                }
            }
            pictureBox_显示.Image = bp;
        }
        //保存图片
        private void saveImage(object obj, EventArgs ea)
        {
            try {
                saveFileDialog1.Filter = "位图|*.bmp";
                saveFileDialog1.RestoreDirectory = true;
                //保存图片到本地
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    bp.Save(saveFileDialog1.FileName);
                    MessageBox.Show("图像保存成功!");
                }
            }catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        //退出
        private void doExit(object obj,EventArgs ea)
        {
            this.Dispose();
        }
    }
}
