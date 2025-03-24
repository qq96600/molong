using Common;
using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tool_Categoryt : MonoBehaviour
{

    public static tool_Categoryt Tool;
    public T Find<T>(string name)
    {
        if (transform.Find(name) == null)
        {
            Debug.LogError(this + " 子对象: " + name + " 没有找到!");
            return default(T);
        }
        return transform.Find(name).GetComponent<T>();
    }
    /// <summary>
    /// 获取数据列表
    /// </summary>
    /// <param name="bag"></param>
    public static Bag_Base_VO Read_Bag(Bag_Base_VO bag)
    {
        Bag_Base_VO bag_base = new Bag_Base_VO();
        string[] slits = bag.user_value.Split(' ');
        Debug.Log(slits[0]);
        if (slits.Length > 1)
        {
            foreach (var item in SumSave.db_stditems)
            {
                if (item.Name == slits[0])
                {
                    bag_base = item;
                    bag_base.user_value=bag.user_value;
                    return bag_base;
                }
             }
        }

        return bag_base;
    }
}
