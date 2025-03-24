using Common;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

    /// <summary>
    /// 创建装备
    /// </summary>
    /// <param name="bag"></param>
    public static Bag_Base_VO crate_equip(string bag_name)
    {
        Bag_Base_VO bag = new Bag_Base_VO();
        foreach (var item in SumSave.db_stditems)
        {
            if (item.Name == bag_name)
            {
                bag = item;
                continue;  
            }
        }
        string user_value = bag_name;
        //强化等级
        user_value += " " + 0;
        //品质
        int quality = Quality();
        user_value += " " + quality;

        if (quality > 0)
        {
            List<(int, int)> list = new List<(int, int)>();
            //随机属性 1物攻下 2物攻上 3魔攻下 4魔攻上 5物防  6魔防 7生命8魔法
            int base_quality = Random.Range(1, 9);
            int base_quality_value = Random.Range(bag.need_lv+5, bag.need_lv * 2 + 10);
            if (base_quality > 6) base_quality_value *= 10;
            list.Add((base_quality, base_quality_value));
            quality = 7;
            if (quality > 1)
            {
                base_quality = Random.Range(8, 15);
                base_quality_value = Random.Range(3, bag.need_lv / 2 + 6);
                if (base_quality <= 9) base_quality_value *= 10;
                list.Add((base_quality, base_quality_value));
                if (quality > 2)
                {
                    base_quality = Random.Range(8, 15);
                    base_quality_value = Random.Range(3, bag.need_lv / 2 + 6);
                    if (base_quality <= 9) base_quality_value *= 10;
                    list.Add((base_quality, base_quality_value));
                    if( quality > 3)
                    {
                        base_quality = Random.Range(19, 30);
                        base_quality_value = Random.Range(3, 6);
                        list.Add((base_quality, base_quality_value));
                        if (quality > 4)
                        {
                            base_quality = Random.Range(19, 30);
                            base_quality_value = Random.Range(3, 6);
                            list.Add((base_quality, base_quality_value));
                            if (quality > 5)
                            {
                                base_quality = Random.Range(19, 30);
                                base_quality_value = Random.Range(3, 6);
                                list.Add((base_quality, base_quality_value));
                                if (quality > 6)
                                {
                                    //五行
                                    base_quality = Random.Range(1, 6);
                                    base_quality_value = Random.Range(3, bag.need_lv / 2 + 6);
                                    list.Add((base_quality, base_quality_value));
                                }
                            }
                        }
                    }
                }
                string type = "", value = "";
                int index = 0;
                foreach (var item in list)
                {
                    index++;
                    type += item.Item1 + (index == list.Count ? "" : ",");
                    value += item.Item2 + (index == list.Count ? "" : ",");
                }
                user_value += " " + type + " " + value;
            }
        }
        bag.user_value = user_value;
        return bag;
    }
    /// <summary>
    /// 获取装备品质
    /// </summary>
    /// <param name="boss"></param>
    /// <returns></returns>
    public static int Quality(bool boss = false)
    {
        int random = Random.Range(10000 * SumSave.titleLucky / 100, 100001);
        int[] needs = new int[] { 0, 23000, 43000, 61000, 75000, 90000, 99600, 100001 };
        int result = 1;
        for (int i = 0; i < needs.Length; i++)
        {
            if (random > needs[i] + (boss ? 0 : 390)) result = Mathf.Max(result, i + 1);
        }
        if (boss)
        {
            result = Mathf.Max(4, result);

            if (Random.Range(0, 100) < 10)
            {
                result = 7;
            }
        }
        return result;
    }

}
