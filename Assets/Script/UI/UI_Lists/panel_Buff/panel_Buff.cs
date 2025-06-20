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


    /// <summary>
    /// 角色类型
    /// </summary>
    private enum_skin_state skin_state;
    /// <summary>
    /// 角色皮肤预制体
    /// </summary>
    private GameObject skin_prefabs;
    /// <summary>
    /// 角色within位置,五行属性显示位置
    /// </summary>
    private Transform panel_role_health,Five_element_transform;
    /// <summary>
    /// btn_item预制体
    /// </summary>
    private btn_item btn_item_prefabs;
    /// <summary>
    /// 五行类型
    /// </summary>
    private string[] five_element_type = { "土", "火", "水", "木", "金" };  

    /// <summary>
    /// 五行属性显示
    /// </summary>
    private List<btn_item> btn_items=new List<btn_item>();

    public override void Hide()
    {
        base.Hide();
    }

    public override void Initialize()
    {
        base.Initialize();
        inputField = Find<InputField>("bg_main/InputField");
        confirm = Find<Button>("bg_main/confirm");
        info = Find<Text>("bg_main/ScrollView/Viewport/Content/info");
        confirm.onClick.AddListener(OnConfirmClick);

       
        skin_prefabs = Resources.Load<GameObject>("Prefabs/Skins/within_" + SumSave.crt_hero.hero_pos);
        panel_role_health = Find<Transform>("bg_main/bg");

        Five_element_transform= Find<Transform>("bg_main/Five_element_attribute/Viewport/Content");
        btn_item_prefabs = Battle_Tool.Find_Prefabs<btn_item>("btn_item");

        Instantiate(skin_prefabs, panel_role_health);
        Display_Five_element_attribute();
    }
    /// <summary>
    /// 点击事件
    /// </summary>
    private void OnConfirmClick()
    {
        Alert.Show("提示", "确定将角色名称修改为\n" + Show_Color.Red(inputField.text) + " ？", Confirm);
    }
    /// <summary>
    /// 确认
    /// </summary>
    /// <param name="arg"></param>
    private void Confirm(object arg)
    { 
        SumSave.crt_hero.hero_name= inputField.text;
        SumSave.crt_hero.hero_material_list[0] = 1;
        SumSave.crt_hero.MysqlData();
        SendNotification(NotiList.Refresh_Max_Hero_Attribute);
        Show();
    }
    public override void Show()
    {
        base.Show();
        confirm.gameObject.SetActive(SumSave.crt_hero.hero_material_list[0] == 0 || SumSave.crt_hero.hero_name == "墨龙新星");
        inputField.text = SumSave.crt_hero.hero_name;
        InitInformation();
        Refresh_Five_element_attribute();
    }
    private void Update()
    {
        InitInformation();
    }

    /// <summary>
    /// 刷新五行属性
    /// </summary>
    private void Refresh_Five_element_attribute()
    {
        for(int i= 0; i < btn_items.Count; i++)
        {
            btn_items[i].Show(i, five_element_type[i] + "\n" + SumSave.crt_MaxHero.life[i].ToString());
        }
    }

    /// <summary>
    /// 初始化五行属性
    /// </summary>
    private void Display_Five_element_attribute()
    {
        ClearObject(Five_element_transform);

        int[] five_element = SumSave.crt_MaxHero.life;

        for(int i= 0; i < five_element.Length; i++)
        {
            btn_item item= Instantiate(btn_item_prefabs, Five_element_transform);
            item.Show(i, five_element_type[i]+"\n"+ five_element[i].ToString());  
            btn_items.Add(item);
        }
    }



    private void InitInformation()
    {
        string dec = " ";
        if (SumSave.crt_player_buff.player_Buffs.Count > 0)
        {
            foreach (var item in SumSave.crt_player_buff.player_Buffs)
            {
                (DateTime, int, float, int) time = item.Value;
                int remainingTime = Battle_Tool.SettlementTransport((time.Item1).ToString("yyyy-MM-dd HH:mm:ss"));
                if (time.Item4 == 3)
                {
                    if (remainingTime < time.Item2)
                    {
                        dec += Show_Color.Red(item.Key + ": 效果 经验值和灵珠值获取增加" + time.Item3 + "倍 剩余" + (time.Item2 - (SumSave.nowtime - time.Item1).Minutes) + "Min\n ");
                    }
                }

                if ( time.Item4==1||time.Item4 == 2)
                {
                    if (remainingTime < time.Item2)
                    {
                        dec += Show_Color.Red(item.Key + ": 效果" + time.Item3 + "倍 剩余" + (time.Item2 - remainingTime) + "Min\n ");
                    }
                }
            }
        }




        dec += enum_skill_attribute_list.经验加成 + ": " + Show_Buff(enum_skill_attribute_list.经验加成) + "%\n ";
        dec += enum_skill_attribute_list.人物历练 + ": " + Show_Buff(enum_skill_attribute_list.人物历练) + "%\n ";
        dec += enum_skill_attribute_list.灵珠收益 + ": " + Show_Buff(enum_skill_attribute_list.灵珠收益) + "%\n ";

        dec += enum_skill_attribute_list.装备爆率 + ": " + Show_Buff(enum_skill_attribute_list.装备爆率) + "%\n ";
        dec += enum_skill_attribute_list.装备掉落 + ": " + Show_Buff(enum_skill_attribute_list.装备掉落) + "%\n ";
        dec += enum_skill_attribute_list.宠物获取 + ": " + Show_Buff(enum_skill_attribute_list.宠物获取) + "%\n ";
        dec += enum_skill_attribute_list.寻怪间隔 + ": -" + (Show_Buff(enum_skill_attribute_list.寻怪间隔) / 10f) + "s\n ";
        dec += enum_skill_attribute_list.复活次数 + ": " + Show_Buff(enum_skill_attribute_list.复活次数) + "次\n ";


        info.text = dec;
    }

    /// <summary>
    /// 显示buff
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private float Show_Buff(enum_skill_attribute_list index)
    {
        float value = 0;
        if ((int)index < SumSave.crt_MaxHero.bufflist.Count)
            value = SumSave.crt_MaxHero.bufflist[(int)index];
        return value;
    }

    protected override void Awake()
    {
        base.Awake();
    }
}

