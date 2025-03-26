using Common;
using MVC;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
/// <summary>
/// չʾװ���Ľ���
/// </summary>
public class panel_equip : Panel_Base
{

    private equip_item equip_item_prafabs;
    private Transform crt_pos_equip;
    private panel_bag panel_bag;
    private bag_item crt_bag;
    private Bag_Base_VO crt_equip;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Initialize()
    {
        base.Initialize();
        equip_item_prafabs = Resources.Load<equip_item>("Prefabs/panel_equip/equip_item"); 
        crt_pos_equip = Find<Transform>("bg_main");
        panel_bag=UI_Manager.I.GetPanel<panel_bag>();
    }

    public override void Show()
    {
        base.Show();
    }
    /// <summary>
    /// ���ݲ���
    /// </summary>
    /// <param name="bag"></param>
    public void Init(bag_item bag)
    {
        for (int i = crt_pos_equip.childCount - 1; i >= 0; i--)
        {
            Destroy(crt_pos_equip.GetChild(i).gameObject);
        }
        crt_bag = bag;
        equip_item item = Instantiate(equip_item_prafabs, crt_pos_equip);
        item.Data = bag.Data;
        item.Show_Info_Btn();

        foreach (var equip in SumSave.crt_euqip)
        {
            if (equip.StdMode == crt_bag.Data.StdMode)
            {
                equip_item equip_item = Instantiate(equip_item_prafabs, crt_pos_equip);
                equip_item.Data = equip;
                crt_equip = equip;
                equip_item.Show_take_Btn();
            }
        }
        
    }
    /// <summary>
    /// ��ʾװ��
    /// </summary>
    /// <param name="bag"></param>
    public void Select_Equip(bag_item bag)
    {
        for (int i = crt_pos_equip.childCount - 1; i >= 0; i--)
        {
            Destroy(crt_pos_equip.GetChild(i).gameObject);
        }
        crt_equip = bag.Data;
        equip_item item = Instantiate(equip_item_prafabs, crt_pos_equip);
        item.Data = bag.Data;
        item.Show_take_Btn();
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="index"></param>
    protected void OnTake_Btn(int index)
    {
        if (index == 0)
        {
            SumSave.crt_bag.Add(crt_equip);
            SumSave.crt_euqip.Remove(crt_equip);
            Game_Omphalos.i.Wirte_ResourcesList(Emun_Resources_List.bag_value, SumSave.crt_bag);
            Game_Omphalos.i.Wirte_ResourcesList(Emun_Resources_List.equip_value, SumSave.crt_euqip);
        }
        Refresh();
    }
    /// <summary>
    /// ����¼� 0 ���� 1����
    /// </summary>
    /// <param name="data"></param>
    protected void OnClick_Btn(int index)
    {
        //����
        if (index == 0)
        {
            List<Bag_Base_VO> euqip = new List<Bag_Base_VO>();
            foreach (var item in SumSave.crt_euqip)
            {
                if (item.StdMode == crt_bag.Data.StdMode)
                {
                    euqip.Add(item);
                }
            }
            for (int i = 0; i < euqip.Count; i++)
            {
                SumSave.crt_bag.Add(euqip[i]);
                SumSave.crt_euqip.Remove(euqip[i]);
            }
            SumSave.crt_bag.Remove(crt_bag.Data);
            SumSave.crt_euqip.Add(crt_bag.Data);
            Game_Omphalos.i.Wirte_ResourcesList(Emun_Resources_List.bag_value, SumSave.crt_bag);
            Game_Omphalos.i.Wirte_ResourcesList(Emun_Resources_List.equip_value, SumSave.crt_euqip);

        }
        else
        {
            int moeny= crt_bag.Data.price;

            //����
            SumSave.crt_bag.Remove(crt_bag.Data);
            Game_Omphalos.i.Wirte_ResourcesList(Emun_Resources_List.bag_value, SumSave.crt_bag);
        }
        Refresh();
    }

    private void Refresh()
    {
        Hide();
        panel_bag.Show();
    }
}
