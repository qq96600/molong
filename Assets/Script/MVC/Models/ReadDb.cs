using System;
using System.Collections.Generic;
using System.Data;
using MVC;
using MySql.Data.MySqlClient;
using UnityEngine;

/// <summary>
/// 读取数据
/// </summary>
public static class ReadDb
{

    public static db_formula_vo Read(MySqlDataReader reader, db_formula_vo item)
    {
       item.formula_type= reader.GetInt32(reader.GetOrdinal("formula_type"));
        item.formula_result= reader.GetString(reader.GetOrdinal("formula_result"));
        item.formula_need= reader.GetString(reader.GetOrdinal("formula_need"));
        item.Init();
        return item;
    }


    public static user_world_boss Read(MySqlDataReader reader, user_world_boss item)
    {
        item.damage = reader.GetInt32(reader.GetOrdinal("damage"));
        item.datetime= Convert.ToDateTime(reader.GetString(reader.GetOrdinal("datetime")));
        return item;
    }
    public static db_fate_vo Read(MySqlDataReader reader, db_fate_vo item)
    {
        item.fate_id= reader.GetInt32(reader.GetOrdinal("fate_index"));
        item.fate_value= reader.GetString(reader.GetOrdinal("fate_value"));
        item.Init();
        return item;
    }
    public static user_base_vo Read(MySqlDataReader reader, user_base_vo item)
    {
        item.uid = reader.GetString(reader.GetOrdinal("uid"));
        item.RegisterDate = Convert.ToDateTime(reader.GetString(reader.GetOrdinal("RegisterDate")));
        item.Nowdate = Convert.ToDateTime(reader.GetString(reader.GetOrdinal("Nowdate")));
        item.par = reader.GetInt32(reader.GetOrdinal("par"));
        return item;
    }
    public static user_Accumulatedrewards_vo Read(MySqlDataReader reader, user_Accumulatedrewards_vo item)
    {
        item.user_value = reader.GetString(reader.GetOrdinal("accumulated_rewards"));
        int Real_recharge= reader.GetInt32(reader.GetOrdinal("Real_recharge"));
        int sum_recharge = reader.GetInt32(reader.GetOrdinal("sum_recharge"));
        item.Init(Real_recharge, sum_recharge);
        return item;
    }
    public static db_mail_vo Read(MySqlDataReader reader, db_mail_vo item)
    {
        item.mail_id = reader.GetInt32(reader.GetOrdinal("id"));
        item.mail_time = Convert.ToDateTime(reader.GetString(reader.GetOrdinal("mail_time")));
        item.mail_par= reader.GetInt32(reader.GetOrdinal("mail_par"));
        item.uid= reader.GetString(reader.GetOrdinal("uid"));
        item.user_value= reader.GetString(reader.GetOrdinal("user_value"));
        item.dec = reader.GetString(reader.GetOrdinal("dec"));
        item.moeny= reader.GetInt32(reader.GetOrdinal("moeny"));
        item.Init();
        return item;
    }
    public static user_mail_vo Read(MySqlDataReader reader, user_mail_vo item)
    {
        item.user_value = reader.GetString(reader.GetOrdinal("user_value"));
        item.Init();
        return item;
    }
    public static user_player_Buff Read(MySqlDataReader reader, user_player_Buff item)
    {
        item.player_baff = reader.GetString(reader.GetOrdinal("player_Buff"));
        item.SplitBuff();
        return item;
    }

    public static user_collect_vo Read(MySqlDataReader reader, user_collect_vo item)
    {
        item.collect_value = reader.GetString(reader.GetOrdinal("collect_value"));
        //item.collect_suit_value = reader.GetString(reader.GetOrdinal("collect_suit_value"));
        return item;
    }

