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
    /// 获得单位
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public static string Obtain_unit(int index)
    {
        string dec = "";
        switch ((enum_skill_attribute_list)index)
        {
            case enum_skill_attribute_list.生命值:
                break;
            case enum_skill_attribute_list.法力值:
                break;
            case enum_skill_attribute_list.内力值:
                break;
            case enum_skill_attribute_list.蓄力值:
                break;
            case enum_skill_attribute_list.物理防御:
                break;
            case enum_skill_attribute_list.魔法防御:
                break;
            case enum_skill_attribute_list.物理攻击:
                break;
            case enum_skill_attribute_list.魔法攻击:
                break;
            case enum_skill_attribute_list.生命回复:
                break;
            case enum_skill_attribute_list.法力回复:
                break;
            case enum_skill_attribute_list.命中:
                break;
            case enum_skill_attribute_list.躲避:
                break;
            case enum_skill_attribute_list.穿透:
                break;
            case enum_skill_attribute_list.格挡:
                break;
            case enum_skill_attribute_list.暴击:
                break;
            case enum_skill_attribute_list.幸运:
                break;
            case enum_skill_attribute_list.真实伤害:
                break;
            case enum_skill_attribute_list.伤害减免:
                break;
            case enum_skill_attribute_list.异常抗性:
            case enum_skill_attribute_list.暴击伤害:

            case enum_skill_attribute_list.伤害加成:
                dec = "%";
                break;
            case enum_skill_attribute_list.伤害吸收:
                break;
            case enum_skill_attribute_list.攻击速度:
                break;
            case enum_skill_attribute_list.移动速度:
                break;
            case enum_skill_attribute_list.生命加成:
            case enum_skill_attribute_list.法力加成:
            case enum_skill_attribute_list.物攻加成:
            case enum_skill_attribute_list.魔攻加成:
            case enum_skill_attribute_list.物防加成:
            case enum_skill_attribute_list.魔防加成:
                dec = "%";
                break;
            case enum_skill_attribute_list.土属性强化:
                break;
            case enum_skill_attribute_list.火属性强化:
                break;
            case enum_skill_attribute_list.水属性强化:
                break;
            case enum_skill_attribute_list.木属性强化:
                break;
            case enum_skill_attribute_list.金属性强化:
                break;
            case enum_skill_attribute_list.经验加成:
            case enum_skill_attribute_list.装备掉落:
            case enum_skill_attribute_list.极品宠物掉落:
            case enum_skill_attribute_list.人物修为:
            case enum_skill_attribute_list.宠物经验:
            case enum_skill_attribute_list.内功经验:
            case enum_skill_attribute_list.金币收益:
            case enum_skill_attribute_list.装备爆率:
            case enum_skill_attribute_list.宠物获取:
            case enum_skill_attribute_list.云游商人折扣:
            case enum_skill_attribute_list.祈愿收益:
            case enum_skill_attribute_list.奇遇任务收益:
            case enum_skill_attribute_list.游历危险躲避率:
            case enum_skill_attribute_list.游历双倍获得率:
                dec = "%";
                break;
            case enum_skill_attribute_list.游历时长:
                break;
            case enum_skill_attribute_list.游历龙珠收益:
                dec = "%";
                break;
            case enum_skill_attribute_list.寻怪间隔:
                break;
            case enum_skill_attribute_list.宠物容量:
                break;
            case enum_skill_attribute_list.土:
                break;
            case enum_skill_attribute_list.火:
                break;
            case enum_skill_attribute_list.水:
                break;
            case enum_skill_attribute_list.木:
                break;
            case enum_skill_attribute_list.金:
                break;
            case enum_skill_attribute_list.五行伤害:
            case enum_skill_attribute_list.五行伤害减少:
                dec = "%";
                break;
            case enum_skill_attribute_list.灵力:
                break;
            case enum_skill_attribute_list.体魄:
                break;
            case enum_skill_attribute_list.神识:
                break;
            case enum_skill_attribute_list.宠物攻击:
                break;
            case enum_skill_attribute_list.宠物防御:
                break;
            case enum_skill_attribute_list.宠物生命:
                break;
            case enum_skill_attribute_list.宠物暴击:
                break;
            case enum_skill_attribute_list.宠物暴击伤害:
                dec = "%";
                break;
            case enum_skill_attribute_list.宠物暴击率:
                break;
            case enum_skill_attribute_list.宠物攻击速度:
                break;
            case enum_skill_attribute_list.技能伤害:
                dec = "%";
                break;
            case enum_skill_attribute_list.燃血:
                break;
            case enum_skill_attribute_list.灵身:
                break;
            case enum_skill_attribute_list.连击:
                break;
            case enum_skill_attribute_list.受到减免伤害:
                break;
            case enum_skill_attribute_list.复活次数:
                break;
            case enum_skill_attribute_list.幸运一击的概率:
                dec = "%";

                break;
            case enum_skill_attribute_list.幸运一击的伤害:
                dec = "%";

                break;
            case enum_skill_attribute_list.攻击时概率抵消伤害:
                dec = "%";

                break;
            case enum_skill_attribute_list.被攻击时反击真实伤害:
                break;
            case enum_skill_attribute_list.每次攻击增加伤害:
                dec = "%";

                break;
            case enum_skill_attribute_list.治疗术效果:
            case enum_skill_attribute_list.施毒术效果:
            case enum_skill_attribute_list.青云门技能伤害:
            case enum_skill_attribute_list.魔法盾效果:
            case enum_skill_attribute_list.血刀刀法伤害:
                dec = "%";

                break;
            default:
                break;
        }
        return dec;
    }
    /// <summary>
    /// 获取时间
    /// </summary>
    /// <returns></returns>
    public static DateTime Nowtime()
    { 
        return DateTime.Now; ;
    }
    /// <summary>
    /// 获取数据列表
    /// </summary>
    /// <param name="bag"></param>
    public static Bag_Base_VO Read_Bag(Bag_Base_VO bag)
    {
        Bag_Base_VO bag_base = new Bag_Base_VO();
        string[] slits = bag.user_value.Split(' ');
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
    /// 读取技能
    /// </summary>
    /// <param name="base_item"></param>
    /// <returns></returns>
    public static base_skill_vo Read_skill(base_skill_vo base_item)
    {
        base_skill_vo skill = new base_skill_vo();
        string[] slits = base_item.user_value.Split(' ');
        if (slits.Length > 1)
        {
            foreach (var item in SumSave.db_skills)
            {
                if (item.skillname == slits[0])
                {
                    skill = item;
                    skill.user_value = base_item.user_value;
                    skill.user_values = slits;
                    return skill;
                }
            }
        }
        return skill;
    }

    public static user_pet_hatching_vo crate_Pet(string name)
    { 
        user_pet_hatching_vo pet = new user_pet_hatching_vo();

        return pet;
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
                                    base_quality = Random.Range(30, 35);
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
    /// 创建技能
    /// </summary>
    /// <param name="skill_name"></param>
    /// <returns></returns>
    public static base_skill_vo crate_skill(string skill_name)
    {
        base_skill_vo skill = ArrayHelper.Find(SumSave.db_skills, e => e.skillname == skill_name);
        /// 1技能名称 2技能等级 3技能位置 4技能内力 5技能类型 6技能伤害类型 7技能最大等级 8技能初始化升级经验 9技能升级

        skill.user_value = skill_name;
        //等级
        skill.user_value += " " + 1;
        //位置 0不上场
        skill.user_value += " " + 0;
        //分配内力 默认为0
        skill.user_value += " " + 0;

        return skill;

    }
    /// <summary>
    /// 获取随机数
    /// </summary>
    /// <returns></returns>
    public static int Obtain_Random()
    { 
        return Random.Range(1, 10000);
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
