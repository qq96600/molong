using Common;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class panel_store : Base_Mono
{
    /// <summary>
    /// 商店类型父物体
    /// </summary>
    private Transform store_type; 
    /// <summary>
    /// 商店类型名字
    /// </summary>
    private string[] type_name = { "道具", "限购", "离线" };
    /// <summary>
    /// 商店类型按钮
    /// </summary>
    private btn_item btn_item;
    /// <summary>
    /// 商店内物品父物体
    /// </summary>
    private Transform store_item;
    /// <summary>
    /// 商店内物品预制体
    /// </summary>
    private material_item material_item;
    /// <summary>
    /// 当前商店物品字典
    /// </summary>
    private Dictionary<string, db_store_vo> items_dic = new Dictionary<string, db_store_vo>();
    /// <summary>
    /// 当前商店物品列表
    /// </summary>
    private List<db_store_vo> items_list = new List<db_store_vo>();

    private void Awake()
    {
        store_type = Find<Transform>("store_type");
        btn_item = Resources.Load<btn_item>("Prefabs/base_tool/btn_item");
        store_item = Find<Transform>("store_item/Viewport/Content");
        material_item = Resources.Load<material_item>("Prefabs/panel_bag/material_item");
        ClearObject(store_type);
        for (int i = 0; i < type_name.Length; i++)
        {
            btn_item btn = Instantiate(btn_item, store_type);
            btn.Show(i, type_name[i]);
            btn.GetComponent<Button>().onClick.AddListener(() => { ShowItem(btn.index); });
        }
    }

    /// <summary>
    /// 显示商店内容
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private void ShowItem(int index)
    {
        items_dic.Clear();
        items_list.Clear();
        ClearObject(store_item);
        for (int i = 0; i < SumSave.db_stores_dic.Count; i++)//判断是否为当前商店类型
        {
            if (SumSave.db_stores_dic[i].store_Type == (index + 1))
            {
                if (!items_dic.ContainsKey(SumSave.db_stores_dic[i].ItemName))
                {
                    items_dic.Add(SumSave.db_stores_dic[i].ItemName, SumSave.db_stores_dic[i]);
                    items_list.Add(SumSave.db_stores_dic[i]);
                }
                else
                {
                    Debug.LogError("商店内" + SumSave.db_stores_dic[i].ItemName + "物品重复");
                }
            }
        }
        
        for (int i = 0; i < items_list.Count; i++)//显示商店内物品
        {
            material_item item = Instantiate(material_item, store_item);

            item.Init((items_list[i].ItemName, items_list[i].ItemPrice));

            item.GetComponent<Button>().onClick.AddListener(() => { ShowItemInfo(items_list[i]); });
        }
       
    }
    /// <summary>
    /// 点击物品显示物品信息
    /// </summary>
    /// <param name="db_store_vo"></param>
    private void ShowItemInfo(db_store_vo db_store_vo)
    {
        Debug.Log("点击"+db_store_vo.ItemName);
    }
}
