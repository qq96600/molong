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
            Read_Db_Magic();
            Read_Db_stditems();
            Read_Db_Hero();
            Read_Db_Setting_Aoption();
            Read_Db_artifact();
            Read_Db_Pass();
            Read_Db_Panlt();
            Read_Db_Map();
            Read_Db_Pet();
            CloseMySqlDB();

        }
        /// <summary>
        /// 孵化表
        /// </summary>
        private void Read_Db_Pet()
        {
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_pet);

            SumSave.db_pet = new List<user_pet_hatching_vo>();

            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_pet.Add(ReadDb.Read_Pass(mysqlReader, new user_pet_hatching_vo()));
                }
            }

            List<user_pet_hatching_vo> db_pet = SumSave.db_pet;
            SumSave.db_pet_dic= new Dictionary<string, user_pet_hatching_vo>();
            for (int i = 0; i < db_pet.Count; i++)
            {
                SumSave.db_pet_dic.Add(db_pet[i].petEggsName, db_pet[i]);
            }
            

        }

        /// <summary>
        /// 读取植物数据库
        /// </summary>
        private void Read_Db_Panlt()
        {
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_plant);

            SumSave.db_plants = new List<user_plant_vo>();

            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_plants.Add(ReadDb.Read_Pass(mysqlReader, new user_plant_vo()));
                }
            }
        }
        /// <summary>
        /// 读取通行证数据库
        /// </summary>
        private void Read_Db_Pass()
        {
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_pass);

            SumSave.db_pass = new List<user_pass_vo>();

            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_pass.Add(ReadDb.Read_Pass(mysqlReader, new user_pass_vo()));
                }
            }
        }

        /// <summary>
        /// 读取神器列表
        /// </summary>
        private void Read_Db_artifact()
        {
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_artifact);

            SumSave.db_Artifacts = new List<db_artifact_vo>();

            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_Artifacts.Add(ReadDb.Read(mysqlReader, new db_artifact_vo()));
                }
            }
        }

        /// <summary>
        /// 读取设置选项
        /// </summary>
        private void Read_Db_Setting_Aoption()
        {
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_setting);

            SumSave.db_sttings = new List<user_setting_type_vo>();
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_sttings.Add(ReadDb.Read(mysqlReader, new user_setting_type_vo()));
                }
            }
        }
        /// <summary>
        /// 读取英雄数据库
        /// </summary>
        private void Read_Db_Hero()
        {
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_hero);
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
        /// <summary>
        /// 读取地图数据库
        /// </summary>
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

            Battle_Tool.tool_map();
        }
    }
}