    public static user_greenhand_vo Read(MySqlDataReader reader, user_greenhand_vo item)
    {
        item.user_value = reader.GetString(reader.GetOrdinal("valuelist"));
        item.crt_task = reader.GetInt32(reader.GetOrdinal("crt_task"));
        item.Init();
        return item;
    }
    public static GreenhandGuide_TotalTaskVO Read(MySqlDataReader reader, GreenhandGuide_TotalTaskVO item)
    {
        item.id = reader.GetInt32(reader.GetOrdinal("id"));
        item.TaskDesc = reader.GetString(reader.GetOrdinal("TaskDesc"));
        item.tasktype = (GreenhandGuideTaskType)Enum.Parse(typeof(GreenhandGuideTaskType), reader.GetString(reader.GetOrdinal("tasktype")));
        item.Award = reader.GetString(reader.GetOrdinal("Award")).Split(';');
        string[] data = reader.GetString(reader.GetOrdinal("AwardNumber")).Split(';');
        item.taskorder = reader.GetInt32(reader.GetOrdinal("taskorder"));
        item.taskid = reader.GetInt32(reader.GetOrdinal("taskid"));
        item.task_dec_type = item.TaskDesc;
        item.task_dec_value = reader.GetString(reader.GetOrdinal("task_dec_value"));
        item.AwardNumber = new int[data.Length];
        int i = 0;
        foreach (string value in data)
        {
            item.AwardNumber[i] = int.Parse(value);
            i++;
        }
        item.AwardType = reader.GetString(reader.GetOrdinal("AwardType")).Split(';');
        item.progress = reader.GetInt32(reader.GetOrdinal("progress"));
        return item;
    }
    public static bag_seed_vo Read(MySqlDataReader reader, bag_seed_vo item)
    {
        item.user_value = reader.GetString(reader.GetOrdinal("user_value"));
        item.formula_value = reader.GetString(reader.GetOrdinal("formula_value"));
        item.use_value = reader.GetString(reader.GetOrdinal("user_use_value"));
        item.Init();
        return item;
    }
    public static db_signin_vo Read(MySqlDataReader reader, db_signin_vo item)
    {
        item.index = reader.GetInt32(reader.GetOrdinal("index"));
        item.value = reader.GetString(reader.GetOrdinal("value"));
        return item;
    }
    public static db_base_par Read(MySqlDataReader reader, db_base_par item)
    {
        item.index= reader.GetInt32(reader.GetOrdinal("par"));
        item.opentime = Convert.ToDateTime(reader.GetString(reader.GetOrdinal("time")));
        item.openstate= reader.GetInt32(reader.GetOrdinal("openstate"));
        item.device = reader.GetInt32(reader.GetOrdinal("device"));
        return item;
    }


    public static user_signin_vo Read(MySqlDataReader reader, user_signin_vo item)
    {
        item.now_time = Convert.ToDateTime(reader.GetString(reader.GetOrdinal("now_time")));
        item.number = reader.GetInt32(reader.GetOrdinal("number"));
        item.user_value = reader.GetString(reader.GetOrdinal("user_value"));
        item.max_number= reader.GetInt32(reader.GetOrdinal("max_number"));
        item.Init();
        return item;
    }
    public static user_explore_vo Read(MySqlDataReader reader, user_explore_vo item)
    {
        item.user_value = reader.GetString(reader.GetOrdinal("user_value"));
        item.Init();
        return item;
    }
    public static db_seed_vo Read(MySqlDataReader reader, db_seed_vo item)
    {
        item.type = reader.GetString(reader.GetOrdinal("type"));
        item.sequence = reader.GetInt32(reader.GetOrdinal("sequence"));
        item.seed_name = reader.GetString(reader.GetOrdinal("seed_name"));
        item.seed_formula = reader.GetString(reader.GetOrdinal("seed_formula"));
        item.pill = reader.GetString(reader.GetOrdinal("pill"));
        item.formula = reader.GetString(reader.GetOrdinal("formula"));
        item.pill_effect = reader.GetString(reader.GetOrdinal("pill_effect"));
        item.Weight= reader.GetInt32(reader.GetOrdinal("Weight"));
        item.seed_number= reader.GetInt32(reader.GetOrdinal("seed_number"));
        item.rule = reader.GetInt32(reader.GetOrdinal("rule"));
        item.dicdictionary_index = reader.GetInt32(reader.GetOrdinal("dicdictionary_index"));
        return item;
    }

    public static db_collect_vo Read(MySqlDataReader reader, db_collect_vo item)
    {
        item.Name= reader.GetString(reader.GetOrdinal("Name"));
        item.StdMode = reader.GetString(reader.GetOrdinal("StdMode"));
        item.bonuses_type= reader.GetString(reader.GetOrdinal("Collect bonuses type"));
        item.bonuses_value = reader.GetString(reader.GetOrdinal("Collect bonuses value"));

        item.Init();
        return item;
    }
    public static user_needlist_vo Read(MySqlDataReader reader, user_needlist_vo item)
    {
        item.store_value = reader.GetString(reader.GetOrdinal("store_value"));
        item.map_value = reader.GetString(reader.GetOrdinal("map_value"));
        item.fate_value = reader.GetString(reader.GetOrdinal("fate_value"));
        item.user_value= reader.GetString(reader.GetOrdinal("user_value"));
        item.Init();
        return item;
    }



