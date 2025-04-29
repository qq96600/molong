using Common;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class user_achievement_vo : Base_VO
{
    /// <summary>
    /// 自身uid
    /// </summary>
    public string uid;
    /// <summary>
    /// 区
    /// </summary>
    public int par;
    /// <summary>
    /// 经验值
    /// </summary>
    public string achievement_exp;
    /// <summary>
    /// 等级
    /// </summary>
    public string achievement_lvs;

    /// <summary>
    /// 具体成就以及成就等级
    /// </summary>
    private Dictionary<string, int> user_achievements = new Dictionary<string, int>();
    private Dictionary<string, int> user_achievements_lv = new Dictionary<string, int>();
  

    /// <summary>
    /// 获取经验值
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, int> Set_Exp()
    {
        return user_achievements;
    }

    public void Get_Exp(Dictionary<string, int> data)
    {
        user_achievements = data;
    }
    /// <summary>
    /// 获取等级
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, int> Set_Lv()
    {
        return user_achievements_lv;
    }

    /// <summary>
    /// 拆解数据
    /// </summary>
    public void Init()
    {
        string[] strs = achievement_exp.Split('|');
        string[] lvs = achievement_lvs.Split('|');

        for (int i = 0; i < strs.Length; i++)
        {
            string[] str = strs[i].Split(' ');
            string[] lv = lvs[i].Split(' ');
            if (!user_achievements.ContainsKey(str[0])) user_achievements.Add(str[0], int.Parse(str[1]));
            if (!user_achievements_lv.ContainsKey(lv[0])) user_achievements_lv.Add(lv[0], int.Parse(lv[1]));
        }
    }

    public void Refresh()
    {
        achievement_exp = "";
        achievement_lvs = "";
        int number = 0;
        foreach (var item in user_achievements)
        {
            ++number;
            achievement_exp += item.Key + " " + item.Value + (number == user_achievements.Count ? "" : "|");
        }
        number = 0;
        foreach (var item in user_achievements_lv)
        {
            ++number;
            achievement_lvs += item.Key + " " + item.Value + (number == user_achievements.Count ? "" : "|");
        }
    }
    public override string[] Set_Instace_String()
    {
        return new string[] { 
            GetStr(0), 
            GetStr(SumSave.crt_user.uid), 
            GetStr(SumSave.par), 
            GetStr(achievement_exp),
            GetStr(achievement_lvs) 
        };
    }

    public override string[] Get_Update_Character()
    {
        return new string[]
        {
            "achievement_exp",
            "achievement_lvs"
        };

    }
    public override string[] Set_Uptade_String()
    {
        return new string[]
        {
        GetStr(data_achievement_value(user_achievements)),
        GetStr(data_achievement_value(user_achievements_lv))
        };
    }

   /// <summary>
   /// 转化数据
   /// </summary>
   /// <param name="data"></param>
   /// <returns></returns>
    private string data_achievement_value(Dictionary<string,int> data)
    {
        string value = "";
        foreach (var item in data)
        {
            value+=(value == "" ? "" : "|") + item.Key + " " + item.Value;
        }
        return value;
    }
}
