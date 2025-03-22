using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MVC
{
    /// <summary>
    /// ���ƶ�ʱ��
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
        /// ������ʱ�� 
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
        /// ��ѯ����
        /// </summary>
        /// <param name="type">������ʽ</param>
        /// <param name="tableName">�����б�</param>
        /// <param name="sql">д��ֵ</param>
        /// /// <param name="sql_names">������</param>
        public string[] GetQueue(Mysql_Type type, Mysql_Table_Name tableName, string[] sql, string[] sql_names = null)
        {
            foreach (var item in wirtes)
            {
                if (item.type == type)
                {
                    if (item.tableName == tableName)
                    {
                        //ִ�кϲ�
                        return item.columnValues;
                    }
                }
            }
            //��ȡ���б�
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
        /// ִ��д�����
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
        /// ����д��
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
