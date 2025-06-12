using Common;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class equip_item : Base_Mono
{
    private Image item_icon;

    private Transform crt_bag,crt_btn;

    private bag_item bag_item_Prefabs;

    private btn_item btn_item_Prefabs;
    private string[] btn_list = new string[] { "穿戴","锁定", "出售" };
    private string[] take_btn_list=new string[] { "卸下" };
    private string[] warehouse_btn_list = new string[] { "放入"};
    private Text show_name, show_base_need, show_info;
    private void Awake()
    {
        crt_bag=Find<Transform>("show_icon");
        crt_btn=Find<Transform>("btn_list");
        btn_item_Prefabs = Battle_Tool.Find_Prefabs<btn_item>("btn_item"); //Resources.Load<btn_item>("Prefabs/base_tool/btn_item");
        bag_item_Prefabs = Battle_Tool.Find_Prefabs<bag_item>("bag_item"); //Resources.Load<bag_item>("Prefabs/panel_bag/bag_item");
        show_name = Find<Text>("show_name/info");
        show_base_need = Find<Text>("show_base_need");
        show_info = Find<Text>("show_info");
    }


    private Bag_Base_VO data;

    /// <summary>
    /// Data
    /// </summary>
    public Bag_Base_VO Data
    {
        set
        {
            data = value;
            if (data == null) return;
            base_init();
        }
        get
        {
            return data;
        }
    }

    private void base_init()
    {
        //生成物品显示
        if(bag_item_Prefabs==null||crt_bag==null)
        {
            Awake();
        }
        ClearObject(crt_bag);
        Instantiate(bag_item_Prefabs, crt_bag).Data = data;
        show_name.text=data.Name;
        string[] info = data.user_value.Split(' ');
        int strengthenlv= int.Parse(info[1]);

        show_base_need.text = "品质:" + (enum_equip_quality_list)int.Parse(info[2]) + "\n" +
            "类型:" + data.StdMode + "\n" +
            "需求:" + data.equip_lv + "级";
        string dec = Show_Color.Yellow("[基础属性]");
        
        if (data.damgemin > 0 || data.damagemax > 0)
        { 
            dec += "\n" + Show_Color.White("物理攻击:" + data.damgemin + "-" + data.damagemax);
            if (strengthenlv > 0)
            { 
                dec += Show_Color.Grey("(+" + (data.need_lv * strengthenlv) + ")");
            }
        }
        if (data.magicmin > 0 || data.magicmax > 0)
        { 
            dec += "\n" + Show_Color.White("魔法攻击:" + data.magicmin + "-" + data.magicmax);
            if (strengthenlv > 0)
            { 
                dec += Show_Color.Grey("(+" + (data.need_lv * strengthenlv) + ")");
            }
        }
        if (data.defmin > 0 || data.defmax > 0)
        {
            dec += "\n" + Show_Color.White("物理防御:" + data.defmin + "-" + data.defmax);
            if (strengthenlv > 0)
            {
                dec += Show_Color.Grey("(+" + (data.need_lv * strengthenlv / 2) + ")");
            }
        }
        if (data.macdefmin > 0 || data.macdefmax > 0)
        {
            dec += "\n" + Show_Color.White("魔法防御:" + data.macdefmin + "-" + data.macdefmax);
            if (strengthenlv > 0)
            {
                dec += Show_Color.Grey("(+" + (data.need_lv * strengthenlv / 2) + ")");
            }
        }
        if (data.hp > 0)
        { 
            dec += "\n" + Show_Color.White("生命值:  " + data.hp);
        }
        if (data.mp > 0)
        { 
            dec += "\n" + Show_Color.White("魔法值:  " + data.mp);
        }
        if (info.Length > 4)
        {
            //类型
            string[] arr = info[3].Split(',');
            //值
            string[] arr_value = info[4].Split(',');
            dec += "\n" + Show_Color.Yellow("[附加属性]");
            int index = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                ++index;
                if (i == 0)
                {
                    switch (int.Parse(arr[i]))
                    {
                        case 1: dec += "\n" + Show_Color.Red("物理攻击: " + arr_value[i] + " - 0"); break;
                        case 2: dec += "\n" + Show_Color.Red("物理攻击: 0 -" + arr_value[i]); break;
                        case 3: dec += "\n" + Show_Color.Red("魔法攻击: " + arr_value[i]+" - 0"); break;
                        case 4: dec += "\n" + Show_Color.Red("魔法攻击: 0 -" + arr_value[i]); break;
                        case 5: dec += "\n" + Show_Color.Red("物理防御: " + arr_value[i]); break;
                        case 6: dec += "\n" + Show_Color.Red("魔法防御: " + arr_value[i]); break;
                        case 7: dec += "\n" + Show_Color.Red("生命值:   " + arr_value[i]); break;
                        case 8: dec += "\n" + Show_Color.Red("魔法值:   " + arr_value[i]); break;
                        default:
                            break;
                    }
                }
                else
                {
                    if (index >= 7)
                    {
                        if (index==7)
                        {
                            dec += "\n" + Show_Color.Yellow("[五行加成]");
                        }
                        dec += "\n" + Show_Color.Purple((enum_skill_attribute_list)(int.Parse(arr[i])) + ":" + arr_value[i]);

                    }
                    else
                    if (index >= 4)
                    {
                        if (index==4)
                        {
                            dec += "\n" + Show_Color.Yellow("[加成属性]");
                        }
                        dec += "\n" + Show_Color.Red((enum_skill_attribute_list)(int.Parse(arr[i])) + ":" + arr_value[i] +"%");
                    }else
                        dec += "\n" + Show_Color.Orange((enum_skill_attribute_list)(int.Parse(arr[i])) + ":" + arr_value[i] + "");


                }

            }
        }

        if (Data.suit !=0)
        {
            dec+= Show_Suit();
        }
        show_info.text = dec;

    }

    private string Show_Suit()
    {
        string dec = "";
        int number = 0;
        db_suit_vo suit = SumSave.db_suits.Find(x => x.suit_type == Data.suit);
        foreach (Bag_Base_VO item in SumSave.crt_euqip)
        {
            if (item.suit == data.suit)
            {
                foreach (var value in SumSave.db_suits)
                {
                    if (item.suit == value.suit_type)
                    {
                        number++;
                    }
                }
            }
        }
        dec+="\n" + Show_Color.Yellow("[套装] "+suit.suit_name)+"("+number+"/"+suit.suit_number+")";
        for (int i = 0; i < suit.suit_list.Count; i++)
        {
            if (number >= suit.suit_list[i].Item1)
            {
                dec += "\n" + (enum_skill_attribute_list)suit.suit_list[i].Item2 + " : " + Math.Abs(suit.suit_list[i].Item3) + tool_Categoryt.Obtain_unit(suit.suit_list[i].Item2);
            }
            else
                dec += "\n" + Show_Color.Grey((enum_skill_attribute_list)suit.suit_list[i].Item2 + " : " + Math.Abs(suit.suit_list[i].Item3) + tool_Categoryt.Obtain_unit(suit.suit_list[i].Item2));
        }
        return dec;
    }

    /// <summary>
    /// 判断是否有开关
    /// </summary>
    public void Show_Info_Btn()
    {
        for (int i = 0; i < btn_list.Length; i++)
        {
            btn_item item = Instantiate(btn_item_Prefabs, crt_btn);
            item.Show(i, btn_list[i]);
            item.GetComponent<Button>().onClick.AddListener(() => { OnClick_Btn(item); });
        }
    }
    public void Show_House_Btn(bool ishouse)
    {
        ClearObject(crt_btn);
        string house = ishouse ? "存入" : "取出";
        btn_item item = Instantiate(btn_item_Prefabs, crt_btn);
        item.Show(0, house);
        item.GetComponent<Button>().onClick.AddListener(() => {
            if (ishouse) OnHousedeposit_Btn(item);
            else
            {
                OnTakeOut_Btn(item);
            }  
        });
    }
    /// <summary>
    ///取出
    /// </summary>
    /// <param name="item"></param>
    private void OnTakeOut_Btn(btn_item item)
    {
        transform.parent.SendMessage("OnTakeOut_Btn",Data);
    }
    /// <summary>
    /// 存入
    /// </summary>
    /// <param name="item"></param>
    private void OnHousedeposit_Btn(btn_item item)
    {
        transform.parent.SendMessage("OnHousedeposit_Btn", Data);

    }

    /// <summary>
    /// 脱下
    /// </summary>
    public void Show_take_Btn()
    {
        for (int i = 0; i < take_btn_list.Length; i++)
        {
            btn_item item = Instantiate(btn_item_Prefabs, crt_btn);
            item.Show(i, take_btn_list[i]);
            item.GetComponent<Button>().onClick.AddListener(() => { OnTake_Btn(item); });
        }
    }
    /// <summary>
    /// 仓库转移
    /// </summary>
    public void Show_warehouse()
    {
        for (int i = 0; i < warehouse_btn_list.Length; i++)
        {
            btn_item item = Instantiate(btn_item_Prefabs, crt_btn);
            item.Show(i, warehouse_btn_list[i]);
            item.GetComponent<Button>().onClick.AddListener(() => { warehouseTransfer(); });
        }
    }
    /// <summary>
    /// 仓库转移
    /// </summary>
    private void warehouseTransfer()
    {
        //transform.parent();
    }


    /// <summary>
    /// 脱下
    /// </summary>
    /// <param name="item"></param>
    private void OnTake_Btn(btn_item item)
    {
        transform.parent.parent.SendMessage("OnTake_Btn", item.index);
    }

    /// <summary>
    /// 点击按钮
    /// </summary>
    /// <param name="item"></param>
    private void OnClick_Btn(btn_item item)
    {
        transform.parent.parent.SendMessage("OnClick_Btn", item.index);
    }
}
