using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 分类器
{
    class DmMath
    {
        public static double Log2(double x)
        {
            if (x == 1) return 10.0;
            return (Math.Log(x) / Math.Log(2));
        }
        public static double[] sort(double[] array)
        {
            double[] result = array;
            for(int i = 0; i < result.Length; i++)
            {
                for(int j = i + 1; j < result.Length; j++)
                {
                    if (result[i] > result[j])
                    {
                        double temp = result[i];
                        result[i] = result[j];
                        result[j] = temp;
                    }
                }
            }
            return result;
        }
        public static bool isDifferent(String str, String[] array, int index)
        {
            if (index >= array.Length) return false;
            for(int i = 0; i < index; i++)
            {
                if (str == array[i]) return false;
            }
            return true;
        }
        public static bool isDifferent(String str,String[] array)
        {
            for(int i = 0; i < array.Length; i++)
            {
                if (str == array[i]) return false;
            }
            return true;
        }
        public static String[] addElement(String[] source, String element)
        {
            String[] temp = source;
            String[] temp2 = new String[temp.Length + 1];
            for(int i = 0; i < temp.Length; i++)
            {
                temp2[i] = temp[i];
            }
            temp2[temp.Length] = element;
            return temp2;
        }
    }
}