    public static db_hall_vo Read(MySqlDataReader reader, db_hall_vo item)
    {
        string otainlist= reader.GetString(reader.GetOrdinal("otainlist"));
        item.otainlist_btn = new System.Collections.Generic.List<string>();
        string[] otainlist_btn = otainlist.Split(' ');
        for (int i = 0; i < otainlist_btn.Length; i++)
        { 
            item.otainlist_btn.Add(otainlist_btn[i]);
        }

        string otainpanel= reader.GetString(reader.GetOrdinal("otainpanel"));
        item.otainpanel = new System.Collections.Generic.List<string>();
        string[] otainpanel_btn = otainpanel.Split(' ');
        for (int i = 0; i < otainpanel_btn.Length; i++)
        { 
            item.otainpanel.Add(otainpanel_btn[i]);
        }

        string maplist= reader.GetString(reader.GetOrdinal("maplist"));
        item.maplist_btn = new System.Collections.Generic.List<string>();
        string[] maplist_btn = maplist.Split(' ');
        for (int i = 0; i < maplist_btn.Length; i++)
        { 
            item.maplist_btn.Add(maplist_btn[i]);
        }

        string mappanel= reader.GetString(reader.GetOrdinal("mappanel"));
        item.mappanel = new System.Collections.Generic.List<string>();
        string[] mappanel_btn = mappanel.Split(' ');
        for (int i = 0; i < mappanel_btn.Length; i++)
        { 
            item.mappanel.Add(mappanel_btn[i]);
        }

        string herolist= reader.GetString(reader.GetOrdinal("herolist"));
        item.herolist_btn = new System.Collections.Generic.List<string>();
        string[] herolist_btn = herolist.Split(' ');
        for (int i = 0; i < herolist_btn.Length; i++)
        { 
            item.herolist_btn.Add(herolist_btn[i]);
        }

        string heropanel= reader.GetString(reader.GetOrdinal("heropanel"));
        item.heropanel = new System.Collections.Generic.List<string>();
        string[] heropanel_btn = heropanel.Split(' ');
        for (int i = 0; i < heropanel_btn.Length; i++)
        { 
            item.heropanel.Add(heropanel_btn[i]);
        }
        return item;
    }
    public static db_lv_vo Read(MySqlDataReader reader, db_lv_vo item)
    {
        string hero_lv_exp = reader.GetString(reader.GetOrdinal("hero_lv_exp"));
        item.hero_lv_list = new System.Collections.Generic.List<long>();
        string[] hero_lv_exp_list = hero_lv_exp.Split(' ');
        for (int i = 0; i < hero_lv_exp_list.Length; i++)
        {
            item.hero_lv_list.Add(Convert.ToInt64(hero_lv_exp_list[i]));
        }
        string word_lv_exp= reader.GetString(reader.GetOrdinal("word_lv_exp"));
        item.world_lv_list = new System.Collections.Generic.List<(string, int)>();
        string[] word_lv_exp_list = word_lv_exp.Split('&');
        for (int i = 0; i < word_lv_exp_list.Length; i++)
        { 
            string[] word_lv_exp_list2 = word_lv_exp_list[i].Split(' ');
            if(word_lv_exp_list2.Length==2)
            item.world_lv_list.Add((word_lv_exp_list2[0], Convert.ToInt32(word_lv_exp_list2[1])));
        }
        string world_offect_list = reader.GetString(reader.GetOrdinal("world_offect_list"));
        item.world_offect_list = new System.Collections.Generic.List<int>();
        string[] world_offect_list2 = world_offect_list.Split(' ');
        for (int i = 0; i < world_offect_list2.Length; i++)
        { 
            item.world_offect_list.Add(Convert.ToInt32(world_offect_list2[i]));
        }
        string word_lv_max_value= reader.GetString(reader.GetOrdinal("word_lv_max_value"));
        item.word_lv_max_value = new System.Collections.Generic.List<int>();
        string[] word_lv_max_value2 = word_lv_max_value.Split(' ');
        for (int i = 0; i < word_lv_max_value2.Length; i++)
        { 
            item.word_lv_max_value.Add(Convert.ToInt32(word_lv_max_value2[i]));
        }
        return item;
    }

    public static db_store_vo Read(MySqlDataReader reader, db_store_vo item)
    {
        item.store_Type = reader.GetInt32(reader.GetOrdinal("StoreType"));
        item.ItemName = reader.GetString(reader.GetOrdinal("ItemName"));
        item.ItemPrice = reader.GetInt32(reader.GetOrdinal("ItemPrice"));
        item.ItemMaxQuantity= reader.GetInt32(reader.GetOrdinal("ItemMaxQuantity"));
        item.unit = reader.GetString(reader.GetOrdinal("unit"));
        string[] dis= reader.GetString(reader.GetOrdinal("discount")).Split(' ');
        
        if(dis.Length ==2)
        {
            item.discount = (int.Parse(dis[0]) , int.Parse(dis[1]));
        }
        else
        {
            item.discount = (0, 0);
        }
        return item;
    }

