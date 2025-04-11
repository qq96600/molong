using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVC;
using Common;
using System;
using System.Security.Cryptography;
/// <summary>
/// ս��������
/// </summary>
public static class Battle_Tool 
{
    /// <summary>
    /// ��Դ�洢��
    /// </summary>
    //private static List<(string, int)> resources_list = new List<(string, int)>();
    /// <summary>
    /// ��ȡ��Դ
    /// </summary>
    /// <param name="resources_name">����</param>
    /// <param name="number">����</param>
    public static void Obtain_Resources( object resources_name,int number)
    { 
        Dictionary<string, int> dic = new Dictionary<string, int>();
        dic.Add(resources_name.ToString(), number);
        SumSave.crt_bag_resources.Get(dic);
        //д�����ݿ�
        Game_Omphalos.i.Wirte_ResourcesList(Emun_Resources_List.material_value, SumSave.crt_bag_resources.GetData());
    }
    /// <summary>
    /// �������
    /// </summary>
    /// <param name="crt"></param>
    /// <param name="lv">1С��2��Ӣ3boss</param>
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
        Array values = Enum.GetValues(typeof(enum_monster_state));
        enum_monster_state state = (enum_monster_state)values.GetValue(RandomNumberGenerator.GetInt32(values.Length));
        switch (state)
        {
            case enum_monster_state.������:
                break;
            case enum_monster_state.ǿ׳��:
                base_crt.MaxHP = (int)(crt.MaxHP * 1.5f);
                break;
            case enum_monster_state.���ҵ�:
                break;
            case enum_monster_state.�־��:
                base_crt.attack_speed= (int)(crt.attack_speed / 2);
                break;
            case enum_monster_state.��Ⱦ��:
                break;
            case enum_monster_state.��˯��:
                base_crt.Heal_Hp = (int)(crt.Heal_Hp * 1.5f);
                break;
            case enum_monster_state.��Ĭ��:
                base_crt.DefMax = (int)(crt.DefMax * 1.5f);
                base_crt.MagicDefMax = (int)(crt.MagicDefMax * 1.5f);
                break;
            case enum_monster_state.���ص�:
                base_crt.attack_distance = (int)(crt.attack_distance * 1.5f);
                break;
            case enum_monster_state.�ֲ���:
                base_crt.damageMax= (int)(crt.damageMax * 1.5f);
                base_crt.MagicdamageMax = (int)(crt.MagicdamageMax * 1.5f);
                break;
            case enum_monster_state.��ŭ��:
                base_crt.crit_rate= (int)(crt.crit_rate * 1.5f);
                break;
            default:
                break;
        }
        base_crt.monster_attrList.Add((int)state);
        return base_crt;

    }
    /// <summary>
    /// ��֤��ͼ�б�
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
                            Debug.Log("������ " + SumSave.db_maps[i].map_name + " " + values[j]);
                        else
                        {
                            Bag_Base_VO bag = ArrayHelper.Find(SumSave.db_stditems, e => e.Name == values1[0]);
                            if (bag == null) Debug.Log("���Ӵ��� �����ݿ��������" + SumSave.db_maps[i].map_name + " " + values[j]);
                        }
                    }
                    else Debug.Log(SumSave.db_maps[i].map_name + " " + values[j]);
                }
            }
        }
    }


}

