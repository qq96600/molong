using Common;
using Components;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Daily_copies : Base_Mono
{
    private Transform pos_crtmap;

    private copies_item copies_item_Prefabs;
    
    private void Awake()
    {
        pos_crtmap=Find<Transform>("Scroll View/Viewport/Content");
        copies_item_Prefabs = Battle_Tool.Find_Prefabs<copies_item>("copies_item");
        for (int i = SumSave.db_maps.Count; i > 0; i--)
        {
            if (SumSave.db_maps[i].map_type == 4)
            {
                copies_item item = Instantiate(copies_item_Prefabs, pos_crtmap);
                item.Init(SumSave.db_maps[i],1);
                item.GetComponent<Button>().onClick.AddListener(() => { OnClick(item); });
            }
        }
    }
    /// <summary>
    /// 点击事件
    /// </summary>
    /// <param name="item"></param>
    private void OnClick(copies_item item)
    {
        string dec="第"+item.index+"副本";
        Alert.Show("进入副本", dec, confirm);
    }
    /// <summary>
    /// 确认进入
    /// </summary>
    /// <param name="arg0"></param>
    private void confirm(object arg0)
    {
        Alert_Dec.Show("等级不足");
    }

    public override void Show()
    {
        base.Show();
        if (int.Parse( SumSave.crt_hero.hero_lv) > 30)
        {
            Alert_Dec.Show("副本开启等级为30级");
            gameObject.SetActive(false);
        }
    }
}
