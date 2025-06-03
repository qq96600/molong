using Common;
using Components;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class offect_strengthen : Base_Mono
{
    private List<string> btn_list = new List<string>() { "穿戴", "背包" };
    private Transform pos_btn,pos_bag,pos_icon;
    /// <summary>
    /// 功能按钮
    /// </summary>
    private btn_item btn_item_Prefabs;
    /// <summary>
    /// 背包预制体
    /// </summary>
    private bag_item bag_item_Prefabs;
    /// <summary>
    /// 强化费用
    /// </summary>
    private List<long> needs = new List<long> { 100, 1000, 10000, 100000, 100000, 1000000, 1000000, 10000000, 100000000, 1000000000, 2000000000, 3000000000, 3000000000, 3000000000 };
    /// <summary>
    /// 当前选择
    /// </summary>
    private bag_item crt_bag;
    /// <summary>
    /// 强化
    /// </summary>
    private Button  btn_strengthen;
    /// <summary>
    /// 显示信息
    /// </summary>
    private Text info;
    /// <summary>
    /// 当前选择
    /// </summary>
    private int index;
    private void Awake()
    {
        pos_btn = Find<Transform>("btn_list");
        pos_bag = Find<Transform>("Scroll View/Viewport/Content");
        pos_icon = Find<Transform>("icon");
        btn_item_Prefabs = Battle_Tool.Find_Prefabs<btn_item>("btn_item"); //Resources.Load<btn_item>("Prefabs/base_tool/btn_item");
        bag_item_Prefabs = Battle_Tool.Find_Prefabs<bag_item>("bag_item"); //Resources.Load<bag_item>("Prefabs/panel_bag/bag_item");
        btn_strengthen = Find<Button>("btn_strengthen");
        info = Find<Text>("info");
        btn_strengthen.onClick.AddListener(Strengthen);
        for (int i = 0; i < btn_list.Count; i++)
        {
            btn_item item = Instantiate(btn_item_Prefabs, pos_btn);
            item.Show(i, btn_list[i]);
            item.GetComponent<Button>().onClick.AddListener(() => { Select_Btn(item.index); });
        }
    }
    /// <summary>
    /// 强化
    /// </summary>
    private void Strengthen()
    {
        string[] infos = crt_bag.Data.user_value.Split(' ');
        int lv = int.Parse(infos[1]);
        if (lv >= crt_bag.Data.need_lv/10+3)
        { 
            Alert_Dec.Show("当前装备强化等级已满");
            return;
        }
        NeedConsumables(currency_unit.灵珠, needs[lv]);
        if (RefreshConsumables())
        {
            infos[1]= (lv + 1).ToString();
            crt_bag.Data.user_value = Battle_Tool.Equip_User_Value(infos);// crt_bag.Data.user_value.Replace(infos[1], (lv + 1).ToString());
            Select_Strengthen(crt_bag);
            if (index == 0)
            {
                Game_Omphalos.i.Wirte_ResourcesList(Emun_Resources_List.equip_value, SumSave.crt_euqip);
            }
            else Game_Omphalos.i.Wirte_ResourcesList(Emun_Resources_List.bag_value, SumSave.crt_bag);

            EquipmentEnhancementTask();
        }
        else Alert_Dec.Show(currency_unit.灵珠 + "不足 " + needs[lv]);
    }
    /// <summary>
    /// 装备强化任务
    /// </summary>
    private void EquipmentEnhancementTask()
    {
        tool_Categoryt.Base_Task(1027);
        tool_Categoryt.Base_Task(1037);
        tool_Categoryt.Base_Task(1041);
    }

        /// <summary>
        /// 选择对象
        /// </summary>
        /// <param name="index"></param>
    private void Select_Btn(object index)
    {
        this.index = (int)index;
        Base_Show();
    }

    public override void Show()
    {
        base.Show();
        Base_Show();
    }
    /// <summary>
    /// 初始化
    /// </summary>
    private void Base_Show()
    {
        ClearObject(pos_bag);
        if (index == 0)
        {
            if (Show_Bag(SumSave.crt_euqip))
            {
                Alert_Dec.Show("当前无可强化装备");
            }
        }
        if (index == 1)
        {
            if (Show_Bag(SumSave.crt_bag))
            {
                Alert_Dec.Show("当前无可强化装备");
            }
        }
    }

    private bool Show_Bag(List<Bag_Base_VO> list)
    {
        bool exist = true;
        for (int i = 0; i < list.Count; i++)
        {
            switch ((EquipConfigTypeList)Enum.Parse(typeof(EquipConfigTypeList), list[i].StdMode))
            {
                case EquipConfigTypeList.武器:
                case EquipConfigTypeList.衣服:
                case EquipConfigTypeList.头盔:
                case EquipConfigTypeList.项链:
                case EquipConfigTypeList.护臂:
                case EquipConfigTypeList.戒指:
                case EquipConfigTypeList.手镯:
                case EquipConfigTypeList.扳指:
                case EquipConfigTypeList.腰带:
                case EquipConfigTypeList.靴子:
                //case EquipConfigTypeList.护符:
                //case EquipConfigTypeList.灵宝:
                //case EquipConfigTypeList.勋章:
                //case EquipConfigTypeList.饰品:
                //case EquipConfigTypeList.玉佩:
                case EquipConfigTypeList.披风:
                    exist = false;
                    bag_item item = Instantiate(bag_item_Prefabs, pos_bag);
                    item.Data = (list[i]);
                    item.GetComponent<Button>().onClick.AddListener(() => { Select_Strengthen(item); });
                    if (crt_bag == null) Select_Strengthen(item);
                    break;
                default:
                    break;
            }
        }

        return exist;
        
    }
    /// <summary>
    /// 选择强化
    /// </summary>
    /// <param name="data"></param>
    private void Select_Strengthen(bag_item data)
    {
        if(data!=null)
        {
            crt_bag = data;
        }
       
        ClearObject(pos_icon);
        Instantiate(bag_item_Prefabs, pos_icon).Data = data.Data;
        string[] infos = crt_bag.Data.user_value.Split(' ');
        int lv = int.Parse(infos[1]);
        info.text = "强化" + data.Data.Name + "需要" + currency_unit.灵珠 + needs[lv];
    }
}
