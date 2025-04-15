using Common;
using System;
using System.Collections.Generic;

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
            QueryTime();
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
            Read_Db_Pet_explore();
            Read_Db_Lv();
            Read_Db_Hall();
            Read_Db_Achievement();
            Read_Db_Store();
            CloseMySqlDB();

        }



        private void Read_Db_Store()
        {
            //mysqlReader = MysqlDb.ReadFullTable("db_achieve");
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_store);

            SumSave.db_stores_dic = new List<db_store_vo>();

            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_stores_dic.Add(ReadDb.Read(mysqlReader, new db_store_vo()));
                }
            }
        }




        /// <summary>
        /// 成就数据库
        /// </summary>
        private void Read_Db_Achievement()
        {
            //mysqlReader = MysqlDb.ReadFullTable("db_achieve");
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_achieve);

            SumSave.db_Achievement_dic= new List<db_achievement_VO>();

            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_Achievement_dic.Add(ReadDb.Read(mysqlReader, new db_achievement_VO()));
                }
            }
        }


        /// <summary>
        /// 大厅按钮
        /// </summary>
        private void Read_Db_Hall()
        {
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_hall);

            SumSave.db_halls = new db_hall_vo();

            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_halls = (ReadDb.Read(mysqlReader, new db_hall_vo()));
                }
            }
        }

        /// <summary>
        /// 升级经验
        /// </summary>
        private void Read_Db_Lv()
        {
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_lv);

            SumSave.db_lvs = new db_lv_vo();

            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_lvs=(ReadDb.Read(mysqlReader, new db_lv_vo()));
                }
            }
        }

        /// <summary>
        /// 读取宠物探索地图
        /// </summary>
        private void Read_Db_Pet_explore()
        {
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_pet_explore);

            SumSave.db_pet_explore = new List<user_pet_explore_vo>();

            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_pet_explore.Add(ReadDb.Read_Pass(mysqlReader, new user_pet_explore_vo()));
                }
            }

            List<user_pet_explore_vo> db_pet_explore = SumSave.db_pet_explore;
            SumSave.db_pet_explore_dic = new Dictionary<string, user_pet_explore_vo>();
            for (int i = 0; i < db_pet_explore.Count; i++)
            {
                SumSave.db_pet_explore_dic.Add(db_pet_explore[i].petExploreMapName, db_pet_explore[i]);
            }

        }


        /// <summary>
        /// 读取宠物表
        /// </summary>
        private void Read_Db_Pet()
        {
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_pet);

            SumSave.db_pet = new List<user_pet_vo>();

            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_pet.Add(ReadDb.Read_Pass(mysqlReader, new user_pet_vo()));
                }
            }

            List<user_pet_vo> db_pet = SumSave.db_pet;
            SumSave.db_pet_dic= new Dictionary<string, user_pet_vo>();
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

            List<user_plant_vo> db_plants = SumSave.db_plants;
            SumSave.db_plants_dic = new Dictionary<string, user_plant_vo>();
            for (int i = 0; i < db_plants.Count; i++)
            {
                SumSave.db_plants_dic.Add(db_plants[i].plantName, db_plants[i]);
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
