using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class panel_map : Panel_Base
{
    /// <summary>
    /// 掉落列表
    /// </summary>
    private Transform pos_life;
    private btn_item btn_item_prefab;
    /// <summary>
    /// 地图名称
    /// </summary>
    private Text map_name;
    /// <summary>
    /// 等级要求
    /// </summary>
    private Text need_lv;
    /// <summary>
    /// 怪物列表
    /// </summary>
    private Text monster_list;
    /// <summary>
    /// 门票要求
    /// </summary>
    private Text need_Required;
    /// <summary>
    /// 地图列表
    /// </summary>
    private Dictionary<map_pos_item, user_map_vo> maplists = new Dictionary<map_pos_item, user_map_vo>();

    /// <summary>
    /// 战斗地图
    /// </summary>
    private panel_fight fight_panel;
    /// <summary>
    /// 当前选择地图
    /// </summary>
    private map_pos_item crt_map;
    /// <summary>
    /// 进入地图按钮
    /// </summary>
    private Button enter_map_button;
    private Transform base_show_info;
    private material_item material_item_parfabs;

    protected override void Awake()
    {
        base.Awake();
        map_name= Find<Text>("bg_main/base_info/map_name");
        need_lv = Find<Text>("bg_main/base_info/need_lv");
        monster_list = Find<Text>("bg_main/base_info/monster_list");
        need_Required = Find<Text>("bg_main/base_info/need_Required");
        pos_life = Find<Transform>("bg_main/base_info/ProfitList/Scroll View/Viewport/Content");
        enter_map_button = Find<Button>("bg_main/base_info/enter_map_button");
        enter_map_button.onClick.AddListener(Open_Map);
        fight_panel = UI_Manager.I.GetPanel<panel_fight>();
        base_show_info = Find<Transform>("bg_main/base_info");
        material_item_parfabs = Resources.Load<material_item>("Prefabs/panel_bag/material_item");
    }


    public override void Initialize()
    {
        base.Initialize();

        btn_item_prefab = Resources.Load<btn_item>("Prefabs/base_tool/btn_item");
       
    }

    protected void Instance_Pos(map_pos_item item)
    {

        if (!maplists.ContainsKey(item))
        {
            foreach (var map in SumSave.db_maps)
            {
                if (map.map_index == item.index)
                { 
                    maplists.Add(item, map);
                    btn_item btn = Instantiate(btn_item_prefab, item.transform);
                    btn.Show(item.index, map.map_name);
                    btn.GetComponent<Button>().onClick.AddListener(delegate { Select_Map(item); });
                }
            }
        }
    }
    /// <summary>
    /// 选择地图
    /// </summary>
    /// <param name="item"></param>
    private void Select_Map(map_pos_item item)
    {
        crt_map = item;
        base_show_info.gameObject.SetActive(true);
        user_map_vo map = maplists[item];
        map_name.text = map.map_name;
        need_lv.text = "等级要求： "+ map.need_lv.ToString();
        monster_list.text = "怪物列表： "+map.monster_list.ToString();
        need_Required.text = "门票要求： "+map.need_Required.ToString();
        for (int i = pos_life.childCount - 1; i >= 0; i--)//清空区域内按钮
        {
            Destroy(pos_life.GetChild(i).gameObject);
        }
        foreach (string str in map.ProfitList.Split('&'))
        {
            str.Split(' ');
            string[] str1 = str.Split(' ');
            Instantiate(material_item_parfabs, pos_life).Init(((str1[0]), 0));
        }
    }

    public override void Hide()
    {
        if (base_show_info.gameObject.activeInHierarchy)
        {
            base_show_info.gameObject.SetActive(false);
        }
        else
            base.Hide();
    }

    public override void Show()
    {
        base.Show();
        base_show_info.gameObject.SetActive(false);
    }

    private void Open_Map()
    { 
        if(crt_map==null)
            return;
        //打开地图
        Debug.Log("打开地图");
        fight_panel.Open_Map(maplists[crt_map]);
        Hide();
    }
}
