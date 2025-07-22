using Common;
using System;
using System.Collections.Generic;
using UnityEngine;

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
            if (MysqlDb.MysqlClose) return;//未联网
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
            Read_Db_Seed();
            Read_Db_Collect();
            Read_db_signin();
            Read_db_par();
            Read_Guide_TotalTask();
            Read_db_Accumulatedrewards();
            Read_Guide_Fate();
            Read_db_vip();
            Read_db_formula();
            Read_db_weather();
            Read_Db_Suit();
            Read_Db_Dec();
            CloseMySqlDB();
        }


        /// <summary>
        /// 获得天气
        /// </summary>
        public void Read_db_weather()
        {
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_weather);
            SumSave.db_weather_list = new List<db_weather>();
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_weather_list.Add(ReadDb.Read_weather(mysqlReader));
                }
            }
        }


        /// <summary>
        /// 读取造化炉合成列表
        /// </summary>
        public void Read_db_formula()
        {
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_formula);
            SumSave.db_formula_list = new List<db_formula_vo>();
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_formula_list.Add(ReadDb.Read_formula(mysqlReader));
                }
            }
        }



        /// <summary>
        /// 读取vip列表
        /// </summary>
        public void Read_db_vip()
        {
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_vip);
            SumSave.db_vip_list = new List<db_vip>();
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_vip_list.Add(ReadDb.Read_Vip(mysqlReader));
                }
            }
        }


        /// <summary>
        /// 累计奖励
        /// </summary>
        public void Read_db_Accumulatedrewards()
        {
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_accumulatedrewards);
            SumSave.db_Accumulatedrewards = new db_Accumulatedrewards_vo("","","");
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_Accumulatedrewards = ReadDb.Read_Accumulatedrewards_vo(mysqlReader);
                }
            }
        }

        /// <summary>
        /// 读取命运殿堂列表
        /// </summary>
        public void Read_Guide_Fate()
        {
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_fate);
            SumSave.db_fate_list = new List<db_fate_vo>();
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_fate_list.Add(ReadDb.Read_fate(mysqlReader));
                }
            }
        }
        /// <summary>
        /// 读取大世界列表
        /// </summary>
        public void Read_Guide_TotalTask()
        {
            SumSave.GreenhandGuide_TotalTasks = new Dictionary<int, GreenhandGuide_TotalTaskVO>();
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_basetask);
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    GreenhandGuide_TotalTaskVO item = new GreenhandGuide_TotalTaskVO();
                    item = ReadDb.Read(mysqlReader, item);
                    SumSave.GreenhandGuide_TotalTasks.Add(item.taskid, item);
                }
            }
        }
        /// <summary>
        /// 读取服务器
        /// </summary>
        public void Read_db_par()
        {
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_pars);

            SumSave.db_pars = new List<db_base_par>();

            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_pars.Add(ReadDb.Read_base_par(mysqlReader));

                }
            }
        }
        /// <summary>
        /// 读取签到信息
        /// </summary>
        private void Read_db_signin()
        {
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_signin);

            SumSave.db_Signins = new List<db_signin_vo>();

            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_Signins.Add(ReadDb.Read_signin(mysqlReader));
                }
            }
        }

        /// <summary>
        /// 获取收集信息
        /// </summary>
        private void Read_Db_Collect()
        {
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_collect);

            SumSave.db_collect_vo = new List<db_collect_vo>();

            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_collect_vo.Add(ReadDb.Read_collect(mysqlReader));
                }
            }
        }


        /// <summary>
        /// 商店数据库
        /// </summary>
        private void Read_Db_Store()
        {
            //mysqlReader = MysqlDb.ReadFullTable("db_achieve");
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_store);

            SumSave.db_stores_list = new List<db_store_vo>();

            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_stores_list.Add(ReadDb.Read(mysqlReader));
                }
            }

            for (int i = 0; i < SumSave.db_stores_list.Count; i++)
            {
                if (SumSave.db_stores_list[i].ItemMaxQuantity > 0)
                {
                    SumSave.db_stores_dic.Add(SumSave.db_stores_list[i].ItemName, SumSave.db_stores_list[i]);
                }
            }
        }




        /// <summary>
        /// 成就数据库
        /// </summary>
        private void Read_Db_Achievement()
        {
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_achieve);

            SumSave.db_Achievement_dic= new List<db_achievement_VO>();

            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_Achievement_dic.Add(ReadDb.Read_achievement_VO(mysqlReader));
                }
            }
        }


        /// <summary>
        /// 获取炼丹信息
        /// </summary>
        private void Read_Db_Seed()
        {
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_seed);

            SumSave.db_seeds = new List<db_seed_vo>();

            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_seeds.Add(ReadDb.Read_seed(mysqlReader));
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
            SumSave.base_setting = new List<int>();
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_lvs=(ReadDb.Read(mysqlReader, new db_lv_vo()));
                }
            }
        }
        /// <summary>
        /// 具体功能消息
        /// </summary>
        private void Read_Db_Dec()
        {
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_dec);

            SumSave.db_dec = new List<db_dec>();

            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_dec.Add(ReadDb.Read_dec(mysqlReader));
                }
            }
        }



        /// <summary>
        /// 套装
        /// </summary>
        private void Read_Db_Suit()
        {
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_suit);

            SumSave.db_suits = new List<db_suit_vo>();

            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_suits.Add (ReadDb.Read_suit(mysqlReader));
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

            SumSave.db_pet = new List<db_pet_vo>();

            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_pet.Add(ReadDb.Read_Pass(mysqlReader, new db_pet_vo()));
                }
            }

            List<db_pet_vo> db_pet = SumSave.db_pet;
            SumSave.db_pet_dic= new Dictionary<string, db_pet_vo>();
            for (int i = 0; i < db_pet.Count; i++)
            {
                SumSave.db_pet_dic.Add(db_pet[i].petName, db_pet[i]);
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
                    SumSave.db_Artifacts.Add(ReadDb.Read_artifact_vo(mysqlReader));
                }
            }

            //List<string> db_artifact = new List<string>();
            //for(int i = 0; i < SumSave.db_Artifacts.Count; i++)
            //{
            //    if(UI.UI_Manager.I.GetEquipSprite("icon/", SumSave.db_Artifacts[i].arrifact_name) == null)
            //    {
            //        db_artifact.Add(SumSave.db_Artifacts[i].arrifact_name);
            //    }
            //}

            //string str = "";
            //for(int i = 0; i < db_artifact.Count; i++)
            //{
            //    str+= db_artifact[i] + ",";
            //}
            //Debug.Log(str);

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
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_heros);
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
#if UNITY_EDITOR
            Battle_Tool.tool_item();
