using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVC;
using Common;
/// <summary>
/// 战斗工具类
/// </summary>
public static class Battle_Tool 
{
    /// <summary>
    /// 资源存储器
    /// </summary>
    //private static List<(string, int)> resources_list = new List<(string, int)>();
    /// <summary>
    /// 获取资源
    /// </summary>
    /// <param name="resources_name">名称</param>
    /// <param name="number">数量</param>
    public static void Obtain_Resources( object resources_name,int number)
    { 
        Dictionary<string, int> dic = new Dictionary<string, int>();
        dic.Add(resources_name.ToString(), number);
        SumSave.crt_bag_resources.Get(dic);
        //写入数据库
        Game_Omphalos.i.Wirte_ResourcesList(Emun_Resources_List.material_value, SumSave.crt_bag_resources.GetData());
    }
    /// <summary>
    /// 创造怪物
    /// </summary>
    /// <param name="crt"></param>
    /// <param name="lv">1小怪2精英3boss</param>
    public static void crate_monster(crtMaxHeroVO crt, int lv = 1)
    {


    }
    /// <summary>
    /// 验证地图列表
    /// </summary>
    public static void tool_map()
    {
        for (int i = 0; i < SumSave.db_maps.Count; i++)
        {
            string value= SumSave.db_maps[i].ProfitList;
            string[] values = value.Split('&');
            if (values.Length > 1)
            {
                for (int j = 0; j < values.Length; j++)
                {
                    string[] values1 = values[j].Split(' ');
                    if (values1.Length == 3)
                    {
                        if (values1[0] != values1[2])
                            Debug.Log("配表错误 " + SumSave.db_maps[i].map_name + " " + values[j]);
                        else
                        {
                            Bag_Base_VO bag = ArrayHelper.Find(SumSave.db_stditems, e => e.Name == values1[0]);
                            if (bag == null) Debug.Log("连接错误 与数据库关联错误" + SumSave.db_maps[i].map_name + " " + values[j]);
                        }
                    }
                    else Debug.Log(SumSave.db_maps[i].map_name + " " + values[j]);
                }
            }
        }
    }


}
