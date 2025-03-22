using Common;
using MySql.Data.MySqlClient;
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
        public Base_Proxy()
        {
            this.ProxyName = NAME;
        }
        /// <summary>
        /// 
        /// </summary>
        protected MysqlDbAccess MysqlDb;

        protected MySqlDataReader mysqlReader;

        bool OpenSelect = true;
        /// <summary>
        ///  打开网络数据库
        /// </summary>
        public void OpenMySqlDB()
        {
            MysqlDb = new MysqlDbAccess("server=rm-bp1ilq26us071qrl8eo.mysql.rds.aliyuncs.com;port=3306;database=onlinestore;user=mysql_db_shadow;password=tcm520WLF;");
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
                                MysqlDb.UpdateInto(wirtes[i].tableName, wirtes[i].columnNames, wirtes[i].columnValues, "uid", GetStr(SumSave.crt_user.uid));
                                break;
                            case Mysql_Type.Delete:
                                MysqlDb.Delete(wirtes[i].tableName, new string[] { "uid" }, new string[] { GetStr(SumSave.crt_user.uid)});
                                //SumSave.
                                break;

                            default: break;
                        }
                        wirtes[i].exist = false;
                        return;
                    }
                }
               
            }
        }

    }
}
