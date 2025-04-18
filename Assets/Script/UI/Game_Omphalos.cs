using Common;
using Components;
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
            InvokeRepeating("Read_User_Ranks", 600, 600);
        }
        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        /// <param name="dec"></param>
        public void Alert_Info(string dec)
        {
            Alert.Show("�쳣��ʾ", dec);
        }
        /// <summary>
        /// д����־
        /// </summary>
        /// <param name="log"></param>
        public void LogList(string log)
        {
            //д����־
            SendNotification(NotiList.loglist, log);
        }
        /// <summary>
        /// д������
        /// </summary>
        private void CountTime()
        {
            SendNotification(NotiList.Execute_Write, wirtes);
        }
        /// <summary>
        /// ÿ10����ˢ��һ�����а�
        /// </summary>
        private void Read_User_Ranks()
        {
            //ÿ������ ����ʱ��
            SumSave.crt_pass.day_state[0] += 10;
            SendNotification(NotiList.Read_User_Ranks);
            Battle_Tool.validate_rank();
        }
        public void Delete(string dec)
        { 
          SendNotification(NotiList.Delete,SumSave.nowtime+" "+SumSave.crt_user.uid+" "+  dec);
        }

        /// <summary>
        /// ��ѯ����
        /// </summary>
        /// <param name="type">������ʽ</param>
        /// <param name="tableName">�����б�</param>
        /// <param name="sql">д��ֵ</param>
        /// /// <param name="sql_names">������</param>
        public void GetQueue(Mysql_Type type, Mysql_Table_Name tableName, string[] sql, string[] sql_names = null)
        {
            foreach (var item in wirtes)
            {
                if (item.type == type)
                {
                    if (item.tableName == tableName && type != Mysql_Type.InsertInto)
                    {
                        //ִ�кϲ�
                        item.columnValues = sql;
                        item.exist = true;
                        //return item.columnValues;
                        return;
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
            //return sql;

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

        /// <summary>
        /// д����Դ
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
        /// д�������Դ
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
        /// д������
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
