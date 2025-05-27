using Common;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class panel_wodleBoss : Panel_Base
{
    /// <summary>
    /// 世界boss图标
    /// </summary>
    private Image icon;
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
    /// boss挑战进度条
    /// </summary>
    private Slider progress;
    /// <summary>
    /// 血量显示，次数显示
    /// </summary>
    private Text Hp_Text, numberText, nameText;
    /// <summary>
    /// 最大生命值
    /// </summary>
    private long maxHp;
    /// <summary>
    /// 当前世界Boss挑战次数，最大挑战次数
    /// </summary>
    private int boss_number = 0, boss_number_max = 3;

    public override void Initialize()
    {
        base.Initialize();
        fight_panel = UI_Manager.I.GetPanel<panel_fight>();
        icon = Find<Image>("boss_icon");
        up_map = Find<Button>("up_map");
        up_map.onClick.AddListener(Challenge);
        crt = Find<Transform>("information/Viewport/Content");
        progress = Find<Slider>("progress");
        Hp_Text = Find<Text>("progress/Hp_Text");
        numberText = Find<Text>("up_map/number");
        nameText =  Find<Text>("boss_icon/nameText");
        List<(string, int)> list = SumSave.crt_needlist.SetMap();
        boss_number = boss_number_max;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Item1 == SumSave.crt_world_boos.name)
            {
                boss_number = list[i].Item2;
            }
        }
        Init();
    }
    /// <summary>
    /// 点击挑战
    /// </summary>
    private void Challenge()
    {

        world_Boss_set((long)SumSave.crt_MaxHero.totalPower);



    }
    /// <summary>
    /// 获得奖励
    /// </summary>
    private void GainRewards()
    {

    }



    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {

        SendNotification(NotiList.Read_Crate_world_boss_Login);
        icon.sprite = Resources.Load<Sprite>("Prefabs/monsters/" + SumSave.crt_world_boos.name);
        maxHp = 1000000 * SumSave.crt_world_boos.number;
        progress.maxValue = maxHp;
        progress.value = SumSave.crt_world_boos.Get();
        numberText.text = "挑战次数:" + boss_number.ToString() + "/3";
        nameText.text = SumSave.crt_world_boos.name;

    }
    public override void Hide()
    {
        base.Hide();
    }

    public override void Show()
    {
        base.Show();
    }
    /// <summary>
    /// 写入对世界boss的伤害
    /// </summary>
    /// <param name="finalDamage"></param>
    private void world_Boss_set(long finalDamage)
    {
        SumSave.crt_world_boos.Set(finalDamage);
        SendNotification(NotiList.Read_Crate_world_boss_update, "");
    }

    /// <summary>
    /// 挑战次数++
    /// </summary>
    private void IncreaseFrequency()
    {
        bool exist = true;
        List<(string, int)> list = SumSave.crt_needlist.SetMap();
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Item1 == SumSave.crt_world_boos.name)
            {
                exist = false;
                list[i] = (list[i].Item1, list[i].Item2 + 1);
                SumSave.crt_needlist.SetMap(list[i]);
                break;
            }
        }
        if (exist)
        {
            SumSave.crt_needlist.SetMap((SumSave.crt_world_boos.name, 1));

        }
        boss_number++;
        Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_needlist,
           SumSave.crt_needlist.Set_Uptade_String(), SumSave.crt_needlist.Get_Update_Character());
    }
}
