using Common;
using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bag_seed_vo : Base_VO
{
    /// <summary>
    /// 0名称 1生成时间 2效果
    /// </summary>
    private List<(string,List<string>)> seedList;

    public void Init()
    { 
        seedList = new List<(string,List<string>)>();
        string[] splits = user_value.Split('&');
        for (int i = 0; i < splits.Length; i++)
        {
            if (splits[i] != "")
            { 
                string[] split = splits[i].Split(',');
                seedList.Add((split[0], new List<string>()));
                for (int j = 1; j < split.Length; j++)
                { 
                    seedList[i].Item2.Add(split[j]);
                }
            }
        }
    }
    /// <summary>
    /// 获取物品
    /// </summary>
    /// <returns></returns>
    public List<(string, List<string>)> Get()
    { 
      return seedList;
    }
    /// <summary>
    /// 添加物品
    /// </summary>
    /// <param name="split"></param>
    public void Set( List<string> split)
    {
        (string,List<string>) temp = (split[0], new List<string>());
        for (int j = 1; j < split.Count; j++)
        {
            temp.Item2.Add(split[j]);
        }
        seedList.Add(temp);
    }
    /// <summary>
    /// 写入数据库
    /// </summary>
    private string DataSet()
    { 
        string value="";
        for (int i = 0; i < seedList.Count; i++)
        {
            value += (value == "" ? "" : "&") + seedList[i].Item1;
            for (int j = 0 ; j < seedList[i].Item2.Count; j++)
            {
                value+= " " + seedList[i].Item2[j];
            }
        }
        return value;
    }

    public override string[] Get_Update_Character()
    {
        return new string[] { "user_value" };
    }

    public override string[] Set_Uptade_String()
    {
        return new string[] { GetStr(DataSet()) };
    }

    public override string[] Set_Instace_String()
    {
        return 
            new string[]
            {
                GetStr(0),
                GetStr(SumSave.crt_user.uid),
                GetStr(user_value) 
            };
    }
}
