using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MVC
{
    /// <summary>
    /// 控制定时器
    /// </summary>
    public class Game_Omphalos : Base_Mono
    {
        public static Game_Omphalos i;
        private void Awake()
        {
            i= this;
            AppFacade.I.Startup();
        }
        private List<Base_Wirte_VO> wirtes = new List<Base_Wirte_VO>();
        /// <summary>
        /// 开启定时器 
        /// </summary>
        public void activation()
        {
            InvokeRepeating("CountTime", 1, 1);
        }
        private void CountTime()
        {
            SendNotification(NotiList.Execute_Write, wirtes);
        }

        /// <summary>
        /// 查询队列
        /// </summary>
        /// <param name="type">函数公式</param>
        /// <param name="tableName">调用列表</param>
        /// <param name="sql">写入值</param>
        /// /// <param name="sql_names">序列名</param>
        public string[] GetQueue(Mysql_Type type, Mysql_Table_Name tableName, string[] sql, string[] sql_names = null)
        {
            foreach (var item in wirtes)
            {
                if (item.type == type)
                {
                    if (item.tableName == tableName)
                    {
                        //执行合并
                        return item.columnValues;
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
            return sql;

            /// <summary>
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
    }

}
