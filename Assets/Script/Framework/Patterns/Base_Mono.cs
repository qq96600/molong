
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
        /// <summary>
        /// 单位
        /// </summary>
        protected enum DicList
        {
            金币,
            Boss点,
            元宝,
        }

        /// <summary>
        /// 物资需求列表
        /// </summary>
        protected Dictionary<string, int> dic = new Dictionary<string, int>();
        /// <summary>
        /// 类型
        /// </summary>
        /// <summary>
        /// 元宝消耗
        /// </summary>
        /// <param name="moeny"></param>
        protected void silverCoin(int moeny, string value = "", int type = 1)
        {
            /*
            if (type == 1)
            {
                if (SumSave.crt_user.Silver >= moeny)
                {
                    //绑定元宝
                    SendNotification(NotiList.silverCoin, -moeny);
                }
                else
                {
                    //充值元宝
                    moeny -= SumSave.UserBase.Silver;

                    SendNotification(NotiList.silverCoin, -SumSave.UserBase.Silver + "");

                    SendNotification(NotiList.SubtractDiamonds, moeny + "+" + value);

                }
            }
            else
            {
                SendNotification(NotiList.SubtractDiamonds, moeny + "+" + value);
            }


            */


        }

        /// <summary>
        /// 消耗物品
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        protected bool RefreshConsumables()
        {
            return true;
            /*
            Dictionary<string, int> keys = dic;
            bool exist = false;
            //dic.Clear();
            int count = 0, moeny = 0, MilitaryExploits = 0, silver = 0;
            SumSave.UserConsumables.Clear();
            foreach (string item in keys.Keys)
            {
                if (item == DicList.金币.ToString())
                {
                    if (SumSave.UserBase.Money >= Mathf.Abs(keys[item]))
                    {
                        moeny = Mathf.Abs(keys[item]);
                        count++;
                    }
                    else 称号中心.Instance.alertdec("金币不足需要 " + keys[item]);
                }
                else
                if (item == DicList.Boss点.ToString())
                {
                    if (SumSave.UserBase.MilitaryExploits >= Mathf.Abs(keys[item]))
                    {
                        MilitaryExploits = Mathf.Abs(keys[item]);
                        count++;
                    }
                    else
                        称号中心.Instance.alertdec("Boss点不足需要 " + keys[item]);
                }
                else if (item == DicList.元宝.ToString())
                {
                    if (SumSave.UserBase.Silver >= Mathf.Abs(keys[item]))
                    {
                        silver = Mathf.Abs(keys[item]);
                        count++;
                    }
                    else
                        称号中心.Instance.alertdec("元宝不足需要 " + keys[item]);
                }
                else
                    foreach (Bag_Consumables_VO bag in SumSave.CrtAllConsumables)
                    {
                        if (item == bag.Name)
                        {

                            if (bag.Number >= Mathf.Abs(keys[item]))
                            {
                                Addition.DropBag(item, -Mathf.Abs(keys[item]), 3);
                                count++;
                            }
                            else
                            {
                                称号中心.Instance.alertdec(bag.Name + " 不足需要 " + keys[item]);
                                SumSave.UserConsumables.Clear();
                            }
                            continue;
                        }
                    }
            }
            if ((count == keys.Count))
            {
                if (moeny > 0) SendNotification(NotiList.Add_Money, -Mathf.Abs(moeny));
                if (silver > 0) SendNotification(NotiList.silverCoin, -Mathf.Abs(silver));
                if (MilitaryExploits > 0)
                {
                    SumSave.UserBase.MilitaryExploits -= MilitaryExploits;
                    SendNotification(NotiList.MilitaryExploits, 0);
                }
                SendNotification(NotiList.RefreshConsumables);
            }
            else SumSave.UserConsumables.Clear();
            exist = count == keys.Count;
            dic.Clear();
            return exist;
            */
        }
        /// <summary>
        /// 获取需求
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="value"></param>
        //[System.Obsolete("消耗材料使用->称号中心.Instance.RedConsumables()")]
        protected void NeedConsumables(object keys, int value)
        {
            if (!dic.ContainsKey(keys.ToString())) dic.Add(keys.ToString(), value);
            else dic[keys.ToString()] += value;
        }

    }
}
