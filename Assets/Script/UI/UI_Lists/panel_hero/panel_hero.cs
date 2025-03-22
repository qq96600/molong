using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVC;
using UI;
using System;
using Common;

public class panel_hero : Panel_Base
{
    /// <summary>
    /// λ��
    /// </summary>
    private Transform crt; 
    /// <summary>
    /// ��ϢԤ����
    /// </summary>
    public info_item info_item_prefabs;

    private Dictionary<enum_attribute_list, info_item> info_item_dic = new Dictionary<enum_attribute_list, info_item>();
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Initialize()
    {
        base.Initialize();
        crt = Find<Transform>("bg_main/base_info/Scroll View/Viewport/Content");

        for (int i = 0; i < Enum.GetNames(typeof(enum_attribute_list)).Length; i++)
        { 
            info_item item = Instantiate(info_item_prefabs, crt);
            item.Show((enum_attribute_list)i, UnityEngine.Random.Range(1, 1000));
            info_item_dic.Add((enum_attribute_list)i, item);
        }
    }
    public override void Show()
    {
        base.Show();
        base_show();
    }
    /// <summary>
    /// ��ʾ���Ա�
    /// </summary>
    private void base_show()
    {
        SumSave.crt_MaxHero = new crtMaxHeroVO();
        foreach (var item in info_item_dic.Keys)
        {
            switch (item)
            {
                case enum_attribute_list.����ֵ:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.MaxHP);
                    break;
                case enum_attribute_list.����ֵ:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.MaxMp);
                    break;
                case enum_attribute_list.����ֵ:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.internalforceMP);
                    break;
                case enum_attribute_list.����ֵ:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.EnergyMp);
                    break;
                case enum_attribute_list.�������:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.DefMin+" - "+SumSave.crt_MaxHero.DefMax);
                    break;
                case enum_attribute_list.ħ������:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.MagicDefMin + " - " + SumSave.crt_MaxHero.MagicDefMax);
                    break;
                case enum_attribute_list.������:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.damageMin + " - " + SumSave.crt_MaxHero.damageMax);
                    break;
                case enum_attribute_list.ħ������:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.MagicdamageMin + " - " + SumSave.crt_MaxHero.MagicdamageMax);
                    break;
                case enum_attribute_list.����:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.hit);
                    break;
                case enum_attribute_list.���:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.dodge);
                    break;
                case enum_attribute_list.��͸:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.penetrate); 
                    break;
                case enum_attribute_list.��:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.block);
                    break;
                case enum_attribute_list.����:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.crit_rate);
                    break;
                case enum_attribute_list.����:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.Lucky);
                    break;
                case enum_attribute_list.�����˺�:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.crit_damage);
                    break;
                case enum_attribute_list.�˺��ӳ�:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.double_damage);
                    break;
                case enum_attribute_list.��ʵ�˺�:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.Real_harm);
                    break;
                case enum_attribute_list.�˺�����:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.Damage_Reduction);
                    break;
                case enum_attribute_list.�˺�����:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.Damage_absorption);
                    break;
                case enum_attribute_list.�쳣����:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.resistance);
                    break;
                case enum_attribute_list.�����ٶ�:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.attack_speed);
                    break;
                case enum_attribute_list.�ƶ��ٶ�:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.move_speed);
                    break;
                case enum_attribute_list.�����ӳ�:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.bonus_Hp);
                    break;
                case enum_attribute_list.�����ӳ�:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.bonus_Mp);
                    break;
                case enum_attribute_list.�����ظ�:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.Heal_Hp);
                    break;
                case enum_attribute_list.�����ظ�:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.Heal_Mp);
                    break;
                case enum_attribute_list.�﹥�ӳ�: 
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.bonus_Damage);
                    break;
                case enum_attribute_list.ħ���ӳ�:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.bonus_MagicDamage);
                    break;
                case enum_attribute_list.����ӳ�:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.bonus_Def);
                    break;
                case enum_attribute_list.ħ���ӳ�:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.bonus_MagicDef);
                    break;
                default:
                    break;
            }
        }
    }
}
