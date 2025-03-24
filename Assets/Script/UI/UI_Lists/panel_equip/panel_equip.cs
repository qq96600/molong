using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
/// <summary>
/// 展示装备的界面
/// </summary>
public class panel_equip : Panel_Base
{

    private equip_item equip_item_prafabs;
    /// <summary>
    /// 存储当前物品 和穿戴物品
    /// </summary>
    private bag_item crt_bag, crt_equip;

    private Transform crt_pos_equip;
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Initialize()
    {
        base.Initialize();
        equip_item_prafabs = Resources.Load<equip_item>("Prefabs/panel_equip/equip_item"); 
        crt_pos_equip = Find<Transform>("bg_main");

    }

    public override void Show()
    {
        base.Show();
    }
    /// <summary>
    /// 传递参数
    /// </summary>
    /// <param name="bag"></param>
    public void Init(bag_item bag)
    { 
        crt_bag = bag;
        equip_item item = Instantiate(equip_item_prafabs, crt_pos_equip);
        item.Data = bag.Data;

    }
}
