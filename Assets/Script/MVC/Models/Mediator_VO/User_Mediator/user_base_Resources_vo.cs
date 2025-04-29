using Common;
using MVC;
using System;

public class user_base_Resources_vo:Base_VO
{
    /// <summary>
    /// 当前时间
    /// </summary>
    public DateTime now_time;
    /// <summary>
    /// 地图索引 
    /// </summary>
    public string user_map_index;
    /// <summary>
    /// 技能列表
    /// </summary>
    public string skill_value;
    /// <summary>
    /// 仓库列表
    /// </summary>
    public string house_value;
    /// <summary>
    /// 背包列表
    /// </summary>
    public string bag_value;
    /// <summary>
    /// 材料列表
    /// </summary>
    public string material_value;
    /// <summary>
    /// 装备列表
    /// </summary>
    public string equip_value;
    /// <summary>
    /// 页码
    /// </summary>
    public int[] pages;
    /// <summary>
    /// 获取时间
    /// </summary>
    /// <returns></returns>
    private string Get_Time()
    {
        if (SumSave.nowtime > now_time)
        {
            now_time = SumSave.nowtime;
        }
        return now_time.ToString();
    }
    public override string[] Set_Instace_String()
    {
        //return new string[] { "now_time", "skill_value", "house_value", "bag_value", "material_value", "equip_value" };
        return new string[]
        {
        GetStr(0),
        GetStr(SumSave.crt_user.uid),        
        GetStr(now_time),
        GetStr(SumSave.db_maps[0].map_name),
        GetStr(skill_value),
        GetStr(house_value),
        GetStr(bag_value),
        GetStr(material_value),
        GetStr(equip_value),
        GetStr(ArrayHelper.Data_Encryption(pages))
        };
    }

    public override string[] Get_Update_Character()
    {
        return new string[] 
        { 
            "now_time",
            "user_map_index",
            "skill_value", 
            "house_value",
            "bag_value", 
            "material_value", 
            "equip_value"
        };
    }
    public override string[] Set_Uptade_String()
    {
        return new string[]
        {
         GetStr(Get_Time()),
         GetStr(user_map_index),
         GetStr(skill_value),
         GetStr(house_value),
         GetStr(bag_value),
         GetStr(material_value),
         GetStr(equip_value),
        };
    }
}
    
