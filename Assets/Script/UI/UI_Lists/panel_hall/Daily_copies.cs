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
        copies_item_Prefabs = Resources.Load<copies_item>("Prefabs/panel_hall/Daily_copies/copies_item");
        for (int i = 0; i < 10; i++)
        { 
            copies_item item = Instantiate(copies_item_Prefabs, pos_crtmap);
            item.Init(i);
            item.GetComponent<Button>().onClick.AddListener( ()=> { OnClick(item); });
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

    }

    public override void Show()
    {
        base.Show();
    }
}
