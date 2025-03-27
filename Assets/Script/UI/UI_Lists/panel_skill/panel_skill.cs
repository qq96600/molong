using Common;
using Components;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
    private Transform crt_btn,crt_offect_btn, crt_skill;
    /// <summary>
    /// ��ǰѡ�еļ���
    /// </summary>
    private skill_item user_skill;
    /// <summary>
    /// �洢�������
    /// </summary>
    private Dictionary<skill_Offect_btn_list,btn_item> btn_item_dic = new Dictionary<skill_Offect_btn_list, btn_item>();

    private Text base_info;

    private skill_btn_list select_btn_type = skill_btn_list.ս��;
    /// <summary>
    /// ѡ�м���
    /// </summary>
    private offect_up_skill offect_skill;
    /// <summary>
    /// ��������
    /// </summary>
    private allocation_skill_damage allocation_skill_damage;
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Initialize()
    {
        base.Initialize();
        crt_btn = Find<Transform>("bg_main/skills/btns");
        crt_skill = Find<Transform>("bg_main/skills/Scroll View/Viewport/Content");
        crt_offect_btn=Find<Transform>("bg_main/show_skill/btn_list");
        btn_item_Prefabs = Resources.Load<btn_item>("Prefabs/base_tool/btn_item");
        skill_item_Prefabs = Resources.Load<skill_item>("Prefabs/panel_skill/skill_item");
        base_info = Find<Text>("bg_main/show_skill/bg_info/base_info");
        offect_skill = Find<offect_up_skill>("bg_main/offect_up_skill");
        allocation_skill_damage = Find<allocation_skill_damage>("bg_main/allocation_skill_damage");
        for (int i = 0; i < Enum.GetNames(typeof(skill_btn_list)).Length; i++)
        {
            btn_item btn_item = Instantiate(btn_item_Prefabs, crt_btn);
            btn_item.Show(i+1, (skill_btn_list)(i+1));
            btn_item.GetComponent<Button>().onClick.AddListener(delegate { Select_Btn(btn_item); });
        }
        for (int i = 0; i < Enum.GetNames(typeof(skill_Offect_btn_list)).Length; i++)
        {
            btn_item btn_item = Instantiate(btn_item_Prefabs, crt_offect_btn);
            btn_item.Show(i, (skill_Offect_btn_list)i);
            btn_item.GetComponent<Button>().onClick.AddListener(delegate { Select_Offect_Btn(btn_item); });
            btn_item_dic.Add((skill_Offect_btn_list)i, btn_item);
        }
    }
    /// <summary>
    /// ѡ�м���
    /// </summary>
    /// <param name="btn_item"></param>
    private void Select_Offect_Btn(btn_item btn_item)
    {
        switch ((skill_Offect_btn_list)btn_item.index)
        {
            case skill_Offect_btn_list.����:
                offect_skill.gameObject.SetActive(true);
                offect_skill.Show(user_skill.Data);
                break;
            case skill_Offect_btn_list.����:
                UpLv();
                break;
            case skill_Offect_btn_list.����:
                allocation_skill_damage.gameObject.SetActive(true);
                allocation_skill_damage.Show(user_skill.Data);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// ��������
    /// </summary>
    private void UpLv()
    {
        //ȱ�������ж�
        if (user_skill.Data.skill_max_lv > int.Parse(user_skill.Data.user_values[1]))
        {
            Alert_Dec.Show("�������� * 3333����ֵ " + user_skill.Data.skillname + "�ȼ�����");
            int lv = int.Parse(user_skill.Data.user_values[1]);
            lv++;
            user_skill.Data.user_values[1] = lv.ToString();
            user_skill.Data.user_value = ArrayHelper.Data_Encryption(user_skill.Data.user_values);
            Game_Omphalos.i.Wirte_ResourcesList(Emun_Resources_List.skill_value, SumSave.crt_skills);
            SendNotification(NotiList.Refresh_Max_Hero_Attribute);
            user_skill.Refresh();
            Select_skill(user_skill);
        }
        else Alert_Dec.Show("���ܵȼ�����");
    }
    /// <summary>
    /// ѡ�й���
    /// </summary>
    /// <param name="btn_item"></param>
    private void Select_Btn(btn_item btn_item)
    {
        select_btn_type = (skill_btn_list)btn_item.index;
        user_skill = null;
        Show_skill();
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
    /// <summary>
    /// ��ʾ�����б�
    /// </summary>
    private void Show_skill()
    {
        for (int i = crt_skill.childCount - 1; i >= 0; i--)
        {
            Destroy(crt_skill.GetChild(i).gameObject);
        }
        foreach (var item in btn_item_dic.Keys)
        {
            btn_item_dic[item].gameObject.SetActive(false);
        }
        //SumSave.crt_skills.Add(tool_Categoryt.crate_skill(SumSave.db_skills[Random.Range(0, SumSave.db_skills.Count)].skillname));
        for (int i = 0; i < SumSave.crt_skills.Count; i++)
        {
            if ((skill_btn_list)SumSave.crt_skills[i].skill_type == select_btn_type)
            {
                skill_item item = Instantiate(skill_item_Prefabs, crt_skill);
                item.Data = SumSave.crt_skills[i];
                item.GetComponent<Button>().onClick.AddListener(delegate { Select_skill(item); });
                if (user_skill == null) Select_skill(item);
            }
           
        }
    }
    /// <summary>
    /// ѡ����Ʒ
    /// </summary>
    /// <param name="item"></param>
    private void Select_skill(skill_item item)
    {
        user_skill = item;
        Open_Select_Btn();
        Show_info();
    }
    /// <summary>
    /// ��ʾ���ܿ���
    /// </summary>
    private void Open_Select_Btn()
    {
        if (user_skill.Data.skill_max_lv > user_skill.Data.skilllv) btn_item_dic[skill_Offect_btn_list.����].gameObject.SetActive(true);
        if ((skill_btn_list)user_skill.Data.skill_type != skill_btn_list.����) btn_item_dic[skill_Offect_btn_list.����].gameObject.SetActive(true);
        if ((skill_btn_list)user_skill.Data.skill_type == skill_btn_list.ս��) btn_item_dic[skill_Offect_btn_list.����].gameObject.SetActive(true);

    }

    /// <summary>
    /// ��ʾ��Ϣ
    /// </summary>
    private void Show_info()
    {

        string dec = user_skill.Data.skillname + "\nLv." + user_skill.Data.user_values[1]+"��";
        int lv= int.Parse(user_skill.Data.user_values[1]);
        dec += "���ķ��� " + user_skill.Data.skill_spell + "%\n";
        dec += "����cd " + user_skill.Data.skill_cd / 60f + "��\n";
        dec += "��Ŀ�����" + (user_skill.Data.skill_damage + (user_skill.Data.skill_power * lv)) + "%�˺�";
        if (lv >= user_skill.Data.skill_max_lv)
        { 
        dec += "\n������";
        }
        else
        {
            dec += "������Ҫ " + user_skill.Data.skill_need_exp * Mathf.Pow(user_skill.Data.skill_need_coefficient[0], user_skill.Data.skill_need_coefficient[1]) * (lv + 1) + "\n";
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
