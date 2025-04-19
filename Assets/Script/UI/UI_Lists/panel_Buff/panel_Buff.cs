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
    /// �����
    /// </summary>
    private InputField inputField;
    /// <summary>
    /// ȷ�ϰ�ť
    /// </summary>
    private Button confirm;
    /// <summary>
    /// ��Ϣ��ʾ
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
    /// ����¼�
    /// </summary>
    private void OnConfirmClick()
    {

    }

    public override void Show()
    {
        base.Show();
        inputField.text = SumSave.crt_hero.hero_name;
        string dec = "";
        dec += enum_skill_attribute_list.����ӳ� + ": " + Show_Buff(enum_skill_attribute_list.����ӳ�) + "%\n";
        dec += enum_skill_attribute_list.�������� + ": " + Show_Buff(enum_skill_attribute_list.��������) + "%\n";
        dec += enum_skill_attribute_list.װ������ + ": " + Show_Buff(enum_skill_attribute_list.װ������) + "%\n";
        dec += enum_skill_attribute_list.װ������ + ": " + Show_Buff(enum_skill_attribute_list.װ������) + "%\n";
        dec += enum_skill_attribute_list.�����ȡ + ": " + Show_Buff(enum_skill_attribute_list.�����ȡ) + "%\n";
        dec += enum_skill_attribute_list.Ѱ�ּ�� + ": -" + (Show_Buff(enum_skill_attribute_list.Ѱ�ּ��) / 10f) + "s\n";
        dec += enum_skill_attribute_list.������� + ": " + Show_Buff(enum_skill_attribute_list.�������) + "��\n";
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
