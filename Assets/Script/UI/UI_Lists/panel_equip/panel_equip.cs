using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
/// <summary>
/// 展示装备的界面
/// </summary>
public class panel_equip : Panel_Base
{

    /// <summary>
    /// 存储当前物品 和穿戴物品
    /// </summary>
    private bag_item crt_bag, crt_equip;
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Initialize()
    {
        base.Initialize();
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
    }
}
