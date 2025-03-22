using System.Drawing;
using UnityEngine.UI;
//using System.Drawing.Imaging; 

namespace MVC
{
    /// <summary>
	/// 颜色显示功能
	/// </summary>
	public static class Show_Color
    {
        /// <summary>
		/// 红色
		/// </summary>
		/// <param name="red"></param>
		/// <returns></returns>
		public static string Red<T>(T red)
        {

            return "<color=red>" + red + "</color>";
        }
        /// <summary>
        /// 白色
        /// </summary>
        /// <param name="white"></param>
        /// <returns></returns>
        public static string White<T>(T white)
        {
            return "<color=white>" + white + "</color>";
        }
        /// <summary>
        /// 绿色
        /// </summary>
        /// <param name="green"></param>
        /// <returns></returns>
        public static string Green<T>(T green)
        {
            return "<color=green>" + green + "</color>";
        }
        /// <summary>
        /// 蓝色
        /// </summary>
        /// <param name="green"></param>
        /// <returns></returns>
        public static string Blue<T>(T Blue)
        {
            return "<color=blue>" + Blue + "</color>";

            //Blue.ToString().color=
        }
        /// <summary>
        /// 紫色
        /// </summary>
        /// <param name="purple"></param>
        /// <returns></returns>
        public static string Purple<T>(T purple)
        {
            return "<color=purple>" + purple + "</color>";
        }
        /// <summary>
        /// 黄色
        /// </summary>
        /// <param name="yellow"></param>
        /// <returns></returns>
        public static string Yellow<T>(T yellow)
        {
            return "<color=yellow>" + yellow + "</color>";
        }
        /// <summary>
        /// 橙色
        /// </summary>
        /// <param name="orange"></param>
        /// <returns></returns>
        public static string Orange<T>(T orange)
        {
            return "<color=orange>" + orange + "</color>";
        }
        /// <summary>
        /// 粉色
        /// </summary>
        /// <param name="pink"></param>
        /// <returns></returns>
        public static string Pink<T>(T pink)
        {
            return "<color=pink>" + pink + "</color>";
        }
        /// <summary>
        /// 灰色
        /// </summary>
        /// <param name="grey"></param>
        /// <returns></returns>
        public static string Grey<T>(T grey)
        {
            return "<color=grey>" + grey + "</color>";
        } 
    }
}
