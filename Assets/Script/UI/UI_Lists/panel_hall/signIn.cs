using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityEngine.UI;
using System;
using Components;
using UI;

public class signIn : Base_Mono
{
    /// <summary>
    /// 签到列表
    /// </summary>
    private Transform pos_list;
    /// <summary>
    /// 签到列表
    /// </summary>
    private signln_item signln_item_prafabs;
    /// <summary>
    /// 签到信息
    /// </summary>
    private Text info;
    /// <summary>
    /// 签到按钮
    /// </summary>
    private Button btn_signln,max_signln;

    private Panel_Accumulatedrewards panel_accumulatedrewards;

    private void Awake()
    {
        pos_list=Find<Transform>("Scroll View/Viewport/Content");
        signln_item_prafabs = Battle_Tool.Find_Prefabs<signln_item>("signln_item"); //Resources.Load<signln_item>("prefabs/panel_hall/signIn/signln_item");
        info = Find<Text>("bg/info");
        btn_signln = Find<Button>("btn_signln");
        btn_signln.onClick.AddListener(OnClick_signln);
        max_signln=Find<Button>("btn_maxsignln");
        max_signln.onClick.AddListener(OnClick_maxsignln);
        panel_accumulatedrewards = UI_Manager.I.GetPanel<Panel_Accumulatedrewards>();
        ClearObject(pos_list);
        List<int> list = SumSave.crt_signin.Set();
        for (int i = 0; i < SumSave.db_Signins.Count; i++)
        {
            signln_item item = Instantiate(signln_item_prafabs, pos_list);
            while (i >= list.Count)
            {
                list.Add(0);
            }
            item.Init(i, SumSave.db_Signins[i], list[i]);
            item.GetComponent<Button>().onClick.AddListener(() => { OnClick(item); });
        }
    }

    private void OnClick_maxsignln()
    {
        panel_accumulatedrewards.Show();
        panel_accumulatedrewards.Init(2);
    }

    /// <summary>
    /// 每日签到
    /// </summary>
    private void OnClick_signln()
    {
        if ((SumSave.nowtime - SumSave.crt_signin.now_time).Days >= 1)
        {
            SumSave.crt_signin.now_time = Convert.ToDateTime(SumSave.nowtime.ToString("yyyy-MM-dd"));
            SumSave.crt_signin.number++;
            SumSave.crt_signin.max_number++;
            Clear();
            Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_signin, SumSave.crt_signin.Set_Uptade_String(),
                SumSave.crt_signin.Get_Update_Character());
            Alert_Dec.Show("签到成功");
            SumSave.crt_pass.clear_data();
            SumSave.crt_user_unit.verify_data(currency_unit.灵珠, 1000000 * SumSave.crt_signin.number);
            
            MonthlyCardRewards(3);
            SignInTask();
            base_show();
        }
        else Alert_Dec.Show("今日已签到");
    }
    /// <summary>
    /// 月卡签到获得奖励
    /// </summary>
    /// <param name="index"></param>
    private void MonthlyCardRewards(int index)
    {
        foreach (var item in SumSave.crt_player_buff.player_Buffs)
        {
            (DateTime, int, float, int) time = item.Value;
            if (index == time.Item4)
            {
                if ((SumSave.nowtime - time.Item1).Minutes < time.Item2)
                {
                    SumSave.crt_user_unit.verify_data(currency_unit.魔丸, 30);
                    SumSave.crt_accumulatedrewards.Set(2, 4);
                }
            }
            else
            {
                SumSave.crt_accumulatedrewards.Set(2, 2);//不是月卡给2点荣耀点
            }
        }
    }

        /// <summary>
        /// 完成签到任务
        /// </summary>
    private void SignInTask()
    {
        tool_Categoryt.Base_Task(1026);
    }

    /// <summary>
    /// 重置需求
    /// </summary>
    private void Clear()
    {
        //累积vip经验
        //SumSave.crt_accumulatedrewards.Set(2, 2);
        //限购商店,地图次数刷新
        SumSave.crt_needlist.DailyClear();
    }

    /// <summary>
    /// 点击事件
    /// </summary>
    /// <param name="item"></param>
    private void OnClick(signln_item item)
    {
        int index = item.Set();
        List<int> list = SumSave.crt_signin.Set();
        if (index<= list.Count && list[index] == 1)
        {
            Alert_Dec.Show("已领取奖励");
            return;
        } 
        db_signin_vo vo = SumSave.db_Signins[index];
        if (SumSave.crt_signin.number < vo.index)
        {
            Alert_Dec.Show("未满足领取条件") ;
            return;
        }
        string[] strs = vo.value.Split('*');
        string dec = strs[0] + "*" + strs[1];
        Alert.Show("领取奖励", "确认是否领取" + dec, Confirmreceipt, item);
    }
    /// <summary>
    /// 领取奖励
    /// </summary>
    /// <param name="arg0"></param>
    private void Confirmreceipt(object arg0)
    {
        signln_item item = arg0 as signln_item;
        int index = item.Set();
        Battle_Tool.Obtain_result(SumSave.db_Signins[index].value);
        Alert_Dec.Show("领取成功");
        SumSave.crt_signin.Set(index);
        item.Init(index, SumSave.db_Signins[index], 1);
        Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_signin, SumSave.crt_signin.Set_Uptade_String(),
    SumSave.crt_signin.Get_Update_Character());
    }

    public override void Show()
    {
        base.Show();
        base_show();
    }
    /// <summary>
    /// 显示基础信息
    /// </summary>
    private void base_show()
    {
        string dec = "签到奖励";
        dec += "\n累积签到天数 " + "* " + SumSave.crt_signin.number + " 天";
        dec += "\n签到获得" + currency_unit.灵珠 + "* " + (1000000 * (SumSave.crt_signin.number + 1));
        info.text = dec;
    }
}
