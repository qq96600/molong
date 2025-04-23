using Common;
using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class user_pet_vo : Base_VO
{
    /// <summary>
    /// 已孵化宠物信息
    /// </summary>
    public string pet_value;
    /// <summary>
    /// 已孵化宠物信息 1宠物名字 2宠物等级 3宠物经验
    /// </summary>
    public List<(string, int, int)> pet_bag=new List<(string, int, int)>();
    /// <summary>
    /// 宠物信息 0宠物名字 1孵化时间  2宠物品质 3宠物等级 4宠物经验 5宠物属性 6 pos 他在干什么0闲置1守护庄园2是探索
    /// </summary>
    public List<string> crt_pet_list = new List<string>();
   


    public void Init()
    {
        string[] pet = pet_value.Split('&');
        for (int i = 0; i < pet.Length; i++)
        {
            crt_pet_list.Add(pet[i]);
            string[] pet_info = pet[i].Split(',');
            if (pet_info.Length == 3)
            {
                pet_bag.Add((pet_info[0], Convert.ToInt32(pet_info[1]), Convert.ToInt32(pet_info[2])));
            }
        }
    }


    public string Set_pet_value()
    {
        string dec = "";
        for(int i = 0; i < crt_pet_list.Count; i++)
        {
            if(i>0)
            {
                dec += "&";
            }
            dec += crt_pet_list[i];
        }

        return dec;

    }


    public override string[] Set_Instace_String()
    {
        return new string[]
        {
           GetStr(0),
           GetStr(SumSave.crt_user.uid),
           GetStr(pet_value),
        };
    }

    public override string[] Get_Update_Character()
    {
        return new string[]
        {
            "user_value",
        };
    }


    public override string[] Set_Uptade_String()
    {
        return new string[]
        {
            GetStr(Set_pet_value()),
        };
    }

}
