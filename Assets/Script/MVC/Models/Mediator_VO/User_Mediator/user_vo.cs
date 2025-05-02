using Common;
using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class user_vo : Base_VO
{

    /// <summary>
    /// 0 灵珠，1 历练，2 魔丸
    /// </summary>
    private List<long> list = new List<long>();
    private List<long> verify_list = new List<long>();

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
                list.Add(long.Parse(str[i]));
                verify_list.Add(long.Parse(str[i]) + index);
            }
        }
    }
    public List<long> Set()
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
    public void verify_data(currency_unit _index,long value)
    {
        for (int i = 0; i < list.Count; i++)
        {
            //原始数据未发生改变
            if (list[i] + index == verify_list[i])
            {
            }
            else Game_Omphalos.i.Delete(_index + " 显示数据 " + list[i] + " 验证值 " + index + " " + verify_list[i]);
        }

        switch (_index)
        {
            case currency_unit.灵珠:
                if (value > 0)
                {
                    Combat_statistics.AddMoeny(value);
                    SumSave.crt_achievement.increase_date_Exp((Achieve_collect.获得灵珠).ToString(), value);
                }
                else if (value < 0)//扣除灵珠成就
                {
                    SumSave.crt_achievement.increase_date_Exp((Achieve_collect.花费灵珠).ToString(), value);
                }

                list[0] += value;
                verify_list[0] += value;
                break;
            case currency_unit.历练:
                if (value >= SumSave.base_setting[0]) Game_Omphalos.i.Delete("获得" + (currency_unit)_index + value);
                else
                {
                    if (value > 0) Combat_statistics.AddPoint(value);

                    list[1] += value;
                    verify_list[1] += value;
                }
                return;
            case currency_unit.魔丸:
                if (value >= SumSave.base_setting[1]) Game_Omphalos.i.Delete("获得" + (currency_unit)_index + value);
                else
                {
                    list[2] += value;
                    verify_list[2] += value;
                }
                return;
        }
        
    }

    public override string[] Get_Update_Character()
    {
        return new string[] { "value" };
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
