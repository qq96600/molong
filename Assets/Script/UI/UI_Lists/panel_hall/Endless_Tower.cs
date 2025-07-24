using Common;
using Components;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static user_endless_battle;
using Random = UnityEngine.Random;

/// <summary>
/// 无尽深渊
/// </summary>
public class Endless_Tower : Panel_Base
{
    /// <summary>
    /// 进入调整按钮
    /// </summary>
    private Button up_map;
    /// <summary>
    /// 战斗地图
    /// </summary>
    private panel_EndlessBattle fight_panel;
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
    /// <summary>
    /// 可获得奖励次数文本
    /// </summary>
    private Text number;

    public override void Initialize()
    {
        base.Initialize();
        fight_panel = UI_Manager.I.GetPanel<panel_EndlessBattle>();
        up_map = Find<Button>("up_map");
        up_map.onClick.AddListener(Challenge);
        crt = Find<Transform>("information/Viewport/Content");
        base_info = Find<Text>("boss_icon/nameText");
        rank_itemPrefab = Battle_Tool.Find_Prefabs<rank_item>("rank_item");
        information = Find<Transform>("information/Viewport/Content");
        number= Find<Text>("number");
    }

    /// <summary>
    /// 初始试练塔排行榜
    /// </summary>
    private void GetList()
    {
        ClearObject(crt);
        SendNotification(NotiList.Read_EndlessBattle);
        List <endlsess_battle> endless_list = SumSave.crt_endless_battle.endless_list;
        for (int i = 0; i < endless_list.Count; i++)
        {
            rank_item item = Instantiate(rank_itemPrefab, crt);
            (string, string, long) data = (endless_list[i].type,endless_list[i].name, endless_list[i].num);
            item.Data2 = data;
            item.Show_index2(i + 1);
        }
    }
    /// <summary>
    /// 点击挑战
    /// </summary>
    private void Challenge()
    {
        user_map_vo map = ArrayHelper.Find(SumSave.db_maps, e => e.map_type == 7);
        fight_panel.Show();
        fight_panel.Open_Map(map);
        this.gameObject.SetActive(false);
    }
    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {
        GetList();
        List<(string, int)> list = SumSave.crt_needlist.SetMap();
        bool exist = true;
        string str = "";
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Item1 == "无尽深渊")
            {
                exist = false;
                str = "已获得奖励次数:"+ list[i].Item2+ "/1"; 
                break;
            }
        }
        if(exist)
        {
            str = "已获得奖励次数:0/1";
        }
        str += "\n最大击杀数量："+ SumSave.crt_endless_battle.endless_dic[SumSave.uid].num;
        number.text = str;
    }
    public override void Hide()
    {
        base.Hide();
    }

    public override void Show()
    {
        base.Show();
        if (SumSave.crt_MaxHero.Lv < 60)
        {
            Alert_Dec.Show("无尽深渊开启等级为60级");
            gameObject.SetActive(false);
            return;
        }
        Init();
    }
}
