using Common;
using Components;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class panel_setting : Panel_Base
{
    /// <summary>
    /// 预制体位置
    /// </summary>
    private Transform crt_setting;
    /// <summary>
    /// 预制体
    /// </summary>
    private setting_item setting_Item_prefab;

    private string[] setting_name = { "设置1", "设置2", "设置3", "设置4", "设置5" };

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Initialize()
    {
        base.Initialize();
        crt_setting=Find<Transform>("bg_main/Scroll View/Viewport/Content");
        setting_Item_prefab=Resources.Load<setting_item>("Prefabs/panel_setting/setting_item");

        for (int i = 0; i < SumSave.db_sttings.Count; i++)
        {
            setting_item item = Instantiate(setting_Item_prefab, crt_setting);
            item.Init(i, SumSave.db_sttings[i].option_setting.Split(' '), SumSave.crt_setting.user_setting[i]);
        }
        
    }
    /// <summary>
    /// 设置
    /// </summary>
    /// <param name="data"></param>
    protected void Select_Setting((int index, int value) data)
    {
        SumSave.crt_setting.user_setting[data.index] = data.value;
        SendNotification(NotiList.Refresh_User_Setting, SumSave.crt_setting);
        Alert_Dec.Show("设置成功");
    }

    public override void Show()
    {
        base.Show();
    }
}
