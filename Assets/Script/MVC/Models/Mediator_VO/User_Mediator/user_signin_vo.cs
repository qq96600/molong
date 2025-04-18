using Common;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class user_signin_vo : Base_VO
{
    /// <summary>
    /// 上次签到时间
    /// </summary>
    public DateTime now_time;
    /// <summary>
    /// 签到数量
    /// </summary>
    public int number;
    /// <summary>
    /// 是否领取奖励
    /// </summary>
    private List<int> values = new List<int>();

    public void Init()
    { 
    
        string[] str = user_value.Split(' ');
        if (str.Length > 0)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (!string.IsNullOrEmpty(str[i]))
                    values.Add(int.Parse(str[i]));
            }
        }
        if (values.Count < SumSave.db_Signins.Count)
        {
            for (int i = 0; i < SumSave.db_Signins.Count - values.Count; i++)
            { 
                values.Add(0);
            }
        }
    }
    /// <summary>
    /// 查看
    /// </summary>
    /// <returns></returns>
    public List<int> Set()
    { 
        return values;
    }
    /// <summary>
    /// 获取编号
    /// </summary>
    /// <param name="index"></param>
    public void Set(int index)
    {
        if (index < values.Count)
            values[index] = 1;
        else
        {
            while (index < values.Count)
            {
                values.Add(0);
            }
            values[index] = 1;
        }
    }

    private string DataSet()
    { 
        string value="";
        for (int i = 0; i < values.Count; i++)
        { 
            value += values[i].ToString() + " ";
        }
        return value;
    }

    public override string[] Get_Update_Character()
    {
        return
            new string[]
            {
                "now_time",
                "number",
                "user_value"
            };
    }

    public override string[] Set_Uptade_String()
    {
        return new string[]
            {
              GetStr(now_time.ToString("yyyy-MM-dd")),
              GetStr(number),
              GetStr(DataSet())
            };
    }

    public override string[] Set_Instace_String()
    {
        return new string[]
        {
        GetStr(0),
        GetStr(SumSave.crt_user.uid),
        GetStr(now_time),
        GetStr(number),
        GetStr(DataSet())
        };
    }

}
