using Common;
using Components;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class monitor_info : Base_Mono
{
    ///<summary>
    ///右上角健康栏Hp,Mp显示
    /// <summary>
    private Slider role_exp;

    private Text show_name, show_moeny, show_point, show_diamond,show_exp;

    private BattleHealth health;
    /// <summary>
    /// 角色皮肤
    /// </summary>
    private enum_skin_state skin_state;
    /// <summary>
    /// 角色皮肤预制体
    /// </summary>
    private GameObject skin_prefabs;
    /// <summary>
    /// 角色头像
    /// </summary>
    private Transform pos_health;
    /// <summary>
    /// 任务按钮
    /// </summary>
    private Button btn_base_task;
    /// <summary>
    /// 名称按钮
    /// </summary>
    private Button btn_back;
    /// <summary>
    /// 任务信息
    /// </summary>
    private Text show_task;
    private void Awake()
    {
        role_exp = Find<Slider>("base_info/Slider");
        show_exp = Find<Text>("base_info/Slider/exp");
        show_name = Find<Text>("base_info/role_name/info");
        show_moeny = Find<Text>("show_unit/moeny/info");
        show_point = Find<Text>("show_unit/Point/info");
        show_diamond = Find<Text>("show_unit/diamond/info");
        pos_health = Find<Transform>("base_info/profile_picture");
        btn_base_task = Find<Button>("base_task");
        show_task = Find<Text>("base_task/task_info");
        btn_base_task.onClick.AddListener(Show_GreenhandGuide);
        btn_back = Find<Button>("base_info/role_name");
        btn_back.onClick.AddListener(() => {
            UI_Manager.I.TogglePanel(Panel_List.panel_Buff, true);
        });
        Instance_Skin();
    }
    /// <summary>
    /// 领取任务奖励
    /// </summary>
    private void Show_GreenhandGuide()
    {
        if (SumSave.GreenhandGuide_TotalTasks.ContainsKey(SumSave.crt_greenhand.crt_task))
        {
            GreenhandGuide_TotalTaskVO task = SumSave.GreenhandGuide_TotalTasks[SumSave.crt_greenhand.crt_task];
            string dec = "";
            dec += task.task_dec_value +
              Show_Color.Green("\n进度(" + SumSave.crt_greenhand.crt_progress + "/" + task.progress + ")");
            Alert.Show(task.TaskDesc, dec, Claim_Rewards);
        }
        else Alert_Dec.Show("任务已完成");
    }
    /// <summary>
    /// 领取奖励
    /// </summary>
    /// <param name="arg0"></param>
    private void Claim_Rewards(object arg0)
    {
        if (SumSave.GreenhandGuide_TotalTasks.ContainsKey(SumSave.crt_greenhand.crt_task))
        {
            string dec = "";
            GreenhandGuide_TotalTaskVO task = SumSave.GreenhandGuide_TotalTasks[SumSave.crt_greenhand.crt_task];
            if (SumSave.crt_greenhand.crt_progress >= task.progress)    
            {
                foreach (var item in SumSave.crt_greenhand.task_list)
                {
                    if (item == task.taskid)
                    {
                        Alert_Dec.Show("任务已完成");
                        return;
                    } 
                }
                SumSave.crt_greenhand.task_list.Add(task.taskid);
                bool exist= false;
                foreach (var item in SumSave.GreenhandGuide_TotalTasks.Keys)
                {
                    //获取下一个进度条列表
                    if (exist)
                    { 
                        SumSave.crt_greenhand.crt_task = item;  
                        SumSave.crt_greenhand.crt_progress = 0;
                        exist = false;
                        break;
                    }else
                    if (SumSave.crt_greenhand.crt_task == item)
                    {
                        exist = true;
                    }
                }
                Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto,Mysql_Table_Name.mo_user_greenhandguide,
                    SumSave.crt_greenhand.Set_Uptade_String(),SumSave.crt_greenhand.Get_Update_Character());
                for (int i = 0; i < task.Award.Length; i++)
                {
                    dec += "获得 " + Show_Color.Green(task.Award[i] + ":" + task.AwardNumber[i] + "\n");
                    switch ((GreenhandGuide_Enum_List)Enum.Parse(typeof(GreenhandGuide_Enum_List), task.AwardType[i]))
                    {
                        case GreenhandGuide_Enum_List.武器:     
                        case GreenhandGuide_Enum_List.装备: 
                            SumSave.crt_bag.Add(tool_Categoryt.crate_equip(task.Award[i], task.AwardNumber[i]));
                            Game_Omphalos.i.Wirte_ResourcesList(Emun_Resources_List.bag_value, SumSave.crt_bag);
                            break;
                        case GreenhandGuide_Enum_List.物品:
                        case GreenhandGuide_Enum_List.材料:
                        case GreenhandGuide_Enum_List.技能:
                        case GreenhandGuide_Enum_List.奇物:
                            Battle_Tool.Obtain_Resources(task.Award[i], task.AwardNumber[i]);
                            break;
                        case GreenhandGuide_Enum_List.经验:
                            Battle_Tool.Obtain_Exp(task.AwardNumber[i]);
                            break;
                        case GreenhandGuide_Enum_List.灵珠:
                            Battle_Tool.Obtain_Unit(currency_unit.灵珠, task.AwardNumber[i]);
                            break;
                        case GreenhandGuide_Enum_List.宠物:
                            Battle_Tool.Obtain_Pet(task.Award[i], task.AwardNumber[i]);
                            break;
                        default:
                            break;
                    }
                }
                base_info_Task();
                Alert.Show(task.TaskDesc, dec);
            }
            else Alert_Dec.Show("任务未完成");
        }
    }
    /// <summary>
    /// 初始化角色皮肤
    /// </summary>
    private void Instance_Skin()
    {
        for (int i = pos_health.childCount - 1; i >= 0; i--)//清空区域内按钮
        {
            Destroy(pos_health.GetChild(i).gameObject);
        }
        skin_prefabs = Resources.Load<GameObject>("Prefabs/Skins/within_" + SumSave.crt_hero.hero_pos);
        Instantiate(skin_prefabs, pos_health);
    }
    private void Update()
    {
        if (SumSave.crt_MaxHero != null)
        {
            if (int.Parse(SumSave.crt_hero.hero_index) != (int)skin_state)
            {
                Instance_Skin();
            }
            show_name.text = SumSave.crt_MaxHero.show_name;
            show_exp.text = " Lv." + SumSave.crt_MaxHero.Lv +
               "(" + SumSave.crt_MaxHero.Exp * 100 / SumSave.db_lvs.hero_lv_list[SumSave.crt_MaxHero.Lv] + "%)";
            role_exp.value = SumSave.crt_MaxHero.Exp;
            List<long> list = SumSave.crt_user_unit.Set();
            show_moeny.text = Battle_Tool.FormatNumberToChineseUnit(list[0]) + " " + currency_unit.灵珠;
            show_point.text = Battle_Tool.FormatNumberToChineseUnit(list[1]) + " " + currency_unit.历练;
            show_diamond.text = Battle_Tool.FormatNumberToChineseUnit(list[2]) + " " + currency_unit.魔丸;
            base_info_Task();
        }
    }
    /// <summary>
    /// 显示任务信息
    /// </summary>
    private void base_info_Task()
    {
        string dec = "";
        if (SumSave.GreenhandGuide_TotalTasks.ContainsKey(SumSave.crt_greenhand.crt_task))
        {
            GreenhandGuide_TotalTaskVO task = SumSave.GreenhandGuide_TotalTasks[SumSave.crt_greenhand.crt_task];
            dec = task.TaskDesc + "(" + SumSave.crt_greenhand.crt_progress + "/" + task.progress + ")";
        }
        show_task.text = dec;
    }
}
