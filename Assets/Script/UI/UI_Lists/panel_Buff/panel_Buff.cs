using Common;
using Components;
using MVC;
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


    /// <summary>
    /// ��ɫ����
    /// </summary>
    private enum_skin_state skin_state;
    /// <summary>
    /// ��ɫƤ��Ԥ����
    /// </summary>
    private GameObject skin_prefabs;
    /// <summary>
    /// ��ɫ�ڹ�λ��
    /// </summary>
    private Transform panel_role_health;
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

        int hero_index = int.Parse(SumSave.crt_hero.hero_index);
        skin_state = (enum_skin_state)hero_index;
        skin_prefabs = Resources.Load<GameObject>("Prefabs/Skins/�ڹ�_" + skin_state.ToString());
        panel_role_health = Find<Transform>("bg_main/bg");
        Instantiate(skin_prefabs, panel_role_health);

    }
    /// <summary>
    /// ����¼�
    /// </summary>
    private void OnConfirmClick()
    {
        Alert.Show("��ʾ", "ȷ������ɫ�����޸�Ϊ\n" + Show_Color.Red(inputField.text) + " ��", Confirm);
    }
    /// <summary>
    /// ȷ��
    /// </summary>
    /// <param name="arg"></param>
    private void Confirm(object arg)
    { 
        SumSave.crt_hero.hero_name= inputField.text;
        SumSave.crt_hero.hero_material_list[0] = 1;
        Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_hero, new string[] { "'" + inputField.text + "'" }, new string[] { "hero_name" });
        SendNotification(NotiList.Refresh_Max_Hero_Attribute);
        Show();
    }
    public override void Show()
    {
        base.Show();
        confirm.gameObject.SetActive(SumSave.crt_hero.hero_material_list[0] == 0);
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
    /// <summary>
    /// ��ʾbuff
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private int Show_Buff(enum_skill_attribute_list index)
    {
        int value = 0;
        if ( (int)index < SumSave.crt_MaxHero.bufflist.Count)
            value = SumSave.crt_MaxHero.bufflist[(int)index];
        return value;
    }

    protected override void Awake()
    {
        base.Awake();
    }
}
