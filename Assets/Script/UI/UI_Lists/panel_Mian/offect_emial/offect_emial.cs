using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 邮件列表
/// </summary>
public class offect_emial : Base_Mono
{
    /// <summary>
    /// 邮件位置
    /// </summary>
    private Transform pos_list, pos_receive;
    /// <summary>
    /// 邮件列表
    /// </summary>
    private emial_item emial_Item_Prefab;

    private material_item material_Item_Prefab;
    /// <summary>
    /// 显示信息
    /// </summary>
    private Text title, content;
    /// <summary>
    /// 接收按钮
    /// </summary>
    private Button btn_receive;
    private void Awake()
    {
        emial_Item_Prefab = Battle_Tool.Find_Prefabs<emial_item>("emial_item");
        pos_list = Find<Transform>("offect/Scroll View/Viewport/Content");
        content = Find<Text>("offect/show_offect/content");
        pos_receive= Find<Transform>("offect/show_offect/Scroll View/Viewport/Content");
        material_Item_Prefab= Battle_Tool.Find_Prefabs<material_item>("material_item");
    }
    public override void Show()
    {
        base.Show();
        Base_Show();
    }

    private void Base_Show()
    {

    }
}
