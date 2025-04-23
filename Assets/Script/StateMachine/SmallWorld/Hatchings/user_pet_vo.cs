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
    /// 宠物信息 0宠物名字 1孵化时间  2宠物品质 3宠物等级 4宠物经验 5宠物属性 6 pos 他在干什么0闲置1守护庄园2是探索
    /// </summary>
    public List<string> crt_pet_list = new List<string>();
   


    public void Init()
    {
        string[] pet = pet_value.Split('&');
        for (int i = 0; i < pet.Length; i++)
        {
            crt_pet_list.Add(pet[i]);
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
           GetStr(crt_pet_list),
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
