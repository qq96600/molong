using Common;
using Components;
using System;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MVC
{
    /// <summary>
    ///  处理用户相关数据:登录,注册和注销
    /// </summary>
    public class MySql_Proxy : Base_Proxy
    {
        /// <summary>
        /// NAME
        /// </summary>
        public new const string NAME = "MySql_Proxy";

        public MySql_Proxy()
        {
            this.ProxyName = NAME;
        }


        public void Read_Instace()
        {
            OpenMySqlDB();
            Read_Db_Monster();
            Read_Db_Map();
            Read_Db_Magic();
            Read_Db_stditems();
            CloseMySqlDB();

        }

        /// <summary>
        /// 读取物品数据库
        /// </summary>
        private void Read_Db_stditems()
        {
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_stditems);
            SumSave.db_stditems = new List<Bag_Base_VO>();
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_stditems.Add(ReadDb.Read(mysqlReader, new Bag_Base_VO()));
                }
            }
        }
        /// <summary>
        /// 读取技能数据库
        /// </summary>
        private void Read_Db_Magic()
        {
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_magic);
            SumSave.db_skills = new List<base_skill_vo>();
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_skills.Add(ReadDb.Read(mysqlReader, new base_skill_vo()));
                }
            }
        }
        /// <summary>
        /// 读取怪物数据库
        /// </summary>
        private void Read_Db_Monster()
        {
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_monster);
            SumSave.db_monsters = new List<crtMaxHeroVO>();
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_monsters.Add(ReadDb.Read(mysqlReader, new crtMaxHeroVO()));
                }
            }
        }

        private void Read_Db_Map()
        {
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_map);
            SumSave.db_maps = new List<user_map_vo>();
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_maps.Add(ReadDb.Read(mysqlReader, new user_map_vo()));
                }
            }
        }
    }
}
