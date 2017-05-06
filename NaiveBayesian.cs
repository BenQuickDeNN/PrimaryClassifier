using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 分类器
{
    class NaiveBayesian
    {//朴素贝叶斯类
        public object[,] data_obj;//数据对象
        public String targetName;//目标特征名称
        public String[] targetAttribute;//目标特征内容
        int column = 1;
        public String[] attribute;//其他特征
        public String[] attribute_content;//其他特征内容
        public double[,] P_attribute_content;//其他特征内容的概率分布
        public double[] P_targetAttribute;//目标特征内容的概率分布
        //指令常量
        public const int METHOD_NAIVEBAYESIAN = 0xf100;
        public const int METHOD_COMMAND_SETTARGETATTRIBUTE = 0xf110;
        public const int METHOD_COMMAND_ATTRIBUTE_DEF = 0xf111;
        public const int METHOD_COMMAND_ATTRIBUTE_END = 0xf112;
        public const int ATTRIBUTE_NOT_FOUND = 0xf11f;
        public const int ERROR_PARAMETER = 0xf11e;
        public NaiveBayesian(object[,] data)
        {
            data_obj = data;
            targetAttribute = new String[1];
        }
        public bool setTargetAttribute(String attribute)
        {
            //选择属性名为attribute的一列
            bool FLAG_IS_EXIST = false;
            while (column <= data_obj.GetLength(1))
            {
                if (data_obj[1, column].ToString() == attribute)
                {
                    FLAG_IS_EXIST = true;
                    break;
                }
                ++column;
            }
            if (!FLAG_IS_EXIST)
            {
                --column;
                return false;//没有找到该属性，返回
            }
            //统计特征内容的种类
            targetName = attribute;
            int feature_count = 1;
            targetAttribute[0] = data_obj[2, column].ToString();//至少有一种类型
            for (int i = 2; i <= data_obj.GetLength(0); i++)
            {
                bool FLAG_FIND_MORE_CONTENT = true;
                for(int j = 0; j < targetAttribute.Length; j++)
                {
                    if (data_obj[i, column].ToString() == targetAttribute[j])
                    {
                        FLAG_FIND_MORE_CONTENT = false;
                    }
                }
                if (FLAG_FIND_MORE_CONTENT)
                {
                    ++feature_count;
                    String[] temp = targetAttribute;
                    targetAttribute = new String[feature_count];
                    for(int j = 0; j < feature_count - 1; j++)
                    {
                        targetAttribute[j] = temp[j];
                    }
                    targetAttribute[feature_count - 1] = data_obj[i, column].ToString();//新增类型
                }
            }
            //计算目标特征内容的概率
            P_targetAttribute = new double[feature_count];
            double total_feature = (double)(data_obj.GetLength(0) - 1);//总的内容数
            //计算各种内容的概率
            for(int i = 0; i < feature_count; i++)
            {
                double content_count = 0;//统计内容的个数
                for(int j = 2; j <= data_obj.GetLength(0); j++)
                {
                    if (data_obj[j, column].ToString() == targetAttribute[i])
                    {
                        content_count += 1.0;
                    }
                }
                P_targetAttribute[i] = content_count / total_feature;//得到对应的内容的概率
            }
            return true;
        }
        //设置其他特征
        public void setAttribute(String[] line)
        {
            attribute = line;
            attribute_content = new String[attribute.Length];//特征内容初始化
            P_attribute_content = new double[attribute.Length, targetAttribute.Length];//特征内容概率初始化

        }
        //设置其他特征内容
        public bool setAttributeValue(String[] line)
        {
            if (line.Length != attribute_content.Length) return false;
            attribute_content = line;
            //用三重循环，计算其他特征内容的概率分布
            for(int i1 = 0; i1 < targetAttribute.Length; i1++)
            {//遍历所有的目标特征
                double target_count = 0;//记录目标特征内容数
                for(int i2 = 2; i2 <= data_obj.GetLength(0); i2++)
                {
                    if (targetAttribute[i1] == data_obj[i2, column].ToString())
                    {
                        target_count += 1.0;
                    }
                }
                int attribute_count = 0;//记录翻到的特征名
                for(int i2 = 1; i2 <= data_obj.GetLength(1); i2++)
                {//遍历所有的特征
                    if(attribute_count<attribute.Length)
                    if (data_obj[1, i2].ToString() == attribute[attribute_count])
                    {
                        double content_count = 0;//记录内容出现次数
                        for (int i3 = 2; i3 <= data_obj.GetLength(0); i3++)
                        {//遍历特征的内容
                            if (data_obj[i3, i2].ToString() == attribute_content[attribute_count] && 
                                data_obj[i3,column].ToString() == targetAttribute[i1])
                            {
                                content_count += 1.0;
                            }
                        }
                        P_attribute_content[attribute_count, i1] = content_count / target_count;//计算特征内容分布概率
                        attribute_count++;
                    }
                }
            }
            return true;
        }
        //显示结果
        public String display()
        {
            String result = "";
            for(int i = 0; i < P_attribute_content.GetLength(0); i++)
            {
                for(int j = 0; j < P_attribute_content.GetLength(1); j++)
                {
                    result += "P(" + attribute[i] + " = " + attribute_content[i] + " | " + targetName + " = " +
                        targetAttribute[j] + ") = " + P_attribute_content[i, j] + "\n";
                }
            }
            //计算在满足一定特征的情况下，目标特征内容的概率
            double[] P_result = new double[targetAttribute.Length];
            for(int i = 0; i < P_result.Length; i++)
            {
                double p_temp = 1.0;
                for(int j = 0; j < attribute.Length; j++)
                {
                    p_temp = p_temp * P_attribute_content[j, i];
                }
                P_result[i] = p_temp * P_targetAttribute[i];
                result += "P(attributeValue | " + targetName + " = " + targetAttribute[i] + ")P(" +
                    targetName + " = " + targetAttribute[i] + ") = " + P_result[i] + "\n";
            }
            //计算各事件发生的概率
            double one = 1;
            double temp_all = 0;
            for(int i = 0; i < P_result.Length; i++)
            {
                temp_all += P_result[i];
            }
            double temp_time = one / temp_all;//倍数
            double[] P_result2 = new double[P_result.Length];
            for(int i = 0; i < P_result.Length; i++)
            {
                P_result2[i] = P_result[i] * temp_time;
                result += "P(" + targetName + " = " + targetAttribute[i] + " | attributeValue) = " + P_result2[i] + "\n";
            }
            return result;
        }
    }
}
