using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 累积奖励预制体
/// </summary>
public class db_Accumulatedrewards_vo : Base_VO
{
    public string pass_value;
    public string signin_value;
    public List<(int,string)> pass_list = new List<(int, string)>();
    public List<(int,string)> signin_list = new List<(int, string)>();
    public void Init()
    { 
        string[] pass = pass_value.Split('*');
        for (int i = 0; i < pass.Length; i++)
        {
            if (pass[i] != "")
            { 
                string[] pass2 = pass[i].Split('|');
                pass_list.Add((int.Parse(pass2[0]), pass2[1]));
            }
        }
        string[] signin = signin_value.Split('*');
        for (int i = 0; i < signin.Length; i++)
        {
            if (signin[i] != "")
            { 
                string[] signin2 = signin[i].Split('|');
                signin_list.Add((int.Parse(signin2[0]), signin2[1]));
            }
        }

    }
}
