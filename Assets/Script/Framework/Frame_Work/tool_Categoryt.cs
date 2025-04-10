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
            Debug.LogError(this + " �Ӷ���: " + name + " û���ҵ�!");
            return default(T);
        }
        return transform.Find(name).GetComponent<T>();
    }
    /// <summary>
    /// ��õ�λ
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public static string Obtain_unit(int index)
    {
        string dec = "";
        switch ((enum_skill_attribute_list)index)
        {
            case enum_skill_attribute_list.����ֵ:
                break;
            case enum_skill_attribute_list.����ֵ:
                break;
            case enum_skill_attribute_list.����ֵ:
                break;
            case enum_skill_attribute_list.����ֵ:
                break;
            case enum_skill_attribute_list.�������:
                break;
            case enum_skill_attribute_list.ħ������:
                break;
            case enum_skill_attribute_list.������:
                break;
            case enum_skill_attribute_list.ħ������:
                break;
            case enum_skill_attribute_list.�����ظ�:
                break;
            case enum_skill_attribute_list.�����ظ�:
                break;
            case enum_skill_attribute_list.����:
                break;
            case enum_skill_attribute_list.���:
                break;
            case enum_skill_attribute_list.��͸:
                break;
            case enum_skill_attribute_list.��:
                break;
            case enum_skill_attribute_list.����:
                break;
            case enum_skill_attribute_list.����:
                break;
            case enum_skill_attribute_list.��ʵ�˺�:
                break;
            case enum_skill_attribute_list.�˺�����:
                break;
            case enum_skill_attribute_list.�쳣����:
            case enum_skill_attribute_list.�����˺�:

            case enum_skill_attribute_list.�˺��ӳ�:
                dec = "%";
                break;
            case enum_skill_attribute_list.�˺�����:
                break;
            case enum_skill_attribute_list.�����ٶ�:
                break;
            case enum_skill_attribute_list.�ƶ��ٶ�:
                break;
            case enum_skill_attribute_list.�����ӳ�:
            case enum_skill_attribute_list.�����ӳ�:
            case enum_skill_attribute_list.�﹥�ӳ�:
            case enum_skill_attribute_list.ħ���ӳ�:
            case enum_skill_attribute_list.����ӳ�:
            case enum_skill_attribute_list.ħ���ӳ�:
                dec = "%";
                break;
            case enum_skill_attribute_list.������ǿ��:
                break;
            case enum_skill_attribute_list.������ǿ��:
                break;
            case enum_skill_attribute_list.ˮ����ǿ��:
                break;
            case enum_skill_attribute_list.ľ����ǿ��:
                break;
            case enum_skill_attribute_list.������ǿ��:
                break;
            case enum_skill_attribute_list.����ӳ�:
            case enum_skill_attribute_list.װ������:
            case enum_skill_attribute_list.��Ʒ�������:
            case enum_skill_attribute_list.������Ϊ:
            case enum_skill_attribute_list.���ﾭ��:
            case enum_skill_attribute_list.�ڹ�����:
            case enum_skill_attribute_list.�������:
            case enum_skill_attribute_list.װ������:
            case enum_skill_attribute_list.�����ȡ:
            case enum_skill_attribute_list.���������ۿ�:
            case enum_skill_attribute_list.��Ը����:
            case enum_skill_attribute_list.������������:
            case enum_skill_attribute_list.����Σ�ն����:
            case enum_skill_attribute_list.����˫�������:
                dec = "%";
                break;
            case enum_skill_attribute_list.����ʱ��:
                break;
            case enum_skill_attribute_list.������������:
                dec = "%";
                break;
            case enum_skill_attribute_list.Ѱ�ּ��:
                break;
            case enum_skill_attribute_list.��������:
                break;
            case enum_skill_attribute_list.��:
                break;
            case enum_skill_attribute_list.��:
                break;
            case enum_skill_attribute_list.ˮ:
                break;
            case enum_skill_attribute_list.ľ:
                break;
            case enum_skill_attribute_list.��:
                break;
            case enum_skill_attribute_list.�����˺�:
            case enum_skill_attribute_list.�����˺�����:
                dec = "%";
                break;
            case enum_skill_attribute_list.����:
                break;
            case enum_skill_attribute_list.����:
                break;
            case enum_skill_attribute_list.��ʶ:
                break;
            case enum_skill_attribute_list.���﹥��:
                break;
            case enum_skill_attribute_list.�������:
                break;
            case enum_skill_attribute_list.��������:
                break;
            case enum_skill_attribute_list.���ﱩ��:
                break;
            case enum_skill_attribute_list.���ﱩ���˺�:
                dec = "%";
                break;
            case enum_skill_attribute_list.���ﱩ����:
                break;
            case enum_skill_attribute_list.���﹥���ٶ�:
                break;
            case enum_skill_attribute_list.�����˺�:
                dec = "%";
                break;
            case enum_skill_attribute_list.ȼѪ:
                break;
            case enum_skill_attribute_list.����:
                break;
            case enum_skill_attribute_list.����:
                break;
            case enum_skill_attribute_list.�ܵ������˺�:
                break;
            case enum_skill_attribute_list.�������:
                break;
            case enum_skill_attribute_list.����һ���ĸ���:
                dec = "%";

                break;
            case enum_skill_attribute_list.����һ�����˺�:
                dec = "%";

                break;
            case enum_skill_attribute_list.����ʱ���ʵ����˺�:
                dec = "%";

                break;
            case enum_skill_attribute_list.������ʱ������ʵ�˺�:
                break;
            case enum_skill_attribute_list.ÿ�ι��������˺�:
                dec = "%";

                break;
            case enum_skill_attribute_list.������Ч��:
            case enum_skill_attribute_list.ʩ����Ч��:
            case enum_skill_attribute_list.�����ż����˺�:
            case enum_skill_attribute_list.ħ����Ч��:
            case enum_skill_attribute_list.Ѫ�������˺�:
                dec = "%";

                break;
            default:
                break;
        }
        return dec;
    }
    /// <summary>
    /// ��ȡʱ��
    /// </summary>
    /// <returns></returns>
    public static DateTime Nowtime()
    { 
        return DateTime.Now; ;
    }
    /// <summary>
    /// ��ȡ�����б�
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
    /// ��ȡ����
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
    /// ����װ��
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
        //ǿ���ȼ�
        user_value += " " + 0;
        //Ʒ��
        int quality = Quality();
        user_value += " " + quality;

        if (quality > 0)
        {
            List<(int, int)> list = new List<(int, int)>();
            //������� 1�﹥�� 2�﹥�� 3ħ���� 4ħ���� 5���  6ħ�� 7����8ħ��
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
                                    //����
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
    /// ��������
    /// </summary>
    /// <param name="skill_name"></param>
    /// <returns></returns>
    public static base_skill_vo crate_skill(string skill_name)
    {
        base_skill_vo skill = ArrayHelper.Find(SumSave.db_skills, e => e.skillname == skill_name);
        /// 1�������� 2���ܵȼ� 3����λ�� 4�������� 5�������� 6�����˺����� 7�������ȼ� 8���ܳ�ʼ���������� 9��������

        skill.user_value = skill_name;
        //�ȼ�
        skill.user_value += " " + 1;
        //λ�� 0���ϳ�
        skill.user_value += " " + 0;
        //�������� Ĭ��Ϊ0
        skill.user_value += " " + 0;

        return skill;

    }
    /// <summary>
    /// ��ȡ�����
    /// </summary>
    /// <returns></returns>
    public static int Obtain_Random()
    { 
        return Random.Range(1, 10000);
    }
    /// <summary>
    /// ��ȡװ��Ʒ��
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
