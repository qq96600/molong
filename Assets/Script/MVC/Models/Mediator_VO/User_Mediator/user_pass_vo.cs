using Common;
using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class user_pass_vo : Base_VO
{
    /// <summary>
    /// 等级
    /// </summary>
    public int lv;
    /// <summary>
    /// 第几个通行证
    /// </summary>
    public int pass_index = 0;
    /// <summary>
    /// 奖励
    /// </summary>
    public string reward;
    /// <summary>
    /// 进阶奖励
    /// </summary>
    public string uplv_reward;
    /// <summary>
    /// 等级
    /// </summary>
    public int data_lv;
    /// <summary>
    /// 进阶奖励
    /// </summary>
    public int data_uplv;
    /// <summary>
    /// 经验值
    /// </summary>
    public int data_exp;
    ///领取状态
    private Dictionary<int,List<int>> dic_user_values = new Dictionary<int, List<int>>();

    public void Init()
    { 
        string[] str = user_value.Split('|');

        for (int i = 0; i < str.Length; i++)
        {
            string[] str1 = str[i].Split(',');
            if (str1.Length > 1)
            {
                if(!dic_user_values.ContainsKey(int.Parse(str1[0])))dic_user_values.Add(int.Parse(str1[0]), new List<int>());

                for (int j = 1; j < str1.Length; j++)
                { 
                    if(str1[j]!="")
                    dic_user_values[int.Parse(str1[0])].Add(int.Parse(str1[j]));
                }
            }
        }
    }
    /// <summary>
    /// 返回用户领取状态
    /// </summary>
    /// <returns></returns>
    public Dictionary<int, List<int>> Set()
    {
        return dic_user_values;

    }
    /// <summary>
    /// 设置用户领取状态
    /// </summary>
    /// <param name="list"></param>
    public void Get(Dictionary<int, List<int>> list)
    {
        dic_user_values = list;
    }

    private string Set_data()
    {
        string dec = "";

        foreach (var item in dic_user_values.Keys)
        {
            if (dec != "") dec += "|";
            dec += item + ",";
            foreach (var list in dic_user_values[item])
            {
                dec+= list + ",";
            }
        }
         
        return dec;
    }

    public override string[] Set_Instace_String()
    {
        return new string[]
        {
        GetStr(0),
        GetStr(SumSave.crt_user.uid),
        GetStr(data_lv),
        GetStr(data_exp),
        GetStr("")
        };
    }

    public override string[] Get_Update_Character()
    {
        return new string[]
        {
        "pass_lv",
        "pass_exp",
        "user_value"
        };
    }

    public override string[] Set_Uptade_String()
    {
        return new string[]
        {
        GetStr(data_lv),
        GetStr(data_exp),
        GetStr(Set_data())
        };
    }
}
