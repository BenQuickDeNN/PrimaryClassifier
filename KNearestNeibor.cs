using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 分类器
{
    class KNearestNeibor
    {//K近邻分类法
        public object[,] data_obj;//待分析数据
        public String[] str_type_name;//类型名，长度和待分析数据相同
        public object[] data_sample;//待分类样本
        public double[] Weigh_Vector;//权向量
        public int K_value;//最近邻数
        public int R;//最近邻圆半径
        public double[] K_dist;//距离数组
        private double[] K_dist_sort;//排序后的距离数组
        private String[] str_type_name_sort;//排序后的类型名
        public int[] X_axis;//元组的横坐标，用来画图
        public int[] Y_axis;//元组的纵坐标
        public String method_dist = METHOD_DIST_ATTRIBUTE;//使用的距离衡量方式，默认为属性距离
        //指令常量
        public const int METHOD_KNEARESTNEIGHBOR = 0xf200;
        public const String METHOD_DIST_ECULID = "Eculid";//空间距离
        public const String METHOD_DIST_ATTRIBUTE = "Attribute";//属性距离
        public const String METHOD_TEST_TRAINING = "Training";
        public KNearestNeibor(object[,] data)
        {
            //预赋值
            data_obj = new object[data.GetLength(0) - 1, data.GetLength(1)];
            for(int i = 0; i + 2 <= data.GetLength(0); i++)
            {
                for(int j = 0; j + 1 <= data.GetLength(1); j++)
                {
                    data_obj[i, j] = data[i + 2, j + 1];
                }
            }
            str_type_name = new String[data_obj.GetLength(0)];
            K_dist = new double[str_type_name.Length];
            Weigh_Vector = new double[data_obj.GetLength(1)];
            X_axis = new int[str_type_name.Length];
            Y_axis = new int[str_type_name.Length];
        }
        public void setSample(int index)
        {//依据索引来设置样本,这个方法是主要的执行方法
            data_sample = new object[data_obj.GetLength(1)];
            for(int i = 0; i < data_sample.Length; i++)
            {
                data_sample[i] = data_obj[index, i];
            }

        }
        //设置权向量
        public void setWeighVector(String[] vector)
        {
            double[] dvector = new double[vector.Length];
            for(int i = 0; i < dvector.Length; i++)
            {
                dvector[i] = double.Parse(vector[i]);
            }
            Weigh_Vector = dvector;
        }
        //设置类型名称
        public void setType(String type_name,int index1,int index2)
        {
            for(int i = index1; i <= index2; i++)
            {
                str_type_name[i] = type_name;
            }
        }
        //设置最近邻数
        public void setKValue(int k)
        {
            K_value = k + 1;
        }
        //设置距离衡量方式
        public void setMethodDist(String method)
        {
            method_dist = method;
        }
        //计算距离
        private void doCreateDist()
        {
            switch (method_dist)
            {
                case METHOD_DIST_ECULID://欧几里得距离
                    for(int i = 0; i < K_dist.Length; i++)
                    {
                        double temp = 0;
                        for(int j = 0; j < data_sample.Length; j++)
                        {
                            temp += Math.Sqrt((double.Parse(data_sample[j].ToString()) -
                                double.Parse(data_obj[i, j].ToString())) *
                                (double.Parse(data_sample[j].ToString()) -
                                double.Parse(data_obj[i, j].ToString()))) * Weigh_Vector[j];
                        }
                        K_dist[i] = temp;//距离赋值
                    }
                    break;
                case METHOD_DIST_ATTRIBUTE://属性距离
                    for(int i = 0; i < K_dist.Length; i++)
                    {
                        double temp = 0;
                        for (int j = 0; j < data_sample.Length; j++)
                        {
                            if (data_sample[j].ToString() != data_obj[i, j].ToString())
                            {
                                temp += 1.0 * Weigh_Vector[j];//如果属性不同距离就加1
                            }
                        }
                        K_dist[i] = temp;
                    }
                    break;
            }
        }
        //排序,选出最近的K个邻居
        private void findNeighbor()
        {
            //同时对距离和类型名排序
            str_type_name_sort = str_type_name;
            K_dist_sort = K_dist;
            for(int i = 0; i < K_dist_sort.Length; i++)
            {
                for(int j = i + 1; j < K_dist_sort.Length; j++)
                {
                    if (K_dist_sort[i] > K_dist_sort[j])
                    {
                        double temp = K_dist_sort[i];
                        K_dist_sort[i] = K_dist_sort[j];
                        K_dist_sort[j] = temp;
                        String str_temp = str_type_name_sort[i];
                        str_type_name_sort[i] = str_type_name_sort[j];
                        str_type_name_sort[j] = str_temp;
                    }
                }
            }
        }
        //生成横纵坐标，以便画图
        public void createAxis(int w,int h)
        {
            int temp = w < h ? w : h;//选择长宽中较小的一个作为画布尺寸
            double inch = (double)temp;
            //获取中心坐标
            double center_x =(double)( w / 2);
            double center_y = (double)(h / 2);
            double chengshu = inch /(2 * K_dist_sort[K_dist_sort.Length - 1]);//获取变换比例所需的乘数
            //为每一个距离设置半径及横纵坐标
            for(int i = 0; i < K_dist_sort.Length; i++)
            {
                double r = K_dist_sort[i] * chengshu;
                Random rd = new Random();
                double x = rd.Next(0, (int)r);//随机生成横坐标
                if (i % 5 == 0 || i % 7 == 0)
                {
                    x = -x;
                }
                double y = Math.Sqrt(r * r - x * x);//纵坐标算出来
                if (i % 2 == 0) y = -y;//如果x为偶数，则y取负
                if (i == K_value)
                {
                    R = (int)r;
                }
                //坐标赋值回去
                X_axis[i] = (int)(x + center_x);
                Y_axis[i] = (int)(y + center_y);
            }
        }
        //显示结果
        public String display()
        {
            doCreateDist();//生成距离
            findNeighbor();//距离排序
            String result = "";
            String[] target = new String[K_value];
            for (int i = 0; i < K_value; i++)
            {
                result += "type name: " + str_type_name_sort[i] + " distance: " + K_dist_sort[i] + "\n";
                target[i] = str_type_name_sort[i];
            }
            result += "the sample is belong to " + getInfo(target) + "\n";
            return result;
        }
        //显示图形
        public void figure()
        {
            createAxis(784, 530);//图片尺寸为530
            StreamWriter myStream = new StreamWriter(@"./cache/figure.fg");
            String temp_str = "Method KNearestNeighbor;";
            myStream.Write(temp_str + "\n");
            temp_str = "TypeName ";//类型名
            for (int i = 0; i < str_type_name_sort.Length - 1; i++)
            {
                temp_str += str_type_name_sort[i] + ",";
            }
            temp_str += str_type_name_sort[str_type_name_sort.Length - 1] + ";";
            myStream.Write(temp_str + "\n");
            temp_str = "XAxis ";//横坐标
            for(int i = 0; i < X_axis.Length - 1; i++)
            {
                temp_str += X_axis[i] + ",";
            }
            temp_str += X_axis[X_axis.Length - 1] + ";";
            myStream.Write(temp_str + "\n");
            temp_str = "YAxis ";
            for(int i = 0; i < Y_axis.Length - 1; i++)
            {
                temp_str += Y_axis[i] + ",";
            }
            temp_str += Y_axis[Y_axis.Length - 1] + ";";
            myStream.Write(temp_str + "\n");
            temp_str = "KValue " + (K_value - 1) + ";";
            myStream.Write(temp_str + "\n");
            temp_str = "RValue " + R + ";";
            myStream.Write(temp_str);
            myStream.Close();//关闭流
        }
        //获取出现次数最多的字符串
        private String getInfo(String[] str)
        {
            int[] count = new int[str.Length];
            for(int i = 0; i < str.Length; i++)
            {
                for(int j = i + 1; j < str.Length; j++)
                {
                    if (str[i] == str[j]) ++count[i];
                }
            }
            int max_count = 0;
            String result = "";
            for(int i = 0; i < count.Length; i++)
            {
                if (count[i] > max_count)
                {
                    max_count = count[i];
                    result = str[i];
                }
            }
            return result;
        }
        //测试方法
        public String testResult(String method)
        {

            switch (method)
            {
                case METHOD_TEST_TRAINING:
                    double correct_instance = 0;
                    double sum = (double)data_obj.GetLength(0);
                    for (int i = 0; i < data_obj.GetLength(0); i++)
                    {
                        setSample(i);//测试样本
                        doCreateDist();//生成距离
                        findNeighbor();//距离排序
                        String[] target = new String[K_value];
                        for (int j = 0; j < K_value; j++)
                        {
                            target[j] = str_type_name_sort[j];
                        }
                        if (getInfo(target) == str_type_name[i]) correct_instance += 1.0;
                    }
                    double correct_rate = correct_instance / sum;
                    return "correct instance: " + (int)correct_instance + "\nall instance: " + (int)sum + "\n"
                        + "correct rate: " + correct_rate * (double)(100) + "%\n";
            }
            return "";
        }
        //寻找最佳K值
        public int getBestKValue()
        {
            int max_correct = 0;
            int best_k = 2;
            int correct_instance = 0;
            for (int k = 2; k < data_obj.GetLength(0); k++)
            {
                correct_instance = 0;
                for (int i = 0; i < data_obj.GetLength(0); i++)
                {
                    setSample(i);//测试样本
                    doCreateDist();//生成距离
                    findNeighbor();//距离排序
                    String[] target = new String[k];
                    for (int j = 0; j < k; j++)
                    {
                        target[j] = str_type_name_sort[j];
                    }
                    if (getInfo(target) == str_type_name[i]) ++correct_instance;
                }
                debug();
                if (correct_instance > max_correct)
                {
                    max_correct = correct_instance;
                    best_k = k;
                }
            }
            --best_k;
            return best_k;
        }
        //调试函数
        private void debug() { }
    }
}
