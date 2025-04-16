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

public class panel_bag : Panel_Base
{
    /// <summary>
    /// װ������
    /// </summary>
    private Dictionary<EquipTypeList, show_equip_item> dic_equips = new Dictionary<EquipTypeList, show_equip_item>();
    /// <summary>
    /// ���ܰ�ť
    /// </summary>
    private btn_item btn_item_Prefabs;

    private bag_item bag_item_Prefabs;

    private material_item material_item_Prefabs;
    /// <summary>
    /// ��ťλ��
    /// </summary>
    private Transform crt_btn, crt_bag;

    private panel_equip panel_equip;

    private bag_btn_list crt_select_btn = bag_btn_list.װ��;
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Initialize()
    {
        base.Initialize();
        crt_btn = Find<Transform>("bg_main/show_bag/btns");
        crt_bag = Find<Transform>("bg_main/show_bag/Scroll View/Viewport/Content");
        btn_item_Prefabs = Resources.Load<btn_item>("Prefabs/base_tool/btn_item");
        bag_item_Prefabs = Resources.Load<bag_item>("Prefabs/panel_bag/bag_item");
        material_item_Prefabs= Resources.Load<material_item>("Prefabs/panel_bag/material_item"); 
        panel_equip = UI_Manager.I.GetPanel<panel_equip>();
        for (int i = 0; i < Enum.GetNames(typeof(bag_btn_list)).Length; i++)
        {
            btn_item btn_item = Instantiate(btn_item_Prefabs, crt_btn);//ʵ��������װ��
            btn_item.Show(i, (bag_btn_list)i);
            btn_item.GetComponent<Button>().onClick.AddListener(delegate { Select_Btn(btn_item); });
        }
    }
    /// <summary>
    /// ѡ�й���
    /// </summary>
    /// <param name="btn_item"></param>
    private void Select_Btn(btn_item btn_item)
    {
        crt_select_btn = (bag_btn_list)btn_item.index;
        Show_Bag();
        Alert_Dec.Show("�� " + crt_select_btn + " �б�");
    }

    /// <summary>
    /// ��ʼ��λ��
    /// </summary>
    /// <param name="item"></param>
    protected void Instance_Pos(show_equip_item item)
    {
        if (!dic_equips.ContainsKey(item.type)) dic_equips.Add(item.type, item);
    }

    public override void Show()
    {
        base.Show();
        Show_Bag();
        Base_Show();
        base_Equip();
       
    }
    /// <summary>
    /// ��ʾװ��
    /// </summary>
    private void base_Equip()
    {
        foreach (EquipTypeList item in dic_equips.Keys)
        {
            dic_equips[item].Init();
            foreach (Bag_Base_VO equip in SumSave.crt_euqip)
            {
                if (equip.StdMode == item.ToString())
                {
                    
                    dic_equips[item].Data = equip;
                }
            }
        }
    }

    /// <summary>
    /// ��ʾ����װ���б�
    /// </summary>
    private void Base_Show()
    {
        foreach (var item in dic_equips.Keys)
        {
            dic_equips[item].Init();

            foreach (Bag_Base_VO equip in SumSave.crt_euqip)
            {
                if (equip.StdMode == item.ToString())
                {
                    dic_equips[item].Data = equip;
                }
            }
            
        }
    }
    /// <summary>
    /// ��ʾ������Ʒ
    /// </summary>
    private void Show_Bag()
    {
        ClearObject(crt_bag);
        switch (crt_select_btn)
        {
            case bag_btn_list.װ��:
                for (int i = 0; i < SumSave.crt_bag.Count; i++)
                {
                    bag_item item = Instantiate(bag_item_Prefabs, crt_bag);
                    item.Data = SumSave.crt_bag[i];
                    item.GetComponent<Button>().onClick.AddListener(delegate { Select_Bag(item); });
                }
                break;
            case bag_btn_list.����:
                List<(string, int)> lists = SumSave.crt_bag_resources.Set();
                for (int i = 0; i < lists.Count; i++)
                {
                    (string,int) data = lists[i];
                    if (data.Item2 > 0)
                    {
                        material_item item = Instantiate(material_item_Prefabs, crt_bag);
                        item.Init(data);
                        item.GetComponent<Button>().onClick.AddListener(delegate { Select_Material(item); });
                    }
                    
                }
                break;
            case bag_btn_list.����Ʒ:
                List<(string, List<string>)> list = SumSave.crt_seeds.GetSeedList();
                for (int i = 0; i < list.Count; i++)
                {
                    (string, int) data =(list[i].Item1,1);
                    
                    material_item item = Instantiate(material_item_Prefabs, crt_bag);

                    item.Init(data);
                    item.GetComponent<Button>().onClick.AddListener(delegate { Select_Material(item); });
                    

                }
                break;
            default:
                break;
        }
        
    }
    /// <summary>
    /// ѡ�����
    /// </summary>
    /// <param name="item"></param>
    private void Select_Material(material_item item)
    {
        panel_equip.Show();
        panel_equip.Select_Material(item);
    }

    /// <summary>
    /// ѡ����Ʒ
    /// </summary>
    /// <param name="item"></param>
    private void Select_Bag(bag_item item)
    {
        panel_equip.Show();
        panel_equip.Init(item);
       
    }
    /// <summary>
    /// ��ʾ����װ��
    /// </summary>
    /// <param name="item"></param>
    protected void Select_Equip(bag_item item)
    {
        panel_equip.Show();
        panel_equip.Select_Equip(item);

    }
}
