using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class panel_skill : Panel_Base
{
    
    /// <summary>
    /// ���ܰ�ť
    /// </summary>
    private btn_item btn_item_Prefabs;

    private skill_item skill_item_Prefabs;
    /// <summary>
    /// ��ťλ��
    /// </summary>
    private Transform crt_btn, crt_skill;
    /// <summary>
    /// ��ǰѡ�еļ���
    /// </summary>
    private skill_item user_skill;

    private Text base_info;
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Initialize()
    {
        base.Initialize();
        crt_btn = Find<Transform>("bg_main/skills/btns");
        crt_skill = Find<Transform>("bg_main/skills/Scroll View/Viewport/Content");
        btn_item_Prefabs = Resources.Load<btn_item>("Prefabs/base_tool/btn_item");
        skill_item_Prefabs = Resources.Load<skill_item>("Prefabs/panel_skill/skill_item");
        base_info = Find<Text>("bg_main/show_skill/bg_info/base_info");
        for (int i = 0; i < Enum.GetNames(typeof(skill_btn_list)).Length; i++)
        {
            btn_item btn_item = Instantiate(btn_item_Prefabs, crt_btn);
            btn_item.Show(i, (skill_btn_list)i);
            btn_item.GetComponent<Button>().onClick.AddListener(delegate { Select_Btn(btn_item); });
        }
    }
    /// <summary>
    /// ѡ�й���
    /// </summary>
    /// <param name="btn_item"></param>
    private void Select_Btn(btn_item btn_item)
    {
        Debug.Log("ѡ��" + (skill_btn_list)btn_item.index);
    }

    public override void Show()
    {
        base.Show();
        Show_skill();
    }
    /// <summary>
    /// ��ʾװ���б�
    /// </summary>
    private void Base_Show()
    {
      
    }

    private void Show_skill()
    {
        for (int i = crt_skill.childCount - 1; i >= 0; i--)
        {
            Destroy(crt_skill.GetChild(i).gameObject);
        }
        for (int i = 0; i < SumSave.db_skills.Count; i++)
        {
            skill_item item = Instantiate(skill_item_Prefabs, crt_skill);
            item.Data = SumSave.db_skills[i];
            item.GetComponent<Button>().onClick.AddListener(delegate { Select_skill(item); });
            if (user_skill == null) Select_skill(item);
        }
    }
    /// <summary>
    /// ѡ����Ʒ
    /// </summary>
    /// <param name="item"></param>
    private void Select_skill(skill_item item)
    {
        user_skill = item;

        Show_info();
    }

    private void Show_info()
    {

        string dec = user_skill.Data.skillname + "\nLv." + user_skill.Data.skilllv;

        dec += "���ķ��� " + user_skill.Data.skill_spell + "%\n";
        dec += "����cd " + user_skill.Data.skill_cd / 60f + "��\n";
        dec += "��Ŀ�����" + (user_skill.Data.skill_damage + (user_skill.Data.skill_power * user_skill.Data.skilllv)) + "%�˺�";
        if (user_skill.Data.skilllv >= user_skill.Data.skill_max_lv)
        { 
        dec += "\n������";
        }
        else
        {
            dec += "������Ҫ " + user_skill.Data.skill_need_exp * Mathf.Pow(user_skill.Data.skill_need_coefficient[0], user_skill.Data.skill_need_coefficient[1]) * (user_skill.Data.skilllv+1) + "\n";
        }
        if (user_skill.Data.skill_open_type.Count > 0)
        {
            dec += "������Ч ";
            for (int i = 0; i < user_skill.Data.skill_open_type.Count; i++)
            {
                dec += (enum_attribute_list)user_skill.Data.skill_open_type[i] + " + " + user_skill.Data.skill_open_value[i] + "\n";
            }
        }
        if (user_skill.Data.skill_pos_type.Count > 0)
        {
            dec += "������Ч ";
            for (int i = 0; i < user_skill.Data.skill_pos_type.Count; i++)
            {
                dec += (enum_attribute_list)user_skill.Data.skill_pos_type[i] + " + " + user_skill.Data.skill_pos_value[i] + "\n";
            }
        }
        if (user_skill.Data.skill_need_state.Count > 0)
        {
            dec += "�ȼ���Ч \n";
            for (int i = 0; i < user_skill.Data.skill_need_state.Count; i++)
            {
                dec += user_skill.Data.skill_need_state[i].Item1+" ���� "+(user_skill.Data.skill_need_state[i].Item2) + "\n";
            }
        }
        base_info.text = dec;
    }
}
