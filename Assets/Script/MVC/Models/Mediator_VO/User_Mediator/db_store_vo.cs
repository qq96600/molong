using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class db_store_vo : Base_VO
{
    /// <summary>
    /// 商店类型 1，道具商店 2，限购商店 3，离线商店
    /// </summary>
    public readonly int store_Type;
    /// <summary>
    /// 物品名称
    /// </summary>
    public readonly string ItemName;
    /// <summary>
    /// 物品价格
    /// </summary>
    public readonly int ItemPrice;
    /// <summary>
    /// 所需要的货币类型
    /// </summary>
    public readonly string unit;
    /// <summary>
    /// 折扣区间  没有折扣为（0，0）
    /// </summary>
    public readonly (int,int) discount;
    /// <summary>
    /// 最大购买数量
    /// </summary>
    public readonly int ItemMaxQuantity;

    public db_store_vo(int store_Type, string ItemName, int ItemPrice, string unit, (int, int) discount, int ItemMaxQuantity)
    { 
        this.store_Type = store_Type;
        this.ItemName = ItemName;
        this.ItemPrice = ItemPrice;
        this.unit = unit;
        this.discount = discount;
        this.ItemMaxQuantity = ItemMaxQuantity;
    }
     
}
