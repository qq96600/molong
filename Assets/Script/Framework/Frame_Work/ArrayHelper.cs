using System;
using System.Collections.Generic;

namespace Common
{
    public static class ArrayHelper
    {
        /// <summary>
        /// 数字数组转字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Data_Encryption<T>(this T[] str)
        {
            string dec = "";

            for (int i = 0; i < str.Length; i++)
            {
                dec += (dec == "" ? "" : " ") + str[i];
            }
            return dec;
        }
        public static T[] FindAll<T>(List<T> array, Func<T, bool> handler)
        {
            List<T> result = new List<T>(array.Count);

            for (int i = 0; i < array.Count; i++)
            {
              
                if (handler(array[i]))
                {
                    result.Add(array[i]);
                }
            }
            return result.ToArray();
        }

        public static T Find<T>(this List<T> array, Func<T, bool> handler)
        {
            for (int i = 0; i < array.Count; i++)
            {
                if (handler(array[i]))
                {
                    return array[i];
                }
            }
            return default(T);
        }

        public static T Find<T>(this T[] array, Func<T, bool> handler)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (handler(array[i]))
                {
                    return array[i];
                }
            }
            return default(T);
        }
        /// <summary>
        /// 选择下一个属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="crt"></param>
        /// <returns></returns>
        public static T ObtainT<T>(List<T> array, T crt)
        {

            if (array == null || array.Count == 0) return default;

            int cerid = array.FindIndex(x => x.Equals(crt));

            if (cerid < array.Count - 1) return array[cerid + 1];

            else return array[0];
        }

        public static T GetMax<T, R>(this T[] array, Func<T, R> handler) where R : IComparable
        {
            if (array == null || array.Length == 0) return default(T);

            T max = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                //if (max.HP < array[i].HP)
                //if(XXX(max) < XXX(array[i]))
                //if(handler(max) < handler(array[i]))
                if (handler(max).CompareTo(handler(array[i])) < 0)
                    max = array[i];
            }
            return max;
        }
        public static T GetMax<T, R>(this List<T> array, Func<T, R> handler) where R : IComparable
        {
            if (array == null || array.Count == 0) return default(T);

            T max = array[0];

            for (int i = 1; i < array.Count; i++)
            {
                //if (max.HP < array[i].HP)
                //if(XXX(max) < XXX(array[i]))
                //if(handler(max) < handler(array[i]))
                if (handler(max).CompareTo(handler(array[i])) < 0)
                    max = array[i];
            }
            return max;
        }

        public static T GetMin<T, R>(List<T> array, Func<T, R> handler) where R : IComparable
        {
            if (array == null || array.Count == 0) return default(T);

            T min = array[0];
            for (int i = 1; i < array.Count; i++)
            {
                if (handler(min).CompareTo(handler(array[i])) > 0)
                    min = array[i];
            }
            return min;
        }

        public static R[] Select<T, R>(this T[] array, Func<T, R> handler)
        {
            R[] result = new R[array.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = handler(array[i]);
            }
            return result;
        }

        public static R[] Sort<T, R>(this T[] array, Func<T, R> handler)
        {
            R[] result = new R[array.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = handler(array[i]);
            }
            return result;
        }

        /// <summary>
        /// 降序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Q"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        public static T[] OrderDescding<T, Q>(this T[] array, Func<T, Q> condition) where Q : IComparable
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - 1 - i; j++)
                {
                    if (condition(array[j]).CompareTo(condition(array[j + 1])) < 0)
                    {
                        T temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }

            return array;
        }

        /// <summary>
        /// 降序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Q"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        public static List<T> OrderDescding<T, Q>(this List<T> array, Func<T, Q> condition) where Q : IComparable
        {
            for (int i = 0; i < array.Count- 1; i++)
            {
                for (int j = 0; j < array.Count - 1 - i; j++)
                {
                    if (condition(array[j]).CompareTo(condition(array[j + 1])) < 0)
                    {
                        T temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }

            return array;
        }

        public static T[] Ascending<T, Q>(this T[] array, Func<T, Q> condition) where Q : IComparable
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - 1 - i; j++)
                {
                    if (condition(array[j]).CompareTo(condition(array[j + 1])) > 0)
                    {
                        T temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }

            return array;
        }

        public static List<T> Ascending<T, Q>(this List<T> array, Func<T, Q> condition) where Q : IComparable
        {
            for (int i = 0; i < array.Count - 1; i++)
            {
                for (int j = 0; j < array.Count - 1 - i; j++)
                {
                    if (condition(array[j]).CompareTo(condition(array[j + 1])) > 0)
                    {
                        T temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }

            return array;
        }

    }
}