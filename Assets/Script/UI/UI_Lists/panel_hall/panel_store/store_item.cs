using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class store_item : Base_Mono
{
    /// <summary>
    /// 购买
    /// </summary>
    private Button buy;
    /// <summary>
    /// 显示信息
    /// </summary>
    private Text baseinfo;
    /// <summary>
    /// 显示图标
    /// </summary>
    private Image icon;
    /// <summary>
    /// 数据
    /// </summary>
    private material_item material_item_Prefabs;
    private (string, int) data;
    private void Awake()
    {
        baseinfo=Find<Text>("info");
        icon=Find<Image>("icon");
        material_item_Prefabs = Resources.Load<material_item>("Prefabs/panel_bag/material_item");
    }
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="bag_Resources"></param>
    public void Init((string, int) bag_Resources,string unit)
    {
        data = bag_Resources;
        baseinfo.text = Show_Color.White(bag_Resources.Item1) + "\n单价"
            + Battle_Tool.FormatNumberToChineseUnit(bag_Resources.Item2)
            + " " + unit
            + "\n" + Show_Color.Green("购买");
        material_item item = Instantiate(material_item_Prefabs, icon.transform);
        item.Init((bag_Resources.Item1,1));
    }

    public void PetInit((string, int) bag_Resources, string unit)
    {
        data = bag_Resources;
        baseinfo.alignment = TextAnchor.MiddleCenter;
        baseinfo.text = Show_Color.White(bag_Resources.Item1);
        material_item item = Instantiate(material_item_Prefabs, icon.transform);
        item.Init((bag_Resources.Item1, 1));
    }
}
