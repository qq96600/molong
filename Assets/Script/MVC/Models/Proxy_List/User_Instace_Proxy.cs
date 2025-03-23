using Common;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace MVC
{
    public class User_Instace_Proxy : Base_Proxy
    {
        /// <summary>
        ///  NAME
        /// </summary>
        public new const string NAME = "User_Instace_Proxy";
        /// <summary>
        ///  构造函数
        /// </summary>
        public User_Instace_Proxy()
        {
            this.ProxyName = NAME;
        }
        /// <summary>
        /// 登录
        /// </summary>
        public void User_Login()
        {
            OpenMySqlDB();
            mysqlReader = MysqlDb.Select(Mysql_Table_Name.mo_user_base, "uid", GetStr(SumSave.uid));
            SumSave.crt_user = new user_base_vo();
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                { 
                    SumSave.crt_user = ReadDb.Read(mysqlReader, SumSave.crt_user);
                }
                SumSave.crt_user.Nowdate = DateTime.Now;
                Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_base, SumSave.crt_user.Set_Uptade_String(), SumSave.crt_user.Get_Update_Character());
                Read_Instace();
            }
            else
            {
                SumSave.crt_user.uid = SumSave.uid; //Guid.NewGuid().ToString("N");
                SumSave.crt_user.Nowdate = DateTime.Now;
                SumSave.crt_user.RegisterDate = DateTime.Now;
                SumSave.crt_user.par = SumSave.par;
                Game_Omphalos.i.GetQueue(Mysql_Type.InsertInto, Mysql_Table_Name.mo_user_base, SumSave.crt_user.Set_Instace_String());
                Init();
            }
            CloseMySqlDB();
        }
        //初始化文件
        private void Init()
        { 
            
        

        }
        /// <summary>
        /// 读取自身数据
        /// </summary>
        private void Read_Instace()
        {
            Read_User_Hero();
        }
        /// <summary>
        /// 读取英雄数据
        /// </summary>
        private void Read_User_Hero()
        {
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.mo_user_hero);
            SumSave.db_heros = new List<db_hero_vo>();
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_heros.Add(ReadDb.Read(mysqlReader, new db_hero_vo()));
                }
            }
        }
        /// <summary>
        /// 导入数据
        /// </summary>
        public void Execute_Write(List<Base_Wirte_VO> list)
        {
            OpenMySqlDB();
            //写入数据
            ExecuteWrite(list);
            CloseMySqlDB();
        }
        public void Read_Locality()
        {

        }
    }
}