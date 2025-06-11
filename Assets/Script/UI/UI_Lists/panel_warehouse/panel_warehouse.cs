using Common;
using MVC;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class panel_warehouse : Panel_Base
{
    /// <summary>
    /// 装备预制体
    /// </summary>
    private bag_item bag_item_Prefabs;

    /// <summary>
    /// 装备栏显示位置,仓库栏显示位置
    /// </summary>
    private Transform Equipment, Warehouse;
   

    public override void Initialize()
    {
        base.Initialize();
        bag_item_Prefabs = Battle_Tool.Find_Prefabs<bag_item>("bag_item");
        Equipment = Find<Transform>("Equipment/Viewport/Content");
        Warehouse = Find<Transform>("Warehouse/Viewport/Content");
    }
    /// <summary>
    /// 显示装备
    /// </summary>
    public void ShowEquipment()
    {
        ClearObject(Equipment);
        ArrayHelper.OrderDescding(SumSave.crt_bag, e => int.Parse(e.user_value.Split(' ')[2]));
        for (int i = 0; i < SumSave.crt_bag.Count; i++)
        {
            bag_item item = Instantiate(bag_item_Prefabs, Equipment);
            item.Data = SumSave.crt_bag[i];
            item.GetComponent<Button>().onClick.AddListener(delegate { PutIn_warehouse(item); });
        }
        //page_info.text = SumSave.crt_bag.Count + "/" + SumSave.crt_resources.pages[0];
    }

    /// <summary>
    /// 点击从装备放入到仓库
    /// </summary>
    /// <param name="item"></param>
    private void PutIn_warehouse(bag_item _item)
    {
        List<Bag_Base_VO> euqip = new List<Bag_Base_VO>();
        SumSave.crt_bag.Remove(_item.Data);
        SumSave.crt_house.Add(_item.Data);
        Game_Omphalos.i.Wirte_ResourcesList(Emun_Resources_List.bag_value, SumSave.crt_bag);
        Game_Omphalos.i.Wirte_ResourcesList(Emun_Resources_List.house_value, SumSave.crt_house);
        Refresh();
    }



    /// <summary>
    /// 显示仓库
    /// </summary>
    public void ShowWarehouse()
    {
        ClearObject(Warehouse);
        ArrayHelper.OrderDescding(SumSave.crt_house, e => int.Parse(e.user_value.Split(' ')[2]));
        for (int i = 0; i < SumSave.crt_house.Count; i++)
        {
            bag_item item = Instantiate(bag_item_Prefabs, Warehouse);
            item.Data = SumSave.crt_house[i];
            item.GetComponent<Button>().onClick.AddListener(delegate { PutIn_Equipment(item); });
        }
        //page_info.text = SumSave.crt_bag.Count + "/" + SumSave.crt_resources.pages[0];
    }

    /// <summary>
    /// 点击从仓库放入到装备
    /// </summary>
    /// <param name="item"></param>
    private void PutIn_Equipment(bag_item _item)
    {
        List<Bag_Base_VO> euqip = new List<Bag_Base_VO>();
        SumSave.crt_house.Remove(_item.Data);
        SumSave.crt_bag.Add(_item.Data);
        Game_Omphalos.i.Wirte_ResourcesList(Emun_Resources_List.bag_value, SumSave.crt_bag);
        Game_Omphalos.i.Wirte_ResourcesList(Emun_Resources_List.house_value, SumSave.crt_house);
        Refresh();
    }
    /// <summary>
    /// 刷新显示
    /// </summary>
    public void Refresh()
    {
        ShowEquipment();
        ShowWarehouse();
    }


    public override void Hide()
    {
        base.Hide();
    }



    public override void Show()
    {
        base.Show();
        Refresh();
    }



}
