using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVC;
using Common;
using System;
using System.Security.Cryptography;
using Random = UnityEngine.Random;
using Components;
/// <summary>
/// 战斗工具类
/// </summary>
public static class Battle_Tool 
{
    /// <summary>
    /// 资源存储器
    /// </summary>
    //private static List<(string, int)> resources_list = new List<(string, int)>();
    /// <summary>
    /// 获取资源
    /// </summary>
    /// <param name="resources_name">名称</param>
    /// <param name="number">数量</param>
    public static void Obtain_Resources( object resources_name,int number)
    { 
        Dictionary<string, int> dic = new Dictionary<string, int>();
        dic.Add(resources_name.ToString(), number);
        SumSave.crt_bag_resources.Get(dic);
        //写入数据库
        Game_Omphalos.i.Wirte_ResourcesList(Emun_Resources_List.material_value, SumSave.crt_bag_resources.GetData());
    }
    /// <summary>
    /// 获取资源
    /// </summary>
    /// <param name="result"></param>
    public static void Obtain_result(string result)
    {
        if (result == "0") return;
        string[] result_list = result.Split('*');
        switch (int.Parse(result_list[2]))
        {
            case 1://获取资源
                Obtain_Resources(result_list[0], int.Parse(result_list[1]));
                break;
            case 2:
                Obtain_Resources(result_list[0], int.Parse(result_list[1]));
                break;
            case 3:
                SumSave.crt_user_unit.verify_data(currency_unit.灵珠, int.Parse(result_list[1]));
                break;

            case 4:
                SumSave.crt_user_unit.verify_data(currency_unit.魔丸, int.Parse(result_list[1]));
                break;

            case 5:
              
                break;
            case 6:
                if (SumSave.crt_world != null)
                {
                    SumSave.crt_world.Set(int.Parse(result_list[1]));
                    Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_world, SumSave.crt_world.Set_Uptade_String(), SumSave.crt_world.Get_Update_Character());
                }
                else Alert_Dec.Show("小世界未激活");
                break;
            case 7:
       
                break;
            case 8:
                break;
            default:
                break;
        }
    }

    public static void tool_item()
    {
        return;
        foreach (var item in SumSave.db_stditems)
        {
            UI.UI_Manager.I.GetEquipSprite("icon/", item.Name);
        }
    }
    /// <summary>
    /// 显示货币单位
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static string FormatNumberToChineseUnit(long number, int decimalPlaces = 2)
    {
        string format = "0." + new string('#', decimalPlaces);

        if (number < 0)
        {
            return "-" + FormatNumberToChineseUnit(-number, decimalPlaces);
        }

        if (number >= 100000000)
        {
            return (number / 100000000.0).ToString(format) + "亿";
        }
        else if (number >= 10000)
        {
            return (number / 10000.0).ToString(format) + "万";
        }
        else
        {
            return number.ToString();
        }
    }
    /// <summary>
    /// 刷新排行榜
    /// </summary>
    private static void Refresh_Rank()
    {
        Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.user_rank, SumSave.user_ranks.Set_Uptade_String(), SumSave.user_ranks.Get_Update_Character());
    }

    /// <summary>
    /// 创建排行榜
    /// </summary>
    private static  void crate_rank()
    {
        base_rank_vo rank = new base_rank_vo();

        rank.uid = SumSave.crt_user.uid;
        rank.type = SumSave.crt_hero.hero_type;
        rank.name = SumSave.crt_hero.hero_name;
        rank.lv = SumSave.crt_MaxHero.Lv;
        rank.ranking_index = 1;
        rank.value = (int)SumSave.crt_MaxHero.totalPower;
        SumSave.user_ranks.lists.Add(rank);
        //排序
        Refresh_Rank();
    }
    /// <summary>
    /// 验证排行榜
    /// </summary>
    public static void validate_rank()
    {
        bool exist = false;
        if (SumSave.user_ranks.lists.Count < 50)
        {
            for (int i = 0; i < SumSave.user_ranks.lists.Count; i++)
            {
                if (SumSave.user_ranks.lists[i].uid == SumSave.crt_user.uid)
                {
                    exist = true;
                    SumSave.user_ranks.lists[i].value = (int)SumSave.crt_MaxHero.totalPower;
                    SumSave.user_ranks.lists[i].lv = SumSave.crt_MaxHero.Lv;
                    SumSave.user_ranks.lists[i].type = SumSave.crt_hero.hero_type;// SumSave.crtHeroMaxs[0].Type;  
                    Refresh_Rank();
                    if (SumSave.crt_MaxHero.totalPower < SumSave.user_ranks.lists[i].value)//小于的情况 写入排行榜战力 且替换排行榜战力
                    {
                        Game_Omphalos.i.Alert_Info($"你的战力降低了{"原战斗力" + SumSave.user_ranks.lists[i].value + " 当前" + (int)SumSave.crt_MaxHero.totalPower}");
                    }
                }
            }
            //存在刷新 不在添加
            if (exist)
            {
                Refresh_Rank();
            }
            else crate_rank();
        }
        else if (SumSave.user_ranks.lists.Count > 50 && 
        SumSave.crt_MaxHero.totalPower > SumSave.user_ranks.lists[SumSave.user_ranks.lists.Count - 1].value) //50个榜已满,且自身战力大于榜上最低的一名
        {
            for (int i = SumSave.user_ranks.lists.Count - 2; i >= 0; i--)//此前已经满足 第50名条件，直接从49名开始往上遍历
            {
                if (SumSave.crt_MaxHero.totalPower > SumSave.user_ranks.lists[i].value) //逐个遍历,继续往上
                {
                    if (i == 0)
                    {
                        for (int j = SumSave.user_ranks.lists.Count - 1; j > 0; j++) //往后依次替换,该情况仅适用于第一名
                        {
                            SumSave.user_ranks.lists[j].value = SumSave.user_ranks.lists[j - 1].value;
                            SumSave.user_ranks.lists[j].lv = SumSave.user_ranks.lists[j - 1].lv;
                            SumSave.user_ranks.lists[j].uid = SumSave.user_ranks.lists[j - 1].uid;
                            SumSave.user_ranks.lists[j].name = SumSave.user_ranks.lists[j - 1].name;
                            SumSave.user_ranks.lists[j].type = SumSave.user_ranks.lists[j - 1].type;
                        }
                        SumSave.user_ranks.lists[0].value = (int)SumSave.crt_MaxHero.totalPower;
                        SumSave.user_ranks.lists[0].lv = SumSave.crt_MaxHero.Lv;
                        SumSave.user_ranks.lists[0].uid = SumSave.crt_user.uid;
                        SumSave.user_ranks.lists[0].name = SumSave.crt_MaxHero.show_name;
                        SumSave.user_ranks.lists[0].type = SumSave.crt_hero.hero_type;
                        Refresh_Rank();
                        break;
                    }//替换第一名
                    else continue;
                }
                else
                {
                    if (i != SumSave.user_ranks.lists.Count - 2) //如果说在倒数第二个结束，就直接替换倒数第一个，不需要进循环
                    {
                        for (int j = SumSave.user_ranks.lists.Count - 1; j > i + 1; j++) //往后依次替换，这个时候已经比i小，不需要取到i
                        {
                            SumSave.user_ranks.lists[j].value = SumSave.user_ranks.lists[j - 1].value;
                            SumSave.user_ranks.lists[j].lv = SumSave.user_ranks.lists[j - 1].lv;
                            SumSave.user_ranks.lists[j].uid = SumSave.user_ranks.lists[j - 1].uid;
                            SumSave.user_ranks.lists[j].name = SumSave.user_ranks.lists[j - 1].name;
                            SumSave.user_ranks.lists[j].type = SumSave.user_ranks.lists[j - 1].type;
                        }
                    }
                    // 47   i  = cou - 4     j50  49 -1    49-48  -2
                    SumSave.user_ranks.lists[i + 1].value = (int)SumSave.crt_MaxHero.totalPower;
                    SumSave.user_ranks.lists[i + 1].lv = SumSave.crt_MaxHero.Lv;
                    SumSave.user_ranks.lists[i + 1].uid = SumSave.crt_user.uid;
                    SumSave.user_ranks.lists[i + 1].name = SumSave.crt_MaxHero.show_name;
                    SumSave.user_ranks.lists[i + 1].type = SumSave.crt_hero.hero_type;
                    Refresh_Rank();
                    break;  //比到战力更高的，就结束，替换直到这之前的前一位  这时已留出i之前的空位
                }
            }
        }
        SumSave.user_ranks.lists.Sort((x, y) => -x.value.CompareTo(y.value));
    }
    /// <summary>
    /// 创造怪物
    /// </summary>
    /// <param name="crt"></param>
    /// <param name="lv">1小怪2精英3boss</param>
    public static crtMaxHeroVO crate_monster(crtMaxHeroVO crt, int lv = 1)
    {
        crtMaxHeroVO base_crt = new crtMaxHeroVO();
        base_crt.show_name = crt.show_name;
        base_crt.index = crt.index;
        base_crt.Lv = crt.Lv;
        base_crt.Exp = crt.Exp;
        base_crt.icon = crt.icon;
        base_crt.MaxHP = crt.MaxHP;
        base_crt.MaxMp = crt.MaxMp;
        base_crt.internalforceMP = crt.internalforceMP;
        base_crt.EnergyMp = crt.EnergyMp;
        base_crt.DefMin = crt.DefMin;
        base_crt.DefMax = crt.DefMax;
        base_crt.MagicDefMin = crt.MagicDefMin;
        base_crt.MagicDefMax = crt.MagicDefMax;
        base_crt.damageMin = crt.damageMin;
        base_crt.damageMax = crt.damageMax;
        base_crt.MagicdamageMin = crt.MagicdamageMin;
        base_crt.MagicdamageMax = crt.MagicdamageMax;
        base_crt.hit = crt.hit;
        base_crt.dodge = crt.dodge;
        base_crt.penetrate = crt.penetrate;
        base_crt.block = crt.block;
        base_crt.crit_rate = crt.crit_rate;
        base_crt.crit_damage = crt.crit_damage;
        base_crt.double_damage = crt.double_damage;
        base_crt.Lucky = crt.Lucky;
        base_crt.Real_harm = crt.Real_harm;
        base_crt.Damage_Reduction = crt.Damage_Reduction;
        base_crt.Damage_absorption = crt.Damage_absorption;
        base_crt.resistance = crt.resistance;
        base_crt.move_speed = crt.move_speed;
        base_crt.attack_speed = crt.attack_speed;
        base_crt.attack_distance = crt.attack_distance;
        base_crt.bonus_Hp = crt.bonus_Hp;
        base_crt.bonus_Mp = crt.bonus_Mp;
        base_crt.bonus_Damage = crt.bonus_Damage;
        base_crt.bonus_MagicDamage = crt.bonus_MagicDamage;
        base_crt.bonus_Def = crt.bonus_Def;
        base_crt.bonus_MagicDef = crt.bonus_MagicDef;
        base_crt.Heal_Hp = crt.Heal_Hp;
        base_crt.Heal_Mp = crt.Heal_Mp;
        base_crt.unit = Random.Range(crt.Lv * 5, crt.Lv * 10) + 1;
        Array values = Enum.GetValues(typeof(enum_monster_state));
        enum_monster_state state = (enum_monster_state)values.GetValue(RandomNumberGenerator.GetInt32(values.Length));
        switch (state)
        {
            case enum_monster_state.正常的:
                break;
            case enum_monster_state.强壮的:
                base_crt.MaxHP = (int)(crt.MaxHP * 1.5f);
                break;
            case enum_monster_state.混乱的:
                break;
            case enum_monster_state.恐惧的:
                base_crt.attack_speed= (int)(crt.attack_speed / 2);
                break;
            case enum_monster_state.感染的:
                break;
            case enum_monster_state.沉睡的:
                base_crt.Heal_Hp = (int)(crt.Heal_Hp * 1.5f);
                break;
            case enum_monster_state.沉默的:
                base_crt.DefMax = (int)(crt.DefMax * 1.5f);
                base_crt.MagicDefMax = (int)(crt.MagicDefMax * 1.5f);
                break;
            case enum_monster_state.神秘的:
                base_crt.attack_distance = (int)(crt.attack_distance * 1.5f);
                break;
            case enum_monster_state.恐怖的:
                base_crt.damageMax= (int)(crt.damageMax * 1.5f);
                base_crt.MagicdamageMax = (int)(crt.MagicdamageMax * 1.5f);
                break;
            case enum_monster_state.激怒的:
                base_crt.crit_rate= (int)(crt.crit_rate * 1.5f);
                break;
            default:
                break;
        }
        base_crt.monster_attrList.Add((int)state);
        return base_crt;

    }
    /// <summary>
    /// 验证地图列表
    /// </summary>
    public static void tool_map()
    {
        for (int i = 0; i < SumSave.db_maps.Count; i++)
        {
            string value= SumSave.db_maps[i].ProfitList;
            string[] values = value.Split('&');
            if (values.Length > 1)
            {
                for (int j = 0; j < values.Length; j++)
                {
                    string[] values1 = values[j].Split(' ');
                    if (values1.Length == 3)
                    {
                        if (values1[0] != values1[2])
                            Debug.Log("配表错误 " + SumSave.db_maps[i].map_name + " " + values[j]);
                        else
                        {
                            Bag_Base_VO bag = ArrayHelper.Find(SumSave.db_stditems, e => e.Name == values1[0]);
                            if (bag == null) Debug.Log("连接错误 与数据库关联错误" + SumSave.db_maps[i].map_name + " " + values[j]);//对应的2个表格对不上
                        }
                    }
                    else Debug.Log(SumSave.db_maps[i].map_name + " " + values[j]);
                }
            }
        }
    }


}

