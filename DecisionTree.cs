using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 分类器
{//决策树
    class DecisionTree
    {
        //指令常量
        public const int METHOD_DECISIONTREE = 0xf300;
        public object[,] data_obj;//源数据对象
        public String[] attribute_name;//属性名
        public String Target_attribute;//目标属性
        public double Info_threshold = 0;//信息增益率阈值，默认为0
        public Node[] node;//节点组
        public Brunch[] brunch;//树枝组
        public int now_serial=0;//当前序列
        public DecisionTree(object[,] data)
        {
            data_obj = new object[data.GetLength(0), data.GetLength(1)];
            for(int i = 0; i < data_obj.GetLength(0); i++)
            {
                for(int j = 0; j < data_obj.GetLength(1); j++)
                {
                    data_obj[i, j] = data[i + 1, j + 1];
                }
            }
            attribute_name = new String[data_obj.GetLength(1)];//申请空间
            //为属性名赋值
            for(int i = 0; i < attribute_name.Length; i++)
            {
                attribute_name[i] = data_obj[0, i].ToString();
            }
            node = new Node[0];
            brunch = new Brunch[0];
        }
        //设置信息增益率阈值
        public void setInfoThreshold(double threshold)
        {
            Info_threshold = threshold;
        }
        //设置目标属性
        public void setTargetAttribute(String attribute)
        {
            Target_attribute = attribute;
        }
        //计算D信息需求
        public double getInfoD(object[,] data, int target_index)
        {
            String[] str = new String[data.GetLength(0) - 1];
            for(int i = 0; i + 1 < data.GetLength(0); i++)
            {
                str[i] = data[i + 1, target_index].ToString();
            }
            //获取同一属性下不同值的出现次数
            double[] count = new double[0];
            String[] attribute_value = new String[0];//记录出现的属性值
            for(int i = 1; i < str.Length; i++)
            {
                if (DmMath.isDifferent(str[i], attribute_value))
                {//重新申请计数空间
                    double[] temp_count = count;
                    count = new double[temp_count.Length + 1];
                    String[] temp_attribute_value = attribute_value;
                    attribute_value = new String[temp_attribute_value.Length + 1];
                    for(int j = 0; j < temp_count.Length; j++)
                    {
                        count[j] = temp_count[j];
                        attribute_value[j] = temp_attribute_value[j];
                    }
                    count[temp_count.Length] = 1;
                    attribute_value[temp_attribute_value.Length] = str[i];
                }
                else
                {
                    for(int j = 0; j < attribute_value.Length; j++)
                    {
                        if (str[i] == attribute_value[j])
                        {
                            count[j] += 1.0;
                        }
                    }
                }
            }
            double total_row = (double)(str.Length - 1);
            double resultD = 0;
            for(int i = 0; i < count.Length; i++)
            {
                resultD += (count[i] / total_row) * DmMath.Log2(Math.Abs((count[i] + 0.001)/ (total_row + 0.001)));
            }
            resultD = -resultD;//得到数据中的期望信息增益
            return resultD;
        }
        //计算A信息增益
        public double getGainA(object[,] data, int attribute_index, int target_index)
        {
            double infoD = getInfoD(data, target_index);//获取本组分类信息需求
            double total_length = (double)(data.GetLength(0) - 1);
            double[] count = new double[0];//属性值计数
            String[] attribute_value = new String[0];//标称名记录
            String[] attribute = new String[data.GetLength(0) - 1];//原始数据中的属性值
            String[] target_value = attribute;//目标属性值
            //将属性值赋予局部变量
            for(int i = 0; i < data.GetLength(0) - 1; i++)
            {
                attribute[i] = data[i + 1, attribute_index].ToString();
                target_value[i] = data[i + 1, target_index].ToString();
            }
            //遍历属性值
            for(int i = 0; i < attribute.Length; i++)
            {
                if (DmMath.isDifferent(attribute[i], attribute_value))
                {
                    String[] temp1 = attribute_value;
                    attribute_value = new String[temp1.Length + 1];
                    for(int j = 0; j < temp1.Length; j++)
                    {
                        attribute_value[j] = temp1[j];
                    }
                    attribute_value[temp1.Length] = attribute[i];
                    double[] temp2 = count;
                    count = new double[temp2.Length + 1];
                    for(int j = 0; j < temp2.Length; j++)
                    {
                        count[j] = temp2[j];
                    }
                    count[temp2.Length] = 1.0;
                    continue;
                }
                else
                {
                    for(int j = 0; j < attribute_value.Length; j++)
                    {
                        if (attribute_value[j] == attribute[j]) count[j] += 1.0;
                    }
                }
            }
            //完成属性数量的计算，接下来算各个属性数量对应的目标属性的数量
            int target_count = 0;//目标属性数
            for (int i = 0; i < target_value.Length; i++)
            {
                if (DmMath.isDifferent(target_value[i], target_value, i))
                {
                    ++target_count;
                }
            }
            String[] sub_attribute_value = new String[target_count];
            int temp_index = 0;//赋予目标属性值
            for(int i = 0; i < target_value.Length; i++)
            {
                if (DmMath.isDifferent(target_value[i], sub_attribute_value))
                {
                    if (temp_index < sub_attribute_value.Length)
                    {
                        sub_attribute_value[temp_index] = target_value[i];
                        ++temp_index;
                    }
                }
            }
            double[,] sub_count = new double[count.Length, target_count];
            for(int i = 0; i < count.Length; i++)
            {
                for(int j = 0; j < target_value.Length; j++)
                {
                    if (attribute_value[i] != attribute[j]) continue;//选出当前属性值下的目标属性
                    for(int loop1 = 0; loop1 < sub_attribute_value.Length; loop1++)
                    {
                        if (target_value[j] == sub_attribute_value[loop1])
                        {
                            sub_count[i, loop1] += 1.0;
                            break;//加快运算速度
                        }
                    }
                }
            }
            //内部概率计算完毕，接下来计算GainA
            double[] sub_total = new double[count.Length];
            for(int i = 0; i < sub_total.Length; i++)
            {
                for(int j = 0; j < sub_count.GetLength(1); j++)
                {
                    sub_total[i] += sub_count[i, j];
                }
            }
            double InfoA = 0;
            for(int i = 0; i < count.Length; i++)
            {
                double sub_InfoA = 0;
                for(int j = 0; j < sub_count.GetLength(1); j++)
                {
                    sub_InfoA += (sub_count[i, j] / sub_total[i]) * DmMath.Log2( Math.Abs( (sub_count[i, j] +0.001) / (sub_total[i] + 0.001)));
                    //sub_InfoA += (sub_count[i, j] / sub_total[i]);
                }
                //InfoA += (count[i] / total_length) * sub_InfoA;
                InfoA += (count[i] / total_length) * sub_InfoA;
            }
            InfoA = -InfoA;//取反
            double GainA = infoD - InfoA;
            StreamReader sr = new StreamReader(@"./cache/gainA.cc");
            String content = "";
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                content += line;
            }
            sr.Close();
            StreamWriter sw = new StreamWriter(@"./cache/gainA.cc");
            sw.Write("");
            sw.Write(content + GainA + ";\n");
            sw.Close();
            return GainA;
        }
        //分裂信息的计算
        public double getSplitInfoA(object[,] data,int attribute_index)
        {
            return getInfoD(data, attribute_index);
        }
        //计算信息增益率
        public double getGainRate(object[,] data, int attribute_index,int target_index)
        {
            double GainRate = getSplitInfoA(data,attribute_index)/getGainA(data, attribute_index, target_index);
            return GainRate;
        }
        //挑选信息增益最大的属性
        public int getIndexOfRichestInfo(object[,] data,int target_index)
        {
            StreamReader sr = new StreamReader(@"./cache/gainratea.cc");
            String content = "";
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                content += line;
            }
            sr.Close();
            StreamWriter sw = new StreamWriter(@"./cache/gainratea.cc");
            double[] GainRateA = new double[data.GetLength(1)];
            for(int i = 0; i < data.GetLength(1); i++)
            {
                if (i == target_index) continue;
                GainRateA[i] = getGainRate(data, i, target_index);
                content += GainRateA[i] + ";\n";
            }
            sw.Write("");
            sw.Write(content);
            sw.Close();
            int max_index = 10;
            double max_rate = -10000.0;
            GainRateA[target_index] = max_rate;
            for(int i = 0; i < GainRateA.Length; i++)
            {
                if (GainRateA[i] > max_rate)
                {
                    max_rate = GainRateA[i];
                    max_index = i;
                }
            }
            if(Info_threshold <= max_rate) return max_index;
            return target_index;
        }
        //生成节点和树枝
        public void createTree()
        {
            //获取目标属性的索引
            int loop1 = 0;
            while (loop1 < data_obj.GetLength(1))
            {
                if (data_obj[0, loop1].ToString() == Target_attribute) break;
                ++loop1;
            }
            //创建节点
            createNode(data_obj, loop1, 0, 0);
        }
        //绘制决策树
        public void figure()
        {
            createTree();//建树
            //将节点数据和树枝数据保存到缓存
            StreamWriter myStream = new StreamWriter(@"./cache/figure.fg");
            String temp_str = "Method DecisionTree;";
            myStream.Write(temp_str + "\n");
            temp_str = "";
            //写入节点数据
            for(int i = 0; i < node.Length; i++)
            {
                temp_str += "Node " + node[i].Scale + "," + node[i].Serial + "," + node[i].Name + ";\n";
            }
            myStream.Write(temp_str);
            //写入树枝数据
            temp_str = "";
            for(int i = 0; i < brunch.Length; i++)
            {
                temp_str += "Brunch " + brunch[i].Scale + "," + brunch[i].Serial + "," + brunch[i].Name + ";\n";
            }
            myStream.Write(temp_str);
            myStream.Close();//关闭流
            now_serial = 0;//序列归零
        }
        //生成节点和树枝
        public void createNode(object[,] data, int target_index, int scale, int serial)
        {
            int attribute_index = getIndexOfRichestInfo(data, target_index);//获取信息增益率最高的属性索引
            if (attribute_index == target_index)
            {
                //生成决策节点
                //统计决策属性中各个属性值的出现次数
                int[] count = new int[0];
                String[] value_name = new String[0];
                for(int i = 1; i < data.GetLength(0); i++)
                {
                    if (DmMath.isDifferent(data[i, target_index].ToString(), value_name))
                    {
                        int[] temp_count = count;
                        count = new int[temp_count.Length + 1];
                        String[] temp_value_name = value_name;
                        value_name = new String[temp_value_name.Length + 1];
                        for(int j = 0; j < temp_count.Length; j++)
                        {
                            count[j] = temp_count[j];
                            value_name[j] = temp_value_name[j];
                        }
                        count[temp_count.Length] = 1;
                        value_name[temp_value_name.Length] = data[i, target_index].ToString();
                        continue;
                    }
                    else
                    {
                        for(int j = 0; j < value_name.Length; j++)
                        {
                            if (data[i, target_index].ToString() == value_name[j])
                            {
                                ++count[j];
                                break;
                            }
                        }
                    }
                }
                //选择出现次数最大的属性，并计算出现的概率
                int max_count = 0;
                double sum = 0;
                String max_value = "";
                for(int i = 0; i < count.Length; i++)
                {
                    sum += (double)count[i];
                    if (count[i] > max_count)
                    {
                        max_count = count[i];
                        max_value = value_name[i];
                    }
                }
                double temp_rate = ((double)max_count) / sum;//计算概率
                max_value += "_" + temp_rate;
                //创建决策节点
                Node[] temp_node1 = node;
                node = new Node[temp_node1.Length + 1];
                for(int i = 0; i < temp_node1.Length; i++)
                {
                    node[i] = temp_node1[i];
                }
                node[temp_node1.Length] = new Node(scale, serial, data[0,target_index].ToString()+"="+max_value);
                ++now_serial;
                return;
            }
            String name = data[0, attribute_index].ToString();//获取该属性的名称
            String[] attribute_value = new String[0];//获取该属性值的名称
            for(int i = 1; i < data.GetLength(0); i++)
            {
                if (DmMath.isDifferent(data[i, attribute_index].ToString(), attribute_value))
                {
                    //增加属性元素
                    attribute_value = DmMath.addElement(attribute_value, data[i, attribute_index].ToString());
                }
            }
            //构建节点
            Node[] temp_node = node;
            node = new Node[temp_node.Length + 1];
            for(int i = 0; i < temp_node.Length; i++)
            {
                node[i] = temp_node[i];
            }
            node[temp_node.Length] = new Node(scale, serial, name);
            //构建树枝，并进行深度优先递归
            for(int i = 0; i < attribute_value.Length; i++)
            {
                Brunch[] temp_brunch = brunch;
                brunch = new Brunch[temp_brunch.Length + 1];
                for(int j = 0; j < temp_brunch.Length; j++)
                {
                    brunch[j] = temp_brunch[j];
                }
                brunch[temp_brunch.Length] = new Brunch(scale,  now_serial, "=" + attribute_value[i]);//构建树枝
                //筛选数据
                object[,] next_data = pickUpData(attribute_value[i], data, attribute_index, attribute_index);
                if (attribute_index < target_index)
                {
                    createNode(next_data, target_index - 1, scale + 1, now_serial);
                }
                else
                {
                    createNode(next_data, target_index, scale + 1, now_serial);
                }
            }
        }
        //筛选数据
        public object[,] pickUpData(String key_word,object[,] data, int key_index, int avoid_index)
        {
            object[,] next_data = new object[1, data.GetLength(1) - 1];
            int loop1 = 0;
            //属性名赋值
            for(int i = 0; i < data.GetLength(1); i++)
            {
                if (i == avoid_index) continue;
                next_data[0, loop1] = data[0, i];
                ++loop1;
            }
            for(int i = 1; i < data.GetLength(0); i++)
            {
                if (data[i, key_index].ToString() == key_word)
                {
                    object[,] temp_next_data = next_data;
                    next_data = new object[temp_next_data.GetLength(0) + 1, temp_next_data.GetLength(1)];
                    for(int j = 0; j < temp_next_data.GetLength(0); j++)
                    {
                        for(int k = 0; k < temp_next_data.GetLength(1); k++)
                        {
                            next_data[j, k] = temp_next_data[j, k];
                        }
                    }
                    int loop2 = 0;
                    for(int j = 0; j < data.GetLength(1); j++)
                    {
                        if (j == avoid_index) continue;
                        next_data[temp_next_data.GetLength(0), loop2] = data[i, j];
                        ++loop2;
                    }
                }
            }
            return next_data;
        }
        //节点类

        public class Node
        {
            //节点的位置用编码存储,编码有两组，一组显示该节点所在的层数，第二组显示该节点在该层的位置
            public int Scale;//节点所在层数
            public int Serial;//节点所在的位置
            public String Name;//名字
            public Node() { }
            public Node(int scale, int serial, String name)
            {
                Scale = scale;
                Serial = serial;
                Name = name;
            }
        }
        //树枝类
        public class Brunch
        {
            public int Scale;
            public int Serial;
            public String Name;
            public Brunch() { }
            public Brunch(int scale, int serial, String name)
            {
                Scale = scale;
                Serial = serial;
                Name = name;
            }
        }
    }
}
