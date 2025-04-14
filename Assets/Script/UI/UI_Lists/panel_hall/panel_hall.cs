using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class panel_hall : Panel_Base
{
    /// <summary>
    /// 资源，提升，地图父物体
    /// </summary>
    private Transform pos_hero, pos_map, pos_otain;
    /// <summary>
    /// 预制体组件
    /// </summary>
    private btn_item btn_item_Prefabs;
    private List<string> otainlist=new List<string>(){ "强化", "合成", "炼丹" };
    private List<string> maplist = new List<string>() { "地图1", "地图2", "地图3" };
    private List<string> herolist = new List<string>() { "签到", "通行证", "收集", "成就"};
    /// <summary>
    /// 成就系统
    /// </summary>
    private Achievement achievement;

    public override void Hide()
    {
        base.Hide();
    }

    public override void Initialize()
    {
        base.Initialize();
        pos_hero = Find<Transform>("bg_main/panel_list/otainlist/Scroll View/Viewport/Content");
        pos_map = Find<Transform>("bg_main/panel_list/maplist/Scroll View/Viewport/Content");
        pos_otain = Find<Transform>("bg_main/panel_list/herolist/Scroll View/Viewport/Content");
        btn_item_Prefabs = Resources.Load<btn_item>("Prefabs/base_tool/btn_item");

        achievement=Find<Achievement>("bg_main/achievement");

        ClearObject(pos_hero);
        ClearObject(pos_map);
        ClearObject(pos_otain);
        for (int i = 0; i < herolist.Count; i++)
        { 
            btn_item item = Instantiate(btn_item_Prefabs, pos_hero);
            item.Show(i,herolist[i]);
            item.GetComponent<Button>().onClick.AddListener(() => { OnClickHeroItem(item); });
        }
        for (int i = 0; i < maplist.Count; i++)
        { 
            btn_item item = Instantiate(btn_item_Prefabs, pos_map);
            item.Show(i,maplist[i]);
            item.GetComponent<Button>().onClick.AddListener(() => { OnClickMapItem(item); });
        }
        for (int i = 0; i < otainlist.Count; i++)
        { 
            btn_item item = Instantiate(btn_item_Prefabs, pos_otain);
            item.Show(i,otainlist[i]);
            item.GetComponent<Button>().onClick.AddListener(() => { OnClickOtainItem(item); });
        }
    }
    /// <summary>
    /// 打开资源提升开关
    /// </summary>
    /// <param name="item"></param>
    private void OnClickOtainItem(btn_item item)
    {

    }
    /// <summary>
    /// 打开地图
    /// </summary>
    /// <param name="item"></param>
    private void OnClickMapItem(btn_item item)
    {

    }
    /// <summary>
    /// 打开提升开关
    /// </summary>
    /// <param name="item"></param>
    private void OnClickHeroItem(btn_item item)
    {
        switch (item.index)//"签到", "通行证", "收集", "成就"
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                achievement.Show();
                break;


        }

    }

    public override void Show()
    {
        base.Show();
    }

    protected override void Awake()
    {
        base.Awake();
    }
}
