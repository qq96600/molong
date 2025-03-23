using System;
using System.Data;
using MVC;
using MySql.Data.MySqlClient;
using UnityEngine;

/// <summary>
/// 读取数据
/// </summary>
public static class ReadDb
{
    public static user_base_vo Read(MySqlDataReader reader, user_base_vo item)
    {
        item.uid = reader.GetString(reader.GetOrdinal("uid"));
        item.RegisterDate = Convert.ToDateTime(reader.GetString(reader.GetOrdinal("RegisterDate")));
        item.Nowdate = Convert.ToDateTime(reader.GetString(reader.GetOrdinal("Nowdate")));
        item.par = reader.GetInt32(reader.GetOrdinal("par"));
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
            Debug.Log(up_value_array[i]);
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
        item.skill_need_exp = reader.GetInt32(reader.GetOrdinal("skill_need_exp"));
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
        if (skill_pos_type_array.Length > 0)
        {
            for (int i = 0; i < skill_pos_type_array.Length; i++)
            { 
                item.skill_pos_type.Add(int.Parse(skill_pos_type_array[i]));
            }
        }
        string skill_pos_value= reader.GetString(reader.GetOrdinal("skill_pos_value"));
        string[] skill_pos_value_array = skill_pos_value.Split(' ');
        item.skill_pos_value = new System.Collections.Generic.List<int>();
        if (skill_pos_value_array.Length > 0)
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
        item.skill_cd= reader.GetInt32(reader.GetOrdinal("skill_cd"));
        return item;
    } 

}