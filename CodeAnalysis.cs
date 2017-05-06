using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 分类器
{//代码分析器
    class CodeAnalysis
    {
        //指令常量集
        public const int COMMAND_IMPORT_DATA = 0xf001;
        public const int COMMAND_START = 0xf002;
        public const int COMMAND_USE = 0xf003;
        public const int COMMAND_END = 0xf004;
        public const int COMMAND_UNDEFINE_COMMAND = 0xf00f;
        //方法
        public NaiveBayesian nb;
        public KNearestNeibor knn;
        public DecisionTree dt;
        public CodeAnalysis() { }
        //用于提取某字符之前的字符串
        public static String getStringBefore(String str, char beforeEle)
        {
            String result = "";
            for(int i = 0; i < str.Length; i++)
            {
                if (str.ElementAt<char>(i) == beforeEle) break;
                result += str.ElementAt<char>(i).ToString();
            }
            return result;
        }
        public static String getStringAfter(String str,char afterele)
        {
            String result = "";
            for (int i = str.Length-1; i >= 0; i--)
            {
                if (str.ElementAt<char>(i) == afterele) break;
                result += str.ElementAt<char>(i).ToString();
            }
            return result;
        }
        //获取指令
        public static String getCommandString(String str)
        {
            String command = "";
            command = getStringBefore(str, ';');//获取分号之前的内容
            //从左到右，从开始有字的部分到空格之前
            String temp = "";
            for(int i = 0; i < command.Length; i++)
            {
                if (temp != "" &&( command.ElementAt<char>(i) == ' ' || command.ElementAt<char>(i) == '\t')) break;
                if (command.ElementAt<char>(i)!=' ' || command.ElementAt<char>(i) == '\t')
                {
                    temp += command.ElementAt<char>(i).ToString();
                }
            }
            return temp;
        }
        //获取操作变量
        public static String[] getValueString(String str)
        {
            String temp1 = getStringBefore(str, ';');//获取分号之前的部分
            //从右到左，从开始有字的部分到空格之前
            String temp2 = "";
            for(int i = temp1.Length - 1; i >= 0; i--)
            {
                if (temp1.ElementAt<char>(i) == ' ' && temp2 != "") break;
                if(temp1.ElementAt<char>(i)!=' ')
                {
                    temp2 += temp1.ElementAt<char>(i).ToString();
                }
            }
            String temp3 = "";
            //字符串倒置
            for(int i = temp2.Length - 1; i >= 0; i--)
            {
                temp3 += temp2.ElementAt<char>(i).ToString();
            }
            //检验该行是否为不带操作变量的指令
            if (temp3 == getCommandString(str))
            {
                String[] empty = new String[1];
                empty[0] = "";
                return empty;
            }
            //统计逗号的个数
            int num_of_dh = 0;
            for(int i = 0; i < temp3.Length; i++)
            {
                if (temp3.ElementAt<char>(i) == ',') ++num_of_dh;
            }
            String[] result = new String[num_of_dh + 1];
            //逐个存入变量名
            int loop1 = 0;
            for(int i = 0; i < result.Length; i++)
            {
                while (loop1 < temp3.Length)
                {
                    if (temp3.ElementAt<char>(loop1) == ',')
                    {
                        ++loop1;
                        break;
                    }
                    result[i] += temp3.ElementAt<char>(loop1).ToString();
                    ++loop1;
                }
            }
            return result;
        }
        //根据数字转换成Excel列号
        public static String getExcelColumn(int num)
        {
            String column = "";
            int x = num;
            const int letter = 26;
            while (x > 0)
            {
                int temp = 'A' + ((x - 1) % letter);
                column += (char)temp;
                x = x / letter;
            }
            //倒置输出字符
            String result = "";
            for(int i = column.Length - 1; i >= 0; i--)
            {
                result += column.ElementAt<char>(i).ToString();
            }
            return result;
        }
        //依据指令名执行相应的方法
        public int doActionAccordingToCommand(String str, object[,] data, int action_model)
        {
            String command = getCommandString(str);
            String[] value = getValueString(str);
            //公共指令
            switch (command)
            {
                case "use":
                    if (value.Length < 1) break;
                    switch (value[0])
                    {
                        case "NaiveBayesian":
                            nb = new NaiveBayesian(data);
                            return NaiveBayesian.METHOD_NAIVEBAYESIAN;//返回操作模型为朴素贝叶斯
                        case "K_NearestNeighbor":
                            knn = new KNearestNeibor(data);
                            return KNearestNeibor.METHOD_KNEARESTNEIGHBOR;//返回操作模型为K最近邻
                        case "DecisionTree":
                            dt = new DecisionTree(data);
                            return DecisionTree.METHOD_DECISIONTREE;
                    }
                    return COMMAND_USE;
            }
            //操作模型特有指令
            switch (action_model)
            {
                case NaiveBayesian.METHOD_NAIVEBAYESIAN:
                    switch (command)
                    {
                        case "setTargetAttribute":
                            nb.setTargetAttribute(value[0]);
                            break;
                        case "attribute":
                            nb.setAttribute(value);
                            break;
                        case "attributeValue":
                            nb.setAttributeValue(value);
                            break;
                    }
                    return NaiveBayesian.METHOD_NAIVEBAYESIAN;
                case KNearestNeibor.METHOD_KNEARESTNEIGHBOR:
                    switch (command)
                    {
                        case "setType":
                            knn.setType(value[0], int.Parse(value[1]) - 1, int.Parse(value[2]) - 1);
                            break;
                        case "setKValue":
                            knn.setKValue(int.Parse(value[0]));
                            break;
                        case "setMethodDist":
                            knn.setMethodDist(value[0]);
                            break;
                        case "setWeighVector":
                            knn.setWeighVector(value);
                            break;
                        case "setSample":
                            knn.setSample(int.Parse(value[0]) - 1);
                            break;
                        case "test":
                            knn.testResult(value[0]);
                            break;
                    }
                    return KNearestNeibor.METHOD_KNEARESTNEIGHBOR;
                case DecisionTree.METHOD_DECISIONTREE:
                    switch (command)
                    {
                        case "setThreshold":
                            dt.setInfoThreshold(double.Parse(value[0]));
                            break;
                        case "setTargetAttribute":
                            dt.setTargetAttribute(value[0]);
                            break;
                    }
                    return DecisionTree.METHOD_DECISIONTREE;
            }
            return COMMAND_UNDEFINE_COMMAND;
        }
    }
}
