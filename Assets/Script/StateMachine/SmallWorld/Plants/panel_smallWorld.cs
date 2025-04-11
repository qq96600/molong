using Common;
using Components;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class panel_smallWorld : Panel_Base
{
    /// <summary>
    /// 种植界面
    /// </summary>
    private panel_plant _plant;
    /// <summary>
    /// 孵化界面
    /// </summary>
    private Pet_Hatching _Hatching;
    /// <summary>
    /// 宠物探索位置
    /// </summary>
    private Pet_explore _explore;

    /// <summary>
    /// 按钮位置
    /// </summary>
    private Transform pos_btn;
    /// <summary>
    /// 功能按钮
    /// </summary>
    private btn_item btn_item_Prefabs;
    /// <summary>
    /// 背景
    /// </summary>
    private Image small_World_bg;
    /// <summary>
    /// 显示值
    /// </summary>
    private Text base_info;
    private string[] btn_list = new string[] { "升级", "农庄", "灵宠", "探险" };
    public override void Hide()
    {
        if (small_World_bg.gameObject.activeInHierarchy)//从最上层关闭
        {
            if (_plant.gameObject.activeInHierarchy) _plant.Hide();
            if(_Hatching.gameObject.activeInHierarchy) _Hatching.Hide();
            if (_explore.gameObject.activeInHierarchy) _explore.Hide();

            small_World_bg.gameObject.SetActive(false);
        }else
        base.Hide();
    }

    public override void Initialize()
    {
        base.Initialize();
        small_World_bg = Find<Image>("small_World");
        _plant=Find<panel_plant>("small_World/Panel_plant");
        _Hatching = Find<Pet_Hatching>("small_World/Pet_Hatching");
        _explore = Find<Pet_explore>("small_World/Pet_explore");
        pos_btn=Find<Transform>("bg_main/btn_list");
        base_info = Find<Text>("bg_main/base_info/info");
        btn_item_Prefabs = Resources.Load<btn_item>("Prefabs/base_tool/btn_item");
        for (int i = 0; i < btn_list.Length; i++)
        {
            btn_item btn_item = Instantiate(btn_item_Prefabs, pos_btn);//实例化背包装备
            btn_item.Show(i, btn_list[i]);
            btn_item.GetComponent<Button>().onClick.AddListener(delegate { Select_Btn(btn_item); });
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="btn_item"></param>
    private void Select_Btn(btn_item btn_item)
    {
        small_World_bg.gameObject.SetActive(true);
        switch (btn_list[btn_item.index])
        { 
            case "升级":
                small_World_bg.gameObject.SetActive(false);
                uplv();
                break;
            case "农庄":
                _plant.Show();
                break;
            case "灵宠":
                _Hatching.Show();
                break;
            case "探险":
                _explore.Show();
                break;
        
        }
    }
    /// <summary>
    /// 升级
    /// </summary>
    private void uplv()
    {
        if (SumSave.crt_world.World_Lv >= SumSave.db_lvs.world_lv_list.Count)
        { 
            Alert_Dec.Show("已达最高等级");
            return;
        }
        (string,int) dec = SumSave.db_lvs.world_lv_list[SumSave.crt_world.World_Lv];
        NeedConsumables(dec.Item1, dec.Item2);
        if (RefreshConsumables())
        { 
            SumSave.crt_world.World_Lv++;
            Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user_world, SumSave.crt_world.Set_Uptade_String(), SumSave.crt_world.Get_Update_Character());
        }
    }

    public override void Show()
    {
        base.Show();
        if (SumSave.crt_world == null)
        {
            Alert_Dec.Show("小世界未激活");
            Hide();
        }
        Base_Show();
    }

    private void Base_Show()
    {
        List<string> list = new List<string>();
        list = SumSave.crt_world.Get();
        int time = (int)(SumSave.nowtime - Convert.ToDateTime(list[0])).TotalMinutes;
        string dec = "界灵：Lv." + SumSave.crt_world.World_Lv + "\n";
        dec += "灵气 ：" + Obtain_Init(1,time,int.Parse(list[1])) + "(Max" + Obtain_Init(2) + ")\n";
        dec += "每分钟可获得 ：" + SumSave.db_lvs.world_offect_list[SumSave.crt_world.World_Lv]+  "灵气\n";
        dec += "历练获得 :" + (SumSave.crt_world.World_Lv * 10) + "%";
        base_info.text = dec;
    }
    /// <summary>
    /// 获取灵气值
    /// </summary>
    /// <param name="time"></param>
    private int Obtain_Init(int type,int time=0,int crt_value=0)
    {
       
        int value = 0;
        switch (type)
        {
            case 1:
                ///判断越界
                ArrayHelper.SafeGet(SumSave.db_lvs.world_offect_list, SumSave.crt_world.World_Lv, out value);
                value = time * SumSave.db_lvs.world_offect_list[SumSave.crt_world.World_Lv];
                value+= crt_value;
                value = Mathf.Min(value, SumSave.db_lvs.word_lv_max_value[SumSave.crt_world.World_Lv]);
                break;
            case 2:
                value = SumSave.db_lvs.word_lv_max_value[SumSave.crt_world.World_Lv];
                break;
            default:
                break;
        }

        return value;
    }
    
    protected override void Awake()
    {
        base.Awake();
    }


    

}