    public static db_achievement_VO Read(MySqlDataReader reader, db_achievement_VO item)
    {
        item.achievement_type = reader.GetInt32(reader.GetOrdinal("achieve_type"));
        item.achievement_value = reader.GetString(reader.GetOrdinal("achieve_value"));
        item.achievement_need = reader.GetString(reader.GetOrdinal("achieve_need"));
        item.achievement_show_lv = reader.GetString(reader.GetOrdinal("achieve_show_lv")).Split('|');
        string[] split = item.achievement_need.Split('|');
        foreach (string value in split)
        {
            item.achievement_needs.Add(long.Parse(value));
        }
        item.achievement_reward = reader.GetString(reader.GetOrdinal("achieve_reward"));
        split = item.achievement_reward.Split('|');
        foreach (string value in split)
        {
            item.achievement_rewards.Add(value);
        }
        item.achievement_exchange_offect = reader.GetString(reader.GetOrdinal("achieve_exchange_offect"));

        return item;
    }
    public static user_achievement_vo Read(MySqlDataReader reader, user_achievement_vo item)
    {
        item.achievement_exp = reader.GetString(reader.GetOrdinal("achieve_exp"));
        item.achievement_lvs = reader.GetString(reader.GetOrdinal("achieve_lvs"));



        #region 更换现有玩家成就词条
        //if (item.achievement_exp == "" && item.achievement_lvs == "")
        //{
        //    return item;
        //}
        //string[] str = item.achievement_exp.Split("|");
        //string[] strsTemp = str[0].Split(" ");
        //if (strsTemp[0] != Common.SumSave.db_Achievement_dic[0].achievement_value)  //如果为旧数据库词条 只需检测第一条 一条不符则全部不符
        //{
        //    string[] lv = item.achievement_lvs.Split("|");
        //    List<string[]> expList = new List<string[]>();  //expList[0][0]为第一词条的名称 expList[0][1]为第一词条的值 依次类推
        //    List<string[]> lvList = new List<string[]>();
        //    for (int j = 0; j < lv.Length; j++)
        //    {
        //        expList.Add(str[j].Split(" "));
        //        lvList.Add(lv[j].Split(" "));
        //    }
        //    if (Common.SumSave.db_Achievement_dic.Count != expList.Count)
        //    {
        //        int temp = 0; //从17开始需要 跳过3个索引
        //        for (int i = 0; i < Common.SumSave.db_Achievement_dic.Count - 3; i++) //这里可以直接减去3个雪域地图
        //        {
        //            if (i == 17 || i == 18 || i == 19) { temp = 3; }//部分玩家没有雪域地图
        //            if (Common.SumSave.db_Achievement_dic[i + temp].achievement_value != expList[i][0])
        //            {
        //                expList[i][0] = Common.SumSave.db_Achievement_dic[i + temp].achievement_value;
        //                lvList[i][0] = Common.SumSave.db_Achievement_dic[i + temp].achievement_value;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i < Common.SumSave.db_Achievement_dic.Count; i++)
        //        {
        //            if (Common.SumSave.db_Achievement_dic[i].achievement_value != expList[i][0])
        //            {
        //                expList[i][0] = Common.SumSave.db_Achievement_dic[i].achievement_value;
        //                lvList[i][0] = Common.SumSave.db_Achievement_dic[i].achievement_value;
        //            }
        //        }
        //    }

        //    item.achievement_exp = "";
        //    item.achievement_lvs = "";
        //    for (int i = 0; i < str.Length; i++)
        //    {
        //        item.achievement_exp += i == 0 ? expList[i][0] + " " + expList[i][1] : "|" + expList[i][0] + " " + expList[i][1];
        //        item.achievement_lvs += i == 0 ? lvList[i][0] + " " + lvList[i][1] : "|" + lvList[i][0] + " " + lvList[i][1];
        //    }
        //}
        #endregion
        item.Init();
        return item;
    }



    public static user_pet_explore_vo Read_Pass(MySqlDataReader reader, user_pet_explore_vo item)
    {
        item.petExploreMapName = reader.GetString(reader.GetOrdinal("petEvent_name"));
        item.petEvent_reward = reader.GetString(reader.GetOrdinal("petEvent_reward"));
        item.Init();
        return item;
    }


    public static user_world_vo Read(MySqlDataReader reader, user_world_vo item)
    {
        item.World_Lv = reader.GetInt32(reader.GetOrdinal("World_Lv"));
        item.user_value = reader.GetString(reader.GetOrdinal("user_value"));
        item.Init();
        return item;
    }
    public static user_pet_vo Read(MySqlDataReader reader, user_pet_vo item)
    {
        item.pet_value = reader.GetString(reader.GetOrdinal("user_value"));
        item.Init();
        return item;
    }

    public static db_pet_vo Read(MySqlDataReader reader, db_pet_vo item)
    {
        item.user_value = reader.GetString(reader.GetOrdinal("user_value"));
        item.Init();
        return item;
    }
    public static db_Accumulatedrewards_vo Read(MySqlDataReader reader, db_Accumulatedrewards_vo item)
    {
        item.pass_value = reader.GetString(reader.GetOrdinal("pass_value"));
        item.signin_value = reader.GetString(reader.GetOrdinal("signin_value"));
        item.fate_value= reader.GetString(reader.GetOrdinal("fate_value"));
        item.Init();
        return item;
    }

