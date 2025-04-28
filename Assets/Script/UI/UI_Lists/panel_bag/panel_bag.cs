using Common;
using Components;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class panel_bag : Panel_Base
{
    /// <summary>
    /// 装备类型
    /// </summary>
    private Dictionary<EquipTypeList, show_equip_item> dic_equips = new Dictionary<EquipTypeList, show_equip_item>();
    /// <summary>
    /// 功能按钮
    /// </summary>
    private btn_item btn_item_Prefabs;

    private bag_item bag_item_Prefabs;

    private material_item material_item_Prefabs;
    /// <summary>
    /// 按钮位置
    /// </summary>
    private Transform crt_btn, crt_bag, pos_function;

    private panel_equip panel_equip;


    /// <summary>
    /// 角色皮肤
    /// </summary>
    private enum_skin_state skin_state;
    /// <summary>
    /// 角色皮肤预制体
    /// </summary>
    private GameObject skin_prefabs;
    /// <summary>
    /// 角色内观位置
    /// </summary>
    private Transform panel_role_health;

    private bag_btn_list crt_select_btn = bag_btn_list.装备;
    /// <summary>
    /// 功能列表
    /// </summary>
    private string[] function_list =new string[] { "一键分解","一键出售"};
    /// <summary>
    /// 显示页面
    /// </summary>
    private Text page_info;
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Initialize()
    {
        base.Initialize();
        crt_btn = Find<Transform>("bg_main/show_bag/btns");
        crt_bag = Find<Transform>("bg_main/show_bag/Scroll View/Viewport/Content");
        //btn_item_Prefabs = Resources.Load<btn_item>("Prefabs/base_tool/btn_item");
        btn_item_Prefabs = Battle_Tool.Find_Prefabs<btn_item>("btn_item");
        bag_item_Prefabs = Battle_Tool.Find_Prefabs<bag_item>("bag_item");
        material_item_Prefabs = Battle_Tool.Find_Prefabs<material_item>("material_item");
        //material_item_Prefabs= Resources.Load<material_item>("Prefabs/panel_bag/material_item"); 
        panel_equip = UI_Manager.I.GetPanel<panel_equip>();

       
        skin_prefabs = Resources.Load<GameObject>("Prefabs/Skins/内观_" + SumSave.crt_hero.hero_pos);
        panel_role_health = Find<Transform>("bg_main/bag_equips/panel_role_health");
        Instantiate(skin_prefabs, panel_role_health);

        for (int i = 0; i < Enum.GetNames(typeof(bag_btn_list)).Length; i++)
        {
            btn_item btn_item = Instantiate(btn_item_Prefabs, crt_btn);//实例化背包装备
            btn_item.Show(i, (bag_btn_list)i);
            btn_item.GetComponent<Button>().onClick.AddListener(delegate { Select_Btn(btn_item); });
        }

        pos_function=Find<Transform>("bg_main/show_bag/function_list");
        page_info= Find<Text>("bg_main/show_bag/function_list/page_info");
        for (int i = 0; i < function_list.Length; i++)
        { 
            btn_item btn_item = Instantiate(btn_item_Prefabs, pos_function);//实例化背包装备
            btn_item.Show(i, function_list[i]);
            btn_item.GetComponent<Button>().onClick.AddListener(delegate { Select_functionBtn(btn_item); });
        }
    }
    /// <summary>
    /// 一键回收分解
    /// </summary>
    /// <param name="btn_item"></param>
    private void Select_functionBtn(btn_item btn_item)
    {

        switch (function_list[btn_item.index])
        { 
            case "一键分解":
                Alert_Dec.Show("未查询到满足分解条件的物品");
                break;
            case "一键出售":
                SellAll();
                break;
        }
    }
    /// <summary>
    /// 全部出售
    /// </summary>
    private void SellAll()
    {
        int moeny= 0;
        List<Bag_Base_VO> sell_list = new List<Bag_Base_VO>();
        foreach (Bag_Base_VO item in SumSave.crt_bag)
        {
            if (item.user_value != null)
            {
                string[] info_str = item.user_value.Split(' ');
                if (info_str.Length >= 6)
                {
                    if (info_str[6] == "0")
                    {
                        sell_list.Add(item);
                        moeny += item.price;
                    }
                }
                else
                {
                    sell_list.Add(item);
                    moeny += item.price;
                }
            }
        }
        if (sell_list.Count > 0)
        {
            foreach (Bag_Base_VO item in sell_list)
            { 
                SumSave.crt_bag.Remove(item);
            }
        }
        Alert_Dec.Show("出售成功，获得灵珠 " + moeny);
        Game_Omphalos.i.Wirte_ResourcesList(Emun_Resources_List.bag_value, SumSave.crt_bag);
        Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user, SumSave.crt_user_unit.Set_Uptade_String(), SumSave.crt_user_unit.Get_Update_Character());
        SumSave.crt_user_unit.verify_data(currency_unit.灵珠, moeny);
        Show_Bag();
    }

    /// <summary>
    /// 选中功能
    /// </summary>
    /// <param name="btn_item"></param>
    private void Select_Btn(btn_item btn_item)
    {
        crt_select_btn = (bag_btn_list)btn_item.index;
        Show_Bag();
        Alert_Dec.Show("打开 " + crt_select_btn + " 列表");
    }

    /// <summary>
    /// 初始化位置
    /// </summary>
    /// <param name="item"></param>
    protected void Instance_Pos(show_equip_item item)
    {
        if (!dic_equips.ContainsKey(item.type)) dic_equips.Add(item.type, item);
    }

    public override void Show()
    {
        base.Show();
        Show_Bag();
        Base_Show();
        base_Equip();
       
    }
    /// <summary>
    /// 显示装备
    /// </summary>
    private void base_Equip()
    {
        foreach (EquipTypeList item in dic_equips.Keys)
        {
            dic_equips[item].Init();
            foreach (Bag_Base_VO equip in SumSave.crt_euqip)
            {
                if (equip.StdMode == item.ToString())
                {
                    
                    dic_equips[item].Data = equip;
                }
            }
        }
    }

    /// <summary>
    /// 显示穿戴装备列表
    /// </summary>
    private void Base_Show()
    {
        foreach (var item in dic_equips.Keys)
        {
            dic_equips[item].Init();

            foreach (Bag_Base_VO equip in SumSave.crt_euqip)
            {
                if (equip.StdMode == item.ToString())
                {
                    dic_equips[item].Data = equip;
                }
            }
            
        }
    }
    /// <summary>
    /// 显示背包物品
    /// </summary>
    private void Show_Bag()
    {
        ClearObject(crt_bag);
        switch (crt_select_btn)
        {
            case bag_btn_list.装备:
                for (int i = 0; i < SumSave.crt_bag.Count; i++)
                {
                    bag_item item = Instantiate(bag_item_Prefabs, crt_bag);
                    item.Data = SumSave.crt_bag[i];
                    item.GetComponent<Button>().onClick.AddListener(delegate { Select_Bag(item); });
                }
                page_info.text= SumSave.crt_bag.Count+"/"+ SumSave.crt_resources.pages[0];
                break;
            case bag_btn_list.材料:
                List<(string, int)> lists = SumSave.crt_bag_resources.Set();
                for (int i = 0; i < lists.Count; i++)
                {
                    (string,int) data = lists[i];
                    if (data.Item2 > 0)
                    {
                        material_item item = Instantiate(material_item_Prefabs, crt_bag);
                        item.Init(data);
                        item.GetComponent<Button>().onClick.AddListener(delegate { Select_Material(item); });
                    }
                }
                page_info.text = lists.Count + "/" + SumSave.crt_resources.pages[0];

                break;   
               case bag_btn_list.丹囊:
                List<(string, List<string>)> list = SumSave.crt_seeds.Getformulalist();
                for (int i = 0; i < list.Count; i++)
                {
                    material_item item = Instantiate(material_item_Prefabs, crt_bag);
                    item.Init(list[i]);
                    item.GetComponent<Button>().onClick.AddListener(delegate { Select_Getformula_Material(item); });
                }
                page_info.text = list.Count + "/" + SumSave.crt_resources.pages[1];

                break;
            case bag_btn_list.消耗品:
                List<(string, List<string>)> Seedlist = SumSave.crt_seeds.GetSeedList();
                for (int i = 0; i < Seedlist.Count; i++)
                {
                    material_item item = Instantiate(material_item_Prefabs, crt_bag);
                    item.Init(Seedlist[i]);
                    item.GetComponent<Button>().onClick.AddListener(delegate { Select_SeedMaterial(item); });
                }
                page_info.text = Seedlist.Count + "/" + SumSave.crt_resources.pages[1];
                break;
            default:
                break;
        }
        
    }
    /// <summary>
    /// 吃丹药
    /// </summary>
    /// <param name="item"></param>
    private void Select_SeedMaterial(material_item item)
    {
        panel_equip.Show();
        panel_equip.Use_Seed(item);
    }
    /// <summary>
    /// 选择合成材料
    /// </summary>
    /// <param name="item"></param>
    private void Select_Getformula_Material(material_item item)
    {
        panel_equip.Show();
        panel_equip.Select_Seed(item);
    }

    /// <summary>
    /// 选择材料
    /// </summary>
    /// <param name="item"></param>
    private void Select_Material(material_item item)
    {
        panel_equip.Show();
        panel_equip.Select_Material(item);
    }

    /// <summary>
    /// 选择物品
    /// </summary>
    /// <param name="item"></param>
    private void Select_Bag(bag_item item)
    {
        panel_equip.Show();
        panel_equip.Init(item);
       
    }
    /// <summary>
    /// 显示自身装备
    /// </summary>
    /// <param name="item"></param>
    protected void Select_Equip(bag_item item)
    {
        panel_equip.Show();
        panel_equip.Select_Equip(item);

    }
}