#elif UNITY_ANDROID

#elif UNITY_IPHONE
            
#endif
        }
        /// <summary>
        /// 读取技能数据库
        /// </summary>
        private void Read_Db_Magic()
        {
            mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_magic);
            //mysqlReader = MysqlDb.ReadFullTable(Mysql_Table_Name.db_magic_copy1);
            SumSave.db_skills = new List<base_skill_vo>();
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.db_skills.Add(ReadDb.Read(mysqlReader, new base_skill_vo()));
                }
            }

            for(int i = 0; i < SumSave.db_skills.Count; i++)
            {
                Sprite skill_spr = UI.UI_Manager.I.GetEquipSprite("icon/", SumSave.db_skills[i].skillname);// Resources.Load<Sprite>("icon/" + SumSave.db_skills[i].skillname);
                if (skill_spr == null)
                {
                    Debug.LogError("Resources/icon 中没有："+ SumSave.db_skills[i].skillname+"技能图标");
                }
                //Sprite skill_spr2 = UI.UI_Manager.I.GetEquipSprite("skill/", SumSave.db_skills[i].skillname );// Resources.Load<Sprite
                //if (skill_spr == null)
                //{
                //    Debug.LogError("Resources/skill 中没有：" + SumSave.db_skills[i].skillname + "技能图标");
                //}
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

            //List <crtMaxHeroVO> monsters = new List<crtMaxHeroVO>();
            //string str = "未找到怪物: ";
            //for (int i = 0; i < SumSave.db_monsters.Count; i++)
            //{
            //    if(Resources.Load<Sprite>("Prefabs/monsters/" + SumSave.db_monsters[i].show_name) ==null) 
            //    {
            //        //Debug.Log("没有"+ SumSave.db_monsters[i].show_name);
            //        monsters.Add(SumSave.db_monsters[i]);
            //    }
            //}    

            //for (int i = 0; i < monsters.Count; i++)
            //{
            //    str += monsters[i].show_name + " ";
            //}
            //Debug.Log(str);



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
#if UNITY_EDITOR
            Battle_Tool.tool_map();
#elif UNITY_ANDROID

#elif UNITY_IPHONE
            
#endif
        }
    }
}
