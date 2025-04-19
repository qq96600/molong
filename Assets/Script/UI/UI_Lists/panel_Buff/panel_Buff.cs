using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class panel_Buff : Panel_Base
{
    /// <summary>
    /// 输入框
    /// </summary>
    private InputField inputField;
    /// <summary>
    /// 确认按钮
    /// </summary>
    private Button confirm;
    /// <summary>
    /// 信息显示
    /// </summary>
    private Text info;
    public override void Hide()
    {
        base.Hide();
    }

    public override void Initialize()
    {
        base.Initialize();
        inputField = Find<InputField>("bg_main/InputField");
        confirm = Find<Button>("bg_main/confirm");
        info = Find<Text>("bg_main/info/info");
        confirm.onClick.AddListener(OnConfirmClick);

    }
    /// <summary>
    /// 点击事件
    /// </summary>
    private void OnConfirmClick()
    {

    }

    public override void Show()
    {
        base.Show();
        inputField.text = SumSave.crt_hero.hero_name;
        string dec = "";
        dec += enum_skill_attribute_list.经验加成 + ": " + Show_Buff(enum_skill_attribute_list.经验加成) + "%\n";
        dec += enum_skill_attribute_list.灵珠收益 + ": " + Show_Buff(enum_skill_attribute_list.灵珠收益) + "%\n";
        dec += enum_skill_attribute_list.装备爆率 + ": " + Show_Buff(enum_skill_attribute_list.装备爆率) + "%\n";
        dec += enum_skill_attribute_list.装备掉落 + ": " + Show_Buff(enum_skill_attribute_list.装备掉落) + "%\n";
        dec += enum_skill_attribute_list.宠物获取 + ": " + Show_Buff(enum_skill_attribute_list.宠物获取) + "%\n";
        dec += enum_skill_attribute_list.寻怪间隔 + ": -" + (Show_Buff(enum_skill_attribute_list.寻怪间隔) / 10f) + "s\n";
        dec += enum_skill_attribute_list.复活次数 + ": " + Show_Buff(enum_skill_attribute_list.复活次数) + "次\n";
        info.text = dec;

    }

    private int Show_Buff(enum_skill_attribute_list index)
    {

        int value = 0;
        if (SumSave.crt_MaxHero.bufflist.Contains((int)index))
            value = SumSave.crt_MaxHero.bufflist[(int)index];
        return value;

    }

    protected override void Awake()
    {
        base.Awake();
    }
}
