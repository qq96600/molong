using Common;
using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class user_achievement_vo : Base_VO
{
    /// <summary>
    /// ����uid
    /// </summary>
    public string uid;
    /// <summary>
    /// ��
    /// </summary>
    public int par;
    /// <summary>
    /// ����ֵ
    /// </summary>
    public string achievement_exp;
    /// <summary>
    /// �ȼ�
    /// </summary>
    public string achievement_lvs;

    /// <summary>
    /// ����ɾ��Լ��ɾ͵ȼ�
    /// </summary>
    private Dictionary<string, int> user_achievements = new Dictionary<string, int>();
    private Dictionary<string, int> user_achievements_lv = new Dictionary<string, int>();
  

    /// <summary>
    /// ��ȡ����ֵ
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, int> Set_Exp()
    {
        return user_achievements;
    }
    /// <summary>
    /// ��ȡ�ȼ�
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, int> Set_Lv()
    {
        return user_achievements_lv;
    }

    /// <summary>
    /// �������
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
}