    public static db_vip Read(MySqlDataReader reader, db_vip item)
    {
        item.vip_lv= reader.GetInt32(reader.GetOrdinal("vip_lv"));
        item.vip_name= reader.GetString(reader.GetOrdinal("vip_name"));
        item.vip_exp= reader.GetInt32(reader.GetOrdinal("vip_exp"));
        item.experienceBonus= reader.GetInt32(reader.GetOrdinal("experienceBonus"));
        item.lingzhuIncome= reader.GetInt32(reader.GetOrdinal("lingzhuIncome"));
        item.equipmentExplosionRate= reader.GetInt32(reader.GetOrdinal("equipmentExplosionRate"));
        item.characterExperience= reader.GetInt32(reader.GetOrdinal("characterExperience"));
        item.monsterHuntingInterval= reader.GetInt32(reader.GetOrdinal("monsterHuntingInterval"));
        item.hpRecovery= reader.GetInt32(reader.GetOrdinal("hpRecovery"));
        item.manaRegeneration= reader.GetInt32(reader.GetOrdinal("manaRegeneration"));
        item.goodFortune= reader.GetInt32(reader.GetOrdinal("goodFortune"));
        item.strengthenCosts= reader.GetInt32(reader.GetOrdinal("strengthenCosts"));
        item.offlineInterval= reader.GetInt32(reader.GetOrdinal("offlineInterval"));
        item.signInIncome= reader.GetInt32(reader.GetOrdinal("signInIncome"));
        item.whippingCorpses= reader.GetInt32(reader.GetOrdinal("whippingCorpses"));
        item.upperLimitOfSpiritualEnergy= reader.GetInt32(reader.GetOrdinal("upperLimitOfSpiritualEnergy"));
        return item;
    }



    public static user_plant_vo Read(MySqlDataReader reader, user_plant_vo item)
    {
        item.plantLeve= reader.GetInt32(reader.GetOrdinal("plantLeve"));
        item.user_value= reader.GetString(reader.GetOrdinal("user_value"));
        item.Init();
        return item;
    }
    public static user_plant_vo Read_Pass(MySqlDataReader reader, user_plant_vo item)
    {
        item.plantName= reader.GetString(reader.GetOrdinal("plantType"));
        item.plantTime= reader.GetInt32(reader.GetOrdinal("plantTime"));
        item.HarvestMaterials= reader.GetString(reader.GetOrdinal("HarvestMaterials"));
        item.harvestnumber = reader.GetInt32(reader.GetOrdinal("harvestnumber"));
        item.lossnumber = reader.GetInt32(reader.GetOrdinal("lossnumber"));
        return item; 
    }


    public static db_pet_vo Read_Pass(MySqlDataReader reader, db_pet_vo item)
    {
        item.petName= reader.GetString(reader.GetOrdinal("petName"));
        item.petEggsName = reader.GetString(reader.GetOrdinal("petEggsName"));
        item.hatchingTime=reader.GetInt32(reader.GetOrdinal("hatchingTime"));
        item.hero_type= reader.GetInt32(reader.GetOrdinal("hero_type"));
        item.crate_value= reader.GetString(reader.GetOrdinal("crate_value"));
        item.up_value= reader.GetString(reader.GetOrdinal("up_value"));
        item.up_base_value= reader.GetString(reader.GetOrdinal("up_base_value"));
        item.hero_talent = reader.GetString(reader.GetOrdinal("hero_talent"));
        item.pet_explore = reader.GetString(reader.GetOrdinal("pet_explore"));
        item.GetNumerical();
        return item;
    }
    public static user_pass_vo Read(MySqlDataReader reader, user_pass_vo item)
    {
        item.data_lv = reader.GetInt32(reader.GetOrdinal("pass_lv"));
        item.data_exp = reader.GetInt32(reader.GetOrdinal("pass_exp"));
        item.Max_task_number= reader.GetInt32(reader.GetOrdinal("Max_task_number"));
        item.user_value = reader.GetString(reader.GetOrdinal("user_value"));
        item.day_state_value = reader.GetString(reader.GetOrdinal("day_state_value"));
        item.Init();
        return item;
    }
    public static user_pass_vo Read_Pass(MySqlDataReader reader, user_pass_vo item)
    {
        item.lv = reader.GetInt32(reader.GetOrdinal("db_lv"));
        item.pass_index= reader.GetInt32(reader.GetOrdinal("db_index"));
        item.reward = reader.GetString(reader.GetOrdinal("reward"));
        item.uplv_reward = reader.GetString(reader.GetOrdinal("uplv_reward"));
        return item;
    }
    public static user_vo Read(MySqlDataReader reader, user_vo item)
    {
        string value= reader.GetString(reader.GetOrdinal("value"));
        item.Init(value);
        return item;
    }
    public static user_artifact_vo Read(MySqlDataReader reader, user_artifact_vo item)
    {
        item.artifact_value = reader.GetString(reader.GetOrdinal("artifact_value"));
        item.Init();
        return item;
    }
    public static db_artifact_vo Read(MySqlDataReader reader, db_artifact_vo item)
    {
        item.arrifact_name = reader.GetString(reader.GetOrdinal("Artifact_name"));
        item.arrifact_needs = reader.GetString(reader.GetOrdinal("Artifact_need")).Split('&');
        item.arrifact_effects = reader.GetString(reader.GetOrdinal("Artifact_effect")).Split('&');
        item.arrifact_type = reader.GetInt32(reader.GetOrdinal("Artifact_type"));
        item.Artifact_dec= reader.GetString(reader.GetOrdinal("Artifact_dec"));
        item.Artifact_MaxLv = reader.GetInt32(reader.GetOrdinal("Artifact_MaxLv"));
        return item;
    }
    public static user_base_setting_vo Read(MySqlDataReader reader, user_base_setting_vo item)
    {
        item.user_value = reader.GetString(reader.GetOrdinal("setting_value"));
        item.Init();
        return item;
    }

