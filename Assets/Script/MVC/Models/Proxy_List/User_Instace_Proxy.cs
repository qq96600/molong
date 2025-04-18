using Common;
using System;
using System.Collections.Generic;
using UI;
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
            Read_Instace();
            CloseMySqlDB();
        }
        //初始化文件
        private void Init()
        {
            SumSave.crt_bag = new List<Bag_Base_VO>();
            SumSave.crt_euqip = new List<Bag_Base_VO>();
            SumSave.crt_skills = new List<base_skill_vo>();
            SumSave.crt_bag_resources = new bag_Resources_vo();
            if (SumSave.crt_resources.bag_value.Length > 0)
            { 
                string[] Splits= SumSave.crt_resources.bag_value.Split(';');
                for (int i = 0; i < Splits.Length; i++)
                {
                    if (Splits[i].Length > 0)
                    {
                        Bag_Base_VO bag = new Bag_Base_VO();
                        bag.user_value= Splits[i];
                        SumSave.crt_bag.Add(tool_Categoryt.Read_Bag(bag));
                    }
                }
                
            }
            if (SumSave.crt_resources.equip_value.Length > 0)
            {
                string[] Splits = SumSave.crt_resources.equip_value.Split(';');
                for (int i = 0; i < Splits.Length; i++)
                {
                    if (Splits[i].Length > 0)
                    {
                        Bag_Base_VO bag = new Bag_Base_VO();
                        bag.user_value = Splits[i];
                        SumSave.crt_euqip.Add(tool_Categoryt.Read_Bag(bag));
                    }
                }
            }
            if (SumSave.crt_resources.skill_value.Length > 0)
            {
                string[] Splits = SumSave.crt_resources.skill_value.Split(';');
                for (int i = 0; i < Splits.Length; i++)
                {
                    if (Splits[i].Length > 0)
                    {
                        base_skill_vo skill = new base_skill_vo();
                        skill.user_value = Splits[i];
                        SumSave.crt_skills.Add(tool_Categoryt.Read_skill(skill));
                    }
                }
            }
            SumSave.crt_bag_resources.Init(SumSave.crt_resources.material_value);
        }
        public void Delete(string dec)
        {
            OpenMySqlDB();
            if (MysqlDb.MysqlClose)
            {
                MysqlDb.UpdateInto(Mysql_Table_Name.mo_user_base, new string[] { "Nowdate" }, new string[] { GetStr(dec) }, "uid", GetStr(SumSave.crt_user.uid));//login
                MysqlDb.UpdateInto(Mysql_Table_Name.user_login, new string[] { "login" }, new string[] { GetStr(-1) }, "uid", GetStr(SumSave.crt_user.uid));
            }
            CloseMySqlDB();
        }
        private List<string> log_list = new List<string>();
        public void loglist(string dec)
        {
            log_list.Add(dec);
            OpenMySqlDB();  
            if (MysqlDb.MysqlClose)
            {
                for (int i = 0; i < log_list.Count; i++)
                {
                    MysqlDb.InsertInto(Mysql_Table_Name.loglist, new string[] { GetStr(0), GetStr(log_list[i]) });
                }
                log_list.Clear();
            }
            CloseMySqlDB();
        }

        /// <summary>
        /// 读取自身数据
        /// </summary>
        private void Read_Instace()
        {
            read_User_Rank();
            Read_User_Unit();
            Read_User_Hero();
            Read_User_Resources();
            Read_User_Setting();
            Read_User_Artifact();
            Read_User_Pass();
            Read_User_Plant();
            Read_User_Hatching();
            Read_World();
            Read_User_Achievenment();
            Read_Needlist();
            Read_Seed();
            Read_Signin();
            //Read_collect();
            refresh_Max_Hero_Attribute();
        }
        /// <summary>
        /// 读取签到
        /// </summary>
        private void Read_Signin()
        {
            mysqlReader = MysqlDb.Select(Mysql_Table_Name.mo_user_signin, "uid", GetStr(SumSave.crt_user.uid));
            SumSave.crt_signin = new user_signin_vo();
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.crt_signin = ReadDb.Read(mysqlReader, new user_signin_vo());
                }
            }
            else//为空的话初始化数据
            {
                SumSave.crt_signin.now_time = Convert.ToDateTime(SumSave.nowtime.AddDays(-1).ToString("yyyy-MM-dd"));
                SumSave.crt_signin.number = 0;
                SumSave.crt_signin.user_value = "";
                SumSave.crt_signin.Init();
                Game_Omphalos.i.GetQueue(Mysql_Type.InsertInto, Mysql_Table_Name.mo_user_signin, SumSave.crt_signin.Set_Instace_String());
            }
        }
        /// <summary>
        /// 读取收集列表
        /// </summary>
        private void Read_collect()
        {
            mysqlReader = MysqlDb.Select(Mysql_Table_Name.mo_user_collect, "uid", GetStr(SumSave.crt_user.uid));
            SumSave.crt_needlist = new user_needlist_vo();
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.crt_needlist = ReadDb.Read(mysqlReader, new user_needlist_vo());
                }
            }
            else//为空的话初始化数据
            {
                SumSave.crt_needlist.store_value = "";
                SumSave.crt_needlist.map_value = "";
                SumSave.crt_needlist.user_value = "";
                List<db_store_vo> store_vo = new List<db_store_vo>();

                for (int i = 0; i < SumSave.db_stores_list.Count; i++)
                {
                    if (SumSave.db_stores_list[i].ItemMaxQuantity > 0)
                    {
                        store_vo.Add(SumSave.db_stores_list[i]);
                    }
                }
                for (int i = 0; i < store_vo.Count; i++)
                {
                    if (i > 0)
                    {
                        SumSave.crt_needlist.store_value += ",";
                    }
                    SumSave.crt_needlist.store_value += store_vo[i].ItemName + " " + "0";
                }
                Game_Omphalos.i.GetQueue(Mysql_Type.InsertInto, Mysql_Table_Name.mo_user_needlist, SumSave.crt_needlist.Set_Instace_String());
            }
        }

        /// <summary>
        /// 读取需求列表
        /// </summary>
        private void Read_Needlist()
        {
            mysqlReader = MysqlDb.Select(Mysql_Table_Name.mo_user_needlist, "uid", GetStr(SumSave.crt_user.uid));
            SumSave.crt_needlist= new user_needlist_vo();
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.crt_needlist = ReadDb.Read(mysqlReader, new user_needlist_vo());
                }
            }
            else//为空的话初始化数据
            {
                SumSave.crt_needlist.store_value = "";
                SumSave.crt_needlist.map_value = "";
                SumSave.crt_needlist.user_value = "";
                List<db_store_vo> store_vo = new List<db_store_vo>();
               
                for (int i = 0; i < SumSave.db_stores_list.Count; i++)
                {
                    if (SumSave.db_stores_list[i].ItemMaxQuantity > 0)
                    {
                        store_vo.Add(SumSave.db_stores_list[i]);
                    }
                }
                for (int i = 0; i < store_vo.Count; i++)
                {
                    if(i>0)
                    {
                        SumSave.crt_needlist.store_value += ",";
                    }
                    SumSave.crt_needlist.store_value += store_vo[i].ItemName + " " + "0" ;
                }
                Game_Omphalos.i.GetQueue(Mysql_Type.InsertInto, Mysql_Table_Name.mo_user_needlist, SumSave.crt_needlist.Set_Instace_String());
            }
        }



        /// <summary>
        /// 读取炼丹数据
        /// </summary>
        private void Read_Seed()
        {
            mysqlReader = MysqlDb.Select(Mysql_Table_Name.mo_user_seed, "uid", GetStr(SumSave.crt_user.uid));
            SumSave.crt_seeds = new bag_seed_vo();
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.crt_seeds = ReadDb.Read(mysqlReader, new bag_seed_vo());
                }
            }
            else
            {
                SumSave.crt_seeds.user_value = "";
                SumSave.crt_seeds.Init();
                Game_Omphalos.i.GetQueue(Mysql_Type.InsertInto, Mysql_Table_Name.mo_user_seed, SumSave.crt_seeds.Set_Instace_String());
            }
        }

        /// <summary>
        /// 读取排行榜
        /// </summary>
        private void read_User_Rank()
        {
            mysqlReader = MysqlDb.Select(Mysql_Table_Name.user_rank, "par", GetStr(SumSave.par));
            SumSave.user_ranks = new rank_vo();
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    //获取等级
                    SumSave.user_ranks.Ranking_value = mysqlReader.GetString(mysqlReader.GetOrdinal("value"));
                }
                string[] splits = SumSave.user_ranks.Ranking_value.Split(';');
                //读取排行榜
                foreach (string base_value in splits)
                {
                    if (base_value != "")
                    {
                        base_rank_vo bag_Base_VO = new base_rank_vo();
                        bag_Base_VO.SetPropertyValue(base_value);
                        SumSave.user_ranks.lists.Add(bag_Base_VO);
                    }
                }
            }

        }

        /// <summary>
        /// 读取排行榜
        /// </summary>
        public void Read_User_Rank()
        {
            OpenMySqlDB();
            if (MysqlDb.MysqlClose)
            {
                read_User_Rank();
            }
            CloseMySqlDB();
        }


        private void Read_User_Achievenment()
        {
            mysqlReader = MysqlDb.Select(Mysql_Table_Name.mo_user_achieve, "uid", GetStr(SumSave.crt_user.uid));
            SumSave.crt_achievement = new user_achievement_vo();
            
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.crt_achievement = ReadDb.Read(mysqlReader, new user_achievement_vo());
                }
                Dictionary<string, int> achieves = SumSave.crt_achievement.Set_Exp();
                if (achieves.Count == SumSave.db_Achievement_dic.Count)
                {

                }
                else
                {
                    for (int i = 0; i < SumSave.db_Achievement_dic.Count; i++)
                    {
                        bool exist = true;
                        foreach (string item in achieves.Keys)
                        {
                            if (item == SumSave.db_Achievement_dic[i].achievement_value)
                            {
                                exist = false;
                            }
                        }
                        if (exist)
                        {
                            SumSave.crt_achievement.achievement_exp += "|" + SumSave.db_Achievement_dic[i].achievement_value + " " + 0;
                            SumSave.crt_achievement.achievement_lvs += "|" + SumSave.db_Achievement_dic[i].achievement_value + " " + 0;
                        }
                    }
                    SumSave.crt_achievement.Init();
                }
            }
            else
            {
                for (int i = 0; i < SumSave.db_Achievement_dic.Count; i++)
                {
                    SumSave.crt_achievement.achievement_exp += SumSave.db_Achievement_dic[i].achievement_value + " " + 0 + (i == SumSave.db_Achievement_dic.Count - 1 ? "" : "|");
                }
                SumSave.crt_achievement.achievement_lvs = SumSave.crt_achievement.achievement_exp;
                MysqlDb.InsertInto(Mysql_Table_Name.mo_user_achieve, SumSave.crt_achievement.Set_Instace_String());
                SumSave.crt_achievement.Init();
            }
        }



        /// <summary>
        /// 宠物孵化数据
        /// </summary>
        private void Read_User_Hatching()
        {
            mysqlReader = MysqlDb.Select(Mysql_Table_Name.mo_user_pet_hatching, "uid", GetStr(SumSave.crt_user.uid));
            SumSave.crt_hatching = new user_pet_vo();
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.crt_hatching = ReadDb.Read(mysqlReader, new user_pet_vo());
                }
            }
            else
            {
                SumSave.crt_hatching.user_value = "";
                SumSave.crt_hatching.Init();
                Game_Omphalos.i.GetQueue(Mysql_Type.InsertInto, Mysql_Table_Name.mo_user_pet_hatching, SumSave.crt_hatching.Set_Instace_String());
            }
        }



        /// <summary>
        /// 种植数据
        /// </summary>
        private void Read_User_Plant()
        {
            mysqlReader = MysqlDb.Select(Mysql_Table_Name.mo_user_plant, "uid", GetStr(SumSave.crt_user.uid));
            SumSave.crt_plant = new user_plant_vo();
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.crt_plant = ReadDb.Read(mysqlReader, new user_plant_vo());
                }
            }
            else
            {
                SumSave.crt_plant.user_value = "";
                SumSave.crt_plant.Init();
                Game_Omphalos.i.GetQueue(Mysql_Type.InsertInto, Mysql_Table_Name.mo_user_plant, SumSave.crt_plant.Set_Instace_String());
            }
        }


        /// <summary>
        /// 通行证
        /// </summary>
        private void Read_User_Pass()
        {
            mysqlReader = MysqlDb.Select(Mysql_Table_Name.mo_user_pass, "uid", GetStr(SumSave.crt_user.uid));
            SumSave.crt_pass = new user_pass_vo();
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.crt_pass = ReadDb.Read(mysqlReader, new user_pass_vo());
                }
            }
            else
            {
                SumSave.crt_pass.user_value = "";
                SumSave.crt_pass.day_state_value = "0|0|0|0|0|0";
                SumSave.crt_pass.Init();
                Game_Omphalos.i.GetQueue(Mysql_Type.InsertInto, Mysql_Table_Name.mo_user_pass, SumSave.crt_pass.Set_Instace_String());
            }
        }

        /// <summary>
        /// 获取基础货币
        /// </summary>
        private void Read_User_Unit()
        {
            mysqlReader = MysqlDb.Select(Mysql_Table_Name.mo_user, "uid", GetStr(SumSave.crt_user.uid));
            SumSave.crt_user_unit = new user_vo();
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.crt_user_unit = ReadDb.Read(mysqlReader, new user_vo());
                }
            }
            else
            {
                SumSave.crt_user_unit.Init("10000,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0");
                Game_Omphalos.i.GetQueue(Mysql_Type.InsertInto, Mysql_Table_Name.mo_user, SumSave.crt_user_unit.Set_Instace_String());
            }
        }
        /// <summary>
        /// 读取神器数据
        /// </summary>
        private void Read_User_Artifact()
        {
            mysqlReader = MysqlDb.Select(Mysql_Table_Name.mo_user_artifact, "uid", GetStr(SumSave.crt_user.uid));
            SumSave.crt_artifact = new user_artifact_vo();
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.crt_artifact = ReadDb.Read(mysqlReader, new user_artifact_vo());
                }
            }
            else
            {
                SumSave.crt_artifact.artifact_value = "";
                SumSave.crt_artifact.Init();
                Game_Omphalos.i.GetQueue(Mysql_Type.InsertInto, Mysql_Table_Name.mo_user_artifact, SumSave.crt_artifact.Set_Instace_String());
            }
        }
        /// <summary>
        /// 读取设置文件
        /// </summary>
        private void Read_User_Setting()
        {
            mysqlReader = MysqlDb.Select(Mysql_Table_Name.mo_user_setting, "uid", GetStr(SumSave.crt_user.uid));
            SumSave.crt_setting = new user_base_setting_vo();
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.crt_setting = ReadDb.Read(mysqlReader, new user_base_setting_vo());
                }
            }
            else
            {
                SumSave.crt_setting.user_value = "0 0 0 0 0 0 0 0 0";
                Game_Omphalos.i.GetQueue(Mysql_Type.InsertInto, Mysql_Table_Name.mo_user_setting, SumSave.crt_setting.Set_Instace_String());
            }
        }
        /// <summary>
        /// 读取世界数据
        /// </summary>
        public void Read_World()
        {
            mysqlReader = MysqlDb.Select(Mysql_Table_Name.mo_user_world, "uid", GetStr(SumSave.crt_user.uid));
            if (mysqlReader.HasRows)
            {
                SumSave.crt_world = new user_world_vo();
                while (mysqlReader.Read())
                {
                    SumSave.crt_world = ReadDb.Read(mysqlReader, new user_world_vo());
                }
            }

        }
        /// <summary>
        /// 写入自身数据
        /// </summary>
        /// <param name="data"></param>
        public void Refresh_User_Setting(user_base_setting_vo data)
        {
            data.user_value = data.data_combination(data.user_setting);
            Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_setting, data.Set_Uptade_String(), data.Get_Update_Character());
        }
        /// <summary>
        /// 刷新属性
        /// </summary>
        private void refresh_Max_Hero_Attribute()
        {
            SumSave.crt_MaxHero = new crtMaxHeroVO();
            crtMaxHeroVO crt = new crtMaxHeroVO();
            crt.show_name= SumSave.crt_hero.hero_name;
            for (int i = 0; i < SumSave.db_heros.Count; i++)
            {
                if (SumSave.db_heros[i].hero_name == SumSave.crt_hero.hero_pos)
                {
                    for (int j = 0; j < SumSave.db_heros[i].crate_value.Length; j++)
                    {
                        int value = SumSave.db_heros[i].crate_value[j] + (SumSave.db_heros[i].up_value[j] * (SumSave.crt_hero.hero_Lv / SumSave.db_heros[i].up_base_value[j]));
                        Enum_Value(crt, j, value);
                    }
                }
            }
            //添加装备效果
            for (int i = 0; i < SumSave.crt_euqip.Count; i++)
            {
                Bag_Base_VO data= SumSave.crt_euqip[i];
                string[] info = data.user_value.Split(' ');
                int strengthenlv = int.Parse(info[1]);
                crt.damageMin += data.damgemin;
                crt.damageMax += data.damagemax;
                crt.MagicdamageMin += data.magicmin;
                crt.MagicdamageMax += data.magicmax;
                crt.DefMin += data.defmin;
                crt.DefMax += data.defmax;
                crt.MagicDefMin += data.macdefmin;
                crt.MagicDefMax += data.macdefmax;
                crt.MaxHP += data.hp;
                crt.MaxMp += data.mp;
                if (data.damgemin > 0 || data.damagemax > 0)
                {
                    crt.damageMin += Obtain_Equip_strengthenlv_Value(data, strengthenlv);
                    crt.damageMax += Obtain_Equip_strengthenlv_Value(data, strengthenlv);
                }
                if (data.magicmin > 0 || data.magicmax > 0)
                {
                    crt.MagicdamageMin += Obtain_Equip_strengthenlv_Value(data, strengthenlv);
                    crt.MagicdamageMax += Obtain_Equip_strengthenlv_Value(data, strengthenlv);
                }
                if (data.defmin > 0 || data.defmax > 0)
                {
                    crt.DefMin += Obtain_Equip_strengthenlv_Value(data, strengthenlv, 2);
                    crt.DefMax += Obtain_Equip_strengthenlv_Value(data, strengthenlv, 2);
                }
                if (data.macdefmin > 0 || data.macdefmax > 0)
                {
                    crt.MagicDefMin += Obtain_Equip_strengthenlv_Value(data, strengthenlv, 2);
                    crt.MagicDefMax += Obtain_Equip_strengthenlv_Value(data, strengthenlv, 2);
                }
                if (info.Length > 4)
                {
                    //类型
                    string[] arr = info[3].Split(',');
                    //值
                    string[] arr_value = info[4].Split(',');
                    int index = 0;
                    for (int j = 0; j < arr.Length; j++)
                    {
                        ++index;
                        if (j == 0)
                        {
                            switch (int.Parse(arr[j]))
                            {
                                case 1: crt.damageMin += int.Parse(arr_value[j]);   break;
                                case 2:
                                    crt.damageMax += int.Parse(arr_value[j]);
                                    break;
                                case 3: crt.MagicdamageMin += int.Parse(arr_value[j]); break;
                                case 4: crt.MagicdamageMax += int.Parse(arr_value[j]); break;
                                case 5: crt.DefMax+= int.Parse(arr_value[j]); break;
                                case 6: crt.MagicDefMax += int.Parse(arr_value[j]); break;
                                case 7: crt.MaxHP += int.Parse(arr_value[j]); break;
                                case 8: crt.MaxMp += int.Parse(arr_value[j]); break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            int value= int.Parse(arr_value[j]);
                            Enum_Value(crt, int.Parse(arr[j]), value);
                        }
                    }
                }
            }
            //添加收集效果

            //添加技能效果
            foreach (base_skill_vo skill in SumSave.crt_skills)
            {
                if (skill.skill_open_type.Count > 0)
                {
                    for (int i = 0; i < skill.skill_open_type.Count; i++)
                    {
                        //技能属性根据等级提升
                        //skill.skill_open_value[i] += int.Parse(skill.user_values[1])* skill.skill_power;

                        Enum_Value(crt, skill.skill_open_type[i], skill.skill_open_value[i] * int.Parse(skill.user_values[1]) / skill.skill_max_lv);//根据技能属性类型，加成属性
                    }
                }
                if (skill.skill_pos_type.Count > 0)
                {
                    for (int i = 0; i < skill.skill_pos_type.Count; i++)
                    {
                        if (int.Parse(skill.user_values[3]) > 0)
                        {
                            Enum_Value(crt, skill.skill_pos_type[i], skill.skill_pos_value[i] * int.Parse(skill.user_values[1]) / skill.skill_max_lv);
                        }
                    }
                }
            }
            //添加神器效果
            List < (string, int) > artifacts = SumSave.crt_artifact.Set();
            if (artifacts.Count > 0)
            {
                foreach (var item in artifacts)
                { 
                    db_artifact_vo data = ArrayHelper.Find(SumSave.db_Artifacts, e => e.arrifact_name == item.Item1);
                    if (data != null)
                    {
                        int strengthenlv = item.Item2;
                        string[] splits = data.arrifact_effects;
                        if (splits.Length > 1)
                        {
                            foreach (var base_info in splits)
                            {
                                string[] infos = base_info.Split(' ');
                                //1类型 2每一级加成 3开启等级
                                if (infos.Length >= 3)
                                {
                                    if (strengthenlv >= int.Parse(infos[2]))
                                    { 
                                        Enum_Value(crt, int.Parse(infos[0]), int.Parse(infos[1]) * strengthenlv);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //称号属性
            //成就属性
            //丹药属性
            List<(string, List<int>)> seeds = SumSave.crt_seeds.GetuseList();
            if (seeds.Count > 0)
            {
                foreach (var item in seeds)
                {
                    db_seed_vo data = ArrayHelper.Find(SumSave.db_seeds, e => e.pill == item.Item1);
                    if (data != null)
                    {
                        Enum_Value(crt, data.dicdictionary_index, item.Item1[1]);
                    }
                }
            }
            //皮肤


            SumSave.crt_MaxHero = crt;
            SumSave.crt_MaxHero.Init();
            Battle_Tool.validate_rank();
        }

        /// <summary>
        /// 加成属性
        /// </summary>
        /// <param name="crt">主体</param>
        /// <param name="index">编号</param>
        /// <param name="value">值</param>
        private void Enum_Value(crtMaxHeroVO crt,int index,int value)
        {
            switch ((enum_skill_attribute_list)(index))
            {
                case enum_skill_attribute_list.生命值:
                    crt.MaxHP += value;
                    break;
                case enum_skill_attribute_list.法力值:
                    crt.MaxMp += value;
                    break;
                case enum_skill_attribute_list.内力值:
                    crt.internalforceMP += value;
                    break;
                case enum_skill_attribute_list.蓄力值:
                    crt.EnergyMp += value;
                    break;
                case enum_skill_attribute_list.物理防御:
                    crt.DefMax += value;
                    break;
                case enum_skill_attribute_list.魔法防御:
                    crt.MagicDefMax += value;
                    break;
                case enum_skill_attribute_list.物理攻击:
                    crt.damageMax += value;
                    break;
                case enum_skill_attribute_list.魔法攻击:
                    crt.MagicdamageMax += value;
                    break;
                case enum_skill_attribute_list.命中:
                    crt.hit += value;
                    break;
                case enum_skill_attribute_list.躲避:
                    crt.dodge += value;
                    break;
                case enum_skill_attribute_list.穿透:
                    crt.penetrate += value;
                    break;
                case enum_skill_attribute_list.格挡:
                    crt.block += value;
                    break;
                case enum_skill_attribute_list.暴击:
                    crt.crit_rate += value;
                    break;
                case enum_skill_attribute_list.幸运:
                    crt.Lucky += value;
                    break;
                case enum_skill_attribute_list.暴击伤害:
                    crt.crit_damage += value;
                    break;
                case enum_skill_attribute_list.伤害加成:
                    crt.double_damage += value;
                    break;
                case enum_skill_attribute_list.真实伤害:
                    crt.Real_harm += value;
                    break;
                case enum_skill_attribute_list.伤害减免:
                    crt.Damage_Reduction += value;
                    break;
                case enum_skill_attribute_list.伤害吸收:
                    crt.Damage_absorption += value;
                    break;
                case enum_skill_attribute_list.异常抗性:
                    crt.resistance += value;
                    break;
                case enum_skill_attribute_list.攻击速度:
                    crt.attack_speed += value;
                    break;
                case enum_skill_attribute_list.移动速度:
                    crt.move_speed += value;
                    break;
                case enum_skill_attribute_list.生命加成:
                    crt.bonus_Hp += value;
                    break;
                case enum_skill_attribute_list.法力加成:
                    crt.bonus_Mp += value;
                    break;
                case enum_skill_attribute_list.生命回复:
                    crt.Heal_Hp += value;
                    break;
                case enum_skill_attribute_list.法力回复:
                    crt.Heal_Mp += value;
                    break;
                case enum_skill_attribute_list.物攻加成:
                    crt.bonus_Damage += value;
                    break;
                case enum_skill_attribute_list.魔攻加成:
                    crt.bonus_MagicDamage += value;
                    break;
                case enum_skill_attribute_list.物防加成:
                    crt.bonus_Def += value;
                    break;
                case enum_skill_attribute_list.魔防加成:
                    crt.bonus_MagicDef += value;
                    break;
                case enum_skill_attribute_list.土属性强化:
                case enum_skill_attribute_list.火属性强化:
                case enum_skill_attribute_list.水属性强化:
                case enum_skill_attribute_list.金属性强化:
                case enum_skill_attribute_list.木属性强化:
                    crt.life[index - 30] += value;
                    break;
                default:
                    break;
            }

        }
        /// <summary>
        /// 强化属性加成
        /// </summary>
        /// <param name="data"></param>
        /// <param name="lv"></param>
        /// <returns></returns>
        private int Obtain_Equip_strengthenlv_Value(Bag_Base_VO data,int lv,float coefficient=1)
        {
            return (int)((data.equip_lv * lv) / coefficient);
        }

        ///
        /// <summary>
        /// 
        /// </summary>
        public void Refresh_Max_Hero_Attribute()
        {
            refresh_Max_Hero_Attribute();
            //刷新战斗
            UI_Manager.I.GetPanel<panel_fight>().Refresh_Max_Hero_Attribute();
        }

        /// <summary>
        /// 读取资源数据
        /// </summary>
        private void Read_User_Resources()
        {
            
            mysqlReader = MysqlDb.Select(Mysql_Table_Name.mo_user_value, "uid", GetStr(SumSave.crt_user.uid));//读取角色信息
            SumSave.crt_resources = new user_base_Resources_vo();
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.crt_resources = ReadDb.Read(mysqlReader, new user_base_Resources_vo());
                }
                Init();
                SumSave.crt_resources.now_time= DateTime.Now;
                Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_value, SumSave.crt_resources.Set_Uptade_String(), SumSave.crt_resources.Get_Update_Character());

            }
            else
            {
                SumSave.crt_resources.now_time = DateTime.Now;
                SumSave.crt_resources.skill_value = "";
                SumSave.crt_resources.house_value = "";
                SumSave.crt_resources.bag_value = "";
                SumSave.crt_resources.material_value = "";
                SumSave.crt_resources.equip_value = "";
                SumSave.crt_resources.pages = new int[] { 120, 60, 0, 0, 0};
                Game_Omphalos.i.GetQueue(Mysql_Type.InsertInto, Mysql_Table_Name.mo_user_value, SumSave.crt_resources.Set_Instace_String());
            }
        }

        /// <summary>
        /// 读取英雄数据
        /// </summary>
        private void Read_User_Hero()
        {
            mysqlReader = MysqlDb.Select(Mysql_Table_Name.mo_user_hero, "uid", GetStr(SumSave.crt_user.uid));
            SumSave.crt_hero = new Hero_VO();
            if (mysqlReader.HasRows)
            {
                while (mysqlReader.Read())
                {
                    SumSave.crt_hero = ReadDb.Read(mysqlReader, new Hero_VO());
                }
            }
            else
            {
                SumSave.crt_hero.hero_name = "007";
                SumSave.crt_hero.hero_type = enum_hero_type_list.物理攻击.ToString();
                SumSave.crt_hero.hero_index = "1";
                SumSave.crt_hero.hero_list = "江湖人";
                SumSave.crt_hero.hero_lv = "1";
                SumSave.crt_hero.hero_exp = "0";
                SumSave.crt_hero.hero_Lv = 1;
                SumSave.crt_hero.hero_Exp = 0;
                SumSave.crt_hero.hero_pos = "江湖人";
                SumSave.crt_hero.hero_material_list = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
                Game_Omphalos.i.GetQueue(Mysql_Type.InsertInto, Mysql_Table_Name.mo_user_hero, SumSave.crt_hero.Set_Instace_String());
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