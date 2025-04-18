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
    private List<string> btn_list = new List<string>() { "����", "����" };
    private Transform pos_btn,pos_bag,pos_icon;
    /// <summary>
    /// ���ܰ�ť
    /// </summary>
    private btn_item btn_item_Prefabs;
    /// <summary>
    /// ����Ԥ����
    /// </summary>
    private bag_item bag_item_Prefabs;
    /// <summary>
    /// ǿ������
    /// </summary>
    private List<long> needs = new List<long> { 100, 1000, 10000, 100000, 100000, 1000000, 1000000, 10000000, 100000000, 1000000000, 2000000000, 3000000000, 3000000000, 3000000000 };
    /// <summary>
    /// ��ǰѡ��
    /// </summary>
    private bag_item crt_bag;
    /// <summary>
    /// ǿ��
    /// </summary>
    private Button  btn_strengthen;
    /// <summary>
    /// ��ʾ��Ϣ
    /// </summary>
    private Text info;
    /// <summary>
    /// ��ǰѡ��
    /// </summary>
    private int index;
    private void Awake()
    {
        pos_btn = Find<Transform>("btn_list");
        pos_bag = Find<Transform>("Scroll View/Viewport/Content");
        pos_icon = Find<Transform>("icon");
        btn_item_Prefabs = Resources.Load<btn_item>("Prefabs/base_tool/btn_item");
        bag_item_Prefabs = Resources.Load<bag_item>("Prefabs/panel_bag/bag_item");
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
    /// ǿ��
    /// </summary>
    private void Strengthen()
    {
        string[] infos = crt_bag.Data.user_value.Split(' ');
        int lv = int.Parse(infos[1]);
        if (lv >= crt_bag.Data.need_lv/10+3)
        { 
            Alert_Dec.Show("��ǰװ��ǿ���ȼ�����");
            return;
        }
        NeedConsumables(currency_unit.����, needs[lv]);
        if (RefreshConsumables())
        {
            crt_bag.Data.user_value = crt_bag.Data.user_value.Replace(infos[1], (lv + 1).ToString());
            Select_Strengthen(crt_bag);
            if (index == 0)
            {
                Game_Omphalos.i.Wirte_ResourcesList(Emun_Resources_List.equip_value, SumSave.crt_euqip);
            }
            else Game_Omphalos.i.Wirte_ResourcesList(Emun_Resources_List.bag_value, SumSave.crt_bag);
        }
        else Alert_Dec.Show(currency_unit.���� + "���� " + needs[lv]);
    }
    /// <summary>
    /// ѡ�����
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
    /// ��ʼ��
    /// </summary>
    private void Base_Show()
    {
        ClearObject(pos_bag);
        if (index == 0)
        {
            Show_Bag(SumSave.crt_euqip);
        }
        if (index == 1)
        {
            Show_Bag(SumSave.crt_bag);
        }
    }

    private void Show_Bag(List<Bag_Base_VO> list)
    {

        for (int i = 0; i < list.Count; i++)
        {
            switch ((EquipConfigTypeList)Enum.Parse(typeof(EquipConfigTypeList), list[i].StdMode))
            {
                case EquipConfigTypeList.����:
                case EquipConfigTypeList.�·�:
                case EquipConfigTypeList.ͷ��:
                case EquipConfigTypeList.����:
                case EquipConfigTypeList.����:
                case EquipConfigTypeList.��ָ:
                case EquipConfigTypeList.����:
                case EquipConfigTypeList.��ָ:
                case EquipConfigTypeList.����:
                case EquipConfigTypeList.ѥ��:
                //case EquipConfigTypeList.����:
                //case EquipConfigTypeList.�鱦:
                //case EquipConfigTypeList.ѫ��:
                //case EquipConfigTypeList.��Ʒ:
                //case EquipConfigTypeList.����:
                case EquipConfigTypeList.����:
                    bag_item item = Instantiate(bag_item_Prefabs, pos_bag);
                    item.Data = (list[i]);
                    item.GetComponent<Button>().onClick.AddListener(() => { Select_Strengthen(item); });
                    if (crt_bag == null) Select_Strengthen(item);
                    break;
                default:
                    break;
            }
        }
    }
    /// <summary>
    /// ѡ��ǿ��
    /// </summary>
    /// <param name="data"></param>
    private void Select_Strengthen(bag_item data)
    {
        crt_bag = data;
        ClearObject(pos_icon);
        Instantiate(bag_item_Prefabs, pos_icon).Data = data.Data;
        string[] infos = crt_bag.Data.user_value.Split(' ');
        int lv = int.Parse(infos[1]);
        info.text = "ǿ��" + data.Data.Name + "��Ҫ" + currency_unit.���� + needs[lv];
    }
}