    public static user_setting_type_vo Read(MySqlDataReader reader, user_setting_type_vo item)
    {
        item.id_setting = reader.GetInt32(reader.GetOrdinal("id"));
        item.type_setting = reader.GetString(reader.GetOrdinal("setting_type"));
        item.option_setting= reader.GetString(reader.GetOrdinal("setting_value"));
       

        return item;
    }
    public static user_base_Resources_vo Read(MySqlDataReader reader, user_base_Resources_vo item)
    {
        item.now_time = Convert.ToDateTime(reader.GetString(reader.GetOrdinal("now_time")));
        item.user_map_index= reader.GetString(reader.GetOrdinal("user_map_index"));
        item.skill_value = reader.GetString(reader.GetOrdinal("skill_value"));
        item.house_value = reader.GetString(reader.GetOrdinal("house_value"));
        item.bag_value = reader.GetString(reader.GetOrdinal("bag_value"));
        item.material_value = reader.GetString(reader.GetOrdinal("material_value"));
        item.equip_value = reader.GetString(reader.GetOrdinal("equip_value"));
        string page= reader.GetString(reader.GetOrdinal("page_value"));
        string[] page_value = page.Split(' ');
        item.pages= new int[page_value.Length];
        for (int i = 0; i < page_value.Length; i++)
        {
            item.pages[i] = int.Parse(page_value[i]);
        }
        return item;
    }
    public static Hero_VO Read(MySqlDataReader reader, Hero_VO item)
    {
        item.hero_name = reader.GetValue(reader.GetOrdinal("hero_name")).ToString();
        item.hero_lv = reader.GetString(reader.GetOrdinal("hero_lv"));
        item.hero_Lv = int.Parse(item.hero_lv);
        item.hero_exp= reader.GetString(reader.GetOrdinal("hero_exp"));
        item.hero_Exp = long.Parse(item.hero_exp);
        item.hero_pos= reader.GetString(reader.GetOrdinal("hero_pos"));
        item.hero_value= reader.GetString(reader.GetOrdinal("hero_value"));
        item.hero_material= reader.GetString(reader.GetOrdinal("hero_material"));
        string[] hero_material_array = item.hero_material.Split(' ');
        item.hero_material_list = new int[hero_material_array.Length];
        for (int i = 0; i < hero_material_array.Length; i++)
        { 
            item.hero_material_list[i] = int.Parse(hero_material_array[i]);
        }
        
        return item;
    }
    public static db_hero_vo Read(MySqlDataReader reader, db_hero_vo item)
    {
        item.hero_name = reader.GetString(reader.GetOrdinal("show_name"));
        item.hero_type = reader.GetInt32(reader.GetOrdinal("hero_type"));
        string crate_value = reader.GetString(reader.GetOrdinal("crate_value"));
        string[] crate_value_array = crate_value.Split(' ');
        item.crate_value = new int[crate_value_array.Length];
        for (int i = 0; i < crate_value_array.Length; i++)
        { 
            item.crate_value[i] = int.Parse(crate_value_array[i]);
        }
        string up_base_value = reader.GetString(reader.GetOrdinal("up_base_value"));
        string[] up_base_value_array = up_base_value.Split(' ');
        item.up_base_value= new int[up_base_value_array.Length];
        for (int i = 0; i < up_base_value_array.Length; i++)
        { 
            item.up_base_value[i] = int.Parse(up_base_value_array[i]);
        }
        string up_value= reader.GetString(reader.GetOrdinal("up_value"));
        string[] up_value_array = up_value.Split(' ');
        item.up_value= new int[up_value_array.Length];
        for (int i = 0; i < up_value_array.Length; i++)
        {
            item.up_value[i] = int.Parse(up_value_array[i]);
        }
        return item;
    }
    public static user_map_vo Read(MySqlDataReader reader,user_map_vo item)
    {
        item.map_index = reader.GetInt32(reader.GetOrdinal("map_index"));
        item.map_name= reader.GetString(reader.GetOrdinal("map_name"));
        item.map_type = reader.GetInt32(reader.GetOrdinal("map_type"));
        item.need_lv = reader.GetInt32(reader.GetOrdinal("need_lv"));
        item.need_Required = reader.GetString(reader.GetOrdinal("need_Required"));
        item.ProfitList = reader.GetString(reader.GetOrdinal("ProfitList"));
        item.monster_list = reader.GetString(reader.GetOrdinal("monster_list"));
        item.map_life= reader.GetInt32(reader.GetOrdinal("map_life"));
        return item;
    }

