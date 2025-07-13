using Common;
using Components;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panel_tianmingTai : Base_Mono
{
    /// <summary>
    /// 天命显示位置
    /// </summary>
    private Transform tianming_image;
    /// <summary>
    /// 天命信息显示位置
    /// </summary>
    private Text tianming_Title, title_button;
    /// <summary>
    /// 刷新天命按钮
    /// </summary>
    private Button UpButton;
    /// <summary>
    /// 天命预制体
    /// </summary>
    private GameObject tianming_item;
    /// <summary>
    /// 刷新消耗魔丸数量
    /// </summary>
    private int need = 20;
    
    private void Awake()
    {
        tianming_image = Find<Transform>("tianming_image/Viewport/Content");
        tianming_Title = Find<Text>("tianming_Title/Text");
        UpButton = Find<Button>("UpButton");
        UpButton.onClick.AddListener(UpButtonOnClick);
        title_button= Find<Text>("UpButton/title_button");
        title_button.text = "魔丸*" + need + "刷新一次";
    }

    /// <summary>
    /// 刷新天命台显示
    /// </summary>
    private void RefreshDisplay()
    {
        ClearObject(tianming_image);
        string str = "";
        str = "天命属性：";
        for (int i = 0; i < SumSave.crt_hero.tianming_Platform.Length; i++)
        {
            GameObject game = Resources.Load<GameObject>("Prefabs/halo/halo_" + SumSave.crt_hero.tianming_Platform[i]);
            Instantiate(game, tianming_image);
            str += SumSave.five_element_type[SumSave.crt_hero.tianming_Platform[i]-1]+" ";
        }
        tianming_Title.text = str;
    }

    /// <summary>
    /// 刷新天命属性
    /// </summary>
    private void UpButtonOnClick()
    {
        NeedConsumables(currency_unit.魔丸, need);
        if (RefreshConsumables())
        {
            SumSave.crt_hero.RefreshTianming();
            RefreshDisplay();
        }else
        {
            Alert_Dec.Show("魔丸不足");
        }
            
    }

    public override void Show()
    {
        base.Show();
        RefreshDisplay();
    }
}
