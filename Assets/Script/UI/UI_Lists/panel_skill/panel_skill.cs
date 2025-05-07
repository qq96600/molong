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
    /// 功能按钮
    /// </summary>
    private btn_item btn_item_Prefabs;

    private skill_item skill_item_Prefabs;
    /// <summary>
    /// 技能效果
    /// </summary>
    private skill_offect_item skill_item_parfabs;
    /// <summary>
    /// 按钮位置
    /// </summary>
    private Transform crt_btn,crt_offect_btn, crt_skill,pos_skill;
    /// <summary>
    /// 当前选中的技能
    /// </summary>
    private skill_offect_item user_skill;
    /// <summary>
    /// 存储功能组件
    /// </summary>
    private Dictionary<skill_Offect_btn_list,btn_item> btn_item_dic = new Dictionary<skill_Offect_btn_list, btn_item>();

    private Text base_info;

    private skill_btn_list select_btn_type = skill_btn_list.战斗;
    /// <summary>
    /// 选中技能
    /// </summary>
    private offect_up_skill offect_skill;
    /// <summary>
    /// 分配内力
    /// </summary>
    private allocation_skill_damage allocation_skill_damage;
    /// <summary>
    /// 需求升级经验
    /// </summary>
    private int need_exp;
    /// <summary>
    /// 翻页信息
    /// </summary>
    private Text page_info;
    /// <summary>
    /// 翻页
    /// </summary>
    private int page_index = 0;

    private int crt_skill_number = 0;
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
        btn_item_Prefabs = Battle_Tool.Find_Prefabs<btn_item>("btn_item");// Resources.Load<btn_item>("Prefabs/base_tool/btn_item");
        skill_item_Prefabs = Battle_Tool.Find_Prefabs<skill_item>("skill_item"); //Resources.Load<skill_item>("Prefabs/panel_skill/skill_item");
        base_info = Find<Text>("bg_main/show_skill/bg_info/Viewport/base_info");
        offect_skill = Find<offect_up_skill>("bg_main/offect_up_skill");
        allocation_skill_damage = Find<allocation_skill_damage>("bg_main/allocation_skill_damage");
        page_info = Find<Text>("bg_main/item_list/page_info");
        page_info.GetComponent<Button>().onClick.AddListener(delegate { Page_Change(); });
        pos_skill = Find<Transform>("bg_main/item_list/list");
        skill_item_parfabs = Battle_Tool.Find_Prefabs<skill_offect_item>("skill_offect_item"); //Resources.Load<skill_offect_item>("Prefabs/panel_skill/skill_offect_item");
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
    /// 翻页
    /// </summary>
    private void Page_Change()
    {
        page_index++;
        if (page_index + 1 > (int)Math.Ceiling((double)crt_skill_number / 12))
            page_index = 0;
        Show_skill();
    }

    /// <summary>
    /// 选中技能
    /// </summary>
    /// <param name="btn_item"></param>
    private void Select_Offect_Btn(btn_item btn_item)
    {
        switch ((skill_Offect_btn_list)btn_item.index)
        {
            case skill_Offect_btn_list.上阵:
                offect_skill.gameObject.SetActive(true);
                offect_skill.Show(user_skill.Data);
                break;
            case skill_Offect_btn_list.升级:
                UpLv();
                break;
            case skill_Offect_btn_list.分配:
                allocation_skill_damage.gameObject.SetActive(true);
                allocation_skill_damage.Show(user_skill.Data);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 升级技能
    /// </summary>
    private void UpLv()
    {
        if (user_skill.Data.skill_max_lv > int.Parse(user_skill.Data.user_values[1]))
        {
            if (SumSave.crt_hero.hero_material_list[2] >= need_exp)
            {
                SumSave.crt_hero.hero_material_list[2]-=need_exp;
                //更新英雄信息
                Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto,Mysql_Table_Name.mo_user_hero, SumSave.crt_hero.Set_Uptade_String(),SumSave.crt_hero.Get_Update_Character());
                Alert_Dec.Show("升级消耗  " + need_exp + user_skill.Data.skillname + "等级提升");
                int lv = int.Parse(user_skill.Data.user_values[1]);
                lv++;
                if (user_skill.Data.skill_type == 2)//判断是否为秘籍
                {
                    if (user_skill.Data.skill_need_state.Count > 0)
                    {
                        foreach (var item in user_skill.Data.skill_need_state)
                        {
                            if (lv == item.Item1)//当秘籍技能达到特定等级获得新技能
                            {
                                Alert_Dec.Show("获得技能 " + item.Item2);
                                SumSave.crt_skills.Add(tool_Categoryt.crate_skill(item.Item2));//添加技能
                            }
                        }
                    }
                }
                user_skill.Data.user_values[1] = lv.ToString();
                user_skill.Data.user_value = ArrayHelper.Data_Encryption(user_skill.Data.user_values);
                Game_Omphalos.i.Wirte_ResourcesList(Emun_Resources_List.skill_value, SumSave.crt_skills);
                SendNotification(NotiList.Refresh_Max_Hero_Attribute);
                user_skill.Refresh();
                Select_skill(user_skill);
            }else Alert_Dec.Show("历练值不足");
            
        }
        else Alert_Dec.Show("技能等级已满");
    }
    /// <summary>
    /// 选中功能
    /// </summary>
    /// <param name="btn_item"></param>
    private void Select_Btn(btn_item btn_item)
    {
        select_btn_type = (skill_btn_list)btn_item.index;
        Show_skill();
    }

    public override void Show()
    {
        base.Show();
        Show_skill();
    }
    /// <summary>
    /// 显示技能列表
    /// </summary>
    private void Show_skill()
    {
        base_info.text = "请学习技能";
        user_skill = null;
        for (int i = pos_skill.childCount - 1; i >= 0; i--)
        {
            Destroy(pos_skill.GetChild(i).gameObject);
        }
        foreach (var item in btn_item_dic.Keys)
        {
            btn_item_dic[item].gameObject.SetActive(false);
        }
        //SumSave.crt_skills.Add(tool_Categoryt.crate_skill(SumSave.db_skills[Random.Range(0, SumSave.db_skills.Count)].skillname));//添加技能
        List<base_skill_vo> lists = ArrayHelper.FindAll(SumSave.crt_skills, e => (skill_btn_list)e.skill_type == select_btn_type);
        crt_skill_number= lists.Count;
        if (crt_skill_number == 0) page_info.text = "";
        else
            page_info.text = "下一页" + (page_index + 1) + " / " + (int)Math.Ceiling((double)crt_skill_number / 12);
        int max = Mathf.Min(lists.Count, (page_index + 1) * 12);
        for (int i = page_index * 12; i < max; i++)
        {
            skill_offect_item item = Instantiate(skill_item_parfabs, pos_skill);
            item.Data = lists[i];
            item.GetComponent<Button>().onClick.AddListener(delegate { Select_skill(item); });
            if (user_skill == null) Select_skill(item);
        }
    }
    /// <summary>
    /// 选择物品
    /// </summary>
    /// <param name="item"></param>
    private void Select_skill(skill_offect_item item)
    {
        user_skill = item;
        Open_Select_Btn();
        Show_info();
    }
    /// <summary>
    /// 显示功能开关
    /// </summary>
    private void Open_Select_Btn()
    {
        if (user_skill.Data.skill_max_lv > user_skill.Data.skilllv) btn_item_dic[skill_Offect_btn_list.升级].gameObject.SetActive(true);
        if ((skill_btn_list)user_skill.Data.skill_type != skill_btn_list.特殊) btn_item_dic[skill_Offect_btn_list.上阵].gameObject.SetActive(true);
        if ((skill_btn_list)user_skill.Data.skill_type == skill_btn_list.战斗) btn_item_dic[skill_Offect_btn_list.分配].gameObject.SetActive(true);

    }

    /// <summary>
    /// 显示信息
    /// </summary>
    private void Show_info()
    {
        need_exp = 0;
        string dec = user_skill.Data.skillname + "\nLv." + user_skill.Data.user_values[1]+"级\n";
        int lv= int.Parse(user_skill.Data.user_values[1]);
        dec += "消耗法力 " + user_skill.Data.skill_spell + "%\n";
        dec += "技能cd " + user_skill.Data.skill_cd.ToString("F0") + "秒\n";
        dec += "对目标造成" + (user_skill.Data.skill_damage + (user_skill.Data.skill_power * lv)) + "%伤害\n";
        if (lv >= user_skill.Data.skill_max_lv)
        {
            dec += Show_Color.Red("已满级\n");
        }
        else
        {
            int number = 0;
            foreach (var item in SumSave.crt_skills)
            {
                if (item.skill_type == user_skill.Data.skill_type) number += int.Parse(item.user_values[1]);
            }
            need_exp = (int)(number * 10 * MathF.Pow(2, int.Parse(user_skill.Data.user_values[1])));

            dec += Show_Color.Green("升级需要 " + need_exp + "历练值\n");
        }
        if (user_skill.Data.skill_open_type.Count > 0)
        {
            dec += Show_Color.Yellow("激活特效 \n");
            for (int i = 0; i < user_skill.Data.skill_open_type.Count; i++)
            {
                dec += (enum_attribute_list)user_skill.Data.skill_open_type[i] + " + " + (user_skill.Data.skill_open_value[i] * lv / user_skill.Data.skill_max_lv) + "\n";
            }
        }
        if (user_skill.Data.skill_pos_type.Count > 0)
        {
            dec += Show_Color.Yellow("上阵特效 \n");
            for (int i = 0; i < user_skill.Data.skill_pos_type.Count; i++)
            {
                dec += (enum_attribute_list)user_skill.Data.skill_pos_type[i] + " + " + (user_skill.Data.skill_pos_value[i] * lv / user_skill.Data.skill_max_lv) + "\n";
            }
        }
        if (user_skill.Data.skill_need_state.Count > 0)
        {
            dec += Show_Color.Yellow("等级特效 \n");
            for (int i = 0; i < user_skill.Data.skill_need_state.Count; i++)
            {
                dec += user_skill.Data.skill_need_state[i].Item1+"级 激活 "+(user_skill.Data.skill_need_state[i].Item2) + "\n";
            }
        }
        base_info.text = dec;
    }
}
