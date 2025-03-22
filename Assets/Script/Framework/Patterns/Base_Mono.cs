
using UnityEngine;
using TarenaMVC;
using System.Collections.Generic;
using Common;
using System;

namespace MVC
{
    /// <summary>
    ///  封装发送消息的功能
    /// </summary>
    public class Base_Mono : MonoBehaviour, INotifier
    {
        /// <summary>
        ///  发送消息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public void SendNotification(string name, object data = null)
        {
            AppFacade.I.SendNotification(name, data);
        }
        /// <summary>
        ///  获取组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        protected T Find<T>(string name)
        {
            if (transform.Find(name) == null)
            {
                Debug.LogError(this + " 子对象: " + name + " 没有找到!");
                return default(T);
            }
            return transform.Find(name).GetComponent<T>();
        }

        protected bool Batch_Resources(Dictionary<string, int> resources,string dec)
        {
            return true;
        }


        protected void base_info(string info)
        {
            //SendNotification(NotiList.Data_Log, DateTime.Now + " 获取材料 " + info);

        }

        /// <summary>
        /// 将时间转换为 HH:MM:SS
        /// </summary>
        /// <param name="totalSeconds"></param>
        /// <returns></returns>
        public  string ConvertSecondsToHHMMSS(int totalSeconds)
        {
            int hours = totalSeconds / 3600;
            int minutes = (totalSeconds % 3600) / 60;
            int seconds = totalSeconds % 60;

            return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        }

    }
}
