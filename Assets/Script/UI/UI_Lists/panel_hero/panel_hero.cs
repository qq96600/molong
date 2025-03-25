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
    /// λ��
    /// </summary>
    private Transform crt,crt_pos_hero; 
    /// <summary>
    /// ��ϢԤ����
    /// </summary>
    public info_item info_item_prefabs;
    /// <summary>
    /// ְҵԤ�Ƽ�
    /// </summary>
    private hero_item hero_item_prefabs;
    /// <summary>
    /// �����л���ɫ
    /// </summary>
    private Button open_activation;
    /// <summary>
    /// ѡ��ְҵ
    /// </summary>
    private hero_item crt_hero;
    /// <summary>
    /// ѡ���ɫ���
    /// </summary>
    private Image select_hero;
    /// <summary>
    /// ѡ���ɫ���رհ�ť
    /// </summary>
    private Button select_hero_close_btn;

    private Dictionary<enum_attribute_list, info_item> info_item_dic = new Dictionary<enum_attribute_list, info_item>();

    private Dictionary<string,hero_item> hero_item_dic = new Dictionary<string, hero_item>();
    /// <summary>
    /// ��ʾӢ�����
    /// </summary>
    private Image show_hero_info;
    /// <summary>
    /// ��ʾӢ����Ϣ
    /// </summary>
    private Text hero_info, hero_name;
    /// <summary>
    /// Ӣ���б�
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
    /// ѡ���ɫ
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
        string dec = "�˺�����:" + (item.Data.hero_type == 1 ? "�����˺�" : "ħ���˺�") + "\n";
        for (int i = 0; i < item.Data.crate_value.Length; i++)
        {
            dec += (enum_attribute_list)i + ":" + item.Data.crate_value[i] + "(�ɳ� " + item.Data.up_value[i] + "/" + item.Data.up_base_value[i] + "��)\n";
        }
        hero_info.text = dec;
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
