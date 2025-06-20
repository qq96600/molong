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

/// <summary>
/// 试练塔
/// </summary>
public class Trial_Tower : Panel_Base
{
    /// <summary>
    /// 进入调整按钮
    /// </summary>
    private Button up_map;
    /// <summary>
    /// 战斗地图
    /// </summary>
    private panel_fight fight_panel;
    /// <summary>
    /// 属性显示位置
    /// </summary>
    private Transform crt;
   
    /// <summary>
    /// 血量显示，次数显示
    /// </summary>
    private Text base_info;
    /// <summary>
    /// 排行榜预制体
    /// </summary>
    private rank_item rank_itemPrefab;
    /// <summary>
    /// 排行榜位置
    /// </summary>
    private Transform information;
    /// <summary>
    /// 读取自身属性
    /// </summary>
    private (string, string, long) user;

    public override void Initialize()
    {
        base.Initialize();
        fight_panel = UI_Manager.I.GetPanel<panel_fight>();
        up_map = Find<Button>("up_map");
        up_map.onClick.AddListener(Challenge);
        crt = Find<Transform>("information/Viewport/Content");
        base_info = Find<Text>("boss_icon/nameText");
        rank_itemPrefab = Battle_Tool.Find_Prefabs<rank_item>("rank_item");
        information = Find<Transform>("information/Viewport/Content");
        Init();
    }

    /// <summary>
    /// 初始试练塔排行榜
    /// </summary>
    private void GetList()
    {
        ClearObject(crt);
        for (int i = 0; i < SumSave.crt_Trial_Tower_rank.lists.Count; i++)
        {
            rank_item item = Instantiate(rank_itemPrefab, crt);
            item.Data2 = SumSave.crt_Trial_Tower_rank.lists[i];
            item.Show_index2(i + 1);
        }
    }


    /// <summary>
    /// 点击挑战
    /// </summary>
    private void Challenge()
    {
        fight_panel.Open_Map(ArrayHelper.Find(SumSave.db_maps, e => e.map_type == 6),(int)user.Item3);
    }
    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {
        //读取排行榜
        SendNotification(NotiList.Read_Trial_Tower);
        SumSave.crt_Trial_Tower_rank.lists = ArrayHelper.OrderDescding(SumSave.crt_Trial_Tower_rank.lists, x => x.Item3);
        foreach (var item in SumSave.crt_Trial_Tower_rank.lists)
        {
            if (item.Item1 == SumSave.crt_user.uid)
            { 
                user = item;
            }    
        }
        //获取层数
        base_info.text = "当前进度" + user.Item3 + "层";
        GetList();
    }
    public override void Hide()
    {
        base.Hide();
    }

    public override void Show()
    {
        base.Show();
        if (SumSave.crt_MaxHero.Lv < 40)
        {
            Alert_Dec.Show("试炼塔开启等级为40级");
            gameObject.SetActive(false);
            return;
        }
        Init();
    }
}
