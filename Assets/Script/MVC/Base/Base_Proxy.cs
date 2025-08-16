using Common;
using Components;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using TarenaMVC;
using UI;
using UnityEngine;

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
        public void OpenMySqlDB1()
        {
            MysqlDb = new MysqlDbAccess();
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
            //if (MysqlDb.MysqlClose) return;
            //QueryTime();
            //QueryVersion();
            Program.MysqlMain(wirtes);
            return;
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
                                if (wirtes[i].tableName == Mysql_Table_Name.user_rank|| wirtes[i].tableName== Mysql_Table_Name.user_world_boss_rank)
                                {
                                    MysqlDb.UpdateInto(wirtes[i].tableName, wirtes[i].columnNames, wirtes[i].columnValues, "par", GetStr(SumSave.par));
                                }
                                else 
                                MysqlDb.UpdateInto(wirtes[i].tableName, wirtes[i].columnNames, wirtes[i].columnValues, "uid", GetStr(SumSave.crt_user.uid));
                                break;
                            case Mysql_Type.Delete:
                                MysqlDb.Delete(wirtes[i].tableName, new string[] { "uid" }, new string[] { GetStr(SumSave.crt_user.uid)});
                                break;

                            default: break;
                        }
                        wirtes[i].exist = false;
                        //Base_Wirte_VO wirte = wirtes[i];
                        //wirtes.RemoveAt(i);
                        //wirtes.Add(wirte);
                    }
                }
            }
        }
        private string[] versions = new string[] {"0.2025.03", "0.2025.04" };
        /// <summary>
        /// 检测次数
        /// </summary>
        int versionsnumber = 0;
        /// <summary>
        /// 读取版本
        /// </summary>
        private void QueryVersion()
        {

            SumSave.crt_versions = new user_versions();
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.versions);
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.crt_versions.Version= mysqlReader.GetString(mysqlReader.GetOrdinal("Version"));
                    SumSave.crt_versions.Weather = mysqlReader.GetString(mysqlReader.GetOrdinal("Weather"));
                    SumSave.crt_versions.WeatherTime = mysqlReader.GetString(mysqlReader.GetOrdinal("WeatherTime"));
                    SumSave.crt_versions.Activity = mysqlReader.GetInt32(mysqlReader.GetOrdinal("Activity"));
                    SumSave.crt_versions.tapversionversion = mysqlReader.GetString(mysqlReader.GetOrdinal("tapversionversion"));
                }
            }
            SumSave.OpenGame = false;
            foreach (string item in versions)
            {
                if (SumSave.crt_versions.Version == item)
                    SumSave.OpenGame = true;

            }

            if (!SumSave.OpenGame)
            {
                if (versionsnumber >= 3)
                {
                    Close_Hide2();
                }
                versionsnumber++;
                Alert_Dec.Show("游戏版本不匹配，请更新游戏！");
            }
        }

        private void Close_Hide2()
        {
            UI_Manager.Instance.GetPanel<panel_login>().Show();
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
        protected void QueryMysqlTime()
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
