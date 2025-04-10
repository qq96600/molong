using Common;
using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class user_vo : Base_VO
{
    private List<int> list = new List<int>();

    private List<int> verify_list = new List<int>();

    private int index = -1;
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="value"></param>
    public void Init(string value)
    {
        index = Random.Range(1, 1000);
        string[] str = value.Split(',');
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i].Length > 0)
            {
                list.Add(int.Parse(str[i]));
                verify_list.Add(int.Parse(str[i]) + index);
            }
        }
    }
    public List<int> Set()
    { 
      return list;
    }

    private string Set_data()
    {

        string dec = "";

        for (int i = 0; i < list.Count; i++)
        {
            dec += list[i] + ",";
        }
        return dec;
    }

    /// <summary>
    /// 验证数据
    /// </summary>
    public void verify_data(currency_unit _index,int value)
    {
        switch (_index)
        {
            case currency_unit.金币:
                break;
            case currency_unit.声望:
                if (value >= SumSave.base_setting[0]) Game_Omphalos.i.Delete("获得" + (currency_unit)_index + value);
                return;

            case currency_unit.元宝:
                if (value >= SumSave.base_setting[1]) Game_Omphalos.i.Delete("获得" + (currency_unit)_index + value);
                return;
        }
        for (int i = 0; i < list.Count; i++)
        {
            //原始数据未发生改变
            if (list[i] + index == verify_list[i])
            {
                list[i] += value;
                verify_list[i] += value;
            }
            else Game_Omphalos.i.Delete(_index + " 显示数据 " + list[i] + " 验证值 " + index + " " + verify_list[i]);
        }
    }

    public override string[] Get_Update_Character()
    {
        return new string[] { "user_vo" };
    }

    public override string[] Set_Uptade_String()
    {
        return new string[] { GetStr(Set_data()) };
    }

    public override string[] Set_Instace_String()
    {
        return new string[] {
            GetStr(0),
            GetStr(SumSave.crt_user.uid),
            GetStr(Set_data())
        };
    }
}
