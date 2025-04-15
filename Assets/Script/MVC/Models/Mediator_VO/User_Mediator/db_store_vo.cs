using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class db_store_vo : Base_VO
{
    /// <summary>
    /// 商店类型 1，道具商店 2，限购商店 3，离线商店
    /// </summary>
    public int store_Type;
    /// <summary>
    /// 物品名称
    /// </summary>
    public string ItemName;
    /// <summary>
    /// 物品价格
    /// </summary>
    public int ItemPrice;
    /// <summary>
    /// 最大购买数量
    /// </summary>
    public int ItemMaxQuantity;

}
