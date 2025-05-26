using Common;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using TarenaMVC;

namespace MVC
{
    /// <summary>
    ///  封装访问msqlSQLite数据库的功能
    /// </summary>
    public class Base_Proxy : Proxy
    {
        /// <summary>
        /// NAME
        /// </summary>
        public new const string NAME = "Base_Proxy";
        /// <summary>
        /// 验证双开
        /// </summary>
        private int user_login = 0;
        public Base_Proxy()
        {
            this.ProxyName = NAME;
        }
        /// <summary>
        /// 
        /// </summary>
        protected MysqlDbAccess MysqlDb;

        protected MySqlDataReader mysqlReader;

        /// <summary>
        ///  打开网络数据库
        /// </summary>
        public void OpenMySqlDB()
        {
            MysqlDb = new MysqlDbAccess("server=rm-bp1ilq26us071qrl8eo.mysql.rds.aliyuncs.com;port=3306;database=onlinestore;user=mysql_db_shadow;password=tcm520WLF;");
            User_Login();
        }
        /// <summary>
        /// 验证数据
        /// </summary>
        private void User_Login()
        {
            if (MysqlDb.MysqlClose) return;//未联网
            if (SumSave.crt_user != null)
            {
                mysqlReader = MysqlDb.Select(Mysql_Table_Name.user_login, "uid", GetStr(SumSave.uid));
                if (mysqlReader.HasRows)
                {
                    if (user_login == 0)
                    {
                        user_login = tool_Categoryt.Obtain_Random();
                        MysqlDb.UpdateInto(Mysql_Table_Name.user_login, new string[] { "login" }, new string[] { GetStr(user_login) }, "uid", GetStr(SumSave.uid));
                    }
                    else
                    {
                        int login = 0;
                        while (mysqlReader.Read())
                        {
                            login = mysqlReader.GetInt32(mysqlReader.GetOrdinal("login"));
                        }
                        if (login != user_login)
                        {
                            Game_Omphalos.i.Alert_Info("多开游戏已关闭");
                            CloseMySqlDB();
                        }
                    }
                }
                else
                {
                    user_login = tool_Categoryt.Obtain_Random();
                    MysqlDb.InsertInto(Mysql_Table_Name.user_login, new string[] { GetStr(0), GetStr(SumSave.uid), GetStr(user_login)});
                }
            }
        }

        /// <summary>
        ///  关闭网络数据库
        /// </summary>
        public void CloseMySqlDB()
        {
            if (MysqlDb != null)
            {
                MysqlDb.CloseSqlConnection();

                MysqlDb = null;

                mysqlReader = null;
            }

        }
        /// <summary>
        ///  前后添加单引号
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        protected string GetStr(object o)
        {
            return "'" + o + "'";

        }
        /// <summary>
        /// 执行写入指令每5s执行一条写入 
        /// </summary>
        public void ExecuteWrite(List<Base_Wirte_VO> wirtes)
        {
            if (MysqlDb.MysqlClose) return;
            QueryTime();
            if (wirtes.Count > 0)
            {
                for (int i = 0; i < wirtes.Count; i++)
                {
                    if (wirtes[i].exist)
                    {
                        switch (wirtes[i].type)
                        {
                            case Mysql_Type.InsertInto:
                                MysqlDb.InsertInto(wirtes[i].tableName, wirtes[i].columnValues);
                                break;
                                case Mysql_Type.UpdateInto:
                                if (wirtes[i].tableName == Mysql_Table_Name.user_rank)
                                {
                                    MysqlDb.UpdateInto(wirtes[i].tableName, wirtes[i].columnNames, wirtes[i].columnValues, "par", GetStr(SumSave.par));
                                }
                                else 
                                MysqlDb.UpdateInto(wirtes[i].tableName, wirtes[i].columnNames, wirtes[i].columnValues, "uid", GetStr(SumSave.crt_user.uid));
                                break;
                            case Mysql_Type.Delete:
                                MysqlDb.Delete(wirtes[i].tableName, new string[] { "uid" }, new string[] { GetStr(SumSave.crt_user.uid)});
                                //SumSave.
                                break;

                            default: break;
                        }
                        wirtes[i].exist = false;
                        Base_Wirte_VO wirte = wirtes[i];
                        wirtes.RemoveAt(i);
                        wirtes.Add(wirte);
                        return;
                    }
                }
               
            }
        }

        public void UpdateIntoWodldBoss()
        {
            MysqlDb.UpdateInto( Mysql_Table_Name.user_world_boos,SumSave.crt_world_boos.Get_Update_Character(), SumSave.crt_world_boos.Set_Uptade_String(), "par", GetStr(SumSave.par));
        }


        /// <summary>
        /// 读取时间
        /// </summary>
        protected void QueryTime()
        {
            mysqlReader = MysqlDb.QueryTime();
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    for (int i = 0; i < mysqlReader.FieldCount; i++)
                    {
                        SumSave.nowtime = Convert.ToDateTime(mysqlReader[i].ToString());
                    }
                }
            }
        }
    }
}
