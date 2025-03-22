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
            }
            else
            {
                SumSave.crt_user.uid = SumSave.uid; //Guid.NewGuid().ToString("N");
                SumSave.crt_user.Nowdate = DateTime.Now;
                SumSave.crt_user.RegisterDate = DateTime.Now;
                SumSave.crt_user.par = SumSave.par;
                Game_Omphalos.i.GetQueue(Mysql_Type.InsertInto, Mysql_Table_Name.mo_user_base, SumSave.crt_user.Set_Instace_String());
            }
            CloseMySqlDB();
        }
        public void Read_Instace()
        {
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