    public static Bag_Base_VO Read(MySqlDataReader reader, Bag_Base_VO item)
    {
        item.Name = reader.GetString(reader.GetOrdinal("Name"));
        item.StdMode= reader.GetString(reader.GetOrdinal("StdMode"));
        item.need_lv= reader.GetInt32(reader.GetOrdinal("need_lv"));
        item.equip_lv= reader.GetInt32(reader.GetOrdinal("equip_lv"));
        item.price= reader.GetInt32(reader.GetOrdinal("price"));
        item.hp= reader.GetInt32(reader.GetOrdinal("hp"));
        item.mp= reader.GetInt32(reader.GetOrdinal("mp"));
        item.defmin= reader.GetInt32(reader.GetOrdinal("defmin"));
        item.defmax= reader.GetInt32(reader.GetOrdinal("defmax"));
        item.macdefmin= reader.GetInt32(reader.GetOrdinal("macdefmin"));
        item.macdefmax= reader.GetInt32(reader.GetOrdinal("macdefmax"));
        item.damgemin= reader.GetInt32(reader.GetOrdinal("damgemin"));
        item.damagemax= reader.GetInt32(reader.GetOrdinal("damagemax"));
        item.magicmin= reader.GetInt32(reader.GetOrdinal("magicmin"));
        item.magicmax= reader.GetInt32(reader.GetOrdinal("magicmax"));
        item.dec= reader.GetString(reader.GetOrdinal("dec"));
        item.suit= reader.GetInt32(reader.GetOrdinal("suit"));
        item.suit_name= reader.GetString(reader.GetOrdinal("suit_name"));
        item.suit_dec= reader.GetString(reader.GetOrdinal("suit_dec"));
        return item;
    }
    public static crtMaxHeroVO Read(MySqlDataReader reader, crtMaxHeroVO item)
    {
        item.show_name = reader.GetString(reader.GetOrdinal("show_name"));
        item.index= reader.GetInt32(reader.GetOrdinal("index"));
        item.Lv= reader.GetInt32(reader.GetOrdinal("Lv"));
        item.Exp= reader.GetInt32(reader.GetOrdinal("Exp"));
        item.icon= reader.GetString(reader.GetOrdinal("icon"));
        item.MaxHP= reader.GetInt32(reader.GetOrdinal("MaxHP"));
        item.MaxMp= reader.GetInt32(reader.GetOrdinal("MaxMp"));
        item.internalforceMP= reader.GetInt32(reader.GetOrdinal("internalforceMP"));
        item.EnergyMp= reader.GetInt32(reader.GetOrdinal("EnergyMp"));
        item.DefMin= reader.GetInt32(reader.GetOrdinal("DefMin"));
        item.DefMax= reader.GetInt32(reader.GetOrdinal("DefMax")); 
        item.MagicDefMin= reader.GetInt32(reader.GetOrdinal("MagicDefMin"));
        item.MagicDefMax= reader.GetInt32(reader.GetOrdinal("MagicDefMax"));
        item.damageMin= reader.GetInt32(reader.GetOrdinal("damageMin"));
        item.damageMax= reader.GetInt32(reader.GetOrdinal("damageMax"));
        item.MagicdamageMin= reader.GetInt32(reader.GetOrdinal("MagicdamageMin"));
        item.MagicdamageMax= reader.GetInt32(reader.GetOrdinal("MagicdamageMax"));
        item.hit= reader.GetInt32(reader.GetOrdinal("hit"));
        item.dodge= reader.GetInt32(reader.GetOrdinal("dodge"));
        item.penetrate= reader.GetInt32(reader.GetOrdinal("penetrate"));
        item.block= reader.GetInt32(reader.GetOrdinal("block"));
        item.crit_rate= reader.GetInt32(reader.GetOrdinal("crit_rate"));
        item.crit_damage= reader.GetInt32(reader.GetOrdinal("crit_damage"));
        item.double_damage= reader.GetInt32(reader.GetOrdinal("double_damage"));
        item.Lucky= reader.GetInt32(reader.GetOrdinal("Lucky"));
        item.Real_harm= reader.GetInt32(reader.GetOrdinal("Real_harm"));
        item.Damage_Reduction= reader.GetInt32(reader.GetOrdinal("Damage_Reduction"));
        item.Damage_absorption= reader.GetInt32(reader.GetOrdinal("Damage_absorption"));
        item.resistance= reader.GetInt32(reader.GetOrdinal("resistance"));
        item.move_speed= reader.GetInt32(reader.GetOrdinal("move_speed"));
        item.attack_speed= reader.GetInt32(reader.GetOrdinal("attack_speed"));
        item.attack_distance = reader.GetInt32(reader.GetOrdinal("attack_distance"));
        item.bonus_Hp= reader.GetInt32(reader.GetOrdinal("bonus_Hp"));
        item.bonus_Mp= reader.GetInt32(reader.GetOrdinal("bonus_Mp"));
        item.bonus_Damage = reader.GetInt32(reader.GetOrdinal("bonus_Damage"));
        item.bonus_MagicDamage= reader.GetInt32(reader.GetOrdinal("bonus_MagicDamage"));
        item.bonus_Def= reader.GetInt32(reader.GetOrdinal("bonus_Def"));
        item.bonus_MagicDef= reader.GetInt32(reader.GetOrdinal("bonus_MagicDef"));
        item.Heal_Hp= reader.GetInt32(reader.GetOrdinal("Heal_Hp"));
        item.Heal_Mp= reader.GetInt32(reader.GetOrdinal("Heal_Mp"));
        return item;
    }
    public static base_skill_vo Read(MySqlDataReader reader, base_skill_vo item)
    {
        item.skillname = reader.GetString(reader.GetOrdinal("skill_name")); 
        item.skill_type = reader.GetInt32(reader.GetOrdinal("skill_type"));
        item.skill_damage_type = reader.GetInt32(reader.GetOrdinal("skill_damage_type"));
        item.skilllv = 1; 
        item.skill_max_lv = reader.GetInt32(reader.GetOrdinal("skill_max_lv"));
        item.skill_life = reader.GetInt32(reader.GetOrdinal("skill_need_exp"));
        string skill_need_coefficient= reader.GetString(reader.GetOrdinal("skill_need_coefficient"));
        string[] skill_need_coefficient_array = skill_need_coefficient.Split(' ');
        item.skill_need_coefficient = new System.Collections.Generic.List<int>();
        if (skill_need_coefficient_array.Length > 0)
        {
            for (int i = 0; i < skill_need_coefficient_array.Length; i++)
            {
                item.skill_need_coefficient.Add(int.Parse(skill_need_coefficient_array[i]));
            }
        }
        string skill_need_state= reader.GetString(reader.GetOrdinal("skill_need_state"));
        string[] skill_need_state_array = skill_need_state.Split('*');
        item.skill_need_state = new System.Collections.Generic.List<(int, string)>();
        if (skill_need_state_array.Length > 0)
        {
            for (int i = 0; i < skill_need_state_array.Length; i++)
            {
                string[] splits=skill_need_state_array[i].Split(' ');
                if (splits.Length > 1)
                {
                    (int, string) temp = (int.Parse(splits[0]), splits[1]);
                    item.skill_need_state.Add(temp);
                }
            }
        }
        string skill_open_type = reader.GetString(reader.GetOrdinal("skill_open_type"));
        string[] skill_open_type_array = skill_open_type.Split(' ');
        item.skill_open_type = new System.Collections.Generic.List<int>();
        if (skill_open_type_array.Length > 0)
        {
            for (int i = 0; i < skill_open_type_array.Length; i++)
            { 
                item.skill_open_type.Add(int.Parse(skill_open_type_array[i]));
            }
        }

        string skill_open_value= reader.GetString(reader.GetOrdinal("skill_open_value")); 
        string[] skill_open_value_array = skill_open_value.Split(' ');
        item.skill_open_value = new System.Collections.Generic.List<int>();
        if (skill_open_value_array.Length > 0)
        {
            for (int i = 0; i < skill_open_value_array.Length; i++)
            { 
                item.skill_open_value.Add(int.Parse(skill_open_value_array[i]));
            }
        }
        string skill_pos_type = reader.GetString(reader.GetOrdinal("skill_pos_type"));
        string[] skill_pos_type_array = skill_pos_type.Split(' ');
        item.skill_pos_type = new System.Collections.Generic.List<int>();
        if (skill_pos_type_array.Length > 1)
        {
            for (int i = 0; i < skill_pos_type_array.Length; i++)
            { 
                item.skill_pos_type.Add(int.Parse(skill_pos_type_array[i]));
            }
        }
        string skill_pos_value= reader.GetString(reader.GetOrdinal("skill_pos_value"));
        string[] skill_pos_value_array = skill_pos_value.Split(' ');
        item.skill_pos_value = new System.Collections.Generic.List<int>();
        if (skill_pos_value_array.Length > 1)
        {
            for (int i = 0; i < skill_pos_value_array.Length; i++)
            { 
                item.skill_pos_value.Add(int.Parse(skill_pos_value_array[i]));
            }
        }
        item.skill_damage = reader.GetInt32(reader.GetOrdinal("skill_damage"));
        item.skill_power = reader.GetInt32(reader.GetOrdinal("skill_power"));
        item.skill_suit_type = reader.GetInt32(reader.GetOrdinal("skill_suit_type"));
        item.skill_suit_value = reader.GetInt32(reader.GetOrdinal("skill_suit_value"));
        item.skill_spell= reader.GetInt32(reader.GetOrdinal("skill_spell"));
        item.skill_cd = reader.GetInt32(reader.GetOrdinal("skill_cd")) / 60f;
        item.skill_state= reader.GetInt32(reader.GetOrdinal("skill_state"));
        return item;
    } 

}