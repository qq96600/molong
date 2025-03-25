using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVC;
using UI;
using System;
using Common;
using UnityEngine.UI;

public class panel_hero : Panel_Base
{
    /// <summary>
    /// 位置
    /// </summary>
    private Transform crt,crt_pos_hero; 
    /// <summary>
    /// 信息预制体
    /// </summary>
    public info_item info_item_prefabs;
    /// <summary>
    /// 职业预制件
    /// </summary>
    private hero_item hero_item_prefabs;
    /// <summary>
    /// 开启切换角色
    /// </summary>
    private Button open_activation;
    /// <summary>
    /// 选择职业
    /// </summary>
    private hero_item crt_hero;
    /// <summary>
    /// 选择角色面板
    /// </summary>
    private Image select_hero;
    /// <summary>
    /// 选择角色面板关闭按钮
    /// </summary>
    private Button select_hero_close_btn;

    private Dictionary<enum_attribute_list, info_item> info_item_dic = new Dictionary<enum_attribute_list, info_item>();

    private Dictionary<string,hero_item> hero_item_dic = new Dictionary<string, hero_item>();
    /// <summary>
    /// 显示英雄面板
    /// </summary>
    private Image show_hero_info;
    /// <summary>
    /// 显示英雄信息
    /// </summary>
    private Text hero_info, hero_name;
    /// <summary>
    /// 英雄列表
    /// </summary>
    private Button crate_btn,show_hero_info_close_btn;
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Initialize()
    {
        base.Initialize();
        crt = Find<Transform>("bg_main/base_info/Scroll View/Viewport/Content");
        crt_pos_hero = Find<Transform>("bg_main/select_hero/Scroll View/Viewport/Content");
        open_activation = Find<Button>("bg_main/hero_icon/open_activation");
        open_activation.onClick.AddListener(() => { select_hero.gameObject.SetActive(true); });
        hero_item_prefabs = Resources.Load<hero_item>("Prefabs/panel_hero/hero_item");
        select_hero=Find<Image>("bg_main/select_hero");
        select_hero_close_btn = Find<Button>("bg_main/select_hero/close");
        select_hero_close_btn.onClick.AddListener(() => { select_hero.gameObject.SetActive(false); });
        show_hero_info=Find<Image>("bg_main/select_hero/show_hero_info");
        hero_info =Find<Text>("bg_main/select_hero/show_hero_info/Scroll View/Viewport/info");
        hero_name=Find<Text>("bg_main/select_hero/show_hero_info/hero_name/info");
        show_hero_info_close_btn = Find<Button>("bg_main/select_hero/show_hero_info/close");
        show_hero_info_close_btn.onClick.AddListener(() => { show_hero_info.gameObject.SetActive(false); });
        for (int i = 0; i < Enum.GetNames(typeof(enum_attribute_list)).Length; i++)
        { 
            info_item item = Instantiate(info_item_prefabs, crt);
            item.Show((enum_attribute_list)i, UnityEngine.Random.Range(1, 1000));
            info_item_dic.Add((enum_attribute_list)i, item);
        }
        for (int i = 0; i < SumSave.db_heros.Count; i++)
        {
            hero_item item = Instantiate(hero_item_prefabs, crt_pos_hero);
            item.Data = SumSave.db_heros[i];
            item.GetComponent<Button>().onClick.AddListener(delegate { Select_Hero(item); });
            if(!hero_item_dic.ContainsKey(SumSave.db_heros[i].hero_name))
                hero_item_dic.Add(SumSave.db_heros[i].hero_name, item);
        }
        select_hero.gameObject.SetActive(false);
    }
    /// <summary>
    /// 选择角色
    /// </summary>
    /// <param name="item"></param>
    private void Select_Hero(hero_item item)
    {
        crt_hero = item;
        show_hero_info.gameObject.SetActive(true);
        Show_Hero_Type_Attribute(item);

    }
    private void Show_Hero_Type_Attribute(hero_item item)
    {
        hero_name.text= item.Data.hero_name;
        string dec = "伤害类型:" + (item.Data.hero_type == 1 ? "物理伤害" : "魔法伤害") + "\n";
        for (int i = 0; i < item.Data.crate_value.Length; i++)
        {
            dec += (enum_attribute_list)i + ":" + item.Data.crate_value[i] + "(成长 " + item.Data.up_value[i] + "/" + item.Data.up_base_value[i] + "级)\n";
        }
        hero_info.text = dec;
    }
    public override void Show()
    {
        base.Show();
        base_show();
    }
    /// <summary>
    /// 显示属性表
    /// </summary>
    private void base_show()
    {
        foreach (var item in info_item_dic.Keys)
        {
            switch (item)
            {
                case enum_attribute_list.生命值:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.MaxHP);
                    break;
                case enum_attribute_list.法力值:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.MaxMp);
                    break;
                case enum_attribute_list.内力值:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.internalforceMP);
                    break;
                case enum_attribute_list.蓄力值:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.EnergyMp);
                    break;
                case enum_attribute_list.物理防御:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.DefMin+" - "+SumSave.crt_MaxHero.DefMax);
                    break;
                case enum_attribute_list.魔法防御:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.MagicDefMin + " - " + SumSave.crt_MaxHero.MagicDefMax);
                    break;
                case enum_attribute_list.物理攻击:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.damageMin + " - " + SumSave.crt_MaxHero.damageMax);
                    break;
                case enum_attribute_list.魔法攻击:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.MagicdamageMin + " - " + SumSave.crt_MaxHero.MagicdamageMax);
                    break;
                case enum_attribute_list.命中:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.hit);
                    break;
                case enum_attribute_list.躲避:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.dodge);
                    break;
                case enum_attribute_list.穿透:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.penetrate); 
                    break;
                case enum_attribute_list.格挡:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.block);
                    break;
                case enum_attribute_list.暴击:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.crit_rate);
                    break;
                case enum_attribute_list.幸运:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.Lucky);
                    break;
                case enum_attribute_list.暴击伤害:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.crit_damage);
                    break;
                case enum_attribute_list.伤害加成:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.double_damage);
                    break;
                case enum_attribute_list.真实伤害:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.Real_harm);
                    break;
                case enum_attribute_list.伤害减免:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.Damage_Reduction);
                    break;
                case enum_attribute_list.伤害吸收:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.Damage_absorption);
                    break;
                case enum_attribute_list.异常抗性:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.resistance);
                    break;
                case enum_attribute_list.攻击速度:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.attack_speed);
                    break;
                case enum_attribute_list.移动速度:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.move_speed);
                    break;
                case enum_attribute_list.生命加成:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.bonus_Hp);
                    break;
                case enum_attribute_list.法力加成:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.bonus_Mp);
                    break;
                case enum_attribute_list.生命回复:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.Heal_Hp);
                    break;
                case enum_attribute_list.法力回复:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.Heal_Mp);
                    break;
                case enum_attribute_list.物攻加成: 
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.bonus_Damage);
                    break;
                case enum_attribute_list.魔攻加成:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.bonus_MagicDamage);
                    break;
                case enum_attribute_list.物防加成:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.bonus_Def);
                    break;
                case enum_attribute_list.魔防加成:
                    info_item_dic[item].Show(item, SumSave.crt_MaxHero.bonus_MagicDef);
                    break;
                default:
                    break;
            }
        }
    }
}
