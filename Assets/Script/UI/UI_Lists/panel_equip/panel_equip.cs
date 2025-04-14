using Common;
using Components;
using MVC;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 展示装备的界面
/// </summary>
public class panel_equip : Panel_Base
{

    private equip_item equip_item_prafabs;
    private Transform crt_pos_equip;
    private panel_bag panel_bag;
    private bag_item crt_bag;
    private Bag_Base_VO crt_equip;
    private GridLayoutGroup gridLayoutgroup;

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
        gridLayoutgroup = Find<GridLayoutGroup>("bg_main");
    }

    public override void Show()
    {
        base.Show();
    }

    #region 显示装备
    /// <summary>
    /// 传递参数
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
    /// 显示装备
    /// </summary>
    /// <param name="bag"></param>
    public void Select_Equip(bag_item bag)
    {
        gridLayoutgroup.cellSize = new Vector2(178, 360);
        ClearObject(crt_pos_equip);
        crt_equip = bag.Data;
        equip_item item = Instantiate(equip_item_prafabs, crt_pos_equip);
        item.Data = bag.Data;
        item.Show_take_Btn();
    }

    /// <summary>
    /// 脱下
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
            SendNotification(NotiList.Refresh_Max_Hero_Attribute);
        }
        Refresh();
    }
    /// <summary>
    /// 点击事件 0 穿戴 1出售
    /// </summary>
    /// <param name="data"></param>
    protected void OnClick_Btn(int index)
    {
        //穿戴
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
            SendNotification(NotiList.Refresh_Max_Hero_Attribute);
        }
        else
        {
            int moeny= crt_bag.Data.price;
            SumSave.crt_user_unit.verify_data(currency_unit.灵珠, moeny);
            Alert_Dec.Show("出售成功 获得灵珠" + moeny);
            SumSave.crt_bag.Remove(crt_bag.Data);
            Game_Omphalos.i.Wirte_ResourcesList(Emun_Resources_List.bag_value, SumSave.crt_bag);
            Game_Omphalos.i.GetQueue(Mysql_Type.UpdateInto, Mysql_Table_Name.mo_user, SumSave.crt_user_unit.Set_Uptade_String(), SumSave.crt_user_unit.Get_Update_Character());

        }
        Refresh();
    }

    #endregion
    /// <summary>
    /// 选择材料
    /// </summary>
    /// <param name="item"></param>
    public void Select_Material(material_item item)
    {
        (string, int) data = item.GetItemData();
        gridLayoutgroup.cellSize = new Vector2(600, 400);
        Bag_Base_VO vo = ArrayHelper.Find( SumSave.db_stditems,e=> e.Name == data.Item1);
        //传入数据
        //Show_Material

    }

    private void Refresh()
    {
        Hide();
        panel_bag.Show();
    }
}
