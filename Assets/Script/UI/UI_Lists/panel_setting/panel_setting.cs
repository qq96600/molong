using Common;
using Components;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class panel_setting : Panel_Base
{
    /// <summary>
    /// Ԥ����λ��
    /// </summary>
    private Transform crt_setting;
    /// <summary>
    /// Ԥ����
    /// </summary>
    private setting_item setting_Item_prefab;

    private string[] setting_name = { "����1", "����2", "����3", "����4", "����5" };

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
    /// ����
    /// </summary>
    /// <param name="data"></param>
    protected void Select_Setting((int index, int value) data)
    {
        SumSave.crt_setting.user_setting[data.index] = data.value;
        SendNotification(NotiList.Refresh_User_Setting, SumSave.crt_setting);
        Alert_Dec.Show("���óɹ�");
    }

    public override void Show()
    {
        base.Show();
    }
}
