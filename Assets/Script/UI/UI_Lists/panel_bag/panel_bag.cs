using Common;
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
    /// <summary>
    /// 按钮位置
    /// </summary>
    private Transform crt_btn, crt_bag;

    private panel_equip panel_equip;
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Initialize()
    {
        base.Initialize();
        crt_btn = Find<Transform>("bg_main/show_bag/btns");
        crt_bag = Find<Transform>("bg_main/show_bag/Scroll View/Viewport/Content");
        btn_item_Prefabs = Resources.Load<btn_item>("Prefabs/base_tool/btn_item");
        bag_item_Prefabs = Resources.Load<bag_item>("Prefabs/panel_bag/bag_item");
        panel_equip = UI_Manager.I.GetPanel<panel_equip>();
        for (int i = 0; i < Enum.GetNames(typeof(bag_btn_list)).Length; i++)
        {
            btn_item btn_item = Instantiate(btn_item_Prefabs, crt_btn);//实例化背包装备
            btn_item.Show(i, (bag_btn_list)i);
            btn_item.GetComponent<Button>().onClick.AddListener(delegate { Select_Btn(btn_item); });
        }
    }
    /// <summary>
    /// 选中功能
    /// </summary>
    /// <param name="btn_item"></param>
    private void Select_Btn(btn_item btn_item)
    {
        Debug.Log("选择" + (bag_btn_list)btn_item.index);
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
       
    }
    /// <summary>
    /// 显示装备列表
    /// </summary>
    private void Base_Show()
    {
        foreach (var item in dic_equips.Keys)
        {
            dic_equips[item].Init();
            dic_equips[item].Data = new MVC.Bag_Base_VO();
        }
    }

    private void Show_Bag()
    {
        for (int i = crt_bag.childCount - 1; i >= 0; i--)
        {
            Destroy(crt_bag.GetChild(i).gameObject);
        }
        for (int i = 0; i < 5; i++)
        {
            SumSave.crt_bag.Add(tool_Categoryt.crate_equip(SumSave.db_stditems[Random.Range(0, SumSave.db_stditems.Count)].Name));
           
        }
        for (int i = 0; i < SumSave.crt_bag.Count; i++)
        {
            bag_item item= Instantiate(bag_item_Prefabs, crt_bag);
            item.Data = SumSave.crt_bag[i];
            item.GetComponent<Button>().onClick.AddListener(delegate { Select_Bag(item); });
        }
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
}
