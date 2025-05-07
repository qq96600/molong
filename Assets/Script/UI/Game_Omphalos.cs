using Common;
using Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
namespace MVC
{
    /// <summary>
    /// 控制定时器
    /// </summary>
    public class Game_Omphalos : Base_Mono
    {
        private panel_fight panel_fight;
        public static Game_Omphalos i;
        private void Awake()
        {
            i= this;
            AppFacade.I.Startup();
            panel_fight = UI_Manager.I.GetPanel<panel_fight>();
        }
        private List<Base_Wirte_VO> wirtes = new List<Base_Wirte_VO>();
        /// <summary>
        /// 开启定时器 
        /// </summary>
        public void activation()
        {
            InvokeRepeating("CountTime", 1, 1);
            InvokeRepeating("Read_User_Ranks", 600, 600);
        }
        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="dec"></param>
        public void Alert_Info(string dec)
        {
            Alert.Show("异常提示", dec);
        }
        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="dec"></param>
        public void Alert_Show(string dec)
        {
            Alert_Dec.Show(dec);
        }
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="log"></param>
        public void LogList(string log)
        {
            //写入日志
            SendNotification(NotiList.loglist, log);
        }
        /// <summary>

        /// 时间计数器
        /// </summary>
        private int performTime=0;
        /// <summary>
        /// 写入数据 每1s写入一次
        /// </summary>
        private void CountTime()
        {
            performTime++;
            if (panel_fight.gameObject.activeInHierarchy)
            {
                Combat_statistics.Time();
                panel_fight.Show_Combat_statistics();
            }
            if (performTime==60)
            {
                performTime = 0;
                
                SumSave.crt_achievement.increase_date_Exp((Achieve_collect.在线时间).ToString(), 1);
                 SumSave.crt_pass.day_state[0] += 1;
            }
            SendNotification(NotiList.Execute_Write, wirtes);
        }
        /// <summary>
        /// 每10分钟刷新一次排行榜
        /// </summary>
        private void Read_User_Ranks()
        {
            //每日任务 在线时长
            SendNotification(NotiList.Read_User_Ranks);
            Battle_Tool.validate_rank();
        }
        public void Delete(string dec)
        { 
          SendNotification(NotiList.Delete,SumSave.nowtime+" "+SumSave.crt_user.uid+" "+  dec);
        }

        /// <summary>
        /// 查询队列
        /// </summary>
        /// <param name="type">函数公式</param>
        /// <param name="tableName">调用列表</param>
        /// <param name="sql">写入值</param>
        /// /// <param name="sql_names">序列名</param>
        public void GetQueue(Mysql_Type type, Mysql_Table_Name tableName, string[] sql, string[] sql_names = null)
        {
            foreach (var item in wirtes)
            {
                if (item.type == type)
                {
                    if (item.tableName == tableName && type != Mysql_Type.InsertInto)
                    {
                        //执行合并
                        item.columnValues = sql;
                        item.exist = true;
                        //return item.columnValues;
                        return;
                    }
                }
            }
            //获取新列表
            Base_Wirte_VO vo = new Base_Wirte_VO();
            vo.type = type;
            vo.tableName = tableName;
            vo.columnNames = sql_names;
            vo.columnValues = sql;
            vo.exist = true;
            wirtes.Add(vo);
            //return sql;

            /// <summary>
        }
        /// <summary>
        /// 主账户
        /// </summary>
        private string[] Accout;
        /// <summary>
        /// 写入角色id
        /// </summary>
        /// <param name="vs"></param>
        public void Crate_Accout(string[] vs)
        {
            Accout= vs;
        }
        /// <summary>
        /// 写入tap账户
        /// </summary>
        public void Wirte_Tap()
        {
            SendNotification(NotiList.Read_Crate_Uid, Accout);

        }
        /// <summary>
        /// 写入苹果账户
        /// </summary>
        public void Wirte_Iphone()
        {
            SendNotification(NotiList.Read_Crate_IPhone_Uid, Accout);
        }
        /// <summary>
        /// 读取服务器列表
        /// </summary>
        internal void Crate_Par()
        {
            SendNotification(NotiList.Read_Obtain_Par);
        }

        /// <summary>
        /// 执行写入队列
        /// </summary>
        public void SetWrite(Mysql_Type type, Mysql_Table_Name tableName, string[] sql)
        {
            for (int i = 0; i < wirtes.Count; i++)
            {
                if (wirtes[i].type == type)
                {
                    if (wirtes[i].tableName == tableName)
                    {
                        wirtes[i].columnValues = sql;
                        wirtes[i].exist = true;
                        return;
                    }
                }
            }

        }
        /// <summary>
        /// 调用写入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="all_list"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public string Instance_Wirte<T>(List<T> all_list, int max = 999) where T : Base_VO
        {
            string value = "";

            foreach (T item in all_list)
            {
                if (max > 0)
                {
                    max--;
                    value += item.GetPropertyValue(item);

                    value += ';';
                }
                else return value;
            }
            return value;
        }

        /// <summary>
        /// 写入资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index"></param>
        /// <param name="all_list"></param>
        public void Wirte_ResourcesList<T>(Emun_Resources_List index, List<T> all_list) where T : Base_VO
        {
            string value= OnWirte(all_list);
            switch (index)
            {
                case Emun_Resources_List.skill_value:
                    SumSave.crt_resources.skill_value = value;
                    break;
                case Emun_Resources_List.bag_value:
                    SumSave.crt_resources.bag_value = value;
                    break;
                case Emun_Resources_List.equip_value:
                    SumSave.crt_resources.equip_value = value;
                    break;
                case Emun_Resources_List.material_value:
                    SumSave.crt_resources.material_value = value;
                    break;
            }
            GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_value, SumSave.crt_resources.Set_Uptade_String(), SumSave.crt_resources.Get_Update_Character());
        }
        /// <summary>
        /// 写入材料资源
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void Wirte_ResourcesList(Emun_Resources_List index,string value) 
        {
            switch (index)
            {
                case Emun_Resources_List.skill_value:
                    SumSave.crt_resources.skill_value = value;
                    break;
                case Emun_Resources_List.bag_value:
                    SumSave.crt_resources.bag_value = value;
                    break;
                case Emun_Resources_List.equip_value:
                    SumSave.crt_resources.equip_value = value;
                    break;
                case Emun_Resources_List.material_value:
                    SumSave.crt_resources.material_value = value;
                    break;
            }
            GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_value, SumSave.crt_resources.Set_Uptade_String(), SumSave.crt_resources.Get_Update_Character());
        }


        /// <summary>
        /// 写入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="all_list"></param>
        /// <returns></returns>
        private string OnWirte<T>(List<T> all_list) where T : Base_VO
        {
            string value = "";

            foreach (T item in all_list)
            {
                value += item.user_value;

                value += ';';
            }
            return value;

        }
    }

}
