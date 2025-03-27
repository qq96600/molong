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
    private string[] btn_list = new string[] { "����", "����" };
    private string[] take_btn_list=new string[] { "ж��" };
    private Text show_name, show_base_need, show_info;
    private void Awake()
    {
        crt_bag=Find<Transform>("show_icon");
        crt_btn=Find<Transform>("btn_list");
        btn_item_Prefabs = Resources.Load<btn_item>("Prefabs/base_tool/btn_item");
        bag_item_Prefabs = Resources.Load<bag_item>("Prefabs/panel_bag/bag_item");
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
        //������Ʒ��ʾ
        Instantiate(bag_item_Prefabs, crt_bag).Data = data;
        show_name.text=data.Name;
        string[] info = data.user_value.Split(' ');
        int strengthenlv= int.Parse(info[1]);

        show_base_need.text = "Ʒ��:" + (enum_equip_quality_list)int.Parse(info[2]) + "\n" +
            "����:" + data.StdMode + "\n" +
            "����:" + data.need_lv + "��";
        string dec = Show_Color.Yellow("[��������]");
        
        if (data.damgemin > 0 || data.damagemax > 0)
        { 
            dec += "\n" + Show_Color.White("������:" + data.damgemin + "-" + data.damagemax);
            if (strengthenlv > 0)
            { 
                dec += Show_Color.Grey("(+" + (data.equip_lv * strengthenlv) + ")");
            }
        }
        if (data.magicmin > 0 || data.magicmax > 0)
        { 
            dec += "\n" + Show_Color.White("ħ������:" + data.magicmin + "-" + data.magicmax);
            if (strengthenlv > 0)
            { 
                dec += Show_Color.Grey("(+" + (data.equip_lv * strengthenlv) + ")");
            }
        }
        if (data.defmin > 0 || data.defmax > 0)
        {
            dec += "\n" + Show_Color.White("�������:" + data.defmin + "-" + data.defmax);
            if (strengthenlv > 0)
            {
                dec += Show_Color.Grey("(+" + (data.equip_lv * strengthenlv / 2) + ")");
            }
        }
        if (data.macdefmin > 0 || data.macdefmax > 0)
        {
            dec += "\n" + Show_Color.White("ħ������:" + data.macdefmin + "-" + data.macdefmax);
            if (strengthenlv > 0)
            {
                dec += Show_Color.Grey("(+" + (data.equip_lv * strengthenlv / 2) + ")");
            }
        }
        if (data.hp > 0)
        { 
            dec += "\n" + Show_Color.Green("����ֵ:  " + data.hp);
        }
        if (data.mp > 0)
        { 
            dec += "\n" + Show_Color.Green("ħ��ֵ:  " + data.mp);
        }
        if (info.Length > 4)
        {
            //����
            string[] arr = info[3].Split(',');
            //ֵ
            string[] arr_value = info[4].Split(',');
            dec += "\n" + Show_Color.Yellow("[��������]");
            int index = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                ++index;
                if (i == 0)
                {
                    switch (int.Parse(arr[i]))
                    {
                        case 1: dec += "\n" + Show_Color.Red("������: " + arr_value[i] + " - 0"); break;
                        case 2: dec += "\n" + Show_Color.Red("������: 0 -" + arr_value[i]); break;
                        case 3: dec += "\n" + Show_Color.Red("ħ������: " + arr_value[i]+" - 0"); break;
                        case 4: dec += "\n" + Show_Color.Red("ħ������: 0 -" + arr_value[i]); break;
                        case 5: dec += "\n" + Show_Color.Red("�������: " + arr_value[i]); break;
                        case 6: dec += "\n" + Show_Color.Red("ħ������: " + arr_value[i]); break;
                        case 7: dec += "\n" + Show_Color.Red("����ֵ:   " + arr_value[i]); break;
                        case 8: dec += "\n" + Show_Color.Red("ħ��ֵ:   " + arr_value[i]); break;
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
                            dec += "\n" + Show_Color.Yellow("[���мӳ�]");
                        }
                        dec += "\n" + Show_Color.Purple((enum_skill_attribute_list)(int.Parse(arr[i])) + ":" + arr_value[i]);

                    }
                    else
                    if (index >= 4)
                    {
                        if (index==4)
                        {
                            dec += "\n" + Show_Color.Yellow("[�ӳ�����]");
                        }
                        dec += "\n" + Show_Color.Blue((enum_skill_attribute_list)(int.Parse(arr[i])) + ":" + arr_value[i] +"%");
                    }else
                        dec += "\n" + Show_Color.Green((enum_skill_attribute_list)(int.Parse(arr[i])) + ":" + arr_value[i] + "");


                }

            }
        }
        show_info.text = dec;

    }
    /// <summary>
    /// �ж��Ƿ��п���
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
    /// <summary>
    /// ����
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
    /// ����
    /// </summary>
    /// <param name="item"></param>
    private void OnTake_Btn(btn_item item)
    {
        transform.parent.parent.SendMessage("OnTake_Btn", item.index);
    }

    /// <summary>
    /// �����ť
    /// </summary>
    /// <param name="item"></param>
    private void OnClick_Btn(btn_item item)
    {
        transform.parent.parent.SendMessage("OnClick_Btn", item.index);
    }
}
